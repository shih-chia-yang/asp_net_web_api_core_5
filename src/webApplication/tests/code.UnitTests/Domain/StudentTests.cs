using System;
using code.Domain.Entities;
using Xunit;

namespace tests.code.UnitTests.Domain
{
    public class StudentTests
    {
        [Fact]
        public void Test_Add_Student_Should_be_Success()
        {
            //Given
            string lastName = "test";
            string firstName = "domain";
            DateTime enrollmentDate = DateTime.Parse("2021-01-01");
            //When
            var addNew = Student.CreateNew(lastName,firstName,enrollmentDate);
            //Then
            Assert.NotNull(addNew);
        }
    }
}