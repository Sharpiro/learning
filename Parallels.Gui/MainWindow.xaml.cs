using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Parallels.CustomTask;

namespace Parallels.Gui
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void DeadlockButton_Click(object sender, RoutedEventArgs e)
        {
            var uiThread = Thread.CurrentThread.ManagedThreadId;
            //var result = GetThreadAsync().Result;
            var task = GetThreadAsync();
            var x = 2 + 2;
            x++;
            var result = await task;
            MessageBox.Show($"Ui: {uiThread}, Worker: {result}, Work: {x}");
        }

        private async Task<int> GetThreadAsync()
        {
            //return Task.Run(() =>
            //{
            //    Thread.Sleep(2000);
            //    return Thread.CurrentThread.ManagedThreadId;
            //});
            await Task.Delay(2000);
            return Thread.CurrentThread.ManagedThreadId;
            //await Task.Delay(2000).ConfigureAwait(false);
            //await Task.Delay(2000);
        }
    }
}
