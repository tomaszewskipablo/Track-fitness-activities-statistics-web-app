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
    public class SportController : ControllerBase
    {
        private ISportServices _sportServices;
        private readonly IMapper _mapper;

        public SportController(ISportServices sportServices, IMapper mapper)
        {
            _sportServices = sportServices;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("")]
        public IActionResult GetSports()
        {
            var sports = _sportServices.GetSports();
            var dto = _mapper.Map<IEnumerable<SportDTOCombobox>>(sports);
            return Ok(dto);
        }
    }
}
