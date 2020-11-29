using Microsoft.AspNetCore.Mvc;
using SportApp.Server.Services;
using Common.DAL.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

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
        [HttpPost("TrainingSession")]
        public void RetrieveTrainingSession([FromBody] Activity activity)
        {
            TrainingSession TrainingSession =  new TrainingSession();
            _activityServices.PostActivityStats(TrainingSession);
            //var dto = _mapper.Map<IEnumerable<UserDTO>>(personnelTable);
            //return Ok(dto);
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
            var sports = _activityServices.GetTrainingSession(userId);
            return Ok(sports);
        }
    }
}
