using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewPrep.Core
{
    public class ElevatorSystem
    {
        public int MinFloor { get; }
        public int MaxFloor { get; }
        public List<Elevator> Elevators { get; private set; }
        public List<ElevatorRequest> ElevatorRequests { get; private set; }

        public int MaxElevators { get; }
        public int MaxCapacity { get; }

        public ElevatorSystem(int minFloor, int maxFloor, int maxElevators, int maxCapacity = 2000)
        {
            if (minFloor < 1) throw new ArgumentNullException(nameof(minFloor));
            if (maxFloor < 1) throw new ArgumentNullException(nameof(maxFloor));
            if (maxElevators < 1) throw new ArgumentNullException(nameof(maxElevators));
            if (maxCapacity < 1) throw new ArgumentNullException(nameof(maxCapacity));

            MinFloor = minFloor;
            MaxFloor = maxFloor;
            MaxElevators = maxElevators;
            MaxCapacity = maxCapacity;
            InitializeElevators();
        }

        private void InitializeElevators()
        {
            Elevators = new List<Elevator>();
            for (var i = 0; i < MaxElevators; i++)
            {
                Elevators.Add(new Elevator(i + 1, MaxCapacity));
            }
        }

        public void RequestElevator(int requestedFloor)
        {
            if (requestedFloor < 1) throw new ArgumentException("Requested floor must be greater than 0");

            Elevator bestPick = null;
            foreach (var elevator in Elevators)
            {
                if (bestPick == null)
                    bestPick = elevator;
                if (!elevator.InTransit && bestPick.InTransit)
                    bestPick = elevator;
                if (elevator.GetDistance(requestedFloor) < bestPick.GetDistance(requestedFloor))
                    bestPick = elevator;
            }
            if (bestPick == null) throw new InvalidOperationException("An error occured determining best elevator pick");

            RouteElevator(bestPick, requestedFloor);
        }

        public void RouteElevator(Elevator elevator, int destintationFloor)
        {
            if (elevator == null) throw new ArgumentNullException(nameof(elevator));

            var request = new ElevatorRequest(elevator.Id, destintationFloor);
            ElevatorRequests.Add(request);
            elevator.QueueRequest(request);
        }

        public void Run()
        {
            while (true)
            {
                foreach (var elevator in Elevators)
                {
                    elevator.HandleRequest();
                }
                Task.Delay(TimeSpan.FromSeconds(5)).Wait();
            }
        }
    }

    public class Elevator
    {
        public int Id { get; }
        public int CurrentCapacity { get; private set; }
        public int MaxCapacity { get; }
        public int CurrentFloor { get; private set; } = 1;
        public Queue<ElevatorRequest> Requests { get; private set; }
        public ElevatorRequest CurrentRequest { get; set; }
        public bool InTransit
        {
            get
            {
                if (CurrentRequest == null) return false;
                if (CurrentRequest.Completed) return false;
                return true;
            }
        }


        public Elevator(int id, int maxCapacity)
        {
            if (id < 1) throw new ArgumentNullException(nameof(id));
            if (maxCapacity < 1) throw new ArgumentNullException(nameof(maxCapacity));
            Id = id;
            MaxCapacity = maxCapacity;
            Requests = new Queue<ElevatorRequest>();
        }

        public int GetDistance(int destinationFloor)
        {
            return Math.Abs(CurrentFloor - destinationFloor);
        }

        public void QueueRequest(ElevatorRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            Requests.Enqueue(request);
        }

        public void HandleRequest()
        {
            if (InTransit) return;
            CurrentRequest = Requests.Dequeue();
            MoveToFloor(CurrentRequest.Floor);
        }

        private void MoveToFloor(int floor)
        {
            if (floor < 1) throw new ArgumentNullException(nameof(floor));

            CurrentFloor = floor;
        }

        private void WaitAndLoad()
        {
            Task.Delay(TimeSpan.FromSeconds(5)).Wait();
            const int capacity = 500;
            CurrentCapacity = capacity;
            if (CurrentCapacity > MaxCapacity)
                throw new InvalidOperationException("Too much weight, please remove people");


        }
    }

    public class ElevatorRequest
    {
        public int ElevatorId { get; }
        public int Floor { get; }
        public bool Completed { get; private set; }

        public ElevatorRequest(int elevatorId, int floor)
        {
            if (elevatorId < 1) throw new ArgumentNullException(nameof(elevatorId));
            if (floor < 1) throw new ArgumentNullException(nameof(floor));

            ElevatorId = elevatorId;
            Floor = floor;
        }

        public void CompleteRequest(DateTime timestamp)
        {

        }
    }
}