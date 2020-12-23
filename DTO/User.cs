using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class User
    {
        private string id;
        private string name;
        private string info;
        private DateTime? dateOfBirth;
        private bool? sex;
        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Info { get => info; set => info = value; }
        public DateTime? DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public bool? Sex { get => sex; set => sex = value; }

        public User(string id, string name, DateTime? dao, string info, bool? sex)
        {
            this.id = id;
            this.name = name;
            this.dateOfBirth = dao;
            this.info = info;
            this.sex = sex;
        }
    }
}
