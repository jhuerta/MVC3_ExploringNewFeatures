using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LearnMVC3.Model;
using NUnit.Framework;

namespace LearnMVC3.Tests.Reports
{

    [TestFixture]
    class OrderReportingSpecs
    {
        private dynamic _productions;
        private dynamic _episodes;
        private dynamic _items;
        private dynamic _videoLog;

        [SetUp]
        public void Init()
        {
            _productions = new ProductionsV2();
            _episodes = new EpisodesV2();
            _items = new Items();
            _videoLog = new VideoLog();
        }

        [Test]
        public void episodes_should_have_duration_field()
        {
            var episode = _episodes.First();
            var dictionaryObject = (IDictionary<string, object>) episode;
            Assert.True(dictionaryObject.ContainsKey("Duration"));
        }


        [Test]
        public void items_should_not_have_product_id_nor_OrderId_()
        {
            var item  = _items.First();
            var dictionaryObject = (IDictionary<string, object>)item;
            Assert.False(dictionaryObject.ContainsKey("product_id"));
            Assert.False(dictionaryObject.ContainsKey("Orders_Id"));
            Assert.False(dictionaryObject.ContainsKey("OrderId"));
        }


        [Test]
        public void videoLog_should_contain_download_cost()
        {
            var videoLog = _videoLog.Prototype;
            var dictionaryObject = (IDictionary<string, object>)videoLog;
            Assert.True(dictionaryObject.ContainsKey("FileSize"));
            Assert.True(dictionaryObject.ContainsKey("Cost"));
            Assert.True(dictionaryObject.ContainsKey("Usage"));
        }
    }
}
