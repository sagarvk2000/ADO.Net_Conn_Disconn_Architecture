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
    public class EmployeeCrud
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public EmployeeCrud()
        {
            string connstr = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            con = new SqlConnection(connstr);
        }

        public int AddEmployee(Employee emp)
        {
            string qry = "insert into Employee values(@Ename,@Salary,@Did)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@Ename", emp.Ename);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            cmd.Parameters.AddWithValue("@Did", emp.Did);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int UpdateEmployee(Employee emp)
        {
            //step-->query
            string qry = "update Employee set Ename=@Ename,Salary=@Salary,Did=@Did where Eid=@Eid";
            //step2-->assign qry to command
            cmd = new SqlCommand(qry, con);
            //step 3 -->pass values to parameter
            cmd.Parameters.AddWithValue("@Ename", emp.Ename);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            cmd.Parameters.AddWithValue("@Did", emp.Did);
            cmd.Parameters.AddWithValue("@Eid", emp.Eid);
            //step 4 --> fire the query
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int DeleteEmployee(int Eid)
        {
            //step-->query
            string qry = "delete from Employee  where Eid=@Eid";
            //step2-->assign qry to command
            cmd = new SqlCommand(qry, con);
            //step 3 -->pass values to parameter
            cmd.Parameters.AddWithValue("@Eid", Eid);
            //step 4 --> fire the query
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public List<Department> GetDepartment()
        {
            List<Department> list = new List<Department>();
            string qry = "Select * from Department";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    Department d = new Department();
                    d.Did = Convert.ToInt32(dr["Did"]);
                    d.Dname = dr["Dname"].ToString();
                    list.Add(d);
                }
            }
            con.Close();
            return list;
        }
        public Employee GetEmployeeById(int Eid)
        {
            Employee employee = new Employee();
            string qry = "select * from Employee where Eid=@Eid";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@Eid", Eid);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    employee.Eid = Convert.ToInt32(dr["Eid"]);
                    employee.Ename = dr["Ename"].ToString();
                    employee.Salary = Convert.ToInt32(dr["Salary"]);
                    employee.Did = Convert.ToInt32(dr["Did"]);
                }
            }
            con.Close();
            return employee;
        }

        public DataTable GetAllEmployee()
        {
            DataTable dt = new DataTable();
            string qry = "select * from Employee";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dt.Load(dr);
            }
            con.Close();
            return dt;
        }
    }
}
