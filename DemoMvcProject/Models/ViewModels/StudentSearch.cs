using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace DemoMvcProject.Models.ViewModels
{
    public class StudentSearch
    {
    
        
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Date of Birth")]
      
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ? DOB { get; set; }

        [DisplayName("Email Address")]
        public string Email { get; set; }

        
        [DisplayName("Phone Number")]
        public string Phone { get; set; }




    }
}
