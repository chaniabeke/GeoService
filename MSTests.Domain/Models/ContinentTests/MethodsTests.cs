using FluentAssertions;
using GeoService.Domain.Exceptions;
using GeoService.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace MSTests.Domain.Models.ContinentTests
{
    [TestClass()]
    public class MethodsTests
    {
        #region AddCountry

        [TestMethod]
        public void AddCountry_ShouldThrowException_IfCountryAlreadyExist()
        {
            Continent continent = new Continent(5, "Asia");
            Country country = new Country(8, "India", 88, 510000);
            continent.AddCountry(country);

            Action act = () =>
            {
                continent.AddCountry(country);
            };

            act.Should().Throw<ContinentException>().WithMessage("Continent - AddCountry : country already exists");
        }

        [TestMethod]
        public void AddCountry_ShouldAddCorrect_IfCountryDoesNotExist()
        {
            Continent continent = new Continent(5, "Asia");
            Country country = new Country(8, "India", 88, 510000);
            continent.AddCountry(country);

            continent.Countries.Count.Should().Be(1);
            continent.Countries.First().Should().BeEquivalentTo(country);
        }

        [TestMethod]
        public void AddCountry_ShouldChangeContinent_IfCountryHasOtherContinent()
        {
            Continent continent = new Continent(5, "Asia");
            Continent newContinent = new Continent(2, "Europe");
            Country country = new Country(8, "India", 88, 510000, continent);

            newContinent.AddCountry(country);

            country.Continent.Should().BeEquivalentTo(newContinent);
        }

        #endregion AddCountry

        #region HasCountry

        [TestMethod]
        public void HasCountry_condition_expectedValue()
        {
            Continent continent = new Continent(5, "Asia");
            Country country1 = new Country(8, "India", 88, 510000);
            Country country2 = new Country(5, "China", 1000, 11111111);
            Country country3 = new Country(10, "Japan0", 106564, 5100445400.454564);
            continent.AddCountry(country1);
            continent.AddCountry(country2);
            continent.AddCountry(country3);

            continent.Countries.Count.Should().Be(3);
        }

        #endregion HasCountry

        #region UpdateCountry

        //TODO updatecountry - exeptionmethods
        [TestMethod]
        public void UpdateCountry_ShouldUpdateCountryCorrectly_IfCountryExistWithinContinent()
        {
            Continent continent = new Continent(5, "Asia");
            Country country = new Country(8, "India", 88, 510000);
            continent.AddCountry(country);

            Country newCountry = new Country(9, "Japan", 88, 510000);
            continent.UpdateCountry(country, newCountry);

            continent.Countries.Count.Should().Be(1);
            Country test = continent.Countries.First();
            test.Id.Should().Be(newCountry.Id);
            test.Name.Should().Be(newCountry.Name);
        }

        #endregion UpdateCountry

        //TODO population
    }
}