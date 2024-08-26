using System.ComponentModel.DataAnnotations;

namespace AgentRestVM.Models
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

        [Required, StringLength(100)]
        public string PhotoUrl { get; set; }

        public int LocationX { get; set; } 

        public int LocationY { get; set; }

        public Status Agentstatus { get; set; }

     
    }
}
