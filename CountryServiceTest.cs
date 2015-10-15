using System;
using System.Collections.Generic;
using Ecs.Eclipse.Business.Contracts;
using Ecs.Eclipse.Business.Services;
using Ecs.Eclipse.Data;
using Ecs.Eclipse.Data.Contracts;
using Ecs.Eclipse.Model;
using Ecs.Eclipse.Model.Entities;
using Moq;

using NUnit.Framework;

namespace Ecs.Eclipse.Business.Tests
{
  
    [TestFixture]
    public class CountryServiceTest
    {
        private Mock<ICountryDao> _countryDaoMock;
        private ICountryService _serviceInTest;

        [SetUp]
        public void Setup()
        {
            // initial arrange
            _countryDaoMock = new Mock<ICountryDao>();
            _serviceInTest = new CountryService() { CountryDao = _countryDaoMock.Object };
        }

        [Test]
        public void SaveTest()
        {
            var country = CreateCountry();
            _countryDaoMock.Setup(x => x.SaveOrUpdate(country)).Returns(country);

             _serviceInTest.Save(country);
        }
        
        [Test]
        public void GetById()
        {
            var countrys = new List<Country>();
            var country = CreateCountry();
            country.Id = 1;
            countrys.Add(country);

            _countryDaoMock.Setup(x => x.FindById(country.Id)).Returns(country);
            var result = _serviceInTest.GetById(country.Id);

            Assert.NotNull(result);
            Assert.AreEqual(result.Id, country.Id);
        }

        [Test]
        public void Search()
        {
            var citys = new List<Country>();
            var city = CreateCountry();
            citys.Add(city);
            int totalPage;
            int recordCount;
            _countryDaoMock.Setup(x => x.Search(1, 20, "ID", "desc", "", "", "", out totalPage, out recordCount)).Returns(citys);
            var result = _serviceInTest.Search(1, 20, "ID", "desc", "", "", "", out totalPage, out recordCount);
            Assert.AreEqual(citys.Count, result.Count);
            Assert.AreEqual(result[0].Name, citys[0].Name);
        }

        [Test]
        public void Delete()
        {
            var country = CreateCountry();
            _countryDaoMock.Setup(x => x.Delete(country));

            _serviceInTest.Delete(country);
        }

        [Test]
        public void GetAll()
        {
            var countrys = new List<Country>();
            var country = CreateCountry();
            countrys.Add(country);
            _countryDaoMock.Setup(x => x.FindAll()).Returns(countrys);
            var result = _serviceInTest.GetAll();
            Assert.AreEqual(countrys.Count, result.Count);
            Assert.AreEqual(result[0].Name, countrys[0].Name);
        }
        

        private Country CreateCountry()
        {
            var country = new Country()
            {
                Name = "testcountry",
                Created =
                    new ActivityStamp { By = "1", On = DateTime.Now }
            };

            return country;
        }

    }
}
