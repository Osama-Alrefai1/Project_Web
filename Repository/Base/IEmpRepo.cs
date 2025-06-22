using Project_Web.Models;

namespace Project_Web.Repository.Base
{
    public interface IEmpRepo:IRepository<Employee>
    {
        void setPayRoll(Employee employee);

        decimal getSalary(Employee employee);
    }
}
