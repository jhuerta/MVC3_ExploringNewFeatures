using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using LearnMVC3.Model;
using NUnit.Framework;

namespace LearnMVC3.Tests.Functionals
{
    [TestFixture]
    class ShopifyTests
    {
        private Orders _orders;
        private Items _items;

        [SetUp]
        public void Init()
        {
            _orders = new Orders();
            _items = new Items();

            _items.Delete();
            _orders.Delete();
        }

        [Test]
        [Ignore("This test is working on the same database as live. The test is the client, consuming the real services.There, databases should be shared. Data will be deleted.")]
        public void recevier_should_saver_order()
        {

            var url = "http://localhost/LearnMVC3/Api/Shopify/Receiver";
            //AlternativeWayToPing_AlsoWorking(GetJson(), url);
            Ping(url, GetJson());

           Assert.AreEqual(1, _orders.All().Count());
            Assert.AreEqual(2, _items.All().Count());
        }

        public static bool AlternativeWayToPing_AlsoWorking(string data, string url)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "text/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    //Now you have your response.
                    //or false depending on information in the response
                    return true;
                }

            }
            catch (Exception ex)
            {
                var message = ex.Message;
                throw;
            }
        }
        private string GetJson()
        {
            var jsonFile = @"Functionals/ShopifyPing.js";
            var result = "";
            using (var stream = new StreamReader(jsonFile))
            {
                result = stream.ReadToEnd();
            }

            return result;
        }
        void Ping(string url, string data)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "text/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                    streamWriter.Flush();
                    streamWriter.Close();

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}
