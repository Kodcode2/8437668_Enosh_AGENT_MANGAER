using System.ComponentModel.DataAnnotations;

namespace AgentRestApi.Models
{
    public class TargetModel
    {
        public enum Status
        {
            live,
            elimintated
        }

        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string Position { get; set; }

        public string PhotoUrl { get; set; }
        public int LocationX { get; set; } = -1;
        public int LocationY { get; set; } = -1;

        public Status TargetStatus { get; set; }

        public List<MissionModel> Mission { get; set; } = [];
    }
}
