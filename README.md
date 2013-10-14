#spec.ulous

Lightweight, simple BDD style of testing in .NET. Sits on top of your favorite xUnit framework. RSpec inspired.

This project is sort of "scratch your own itch". What I don't like about other framework is they try too hard to mimic RSpec behavior. This project aims to combine the simplicity of RSpec and C# idioms and practices.

##Principle

This project doesn't aim to have a cutting edge framework but rather helps you organize on how you do tests. Below are my guiding principle:

* Serves as documentation, there should be one place where I could see how to use the subject in test
* DRY (Don't repeat yourself), one location to define the subject and items that supports it
* Subject-centric, there should be one subject per class that should be tested, this it to avoid mixing different test scenarios
* One assertion per test, assertion items are defined mostly in one line, in clear and easy to understand context.
* Separation of Subject and Non-subject elements (i.e. mocks and stubs)

##General Guidelines

* Use Given to define your subject
* Use Initialize to define non-subject elements
* Use Destroy to clean up resources
* Naming conventions (my preference)
    * Class name are in Pascal casing using the format [Method Name]Method_[Context Variation]
        * Example: GetPersonMethod, GetPersonMethod_WithName
    * Assertions uses format Should[Field]Is(Not)[State]
        * Example: ShouldNameIsEmpty, ShouldNotBeNull, ShouldIDIsZero

##How to use

Simple (Note: this uses xUnit and FluentAssertions)
```c#
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
        public void ShouldNameIsEmpty()
        {
            Its.Name.Should().BeEmpty();
        }
        
        [Fact]
        public void ShouldCreatedDateIsToday()
        {
            Its.CreatedDateUtc.Should().Be(TODAY);
        }
    }

```
Inheriting "Initialization" from other test case
```c#
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
```
For test context that returns void
```c#
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
```

## License
spec.ulous is released under the [MIT License](http://www.opensource.org/licenses/MIT).

