using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="{0} required")]
        [StringLength(60,MinimumLength = 3, ErrorMessage ="{0} deve conter no minimo {2}, e no maximo {1}")]
        public string Name { get; set; }

        // Customizando Email
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="{0} Email Adress not valid.")]
        public string Email { get; set; }

        // Customizando Nomes na tela
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "{0} required")]
        public DateTime BirthDate { get; set; }


        // Customizando Nomes na tela
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        [Required(ErrorMessage = "{0} required")]
        [Range(1000,9999,ErrorMessage ="{0} value min {1}, value max {2}")]
        public double BaseSalary { get; set; }

        public Department Departament { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

         
        public Seller ()
        {

        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department departament)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Departament = departament;
        }

        public void AddSales (SalesRecord sr)
        {
            Sales.Add(sr);
        }
        public void RmoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }
        public  Double TotalSales  (DateTime Init, DateTime Final)
        {
            return Sales.Where(sr => sr.Date >= Init && sr.Date <= Final).Sum(sr => sr.Amount);
        }
    }
}
