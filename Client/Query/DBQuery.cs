using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMaker_Client.Query
{
    public class DBQuery
    {
        public string SelectDrink()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT * FROM CM_DRINK ");
           
            return sb.ToString();
        }
        public string SelectDeco()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT * FROM CM_OPTIONS WHERE OPTIONNO > 105 ");

            return sb.ToString();
        }
    }
}
