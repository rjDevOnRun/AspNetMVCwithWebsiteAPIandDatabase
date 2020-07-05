using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WebService.Models;

namespace WebService.Repositories
{
    public class ContactRepository
    {

        public static bool AddContactToDB(Contact contact)
        {

            // Get Connection String to DB
            var connectionString = ConfigurationManager.ConnectionStrings["ContactManagerDB"].ToString();

            // Build an Insert Query (with parameters)
            var sqlQuery = $"INSERT INTO ContactList (Name, PhoneNumber, Note) VALUES (@Name, @PhoneNumber, @Note)";

            // Init
            SqlConnection sqlConnection = null;

            try
            {
                // Establish connection (will dispose automatically)
                using (sqlConnection = new SqlConnection(connectionString))
                {
                    // Open connection
                    sqlConnection.Open();

                    // Init Command
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    
                    // Pass Paramenters to Query
                    sqlCommand.Parameters.AddWithValue("@Name", contact.Name);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
                    sqlCommand.Parameters.AddWithValue("@Note", contact.Note);

                    // Execute the Query
                    sqlCommand.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message + ex.StackTrace);
                return false;
                //throw ex;
            }

        }

        internal static DataTable GetContactsFromDatabase()
        {
            // Get Connection String to DB
            var connectionString = ConfigurationManager.ConnectionStrings["ContactManagerDB"].ToString();

            // Build an Insert Query (with parameters)
            var sqlQuery = $"SELECT * FROM ContactList";

            // Init
            SqlConnection sqlConnection = null;
            DataTable dt = new DataTable();
            try
            {
                // Establish connection (will dispose automatically)
                using (sqlConnection = new SqlConnection(connectionString))
                {
                    // Open connection
                    sqlConnection.Open();

                    // Init Command
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);

                    // Execute the Query
                    IDataReader dr = sqlCommand.ExecuteReader();

                    
                    dt.Load(dr, LoadOption.OverwriteChanges);
                    dr.Close();

                }
                return dt;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message + ex.StackTrace);
                return dt;
            }
        }

        
    }
}