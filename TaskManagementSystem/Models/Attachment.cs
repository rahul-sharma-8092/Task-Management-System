using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class Attachment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Task Id")]
        public string TaskId{ get; set; }

        [Required]
        [Display(Name = "Employee")]
        public string EmployeeName{ get; set; }

        public HttpPostedFileBase Doc {  get; set; }

        [Required]
        public List<HttpPostedFileBase> Docs { get; set; }
    }
}