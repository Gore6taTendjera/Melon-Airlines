using DTOs;
using Logic_Layer;
using Logic_Layer.Interface.LL;
using Logic_Layer.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms_App
{
    public partial class Login : Form
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IServiceProvider _serviceProvider;

        public Login(IUserAccountService userAccountService, IServiceProvider serviceProvider)
        {
            _userAccountService = userAccountService;
            _serviceProvider = serviceProvider;
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            UserLoginDTO user = _userAccountService.Authenticate(tbxLoginUsername.Text.ToString(), tbxLoginPassword.Text.ToString());

            if (user != null && user.UserType == Enums.UserType.Admin)
            {
                Form1 nextForm = _serviceProvider.GetRequiredService<Form1>();
                this.Hide();
                nextForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to login");
            }
        }
    }
}
