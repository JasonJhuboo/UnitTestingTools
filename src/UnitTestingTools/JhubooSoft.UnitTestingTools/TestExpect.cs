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
    }
}
