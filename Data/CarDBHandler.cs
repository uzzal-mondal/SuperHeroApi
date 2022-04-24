using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SuperHeroApi.Data
{
    public class CarDBHandler
    {
        private SqlConnection _connection;
        private readonly IConfiguration _configuration;

        public CarDBHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private void connection()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");  
            _connection = new SqlConnection(connectionString);
        }
        
        public bool AddCar(CarModel model)
        {
            connection();
            SqlCommand sqlcmd = new SqlCommand("AddCar",_connection);
            sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;

            sqlcmd.Parameters.AddWithValue("@CarName", model.CarName);
            sqlcmd.Parameters.AddWithValue("CountryName", model.CountryName);
            sqlcmd.Parameters.AddWithValue("@CarPrice", model.CarPrice);

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

        public List<CarModel> GetCarList()
        {
            connection();
            List<CarModel> carList = new List<CarModel>();
            SqlCommand sqlCommand = new SqlCommand("GetCarDetails", _connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();

            _connection.Open();
            sd.Fill(dataTable);
            _connection.Close();

            foreach(DataRow dr in dataTable.Rows)
            {
                carList.Add(new CarModel
                {
                    Id = Convert.ToInt32(dr["ID"]),
                    CarName = Convert.ToString(dr["CarName"]),
                    CountryName = Convert.ToString(dr["CountryName"]),
                    CarPrice = Convert.ToString(dr["CarPrice"])

                });
            }
            return carList;
        }



        public bool UpdateDetails(CarModel carModel)
        {
            connection();
            SqlCommand sqlcmd = new SqlCommand("GetCarUpdate", _connection);
            sqlcmd.CommandType = CommandType.StoredProcedure;

            sqlcmd.Parameters.AddWithValue("@CarId", carModel.Id);
            sqlcmd.Parameters.AddWithValue("@CarName", carModel.CarName);
            sqlcmd.Parameters.AddWithValue("@CountryName", carModel.CountryName);
            sqlcmd.Parameters.AddWithValue("@CarPrice", carModel.CarPrice);

            _connection.Open();
            int i = sqlcmd.ExecuteNonQuery();
            _connection.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }




        public bool DeleteStudent(int id)
        {
            connection();
            SqlCommand sqlcmd = new SqlCommand("DeleteCar", _connection);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@CarId", id);
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
