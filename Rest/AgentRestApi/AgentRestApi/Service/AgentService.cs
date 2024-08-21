using AgentRestApi.Data;
using AgentRestApi.Dto;
using AgentRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AgentRestApi.Service
{
    public class AgentService(ApplicationDbContext context)  : IAgentService
    {
        public async Task<AgentModel> CreateAgentAsync(AgentDto agentDto)
        {
            if (agentDto == null)
            {
                throw new Exception("not Found");
            }
            AgentModel agentModel = new()
            {
                NickName = agentDto.NickName,
                PhotoUrl = agentDto.PhotoUrl,
            };
            await context.AddAsync(agentModel);
            await context.SaveChangesAsync();
            return agentModel;
        }

        public async Task<AgentModel> UpdateAgenByIdtAsync(AgentDto agentDto, int id)
        {
            var ById = await context.Agents.FindAsync(id);
            if (ById == null)
            {
                throw new Exception("not Found");
            }
            ById = new()
            {
                NickName = agentDto.NickName,
                PhotoUrl = agentDto.PhotoUrl,              
            };
            await context.SaveChangesAsync();
            return ById;
        }

        public async Task<List<AgentModel>> GetAllAsync() =>
            await context.Agents.ToListAsync();
        



    }
}
