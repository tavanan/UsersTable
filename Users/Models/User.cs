using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Users.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Invalid First Name!")]
        public string FirstName { get; set; }
        
        [Required]
        [DisplayName("Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Invalid Last Name!")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Street Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Invalid Street Name!")]
        public string StreetName { get; set; }

        [Required]
        [DisplayName("House Number")]
        [RegularExpression(@"^([0-9]{1,})$", ErrorMessage = "Invalid House Number!")]
        public string HouseNumber { get; set; }

        [DisplayName("Apartment Number")]
        [RegularExpression(@"^([0-9]{0,})$", ErrorMessage = "Invalid Apartment Number!")]
        public string ApartmentNumber { get; set; }

        [Required]
        [DisplayName("Postal Code")]
        [RegularExpression(@"^([0-9]{5})$", ErrorMessage = "Invalid Postal Code!")]
        public string PostalCode { get; set; }

        [Required]
        [DisplayName("Town")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Invalid Town!")]
        public string Town { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]{9})$", ErrorMessage = "Invalid Mobile Number!")]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DisplayName("Age")]
        
        public int Age { get; set; }
    }
}
