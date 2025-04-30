using BUGSystem.BL;
using BUGSystem.BL.DTOs.UserBug;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BUGSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugsController : ControllerBase
    {
        private readonly IBugManager _bugManager;
        private readonly IAttachmentManager _attachmentManager;
        public BugsController(IBugManager bugManager,
            IAttachmentManager attachmentManager)
        {
            _bugManager = bugManager;
            _attachmentManager = attachmentManager;
        }

        [HttpPost]
        public async Task<Results<NoContent, BadRequest>> AddBug([FromBody] BugAddDto bugAddDto)
        {
            try
            {
                
                await _bugManager.AddBugAsync(bugAddDto);
                return TypedResults.NoContent();
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest();
            }
        }

        [HttpGet]
        public async Task<Results<Ok<List<BugViewDto>>, BadRequest<string>>> GetAllBugs()
        {
            try
            {
                var bugs = await _bugManager.GetAllBugsAsync();
                return TypedResults.Ok(bugs);
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex.Message);
            }
        }

        [HttpGet("{bugId}")]
        public async Task<Results<Ok<BugViewDto>, NotFound<string>>> GetBugById(Guid bugId)
        {
            try
            {
                var bug = await _bugManager.GetBugByIdAsync(bugId);
                if (bug == null)
                {
                    return TypedResults.NotFound($"Bug with ID {bugId} not found.");
                }
                return TypedResults.Ok(bug);
            }
            catch (Exception ex)
            {
                return TypedResults.NotFound(ex.Message);
            }
        }

        [HttpPost("{bugId:guid}/attachments")]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> UploadAttachment([FromRoute] Guid bugId, [FromForm] AttachmentUploadDto dto)
        {
            var response = await _attachmentManager
                .SaveAttachmentAsync(dto, bugId);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.BadRequest(response);
            }
        }
        [HttpGet("{bugId:guid}/attachments")]
        public async Task<Results<Ok<GeneralResult>, NotFound<GeneralResult>>> GetAttachmentsByBugId([FromRoute] Guid bugId)
        {
            var response = await _attachmentManager
                .GetAttachmentsByBugIdAsync(bugId);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.NotFound(response);
            }
        }
        [HttpDelete("{bugId:guid}/attachments/{attachmentId:guid}")]
        public async Task<Results<Ok<GeneralResult>, NotFound<GeneralResult>>> DeleteAttachment([FromRoute] Guid bugId, [FromRoute] Guid attachmentId)
        {
            var response = await _attachmentManager
                .DeleteAttachmentByIdAndBugIdAsync(bugId, attachmentId);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.NotFound(response);
            }
        }
        [HttpPost("{bugId:guid}/assignees")]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> AssignUserToBug([FromRoute] Guid bugId, [FromBody] AssignUserDto assignUserRequestDto)
        {
            var response = await _bugManager
                .AssignBugToUserAsync(bugId, assignUserRequestDto);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.BadRequest(response);
            }
        }
        [HttpDelete("{bugId:guid}/assignees/{userId:guid}")]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> RemoveUserFromBug([FromRoute] Guid bugId, [FromRoute] Guid userId)
        {
            var response = await _bugManager
                .RemoveBugAssignmentAsync(bugId, userId);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.BadRequest(response);
            }
        }
    }
}
