using CRUD_PG.Models;
using Npgsql;
using System.Data;
using NpgsqlTypes;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Collections.Generic;
namespace CRUD_PG.DAO
{
    public class EmployeeDAOImplementation : IEmployeeDAO
    {
        private readonly NpgsqlConnection _connection;
        public EmployeeDAOImplementation(NpgsqlConnection connection)
        {
            _connection = connection;
        }
        public async Task<List<Employee>> GetEmployee()
        {
            string query = @" select * from employee.employees ";
            List<Employee> employees = new List<Employee>();
            Employee e = null;
            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand Command = new NpgsqlCommand(query, _connection);
                    Command.CommandType = CommandType.Text;
                    //Command.Parameters.AddWithValue("@pid", id);
                    NpgsqlDataReader reader = await Command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            e = new Employee();
                            e.Id = reader.GetInt32(0);
                            e.Name = reader.GetString(1);
                            e.DOB = reader.GetString(2);
                            e.R_add = reader.GetString(3);
                            e.P_add = reader.GetString(4);
                            e.Contact = reader.GetString(5);
                            e.Email = reader.GetString(6);
                            e.M_Status = reader.GetString(7);
                            e.Gender = reader.GetString(8);
                            e.Occupation = reader.GetString(9);
                            e.Aadhaar = reader.GetString(10);
                            e.Pan = reader.GetString(11);

                            employees.Add(e);

                        }

                    }
                    reader?.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("------Exception-----" + ex.Message);
            }
            return employees;

        }
        public async Task<int> InsertEmployee(Employee e)
        {
            int rowsInserted = 0;
            string message;
            string insertQuery = $"INSERT INTO employee.employees (Name, DOB, Residential_Address, Permanent_Address, Contact, Email, Marital_status, Gender, Occupation, Aadhaar, PAN_Number) VALUES('{e.Name}','{e.DOB}','{e.R_add}','{e.P_add}','{e.Contact}','{e.Email}','{e.M_Status}','{e.Gender}','{e.Occupation}','{e.Aadhaar}','{e.Pan}'  )";


            Console.WriteLine("Query" + insertQuery);
            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand Command = new NpgsqlCommand(insertQuery, _connection);
                    Command.CommandType = CommandType.Text;
                    rowsInserted = await Command.ExecuteNonQueryAsync();
                }
            }
            catch (NpgsqlException ex)
            {
                message = ex.Message;
                Console.WriteLine("------Exception-----" + message);
            }
            return rowsInserted;
        }

        public async Task<int> DeleteEmployee(int Id)
        {
            int rowAffected = 0;
            string delQuery = $"delete from employee.employees where Id =@pId"; ;
            Console.WriteLine("query" + delQuery);
            // conn.Open();
            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand deleteCommand = new NpgsqlCommand(delQuery, _connection);
                    deleteCommand.CommandType = CommandType.Text;
                    //updateCommand.Parameters.AddWithValue("@pid", id);
                    deleteCommand.Parameters.Add("@pId", NpgsqlDbType.Integer).Value = Id;
                    rowAffected = await deleteCommand.ExecuteNonQueryAsync();
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }


            return rowAffected;

        }
        public async Task<int> UpdateEmployee(int Id, string newName, string newDOB, string newR_add, string newP_add, string newContact, string newEmail, string newM_Status, string newGender, string newOccupation, string newAadhaar, string newPan)
        {
            int rowsAffected;
            string query = $"update employee.employees set Name = @NAME  ,dob =@DOB ,Residential_Address = @RADDRESS, Permanent_Address =@PADDRESS, Contact =@CONTACT , Email =@EMAIL, Marital_status =@MSTATUS, Gender =@GENDER, Occupation =@OCC, Aadhaar =@AADHAAR, PAN_Number = @PAN where Id = @pId;";
            using (_connection)
            {
                NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                await _connection.OpenAsync();
                command.CommandType = CommandType.Text;

                NpgsqlParameter nameParameter = new()
                {
                    ParameterName = "@NAME",
                    NpgsqlDbType = NpgsqlDbType.Varchar,
                    Direction = ParameterDirection.Input,
                    Value = newName
                };
                NpgsqlParameter dobParameter = new()
                {
                    ParameterName = "@DOB",
                    NpgsqlDbType = NpgsqlDbType.Varchar,
                    Direction = ParameterDirection.Input,
                    Value = newDOB
                }; 
                NpgsqlParameter raddParameter = new()
                {
                    ParameterName = "@RADDRESS",
                    NpgsqlDbType = NpgsqlDbType.Varchar,
                    Direction = ParameterDirection.Input,
                    Value = newR_add
                }; 
                NpgsqlParameter paddParameter = new()
                {
                    ParameterName = "@PADDRESS",
                    NpgsqlDbType = NpgsqlDbType.Varchar,
                    Direction = ParameterDirection.Input,
                    Value = newP_add
                };
                NpgsqlParameter contactParameter = new()
                {
                    ParameterName = "@CONTACT",
                    NpgsqlDbType = NpgsqlDbType.Varchar,
                    Direction = ParameterDirection.Input,
                    Value = newContact
                };
                NpgsqlParameter emailParameter = new()
                {
                    ParameterName = "@EMAIL",
                    NpgsqlDbType = NpgsqlDbType.Varchar,
                    Direction = ParameterDirection.Input,
                    Value = newEmail
                };
                NpgsqlParameter mstatusParameter = new()
                {
                    ParameterName = "@MSTATUS",
                    NpgsqlDbType = NpgsqlDbType.Varchar,
                    Direction = ParameterDirection.Input,
                    Value = newM_Status
                };
                NpgsqlParameter genderParameter = new()
                {
                    ParameterName = "@GENDER",
                    NpgsqlDbType = NpgsqlDbType.Varchar,
                    Direction = ParameterDirection.Input,
                    Value = newGender
                }; 
                NpgsqlParameter occParameter = new()
                {
                    ParameterName = "@OCC",
                    NpgsqlDbType = NpgsqlDbType.Varchar,
                    Direction = ParameterDirection.Input,
                    Value = newOccupation
                }; 
                NpgsqlParameter aadhaarParameter = new()
                {
                    ParameterName = "@AADHAAR",
                    NpgsqlDbType = NpgsqlDbType.Varchar,
                    Direction = ParameterDirection.Input,
                    Value = newAadhaar
                };
                NpgsqlParameter panParameter = new()
                {
                    ParameterName = "@PAN",
                    NpgsqlDbType = NpgsqlDbType.Varchar,
                    Direction = ParameterDirection.Input,
                    Value = newPan
                };
                NpgsqlParameter idParameter = new()
                {
                    ParameterName = "@pId",
                    NpgsqlDbType = NpgsqlDbType.Numeric,
                    Direction = ParameterDirection.Input,
                    Value = Id
                };
                command.Parameters.Add(nameParameter);
                command.Parameters.Add(idParameter);
                command.Parameters.Add(dobParameter);
                command.Parameters.Add(raddParameter);
                command.Parameters.Add(paddParameter);
                command.Parameters.Add(contactParameter);
                command.Parameters.Add(emailParameter);
                command.Parameters.Add(mstatusParameter);
                command.Parameters.Add(genderParameter);
                command.Parameters.Add(occParameter);
                command.Parameters.Add(aadhaarParameter);
                command.Parameters.Add(panParameter);


                rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected;
            }
        }
    }
}
