using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start Date is required")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "End Date is required")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public string Status { get; set; } // Open, Closed

        [Display(Name = "Is Closed")]
        public bool IsClosed { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public List<Project> ProjectList { get; set; }

        public string SubDescription
        {
            get
            {
                string res = string.Empty;
                if (string.IsNullOrEmpty(Description))
                {
                    return "";
                }
                res = Description.Length < 100 ? Description : Description.Substring(0, 100) + "...";
                return res;
            }
        }
    }
}