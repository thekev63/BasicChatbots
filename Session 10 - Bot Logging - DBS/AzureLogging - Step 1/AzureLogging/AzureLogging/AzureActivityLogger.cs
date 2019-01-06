using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Threading.Tasks;
// for logging
using Microsoft.Bot.Builder.History;
using Microsoft.Bot.Connector;
using System.Data.SqlClient;

// disable warnings for now
#pragma warning disable 1998

namespace SQLLogging
{
    public class SQLActivityLogger : IActivityLogger
    {
        SqlConnection connection;

        public SQLActivityLogger(SqlConnection conn)
        {
            this.connection = conn;
        }

        public async Task LogAsync(IActivity activity)
        {
            string fromId = activity.From.Id;
            string toId = activity.Recipient.Id;
            string message = activity.AsMessageActivity().Text;

            string insertQuery = "INSERT INTO userChatLog(fromId, toId, message) VALUES (@fromId,@toId,@message)";
            // databases can be tricky beasts so use a try-catch block
            try
            {
                // Passing the fromId, toId, message to the the user chatlog table 
                using (SqlCommand command = new SqlCommand(insertQuery, this.connection))
                {
                    command.Parameters.AddWithValue("@fromId", fromId);
                    command.Parameters.AddWithValue("@toId", toId);
                    command.Parameters.AddWithValue("@message", message);

                    // Insert to SQL Server database
                    command.ExecuteNonQuery();
                    Debug.WriteLine("Insertion successful of message: " + activity.AsMessageActivity().Text);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
        }
    }
}