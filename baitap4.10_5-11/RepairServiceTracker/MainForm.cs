using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RepairServiceTracker
{
    public partial class MainForm : Form
    {
        private static readonly string[] DeviceTypes =
        {
            "Dien thoai", "May tinh xach tay", "May tinh bang", "Man hinh", "May in", "Thiet bi khac"
        };

        private readonly BindingList<RepairOrder> _all = new BindingList<RepairOrder>();   // toan bo don
        private readonly BindingList<RepairOrder> _view = new BindingList<RepairOrder>();  // don sau khi loc (gan vao BindingSource)
        private bool _loading;        // dang nap du lieu len o nhap -> bo qua su kien
        private string _imagePath = "";   // anh dang chon cho don hien tai

        public MainForm()
        {
            InitializeComponent();
            BuildUi();
            bs.DataSource = _view;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            cboDevice.Items.AddRange(DeviceTypes);
            cboStatus.Items.AddRange(RepairOrder.Statuses);
            cboFilterStatus.Items.Add("Tat ca");
            cboFilterStatus.Items.AddRange(RepairOrder.Statuses);
            cboFilterStatus.SelectedIndex = 0;

            SeedData();
            ApplyFilter();
            if (_view.Count == 0) ClearInputs();
        }

        private void SeedData()
        {
            DateTime t = DateTime.Today;
            _all.Add(new RepairOrder { Id = "SC001", CustomerName = "Nguyen Van An", Phone = "0912345678", DeviceType = DeviceTypes[0], Problem = "Man hinh bi vo, khong len hinh", ReceivedDate = t.AddDays(-5), EstimatedCost = 1500000, Status = RepairOrder.Repairing, ImagePath = "" });
            _all.Add(new RepairOrder { Id = "SC002", CustomerName = "Tran Thi Binh", Phone = "0987654321", DeviceType = DeviceTypes[1], Problem = "May khong khoi dong duoc", ReceivedDate = t.AddDays(-3), EstimatedCost = 800000, Status = RepairOrder.WaitingParts, ImagePath = "" });
            _all.Add(new RepairOrder { Id = "SC003", CustomerName = "Le Minh Chau", Phone = "0345678901", DeviceType = DeviceTypes[4], Problem = "May in bi ket giay", ReceivedDate = t.AddDays(-10), EstimatedCost = 300000, Status = RepairOrder.Completed, ImagePath = "" });
            _all.Add(new RepairOrder { Id = "SC004", CustomerName = "Pham Quoc Dung", Phone = "0933222111", DeviceType = DeviceTypes[2], Problem = "Pin chai nhanh", ReceivedDate = t.AddDays(-1), EstimatedCost = 450000, Status = RepairOrder.New, ImagePath = "" });
            _all.Add(new RepairOrder { Id = "SC005", CustomerName = "Hoang Thu Ha", Phone = "0966777888", DeviceType = DeviceTypes[3], Problem = "Man hinh bi soc ngang", ReceivedDate = t.AddDays(-7), EstimatedCost = 600000, Status = RepairOrder.Cancelled, ImagePath = "" });
        }

        // ================== Loc / tim kiem ==================

        private void FilterChanged(object sender, EventArgs e) => ApplyFilter();

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboFilterStatus.SelectedIndex = 0;
            ApplyFilter();
        }

        private static bool Has(string s, string kw) =>
            s != null && s.IndexOf(kw, StringComparison.CurrentCultureIgnoreCase) >= 0;

        /// <summary>Loc lai _view tu _all. keepId: giu lai dong co ma nay; keepCurrent: neu khong co keepId thi giu dong dang chon.</summary>
        private void ApplyFilter(string keepId = null, bool keepCurrent = true)
        {
            if (keepCurrent && keepId == null && bs.Current is RepairOrder cur)
                keepId = cur.Id;

            string kw = txtSearch.Text.Trim();
            string status = cboFilterStatus.SelectedIndex > 0 ? (string)cboFilterStatus.SelectedItem : null;

            var result = _all
                .Where(o =>
                    (kw.Length == 0 || Has(o.Id, kw) || Has(o.CustomerName, kw) || Has(o.Phone, kw))
                    && (status == null || o.Status == status))
                .OrderBy(o => o.ReceivedDate)
                .ThenBy(o => o.Id)
                .ToList();

            _view.RaiseListChangedEvents = false;
            _view.Clear();
            foreach (var o in result) _view.Add(o);
            _view.RaiseListChangedEvents = true;
            _view.ResetBindings();

            if (keepId != null)
            {
                int idx = _view.ToList().FindIndex(o => o.Id == keepId);
                if (idx >= 0) bs.Position = idx;
            }

            UpdateStatusBar();
            LoadCurrent();
        }

        /// <summary>So don theo bo loc + tong chi phi du kien cua cac don chua hoan tat (khong tinh don da huy).</summary>
        private void UpdateStatusBar()
        {
            decimal pending = _all
                .Where(o => o.Status != RepairOrder.Completed && o.Status != RepairOrder.Cancelled)
                .Sum(o => o.EstimatedCost);

            lblCount.Text = $"So don (theo bo loc): {_view.Count} / {_all.Count}";
            lblTotal.Text = $"   |   Tong chi phi du kien (don chua hoan tat): {pending:N0}";
        }

        // ================== Chon dong -> nap len o nhap ==================

        private void bs_CurrentChanged(object sender, EventArgs e) => LoadCurrent();

        private void LoadCurrent()
        {
            if (bs.Current is RepairOrder o)
            {
                _loading = true;
                txtId.Text = o.Id;
                txtCustomer.Text = o.CustomerName;
                txtPhone.Text = o.Phone;
                cboDevice.SelectedItem = o.DeviceType;
                txtProblem.Text = o.Problem;
                dtpReceived.Value = o.ReceivedDate;
                txtCost.Text = o.EstimatedCost.ToString("N0");
                cboStatus.SelectedItem = o.Status;
                ShowImage(o.ImagePath);
                _loading = false;
                ClearErrors();
            }
            else
            {
                ClearInputs();
            }
        }

        private void ClearInputs()
        {
            _loading = true;
            txtId.Text = NextId();
            txtCustomer.Clear();
            txtPhone.Clear();
            cboDevice.SelectedIndex = 0;
            txtProblem.Clear();
            dtpReceived.Value = DateTime.Today;
            txtCost.Text = "0";
            cboStatus.SelectedIndex = 0;
            ShowImage(null);
            _loading = false;
            ClearErrors();
        }

        private string NextId()
        {
            int max = 0;
            foreach (var o in _all)
            {
                var m = Regex.Match(o.Id ?? "", @"^SC(\d+)$", RegexOptions.IgnoreCase);
                if (m.Success && int.TryParse(m.Groups[1].Value, out int n) && n > max) max = n;
            }
            return "SC" + (max + 1).ToString("D3");
        }

        // ================== Anh thiet bi ==================

        /// <summary>Hien anh len PictureBox (Zoom). Tra ve false neu khong mo duoc anh.</summary>
        private bool ShowImage(string path)
        {
            var old = picDevice.Image;
            picDevice.Image = null;
            old?.Dispose();

            _imagePath = path ?? "";
            txtImagePath.Text = _imagePath;

            if (_imagePath.Length == 0) return true;
            if (!File.Exists(_imagePath)) return false;

            try
            {
                // Doc qua stream roi copy sang Bitmap de khong khoa file anh
                using (var fs = new FileStream(_imagePath, FileMode.Open, FileAccess.Read))
                using (var img = Image.FromStream(fs))
                {
                    picDevice.Image = new Bitmap(img);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Title = "Chon anh thiet bi",
                Filter = "Anh (*.png;*.jpg;*.jpeg;*.bmp;*.gif)|*.png;*.jpg;*.jpeg;*.bmp;*.gif|Tat ca (*.*)|*.*"
            };
            if (ofd.ShowDialog(this) != DialogResult.OK) return;

            string previous = _imagePath;
            if (!ShowImage(ofd.FileName))
            {
                ShowImage(previous);
                MessageBox.Show("Khong the mo file anh da chon.", "Loi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveImage_Click(object sender, EventArgs e) => ShowImage(null);

        // ================== Kiem tra du lieu (ErrorProvider) ==================

        private void ClearErrors()
        {
            foreach (Control c in new Control[] { txtId, txtCustomer, txtPhone, cboDevice, txtProblem, dtpReceived, txtCost, cboStatus })
                errorProvider1.SetError(c, "");
        }

        private void ClearErrorOnEdit(object sender, EventArgs e)
        {
            if (_loading) return;
            if (sender is Control c) errorProvider1.SetError(c, "");
        }

        private bool Check(Control c, string error)
        {
            errorProvider1.SetError(c, error ?? "");
            return error == null;
        }

        private bool TryParseCost(out decimal cost) =>
            decimal.TryParse(txtCost.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out cost);

        /// <param name="editing">Don dang cap nhat (null neu them moi).</param>
        private bool ValidateInput(RepairOrder editing)
        {
            string id = txtId.Text.Trim();

            string idErr = null;
            if (id.Length == 0)
                idErr = "Ma don khong duoc de trong.";
            else if (_all.Any(o => !ReferenceEquals(o, editing)
                                   && string.Equals(o.Id, id, StringComparison.OrdinalIgnoreCase)))
                idErr = "Ma don da ton tai.";

            string costText = txtCost.Text.Trim();
            string costErr = null;
            if (costText.Length == 0)
                costErr = "Vui long nhap chi phi du kien.";
            else if (!TryParseCost(out decimal cost))
                costErr = "Chi phi du kien phai la so.";
            else if (cost < 0)
                costErr = "Chi phi du kien khong duoc am.";

            bool ok = true;
            ok &= Check(txtId, idErr);
            ok &= Check(txtCustomer, txtCustomer.Text.Trim().Length == 0 ? "Ten khach hang khong duoc de trong." : null);
            ok &= Check(txtPhone, txtPhone.Text.Trim().Length == 0 ? "So dien thoai khong duoc de trong." : null);
            ok &= Check(cboDevice, cboDevice.SelectedIndex < 0 ? "Vui long chon loai thiet bi." : null);
            ok &= Check(txtProblem, txtProblem.Text.Trim().Length == 0 ? "Vui long nhap mo ta loi." : null);
            ok &= Check(txtCost, costErr);
            ok &= Check(cboStatus, cboStatus.SelectedIndex < 0 ? "Vui long chon trang thai." : null);
            return ok;
        }

        private void ReadInputsInto(RepairOrder o)
        {
            TryParseCost(out decimal cost);
            o.Id = txtId.Text.Trim();
            o.CustomerName = txtCustomer.Text.Trim();
            o.Phone = txtPhone.Text.Trim();
            o.DeviceType = (string)cboDevice.SelectedItem;
            o.Problem = txtProblem.Text.Trim();
            o.ReceivedDate = dtpReceived.Value.Date;
            o.EstimatedCost = cost;
            o.Status = (string)cboStatus.SelectedItem;
            o.ImagePath = _imagePath;
        }

        // ================== Them / Cap nhat / Xoa ==================

        private RepairOrder SelectedOrder()
        {
            if (dgv.SelectedRows.Count == 0) return null;
            return dgv.SelectedRows[0].DataBoundItem as RepairOrder;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(null)) return;

            if (MessageBox.Show("Them don sua chua moi?", "Xac nhan",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var o = new RepairOrder();
            ReadInputsInto(o);
            _all.Add(o);
            ApplyFilter(o.Id);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var cur = SelectedOrder();
            if (cur == null)
            {
                MessageBox.Show("Vui long chon mot don trong danh sach de cap nhat.", "Thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!ValidateInput(cur)) return;

            if (MessageBox.Show("Cap nhat don nay?", "Xac nhan",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            ReadInputsInto(cur);
            ApplyFilter(cur.Id);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var cur = SelectedOrder();
            if (cur == null)
            {
                MessageBox.Show("Vui long chon mot don trong danh sach de xoa.", "Thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"Xoa don {cur.Id} cua {cur.CustomerName}?", "Xac nhan xoa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            _all.Remove(cur);
            ApplyFilter(null, false);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
            dgv.ClearSelection();
            txtId.Focus();
        }

        // ================== Xuat CSV (cac don dang hien thi) ==================

        private void mnuExport_Click(object sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                FileName = "don_sua_chua_" + DateTime.Now.ToString("yyyyMMdd") + ".csv"
            };
            if (sfd.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("Ma don,Khach hang,So dien thoai,Loai thiet bi,Mo ta loi,Ngay tiep nhan,Chi phi du kien,Trang thai,Anh");
                foreach (var o in _view)
                {
                    sb.AppendLine(string.Join(",",
                        Csv(o.Id), Csv(o.CustomerName), Csv(o.Phone), Csv(o.DeviceType), Csv(o.Problem),
                        Csv(o.ReceivedDate.ToString("dd/MM/yyyy")),
                        o.EstimatedCost.ToString("0.##", CultureInfo.InvariantCulture),
                        Csv(o.Status), Csv(o.ImagePath)));
                }
                File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                MessageBox.Show($"Da xuat {_view.Count} don.", "Xuat CSV",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Khong the ghi file: " + ex.Message, "Loi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string Csv(string s)
        {
            s ??= "";
            if (s.IndexOfAny(new[] { ',', '"', '\r', '\n' }) >= 0)
                s = "\"" + s.Replace("\"", "\"\"") + "\"";
            return s;
        }
    }
}