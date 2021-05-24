using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesWebMvc.Models;
using SalesWebMvc.Data;
using SalesWebMvc.Services.Exceptions;
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

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public void Insert(Seller obj)
        {
            //obj.Department = _context.Department.First();
            _context.Add(obj);
            _context.SaveChanges();
        }

        public async Task InsertAsync(Seller obj)
        {
            //obj.Department = _context.Department.First();
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public Seller FindById(int id)
        {
            //Traz apenas o ID do departamento
            //return _context.Seller.FirstOrDefault(obj => obj.Id == id);
            //Traz o Id e o restante dos dados do departamento
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            //Traz apenas o ID do departamento
            //return _context.Seller.FirstOrDefault(obj => obj.Id == id);
            //Traz o Id e o restante dos dados do departamento
            return await _context.Seller.Include(obj => obj.Department).
                                         FirstOrDefaultAsync(obj => obj.Id == id);
        } 

        public void remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public async Task removeAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new IntegrityException(ex.Message + " Cannot Delete by the seller has sales.");
            }
        }
        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(s => s.Id == obj.Id))
            {
                throw new NotFoundException("Seller´s Id not Found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }

        public async Task UpdateSync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(s => s.Id == obj.Id);
            if (!hasAny)
            {                
                throw new NotFoundException("Seller´s Id not Found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}
