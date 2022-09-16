using Loto3000.Domain.Entities;
using Loto3000.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Loto3000.UnitTests.DomainTests.PlayerTests
{
    [TestClass]
    public class CreateTicketTests
    {
        [TestMethod]
        public void CreateTicket_WithNoActiveDraws_ThrowsException()
        {
            //Arrange
            var player = new Player() { Credits = 25 };
            var draw = new Draw();
            draw = null;
            int[] nums = { 1, 2, 3, 4, 5, 6, 7 };

            // Act and Assert
            Assert.ThrowsException<NotFoundException>(() => player.CreateTicket(nums, draw));
        }

        [TestMethod]
        public void CreateTicket_CreditsLowerThanTicketPrice_ThrowsException()
        {
            // Arrange
            var player = new Player() { Credits = 10 };
            var draw = new Draw();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7 };

            // Act and Assert
            Assert.ThrowsException<ValidationException>(() => player.CreateTicket(nums, draw));
        }

        [TestMethod]
        public void CreateTicket_ReturnsTicket()
        {
            // Arrange
            var player = new Player() { Credits = 25 };
            var draw = new Draw();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7 };

            // Act
            var ticket = player.CreateTicket(nums, draw);

            // Assert
            Assert.IsNotNull(ticket);
        }
    }
}