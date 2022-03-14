using ElevatorControlSystem.Models;
using ElevatorControlSystem.Models.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ElevatorControlSystem.Tests.Services
{
    [TestClass]
    public class ElevatorStatusServiceTests
    {
        [TestMethod]
        public void ShouldGetCorrectEstimatedTimeForTargetFloor()
        {
            //Arrange
            var sut = new ElevatorStatusService();

            var floorQueue = new List<int>
            {
                2, 4, 6, 5
            };

            //Act & Assert
            Assert.AreEqual(12, sut.GetEstimatedTimeForFloor(floorQueue, 0, 4, 3));
            Assert.AreEqual(12, sut.GetEstimatedTimeForFloor(floorQueue, 1, 5, 2));
        }

        [TestMethod]
        public void ShouldThrowExceptionIfTargetFloorIsNotInQueue()
        {
            //Arrange
            var sut = new ElevatorStatusService();

            var floorQueue = new List<int>
            {
                2, 4, 6, 5
            };

            //Act & Assert
            Assert.ThrowsException<ElevatorStatusServiceException>(() => sut.GetEstimatedTimeForFloor(floorQueue, 0, 7, 3));
        }

        [TestMethod]
        public void ShouldReturnCorrectLiftingDirection()
        {
            //Arrange
            var sut = new ElevatorStatusService();

            //Act & Assert
            Assert.AreEqual(ElevatorLiftingDirection.Up, sut.GetLiftingDirection(5, 2));
            Assert.AreEqual(ElevatorLiftingDirection.Down, sut.GetLiftingDirection(-1, 1));
            Assert.AreEqual(ElevatorLiftingDirection.Still, sut.GetLiftingDirection(1, 1));
        }
    }
}
