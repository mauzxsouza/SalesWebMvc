using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
                                                                                       /*{1} - primeiro parametro*/
        [Required(ErrorMessage ="{0} required")]         /*{0} pega o nome do atributo   {2} - segundo parametro*/
        [StringLength(60, MinimumLength = 3,ErrorMessage ="{0} size should be between {2} and {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage ="Enter a valid email adress!")]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name="Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 50000.0, ErrorMessage ="Salary must be from {1} to {2}")]
        [Display(Name= "Base Salary")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double BaseSalary { get; set; }

        public Department Department { get; set; }

        public int DepartmentId { get; set; }

        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }
        public Seller(int id, string name, string eMail, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            EMail = eMail;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            this.Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime dtInicial, DateTime dtFinal)
        {
            return Sales.Where(sr => sr.Date >= dtInicial && sr.Date <= dtFinal).Sum(sr => sr.Amount);
        }

    }
}
