using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalAdvisor.Models
{
    public class Pagination
    {
        public int id { get; set; }
        public int ProductId { get; set; }
        public int CurrentPage { get; set; }
        //public DateTime WhoIsUsing { get; set; }
       
        
        
        public Pagination()
        {
        }

        public Pagination(int productId, int currentPage)
        {
            ProductId = productId;
            CurrentPage = currentPage;
            //WhoIsUsing = DateTime.Now; //No futuro, para auxiliar que nao aconteça alguma merda quando mais de uma pessoa estiver vendo os manuais
        }



    }
}
