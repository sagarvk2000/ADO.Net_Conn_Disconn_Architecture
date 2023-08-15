using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ADODemo.Models;

namespace ADODemo
{
    public partial class Form2 : Form
    {
        EmployeeCrud crud;
        List<Department> list;
        public Form2()
        {
            InitializeComponent();
            crud = new EmployeeCrud();  
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                list = crud.GetDepartment();
                cmbDept.DataSource = list;
                cmbDept.DisplayMember = "Dname";
                cmbDept.ValueMember = "Did";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                Employee e1 = new Employee();
                e1.Eid = Convert.ToInt32(txtid.Text);
                e1.Ename = txtname.Text;
                e1.Salary = Convert.ToInt32(txtsalary.Text);
                e1.Did = Convert.ToInt32(cmbDept.SelectedValue);
                int res = crud.UpdateEmployee(e1);
                if (res > 0)
                {
                    MessageBox.Show("Record updated..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                Employee e1 = new Employee();
                e1.Ename = txtname.Text;
                e1.Salary = Convert.ToInt32(txtsalary.Text);
                e1.Did = Convert.ToInt32(cmbDept.SelectedValue);
                int res = crud.AddEmployee(e1);
                if (res > 0)
                {
                    MessageBox.Show("Record inserted..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                Employee emp = crud.GetEmployeeById(Convert.ToInt32(txtid.Text));
                if (emp.Eid > 0)
                {
                    foreach (Department item in list)
                    {
                        if (item.Did ==emp.Did )
                        {
                            cmbDept.Text = item.Dname;
                            break;
                        }
                    }
                    txtname.Text = emp.Ename;
                    txtsalary.Text = emp.Salary.ToString();

                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {

                int res = crud.DeleteEmployee(Convert.ToInt32(txtid.Text));
                if (res > 0)
                {
                    MessageBox.Show("Record deleted..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnshow_Click(object sender, EventArgs e)
        {
            DataTable table = crud.GetAllEmployee();
            dataGridView1.DataSource = table;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
