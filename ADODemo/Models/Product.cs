using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODemo.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Cid { get; set; }
    }

    public class Category
    {
        public int cid { get; set; }
        public string cname { get; set; }
    }
}
