using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineCourse.Entity.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        [Display(Name = "زمان آخرین تغییر")]
        public DateTime LastModifieDateTime { get; set; }

        [Display(Name = "نوع پرداخت")]
        public PayType PayType { get; set; }

        [Display(Name = "کد تراکنش")]
        public string TransactionId { set; get; }

        [Display(Name = "وضعیت پرداخت")]
        public PayState PayState { set; get; }

        [Display(Name = "وضعیت")]
        public GeneralState State { set; get; }

        [MaxLength(20)]
        [Display(Name = "Ip پرداخت کننده")]
        public string Ip { set; get; }

        [MaxLength(50)]
        public string Code { set; get; }
        
        [Display(Name = "بانک")]
        public BankId BankId { set; get; }

        public ICollection<Payment> Payments { set; get; }

    }
}
