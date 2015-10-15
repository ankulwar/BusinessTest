using System;
using Ecs.Eclipse.Model.Entities;
using NUnit.Framework;

namespace Ecs.Eclipse.Data.Tests
{
    [TestFixture]
    public class CountryDaoTest : NHibernateDatabaseTest
    {
        //TODO : Need to fix this in staging environment
        [Ignore]
        public void CanSaveAndLoadCountry()
        {

            var id = session.Save(new Country
             {
                 Name = "testcountry",
                 Created = new ActivityStamp() { By = "1", On = DateTime.Now },
                 
             });


            var country = session.Get<Country>(id);

            Assert.NotNull(country);
            Assert.AreEqual("testcountry", country.Name);

        }
    }
}
