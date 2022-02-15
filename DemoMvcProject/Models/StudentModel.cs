using System;
using System.ComponentModel;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMvcProject.Models
{
    // [Table(name="StudentData")]

    public class StudentModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First Name Is Required")]
        [MaxLength(30, ErrorMessage = "Length Cannot Exceed 30 characters.")]
        [DisplayName("First Name")]
        public string FirstName {get;set;}
        


        [MaxLength(30, ErrorMessage = "Length Cannot Exceed 30 characters.")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Date of Birth")]
        [Required(ErrorMessage = "Date of Birth is Required")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        [DisplayName("Email Address")]
        [EmailAddress]
        [Required]

        [Remote(action: "CheckEmailPresent", controller: "Student", AdditionalFields = "Id", ErrorMessage = "This Email Id Already Exists in Database")]
        public string Email { get; set; }

        [Phone]
        [DisplayName("Phone Number")]
        public string Phone { get; set; }




    }
}
