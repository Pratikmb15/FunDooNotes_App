using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.Entity;
using System.Security.Claims;

namespace FunDooNotes.Controllers
{
    [Authorize]
    [Route("api/Collaborator")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorService manager;
        public CollaboratorController(ICollaboratorService collaboratorService)
        {
            manager = collaboratorService;
        }

        [HttpPost]
        public IActionResult AddCollaborator(CollaboratorModel model)
        {
            try
            {
                int UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var create = manager.CreateCollaborator(UserId, model);
                return Ok(new ResponseModel<Collaborator> { Success = true, Message = "Collaborator Added", Data = create });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Exception", Data = e.Message });
            }
        }
        [HttpGet]
        public IActionResult GetAllCollaborators()
        {
            try
            {
                int UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var getAll = manager.GetCollaborators(UserId);
                return Ok(new ResponseModel<List<Collaborator>> { Success = true, Message = "All Collaborators", Data = getAll });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Exception", Data = e.Message });
            }
        }

        [HttpDelete]
        public IActionResult RemoveCollaborator(int UserId)
        {
            try
            {

                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var delete = manager.DeleteCollaborator(userId, UserId);
                if (delete)
                {
                    return Ok(new ResponseModel<bool> { Success = true, Message = "Collaborator Removed", Data = delete });
                }
                else
                {
                    throw new Exception("Collaborator not found !");
                }

            }
            catch (Exception e)
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Exception", Data = e.Message });

            }
        }
    }
}
