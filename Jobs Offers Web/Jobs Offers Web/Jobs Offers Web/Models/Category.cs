using System;
using System.Collections.Generic;
/* الخاص بـ تحديد العنصر مهم ام لا */
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Job_Offers_Website.Models
{
    public class Category
    {

        /* معرف الصنف */
        public int ID { get; set; }

        /* متغير من نوع نص يمثيل اسم النوع وظيفة عمومية / خصوصيه / خارج البلاد */
        [Required]
        [Display(Name = "نوع الوظيفة")]
        public string CategoryName { get; set; }

        /* حقل من النوع النصي يحتوي علي وصف لهذا النوع */
        [Required]
        [Display(Name = "وصف النوع")]
        public string CategoryDescription { get; set; }

        ///* بداية كود الربط التاني */

        public virtual ICollection<Job> Jobs { get; set; }

        /* نهاية كود الربط التاني */

        /* virtual تحل مشكلة تزامن البيانات */
    }

}
