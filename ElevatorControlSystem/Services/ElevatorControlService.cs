using ElevatorControlSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorControlSystem.Models
{
    public class ElevatorControlService
    {
        public int CurrentFloor { get; private set; } = 0;
        public ElevatorStatus Status { get; private set; } = ElevatorStatus.Stopped;
        
        private readonly ElevatorStatusService _elevatorStatusService;
        private readonly int _timeBetweenFloorsInSeconds = 3;
        private List<int> FloorQueue = new List<int>();

        public ElevatorControlService(ElevatorStatusService elevatorStatusService)
        {
            _elevatorStatusService = elevatorStatusService;
        }

        public bool GoToNextFloor()
        {
            if (Status == ElevatorStatus.Emergency) return false;

            if(FloorQueue.Count > 0)
            {
                var nextFloor = FloorQueue.FirstOrDefault();

                Status = ElevatorStatus.Running;

                var liftingDirection = _elevatorStatusService.GetLiftingDirection(nextFloor, CurrentFloor);

                var estimatedTime = _elevatorStatusService.GetEstimatedTimeForFloor(FloorQueue, 
                    CurrentFloor,
                    nextFloor, 
                    _timeBetweenFloorsInSeconds);

                Console.WriteLine($"Going {liftingDirection.ToString().ToLower()} to floor {nextFloor}... ETA: {estimatedTime} second(s)");

                Console.WriteLine($"Arrived!");

                CurrentFloor = nextFloor;

                if(FloorQueue.Remove(nextFloor))
                {
                    Status = ElevatorStatus.Stopped;
                    return true;
                }
            }

            Status = ElevatorStatus.Stopped;
            return false;
        }

        public void EnqueueFloor(int floor)
        {
            if (floor == CurrentFloor) return;

            if (Status == ElevatorStatus.Emergency) return;

            var floorAlreadyEnqueued = FloorQueue.Any(queueElement => queueElement == floor);

            if (floorAlreadyEnqueued) return;

            FloorQueue.Add(floor);
        }

        public void EmergencyStop()
        {
            Status = ElevatorStatus.Emergency;
            FloorQueue.Clear();
        }
    }
}
