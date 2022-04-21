namespace Projeto_BD1.Models
{
    public class BodyResponse
    {
        public BodyResponse(Boolean status, string Message, Object data = null)
        {
            this.Status = status;
            this.Message = Message;
            this.data = data;
        }
        public Boolean Status { get; set; }
        public string Message { get; set; }

        public Object data { get; set; }
    }
}
