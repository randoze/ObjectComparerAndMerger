using Xunit;
using System;
using System.Linq;

namespace SampleTests
{
    public class ObjectMergerTests
    {
        [Fact]
        public void MergeClasses_GeneratesMergedClassCode()
        {
            // Arrange
            var person = new Person { Name = "John", Age = 30 };
            var personDto = new PersonDto { Address = "123 Main St", PhoneNumber = "123-456-7890" };

            // Act
            var mergedClassCode = ObjectMerger.MergeClasses<Person, PersonDto>();

            // Assert
            Assert.Contains("Name", mergedClassCode);
            Assert.Contains("Age", mergedClassCode);
            Assert.Contains("Address", mergedClassCode);
            Assert.Contains("PhoneNumber", mergedClassCode);
        }
    }
}