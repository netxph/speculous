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

        public class FuncDefineGetMethod : TestCase<string>
        {

            protected override void Initialize()
            {
                Define("Message", () => "Hello world!!!");
            }

            protected override Func<string> Given()
            {
                var message = Get<string>("Message");

                return () => message;
            }

            [Fact]
            public void ShouldNotBeNull()
            {
                It.Should().NotBeNull();
            }

            [Fact]
            public void ShouldNotBeEmpty()
            {
                It.Should().NotBeEmpty();
            }

            [Fact]
            public void ShouldHaveMessage()
            {
                It.Should().Be("Hello world!!!");
            }

            public class GetParentTestBag : TestCase<string>
            {

                [Fact]
                public void ShouldHaveMessage()
                {
                    It.Should().Be("Hello world!!!");
                }    

            }

            public class GetParentTestBag_NotInherit : TestCase<string>
            {
                protected override void Initialize()
                {
                    
                }

                [Fact]
                public void ShouldBeNull()
                {
                    It.Should().BeNull();
                }
            }

        }

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

            protected override void Destroy()
            {
                base.Destroy();
            }

            //change context
            public class GetParentContext : TestCase<string>
            {
                [Fact]
                public void ShouldExecuteParentSubject()
                {
                    Subject().Should().Be("Hello there, test");
                }
            }

        }

    }
}
