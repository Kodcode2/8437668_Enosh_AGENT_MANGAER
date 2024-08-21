using System.ComponentModel.DataAnnotations;

namespace AgentRestApi.Models
{
    public class AgentModel
    {
        public enum Status
        {
            offDuty,
            Activete
            
        }
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string NickName { get; set; }

        [Required, StringLength (100)]
        public string PhotoUrl { get; set; }

        public int LocationX { get; set; } = -1;

        public int LocationY { get; set; } = -1;

        public Status Agentstatus { get; set; }

        public List<MissionModel> Missions { get; set; } = [];   


    }
}
