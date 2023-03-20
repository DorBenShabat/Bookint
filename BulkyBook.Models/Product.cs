using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Range(1, 1000)]
        [DisplayName("List Price")]

        public double ListPrice { get; set; }

        [Required]
        [Range(1, 1000)]
        [DisplayName("Price for 1-50")]

        public double Price { get; set; }

        [Required]
        [Range(1, 1000)]
        [DisplayName("Price for 51-100")]

        public double PriceFor50 { get; set; }

        [Required]
        [Range(1, 1000)]
        [DisplayName("Price for 100+")]

        public double PriceFor100 { get; set; }

        [ValidateNever]
        public string URLImage { get; set; }

        [Required]
        [DisplayName("Category")]
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        [ValidateNever]
        public Category Category { get; set; }

        [Required]
        [DisplayName("Cover Type")]
        public int CoverTypeID { get; set; }

        [ForeignKey("CoverTypeID")]
        [ValidateNever]
        public CoverType CoverType { get; set; }
    }
}
