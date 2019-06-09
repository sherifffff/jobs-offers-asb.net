using Jobs_Offers_Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        /* كلاس يمثل قاعده البيانات */
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        /* الداله الخاصه بـ التفاصيل */
        public ActionResult Details (int JobId)
        {
            var job = db.Jobs.Find(JobId);

            if(job == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = JobId;
            return View(job);
        }

        /* ف وضع الجيت ترجع ليا الفيو العادي الفورم
         *  يحتوي علي رسالة التي يرسلها الشخص المتقدم علي وظيفة */ 
         //GET\\
         [Authorize] //لـ عدم عرض هذه ل غير المشتركين ف الموقع
        public ActionResult Apply()
        {
            return View();
        }

        /* تحدد العمليات التي نريد ان تتنفذ عند الضغط علي تقدم ع الوظيفة */
        /* تعمل ف وضع 
         * HPPT POST */
         [HttpPost]
        public ActionResult Apply(string Message)
        {
            var UserId = User.Identity.GetUserId();
            var JobId = (int)Session["JobId"];

            /* إذهب إلي جدول تقديم إلي وظيفة وابحث عن اي عنصر المعرف بتاعه يساوي 
             * معرف الموجود عندي حاليا ومعرف المستخدم يساوي الموجود حاليا 
             * إذا وجد
             * نفذ الاتي */
            var check = db.ApplyForJobs.Where(a => a.JobId == JobId && a.UserId == UserId).ToList();

            /* إذا لا يوجد نفذ الاتي */
            if(check.Count < 1)
            {
                var job = new ApplyForJob();
                job.UserId = UserId;
                job.JobId = JobId;
                job.Message = Message;
                job.ApplyDate = DateTime.Now;

                db.ApplyForJobs.Add(job);
                db.SaveChanges();

                ViewBag.Result = "تمت الإضافة بـ نجاح";
            }
            else
            {
                ViewBag.Result = "المعذرة، لقد سبق وتقدمت إلي نفس الوظيفة";
            }
            return View();
        }

        /* لـ عرض الوظائف حسب المستخدم */
        [Authorize]
         public ActionResult GetJobsByUser()
        {
            /* الحصول ع معرف المستخدم ثم بنبحث علي الوظائف المندرجه تحته */ 
            var UserID = User.Identity.GetUserId();
            var Jobs = db.ApplyForJobs.Where(a => a.UserId == UserID);
            return View(Jobs.ToList());
        }

        /* عرض تفاصيل الوظيفة المتقدم لبها */ 
        [Authorize] //>>> لمنع الظهور لغير المشتركين ف الموقع
      public ActionResult DetailsOfJob(int Id)
        {
            var job = db.ApplyForJobs.Find(Id);

            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        /* */

        /* ============= بداية كود التعديل ل الوظائف المتقدم لها من قبل المستخدم الحالي ============== */

        // GET: Roles/Edit/5
        public ActionResult Edit(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View(job);

        }

        /* ============= نهاية كود التعديل ل الوظائف المتقدم لها من قبل المستخدم الحالي ============== */

        /* نهاية كود الحذف الخاص ب الوظائف المقدم عليها من قبل المستخدم الحالي */

        // GET: Roles/Delete/5
        public ActionResult Delete(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(ApplyForJob job)
        {
            // TODO: Add delete logic here
            var myjob = db.ApplyForJobs.Find(job.Id);
            db.ApplyForJobs.Remove(myjob);
            db.SaveChanges();
            return RedirectToAction("GetJobsByUser");

        }
        /* نهاية كود الحذف الخاص ب الوظائف المقدم عليها من قبل المستخدم الحالي */

        /* بداية كود جلب الوظائف حسب الناشر لها ومعرفة من المتقدمين الي هذه الوظيفة  */
        [Authorize]
        public ActionResult GetJobsByPublisher()
        {
            var UserID = User.Identity.GetUserId();
            /* لـ تحزين كل الوظائف المسجله ف جدول التقديمات ل متغير الابلكيشن */
            /* عملنا ربط بين جدول المتقدمات والوظائف 
             * جلبنا كل الوظائف ب بياناتها جميعا */
            var Jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobId equals job.ID
                       where job.User.Id == UserID
                       select app;
            return View(Jobs.ToList());

        }

        /* نهاية كود جلب الوظائف حسب الناشر لها ومعرفة من المتقدمين الي هذه الوظيفة  */
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();

        }

        public ActionResult search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult search(string searchname)
        {
            var result = db.Jobs.Where(a => a.JobTitle.Contains(searchname)
             || a.JobContent.Contains(searchname)
             || a.Category.CategoryName.Contains(searchname)
             || a.Category.CategoryDescription.Contains(searchname)).ToList();
            return View();
        }
    }

}