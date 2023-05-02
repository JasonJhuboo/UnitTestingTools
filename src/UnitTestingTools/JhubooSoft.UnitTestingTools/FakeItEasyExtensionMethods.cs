using FakeItEasy;
using FakeItEasy.Configuration;

namespace JhubooSoft.UnitTestingTools
{
    public static class FakeItEasyExtensionMethods
    {
        public static TReturnType ReturnsFake<TReturnType>(this IReturnValueArgumentValidationConfiguration<TReturnType> callToFake)
            where TReturnType : class
        {
            TReturnType fakedObject = A.Fake<TReturnType>();
            callToFake.Returns(fakedObject);
            return fakedObject;
        }
    }
}