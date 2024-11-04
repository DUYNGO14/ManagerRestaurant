using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerRestaurant.Domain.Model
{
    public class ResponLogin
    {
       public long TokenExpired { get; set; }
        public string Token { get; set; }
    }
}
