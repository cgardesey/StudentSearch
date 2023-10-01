using PagedList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentSearch.Models
{
    public class StudentSearchViewModels
    {
        [Required(ErrorMessage = "Select Academic Year")]
        [Display(Name = "Academic Year")]
    
        public SelectList AcadyearList { get; set; }



        [Required(ErrorMessage = "Select Semester")]
        [Display(Name = "Semester")]
  
        public List<SelectListItem> SemList { get; set; }
        public SelectList CollegeList { get; set; }


        public string STUDENTID { get; set; }
        public string SURNAME { get; set; }
        public string OTHERNAME { get; set; }
        public string INDEXNO { get; set; }
        public string NAME { get; set; }
        public string PRIMARYMOBILE { get; set; }
        public int YR { get; set; }
        public string SCHOOLMOBILE { get; set; }
        public string EMAIL { get; set; }
        public string POSTADD1 { get; set; }
        public string POSTADD2 { get; set; }
        public string POSTADD3 { get; set; }
        public string POSTADD4 { get; set; }
        public string GUARDIANNAME { get; set; }
        public string GUARDIANADDRESS { get; set; }
        public string GUARDIANEMAIL { get; set; }
        public string GUARDIANPHONE { get; set; }
        public byte[] PICTURE { get; set; }
        public string STUDENTPROGRAMME { get; set; }


        public IPagedList<StudentSearchViewModels> SomeList { get; set; }

        public string FULLNAME { get; set; }

        public int RefNumber { get; set; }




        public string searchStudent { get; set; }
        public int Year { get; set; }
        public int Sem { get; set; }
        public int? College { get; set; }
        public IList StudentDetails { get; set; }
       
    
    }
}