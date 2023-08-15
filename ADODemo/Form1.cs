using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ADODemo.Models;

namespace ADODemo
{
    public partial class Form1 : Form
    {
        ProductCrud crud;
        List<Category> list;
        public Form1()
        {
            InitializeComponent();
            crud = new ProductCrud();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                list = crud.GetCategories();
                cmbCategoryname.DataSource = list;
                cmbCategoryname.DisplayMember = "cname";
                cmbCategoryname.ValueMember = "cid";
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Product prod = crud.GetProductById(Convert.ToInt32(txtProdId.Text));
                if (prod.Id > 0)
                {
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
            DataTable table = crud.GetAllProduct();
            dataGridView1.DataSource = table;
        }
    }
}
