using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ClinicAppointmentManager
{
    public partial class MainForm : Form
    {
        private static readonly string[] Statuses = { "Cho kham", "Dang kham", "Hoan tat", "Da huy" };
        private static readonly string[] Doctors =
        {
            "BS. Nguyen Van An", "BS. Tran Thi Binh", "BS. Le Minh Chau", "BS. Pham Quoc Dung"
        };

        // So dien thoai VN: 0xxxxxxxxx (10 so) hoac +84xxxxxxxxx
        private static readonly Regex PhoneRegex = new Regex(@"^(0|\+84)\d{9}$");

        private readonly BindingList<Appointment> _all = new BindingList<Appointment>();   // toan bo du lieu
        private readonly BindingList<Appointment> _view = new BindingList<Appointment>();  // sau khi loc (gan vao BindingSource)
        private bool _loading;   // dang nap du lieu len o nhap -> bo qua su kien

        public MainForm()
        {
            InitializeComponent();
            BuildUi();
            bs.DataSource = _view;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            cboDoctor.Items.AddRange(Doctors);
            cboStatus.Items.AddRange(Statuses);
            cboFilterStatus.Items.Add("Tat ca");
            cboFilterStatus.Items.AddRange(Statuses);
            cboFilterStatus.SelectedIndex = 0;
            dtpFilter.Value = DateTime.Today;

            SeedData();
            ApplyFilter();
            if (_view.Count == 0) ClearInputs();
        }

        private void SeedData()
        {
            DateTime d = DateTime.Today;
            _all.Add(new Appointment { Id = "LH001", PatientName = "Nguyen Thi Lan", Phone = "0912345678", Time = d.AddHours(23), Doctor = Doctors[0], Reason = "Kham tong quat", Status = Statuses[0] });
            _all.Add(new Appointment { Id = "LH002", PatientName = "Tran Van Hung", Phone = "0987654321", Time = d.AddDays(1).AddHours(9), Doctor = Doctors[1], Reason = "Dau da day", Status = Statuses[0] });
            _all.Add(new Appointment { Id = "LH003", PatientName = "Le Thi Mai", Phone = "0345678901", Time = d.AddDays(1).AddHours(10).AddMinutes(30), Doctor = Doctors[2], Reason = "Tai kham", Status = Statuses[1] });
            _all.Add(new Appointment { Id = "LH004", PatientName = "Pham Duc Minh", Phone = "0933222111", Time = d.AddDays(2).AddHours(14), Doctor = Doctors[3], Reason = "Dau lung", Status = Statuses[2] });
            _all.Add(new Appointment { Id = "LH005", PatientName = "Hoang Thu Ha", Phone = "0966777888", Time = d.AddDays(3).AddHours(8), Doctor = Doctors[0], Reason = "Xet nghiem mau", Status = Statuses[3] });
        }

        // ================== Loc / tim kiem ==================

        private void FilterChanged(object sender, EventArgs e) => ApplyFilter();

        private void chkFilterDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpFilter.Enabled = chkFilterDate.Checked;
            ApplyFilter();
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            chkFilterDate.Checked = false;
            cboFilterStatus.SelectedIndex = 0;
            ApplyFilter();
        }

        /// <summary>Loc lai _view tu _all theo tu khoa, ngay, trang thai. keepId: giu lai dong dang chon.</summary>
        private void ApplyFilter(string keepId = null)
        {
            if (keepId == null && bs.Current is Appointment cur)
                keepId = cur.Id;

            string kw = txtSearch.Text.Trim();
            bool byDate = chkFilterDate.Checked;
            DateTime day = dtpFilter.Value.Date;
            bool byStatus = cboFilterStatus.SelectedIndex > 0;
            string status = cboFilterStatus.SelectedItem as string;

            var result = _all
                .Where(a =>
                    (kw.Length == 0
                        || a.PatientName.IndexOf(kw, StringComparison.CurrentCultureIgnoreCase) >= 0
                        || a.Phone.Contains(kw))
                    && (!byDate || a.Time.Date == day)
                    && (!byStatus || a.Status == status))
                .OrderBy(a => a.Time)
                .ToList();

            _view.RaiseListChangedEvents = false;
            _view.Clear();
            foreach (var a in result) _view.Add(a);
            _view.RaiseListChangedEvents = true;
            _view.ResetBindings();

            if (keepId != null)
            {
                int idx = _view.ToList().FindIndex(a => a.Id == keepId);
                if (idx >= 0) bs.Position = idx;
            }

            UpdateStatusBar();
            LoadCurrent();
        }

        private void UpdateStatusBar()
        {
            lblCount.Text = $"So lich hen: {_view.Count} (tong: {_all.Count})";
        }

        // ================== Chon dong -> nap len o nhap ==================

        private void bs_CurrentChanged(object sender, EventArgs e) => LoadCurrent();

        private void LoadCurrent()
        {
            if (bs.Current is Appointment a)
            {
                _loading = true;
                txtId.Text = a.Id;
                txtPatientName.Text = a.PatientName;
                txtPhone.Text = a.Phone;
                dtpTime.Value = a.Time;
                cboDoctor.SelectedItem = a.Doctor;
                cboStatus.SelectedItem = a.Status;
                txtReason.Text = a.Reason;
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
            txtPatientName.Clear();
            txtPhone.Clear();
            txtReason.Clear();
            dtpTime.Value = TruncateToMinute(DateTime.Now);
            cboDoctor.SelectedIndex = 0;
            cboStatus.SelectedIndex = 0;
            _loading = false;
            ClearErrors();
        }

        private string NextId()
        {
            int max = 0;
            foreach (var a in _all)
            {
                var m = Regex.Match(a.Id ?? "", @"^LH(\d+)$", RegexOptions.IgnoreCase);
                if (m.Success && int.TryParse(m.Groups[1].Value, out int n) && n > max) max = n;
            }
            return "LH" + (max + 1).ToString("D3");
        }

        private static DateTime TruncateToMinute(DateTime d) =>
            new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, 0);

        // ================== Kiem tra du lieu (ErrorProvider) ==================

        private void ClearErrors()
        {
            foreach (Control c in new Control[] { txtId, txtPatientName, txtPhone, dtpTime, cboDoctor, cboStatus, txtReason })
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

        /// <param name="editing">Dong dang cap nhat (null neu them moi).</param>
        private bool ValidateInput(Appointment editing)
        {
            string id = txtId.Text.Trim();
            string phone = txtPhone.Text.Trim();
            DateTime time = TruncateToMinute(dtpTime.Value);

            // Ma lich hen: bat buoc + duy nhat
            string idErr = null;
            if (id.Length == 0)
                idErr = "Ma lich hen khong duoc de trong.";
            else if (_all.Any(a => !ReferenceEquals(a, editing)
                                   && string.Equals(a.Id, id, StringComparison.OrdinalIgnoreCase)))
                idErr = "Ma lich hen da ton tai.";

            // Ngay hen khong trong qua khu (khi cap nhat chi kiem tra neu doi ngay gio)
            string timeErr = null;
            bool timeChanged = editing == null || TruncateToMinute(editing.Time) != time;
            if (timeChanged && time < TruncateToMinute(DateTime.Now))
                timeErr = "Ngay gio hen khong duoc nam trong qua khu.";

            bool ok = true;
            ok &= Check(txtId, idErr);
            ok &= Check(txtPatientName, txtPatientName.Text.Trim().Length == 0 ? "Vui long nhap ho ten benh nhan." : null);
            ok &= Check(txtPhone, phone.Length == 0 ? "Vui long nhap so dien thoai."
                                 : !PhoneRegex.IsMatch(phone) ? "So dien thoai khong hop le (vd: 0912345678)." : null);
            ok &= Check(dtpTime, timeErr);
            ok &= Check(cboDoctor, cboDoctor.SelectedIndex < 0 ? "Vui long chon bac si." : null);
            ok &= Check(cboStatus, cboStatus.SelectedIndex < 0 ? "Vui long chon trang thai." : null);
            ok &= Check(txtReason, txtReason.Text.Trim().Length == 0 ? "Vui long nhap ly do kham." : null);
            return ok;
        }

        private void ReadInputsInto(Appointment a)
        {
            a.Id = txtId.Text.Trim();
            a.PatientName = txtPatientName.Text.Trim();
            a.Phone = txtPhone.Text.Trim();
            a.Time = TruncateToMinute(dtpTime.Value);
            a.Doctor = (string)cboDoctor.SelectedItem;
            a.Status = (string)cboStatus.SelectedItem;
            a.Reason = txtReason.Text.Trim();
        }

        // ================== Them / Cap nhat / Xoa ==================

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(null)) return;

            if (MessageBox.Show("Them lich hen moi?", "Xac nhan",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var a = new Appointment();
            ReadInputsInto(a);
            _all.Add(a);
            ApplyFilter(a.Id);
        }

        private Appointment SelectedAppointment()
        {
            if (dgv.SelectedRows.Count == 0) return null;
            return dgv.SelectedRows[0].DataBoundItem as Appointment;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var cur = SelectedAppointment();
            if (cur == null)
            {
                MessageBox.Show("Vui long chon mot lich hen trong danh sach de cap nhat.", "Thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!ValidateInput(cur)) return;

            if (MessageBox.Show("Cap nhat lich hen nay?", "Xac nhan",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            ReadInputsInto(cur);
            ApplyFilter(cur.Id);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var cur = SelectedAppointment();
            if (cur == null)
            {
                MessageBox.Show("Vui long chon mot lich hen trong danh sach de xoa.", "Thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"Xoa lich hen {cur.Id} cua {cur.PatientName}?", "Xac nhan xoa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            _all.Remove(cur);
            ApplyFilter(keepIdNone);
        }

        private const string keepIdNone = "\0";   // khong giu dong nao sau khi xoa

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
            dgv.ClearSelection();
            txtId.Focus();
        }

        // ================== Xuat CSV ==================

        private void mnuExport_Click(object sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                FileName = "lich_hen_" + DateTime.Now.ToString("yyyyMMdd") + ".csv"
            };
            if (sfd.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("Ma lich hen,Ho ten,So dien thoai,Ngay gio hen,Bac si,Ly do kham,Trang thai");
                foreach (var a in _view)
                {
                    sb.AppendLine(string.Join(",",
                        Csv(a.Id), Csv(a.PatientName), Csv(a.Phone),
                        Csv(a.Time.ToString("dd/MM/yyyy HH:mm")),
                        Csv(a.Doctor), Csv(a.Reason), Csv(a.Status)));
                }
                File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                MessageBox.Show($"Da xuat {_view.Count} lich hen.", "Xuat CSV",
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