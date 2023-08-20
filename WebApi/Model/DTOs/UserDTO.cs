namespace WebApi.Model.DTOs
{
    public class UserDTO
    {
        public UserDTO(int id, string username, string pass, string email)
        {
            this.Id = id;
            this.Username = username;   
            this.Password = pass;
            this.Email = email;
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
