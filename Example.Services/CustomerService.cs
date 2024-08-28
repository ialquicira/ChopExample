using Example.Data;
using Example.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;

namespace Example.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly ISqlClientConnectionBD _sqlClientConnectionBD;
		public CustomerService(ISqlClientConnectionBD sqlClientConnectionBD)
		{
			_sqlClientConnectionBD = sqlClientConnectionBD;
		}

		public void CreateCustomer(Customer domain)
		{
			using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
			{
				try
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = @"INSERT INTO Customers 
                                                (Name
                                                ,Address
                                                ,Age
                                                ,Email
                                                ,Phone
                                                ,CustomerId)
                                                VALUES
                                                (@Name
                                                ,@Address
                                                ,@Age
                                                ,@Email
                                                ,@Phone
                                                ,@CustomerId)";



						command.Parameters.AddWithValue("@Name", domain.Name);
						command.Parameters.AddWithValue("@Address", domain.Address);
						command.Parameters.AddWithValue("@Phone", domain.Phone);
						command.Parameters.AddWithValue("@Email", domain.Email);
						command.Parameters.AddWithValue("@Age", domain.Age);
						command.Parameters.AddWithValue("@CustomerId", Guid.NewGuid());

						connection.Open();
						command.ExecuteNonQuery();
						connection.Close();
					}
				}
				catch (SqlException ex)
				{
					//Guardar la excepcion en algun log de errores
					//ex
				}
				finally
				{
					connection.Close();
				}
			}
		}

		public void DeleteCustomer(Guid CustomerId)
		{
			string strQuery = @"DELETE FROM Customers
                                WHERE CustomerId = '{0}'";
			strQuery = string.Format(strQuery, CustomerId);
			using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
			{
				try
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = strQuery;


						connection.Open();
						command.ExecuteNonQuery();
						connection.Close();
					}
				}
				catch (SqlException ex)
				{
					//Guardar la excepcion en algun log de errores
					//ex
				}
				finally
				{
					connection.Close();
				}
			}
		}

		public IList<Customer> GetAllCustomers()
		{
			List<Customer> modelList = new List<Customer>();
			string strQuery = @"SELECT
                                *
                                FROM Customers";
			using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
				try
				{
					connection.Open();
					SqlCommand command = new SqlCommand(strQuery, connection);
					command.CommandType = CommandType.Text;
					using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
					{
						while (reader.Read())
						{
							Customer customer = new Customer();
							customer.CustomerId = Guid.Parse(reader["CustomerId"].ToString());
							customer.Name = reader["Name"].ToString();
							customer.Address = reader["Address"].ToString();
							customer.Phone = reader["Phone"].ToString();
							customer.Email = reader["Email"].ToString();
							customer.Age = Convert.ToInt32(reader["Age"].ToString());
							modelList.Add(customer);
						}
					}
				}
				catch (SqlException ex)
				{
					//Guardar la excepcion en algun log de errores
					//ex
				}
				finally
				{
					connection.Close();
				}
			return modelList;
		}

		public Customer GetCustomerById(Guid CustomerId)
		{
			List<Customer> modelList = new List<Customer>();
			string strQuery = @"SELECT
                                *
                                FROM Customers
                               WHERE CustomerId = '{0}'";
			strQuery = string.Format(strQuery, CustomerId);
			using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
				try
				{
					connection.Open();
					SqlCommand command = new SqlCommand(strQuery, connection);
					command.CommandType = CommandType.Text;
					using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
					{
						while (reader.Read())
						{
							Customer customer = new Customer();
							customer.CustomerId = Guid.Parse(reader["CustomerId"].ToString());
							customer.Name = reader["Name"].ToString();
							customer.Address = reader["Address"].ToString();
							customer.Phone = reader["Phone"].ToString();
							customer.Email = reader["Email"].ToString();
							customer.Age = Convert.ToInt32(reader["Age"].ToString());
							modelList.Add(customer);
						}
					}
				}
				catch (SqlException ex)
				{
					//Guardar la excepcion en algun log de errores
					//ex
				}
				finally
				{
					connection.Close();
				}
			return modelList.Count() > 0 ? modelList.FirstOrDefault() : new Customer();
		}
		public Customer GetCustomerByFilter(string filter)
		{
			List<Customer> modelList = new List<Customer>();
			string strQuery = @"SELECT * FROM Customers
                                WHERE Name LIKE '%{0}%'
                                AND Email LIKE '%{1}%'";
			strQuery = string.Format(strQuery, filter, filter);
			using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
				try
				{
					connection.Open();
					SqlCommand command = new SqlCommand(strQuery, connection);
					command.CommandType = CommandType.Text;
					using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
					{
						while (reader.Read())
						{
							Customer customer = new Customer();
							customer.CustomerId = Guid.Parse(reader["CustomerId"].ToString());
							customer.Name = reader["Name"].ToString();
							customer.Address = reader["Address"].ToString();
							customer.Phone = reader["Phone"].ToString();
							customer.Email = reader["Email"].ToString();
							customer.Age = Convert.ToInt32(reader["Age"].ToString());
							modelList.Add(customer);
						}
					}
				}
				catch (SqlException ex)
				{
					//Guardar la excepcion en algun log de errores
					//ex
				}
				finally
				{
					connection.Close();
				}
			return modelList.Count() > 0 ? modelList.FirstOrDefault() : new Customer();
		}

		public void UpdateCustomer(Customer domain)
		{
			using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
			{
				try
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = @"UPDATE Customers
                                        SET 
                                        Name = COALESCE(@Name, Name),
                                        Address = COALESCE(@Address, Address),
                                        Phone = COALESCE(@Phone, Phone),
                                        Email = COALESCE(@Email, Email),
                                        Age = COALESCE(@Age, Age)
                                        WHERE CustomerId = @CustomerId";



						command.Parameters.AddWithValue("@Name", domain.Name);
						command.Parameters.AddWithValue("@Address", domain.Address);
						command.Parameters.AddWithValue("@Phone", domain.Phone);
						command.Parameters.AddWithValue("@Email", domain.Email);
						command.Parameters.AddWithValue("@Age", domain.Age);
						command.Parameters.AddWithValue("@CustomerId", domain.CustomerId);

						connection.Open();
						command.ExecuteNonQuery();
						connection.Close();
					}
				}
				catch (SqlException ex)
				{
					//Guardar la excepcion en algun log de errores
					//ex
				}
				finally
				{
					connection.Close();
				}
			}
		}
	}
}
