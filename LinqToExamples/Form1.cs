using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqToExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NorthwindEntities db = new NorthwindEntities();
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Products.OrderBy(x => x.ProductName).ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Employees.Where(x => SqlFunctions.DateDiff("YEAR", x.BirthDate, DateTime.Now) > 60).Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.BirthDate,


            }).ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = db.Order_Details.OrderByDescending(x => x.UnitPrice).Select(x => new
            {
                x.Order,
                x.Product,
                x.UnitPrice,

            }).Where(x => x.UnitPrice > 50).ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Employees.OrderBy(x => x.BirthDate).Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.Title,
                Age = (SqlFunctions.DateDiff("YEAR", x.BirthDate, DateTime.Now))

            }).ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Category category = db.Categories.First(x => x.CategoryID > 7);
                MessageBox.Show(category.CategoryName);
            }
            catch (Exception)
            {
                MessageBox.Show("There is no such category!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            Category category = db.Categories.Find(1);
            MessageBox.Show($"Category Name: {category.CategoryName}\nDescreption: {category.Description}");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Products.Where(x => x.UnitsInStock >= 20 && x.UnitsInStock <= 60).Take(5).OrderByDescending(x => x.UnitPrice).ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Products.OrderByDescending(x => x.UnitsInStock).Skip(5).Take(10).Select(x => new
            {
                x.ProductName,
                x.UnitsInStock,
                x.UnitPrice,
                x.Category,

            }).ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Categories.Where(x => x.Description.Contains("drinks")).ToList();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Employees.Where(x => x.FirstName.StartsWith("A")).OrderByDescending(x => x.BirthDate).ToList();
        }
        private void button15_Click(object sender, EventArgs e)
        {
            Product product = db.Products.FirstOrDefault(x => x.ProductID > 6);

            if (product == null)
            {
                MessageBox.Show("There is no such product!");
            }
            else
            {
                MessageBox.Show(product.ProductName);
            }
        }


        private void button17_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Products.Where(x => x.ProductName.EndsWith("e")).ToList();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Products.GroupBy(x => x.Category.CategoryName).Select(y => new
            {
                CategoryName = y.Key,
                UnitInStock = y.Sum(x => x.UnitsInStock)
            }).ToList();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            bool result = db.Products.Any(x => x.UnitPrice > 100);
            MessageBox.Show(result.ToString());
        }

    
    }
}
