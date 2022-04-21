namespace Projeto_BD1.Models
{
    public class EndUser
    {
        public int ID { get; set; }
        public string Name{ get; set; }
        public string Email { get; set; }
        public int CenterID { get; set; }
        public Boolean IsActive { get; set; }
    }
}
