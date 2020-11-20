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
using SportApp.Shared.Authenticate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

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

        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] AuthenticateRequest model)
        {
            try
            {
                var response = _loginServices.Authenticate(model);

                if (response == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public bool SignupAsync([FromBody] SignupRequest model)
        {
            try
            {
                var user = _mapper.Map<Users>(model);
                return _loginServices.Signup(user);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [AllowAnonymous]
        [HttpPost("exist")]
        public IActionResult PassExist([FromBody] AuthenticateRequest model)
        {
                return Ok(_loginServices.PassExist(model));
        }

        [Authorize]
        [HttpGet("")]
        public IActionResult GetUsers(int id)
        {
            var personnel = _loginServices.GetUsers(id);
            var dto = _mapper.Map<UserDTO>(personnel);
            return Ok(dto);
        }
    }
}
