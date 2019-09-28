using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ZbW.Testing.Dms.Client.ViewModels;

namespace ZbW.Testing.Dms.Client.IntegtarionTests.ViewModels {
    [TestFixture]
    internal class LoginViewModelTests {

        private string TEST_USERNAME = "TestUser";

        [SetUp]
        public void ClearUsername() {
            Properties.Settings.Default.currentUser = "";
            Properties.Settings.Default.Save();
        }

        [Test]
        public void CanLoggin_HasUsername_IsEnabled() {
            // arrage
            var loginViewModel = new LoginViewModel(null);

            // act
            loginViewModel.Benutzername = TEST_USERNAME;
            var result = loginViewModel.CmdLogin.CanExecute();

            //assert
            Assert.That(result, Is.True);
        }


        [Test]
        public void CanLoggin_EmptyUsername_IsDisabled() {
            // arrage
            var loginViewModel = new LoginViewModel(null);

            // act
            loginViewModel.Benutzername = "";
            var result = loginViewModel.CmdLogin.CanExecute();

            //assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CanLoggin_NoUsername_IsDisabled() {
            // arrage
            var loginViewModel = new LoginViewModel(null);

            // act
            var result = loginViewModel.CmdLogin.CanExecute();

            //assert
            Assert.That(result, Is.False);
        }
    }
}
