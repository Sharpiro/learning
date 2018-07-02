using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewPrep.Core
{
    public class ElevatorSystem
    {
        public List<Elevator> Elevators { get; set; } = new List<Elevator> { new Elevator { Id = 1 }, new Elevator { Id = 2 } };
        public Queue<int> FloorQueue { get; set; } = new Queue<int>();
        public int MaxFloors => 5;

        public async Task Run(List<int> floors)
        {
            for (var i = 0; i < floors.Count;)
            {
                if (Elevators.Any(e => e.IsAvailable))
                {
                    var elevator = Elevators.First(e => e.IsAvailable);
                    var elevatorGoToTask = elevator.GoToFloor(floors[i]);
                    i++;
                }
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        public async Task Run()
        {
            while (true)
            {
                if (FloorQueue.Any() && Elevators.Any(e => e.IsAvailable))
                {
                    var floor = FloorQueue.Dequeue();
                    var elevator = Elevators.First(e => e.IsAvailable);
                    var elevatorGoToTask = elevator.GoToFloor(floor);
                }
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        public void QueueFloor(int floor)
        {
            FloorQueue.Enqueue(floor);
            Console.WriteLine($"Queued floor: {floor}");
        }
    }

    public class Elevator
    {
        public int Id { get; set; }
        public int CurrentFloor { get; set; } = 1;
        public bool IsAvailable { get; set; } = true;

        public async Task GoToFloor(int floor)
        {
            Console.WriteLine($"Elevator {Id} going to floor {floor}");

            IsAvailable = false;
            var movingUp = floor > CurrentFloor;
            var floorDif = Math.Abs(CurrentFloor - floor);
            for (var i = 1; i <= floorDif; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
                var previousFloor = CurrentFloor;
                CurrentFloor = movingUp ? ++CurrentFloor : --CurrentFloor;
                Console.WriteLine($"Elevator {Id} went from {previousFloor} to {CurrentFloor}");
            }

            Debug.Assert(CurrentFloor == floor);
            Console.WriteLine($"Elevator {Id} arrived at floor {CurrentFloor}");
            IsAvailable = true;
        }
    }
}