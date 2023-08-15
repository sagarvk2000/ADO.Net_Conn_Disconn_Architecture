using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ADODemo.Models
{
    public class ProductCrud
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public ProductCrud()
        {
            string connstr = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            con = new SqlConnection(connstr);
        }

        public int AddProduct(Product prod)
        {
            //step-->query
            string qry = "insert into Product values(@name,@price,@cid)";
            //step2-->assign qry to command
            cmd = new SqlCommand(qry, con);
            //step 3 -->pass values to parameter
            cmd.Parameters.AddWithValue("@name",prod.Name);
            cmd.Parameters.AddWithValue("@price", prod.Price);
            cmd.Parameters.AddWithValue("@cid", prod.Cid);
            //step 4 --> fire the query
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public int UpdateProduct(Product prod)
        {
            //step-->query
            string qry = "update Product set pname=@name,price=@price,cid=@cid where pid=@id";
            //step2-->assign qry to command
            cmd = new SqlCommand(qry, con);
            //step 3 -->pass values to parameter
            cmd.Parameters.AddWithValue("@name", prod.Name);
            cmd.Parameters.AddWithValue("@price", prod.Price);
            cmd.Parameters.AddWithValue("@cid", prod.Cid);
            cmd.Parameters.AddWithValue("@id", prod.Id);
            //step 4 --> fire the query
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public int DeleteProduct(int id)
        {
            //step-->query
            string qry = "delete from Product  where pid=@id";
            //step2-->assign qry to command
            cmd = new SqlCommand(qry, con);
            //step 3 -->pass values to parameter
            cmd.Parameters.AddWithValue("@id", id);
            //step 4 --> fire the query
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();    
            return result;
        }
        public Product GetProductById(int id)
        {
            Product product = new Product();
            string qry = "select * from Product where pid=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    product.Id = Convert.ToInt32(dr["pid"]);
                    product.Name = dr["pname"].ToString();
                    product.Price = Convert.ToInt32(dr["price"]);
                    product.Cid = Convert.ToInt32(dr["cid"]);
                }
            }
            con.Close();
            return product;
        }

        public List<Category> GetCategories()
        {
            List<Category> list = new List<Category>();
            //strp 1 write a query
            string qry = "select * from Category";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Category c = new Category();
                    c.cid = Convert.ToInt32(dr["cid"]);
                    c.cname = dr["cname"].ToString();
                    list.Add(c);
                }
            }
            con.Close();
            return list;
        }

        public DataTable GetAllProduct()
        {
            DataTable dt = new DataTable();
            string qry = "select * from Product";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                dt.Load(dr);
            }
            con.Close();
            return dt;
        }
    }
}
