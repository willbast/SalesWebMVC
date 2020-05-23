using SalesWebMvc.Models;
using SalesWebMvc.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Data
{
    public class SeedingService
    {
        private SalesWebMvcContext _context { get; set; }

        public SeedingService(SalesWebMvcContext context)
        {
            _context = context;
        }
        public void Seed()
        {

            if (_context.Department.Any() ||
                _context.Seller.Any() ||
                _context.SalesRecord.Any())
            {
                return;
                // banco de dados ja populado não faço inserção.
            }

            Department D1 = new Department { Id = 1, Name = "Eletronics" };
            Department D2 = new Department { Id = 2, Name = "Fashion" };
            Department D3 = new Department { Id = 3, Name = "Foods" };
            Department D4 = new Department { Id = 4, Name = "Others" };

            Seller S1 = new Seller(1, "Bob", "Boob@email", new DateTime(1989, 5, 21), 55230, D1);
            Seller S2 = new Seller(2, "Jose", "Jose@email", new DateTime(1959, 5, 21), 21345, D2);
            Seller S3 = new Seller(3, "Felipe", "Felipe@email", new DateTime(1987, 5, 21), 75685, D3);
            Seller S4 = new Seller(4, "Laura", "Laura@email", new DateTime(1999, 5, 21), 35241, D4);
            Seller S5 = new Seller(5, "Carol", "Carol@email", new DateTime(2020, 5, 21), 42563, D1);

            SalesRecord Sr1 = new SalesRecord(1, new DateTime(2020, 9, 25), 11000, SalesStatus.BILLED, S1);
            SalesRecord Sr2 = new SalesRecord(2, new DateTime(2020, 9, 25), 200, SalesStatus.PENDING, S2);
            SalesRecord Sr3 = new SalesRecord(3, new DateTime(2020, 9, 25), 5000, SalesStatus.CANCELED, S3);
            SalesRecord Sr4 = new SalesRecord(4, new DateTime(2020, 9, 25), 35000, SalesStatus.BILLED, S4);

            _context.Department.AddRange(D1, D2, D3, D4);
            _context.Seller.AddRange(S1, S2, S3, S4, S5);
            _context.SalesRecord.AddRange(Sr1, Sr2, Sr3, Sr4);
            _context.SaveChanges();

        }
    }
}
