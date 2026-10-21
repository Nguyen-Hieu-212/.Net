using System;
using System.Drawing;
using System.Windows.Forms;

namespace MdiApp
{
    public class FormLogin : Form
    {
        private readonly TextBox txtUser = new TextBox { Left = 20, Top = 45, Width = 240 };
        private readonly Button btnOk = new Button { Text = "Đăng nhập", Left = 160, Top = 85, Width = 100, Height = 30 };

        public string UserName { get; private set; } = "";

        public FormLogin()
        {
            Text = "Đăng nhập";
            ClientSize = new Size(280, 135);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = MinimizeBox = false;
            Font = new Font("Segoe UI", 10F);

            Controls.Add(new Label { Text = "Tên người dùng:", Left = 20, Top = 18, AutoSize = true });
            Controls.Add(txtUser);
            Controls.Add(btnOk);
            AcceptButton = btnOk;

            btnOk.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtUser.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên người dùng.");
                    return;
                }
                UserName = txtUser.Text.Trim();
                DialogResult = DialogResult.OK;
            };
        }
    }
}