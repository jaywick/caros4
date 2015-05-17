using Caros.Core.Context;
using Caros.Core.UI;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Tests
{
    [TestFixture]
    public class NavigatorPromptTests
    {
        IContext Context { get; set; }

        public void SetUp(Action waitForUserResponseAction = null)
        {
            Context = new ApplicationContext();

            if (waitForUserResponseAction == null)
                waitForUserResponseAction = new Action(() => { });

            var navigatorMock = new Mock<Navigator>(Context) { CallBase = true };
            navigatorMock
                .Setup(x => x.WaitForUserResponse())
                .Callback(waitForUserResponseAction)
                .CallBase();

            Context.Navigator = navigatorMock.Object;
            Context.Navigator.MenuPage = new Mock<Reference<PageViewModel>>().Object;
            Context.Navigator.PromptPage = new TypeOf<MockPromptViewModel>();
        }

        [TestCase]
        public void ShouldDisplayPrompt()
        {
            SetUp();

            Context.Navigator.Visit<MockPageViewModel_1>();
            Context.Navigator.Prompt("Hey there!");

            var expected = typeof(MockPromptViewModel);
            var actual = Context.Navigator.CurrentPage.GetType();
            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public async void ShouldReturnValueFromPrompt()
        {
            var inputValue = "123456";

            SetUp(() => Context.Navigator.UserRequestsPromptAccept(inputValue));
            Context.Navigator.Visit<MockPageViewModel_1>();
            var result = await Context.Navigator.Prompt("Hey there!");

            var expected = inputValue;
            var actual = result;
            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public async void ShouldReturnNullFromPromptIfCancelled()
        {
            SetUp(() => Context.Navigator.UserRequestsPromptCancel());
            Context.Navigator.Visit<MockPageViewModel_1>();
            var result = await Context.Navigator.Prompt("Hey there!");

            Assert.IsNull(result);
        }
        
        [TestCase]
        public async void ShouldReturnToPromptAfterAlert()
        {
            SetUp(() => Context.Navigator.UserRequestsPromptCancel());

            Context.Navigator.Visit<MockPageViewModel_1>();
            var result = await Context.Navigator.Prompt("Hey there!");
            await Context.Navigator.Alert("alert!");

            var expected = Context.Navigator.PromptPage.Type;
            var actual = Context.Navigator.CurrentPage.GetType();
            Assert.AreEqual(expected, actual);
        }

        #region Mocked Classes

        class MockPromptViewModel : PageViewModel, IAlertDisplayer
        {
            public MockPromptViewModel(IContext context)
                : base(context) { }

            public event Action<string> RequestAccept;

            public event Action RequestCancel;

            public void ShowPrompt(string message, string defaultValue)
            {
            }

            public void ShowAlert(string message)
            {
            }
        }

        class MockPageViewModel_1 : PageViewModel
        {
            public MockPageViewModel_1(IContext context)
                : base(context) { }
        }

        class MockPageViewModel_2 : PageViewModel
        {
            public MockPageViewModel_2(IContext context)
                : base(context) { }
        }

        #endregion

    }
}
