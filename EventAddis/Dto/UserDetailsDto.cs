namespace EventAddis.Dto
{
    public class UserDetailsDto
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime RegisteredAt { get; set; }
        public string Password { get; set; }



    }
}
