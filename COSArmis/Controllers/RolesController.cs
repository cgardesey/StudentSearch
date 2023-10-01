using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using StudentSearch.Models;




namespace StudentSearch.Controllers
{
     [Authorize(Roles = "Administrator")]
    public class RolesController : Controller
    {

        private MyUserManager _userManager;
        private ApplicationDbContext _db = new ApplicationDbContext();
        private CollegeStuDetailsEntities1 db = new CollegeStuDetailsEntities1();

        public MyUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<MyUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        [Authorize(Roles = "Administrator")]
        public ActionResult Index(RolesIndexViewModel model, string Searchusers = "")
        {

            var userRolesOverview = (from a in db.procCollegeUserRolesOverview() where (a.USERNAME.ToUpper().Contains(Searchusers.ToUpper()) || string.IsNullOrEmpty(Searchusers)) select new RolesIndexViewModel { Username = a.USERNAME, Roles = a.ROLESASSIGNED }).ToList();


            model.UserList = userRolesOverview;

            return View(model);
        }

        public ActionResult EditUserRole(EditUserRoleViewModel model)
        {
            var rolesassigned = (from r in db.COLLEGEROLE
                                 join ur in db.COLLEGEUSERROLE on r.COLLEGEROLEID equals ur.COLLEGEROLEID
                                 join au in db.COLLEGEUSER on ur.COLLEGEUSERID equals au.COLLEGEUSERID
                                 where au.USERNAME == model.Username
                                 select new EditUserRoleViewModel { COLLEGEUSERID = au.COLLEGEUSERID, ROLENAME = r.ROLENAME, ROLEID = r.COLLEGEROLEID }).ToList();



            var RoleList = (from r in db.COLLEGEROLE select new { ROLEID = r.COLLEGEROLEID, ROLENAME = r.ROLENAME }).AsEnumerable();
            model.Roles = new SelectList(RoleList, "ROLEID", "ROLENAME");


            model.RolesAssigned = rolesassigned;
            model.COLLEGEUSERID = (from a in db.COLLEGEUSER where a.COLLEGEUSERID == model.COLLEGEUSERID select new { COLLLEGEUSERID = a.COLLEGEUSERID }.COLLLEGEUSERID).FirstOrDefault();

            return View(model);
        }

        public ActionResult DeassignUserRoles(EditUserRoleViewModel model)
        {
            AppUser user = _db.Users.Where(u => u.UserName.Equals(model.Username, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            //if
            //    (this.UserManager.IsInRole(user.Id, model.ROLENAME))
            //     this.UserManager.RemoveFromRole(user.Id, model.ROLENAME);

            var roleassignment = db.COLLEGEUSERROLE.Find(model.COLLEGEUSERID, model.ROLEID);
            if (roleassignment != null)
            {
                try
                {
                    db.COLLEGEUSERROLE.Remove(roleassignment);
                    db.SaveChanges();
                    return RedirectToAction("EditUserRole", new { Username = model.Username });
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }

            }

            return RedirectToAction("EditUserRole", new { Username = model.Username });
        }

        public ActionResult AssignRole(EditUserRoleViewModel model)
        {
            model.COLLEGEUSERID = (from a in db.COLLEGEUSER where a.USERNAME == model.Username select new { COLLEGEUSERID = a.COLLEGEUSERID }.COLLEGEUSERID).FirstOrDefault();
            model.ROLENAME = (from r in db.COLLEGEROLE where r.COLLEGEROLEID == model.ROLEID select new { ROLENAME = r.ROLENAME }.ROLENAME).FirstOrDefault();

            //AppUser user = this.UserManager.FindByName(model.Username);
            //this.UserManager.AddToRole(user.Id, model.ROLENAME);

            COLLEGEUSERROLE roleassignment = new COLLEGEUSERROLE
            {
                COLLEGEUSERID = model.COLLEGEUSERID,
                COLLEGEROLEID = model.ROLEID,
            };
            try
            {
                db.COLLEGEUSERROLE.Add(roleassignment);
                db.SaveChanges();
                return RedirectToAction("EditUserRole", new { Username = model.Username });
            }
            catch (Exception)
            {
                return RedirectToAction("EditUserRole", new { Username = model.Username });
            }


        }
        public ActionResult ManageUserRoles()
        {
            // prepopulat roles for the view dropdown
            var list = _db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            var Users = _db.Users.OrderBy(u => u.UserName).ToList().Select(uu => new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
            ViewBag.Roles = list;
            ViewBag.Users = Users;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {

            var Users = _db.Users.OrderBy(u => u.UserName).ToList().Select(uu => new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
            ViewBag.Users = Users;
            AppUser user = _db.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            this.UserManager.AddToRole(user.Id, RoleName);

            ViewBag.ResultMessage = "Role created successfully !";

            // prepopulat roles for the view dropdown
            var list = _db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                var Users = _db.Users.OrderBy(u => u.UserName).ToList().Select(uu => new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
                ViewBag.Users = Users;
                AppUser user = _db.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                //var account = new AccountController();

                ViewBag.RolesForThisUser = this.UserManager.GetRoles(user.Id);

                // prepopulat roles for the view dropdown
                var list = _db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
            }

            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            var Users = _db.Users.OrderBy(u => u.UserName).ToList().Select(uu => new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
            ViewBag.Users = Users;
            //var account = new AccountController();
            AppUser user = _db.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (this.UserManager.IsInRole(user.Id, RoleName))
            {
                this.UserManager.RemoveFromRole(user.Id, RoleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            // prepopulat roles for the view dropdown
            var list = _db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }


    }
}