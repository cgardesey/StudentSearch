using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentSearch.Models
{
    public class RolesIndexViewModel
    {
        public string Username { get; set; }
        public string Roles { get; set; }
        public IList<RolesIndexViewModel> UserList { get; set; }
        public string Searchusers { get; set; }

    }

    public class EditUserRoleViewModel
    {
        public int COLLEGEUSERID { get; set; }
        public int COLLEGEUSERROLEID { get; set; }
        public string Username { get; set; }
        public IList<EditUserRoleViewModel> RolesAssigned { get; set; }
        public string ROLENAME { get; set; }

        [Display(Name = "Role")]
        public int ROLEID { get; set; }
        public System.Web.Mvc.SelectList Roles { get; set; }

    }


}