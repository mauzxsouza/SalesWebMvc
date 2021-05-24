using System.Linq;
using System.Collections.Generic;
using SalesWebMvc.Models;
using SalesWebMvc.Data;
using Microsoft.EntityFrameworkCore;
namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            //obj.Department = _context.Department.First();
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            //Traz apenas o ID do departamento
            //return _context.Seller.FirstOrDefault(obj => obj.Id == id);
            //Traz o Id e o restante dos dados do departamento
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
    }
}
