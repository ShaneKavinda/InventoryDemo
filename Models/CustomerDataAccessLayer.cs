    using System;
    using System.Collections.Generic;    
    using System.Data;    
    using System.Data.SqlClient;    
    using System.Linq;    
    using System.Threading.Tasks;
using MVCInventoryDemo.Models;

namespace MVCInventoryDemo{
    public class CustomerDataAccessLayer{
        private readonly string connectionString = "data source=SHANE_K_99\\SQLEXPRESS;initial catalog=Inventory;trusted_connection=true";
        // View all Customer Details
        public IEnumerable<Customer> GetAllCustomers(){
            List<Customer> lstCustomer = new List<Customer>();
            using (SqlConnection con = new SqlConnection(connectionString)){
                SqlCommand cmd = new SqlCommand("spGetAllCustomers", con);
                cmd.CommandType = CommandType.StoredProcedure;                        
                con.Open();                    
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read()){
                    Customer customer = new Customer();
                    customer.UserID = (Guid)rdr["UserID"];
                    customer.Username = (string)rdr["Username"];
                    customer.Email = (string)rdr["Email"];
                    customer.FirstName = (string)rdr["FirstName"];
                    customer.LastName = (string)rdr["LastName"];
                    customer.CreatedOn = (DateTime)rdr["CreatedOn"];
                    customer.IsActive = (bool)rdr["IsActive"];
                    lstCustomer.Add(customer);
                }
            }
            return lstCustomer;
        }


        private bool IsUserIdUnique(Guid userId, SqlConnection con){
            using (var command = new SqlCommand("SELECT COUNT(*) FROM Customer WHERE UserID = @UserId", con)){
                command.Parameters.AddWithValue("@UserId", userId);
                int count = (int)command.ExecuteScalar();

                // If the count is 0, the UserID is unique
                return count == 0;
            } 
        }
        // To Add a new Customer Record
        public void AddCustomer(Customer customer){
            using (SqlConnection con = new SqlConnection(connectionString)){
                SqlCommand cmd = new SqlCommand("spInsertCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                
                // Keep generating a new GUID until a unique one is found
                do
                {
                    customer.UserID = Guid.NewGuid();
                }
                while (!IsUserIdUnique(customer.UserID, con));

                cmd.Parameters.AddWithValue("@UserID", customer.UserID);
                cmd.Parameters.AddWithValue("@Username", customer.Username);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                cmd.Parameters.AddWithValue("@IsActive", customer.IsActive);

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Update the record of a Customer
         public void UpdateCustomer(Customer customer){                
            using (SqlConnection con = new SqlConnection(connectionString)){                    
                SqlCommand cmd = new SqlCommand("spUpdateCustomer", con);                    
                cmd.CommandType = CommandType.StoredProcedure;                        
                cmd.Parameters.AddWithValue("@Username", customer.Username);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@CreatedOn", customer.CreatedOn);
                cmd.Parameters.AddWithValue("@IsActive", customer.IsActive);
                cmd.Parameters.AddWithValue("@UserID", customer.UserID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();               
            }            
        }

        // Read record of one Customer
        public Customer GetCustomerData(Guid id){                
            Customer customer = new Customer(); 
            customer.UserID = id;                   
            using (SqlConnection con = new SqlConnection(connectionString)){                                       
                SqlCommand cmd = new SqlCommand("spGetCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;  
                // Add the @UserId parameter
                cmd.Parameters.AddWithValue("@UserId", id);                                                             
                con.Open();                    
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read()){
                    customer.UserID = (Guid)rdr["UserID"];
                    customer.Username = (string)rdr["Username"];
                    customer.Email = (string)rdr["Email"];
                    customer.FirstName = (string)rdr["FirstName"];
                    customer.LastName = (string)rdr["LastName"];
                    customer.CreatedOn = (DateTime)rdr["CreatedOn"];
                    customer.IsActive = (bool)rdr["IsActive"];
                }         
            }                
            return customer;            
        }

        // To Delete the Record of one Customer
        public void DeleteCustomer(Guid UserID){                    
            using (SqlConnection con = new SqlConnection(connectionString)){                    
                SqlCommand cmd = new SqlCommand("spDeleteCustomer", con);                    
                cmd.CommandType = CommandType.StoredProcedure;                        
                cmd.Parameters.AddWithValue("@UserID", UserID);                        
                con.Open();                    
                cmd.ExecuteNonQuery();                    
                con.Close();                
            }            
        }       
    }                                                
}