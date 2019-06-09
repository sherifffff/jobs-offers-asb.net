using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Job_Offers_Website.Models
{
    public class Job
    {
        /* معرف الوظيفة */
        public int ID { get; set; }

        /* عنوان الوظيفة */
        [DisplayName("أسم الوظيفة")]
        public string JobTitle { get; set; }

        /* وصف الوظيفه */
        [DisplayName("وصف الوظيفة")]
        public string JobContent { get; set; }

        /* صورة الوظيفة */
        [DisplayName("صورة الوظيفة")]
        public string JobImage { get; set; }

        /*بداية كود الربط */

        /* ربط جدول الأصناف بـ جدول الوظائف */
        [DisplayName("نوع الوظيفة")]
        public int CategoryID { get; set; }

        public string UserID { get; set; }

        /* نقم بإنشاء موضوع ( ابوجكت ) من نوع الكلاس المربط به */
        public virtual Category Category { get; set; }

        public virtual ApplicationUser User { get; set; }

        /*نهاية كود الربط */

        /* virtual تحل مشكلة تزامن البيانات */

    }
}