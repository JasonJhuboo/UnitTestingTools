using FakeItEasy;
using Xunit;
using Xunit.Abstractions;

namespace JhubooSoft.UnitTestingTools.Tests
{
    public class ReturnsFakeTests
    {
        [Fact]
        public void ReturnsFakeTest()
        {
            //Arrange
            ITestClass1 objectUnderTest = A.Fake<ITestClass1>();

            ITestClass2 expected = A.CallTo(() => objectUnderTest.Object1).ReturnsFake();

            //Act
            ITestClass2 actual = objectUnderTest.Object1;

            //Assert
            Assert.Equal(expected, actual);
        }

        public interface ITestClass1
        {
            ITestClass2 Object1 { get; set; }
        }

        public interface ITestClass2
        {
            string String1 { get; set; }
        }
    }


}