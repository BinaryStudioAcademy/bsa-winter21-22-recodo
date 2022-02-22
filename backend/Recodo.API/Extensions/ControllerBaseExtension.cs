using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Recodo.API.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static int GetUserIdFromToken(this ControllerBase controller)
        {
            var claimsUserId = controller.User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            return int.Parse(claimsUserId);
        }
    }
}
