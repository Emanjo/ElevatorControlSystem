using ElevatorControlSystem.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorControlSystem.Models
{
    public class ElevatorControl
    {
        private const int TimeBetweenFloorsInSeconds = 3;

        public List<Floor> FloorQueue { get; }
        public ElevatorStatus Status { get; }
        public ElevatorCarLiftingDirection LiftingDirection { get; }

        public void EnqueueFloor(Floor floor)
        {
            var floorAlreadyEnqueued = FloorQueue.Any(queueElement => queueElement == floor);

            if (floorAlreadyEnqueued) return;

            FloorQueue.Add(floor);
        }

        public int GetEstimatedTimeForFloor(Floor floor)
        {
            var floorPlacementInQueue = FloorQueue.FirstOrDefault(queueElement => queueElement == floor);

            if (floorPlacementInQueue is null) return 0;

            var selectedFloorIndexInQueue = FloorQueue.IndexOf(floorPlacementInQueue);

            var floorsBeforeSelectedFloor = FloorQueue.Take(selectedFloorIndexInQueue + 1).ToList();

            var estimatedTimeOfArrival = 0;

            foreach (var queuedFloor in floorsBeforeSelectedFloor)
            {
                var indexOfNextFloor = floorsBeforeSelectedFloor.IndexOf(queuedFloor) + 1;

                if (indexOfNextFloor > floorsBeforeSelectedFloor.Count) break;

                var nextFloor = floorsBeforeSelectedFloor[indexOfNextFloor];

                var floorsBetween = queuedFloor.Number + nextFloor.Number;

                estimatedTimeOfArrival += floorsBetween * TimeBetweenFloorsInSeconds;
            }

            return estimatedTimeOfArrival;
        }
    }
}
