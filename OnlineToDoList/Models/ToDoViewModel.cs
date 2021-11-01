using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineToDoList.Models
{
    public class ToDoViewModel
    {
        public int Id { get; set; }

        //[Display(Name = "Desc")]
        [Required(ErrorMessage = "Required Field")]
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Due Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]

        public DateTime DueDate { get; set; }

        public virtual ApplicationUser User { get; set; }

    }

}