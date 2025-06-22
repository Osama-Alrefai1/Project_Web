using Project_Web.Data;
using Project_Web.Models;
using Project_Web.Repository.Base;


namespace Project_Web.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(AppDbContext context) 
        { 
            _context = context;
            categories = new MainRepository<Category>(_context);
            items = new MainRepository<Item>(_context);         
            employees =new  EmpRepo(_context);


        }

        private readonly AppDbContext _context;

        public IRepository<Category> categories { get; private set; }

        public IRepository<Item> items { get; private set; }

        public IEmpRepo employees { get; private set; }

        public int CommitChanges()
        {
           return _context.SaveChanges();
        }

        public void Dispose()
        {
           _context.Dispose();
        }
    }
}
