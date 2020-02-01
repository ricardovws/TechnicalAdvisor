using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalAdvisor.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Dealer Dealer { get; set; }

        public User()
        {
        }

        public User(int id, string name, string email, string password, Dealer dealer)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Dealer = dealer;
        }
    }
}
