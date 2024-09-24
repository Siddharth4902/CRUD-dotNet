using CRUD_PG.Models;
namespace CRUD_PG.DAO
{
    public interface IEmployeeDAO
    {
        Task<List<Employee>> GetEmployee();
        Task<int> InsertEmployee(Employee e);
        Task<int> UpdateEmployee(int Id, string Name, string DOB, string R_add, string P_add, string Contact, string Email, string M_Status, string Gender, string Occupation, string Aadhaar, string Pan);
        
        Task<int> DeleteEmployee(int Id);
    }
}
