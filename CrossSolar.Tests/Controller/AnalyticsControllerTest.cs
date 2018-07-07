using CrossSolar.Controllers;
using CrossSolar.Domain;
using CrossSolar.Models;
using CrossSolar.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CrossSolar.Tests.Mocked.MockedRepository;

namespace CrossSolar.Tests.Controller
{
    public class AnalyticsControllerTest
    {

        private IPanelRepository _panelRepositoryMock;
        private IAnalyticsRepository _analyticsRepository; 
        private IDayAnalyticsRepository _dayAnalyticsRepository;

        private AnalyticsController _controller;


        public AnalyticsControllerTest()
        {
            _analyticsRepository = new MockedAnalyticsRepository();

            _panelRepositoryMock = new MockPanelRepository();

            _dayAnalyticsRepository = new MockedDayAnalyticsRepository();


            _controller = new AnalyticsController(_analyticsRepository, _panelRepositoryMock, _dayAnalyticsRepository);
        }

        [Fact]
        public void PostTest()
        {
            var model = new OneHourElectricityModel()
            {
                DateTime = DateTime.Now,
                KiloWatt = 100
            };

            var result = _controller.Post(1, model).Result;

            // Assert
            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public void DayResultsTest()
        {
            var result = _controller.DayResults(1).Result;

            // Assert
            Assert.NotNull(result);

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public void GetTest()
        {
            var result = _controller.Get(1).Result;

            // Assert
            Assert.NotNull(result);

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);
        }

    }
}
