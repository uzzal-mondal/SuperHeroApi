


using Microsoft.Data.SqlClient;
using SuperHeroApi.model;
using System.Data;

namespace SuperHeroApi.Data
{
    public class EmpDbHandler
    {

        private SqlConnection _connection;
        private readonly IConfiguration _configuration;

        public EmpDbHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private void connection()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(connectionString);
        }

        public bool AddEmp(Employee model)
        {
            connection();
            SqlCommand sqlcmd = new SqlCommand("[AddNewEmployee]", _connection);
            sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;

            sqlcmd.Parameters.AddWithValue("@EmpName", model.employeeName);
            sqlcmd.Parameters.AddWithValue("@EmpEmail", model.employeeEmail);
            sqlcmd.Parameters.AddWithValue("@EmpPhn", model.employeePhone);
            sqlcmd.Parameters.AddWithValue("@EmpBloodGp", model.employeeBloodGroup);
            sqlcmd.Parameters.AddWithValue("@EmpDsg", model.employeeDesignation);
            sqlcmd.Parameters.AddWithValue("@EmpJoinDate", model.employeeJoiningDate);


            _connection.Open();
            int i = sqlcmd.ExecuteNonQuery();
            _connection.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<Employee> GetEmployeeList()
        {
            connection();
            List<Employee> employeesList = new List<Employee>();
            SqlCommand sqlCommand = new SqlCommand("GetEmployeeDetails", _connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();

            _connection.Open();
            sd.Fill(dataTable);
            _connection.Close();

            foreach (DataRow dr in dataTable.Rows)
            {
                employeesList.Add(new Employee
                {
                     Id = Convert.ToInt32(dr["EmpId"]),
                    employeeName = Convert.ToString(dr["EmpName"]),
                    employeeEmail = Convert.ToString(dr["EmpEmail"]),
                    employeePhone = Convert.ToString(dr["EmpPhn"]),
                    employeeBloodGroup = Convert.ToString(dr["EmpBloodGp"]),
                    employeeDesignation = Convert.ToString(dr["EmpDsg"]),
                    employeeJoiningDate = Convert.ToString(dr["EmpJoinDate"])

                });
            }
            return employeesList;
        }


        public bool UpdateDetails(Employee model)
        {
            connection();
            SqlCommand sqlcmd = new SqlCommand("UpdateEmployeeDetails", _connection);
            sqlcmd.CommandType = CommandType.StoredProcedure;

          /*  sqlcmd.Parameters.AddWithValue("@CarId", model.Id);
            sqlcmd.Parameters.AddWithValue("@CarName", model.CarName);
            sqlcmd.Parameters.AddWithValue("@CountryName", model.CountryName);
            sqlcmd.Parameters.AddWithValue("@CarPrice", model.CarPrice);*/

            sqlcmd.Parameters.AddWithValue("@EmpId", model.Id);
            sqlcmd.Parameters.AddWithValue("@EmpName", model.employeeName);
            sqlcmd.Parameters.AddWithValue("@EmpEmail", model.employeeEmail);
            sqlcmd.Parameters.AddWithValue("@EmpPhn", model.employeePhone);
            sqlcmd.Parameters.AddWithValue("@EmpBloodGp", model.employeeBloodGroup);
            sqlcmd.Parameters.AddWithValue("@EmpDsg", model.employeeDesignation);
            sqlcmd.Parameters.AddWithValue("@EmpJoinDate", model.employeeJoiningDate);

            _connection.Open();
            int i = sqlcmd.ExecuteNonQuery();
            _connection.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool DeleteEmployee(int id)
        {
            connection();
            SqlCommand sqlcmd = new SqlCommand("DeleteEmployee", _connection);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@EmpId", id);
            _connection.Open();
            int i = sqlcmd.ExecuteNonQuery(); // return num of row..
            _connection.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

    }
}
