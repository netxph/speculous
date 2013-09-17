using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Moq;

namespace Speculous.Tests
{
    public class TestCaseTests
    {

        public class OverrideParent : TestCase<string>
        {
            protected override void Initialize()
            {
                Define("Dependent", () =>
                {
                    var dependent = new Mock<IDependentObject>();
                    dependent
                    .Setup(d => d.GetBaseMessage())
                    .Returns("Hello there");

                    return dependent.Object;
                });
            }

            protected override Func<string> Given()
            {
                return null;
            }

            public class WhenImplemented : TestCase<string>
            {
                protected override void Initialize()
                {
                    InheritStore();

                    Define("Dependent", () =>
                    {
                        var dependent = new Mock<IDependentObject>();
                        dependent
                        .Setup(d => d.GetBaseMessage())
                        .Returns("Hi there");

                        return dependent.Object;
                    });
                }

                protected override Func<string> Given()
                {
                    var dependent = Get<IDependentObject>("Dependent");

                    var sample = new SampleObject();
                    sample.Dependent = dependent;

                    return () => sample.GetMessage("test");
                }

                [Fact]
                public void ShouldNotBeNull()
                {
                    It.Should().NotBeNull();
                }

                [Fact]
                public void ShouldHaveMessageOverriden()
                {
                    It.Should().Be("Hi there, test");
                }

            }

        }


        public class InheritParent : TestCase<string>
        {
            protected override void Initialize()
            {
                Define("Dependent", () =>
                {
                    var dependent = new Mock<IDependentObject>();
                    dependent
                    .Setup(d => d.GetBaseMessage())
                    .Returns("Hello there");

                    return dependent.Object;
                });
            }

            protected override Func<string> Given()
            {
                return null;
            }

            public class WhenImplemented : TestCase<string>
            {
                protected override Func<string> Given()
                {
                    var dependent = Get<IDependentObject>("Dependent");

                    var sample = new SampleObject();
                    sample.Dependent = dependent;

                    return () => sample.GetMessage("test");
                }

                [Fact]
                public void ShouldNotBeNull()
                {
                    It.Should().NotBeNull();
                }

                [Fact]
                public void ShouldHaveMessageOverriden()
                {
                    It.Should().Be("Hello there, test");
                }
            }

        }


        public class TestBagProperty : TestCase<string>
        {
            protected override void Initialize()
            {
                Define("Dependent", () =>
                {
                    var dependent = new Mock<IDependentObject>();
                    dependent
                    .Setup(d => d.GetBaseMessage())
                    .Returns("Hello there");

                    return dependent.Object;
                });
            }

            protected override Func<string> Given()
            {
                var dependent = Get<IDependentObject>("Dependent");

                var sample = new SampleObject();
                sample.Dependent = dependent;

                return () => sample.GetMessage("test");
            }

            [Fact]
            public void ShouldNotBeNull()
            {
                It.Should().NotBeNull();
            }

            [Fact]
            public void ShouldHaveMessageOverriden()
            {
                It.Should().Be("Hello there, test");
            }

        }


        public class InitializeMethod : TestCase<string>
        {
            IDependentObject _dependent = null;

            protected override void Initialize()
            {
                var dependent = new Mock<IDependentObject>();
                dependent
                    .Setup(d => d.GetBaseMessage())
                    .Returns("Hello there");

                _dependent = dependent.Object;
            }

            protected override Func<string> Given()
            {
                var sample = new SampleObject();
                sample.Dependent = _dependent;

                return () => sample.GetMessage("test");
            }

            [Fact]
            public void ShouldNotBeNull()
            {
                It.Should().NotBeNull();
            }

            [Fact]
            public void ShouldHaveMessageOverridden()
            {
                Subject().Should().Be("Hello there, test");
            }
        }


        public class GivenMethod : TestCase<string>
        {

            protected override Func<string> Given()
            {
                var sample = new SampleObject();

                return () => sample.GetMessage("test");
            }

            [Fact]
            public void ShouldNotBeNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldNotBeEmpty()
            {
                Subject().Should().NotBeEmpty();
            }

            [Fact]
            public void ShouldHaveValue()
            {
                Subject().Should().Be("Hello, test");
            }

            [Fact]
            public void ShouldRespondToIt()
            {
                It.Should().Be("Hello, test");
            }

            [Fact]
            public void ShouldRespondToIts()
            {
                Its.Should().Be("Hello, test");
            }
        }

    }
}
