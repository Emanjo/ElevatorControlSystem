using ElevatorControlSystem.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElevatorControlSystem.Tests.Services
{
    [TestClass]
    public class ElevatorControlServiceTests
    {
        [TestMethod]
        public void CheckIfGetEstimatedTimeForFloorIsCorrect()
        {
            //Arrange
            var sut = new ElevatorControlService(3);

            sut.EnqueueFloor(1);
            sut.EnqueueFloor(5);
            sut.EnqueueFloor(4);

            //Act
            var result1 = sut.GetEstimatedTimeForFloor(4);
            var result2 = sut.GetEstimatedTimeForFloor(5);

            //Assert
            Assert.AreEqual(15, result1);
            Assert.AreEqual(12, result2);
        }
    }
}
