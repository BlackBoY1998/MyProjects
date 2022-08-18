using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CrudincoreusingJquey.Models
{
    public class TranascationModel
    {
        [Key]
        public int TransactionId { get; set; }


        [Column(TypeName ="nvarchar(12)")]
        [DisplayName("Account Number")]
        [Required(ErrorMessage = "Please Enter Account Number")]
        [MaxLength(12,ErrorMessage ="Maximum 12 Characters only")]
        public string AccountNumber { get; set; }


        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Beneficiary Name")]
        [Required(ErrorMessage = "Please Enter Beneficiary Name")]
        //[MaxLength(12, ErrorMessage = "Maximum 12 Characters only")]
        public string BeneficiaryName { get; set; }


        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Bank Name")]
        [Required(ErrorMessage = "Please Enter Bank Name")]
        public string  BankName { get; set; }


        [Column(TypeName = "nvarchar(11)")]
        [DisplayName("SWIFT Code")]
        [Required(ErrorMessage = "Please Enter SWIFT Code")]
        [MaxLength(11, ErrorMessage = "Maximum 11 Characters only")]
        public string SWIFTCode { get; set; }


        [DisplayName("Amount")]
        [Required(ErrorMessage = "Please Enter Amount ")]
        public int Amount { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }

    }
}
