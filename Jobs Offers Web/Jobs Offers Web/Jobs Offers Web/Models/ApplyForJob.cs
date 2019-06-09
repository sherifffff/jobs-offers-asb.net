using Job_Offers_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Jobs_Offers_Web.Models
{
    /* هذا الكلاس هو حاصل علاقة 
     * many to many 
     * بين جدول الوظائف 
     * Job
     * وجدول المستخدمين
     * AspNetUsers
     * المستخدم يستطيع ان يتقدم الي وظيفة أو أكثر
     * والوظيفة يمكن أن يتقدم إليها مستخدم أو أكثر 
     * بالتالي عندنا علاقة متعدددده
     * many to many
     * فحصل عندنا جدول ثالث يربط بين الجدولين الي فوق اسمه
     * ApplyForJob
     * هذا الجدول يحتوي علي معرف الوظيفة ومعرف المتسخدم
     * والمعطيات المتعلقة بـ عملية التقدم تاريخ التقديم والرسالة التي يرسلها 
     * المستخدم إلي المتقدم*/

    public class ApplyForJob
    {

        /* */
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime ApplyDate { get; set; }

        /* معرف الوظيفة */ 
        public int JobId { get; set; }

        /* معرف المستخدم */
        public string UserId { get; set; }

        /* */

        public virtual Job job { get; set; }
        public virtual ApplicationUser User { get; set; }

       
    }
}