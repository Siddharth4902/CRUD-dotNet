using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text.Json.Serialization;

namespace CRUD_PG.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public string R_add { get; set; }
        public string P_add { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string M_Status { get; set; }
        public string Gender { get; set; }
        public string Occupation { get; set; }
        public string Aadhaar { get; set; }
        public string Pan { get; set; }

    }
}
