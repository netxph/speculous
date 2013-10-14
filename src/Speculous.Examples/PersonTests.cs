using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;
using Moq;

namespace Speculous.Examples
{
    public class PersonTests
    {

        public class ConstructorMethod : TestCase<Person>
        {

            readonly DateTime TODAY = new DateTime(2013, 12, 1);

            protected override void Initialize()
            {
                var dateProvider = new Mock<IDateProvider>();

                Define("DateProvider", () =>
                {
                    dateProvider
                        .Setup(d => d.UtcNow())
                        .Returns(TODAY);

                    return dateProvider.Object;
                });
            }

            protected override void Destroy()
            {
                Person.DateProvider = null;
            }

            protected override Func<Person> Given()
            {
                Person.DateProvider = New<IDateProvider>("DateProvider");
                return () => new Person();
            }

            [Fact]
            public void ShouldNotBeNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldIDIsZero()
            {
                Its.ID.Should().Be(0);
            }

            [Fact]
            public void ShouldNameIsEmpty()
            {
                Its.Name.Should().BeEmpty();
            }

            [Fact]
            public void ShouldAgeIsZero()
            {
                Its.Age.Should().Be(0);
            }

            [Fact]
            public void ShouldCreatedDateIsToday()
            {
                Its.CreatedDateUtc.Should().Be(TODAY);
            }

        }

        public class ConstructorMethod_WithName : TestCase<Person>
        {
            protected override void Initialize()
            {
                UseContext(new ConstructorMethod());
            }

            protected override Func<Person> Given()
            {
                Person.DateProvider = New<IDateProvider>("DateProvider");
                return () => new Person("Marc");
            }

            [Fact]
            public void ShouldNameHasValue()
            {
                Its.Name.Should().Be("Marc");
            }

        }

        public class ProcessMethod : TestCase
        {
            protected override Action Given()
            {
                return () => Person.Process();
            }

            [Fact]
            public void ShouldNotThrowError()
            {
                Subject.ShouldNotThrow();
            }
        }


    }
}
