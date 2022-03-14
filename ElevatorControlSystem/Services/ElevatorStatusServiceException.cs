using System;
using System.Runtime.Serialization;

namespace ElevatorControlSystem.Models
{
    [Serializable]
    public class ElevatorStatusServiceException : Exception
    {
        public ElevatorStatusServiceException(string message) : base(message)
        {
        }
    }
}