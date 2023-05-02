using FakeItEasy;
using Xunit;

namespace JhubooSoft.UnitTestingTools.Tests
{
    public class AutoFakedObjectFactoryTests
    {
        [Fact]
        public void AutoFakedObjectFactoryShouldCreateInstanceOfTypeWithParameterlesConstructor()
        {
            //Arrange
            AutoFakedObjectFactory<TestClass1> objectFactory = new AutoFakedObjectFactory<TestClass1>();

            //Act
            TestClass1 objectUnderTest = objectFactory.Create();

            //Assert
            TestExpect.IsInstanceOfType<TestClass1>(objectUnderTest);
        }

        [Fact]
        public void AutoFakedObjectFactoryShouldCreateInstanceOfTypeWithSingleConstructor()
        {
            //Arrange
            AutoFakedObjectFactory<TestClass2> objectFactory = new AutoFakedObjectFactory<TestClass2>();

            //Act
            TestClass2 objectUnderTest = objectFactory.Create();

            //Assert
            TestExpect.IsInstanceOfType<TestClass2>(objectUnderTest);
        }

        public class TestClass1
        {
            
        }

        public class TestClass2
        {
            public ITestDependency1 Parameter1 { get; }

            public TestClass2(ITestDependency1 parameter1)
            {
                Parameter1 = parameter1;
            }
                        
        }

        public interface ITestDependency1
        {

        }

    }


}