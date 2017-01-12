using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Parallels
{
    public static class ParallelExtensions
    {
        /// <summary>
        /// Consider using Nito.AsyncEx nuget package which builds on Jon Skeet's implementation
        /// 
        /// Returns a sequence of tasks which will be observed to complete with the same set 
        /// of results as the given input tasks, but in the order in which the original tasks complete. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<Task<T>> OrderByCompletion<T>(this IEnumerable<Task<T>> inputTasks)
        {
            // Copy the input so we know it’ll be stable, and we don’t evaluate it twice 
            var inputTaskList = inputTasks.ToList();
            // Could use Enumerable.Range here, if we wanted… 
            var completionSourceList = inputTaskList.Select(t => new TaskCompletionSource<T>()).ToList();

            // At any one time, this is "the index of the box we’ve just filled". 
            // It would be nice to make it nextIndex and start with 0, but Interlocked.Increment 
            // returns the incremented value… 
            int prevIndex = -1;

            // We don’t have to create this outside the loop, but it makes it clearer 
            // that the continuation is the same for all tasks. 
            Action<Task<T>> continuation = completedTask =>
            {
                int index = Interlocked.Increment(ref prevIndex);
                var source = completionSourceList[index];
                PropagateResult(completedTask, source);
            };

            foreach (var inputTask in inputTaskList)
            {
                inputTask.ContinueWith(continuation,
                                       CancellationToken.None,
                                       TaskContinuationOptions.ExecuteSynchronously,
                                       TaskScheduler.Default);
            }

            return completionSourceList.Select(source => source.Task);
        }

        /// <summary> 
        /// Propagates the status of the given task (which must be completed) to a task completion source 
        /// (which should not be). 
        /// </summary> 
        private static void PropagateResult<T>(Task<T> completedTask,
            TaskCompletionSource<T> completionSource)
        {
            switch (completedTask.Status)
            {
                case TaskStatus.Canceled:
                    completionSource.TrySetCanceled();
                    break;
                case TaskStatus.Faulted:
                    completionSource.TrySetException(completedTask.Exception.InnerExceptions);
                    break;
                case TaskStatus.RanToCompletion:
                    completionSource.TrySetResult(completedTask.Result);
                    break;
                default:
                    // TODO: Work out whether this is really appropriate. Could set 
                    // an exception in the completion source, of course… 
                    throw new ArgumentException("Task was not completed");
            }
        }
    }
}