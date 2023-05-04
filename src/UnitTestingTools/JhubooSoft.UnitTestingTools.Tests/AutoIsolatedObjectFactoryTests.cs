using FakeItEasy;
using System;
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

        [Fact]
        public void AutoFakedObjectFactoryShouldThrowExceptionIfTypeHasMultipleConstructors()
        {
            //Arrange
            AutoFakedObjectFactory<TestClass3> objectFactory = new AutoFakedObjectFactory<TestClass3>();

            //Act / Assert
            TestExpect.ExceptionOccurred<InvalidOperationException>(() => objectFactory.Create());
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

        public class TestClass3
        {
            public ITestDependency1 Parameter1 { get; }

            public TestClass3(ITestDependency1 parameter1)
            {
                Parameter1 = parameter1;
            }

            public TestClass3(ITestDependency1 parameter1, ITestDependency1 parameter2)
            {
                Parameter1 = parameter1;
            }

        }

        public interface ITestDependency1
        {

        }

    }


}