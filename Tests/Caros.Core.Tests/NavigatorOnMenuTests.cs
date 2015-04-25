using Caros.Core.Context;
using Caros.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caros.Core.Contracts;
using Moq;

namespace Caros.Core.Tests
{
    [TestFixture]
    class NavigatorOnMenuTests
    {
        IContext Context { get; set; }

        public void SetUp()
        {
            Context = new ApplicationContext();

            var navigatorMock = new Mock<Navigator>(Context) { CallBase = true };
            var navigator = navigatorMock.Object;
            Context.Navigator = navigator;
        }

        [TestCase]
        public void ShouldNavigateToMenu()
        {
            SetUp();

            Context.Navigator.MenuPage = new TypeOf<MockMenuViewModel>();
            Context.Navigator.Visit<MockPageViewModel1>();
            Context.Navigator.OpenMenu();

            var expected = typeof(MockMenuViewModel);
            var actual = Context.Navigator.CurrentPage.GetType();
            Assert.IsTrue(Context.Navigator.IsMenuOpen);
            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void ShouldCloseMenuIfMenuLaunchedAgain()
        {
            SetUp();

            Context.Navigator.MenuPage = new TypeOf<MockMenuViewModel>();
            Context.Navigator.Visit<MockPageViewModel1>();
            Context.Navigator.OpenMenu();
            Context.Navigator.OpenMenu();

            var expected = typeof(MockPageViewModel1);
            var actual = Context.Navigator.CurrentPage.GetType();
            Assert.IsFalse(Context.Navigator.IsMenuOpen);
            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void ShouldCloseMenuOnReturn()
        {
            SetUp();

            Context.Navigator.MenuPage = new TypeOf<MockMenuViewModel>();
            Context.Navigator.Visit<MockPageViewModel1>();
            Context.Navigator.OpenMenu();
            Context.Navigator.Return();

            var expected = typeof(MockPageViewModel1);
            var actual = Context.Navigator.CurrentPage.GetType();
            Assert.IsFalse(Context.Navigator.IsMenuOpen);
            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void ShouldCloseMenuOnVisitAnotherPage()
        {
            SetUp();

            Context.Navigator.MenuPage = new TypeOf<MockMenuViewModel>();
            Context.Navigator.Visit<MockPageViewModel1>();
            Context.Navigator.OpenMenu();
            Context.Navigator.Visit<MockPageViewModel2>();

            var expected = typeof(MockPageViewModel2);
            var actual = Context.Navigator.CurrentPage.GetType();
            Assert.IsFalse(Context.Navigator.IsMenuOpen);
            Assert.AreEqual(expected, actual);
        }

        class MockMenuViewModel : PageViewModel, IMenuDisplay
        {
            public MockMenuViewModel(IContext context)
                : base(context) { }

            public void LoadTasks(IEnumerable<NamedAction> tasks)
            {
            }
        }

        class MockPageViewModel1 : PageViewModel
        {
            public MockPageViewModel1(IContext context)
                : base(context) { }
        }

        class MockPageViewModel2 : PageViewModel
        {
            public MockPageViewModel2(IContext context)
                : base(context) { }
        }
    }
}
