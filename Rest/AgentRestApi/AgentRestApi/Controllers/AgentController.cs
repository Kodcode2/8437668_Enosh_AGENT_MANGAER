using AgentRestApi.Dto;
using AgentRestApi.Models;
using AgentRestApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgentRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController(IAgentService agentService) : ControllerBase
    {
        [HttpPost("CreateAgentAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAgent([FromBody] AgentDto agentDto)
        {
            try
            {
                var agentModel = await agentService.CreateAgentAsync(agentDto);
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
          Ok(await agentService.GetAllAsync());
    }
}
