using ElevatorControlSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorControlSystem.Models
{
    public class ElevatorControlService
    {
        private readonly int _timeBetweenFloorsInSeconds;

        private List<int> FloorQueue = new List<int>();
        private int CurrentFloor = 0;
        public ElevatorStatus Status { get; private set; } = ElevatorStatus.Stopped;
        public ElevatorCarLiftingDirection LiftingDirection { get; private set; } = ElevatorCarLiftingDirection.None;

        public ElevatorControlService(int timeBetweenFloorsInSeconds)
        {
            _timeBetweenFloorsInSeconds = timeBetweenFloorsInSeconds;
        }

        public bool RunElevator()
        {
            if(FloorQueue.Count > 0)
            {
                var nextFloor = FloorQueue.FirstOrDefault();

                Console.WriteLine($"Going to floor {nextFloor}...");

                CurrentFloor = nextFloor;

                return FloorQueue.Remove(nextFloor);
            }

            return false;
        }

        public void EnqueueFloor(int floor)
        {
            var floorAlreadyEnqueued = FloorQueue.Any(queueElement => queueElement == floor);

            if (floorAlreadyEnqueued) return;

            FloorQueue.Add(floor);
        }

        public int GetEstimatedTimeForFloor(int floor)
        {
            var floorPlacementInQueueIndex = FloorQueue.IndexOf(floor);

            if (floorPlacementInQueueIndex == -1 || floorPlacementInQueueIndex + 1 > FloorQueue.Count) return 0;

            var floorDestinationsToIncludeInTimeEstimate = FloorQueue.Take(floorPlacementInQueueIndex + 1).ToList();

            var estimatedTimeOfArrival = 0;

            foreach (var currentFloor in floorDestinationsToIncludeInTimeEstimate)
            {
                var indexOfNextFloor = floorDestinationsToIncludeInTimeEstimate.IndexOf(currentFloor) + 1;

                if (indexOfNextFloor + 1 > floorDestinationsToIncludeInTimeEstimate.Count) break;

                var nextFloor = floorDestinationsToIncludeInTimeEstimate[indexOfNextFloor];

                var floorsBetween = Math.Abs(nextFloor - currentFloor);

                estimatedTimeOfArrival += floorsBetween * _timeBetweenFloorsInSeconds;
            }

            return estimatedTimeOfArrival;
        }

        public void EmergencyStop()
        {
            Status = ElevatorStatus.Stopped;
            LiftingDirection = ElevatorCarLiftingDirection.None;

            FloorQueue.Clear();
        }
    }
}
