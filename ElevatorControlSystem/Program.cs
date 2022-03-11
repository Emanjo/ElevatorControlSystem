using ElevatorControlSystem.Models;
using System;
using System.Collections.Generic;

namespace ElevatorControlSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var elevatorControl = new ElevatorControlService(3);

            do
            {
                bool shouldEnterMoreFloors;
                do
                {
                    Console.WriteLine("Enter floor or press enter to continue:");
                    var choice = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(choice)) break;

                    if (!int.TryParse(choice, out int choiseAsInt)) 
                    {
                        Console.WriteLine("Wrong format of floor!");
                        shouldEnterMoreFloors = true;
                        continue;
                    }

                    elevatorControl.EnqueueFloor(choiseAsInt);
                    shouldEnterMoreFloors = true;
                }
                while (shouldEnterMoreFloors);

            }
            while (elevatorControl.RunElevator());
        }
    }
}
