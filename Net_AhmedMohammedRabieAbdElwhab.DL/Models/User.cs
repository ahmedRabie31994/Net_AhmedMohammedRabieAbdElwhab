using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace Net_AhmedMohammedRabieAbdElwhab.DL.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name field is Required")]
        [MaxLength(50,ErrorMessage ="max length of first Name is 50 char")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Last Name field is Required")]
        [MaxLength(70, ErrorMessage = "max length of first Name is 70 char")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Email field is Required")]
        [EmailAddress(ErrorMessage ="Not corrected format")]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        
        [Required(ErrorMessage = "UserName field is Required")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }
        [MaxLength(11,ErrorMessage ="mobile number must be 11 charachter")]
        public string PhoneNumber { get; set; }
    }
}
