using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow.BuinessLogicLayer.CustomExceptions
{
    public class CustomException: Exception 
    {
        public CustomException(List<string> val) { this.list = val; }
        public List<string> list { get; }
    }
}
