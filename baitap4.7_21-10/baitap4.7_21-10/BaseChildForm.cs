using System;
using System.Windows.Forms;

namespace MdiApp
{
    // Lớp cơ sở cho mọi MDI Child: cung cấp cơ chế giao tiếp với Form cha
    public class BaseChildForm : Form
    {
        // Form con -> Form cha: phát thông điệp để cập nhật StatusStrip
        public event EventHandler<string>? ActionPerformed;

        protected void Notify(string message)
        {
            ActionPerformed?.Invoke(this, message);
        }

        // Form cha -> Form con: cha gọi hàm này (VD: nút "Làm mới" trên ToolStrip)
        public virtual void RefreshData() { }
    }
}