using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace LearnMVC3.Tests
{
    [TestFixture]
    public class MembershipSpecs : TestBase
    {
        string validEmail = "name.lastname@domain.com";
        static string validPassword = "validPassword";

        private readonly UserModel _userModel;

        public MembershipSpecs()
        {
            this.Describes("User Describes");
            _userModel = new UserModel();
        }

        [SetUp]
        public void Init()
        {
            _userModel.Delete();

            Assert.AreEqual(0, _userModel.All().Count());
        }

        [Test]
        public void DeleteAll()
        {
            _userModel.Register(validEmail, validPassword, validPassword);

            Assert.AreEqual(1, _userModel.All().Count());

            _userModel.Delete();

            Assert.AreEqual(0, _userModel.All().Count());
        } 

        [Test]
        public void user_should_be_saved_on_register()
        {
            _userModel.Register(validEmail, validPassword, validPassword);

            Assert.AreEqual(1, _userModel.All().Count());
        }


        [Test]
        public void duplicate_email_should_return_message()
        {
            _userModel.Register(validEmail, validPassword, validPassword);
            var result = _userModel.Register(validEmail, validPassword, validPassword);
            var duplicatedEmail = "This email already exist in our sytem";
            Assert.AreEqual(result.Message,duplicatedEmail );
        }

        [Test]
        public void duplicate_email_is_not_allowed()
        {
            _userModel.Register(validEmail, validPassword, validPassword);

            var result = _userModel.Register(validEmail, validPassword, validPassword);

            Assert.IsFalse(result.Success);
        }

        [Test]
        public void valid_email_and_passwords_should_register_user()
        {
            _userModel.Register(validEmail, validPassword, validPassword);
            var user = UserModel.FindByEmail(validEmail);
            Assert.AreEqual(user.Email, validEmail);
        }


        [Test]
        public void registration_should_not_accept_email_with_less_than_6_chars()
        {
            var emailLessThan6chars = "a@b.c";

            var result = _userModel.Register(emailLessThan6chars, validPassword, validPassword);

            Assert.That(result.Success,Is.False);
        }


        [Test]
        public void resitration_should_not_accept_password_with_less_than_6_chars()
        {
            var passwordLessThan6Chars = "one";

            var result = _userModel.Register(validEmail, passwordLessThan6Chars, passwordLessThan6Chars);

            Assert.That(result.Success, Is.False);
        }


        [Test]
        public void registration_should_not_accept_mismatched_password()
        {
            var differentPassword = validPassword + "_this_is_Different";
            
            var result = _userModel.Register(validEmail, validPassword, differentPassword);

            Assert.That(result.Success, Is.False);
        }




    }
}
