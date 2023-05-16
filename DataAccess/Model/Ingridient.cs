using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Ingridient
    {
        public int Id { get; set; }
        public string? IngridientName { get; set; }
        public bool IsVegetarian { get; set; }
    }
}