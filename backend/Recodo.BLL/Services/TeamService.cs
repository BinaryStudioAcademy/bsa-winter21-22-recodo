using Recodo.DAL.Context;
using Recodo.DAL.Entities;
using System.Threading.Tasks;

namespace Recodo.BLL.Services
{
    public class TeamService
    {
        private readonly RecodoDbContext _context;

        public TeamService(RecodoDbContext context)
        {
            _context = context;
        }

        public async Task CreateTeam(User userEntity)
        {
            Team team = new Team();
            team.Name = userEntity.Email.Split('@')[0] + "'s team";
            team.AuthorId = userEntity.Id;
            userEntity.Teams.Add(team);

            await _context.SaveChangesAsync();
        }
    }
}
