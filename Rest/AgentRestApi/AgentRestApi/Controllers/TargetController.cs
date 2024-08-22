using AgentRestApi.Dto;
using AgentRestApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgentRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TargetController(ITargetService targetService) : ControllerBase
    {
        [HttpPost("CreateAgentAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateTarget([FromBody] TargetDto targettDto)
        {
            try
            {
                var agentModel = await targetService.CreateTargettAsync(targettDto);
                return Created("new agent", agentModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllAgents() =>
          Ok(await targetService.GetAllAsync());

        [HttpPut("{id}/pin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CrearLoction([FromBody] LocationDto locationDto, int id)
        {
            try
            {
                var location = await targetService.UpdateLocationByIdTargetAsync(locationDto, id);
                return Ok( location);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/moev")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateMoveLocationByIdAsync(PinDto pinDto, int id)
        {
            try
            {
              return  Ok(await targetService.MoveLocationByIdTargetAsync(pinDto, id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

