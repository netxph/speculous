#spec.ulous

Lightweight, simple BDD style of testing in .NET. Sits on top of your favorite xUnit framework. RSpec inspired.

This project is sort of "scratch your own itch". What I don't like about other framework is they try too hard to mimic RSpec behavior. This project aims to combine the simplicity of RSpec and C# idioms and practices.

##How to use


Simple (Note: this uses xUnit and FluentAssertions)
```c#
    public class MyServiceTests
    {
        public class GetPersonMethod : TestCase<Person>
        {
            public Func<Person> Given()
            {
                var service = new MyService();
                return () => service.GetPerson(1);
            }
            
            [Fact]
            public void ShouldNotBeNull()
            {
                Subject().Should().NotBeNull();
            }
            
            [Fact]
            public void ShouldIDHasValue()
            {
                Its.ID.Should().Be(1);
            }
        }
    }
```
Changing the Subject
```c#
    public class MyServiceTests
    {
        public class GetPersonMethod : TestCase<Person>
        {
            public Func<Person> Given()
            {
                DbFactory.Initialize();
                Cache.Initialize();
                
                var service = new MyService();
                return () => service.GetPerson(1);
            }
            
            [Fact]
            public void ShouldIDHasValue()
            {
                Its.ID.Should().Be(1);
            }
            
            public class PersonNotFound : TestCase<Person>
            {
                public Func<Person> Given()
                {
                    //at this point DbFactory and Cache are already initialized
                    
                    var service = new MyService();
                    return () => service.GetPerson(99);
                }
                
                [Fact]
                public void ShouldThrowPersonNotFound()
                {
                    Action act = Subject;
                    act.ShouldThrow<Exception>()
                        .WithMessage("Person not found.");
                }
            }
        }
    }
```
Initializers and Destroyers
```c#
    public class MyServiceTests
    {
        public class GetPersonMethod : TestCase<Person>
        {
            public Func<Person> Given()
            {
                With(() => Db.Initialize(),
                     () => Db.Dispose());
                     
                var service = new MyService();
                return () => service.GetPerson(1);
            }
            
            [Fact]
            public void ShouldNotBeNull()
            {
                Subject().Should().NotBeNull();
            }

        }
    }
```
## License
spec.ulous is released under the [MIT License](http://www.opensource.org/licenses/MIT).
