using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ZbW.Testing.Dms.Client.Services;
using ZbW.Testing.Dms.Client.ViewModels;

namespace ZbW.Testing.Dms.Client.IntegtarionTests.Service {
    [TestFixture]
    internal class UserServiceTests {
        [SetUp]
        public void ClearUsername() {
            Properties.Settings.Default.currentUser = "";
            Properties.Settings.Default.Save();
        }

        [Test]
        public void SaveUsername_HasUsername_IsSaved() {
            // arrage
            var userService = new UserService();
            var username = "TestUser";

            // act
            userService.SaveUsername(username);
            var result = userService.GetUsername();

            //assert
            Assert.That(result.Equals(username), Is.True);
        }

        [Test]
        public void SaveUsername_EmptyUsername_IsNotSaved() {
            // arrage
            var userService = new UserService();
            var username = "";

            // act
            userService.SaveUsername(username);
            var result = userService.GetUsername();

            //assert
            Assert.That(result.Equals(username), Is.True);
        }

        [Test]
        public void SaveUsername_NoUsername_IsNotSaved() {
            // arrage
            var userService = new UserService();
            string username = null;

            // act
            userService.SaveUsername(username);
            var result = userService.GetUsername();

            //assert
            Assert.That(result.Equals(username), Is.False);
        }
    }
}
