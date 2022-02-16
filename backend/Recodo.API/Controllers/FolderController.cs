using Microsoft.AspNetCore.Mvc;
using Recodo.BLL.Services;
using Recodo.Common.Dtos.Folder;
using System.Threading.Tasks;

namespace Recodo.API.Controllers
{
    [ApiController]
    [Route("api/folders")]
    public class FolderController : ControllerBase
    {
        private readonly FolderService _folderService;

        public FolderController(FolderService folderService)
        {
            _folderService = folderService;
        }

        [HttpPost]
        public async Task<ActionResult<FolderDTO>> Create([FromBody] NewFolderDTO newFolderDTO)
        {
            return Ok(await _folderService.CreateFolder(newFolderDTO));
        }
    }
}
