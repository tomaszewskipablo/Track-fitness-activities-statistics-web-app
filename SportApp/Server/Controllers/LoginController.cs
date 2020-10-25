using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportApp.Shared.ViewModel;
using SportApp.Server.Services;
using Common.DAL.Models;
using AutoMapper;

namespace SportApp.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginServices _loginServices;
        private readonly IMapper _mapper;

        public LoginController(ILoginServices loginServices, IMapper mapper)
        {
            _loginServices = loginServices;
            _mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult GetUsers(int id)
        {
            var personnelTable = _loginServices.GetUsers(5);
            var dto = _mapper.Map<IEnumerable<UserDTO>>(personnelTable);
            return Ok(dto);
        }
    }
}
