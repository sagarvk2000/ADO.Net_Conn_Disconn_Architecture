using ADODemo.Models;
using System;
using System.Collections.Generic;
using System.Data;

using System.Windows.Forms;

namespace ADODemo
{
    public partial class Form3 : Form
    {
        ProductDisconnected crud;
        DataTable table;
        public Form3()
        {
            InitializeComponent();
            crud = new ProductDisconnected();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            DataTable table = crud.GetAllCategories();
            cmbCategoryname.DataSource = table;
            cmbCategoryname.DisplayMember = "cname";
            cmbCategoryname.ValueMember = "cid";

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Product prod = crud.GetProductById(Convert.ToInt32(txtProdId.Text));
                if (prod.Id > 0)
                {
                    List<Category> list = new List<Category>();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        Category c = new Category();
                        c.cid = Convert.ToInt32(table.Rows[i]["cid"]);
                        c.cname = table.Rows[i]["cname"].ToString();
                        list.Add(c);
                    }
                    foreach (Category item in list)
                    {
                        if (item.cid == prod.Cid)
                        {
                            cmbCategoryname.Text = item.cname;
                            break;
                        }
                    }
                    txtProdname.Text = prod.Name;
                    txtProdprice.Text = prod.Price.ToString();

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Product p = new Product();
                p.Name = txtProdname.Text;
                p.Price = Convert.ToInt32(txtProdprice.Text);
                p.Cid = Convert.ToInt32(cmbCategoryname.SelectedValue);
                int res = crud.AddProduct(p);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Product p = new Product();
                p.Id = Convert.ToInt32(txtProdId.Text);
                p.Name = txtProdname.Text;
                p.Price = Convert.ToInt32(txtProdprice.Text);
                p.Cid = Convert.ToInt32(cmbCategoryname.SelectedValue);
                int res = crud.UpdateProduct(p);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                int res = crud.DeleteProduct(Convert.ToInt32(txtProdId.Text));
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

        private void btnShow_Click(object sender, EventArgs e)
        {
            DataSet ds = crud.GetAllProducts();
            dataGridView1.DataSource = ds.Tables["Product"];
        }
    }


}
