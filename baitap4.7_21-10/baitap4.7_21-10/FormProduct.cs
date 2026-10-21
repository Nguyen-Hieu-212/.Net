using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MdiApp
{
    // MDI Child: Quản lý Sản phẩm
    public class FormProduct : BaseChildForm
    {
        private readonly DataGridView dgv = new DataGridView();
        private readonly TextBox txtId = new TextBox { Left = 100, Top = 15, Width = 120 };
        private readonly TextBox txtName = new TextBox { Left = 330, Top = 15, Width = 180 };
        private readonly TextBox txtPrice = new TextBox { Left = 100, Top = 50, Width = 120 };
        private readonly TextBox txtQty = new TextBox { Left = 330, Top = 50, Width = 180 };
        private readonly ContextMenuStrip ctx = new ContextMenuStrip();

        public FormProduct()
        {
            Text = "Quản lý Sản phẩm";
            Size = new Size(780, 460);
            Font = new Font("Segoe UI", 10F);

            var pnl = new Panel { Dock = DockStyle.Top, Height = 95 };
            pnl.Controls.Add(new Label { Text = "Mã SP:", Left = 15, Top = 18, AutoSize = true });
            pnl.Controls.Add(new Label { Text = "Tên SP:", Left = 245, Top = 18, AutoSize = true });
            pnl.Controls.Add(new Label { Text = "Đơn giá:", Left = 15, Top = 53, AutoSize = true });
            pnl.Controls.Add(new Label { Text = "Số lượng:", Left = 245, Top = 53, AutoSize = true });
            pnl.Controls.AddRange(new Control[] { txtId, txtName, txtPrice, txtQty });

            var btnAdd = new Button { Text = "Thêm", Left = 540, Top = 12, Width = 100, Height = 30 };
            var btnDelete = new Button { Text = "Xóa", Left = 650, Top = 12, Width = 100, Height = 30 };
            btnAdd.Click += (s, e) => AddProduct();
            btnDelete.Click += (s, e) => DeleteSelected();
            pnl.Controls.AddRange(new Control[] { btnAdd, btnDelete });

            dgv.Dock = DockStyle.Fill;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = Color.White;
            dgv.RowHeadersVisible = false;
            // Phải đưa control vào Form trước khi gán DataSource thì cột mới được tạo
            Controls.Add(dgv);
            Controls.Add(pnl);
            dgv.DataSource = DataStore.Products;
            dgv.Columns[nameof(Product.ProductId)].HeaderText = "Mã SP";
            dgv.Columns[nameof(Product.ProductName)].HeaderText = "Tên sản phẩm";
            dgv.Columns[nameof(Product.Price)].HeaderText = "Đơn giá";
            dgv.Columns[nameof(Product.Price)].DefaultCellStyle.Format = "N0";
            dgv.Columns[nameof(Product.Quantity)].HeaderText = "Số lượng";

            ctx.Items.Add(new ToolStripMenuItem("Xóa dòng", null, (s, e) => DeleteSelected()));
            ctx.Items.Add(new ToolStripMenuItem("Làm mới dữ liệu", null, (s, e) => RefreshData()));
            ctx.Items.Add(new ToolStripMenuItem("Xem chi tiết", null, (s, e) => ShowDetail()));
            dgv.ContextMenuStrip = ctx;

            dgv.CellMouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                {
                    dgv.ClearSelection();
                    dgv.Rows[e.RowIndex].Selected = true;
                    dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[0];
                }
            };
        }

        private Product? Selected() => dgv.CurrentRow?.DataBoundItem as Product;

        private void AddProduct()
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã SP và Tên SP.");
                return;
            }
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ.");
                return;
            }
            if (!int.TryParse(txtQty.Text, out int qty) || qty < 0)
            {
                MessageBox.Show("Số lượng không hợp lệ.");
                return;
            }
            if (DataStore.Products.Any(x => x.ProductId.Equals(txtId.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Mã SP đã tồn tại.");
                return;
            }

            DataStore.Products.Add(new Product
            {
                ProductId = txtId.Text.Trim(),
                ProductName = txtName.Text.Trim(),
                Price = price,
                Quantity = qty
            });

            Notify($"Thêm sản phẩm \"{txtName.Text.Trim()}\" thành công.");
            ClearInputs();
        }

        private void DeleteSelected()
        {
            var p = Selected();
            if (p == null) return;

            if (MessageBox.Show($"Xóa sản phẩm \"{p.ProductName}\"?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            DataStore.Products.Remove(p);
            Notify($"Xóa sản phẩm \"{p.ProductName}\" thành công.");
        }

        private void ShowDetail()
        {
            var p = Selected();
            if (p == null) return;

            MessageBox.Show(
                $"Mã SP: {p.ProductId}\nTên SP: {p.ProductName}\nĐơn giá: {p.Price:N0}\nSố lượng: {p.Quantity}",
                "Chi tiết sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Notify($"Đã xem chi tiết sản phẩm \"{p.ProductName}\".");
        }

        public override void RefreshData()
        {
            DataStore.Products.ResetBindings();
            dgv.Refresh();
            ClearInputs();
            Notify("Đã làm mới danh sách sản phẩm.");
        }

        private void ClearInputs()
        {
            txtId.Clear(); txtName.Clear(); txtPrice.Clear(); txtQty.Clear();
            txtId.Focus();
        }
    }
}