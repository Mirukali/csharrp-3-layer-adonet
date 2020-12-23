using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAO
{
    public class UserDAO
    {
        private static UserDAO instance;

        public static UserDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new UserDAO();
                return instance;
            }
        }

        private UserDAO() { }
        public List<User> Xem()
        {
            List<User> users = new List<User>();

            string query = "select * from users";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                string id = item["ID"].ToString();
                string name = item["name"].ToString();
                DateTime? dob = item["DateOfBirth"].ToString() == string.Empty ? null : (DateTime?)item["DateOfBirth"];
                string info = item["Info"].ToString();
                bool? sex = item["Sex"].ToString() == string.Empty ? null : (bool?)item["Sex"];

                User newU = new User(id, name, dob, info, sex);

                users.Add(newU);
            }

            return users;
        }

        public bool Sua(string id, User user)
        {

            string query = "Update users set name = @name , dateofbirth = @dateofbirth , info = @info , sex = @sex where id = @oldid ";

            object[] para = new object[] {user.Name, user.DateOfBirth, user.Info, user.Sex, id };

            if (DataProvider.Instance.ExecuteNonQuery(query, para) > 0)
                return true;

            return false;
        }
    }
}
