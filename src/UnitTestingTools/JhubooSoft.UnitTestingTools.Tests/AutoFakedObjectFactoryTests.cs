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

        [Fact]
        public void AutoFakedObjectFactoryShouldAllowCustomisedConstructorParameterToBeProvidedBeforeConstruction()
        {
            //Arrange
            AutoFakedObjectFactory<TestClass4> objectFactory = new AutoFakedObjectFactory<TestClass4>();

            ITestDependency2 fakeConstructorParameter = A.Fake<ITestDependency2>();
            A.CallTo(() => fakeConstructorParameter.Property1).Returns(999);

            objectFactory.UseWhenConstructing<ITestDependency2>(fakeConstructorParameter);

            //Act
            TestClass4 objectUnderTest = objectFactory.Create();

            //Assert
            Assert.Equal(999, objectUnderTest.Parameter2.Property1);
        }

        [Fact]
        public void AutoFakedObjectFactoryShouldAllowRetrievalOfConstructorParameterByTypeAfterTypeCreated()
        {
            //Arrange
            AutoFakedObjectFactory<TestClass4> objectFactory = new AutoFakedObjectFactory<TestClass4>();
            TestClass4 objectUnderTest = objectFactory.Create();

            //Act
            ITestDependency2 fakeConstructorParameter = objectFactory.GetConstructorParameter<ITestDependency2>();

            //Assert
            Assert.NotNull(fakeConstructorParameter);
        }


        [Fact]
        public void AutoFakedObjectFactoryShouldThrowExceptionIfTypeRetrievalOfConstructorParameterRequestedForTypeNotIncludedInConstructorParameter()
        {
            //Arrange
            AutoFakedObjectFactory<TestClass4> objectFactory = new AutoFakedObjectFactory<TestClass4>();
            TestClass4 objectUnderTest = objectFactory.Create();

            //Act / Assert
            TestExpect.ExceptionOccurred<ArgumentException>(() => objectFactory.GetConstructorParameter<ITestDependency3>());
        }

        [Fact]
        public void AutoFakedObjectFactoryShouldThrowExceptionWhenTestDependencyOverriddenMoreThanOnce()
        {
            //Arrange
            AutoFakedObjectFactory<TestClass4> objectFactory = new AutoFakedObjectFactory<TestClass4>();

            ITestDependency2 fakeConstructorParameter = A.Fake<ITestDependency2>();
            A.CallTo(() => fakeConstructorParameter.Property1).Returns(999);

            objectFactory.UseWhenConstructing<ITestDependency2>(fakeConstructorParameter);

            //Act / Assert
            TestExpect.ExceptionOccurred<InvalidOperationException>(() => objectFactory.UseWhenConstructing<ITestDependency2>(fakeConstructorParameter));       //Second time should cause an exception
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
            public ITestDependency2 Parameter2 { get; }

            public TestClass3(ITestDependency1 parameter1)
            {

            }

            public TestClass3(ITestDependency1 parameter1, ITestDependency2 parameter2)
            {
                Parameter1 = parameter1;
                Parameter2 = parameter2;
            }

        }

        public class TestClass4
        {
            public ITestDependency1 Parameter1 { get; }
            public ITestDependency2 Parameter2 { get; }


            public TestClass4(ITestDependency1 parameter1, ITestDependency2 parameter2)
            {
                Parameter1 = parameter1;
                Parameter2 = parameter2;
            }

        }

        public interface ITestDependency1
        {

        }


        public interface ITestDependency2
        {
            int Property1 { get; set; }
        }

        public interface ITestDependency3
        {

        }

    }


}