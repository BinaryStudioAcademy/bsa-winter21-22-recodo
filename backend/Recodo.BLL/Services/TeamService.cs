﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Recodo.BLL.Exceptions;
using Recodo.DAL.Context;
using Recodo.DAL.Entities;
using System.Threading.Tasks;
using Thread_.NET.BLL.Services;

namespace Recodo.BLL.Services
{
    public class TeamService
    {
        private readonly RecodoDbContext _context;
        private readonly AuthService _authService;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;

        public TeamService(RecodoDbContext context, IConfiguration configuration, AuthService authService, EmailService emailService)
        {
            _authService = authService;
            _emailService = emailService;
            _configuration = configuration;
            _context = context;
        }

        public async Task CreateTeam(User userEntity)
        {
            Team team = new Team();
            team.Name = userEntity.WorkspaceName + " team";
            team.AuthorId = userEntity.Id;
            userEntity.Teams.Add(team);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateTeam(int userId, string newName)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(t => t.AuthorId == userId);
            if (team == null)
            {
                throw new NotFoundException("Team");
            }

            team.Name = newName;
            await _context.SaveChangesAsync();
        }

        public async Task<string> SendInviteLink(int userId, string email)
        {
            var existUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (existUser == null)
            {
                throw new NotFoundException("User");
            }

            var token = await _authService.GenerateAccessToken(userId, existUser.Email, existUser.Email);
            string clientHost = _configuration["ClientHost"];
            string url = $"{clientHost}/team-invite/" + token.AccessToken;

            await _emailService.SendEmailAsync(email, "Team Invite", url);

            return url;
        }
    }
}
