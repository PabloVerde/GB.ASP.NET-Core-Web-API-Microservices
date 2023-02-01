using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using MetricsAgent.DAL;
using Moq;
using MetricsAgent.Responses;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL.Models;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsController controller;
        private Mock<IRepository<CpuMetric>> mock;
        private Mock<ILogger<CpuMetricsController>> mockLogger;

        public CpuMetricsControllerUnitTests()
        {
            mock = new Mock<IRepository<CpuMetric>>();
            mockLogger = new Mock<ILogger<CpuMetricsController>>();
            controller = new CpuMetricsController(mock.Object, mockLogger.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();
            var result = controller.Create(new MetricsAgent.Requests.CpuMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });

            mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());

        }

        [Fact]
        public void GetAll_From_Repository()
        {
            mock.Setup(repository => repository.GetAll()).Verifiable();         

            mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
        }
    }
}