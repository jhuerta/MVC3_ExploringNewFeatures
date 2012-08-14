using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace LearnMVC3.Tests.Functionals
{
    [TestFixture]
    class CustomerSpecs : TestBase
    {

        [Test]
        public void a_user_with_monthly_subscription_should_only_be_able_to_stream()
        {
            this.IsPending();
            
        }

        [Test]
        public void a_user_with_yearly_subscription_should_be_able_to_stream_and_download()
        {
            this.IsPending();

        }

        [Test]
        public void a_user_with_cancelled_subscription_should_not_be_able_download_or_stream()
        {
            this.IsPending();

        }

        [Test]
        public void a_user_with_suspended_subscription_should_not_be_able_to_stream_or_download()
        {
            this.IsPending();

        }

        [Test]
        public void a_user_with_overdue_subscription_should_be_able_to_stream_or_download()
        {
            this.IsPending();
        }


        [Test]
        public void a_user_should_be_able_to_add_production_to_cart()
        {
            this.IsPending();
        }

        [Test]
        public void a_user_that_owns_a_production_should_be_able_to_stream()
        {
            this.IsPending();
        }

        [Test]
        public void a_user_that_owns_a_production_should_be_able_to_download()
        {
            this.IsPending();
        }

        [Test]
        public void a_user_should_be_able_to_purchase_subscription()
        {
            this.IsPending();
        }

        [Test]
        public void a_user_should_be_able_to_cancel_subscription()
        {
            this.IsPending();
        }

        
    }
}
