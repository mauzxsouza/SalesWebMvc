using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{    
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        public DepartmentService( SalesWebMvcContext context)
        {
            _context = context;
        }
        
        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }

        //Método que buscas todo derpartamento de forma Síncrona e lenta, pois aguarda o termino
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy( x => x.Name).ToList();
        }
    }
}
