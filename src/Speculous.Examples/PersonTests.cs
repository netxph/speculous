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

                Define<IDateProvider>(() =>
                {
                    dateProvider
                        .Setup(d => d.UtcNow())
                        .Returns(TODAY);

                    return dateProvider.Object;
                });
            }

            protected override void Destroy()
            {
                var provider = New<IDateProvider>();
                provider.CleanUp();
            }

            protected override Func<Person> Given()
            {
                return () => new Person(New<IDateProvider>());
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
                return () => new Person("Marc", New<IDateProvider>());
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
                var person = new Person();

                return () => person.Process();
            }

            [Fact]
            public void ShouldNotThrowError()
            {
                Subject.ShouldNotThrow();
            }
        }


    }
}
