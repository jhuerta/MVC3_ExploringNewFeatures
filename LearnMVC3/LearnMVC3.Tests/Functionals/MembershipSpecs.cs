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
        string anyValidEmail = "name.lastname@domain.com";
        static string validPassword = "validPassword";
        string confirmationPassword = validPassword;
        private Users _users;
        private DynamicModel _db;

        public MembershipSpecs()
        {
            this.Describes("User Describes");
            _users = new Users();
            _db = new DynamicModel("Users","Users","ID");
        }

        [SetUp]
        public void Init()
        {
            _db.Delete();
            
            Assert.AreEqual(0, _db.All().Count());
        }

        [Test]
        public void user_should_be_saved_on_register()
        {
            var result = _users.Register("test@test.com", "password", "password");

            Assert.AreEqual(1, _db.All().Count());
        }


        [Test]
        public void duplicate_email_should_return_message()
        {
            _users.Register(anyValidEmail, validPassword, validPassword);
            var result = _users.Register(anyValidEmail, validPassword, validPassword);
            var duplicatedEmail = "This email already exist in our sytem";
            Assert.AreEqual(result.Message,duplicatedEmail );

        }

        [Test]
        public void valid_email_and_passwords_should_register_user()
        {
            this.IsPending();
            //var result = _users.Register(anyValidEmail, validPassword, validPassword);
            //var user = _users.GetByUserNameAndPassword(anyValidEmail,validPassword);
            //Assert.AreEqual(user.Username, anyValidEmail);
        }


        [Test]
        public void registration_should_not_accept_email_with_less_than_6_chars()
        {
            var emailLessThan6chars = "a@b.c";

            var result = _users.Register(emailLessThan6chars, validPassword, confirmationPassword);

            Assert.That(result.Success,Is.False);
        }


        [Test]
        public void resitration_should_not_accept_password_with_less_than_6_chars()
        {
            var passwordLessThan6Chars = "one";

            var result = _users.Register(anyValidEmail, passwordLessThan6Chars, passwordLessThan6Chars);

            Assert.That(result.Success, Is.False);
        }


        [Test]
        public void registration_should_not_accept_mismatched_password()
        {
            var differentPassword = validPassword + "_this_is_Different";
            
            var result = _users.Register(anyValidEmail, validPassword, differentPassword);

            Assert.That(result.Success, Is.False);
        }


        [Test]
        public void email_mus_be_unique()
        {
            this.IsPending();
        }

    }
}
