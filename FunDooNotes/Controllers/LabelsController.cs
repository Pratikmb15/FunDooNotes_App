using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.Entity;
using System.Security.Claims;

namespace FunDooNotes.Controllers
{
    [Route("api/labels")]
    [ApiController]
    public class LabelsController : ControllerBase
    {

        private readonly ILabelsService manager;
        private readonly INotesBusiness _NotesBusiness;

        public LabelsController(ILabelsService manager, INotesBusiness notesBusiness)
        {
            this.manager = manager;
            _NotesBusiness = notesBusiness;
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateLabel(LabelsModel model)
        {
            try
            {
                int UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var note = _NotesBusiness.GetNotesById(model.NotesId, UserId);
                if (note.Id != UserId) { 
                throw new Exception("User Does'nt have access to this note !");
                }
                var create = manager.CreateLabel(UserId, model);
                return Ok(new ResponseModel<Labels> { Success = true, Message = "Created label", Data = create });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Exception", Data = e.Message });
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                int UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var getAll = manager.GetAllLabels(UserId);
                return Ok(new ResponseModel<List<Labels>> { Success = true, Message = "All labels", Data = getAll });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Exception", Data = e.Message });
            }
        }
        [Authorize]
        [HttpGet("GetLabelById")]
        public IActionResult GetLabel(int LabelId)
        {
            try
            {
                int UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var getLabel = manager.GetLabelById(LabelId, UserId);
                if (getLabel == null) {
                    throw new Exception("Label not found !");
                }
                return Ok(new ResponseModel<Labels> { Success = true, Message = "Label info", Data = getLabel });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Exception", Data = e.Message });
            }
        }


        [Authorize]
        [HttpPut]
        public IActionResult UpdateLabel(int LabelId, string LabelName)
        {
            try
            {
                int UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var updateLabel = manager.UpdateLabel(LabelName, LabelId, UserId);
                if (updateLabel)
                {
                    return Ok(new ResponseModel<bool> { Success = true, Message = "updated label", Data = updateLabel });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = true, Message = "No label found" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Exception", Data = e.Message });
            }
        }

        [Authorize]
        [HttpDelete]
        public IActionResult DeleteLabel(int LabelId)
        {
            try
            {
                int UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var deleteLabel = manager.DeleteLabel(LabelId, UserId);
                if (deleteLabel)
                {
                    return Ok(new ResponseModel<bool> { Success = true, Message = "Label deleted successfully", Data = deleteLabel });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = true, Message = "No label found" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseModel<string> { Success = true, Message = "Exception", Data = e.Message });
            }
        }
    }
}
