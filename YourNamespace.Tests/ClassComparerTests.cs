using System;
using System.IO;
using System.Linq;
using Xunit;

namespace YourNamespace.Tests
{
    public class ClassComparerTests
    {
        [Fact]
        public void CompareClasses_ShouldIdentifyMissingProperties()
        {
            // Arrange
            var person = new Person { Name = "John", Age = 30 };
            var personDto = new PersonDto { Name = "John" }; // Missing Age property

            var comparer = new ClassComparer();

            // Redirect console output
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            comparer.CompareClasses(person, personDto);

            // Assert
            var consoleOutput = stringWriter.ToString();
            Assert.Contains("Missing property: Age", consoleOutput);
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class PersonDto
    {
        public string Name { get; set; }
        // Age property is intentionally missing
    }
}