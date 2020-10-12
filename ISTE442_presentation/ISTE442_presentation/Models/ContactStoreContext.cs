using MySql.Data.MySqlClient;
using Renci.SshNet.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace ISTE442_presentation.Models
{
    public class ContactStoreContext
    {
        public string connectionString { get; set; }

        public ContactStoreContext(string _connectionString)
        {
            connectionString = _connectionString;
        }

        //connection

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public List<Contact> GetContacts()
        {
            var contacts = new List<Contact>();

            using(MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "select * from Contact";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                contacts = readAll(cmd.ExecuteReader());

            }

            return contacts;
        }

        public List<Contact> readAll(DbDataReader reader)
        {
            var contacts = new List<Contact>();

            using (reader)
            {
                while (reader.Read())
                {
                    contacts.Add(new Contact { 
                    contactId = Convert.ToInt32(reader["contactid"]),
                    lastName = reader["lastName"].ToString(),
                    firstName = reader["firstName"].ToString(),
                    email = reader["email"].ToString(),
                    phone_num = reader["phone_num"].ToString(),
                    });
                }
            }

            return contacts;

        }
        public bool deleContact(int contactId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "delete from contact where contactId=@contactId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                BindId(cmd, contactId);

                return Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
        }
        public void BindId(MySqlCommand cmd, int contactId)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@contactId",
                DbType = DbType.Int32,
                Value = contactId
            }) ;
        }
        public long addContact(Contact contact)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "insert into contact(lastName, firstName, email, phone_num) values (@lastname, @firstname, @email, @phone_num)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                BindParms(cmd, "@lastname", contact.lastName);
                BindParms(cmd, "@firstname", contact.firstName);
                BindParms(cmd, "@email", contact.email);
                BindParms(cmd, "@phone_num", contact.phone_num);

                cmd.ExecuteNonQuery();

                return cmd.LastInsertedId;

            }
        }

        public void BindParms(MySqlCommand cmd, string bindString, string value)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = bindString,
                DbType = DbType.String,
                Value = value
            });
        }

        public Contact getContact(int contactId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string sql = "select * from contact where contactId=@contactId";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                BindId(cmd, contactId);

                var contacts = readAll(cmd.ExecuteReader());
                return contacts[0];
            }
        }
        public int updateContact(int contactId, Contact contact)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "update contact set lastname=@lastName, firstname=@firstName, email=@email, phone_num=@phone_num where contactId=@contactId";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                BindParms(cmd, "@lastName", contact.lastName);
                BindParms(cmd, "@firstName", contact.firstName);
                BindParms(cmd, "@email", contact.email);
                BindParms(cmd, "@phone_num", contact.phone_num);
                BindId(cmd, contactId);


                return cmd.ExecuteNonQuery();


            }
        }
    }
}
