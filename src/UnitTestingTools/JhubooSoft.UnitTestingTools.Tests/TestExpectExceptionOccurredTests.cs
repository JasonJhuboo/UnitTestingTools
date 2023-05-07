using System;
using Xunit;
using Xunit.Sdk;

namespace JhubooSoft.UnitTestingTools.Tests
{
    public class TestExpectExceptionOccurredTests
    {
        [Fact]
        public void TestExpectShouldNotInvalidateTestIfExpectedExceptionOccursForMethodWhichReturnsValue()
        {
            //Arrange
            ExceptionGenerator exceptionGenerator = new ExceptionGenerator();

            //Act / Assert
            TestExpect.ExceptionOccurred<InvalidOperationException>(() => exceptionGenerator.ThrowInvalidOperationException2());
        }

        [Fact]
        public void TestExpectShouldNotInvalidateTestIfExpectedExceptionOccursForMethodWhichDoesNotReturnsValue()
        {
            //Arrange
            ExceptionGenerator exceptionGenerator = new ExceptionGenerator();

            //Act / Assert
            TestExpect.ExceptionOccurred<InvalidOperationException>(() => exceptionGenerator.ThrowInvalidOperationException1());
        }

        [Fact]
        public void TestExpectShouldNotInvalidateTestIfExpectedExceptionOccursForMethodWhichHasGenericParameter()
        {
            //Arrange
            ExceptionGenerator exceptionGenerator = new ExceptionGenerator();

            //Act / Assert
            TestExpect.ExceptionOccurred<InvalidOperationException>(() => exceptionGenerator.ThrowInvalidOperationException3<string>());
        }

        [Fact]
        public void TestExpectShouldInvalidateTestIfExpectedExceptionDoesNotGetThrownForMethodWhichReturnsValue()
        {
            //Arrange
            ExceptionGenerator exceptionGenerator = new ExceptionGenerator();

            //Act / Assert
            try
            {
                TestExpect.ExceptionOccurred<InvalidOperationException>(() => exceptionGenerator.DoNotThrowInvalidOperationException1());
                Assert.Fail("No exception occurred.");
            }
            catch (FailException)
            {
                //This is the expected exception                
            }
            catch(Exception)
            {
                Assert.Fail("An unexpected exception occurred.");
            }           
        }

        [Fact]
        public void TestExpectShouldInvalidateTestIfUnexpectedExceptionGetsThrownForMethodWhichReturnsValue()
        {
            //Arrange
            ExceptionGenerator exceptionGenerator = new ExceptionGenerator();

            //Act / Assert
            try
            {
                TestExpect.ExceptionOccurred<NotImplementedException>(() => exceptionGenerator.ThrowInvalidOperationException2());
                Assert.Fail("No exception occurred.");
            }
            catch (FailException)
            {
                //This is the expected exception                
            }
            catch (Exception)
            {
                Assert.Fail("An unexpected exception occurred.");
            }
        }

        private class ExceptionGenerator
        {
            public void ThrowInvalidOperationException1()
            {
                throw new InvalidOperationException("Oh no!");
            }

            public object ThrowInvalidOperationException2()
            {
                throw new InvalidOperationException("Oh no!");
            }

            public TParameter ThrowInvalidOperationException3<TParameter>()
            {
                throw new InvalidOperationException("Oh no!");
            }

            public void DoNotThrowInvalidOperationException1()
            {
                return;
            }
        }
    }
}
