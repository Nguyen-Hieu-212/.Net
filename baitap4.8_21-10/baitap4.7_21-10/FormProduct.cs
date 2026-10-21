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

        // Async + ProgressBar
        private readonly Button btnLoad = new Button { Text = "Nạp dữ liệu (Async)", Left = 15, Top = 8, Width = 180, Height = 30 };
        private readonly ProgressBar progressBar = new ProgressBar { Left = 210, Top = 12, Width = 400, Height = 22 };
        private readonly Label lblPercent = new Label { Text = "0%", Left = 620, Top = 14, AutoSize = true };

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

            // Thanh nạp dữ liệu bất đồng bộ: Button + ProgressBar + Label %
            var pnlLoad = new Panel { Dock = DockStyle.Top, Height = 46 };
            pnlLoad.Controls.AddRange(new Control[] { btnLoad, progressBar, lblPercent });
            Controls.Add(pnlLoad);
            btnLoad.Click += async (s, e) => await LoadDataAsync();

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

        // ================= ASYNC / AWAIT + PROGRESSBAR =================
        // Nạp dữ liệu dạng async Task<List<T>>: dùng Task.Run + Thread.Sleep để giả lập tác vụ nặng (CSDL/File)
        private async Task<List<Product>> LoadProductsAsync(IProgress<int> progress)
        {
            return await Task.Run(() =>
            {
                const int total = 100;
                var result = new List<Product>();
                for (int i = 1; i <= total; i++)
                {
                    Thread.Sleep(40); // giả lập tốn thời gian
                    result.Add(new Product
                    {
                        ProductId = $"SP{i:000}",
                        ProductName = $"Sản phẩm {i}",
                        Price = 100000 + i * 5000,
                        Quantity = 10 + i % 50
                    });
                    progress.Report(i * 100 / total); // báo tiến trình về UI Thread
                }
                return result;
            });
        }

        private async Task LoadDataAsync()
        {
            btnLoad.Enabled = false; // khóa nút để tránh bấm trùng lặp
            progressBar.Value = 0;
            lblPercent.Text = "0%";
            Notify("Bắt đầu nạp danh sách sản phẩm...");

            try
            {
                var progress = new Progress<int>(p =>
                {
                    if (IsDisposed) return;
                    progressBar.Value = p;
                    lblPercent.Text = p + "%";
                    ReportProgress(p); // cập nhật % lên StatusStrip của FormMain
                });

                var data = await LoadProductsAsync(progress);

                DataStore.Products.RaiseListChangedEvents = false;
                DataStore.Products.Clear();
                foreach (var p in data) DataStore.Products.Add(p);
                DataStore.Products.RaiseListChangedEvents = true;
                DataStore.Products.ResetBindings();

                ReportProgress(100);
                Notify($"Nạp xong {data.Count} sản phẩm.");
                if (!IsDisposed)
                    MessageBox.Show($"Nạp xong {data.Count} sản phẩm!", "Thông báo");
            }
            catch (Exception ex)
            {
                ReportProgress(0);
                Notify("Lỗi khi nạp dữ liệu: " + ex.Message);
                if (!IsDisposed)
                    MessageBox.Show("Lỗi khi nạp dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed) btnLoad.Enabled = true; // mở lại nút bấm
            }
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