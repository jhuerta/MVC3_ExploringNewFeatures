using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LearnMVC3.Infrastructure;

namespace LearnMVC3.Tests.Functionals
{
    class FakeTokenStore : ITokenHandler
    {
        private string _token;

        public void SetClientAccess(string token)
        {
            _token = token;
        }

        public void RemoveClientAccess()
        {
            _token = "";
        }

        public string GetToken()
        {
            return _token;
        }
    }
}
