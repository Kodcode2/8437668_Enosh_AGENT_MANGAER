using AgentRestApi.Data;
using AgentRestApi.Dto;
using AgentRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AgentRestApi.Service
{
    public class TargetService(ApplicationDbContext context) : ITargetService
    {
        public async Task<TargetModel> CreateTargettAsync(TargetDto targetDto)
        {
            if (targetDto == null)
            {
                throw new Exception("not Found");
            }
            TargetModel targetModel = new()
            {
                Name = targetDto.Name,
                PhotoUrl = targetDto.PhotoUrl,
            };
            await context.AddAsync(targetModel);
            await context.SaveChangesAsync();
            return targetModel;
        }
        public async Task<List<TargetModel>> GetAllAsync() =>
         await context.Targets.ToListAsync();

    }
}
