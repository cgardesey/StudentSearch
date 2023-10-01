using StudentSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace StudentSearch.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        StudEntities db = new StudEntities();
        CollegeStuDetailsEntities1 _db = new CollegeStuDetailsEntities1();
        public ActionResult Index(StudentSearchViewModels model, int? page)
        {

            var acadyears = (from a in db.VIEWALLCADYEARS orderby a.ACADYEAR descending select new { acadyear = a.ACADYEAR, acadyeartext = a.ACADYEAR - 1 + "/" + a.ACADYEAR }).ToList();
            model.AcadyearList = new SelectList(acadyears, "acadyear", "acadyeartext");

            model.SemList = new List<SelectListItem>(){
            new SelectListItem{Value="1", Text="1"},
            new SelectListItem{Value="2", Text="2"},   
            new SelectListItem{Value="3", Text="3"}, 
            };

            if (User.IsInRole("Administrator"))
            {
                var collegeassigned = (from cr in _db.COLLEGEROLE where cr.ROLENAME != "Administrator" && cr.ROLENAME != "Health Staff" select new { rolename = cr.ROLENAME, roleid = cr.COLLEGEROLEID }).ToList();
                model.CollegeList = new SelectList(collegeassigned, "roleid", "rolename");
            }
            else
            {
                var collegeassigned = (from cr in _db.COLLEGEROLE join cur in _db.COLLEGEUSERROLE on cr.COLLEGEROLEID equals cur.COLLEGEROLEID join cu in _db.COLLEGEUSER on cur.COLLEGEUSERID equals cu.COLLEGEUSERID where cu.USERNAME == User.Identity.Name && cr.ROLENAME != "Health Staff" select new { rolename = cr.ROLENAME, roleid = cr.COLLEGEROLEID }).ToList();
                model.CollegeList = new SelectList(collegeassigned, "roleid", "rolename");
            }





            Session["acadyear"] = model.Year;
            Session["sem"] = model.Sem;
            if (!User.IsInRole("Health Staff"))
            {
                Session["College"] = model.College;
            }


            var Somelist = (from l in db.procStudentInfoDetails(model.College, model.Year, model.Sem)
                            select new StudentSearchViewModels
                            {
                                STUDENTID = l.STUDENTID,
                                FULLNAME = l.FULLNAME,
                                INDEXNO = l.INDEXNO,
                                NAME = l.NAME,
                                PRIMARYMOBILE = l.PRIMARYMOBILE,
                                YR = l.YR,
                                STUDENTPROGRAMME = l.NAME

                            }).ToList();



           int result;
                if (int.TryParse(model.searchStudent, out result))
                {
                     Somelist = Somelist.Where(x => x.STUDENTID == model.searchStudent.Trim()).ToList();
                }
                else
                {
                     if (model.searchStudent == null)
                        {
                            model.searchStudent = "";
                        }


                    string[] mySearch = model.searchStudent.Split(' ');
                    foreach (string word in mySearch)
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            Somelist = Somelist.Where(x => x.FULLNAME.ToLower().Contains(word.ToLower()) || x.INDEXNO == model.searchStudent.Trim()).ToList();
                        }

                    }   
                }
         
            model.SomeList = Somelist.Select(s => new StudentSearchViewModels
                                  {
                                      STUDENTID = s.STUDENTID,
                                      FULLNAME = s.FULLNAME,
                                      INDEXNO = s.INDEXNO,
                                      NAME = s.NAME,
                                      PRIMARYMOBILE = s.PRIMARYMOBILE,
                                      YR = s.YR,
                                      STUDENTPROGRAMME = s.NAME
                                  }).ToPagedList(page ?? 1, 10); 

           

            return View(model);

        }


        //public ActionResult StudentDetails(StudentSearchViewModels model, int? page)
        //{


        //}


        public ActionResult Details(string studentid, StudentSearchViewModels model)
        {
            int Year;
            int Sem;
            int? College;

            if (!User.IsInRole("Health Staff"))
            {
                Year = Convert.ToInt32(Session["acadyear"]);
                Sem = Convert.ToInt32(Session["sem"]);
                College = Convert.ToInt32(Session["College"]);
            }

            else
            {

                Year = Convert.ToInt32(Session["acadyear"]);
                Sem = Convert.ToInt32(Session["sem"]);
                College = null;
            }
            var StudentDetails = (from l in db.procStudentInfoDetails(College, Year, Sem)
                                  where (l.STUDENTID == studentid)
                                  select new
                                  {
                                      STUDENTID = l.STUDENTID,
                                      FULLNAME = l.FULLNAME,
                                      INDEXNO = l.INDEXNO,
                                      NAME = l.NAME,
                                      YR = l.YR,
                                      PRIMARYMOBILE = l.PRIMARYMOBILE,
                                      SCHOOLMOBILE = l.SCHOOLMOBILE,
                                      EMAIL = l.EMAIL,
                                      POSTADD1 = l.POSTADD1,
                                      POSTADD2 = l.POSTADD2,
                                      POSTADD3 = l.POSTADD3,
                                      POSTADD4 = l.POSTADD4,
                                      GUARDIANNAME = l.GUARDIANNAME,
                                      GUARDIANADDRESS = l.GUARDIANADDRESS,
                                      GUARDIANEMAIL = l.GUARDIANEMAIL,
                                      GUARDIANPHONE = l.GUARDIANPHONE,
                                      //PICTURE = l.PICTURE,

                                  }).FirstOrDefault();

            StudentSearchViewModels Student = new StudentSearchViewModels()
            {

                STUDENTID = StudentDetails.STUDENTID,
                FULLNAME = StudentDetails.FULLNAME,
                INDEXNO = StudentDetails.INDEXNO,
                NAME = StudentDetails.NAME,
                YR = StudentDetails.YR,
                PRIMARYMOBILE = StudentDetails.PRIMARYMOBILE,
                SCHOOLMOBILE = StudentDetails.SCHOOLMOBILE,
                EMAIL = StudentDetails.EMAIL,
                POSTADD1 = StudentDetails.POSTADD1,
                POSTADD2 = StudentDetails.POSTADD2,
                POSTADD3 = StudentDetails.POSTADD3,
                POSTADD4 = StudentDetails.POSTADD4,
                GUARDIANNAME = StudentDetails.GUARDIANNAME,
                GUARDIANADDRESS = StudentDetails.GUARDIANADDRESS,
                GUARDIANEMAIL = StudentDetails.GUARDIANEMAIL,
                GUARDIANPHONE = StudentDetails.GUARDIANPHONE,
                Year = Year,
                College = College,
                Sem = Sem
                //PICTURE = StudentDetails.PICTURE
            };









            return View(Student);
        }


    }
}