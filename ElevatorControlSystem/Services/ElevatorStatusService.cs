using ElevatorControlSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorControlSystem.Models
{
    public class ElevatorStatusService
    {
        public ElevatorLiftingDirection GetLiftingDirection(int nextFloor, int currentFloor)
        {
            if (nextFloor == currentFloor) return ElevatorLiftingDirection.Still;

            return nextFloor > currentFloor ? ElevatorLiftingDirection.Up : ElevatorLiftingDirection.Down;
        }

        public int GetEstimatedTimeForFloor(List<int> floorQueue, int currentFloor, int targetFloor, int timeBetweenFloorsInSeconds)
        {
            var floorPlacementInQueueIndex = floorQueue.IndexOf(targetFloor);

            if (floorPlacementInQueueIndex == -1)
            {
                throw new ElevatorStatusServiceException("Target floor is not in queue!");
            }

            var floorDestinationsToIncludeInTimeEstimate = floorQueue.Take(floorPlacementInQueueIndex + 1).ToList();

            floorDestinationsToIncludeInTimeEstimate.Insert(0, currentFloor);

            var estimatedTimeOfArrival = 0;

            foreach (var floor in floorDestinationsToIncludeInTimeEstimate)
            {
                var indexOfNextFloor = floorDestinationsToIncludeInTimeEstimate.IndexOf(floor) + 1;

                if (indexOfNextFloor + 1 > floorDestinationsToIncludeInTimeEstimate.Count) break;

                var nextFloor = floorDestinationsToIncludeInTimeEstimate[indexOfNextFloor];

                var floorsBetween = Math.Abs(nextFloor - floor);

                estimatedTimeOfArrival += floorsBetween * timeBetweenFloorsInSeconds;
            }

            return estimatedTimeOfArrival;
        }
    }
}
