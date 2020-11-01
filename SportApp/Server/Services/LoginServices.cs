﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportApp.Shared.ViewModel;
using Common.DAL;
using Common.DAL.Models;
using SportApp.Shared.Authenticate;
using SportApp.Server.Helpers;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;


namespace SportApp.Server.Services
{
    public interface ILoginServices
    {
        public List<Users> GetUsers(int id);
        public AuthenticateResponse Authenticate(AuthenticateRequest model);
    }

    public class LoginServices : ILoginServices
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var authenticationUsers = _unitOfWork.UsersRepository.Get(
                x => x.Login == model.Username && x.Password == model.Password,null).FirstOrDefault();
            if (authenticationUsers == null)
                return null;

            User user = new User();
            user.Id = authenticationUsers.Id;
            user.Username = authenticationUsers.Login;

            var token = GenerateJwtToken(user);
            return new AuthenticateResponse(user, token);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("papaks jsjsdk sadsjasdisasa!!sasxa"); // long, random string to generate
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(ClaimTypes.Name, user.Username),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public List<Users> GetUsers(int id)
        {
            var table = _unitOfWork.UsersRepository.Get().Distinct().ToList();
            return table;
        }
    }
}
