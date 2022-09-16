using Loto3000.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Loto3000.UnitTests.DomainTests.PlayerTests
{
    [TestClass]
    public class SetVerificationAndForgotPasswordCodeTests
    {
        [TestMethod]
        public void SetForgotPasswordCode_WithCode_SetsForgotPasswordCodeCreatedProperty()
        {
            // Arrange
            var player = new Player();
            var code = "code";

            // Act
            player.SetForgotPasswordCode(code);

            //Assert
            Assert.IsNotNull(player.ForgotPasswordCodeCreated);
        }

        [TestMethod]
        public void SetForgotPasswordCode_WithCode_SetsForgotPasswordCodeProperty()
        {
            // Arrange
            var player = new Player();
            var code = "code";

            // Act
            player.SetForgotPasswordCode(code);

            //Assert
            Assert.AreEqual(code, player.ForgotPasswordCode);
        }

        [TestMethod]
        public void SetVerificationCode_WithCode_SetsForgotPasswordCodeCreatedProperty()
        {
            // Arrange
            var player = new Player();
            var code = "code";

            //Act
            player.SetVerificationCode(code);

            //Assert
            Assert.IsNotNull(player.VerificationCode);
        }

        [TestMethod]
        public void SetVerificationCode_WithCode_SetsForgotPasswordCodeProperty()
        {
            // Arrange
            var player = new Player();
            var code = "code";

            // Act
            player.SetVerificationCode(code);

            //Assert
            Assert.AreEqual(code, player.VerificationCode);
        }
    }
}