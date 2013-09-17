using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Speculous.Tests
{
    public class TestCaseTests
    {

        public class FuncGivenMethod : TestCase<string>
        {
            protected override Func<string> Given()
            {
                SampleObject.BaseMessage = "Hello there";

                var sample = new SampleObject();

                return () => sample.GetMessage("test");
            }

            [Fact]
            public void ShouldExecuteSubject()
            {
                Subject().Should().Be("Hello there, test");
            }

            [Fact]
            public void ShouldExecuteSubjectUsingIt()
            {
                It.Should().Be("Hello there, test");
            }

            [Fact]
            public void ShouldExecuteSubjectUsingIts()
            {
                Its.Should().Be("Hello there, test");
            }

            //change context
            public class ChangeContext : TestCase<string>
            {
                protected override Func<string> Given()
                {
                    var sample = new SampleObject();
                    return () => sample.GetMessage("mike");
                }

                [Fact]
                public void ShouldChangeContext()
                {
                    Subject().Should().Be("Hello there, mike");
                }
            }

        }

    }
}
