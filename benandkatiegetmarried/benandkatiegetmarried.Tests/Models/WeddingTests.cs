using benandkatiegetmarried.Models;
using System;
using Xunit;

namespace benandkatiegetmarriedTests.Models
{
    public class WeddingTests
    {
        [Fact]
        public void Wedding_CanBeCreated_withBrideAndGroom()
        {
            Wedding wedding = new Wedding() { Bride = "Bride", Groom = "Groom" };
            Assert.Equal("Bride", wedding.Bride);
            Assert.Equal("Groom", wedding.Groom);
        }

        [Fact]
        public void Wedding_CannotHaveAStartDateBeforeTodaysDate()
        {
            var wedding = new Wedding();
            Action exec = () => wedding.SetDates(DateTime.Now.AddDays(-1), DateTime.Now);
            Assert.Throws<ArgumentException>(exec);
        }
        [Fact]
        public void Wedding_CannotHaveAnEndDateEarlierThanAStartDate()
        {
            var wedding = new Wedding();
            Action exec = () => wedding.SetDates(DateTime.Now.AddDays(1), DateTime.Now.AddDays(-1));
            Assert.Throws<ArgumentException>(exec);
        }
    }
}
