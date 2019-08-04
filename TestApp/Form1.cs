using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TestLib.UserManageLogics.Factory;
using TestLib.UserManageLogics.Factory.Impl;
using TestLib.UserManageLogics.Models;

namespace TestApp
{
    public partial class Form1 : Form
    {
        private IUserManageLogicFactory _factory = new UserManageLogicFactory();

        private BindingList<UserDataModel> dataSource = new BindingList<UserDataModel>();

        public Form1()
        {
            InitializeComponent();

            dataGridView1.DataSource = dataSource;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var logic = _factory.CreateUserFindLogic();
            logic.Add(new UserDataModel
            {
                Name = textBoxName.Text,
                Age = Convert.ToInt32(numericUpDownAge.Value),
                Mail = textBoxMail.Text,
            });
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            var logic = _factory.CreateUserFindLogic();
            var users = logic.Find(new UserFindModel());

            dataSource.Clear();
            foreach (var user in users)
            {
                dataSource.Add(user);
            }
        }
    }
}
