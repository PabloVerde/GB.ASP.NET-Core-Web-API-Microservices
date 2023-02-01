using MetricsAgent.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.Extensions.Logging;
using Core.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Interfaces;
using AutoMapper;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private ICpuMetricsRepository repository;
        private readonly ILogger<CpuMetricsController> logger;
        private readonly IMapper mapper;
        public CpuMetricsController(ICpuMetricsRepository repository, ILogger<CpuMetricsController> logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.logger.LogDebug(1, "NLog подключен к CpuMetricsController");
            this.mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            logger.LogInformation($"Запрос POST: {request.Time}|{request.Value}");

            repository.Create(new CpuMetric
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            logger.LogInformation($"Запрос GET: ALL {DateTime.Now}");

            IList<CpuMetric> metrics = repository.GetAll();

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };

            foreach(var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<CpuMetricDto>(metric));
            }    

            return Ok(response);
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics(
            [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            logger.LogInformation($"Запрос GET: {fromTime}|{toTime}");

            return Ok();
        }
    }
}
