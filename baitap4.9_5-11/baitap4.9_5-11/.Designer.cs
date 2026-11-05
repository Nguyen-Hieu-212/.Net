namespace LabEquipmentCheckout
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        // Giao dien duoc dung bang code trong MainForm.UI.cs (ham BuildUi),
        // nen designer chi can phan khoi tao co ban nay.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 660);
            this.Name = "MainForm";
            this.Text = "Lab Equipment Checkout";
            this.ResumeLayout(false);
        }

        #endregion
    }
}