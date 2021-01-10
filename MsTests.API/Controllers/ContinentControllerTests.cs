//using GeoService.API.Controllers;
//using GeoService.Domain.Interfaces;
//using GeoService.Domain.Managers;
//using GeoService.EF.DataAccess;
//using Microsoft.Extensions.Configuration;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;

//namespace MsTests.API.Controllers
//{
//    [TestClass]
//    public class ContinentControllerTests
//    {
//        private readonly ContinentManager continentManager;
//        private readonly CountryManager countryManager;
//        private readonly ContinentController continentController;

//        public ContinentControllerTests()
//        {
//            continentManager = new ContinentManager(new UnitOfWork());
//            countryManager = new CountryManager(new UnitOfWork());
//            continentController = new ContinentController(continentManager, countryManager);
//        }

//        public void GetContinent_Should_expectedValue()
//        {
//            // Arrange 


//            // Act 


//            // Assert           

//        }
//    }
//}
