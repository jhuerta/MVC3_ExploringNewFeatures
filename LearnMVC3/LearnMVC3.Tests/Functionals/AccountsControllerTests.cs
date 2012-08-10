using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using LearnMVC3.Controllers;
using LearnMVC3.Infrastructure;
using LearnMVC3.Models;
using NUnit.Framework;

namespace LearnMVC3.Tests.Functionals
{
    

    [TestFixture]
    class AccountsControllerTests : TestBase
    {
        private UserModel _userModel;
        private string validEmail = "test@test.com";
        private string validPassword = "secretPassword";
        private AccountController _controller;

        public AccountsControllerTests()
        {
            this.Describes("User Authentication");

            _userModel = new UserModel();
        }

        [SetUp]
        public void Init()
        {
            _controller = new AccountController(new FakeTokenStore());

            _userModel.Delete();

            Assert.AreEqual(0, _userModel.All().Count());
        }       
        
        [Test]
        public void register_action_persists_user_in_database()
        {
            Assert.AreEqual(0, _userModel.All().Count());
            var result = _userModel.Register(validEmail, validPassword, validPassword);
            Assert.AreEqual(1, _userModel.All().Count());
        }

        [Test]
        public void register_action_should_redirect_with_valid_email_password()
        {
            var result = _controller.Register(validEmail, validPassword, validPassword);

            Assert.IsInstanceOf<RedirectToRouteResult>(result);
        }

        [Test]
        public void changes_token_with_dual_logon()
        {
            _controller.Register(validEmail, validPassword, validPassword);

            _controller.LogOn(validEmail,validPassword);

            var token1 = _userModel.Single(where: "email =@0", args: validEmail).Token;

            _controller.LogOn(validEmail, validPassword);

            var token2 = _userModel.Single(where: "email =@0", args: validEmail).Token;

            Assert.AreNotEqual(token1,token2);
        }


        [Test]
        public void isLoggedIn_returns_flase_for_first_user_on_dual_login()
        {
            _controller.Register(validEmail, validPassword, validPassword);

            _controller.LogOn(validEmail, validPassword);

            Assert.True(_controller.IsLoggedIn);

            var secondUser = new AccountController(new FakeTokenStore());

            secondUser.LogOn(validEmail, validPassword);

            Assert.False(_controller.IsLoggedIn);
        }
    }
} 
