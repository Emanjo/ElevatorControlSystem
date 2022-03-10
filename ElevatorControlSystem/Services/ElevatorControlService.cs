using ElevatorControlSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorControlSystem.Models
{
    public class ElevatorControlService
    {
        private const int TimeBetweenFloorsInSeconds = 3;

        public List<int> FloorQueue { get; } = new List<int>();
        public ElevatorStatus Status { get; }
        public ElevatorCarLiftingDirection LiftingDirection { get; }

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

            foreach (var queuedFloor in floorDestinationsToIncludeInTimeEstimate)
            {
                var indexOfNextFloor = floorDestinationsToIncludeInTimeEstimate.IndexOf(queuedFloor) + 1;

                if (indexOfNextFloor + 1 > floorDestinationsToIncludeInTimeEstimate.Count) break;

                var nextFloor = floorDestinationsToIncludeInTimeEstimate[indexOfNextFloor];

                var highestFloor = Math.Max(queuedFloor, nextFloor);
                var lowestFloor = Math.Min(queuedFloor, nextFloor);

                var floorsBetween = highestFloor - lowestFloor;

                estimatedTimeOfArrival += floorsBetween * TimeBetweenFloorsInSeconds;
            }

            return estimatedTimeOfArrival;
        }
    }
}
