using Microsoft.AspNetCore.Mvc;
using SportApp.Server.Services;
using Common.DAL.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using SportApp.Shared.ViewModel;
using System.Collections.Generic;

namespace SportApp.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private IActivityServices _activityServices;
        private readonly IMapper _mapper;

        public ActivityController(IActivityServices activityServices, IMapper mapper)
        {
            _activityServices = activityServices;
            _mapper = mapper;
        }


        [Authorize]
        [HttpPost("Process")]
        public int ProcessActivity([FromBody] Activity activity)
        {
            return _activityServices.ProcessActivity(activity);
        }

        [Authorize]
        [HttpGet("Calories")]
        public IActionResult GetCalories(int trainingSessionId)
        {
            var sports = _activityServices.GetCalories(trainingSessionId);
            return Ok(sports);
        }

        [Authorize]
        [HttpGet("Session")]
        public IActionResult GetTrainingSession(int userId)
        {
            var trainingSessions = _activityServices.GetTrainingSession(userId);
            var dto = _mapper.Map<IEnumerable<TrainingSessionDTO>>(trainingSessions);
            return Ok(dto);
        }

        [Authorize]
        [HttpDelete("DeleteTrainingSession")]
        public void DeleteTrainingSession(int trainingSession)
        {
            _activityServices.DeleteTrainingSession(trainingSession);
        }
    }
}
