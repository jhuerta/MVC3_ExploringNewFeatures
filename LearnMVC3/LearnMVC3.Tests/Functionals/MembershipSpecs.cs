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

        private readonly Users _users;

        public MembershipSpecs()
        {
            this.Describes("User Describes");
            _users = new Users();
        }

        [SetUp]
        public void Init()
        {
            _users.Delete();

            Assert.AreEqual(0, _users.All().Count());
        }

        [Test]
        public void DeleteAll()
        {
            _users.Register(validEmail, validPassword, validPassword);

            Assert.AreEqual(1, _users.All().Count());

            _users.Delete();

            Assert.AreEqual(0, _users.All().Count());
        } 

        [Test]
        public void user_should_be_saved_on_register()
        {
            _users.Register(validEmail, validPassword, validPassword);

            Assert.AreEqual(1, _users.All().Count());
        }


        [Test]
        public void duplicate_email_should_return_message()
        {
            _users.Register(validEmail, validPassword, validPassword);
            var result = _users.Register(validEmail, validPassword, validPassword);
            var duplicatedEmail = "This email already exist in our sytem";
            Assert.AreEqual(result.Message,duplicatedEmail );
        }

        [Test]
        public void duplicate_email_is_not_allowed()
        {
            _users.Register(validEmail, validPassword, validPassword);

            var result = _users.Register(validEmail, validPassword, validPassword);

            Assert.IsFalse(result.Success);
        }

        [Test]
        public void valid_email_and_passwords_should_register_user()
        {
            _users.Register(validEmail, validPassword, validPassword);
            var user = Users.FindByEmail(validEmail);
            Assert.AreEqual(user.Email, validEmail);
        }


        [Test]
        public void registration_should_not_accept_email_with_less_than_6_chars()
        {
            var emailLessThan6chars = "a@b.c";

            var result = _users.Register(emailLessThan6chars, validPassword, validPassword);

            Assert.That(result.Success,Is.False);
        }


        [Test]
        public void resitration_should_not_accept_password_with_less_than_6_chars()
        {
            var passwordLessThan6Chars = "one";

            var result = _users.Register(validEmail, passwordLessThan6Chars, passwordLessThan6Chars);

            Assert.That(result.Success, Is.False);
        }


        [Test]
        public void registration_should_not_accept_mismatched_password()
        {
            var differentPassword = validPassword + "_this_is_Different";
            
            var result = _users.Register(validEmail, validPassword, differentPassword);

            Assert.That(result.Success, Is.False);
        }




    }
}
