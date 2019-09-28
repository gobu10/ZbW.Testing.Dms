using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ZbW.Testing.Dms.Client.Services;
using FakeItEasy;
using NUnit.Framework.Internal;

namespace ZbW.Testing.Dms.Client.UnitTests 
{
    [TestFixture]
    internal class UserServiceTests
    {

        private string USER_NAME = "TestUser";

        [Test]
        public void SaveUsername_SetUsername_SetUsernameCorrect() {
            //arrage
            var sutUserService = new UserService();

            var userTestableMock = A.Fake<UserTestable>(); // MOCK

            sutUserService.UserTestable = userTestableMock;

            //act
            sutUserService.SaveUsername(USER_NAME);

            //assert
            A.CallTo(() => userTestableMock.SetCurrentUsersernme(USER_NAME))
                .DoesNothing();
            A.CallTo(() => userTestableMock.SaveSettings())
                .DoesNothing();
        }

        [Test]
        public void SaveUsername_handleException_ThrowException() {
            //arrage
            var sutUserService = new UserService();
            var userTestableStub = A.Fake<UserTestable>();

            sutUserService.UserTestable = userTestableStub;

            A.CallTo(() => userTestableStub.SetCurrentUsersernme(A<string>.Ignored)).Throws<Exception>();


            //act
            void TestDelegate() => sutUserService.SaveUsername(null);


            //assert
            Assert.That(TestDelegate, Throws.Exception.TypeOf<Exception>());
        }
    }
}
