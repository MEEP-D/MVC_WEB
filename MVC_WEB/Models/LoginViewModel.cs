namespace MVC_WEB.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
