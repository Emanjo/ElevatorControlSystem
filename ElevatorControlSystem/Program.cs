using ElevatorControlSystem.Models;
using System;

namespace ElevatorControlSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var elevatorControl = new ElevatorControlService(3);

            elevatorControl.EnqueueFloor(1);
            elevatorControl.EnqueueFloor(4);
            elevatorControl.EnqueueFloor(2);
            elevatorControl.EnqueueFloor(0);

            var estimatedTimeForFloor = elevatorControl.GetEstimatedTimeForFloor(2);

            Console.WriteLine(estimatedTimeForFloor);
        }
    }
}
