using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name ="Full Name")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        [Display(Name = "Date of Joining")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfJoining { get; set; }

        [Required]
        public int RoleId { get; set; }

        public string Role { get; set; }

        public string ImagePath { get; set; }
        
        public HttpPostedFileBase Image { get; set; }

        public bool IsDeleted { get; set; }

        public int TotalUsers { get; set; }

        public List<User> UserList { get; set; }

    }

    public class UserWithPagination
    {
        public List<User> UserList { get; set; }

        public Pagination Pagination { get; set; } = new Pagination();
    }
}