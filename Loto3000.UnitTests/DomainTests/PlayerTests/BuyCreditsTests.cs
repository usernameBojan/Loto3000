using Loto3000.Domain.Entities;
using Loto3000.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Loto3000.UnitTests.DomainTests.PlayerTests
{
    [TestClass]
    public class BuyCreditsTests
    {
        [TestMethod]
        public void BuyCredits_DepositAmountLowerThanMinimal_ThrowsException()
        {
            // Arrange
            var player = new Player();
            var amount = 4.5;

            //Act and Assert
            Assert.ThrowsException<ValidationException>(() => player.BuyCredits(amount, player.Credits));
        }
    }
}