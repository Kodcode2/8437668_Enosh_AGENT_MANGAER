namespace AgentRestVM.ModelVM
{
    public class MissionVM
    {

        public int Id {  get; set; }

        public string NickName { get; set; }
        public int Location_X_Agent { get; set; } 
        public int Location_Y_Agent { get; set; } 

        public string TargetName { get; set; }

        public string TargetPosition { get; set; }
        public int Location_X_Target { get; set; } 
        public int Location_Y_Target { get; set; } 
        public double? TimeLeft { get; set; }
        public double? Distance { get; set; }


    }
}
