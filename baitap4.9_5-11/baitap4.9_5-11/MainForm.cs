using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LabEquipmentCheckout
{
    public partial class MainForm : Form
    {
        private static readonly string[] Equipments =
        {
            "Oscilloscope", "Multimeter", "Nguon DC", "May phat tin hieu",
            "Bo thi nghiem vi dieu khien", "Kinh hien vi"
        };

        private static readonly string[] FilterItems =
        {
            "Tat ca", Loan.Borrowing, Loan.Overdue, Loan.Returned
        };

        private readonly BindingList<Loan> _all = new BindingList<Loan>();   // toan bo phieu
        private readonly BindingList<Loan> _view = new BindingList<Loan>();  // phieu sau khi loc (gan vao BindingSource)
        private bool _loading;   // dang nap du lieu len o nhap -> bo qua su kien

        public MainForm()
        {
            InitializeComponent();
            BuildUi();
            bs.DataSource = _view;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            cboEquipment.Items.AddRange(Equipments);
            cboFilterStatus.Items.AddRange(FilterItems);
            cboFilterStatus.SelectedIndex = 0;

            SeedData();
            ApplyFilter();
            if (_view.Count == 0) ClearInputs();
        }

        private void SeedData()
        {
            DateTime t = DateTime.Today;
            _all.Add(new Loan { Id = "PM001", StudentId = "21001234", StudentName = "Nguyen Van An", Equipment = Equipments[0], Quantity = 1, BorrowDate = t.AddDays(-10), DueDate = t.AddDays(-3), ReturnedDate = null });       // qua han
            _all.Add(new Loan { Id = "PM002", StudentId = "21005678", StudentName = "Tran Thi Binh", Equipment = Equipments[1], Quantity = 2, BorrowDate = t.AddDays(-2), DueDate = t.AddDays(5), ReturnedDate = null });         // dang muon
            _all.Add(new Loan { Id = "PM003", StudentId = "21009012", StudentName = "Le Minh Chau", Equipment = Equipments[2], Quantity = 1, BorrowDate = t.AddDays(-8), DueDate = t.AddDays(-1), ReturnedDate = t.AddDays(-2) }); // da tra
            _all.Add(new Loan { Id = "PM004", StudentId = "21003456", StudentName = "Pham Quoc Dung", Equipment = Equipments[4], Quantity = 1, BorrowDate = t.AddDays(-1), DueDate = t.AddDays(6), ReturnedDate = null });        // dang muon
        }

        // ================== Loc / tim kiem ==================

        private void FilterChanged(object sender, EventArgs e) => ApplyFilter();

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboFilterStatus.SelectedIndex = 0;
            ApplyFilter();
        }

        /// <summary>Loc lai _view tu _all. keepId: giu lai dong co ma nay; keepCurrent: neu khong co keepId thi giu dong dang chon.</summary>
        private void ApplyFilter(string keepId = null, bool keepCurrent = true)
        {
            if (keepCurrent && keepId == null && bs.Current is Loan cur)
                keepId = cur.Id;

            string kw = txtSearch.Text.Trim();
            string status = cboFilterStatus.SelectedIndex > 0 ? (string)cboFilterStatus.SelectedItem : null;

            var result = _all
                .Where(l =>
                    (kw.Length == 0
                        || l.StudentId.IndexOf(kw, StringComparison.CurrentCultureIgnoreCase) >= 0
                        || l.Equipment.IndexOf(kw, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    && (status == null || l.Status == status))
                .OrderBy(l => l.BorrowDate)
                .ThenBy(l => l.Id)
                .ToList();

            _view.RaiseListChangedEvents = false;
            _view.Clear();
            foreach (var l in result) _view.Add(l);
            _view.RaiseListChangedEvents = true;
            _view.ResetBindings();

            if (keepId != null)
            {
                int idx = _view.ToList().FindIndex(l => l.Id == keepId);
                if (idx >= 0) bs.Position = idx;
            }

            UpdateStatusBar();
            LoadCurrent();
        }

        private void UpdateStatusBar()
        {
            int overdue = _all.Count(l => l.Status == Loan.Overdue);
            lblCount.Text = $"Tong so phieu: {_all.Count}  |  Qua han: {overdue}  |  Dang hien thi: {_view.Count}";
        }

        // ================== Chon dong -> nap len o nhap ==================

        private void bs_CurrentChanged(object sender, EventArgs e) => LoadCurrent();

        private void LoadCurrent()
        {
            if (bs.Current is Loan l)
            {
                _loading = true;
                txtId.Text = l.Id;
                txtStudentId.Text = l.StudentId;
                txtStudentName.Text = l.StudentName;
                cboEquipment.SelectedItem = l.Equipment;
                nudQty.Value = Math.Max(nudQty.Minimum, Math.Min(nudQty.Maximum, l.Quantity));
                dtpBorrow.Value = l.BorrowDate;
                dtpDue.Value = l.DueDate;
                dtpReturned.Value = l.ReturnedDate ?? DateTime.Today;   // dat Value truoc, Checked sau
                dtpReturned.Checked = l.ReturnedDate.HasValue;
                _loading = false;
                UpdateStatusPreview();
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
            txtStudentId.Clear();
            txtStudentName.Clear();
            cboEquipment.SelectedIndex = 0;
            nudQty.Value = 1;
            dtpBorrow.Value = DateTime.Today;
            dtpDue.Value = DateTime.Today.AddDays(7);
            dtpReturned.Value = DateTime.Today;
            dtpReturned.Checked = false;
            _loading = false;
            UpdateStatusPreview();
            ClearErrors();
        }

        private string NextId()
        {
            int max = 0;
            foreach (var l in _all)
            {
                var m = Regex.Match(l.Id ?? "", @"^PM(\d+)$", RegexOptions.IgnoreCase);
                if (m.Success && int.TryParse(m.Groups[1].Value, out int n) && n > max) max = n;
            }
            return "PM" + (max + 1).ToString("D3");
        }

        /// <summary>Hien tinh trang tinh tu cac o ngay dang nhap.</summary>
        private void UpdateStatusPreview()
        {
            DateTime? returned = dtpReturned.Checked ? dtpReturned.Value : (DateTime?)null;
            txtStatus.Text = Loan.GetStatus(returned, dtpDue.Value);
        }

        // ================== Kiem tra du lieu (ErrorProvider) ==================

        private void ClearErrors()
        {
            foreach (Control c in new Control[] { txtId, txtStudentId, txtStudentName, cboEquipment, nudQty, dtpBorrow, dtpDue, dtpReturned })
                errorProvider1.SetError(c, "");
        }

        private void ClearErrorOnEdit(object sender, EventArgs e)
        {
            if (_loading) return;
            if (sender is Control c) errorProvider1.SetError(c, "");
        }

        private void DateInputChanged(object sender, EventArgs e)
        {
            if (_loading) return;
            errorProvider1.SetError(dtpBorrow, "");
            errorProvider1.SetError(dtpDue, "");
            errorProvider1.SetError(dtpReturned, "");
            UpdateStatusPreview();
        }

        private bool Check(Control c, string error)
        {
            errorProvider1.SetError(c, error ?? "");
            return error == null;
        }

        /// <param name="editing">Phieu dang cap nhat (null neu them moi).</param>
        private bool ValidateInput(Loan editing)
        {
            string id = txtId.Text.Trim();

            string idErr = null;
            if (id.Length == 0)
                idErr = "Ma phieu khong duoc de trong.";
            else if (_all.Any(l => !ReferenceEquals(l, editing)
                                   && string.Equals(l.Id, id, StringComparison.OrdinalIgnoreCase)))
                idErr = "Ma phieu da ton tai.";

            string dueErr = dtpDue.Value.Date < dtpBorrow.Value.Date
                ? "Han tra khong duoc truoc ngay muon." : null;

            string returnedErr = dtpReturned.Checked && dtpReturned.Value.Date < dtpBorrow.Value.Date
                ? "Ngay tra thuc te khong duoc truoc ngay muon." : null;

            bool ok = true;
            ok &= Check(txtId, idErr);
            ok &= Check(txtStudentId, txtStudentId.Text.Trim().Length == 0 ? "Ma sinh vien khong duoc de trong." : null);
            ok &= Check(cboEquipment, cboEquipment.SelectedIndex < 0 ? "Vui long chon thiet bi." : null);
            ok &= Check(nudQty, nudQty.Value <= 0 ? "So luong phai lon hon 0." : null);
            ok &= Check(dtpDue, dueErr);
            ok &= Check(dtpReturned, returnedErr);
            return ok;
        }

        private void ReadInputsInto(Loan l)
        {
            l.Id = txtId.Text.Trim();
            l.StudentId = txtStudentId.Text.Trim();
            l.StudentName = txtStudentName.Text.Trim();
            l.Equipment = (string)cboEquipment.SelectedItem;
            l.Quantity = (int)nudQty.Value;
            l.BorrowDate = dtpBorrow.Value.Date;
            l.DueDate = dtpDue.Value.Date;
            l.ReturnedDate = dtpReturned.Checked ? dtpReturned.Value.Date : (DateTime?)null;
        }

        // ================== Them / Cap nhat / Xoa / Danh dau da tra ==================

        private Loan SelectedLoan()
        {
            if (dgv.SelectedRows.Count == 0) return null;
            return dgv.SelectedRows[0].DataBoundItem as Loan;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(null)) return;

            if (MessageBox.Show("Them phieu muon moi?", "Xac nhan",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var l = new Loan();
            ReadInputsInto(l);
            _all.Add(l);
            ApplyFilter(l.Id);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var cur = SelectedLoan();
            if (cur == null)
            {
                MessageBox.Show("Vui long chon mot phieu trong danh sach de cap nhat.", "Thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!ValidateInput(cur)) return;

            if (MessageBox.Show("Cap nhat phieu muon nay?", "Xac nhan",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            ReadInputsInto(cur);
            ApplyFilter(cur.Id);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var cur = SelectedLoan();
            if (cur == null)
            {
                MessageBox.Show("Vui long chon mot phieu trong danh sach de xoa.", "Thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"Xoa phieu {cur.Id} ({cur.Equipment}, SV {cur.StudentId})?", "Xac nhan xoa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            _all.Remove(cur);
            ApplyFilter(null, false);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            var cur = SelectedLoan();
            if (cur == null)
            {
                MessageBox.Show("Vui long chon mot phieu trong danh sach de danh dau da tra.", "Thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cur.ReturnedDate.HasValue)
            {
                MessageBox.Show("Phieu nay da duoc tra truoc do.", "Thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (DateTime.Today < cur.BorrowDate.Date)
            {
                MessageBox.Show("Ngay tra thuc te khong duoc truoc ngay muon.", "Thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Danh dau phieu {cur.Id} la da tra (ngay {DateTime.Today:dd/MM/yyyy})?", "Xac nhan",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            cur.ReturnedDate = DateTime.Today;
            ApplyFilter(cur.Id);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
            dgv.ClearSelection();
            txtId.Focus();
        }

        // ================== Xuat CSV (cac phieu dang loc) ==================

        private void mnuExport_Click(object sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                FileName = "phieu_muon_" + DateTime.Now.ToString("yyyyMMdd") + ".csv"
            };
            if (sfd.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("Ma phieu,Ma sinh vien,Ho ten,Thiet bi,So luong,Ngay muon,Han tra,Ngay tra thuc te,Tinh trang");
                foreach (var l in _view)
                {
                    sb.AppendLine(string.Join(",",
                        Csv(l.Id), Csv(l.StudentId), Csv(l.StudentName), Csv(l.Equipment),
                        l.Quantity.ToString(),
                        Csv(l.BorrowDate.ToString("dd/MM/yyyy")),
                        Csv(l.DueDate.ToString("dd/MM/yyyy")),
                        Csv(l.ReturnedDate.HasValue ? l.ReturnedDate.Value.ToString("dd/MM/yyyy") : ""),
                        Csv(l.Status)));
                }
                File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                MessageBox.Show($"Da xuat {_view.Count} phieu.", "Xuat CSV",
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