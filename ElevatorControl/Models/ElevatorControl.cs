using System.Collections.Generic;
using System.Linq;

namespace ElevatorControl.Models
{
    public class ElevatorControl
    {
        public int CurrentFloor { get; set; }
        public Queue<int> FloorQueue { get; }
        public ElevatorCarStatus Status { get; }

        public ElevatorCarLiftingDirection LiftingDirection { get; }

        public void GoToFloor(int floor)
        {
            if(!FloorQueue.Any())
            {
                FloorQueue.Enqueue(floor);
            }

            if(CurrentFloor > floor)
            {

            }

        }
    }
}
