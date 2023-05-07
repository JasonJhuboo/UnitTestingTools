using System;
using System.Linq.Expressions;
using Xunit;

namespace JhubooSoft.UnitTestingTools
{
    //Todo: Test TestExpect
    public static class TestExpect
    {
        /// <summary>
        /// Ensures that the object is the correct type, (not a derived type) and is not null.
        /// </summary>
        /// <typeparam name="TExpectedType"></typeparam>
        /// <param name="typeToCheck"></param>
        public static void IsInstanceOfType<TExpectedType>(TExpectedType typeToCheck)
        {
            Assert.IsType<TExpectedType>(typeToCheck);
            Assert.NotNull(typeToCheck);
        }

        /// <summary>
        /// Ensure that an exception occurs.
        /// </summary>
        /// <remarks>
        /// Usage - pass in action that should be tested - for example TestExpect.ExceptionOccurred(()=> myObject.DoSomething());
        /// </remarks>
        /// <typeparam name="TExceptionType">The expection that is expected to occur</typeparam>
        /// <param name="action">The call that is being tested</param>
        public static void ExceptionOccurred<TExceptionType>(Action action)
            where TExceptionType : Exception
        {
            try
            {
                action.Invoke();
            }
            catch(TExceptionType)
            {
                return;
            }
            catch (Exception e)
            {
                Assert.Fail($"The expected exception of type {typeof(TExceptionType).Name} did not occur, but an exception of type {e.GetType().Name} was thrown unexpectedly.");
            }

            Assert.Fail($"The expected exception of type {typeof(TExceptionType).Name} did not occur.");
        }
            
            
    }
}
