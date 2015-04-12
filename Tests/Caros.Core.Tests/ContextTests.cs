using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Caros.Core.Context;
using Caros.Core.Contracts;
using Moq;

namespace Caros.Core.Tests
{
    [TestFixture]
    public class ContextTests
    {
        [TestFixtureSetUp]
        public void Start()
        {
        }

        [TestCase(20, ThemeStyle.Dark)]
        [TestCase(23, ThemeStyle.Dark)]
        [TestCase(0, ThemeStyle.Dark)]
        [TestCase(3, ThemeStyle.Dark)]
        [TestCase(5, ThemeStyle.Dark)]
        [TestCase(8, ThemeStyle.Light)]
        [TestCase(10, ThemeStyle.Light)]
        [TestCase(12, ThemeStyle.Light)]
        [TestCase(15, ThemeStyle.Light)]
        [TestCase(17, ThemeStyle.Light)]
        public void ShouldSetThemeBasedOnTime(int inputHour, ThemeStyle expectedTheme)
        {
            // input
            var inputDate = new System.DateTime(2015, 1, 1, inputHour, 0, 0);

            // mock
            var context = new ApplicationContext();

            var mockClock = new Mock<Clock>(context) { CallBase = true };
            mockClock.Setup(x => x.CurrentTime).Returns(inputDate);

            var mockTheme = new Mock<Theme>(context) { CallBase = true };
            mockTheme.Setup(x => x.UpdateResources(It.IsAny<ThemeStyle>())); // setup with no implementation = dont run anything

            // work
            context.Clock = mockClock.Object;
            context.Environment = new Environment(context);
            context.Theme = mockTheme.Object;
            context.Theme.Initialise();

            // compare
            var actualTheme = context.Theme.Current;
            Assert.AreEqual(expectedTheme, actualTheme);
        }
    }
}
