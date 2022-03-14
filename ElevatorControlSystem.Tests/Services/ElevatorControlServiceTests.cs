using ElevatorControlSystem.Models;
using ElevatorControlSystem.Models.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElevatorControlSystem.Tests.Services
{
    [TestClass]
    public class ElevatorControlServiceTests
    {
        [TestMethod]
        public void ShouldGoToNextFloor()
        {
            //Arrange
            var sut = new ElevatorControlService(new ElevatorStatusService());

            sut.EnqueueFloor(2);

            sut.GoToNextFloor();

            //Act & Assert
            Assert.AreEqual(2, sut.CurrentFloor);
        }

        [TestMethod]
        public void ShouldSetCorrectStatusWhenEmeregncyStop()
        {
            //Arrange
            var sut = new ElevatorControlService(new ElevatorStatusService());

            sut.EnqueueFloor(2);

            sut.GoToNextFloor();

            sut.EmergencyStop();

            //Act & Assert
            Assert.AreEqual(2, sut.CurrentFloor);
            Assert.AreEqual(ElevatorStatus.Emergency, sut.Status);
        }

        [TestMethod]
        public void ShouldNotGoToNextFloorWhenInEmergency()
        {
            //Arrange
            var sut = new ElevatorControlService(new ElevatorStatusService());

            sut.EnqueueFloor(2);
            sut.EnqueueFloor(3);

            sut.GoToNextFloor();

            sut.EmergencyStop();

            sut.GoToNextFloor();

            //Act & Assert
            Assert.AreNotEqual(3, sut.CurrentFloor);
        }
    }
}
