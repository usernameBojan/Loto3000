using Loto3000.Domain.Entities;
using Loto3000.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.UnitTests.DomainTests.TicketTests
{
    [TestClass]
    public class CombinationGeneratorTests
    {
        [TestMethod]
        public void CombinationGenerator_GetsMoreNumbersThanAllowedFromFrontend_ThrowsException()
        {
            // Arrange
            var ticket = new Ticket();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8};

            // Act & Assert
            Assert.ThrowsException<ValidationException>(() => ticket.CombinationGenerator(nums));
        }

        [TestMethod]
        public void CombinationGenerator_GetsSameNumberMoreThanOnceInSequence_ThrowsException()
        {
            // Arrange
            var ticket = new Ticket();
            int[] nums = { 1, 1, 3, 4, 5, 6, 7};

            // Act & Assert
            Assert.ThrowsException<ValidationException>(() => ticket.CombinationGenerator(nums));
        }

        [TestMethod]
        public void CombinationGenerator_GetsHigherOrLowerNumber_ThrowsException()
        {
            // Arrange
            var ticket = new Ticket();
            int[] nums = { 1, 2, 3, 4, 5, 6, 44 };

            // Act & Assert
            Assert.ThrowsException<ValidationException>(() => ticket.CombinationGenerator(nums));
        }
    }
}
