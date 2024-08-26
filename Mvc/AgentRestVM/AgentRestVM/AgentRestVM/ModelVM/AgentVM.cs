using System.ComponentModel.DataAnnotations;

namespace AgentRestVM.ModelVM
{
    public class AgentVM
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string NickName { get; set; }

        [Required, StringLength(100)]
        public string PhotoUrl { get; set; }

        public int LocationX { get; set; } = -1;

        public int LocationY { get; set; } = -1;

        public string Agentstatus { get; set; } 

        public double TimeLeft { get; set; }

        public int Amount { get; set; }
    }
}
