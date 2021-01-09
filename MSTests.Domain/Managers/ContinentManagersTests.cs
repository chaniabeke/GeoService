using FluentAssertions;
using GeoService.Domain.Managers;
using GeoService.Domain.Models;
using GeoService.EF.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSTests.Domain.Managers
{
    [TestClass]
    public class ContinentManagerTests
    { 
        private readonly ContinentManager continentManager;
        private readonly CountryManager countryManager;

        public ContinentManagerTests()
        {
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            continentManager = new ContinentManager(new UnitOfWork(ctx));
            countryManager = new CountryManager(new UnitOfWork(ctx));
        }

        [TestMethod]
        public void AddContinent_ShouldThrowException_IfContinentIsNull()
        {
            // Arrange 


            // Act 


            // Assert           
            Assert.Fail();
        }

        [TestMethod]
        public void AddContinent_ShouldThrowException_IfContinentNameExist()
        {
            // Arrange 


            // Act 


            // Assert           
            Assert.Fail();
        }

        [TestMethod]
        public void AddContinent_ShouldAddContinentAndUpdateCountries_IfContinentIsValid()
        {
            // Arrange 
            Continent continent1 = new Continent("Africa");
            Continent continent1InDb = continentManager.AddContinent(continent1);
            Country country1 = new Country("Afghanistan", 38928346, 652.860, continent1InDb);
            Country country2= new Country("Albania", 2877797, 27.400, continent1InDb);
            Country country1InDb = countryManager.AddCountry(country1);
            Country country2nDb = countryManager.AddCountry(country2);
            List<Country> countries = new List<Country>();
            countries.Add(country1InDb); countries.Add(country2nDb);
            Continent continent2 = new Continent("Asia", countries);
            // Act 
            continentManager.AddContinent(continent2);
            Continent continent2InDb = continentManager.Find("Asia");
            // Assert  
            continent2InDb.Should().BeEquivalentTo(continent2, options => options.Excluding(o => o.Id));
        }

        [TestMethod]
        public void FindContinentById_ShouldThrowException_IfIdIsNegativeOrEqualToZero()
        {
            // Arrange 


            // Act 


            // Assert           
            Assert.Fail();
        }

        [TestMethod]
        public void FindContinentById_ShouldFindContinentWithCountries_IfContinentExists()
        {
            // Arrange 
            Continent continent1 = new Continent("Europe");
            Continent continent1InDb = continentManager.AddContinent(continent1);
            Country country1 = new Country("Algeria", 38928346, 652.860, continent1InDb);
            Country country2 = new Country("Andorra", 2877797, 27.400, continent1InDb);
            Country country1InDb = countryManager.AddCountry(country1);
            Country country2nDb = countryManager.AddCountry(country2);

            // Act 
            Continent continent = continentManager.Find(continent1InDb.Id);

            // Assert      
            continent.Should().NotBeNull();
            continent.Name.Should().Be("Europe");
            continent.Countries.Count.Should().Be(2);
            continent.Countries.First().Should().NotBeNull();
            continent.Countries.First().Should().BeEquivalentTo(country1InDb);
        }

        [TestMethod]
        public void FindContinentByName_ShouldThrowException_IfNameIsNotValid()
        {
            // Arrange 


            // Act 


            // Assert           
            Assert.Fail();
        }

        [TestMethod]
        public void FindContinentByName_ShouldFindContinent_IfContinentExists()
        {
            // Arrange 
            Continent continent1 = new Continent("North-America");
            Continent continent1InDb = continentManager.AddContinent(continent1);

            // Act 
            Continent continent = continentManager.Find(continent1InDb.Name);

            // Assert      
            continent.Should().NotBeNull();
            continent.Name.Should().Be("North-America");
        }

        [TestMethod]
        public void RemoveContinent_ShouldThrowException_IfContinentDoesNotExist()
        {
            // Arrange 


            // Act 


            // Assert           
            Assert.Fail();
        }

        [TestMethod]
        public void RemoveContinent_ShouldThrowException_IfContinentHasCountries()
        {
            // Arrange 


            // Act 


            // Assert           
            Assert.Fail();
        }

        [TestMethod]
        public void RemoveContinent_ShouldRemoveContinent_IfContinentExists()
        {
            // Arrange 
            Continent continent1 = new Continent("South-America");
            Continent continent1InDb = continentManager.AddContinent(continent1);

            // Act 
            continentManager.RemoveContinent(continent1InDb.Id);

            // Assert      
            continentManager.Find(continent1InDb.Id).Should().BeNull();
        }

        [TestMethod]
        public void UpdateContinent_ShouldThrowException_IfContinentNameExists()
        {
            // Arrange 


            // Act 


            // Assert           
            Assert.Fail();
        }

        [TestMethod]
        public void UpdateContinent_ShouldThrowException_IfContinentDoesNotExist()
        {
            // Arrange 


            // Act 


            // Assert           
            Assert.Fail();
        }

        [TestMethod]
        public void UpdateContinent_ShouldUpdateContinent_IfContinentExists()
        {
            // Arrange 
            Continent continent1 = new Continent("Europe");
            Continent continent2 = new Continent("Europe 2.0");
            Continent continent1InDb = continentManager.AddContinent(continent1);
            Continent continent2InDb = continentManager.AddContinent(continent2);
            Country country1 = new Country("Algeria", 38928346, 652.860, continent1InDb);
            Country country2 = new Country("Andorra", 2877797, 27.400, continent2InDb);
            Country country1InDb = countryManager.AddCountry(country1);
            Country country2InDb = countryManager.AddCountry(country2);
            Continent continent1InDbWithCountry = continentManager.Find(continent1InDb.Id);

            Continent continentUpdated = continent1InDbWithCountry;
            continentUpdated.Name = "East-Europe";
            continentUpdated.AddCountry(country2InDb);

            // Act 
            continentManager.UpdateContinent(continent1InDb.Id, continentUpdated);
            Continent updatedContinentInDb = continentManager.Find(continentUpdated.Id);

            // Assert           
            updatedContinentInDb.Name.Should().Be("East-Europe");
            updatedContinentInDb.Countries.Count.Should().Be(2);
        }
    }
}
