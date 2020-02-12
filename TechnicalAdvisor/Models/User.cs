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
        public int DealerID { get; set; }
        public Dealer Dealer { get; set; }

        public User()
        {
        }

        public User(string name, string email, Dealer dealer)
        {
            Name = name;
            Email = email;
            Dealer = dealer;
        }

        public User(int id, string name, string email, Dealer dealer)
        {
            Id = id;
            Name = name;
            Email = email;
            Dealer = dealer;
        }
    }
}
