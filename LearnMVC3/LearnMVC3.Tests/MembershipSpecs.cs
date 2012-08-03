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

        [Test]
        public void valid_email_and_passwords_should_register_user()
        {
            this.IsPending();
        }

    }

}
