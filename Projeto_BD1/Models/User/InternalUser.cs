namespace Projeto_BD1.Models
{
    public class InternalUser
    {
        public int EndUserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int CenterID { get; set; }
        public Boolean IsConfirmEmailSent { get; set; }
        public Boolean IsConfirmed{ get; set; }
        public Boolean IsCleaningStaff { get; set; }

    }
}
