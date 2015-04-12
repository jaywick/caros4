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
        public void ShouldSetDarkThemeIfNightTime(int inputHour, ThemeStyle expectedTheme)
        {
            var inputDate = new System.DateTime(2015, 1, 1, inputHour, 0, 0);
            var MockContext = new MockContext(inputDate);

            Assert.AreEqual(MockContext.Theme.Current, expectedTheme);
        }

        [TestCase(8, ThemeStyle.Light)]
        [TestCase(10, ThemeStyle.Light)]
        [TestCase(12, ThemeStyle.Light)]
        [TestCase(15, ThemeStyle.Light)]
        [TestCase(17, ThemeStyle.Light)]
        public void ShouldSetLightThemeIfDayTime(int inputHour, ThemeStyle expectedTheme)
        {
            var inputDate = new System.DateTime(2015, 1, 1, inputHour, 0, 0);
            var MockContext = new MockContext(inputDate);

            Assert.AreEqual(MockContext.Theme.Current, expectedTheme);
        }

        private class MockContext : IContext
        {
            public RootViewModel RootViewModel { get; set; }
            public ITheme Theme { get; set; }
            public INavigator Navigator { get; set; }
            public IStorage Storage { get; set; }
            public IDatabase Database { get; set; }
            public ServicesManager Services { get; set; }
            public IProfiles Profiles { get; set; }
            public IEnvironment Environment { get; set; }
            public IClock Clock { get; set; }

            public MockContext(System.DateTime inputDate)
            {
                this.Clock = new MockClock(this, inputDate);
                this.Environment = new Environment(this);
                this.Theme = new MockTheme(this);
            }

            public static IContext Create()
            {
                return null;
            }
        }

        private class MockClock : ContextComponent, IClock
        {
            private System.DateTime _inputDate;

            public MockClock(MockContext mockContext, System.DateTime inputDate)
                : base(mockContext)
            {
                _inputDate = inputDate;
            }

            public System.DateTime CurrentTime
            {
                get { return _inputDate; }
            }
        }

        private class MockTheme : Theme
        {
            public MockTheme(IContext context)
                : base(context)
            {
            }

            public override void Set(ThemeStyle style)
            {
                Current = style;
            }
        }
    }
}
