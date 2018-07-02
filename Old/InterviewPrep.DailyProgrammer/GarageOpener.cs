using System;
using System.Collections.Generic;

namespace InterviewPrep.DailyProgrammer
{
    public class GarageOpener
    {
        private const string ButtonClicked = "button_clicked";
        private const string CycleComplete = "cycle_complete";
        private const string BlockDetected = "block_detected";
        private const string BlockCleared = "block_cleared";
        private readonly IList<string> _inputCommands;
        private GarageState _garageState;

        public GarageOpener()
        {
            _garageState = GarageState.Closed;
            Console.WriteLine(_garageState);
            _inputCommands = new List<string> { "button_clicked", "cycle_complete", "button_clicked", "block_detected", "button_clicked",
                "cycle_complete", "button_clicked", "block_cleared", "button_clicked", "cycle_complete" };
        }

        public void Play()
        {
            foreach (var command in _inputCommands)
            {
                switch (command)
                {
                    case ButtonClicked:
                        HandleButtonClicked();
                        break;
                    case CycleComplete:
                        HandleCycleComplete();
                        break;
                    case BlockDetected:
                        HandleBlockDetected();
                        break;
                    case BlockCleared:
                        HandleBlockCleared();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(command), "error in switch statement, key not found");
                }
                Console.WriteLine(_garageState);
            }
        }

        private void HandleButtonClicked()
        {
            switch (_garageState)
            {
                case GarageState.Closed:
                    _garageState = GarageState.Opening;
                    break;
                case GarageState.Open:
                    _garageState = GarageState.Closing;
                    break;
                case GarageState.Closing:
                    _garageState = GarageState.ClosingStopped;
                    break;
                case GarageState.Opening:
                    _garageState = GarageState.OpeningStopped;
                    break;
                case GarageState.ClosingStopped:
                    _garageState = GarageState.Opening;
                    break;
                case GarageState.OpeningStopped:
                    _garageState = GarageState.Closing;
                    break;
                case GarageState.OpeningBlocked:
                    break;
                case GarageState.OpenBlocked:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_garageState), "error in switch statement, key not found");
            }
        }

        private void HandleCycleComplete()
        {
            switch (_garageState)
            {
                case GarageState.Closing:
                    _garageState = GarageState.Closed;
                    break;
                case GarageState.Opening:
                    _garageState = GarageState.Open;
                    break;
                case GarageState.OpeningBlocked:
                    _garageState = GarageState.OpenBlocked;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_garageState), "error in switch statement, key not found");
            }
        }

        private void HandleBlockDetected()
        {
            switch (_garageState)
            {
                case GarageState.Closing:
                    _garageState = GarageState.OpeningBlocked;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_garageState), "error in switch statement, key not found");
            }
        }

        private void HandleBlockCleared()
        {
            switch (_garageState)
            {
                case GarageState.OpeningBlocked:
                    _garageState = GarageState.Opening;
                    break;
                case GarageState.OpenBlocked:
                    _garageState = GarageState.Open;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_garageState), "error in switch statement, key not found");
            }
        }
    }

    public enum GarageState
    {
        Open,
        Closed,
        Opening,
        Closing,
        ClosingStopped,
        OpeningStopped,
        OpenBlocked,
        OpeningBlocked
    }
}
