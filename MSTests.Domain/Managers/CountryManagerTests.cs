using GeoService.Domain.Managers;
using GeoService.EF.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTests.Domain.Managers
{
    [TestClass]
    public class CountryManagerTests
    {
        private readonly CountryManager countryManager;

        public CountryManagerTests()
        {
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            countryManager = new CountryManager(new UnitOfWork(ctx));
        }

        //[TestMethod]
        //public void AddCountry_ShouldThrowException_IfCountryIsNull()
        //{
        //    // Arrange

        //    // Act

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void AddCountry_ShouldThrowException_IfCountryNameExist()
        //{
        //    // Arrange

        //    // Act

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void AddCountry_ShouldAddCountry_IfCountryIsValid()
        //{
        //    // Arrange

        //    // Act

        //    // Assert
        //    Assert.Fail();
        //}
        ////TODO country continent add not null
        //[TestMethod]
        //public void FindCountryById_ShouldThrowException_IfIdIsNegativeOrEqualToZero()
        //{
        //    // Arrange

        //    // Act

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void FindCountryById_ShouldFindCountry_IfCountryExists()
        //{
        //    // Arrange

        //    // Act

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void FindCountryByName_ShouldThrowException_IfNameIsNotValid()
        //{
        //    // Arrange

        //    // Act

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void FindCountryByName_ShouldFindCountry_IfCountryExists()
        //{
        //    // Arrange

        //    // Act

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void RemoveCountry_ShouldThrowException_IfCountryDoesNotExist()
        //{
        //    // Arrange

        //    // Act

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void RemoveCountry_ShouldThrowException_IfCountryExists()
        //{
        //    // Arrange

        //    // Act

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void UpdateCountry_ShouldThrowException_IfCountryNameExists()
        //{
        //    // Arrange

        //    // Act

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void UpdateCountry_ShouldThrowException_IfCountryDoesNotExist()
        //{
        //    // Arrange

        //    // Act

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void UpdateCountry_ShouldUpdateCountry_IfCountryExists()
        //{
        //    // Arrange

        //    // Act

        //    // Assert
        //    Assert.Fail();
        //}
    }
}