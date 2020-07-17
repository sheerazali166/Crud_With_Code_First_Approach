using Crud_With_Entity_Framework.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crud_With_Entity_Framework
{
    public partial class Form1 : Form
    {
        Employee model = new Employee();
        DatabaseContext db = new DatabaseContext();
        int id;

        public Form1()
        {
            InitializeComponent();
            BindGridView();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            model.Name = textBoxName.Text;
            model.Gender = comboBoxGender.SelectedItem.ToString();
            model.Age = Convert.ToInt32(textBoxAge.Text);
            model.Designation = textBoxDesignation.Text;

            db.Employees.Add(model);
            int r = db.SaveChanges();

            if (r > 0)
            {

                MessageBox.Show("Data Successfully Inserted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGridView();
                ResetControls();
            }
            else {

                MessageBox.Show("Data Insertion Failed", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            model = db.Employees.Where(x => x.Id == id).FirstOrDefault();
            model.Name = textBoxName.Text;
            model.Gender = comboBoxGender.SelectedItem.ToString();
            model.Age = Convert.ToInt32(textBoxAge.Text);
            model.Designation = textBoxDesignation.Text;

            db.Entry(model).State = EntityState.Modified;
            int r = db.SaveChanges();

            if (r > 0)
            {

                MessageBox.Show("Data Successfully Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGridView();
                ResetControls();
            }
            else
            {

                MessageBox.Show("Data Updation Failed", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure", "Asking", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {

                model = db.Employees.Where(x => x.Id == id).FirstOrDefault();
                db.Entry(model).State = EntityState.Deleted;
                int r = db.SaveChanges();

                if (r > 0)
                {

                    MessageBox.Show("Data Successfully Delete", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridView();
                    ResetControls();
                }
                else
                {

                    MessageBox.Show("Data Deletion Failed", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else {

                MessageBox.Show("You have cancelled the deletion operation");
            }
        }

        public void BindGridView() {

            dataGridViewShowData.DataSource = db.Employees.ToList<Employee>();
        
        }

        public void ResetControls() {

            textBoxName.Clear();
            comboBoxGender.SelectedItem = null;
            textBoxAge.Clear();
            textBoxDesignation.Clear();

        }

        private void dataGridViewShowData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            id = Convert.ToInt32(dataGridViewShowData.SelectedRows[0].Cells[0].Value);
            model = db.Employees.Where(x => x.Id == id).FirstOrDefault();
            textBoxName.Text = model.Name;
            comboBoxGender.SelectedItem = model.Gender;
            textBoxAge.Text = model.Age.ToString();
            textBoxDesignation.Text = model.Designation;
        }
    }
}
