using System.ComponentModel.DataAnnotations;

namespace ResumeManager.Models
{
    public class UserLogin
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string UserName { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";

    }
}
