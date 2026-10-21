using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MdiApp
{
    // MDI Child: Quản lý Sinh viên (chính là bài Student: BindingSource, Avatar, CSV)
    public class FormStudent : BaseChildForm
    {
        // Dấu phân cách CSV: "," hoặc ";" (Excel tiếng Việt thường dùng ";")
        private const string CsvDelimiter = ",";

        // BindingSource làm trung gian giữa BindingList<Student> và các control
        private readonly BindingSource _bs = new BindingSource();

        private DataGridView dgvStudents = null!;
        private GroupBox grpInput = null!;
        private TextBox txtId = null!, txtName = null!, txtAge = null!;
        private ComboBox cboGender = null!;
        private PictureBox picAvatar = null!;
        private Button btnChooseImage = null!, btnAdd = null!, btnDelete = null!, btnUpdate = null!, btnExportCsv = null!;
        private readonly ErrorProvider _err = new ErrorProvider();
        private readonly ContextMenuStrip ctx = new ContextMenuStrip();

        // Async + ProgressBar
        private readonly Button btnLoad = new Button { Text = "Nạp dữ liệu (Async)", Left = 15, Top = 8, Width = 180, Height = 30 };
        private readonly ProgressBar progressBar = new ProgressBar { Left = 210, Top = 12, Width = 400, Height = 22 };
        private readonly Label lblPercent = new Label { Text = "0%", Left = 620, Top = 14, AutoSize = true };

        private string _loadedAvatarPath = "";

        public FormStudent()
        {
            InitializeUI();
            SetupBindingSource();
            UpdateInputState();

            FormClosed += (s, e) =>
            {
                // Gỡ binding để không giữ tham chiếu tới form đã đóng
                txtId.DataBindings.Clear();
                txtName.DataBindings.Clear();
                txtAge.DataBindings.Clear();
                cboGender.DataBindings.Clear();
                dgvStudents.DataSource = null;
                _bs.Dispose();
                picAvatar.Image?.Dispose();
            };
        }

        // ================= GIAO DIỆN =================
        private void InitializeUI()
        {
            Text = "Quản lý Sinh viên";
            Size = new Size(940, 620);
            Font = new Font("Segoe UI", 10F);

            // --- DataGridView: danh sách sinh viên ---
            dgvStudents = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AutoGenerateColumns = true,
                BackgroundColor = Color.White,
                RowHeadersVisible = false
            };

            // --- ContextMenuStrip cho DataGridView ---
            ctx.Items.Add(new ToolStripMenuItem("Xóa dòng", null, (s, e) => DeleteCurrent()));
            ctx.Items.Add(new ToolStripMenuItem("Làm mới dữ liệu", null, (s, e) => RefreshData()));
            ctx.Items.Add(new ToolStripMenuItem("Xem chi tiết", null, (s, e) => ShowDetail()));
            dgvStudents.ContextMenuStrip = ctx;

            // Chuột phải vào dòng nào thì chọn dòng đó
            dgvStudents.CellMouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                    _bs.Position = e.RowIndex;
            };

            // --- Cụm nhập liệu ---
            grpInput = new GroupBox { Text = "Thông tin sinh viên", Dock = DockStyle.Bottom, Height = 250 };

            var lblId = new Label { Text = "Mã SV:", Left = 20, Top = 35, AutoSize = true };
            txtId = new TextBox { Left = 100, Top = 32, Width = 200 };

            var lblName = new Label { Text = "Họ tên:", Left = 20, Top = 75, AutoSize = true };
            txtName = new TextBox { Left = 100, Top = 72, Width = 200 };

            var lblAge = new Label { Text = "Tuổi:", Left = 20, Top = 115, AutoSize = true };
            txtAge = new TextBox { Left = 100, Top = 112, Width = 200 };

            var lblGender = new Label { Text = "Giới tính:", Left = 20, Top = 155, AutoSize = true };
            cboGender = new ComboBox { Left = 100, Top = 152, Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cboGender.Items.AddRange(new object[] { "Nam", "Nữ" });

            // PictureBox hiển thị ảnh Avatar + nút Chọn ảnh
            picAvatar = new PictureBox
            {
                Left = 340,
                Top = 30,
                Width = 150,
                Height = 170,
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            btnChooseImage = new Button { Text = "Chọn ảnh", Left = 340, Top = 206, Width = 150, Height = 30 };
            btnChooseImage.Click += BtnChooseImage_Click;

            // --- Cụm chức năng: Thêm, Xóa, Cập nhật, Xuất file CSV ---
            btnAdd = new Button { Text = "Thêm", Left = 540, Top = 30, Width = 160, Height = 38 };
            btnDelete = new Button { Text = "Xóa", Left = 540, Top = 80, Width = 160, Height = 38 };
            btnUpdate = new Button { Text = "Cập nhật", Left = 540, Top = 130, Width = 160, Height = 38 };
            btnExportCsv = new Button { Text = "Xuất file CSV", Left = 540, Top = 180, Width = 160, Height = 38 };
            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += (s, e) => DeleteCurrent();
            btnUpdate.Click += BtnUpdate_Click;
            btnExportCsv.Click += BtnExportCsv_Click;

            grpInput.Controls.AddRange(new Control[]
            {
                lblId, txtId, lblName, txtName, lblAge, txtAge, lblGender, cboGender,
                picAvatar, btnChooseImage, btnAdd, btnDelete, btnUpdate, btnExportCsv
            });

            Controls.Add(dgvStudents);
            Controls.Add(grpInput);

            // Thanh nạp dữ liệu bất đồng bộ: Button + ProgressBar + Label %
            var pnlLoad = new Panel { Dock = DockStyle.Top, Height = 46 };
            pnlLoad.Controls.AddRange(new Control[] { btnLoad, progressBar, lblPercent });
            Controls.Add(pnlLoad);
            btnLoad.Click += async (s, e) => await LoadDataAsync();
        }

        // ================= BINDINGSOURCE (2 chiều) =================
        private void SetupBindingSource()
        {
            _bs.DataSource = DataStore.Students;
            dgvStudents.DataSource = _bs;

            dgvStudents.Columns[nameof(Student.StudentId)].HeaderText = "Mã SV";
            dgvStudents.Columns[nameof(Student.FullName)].HeaderText = "Họ tên";
            dgvStudents.Columns[nameof(Student.Age)].HeaderText = "Tuổi";
            dgvStudents.Columns[nameof(Student.Gender)].HeaderText = "Giới tính";
            dgvStudents.Columns[nameof(Student.AvatarPath)].HeaderText = "Đường dẫn ảnh";

            // Đồng bộ dữ liệu giữa các Control nhập liệu và dòng đang chọn (DataBindings.Add)
            txtId.DataBindings.Add("Text", _bs, nameof(Student.StudentId), false, DataSourceUpdateMode.OnPropertyChanged);
            txtName.DataBindings.Add("Text", _bs, nameof(Student.FullName), false, DataSourceUpdateMode.OnPropertyChanged);
            var ageBinding = txtAge.DataBindings.Add("Text", _bs, nameof(Student.Age), true, DataSourceUpdateMode.OnPropertyChanged);
            cboGender.DataBindings.Add("SelectedItem", _bs, nameof(Student.Gender), true, DataSourceUpdateMode.OnPropertyChanged);

            ageBinding.BindingComplete += (s, e) =>
            {
                if (e.BindingCompleteState != BindingCompleteState.Success)
                    _err.SetError(txtAge, "Tuổi phải là số nguyên");
                else
                    _err.SetError(txtAge, "");
            };

            // Khi chọn dòng khác (hoặc đường dẫn ảnh đổi) -> PictureBox tự nạp ảnh từ AvatarPath
            _bs.CurrentChanged += (s, e) => LoadAvatar();
            _bs.CurrentItemChanged += (s, e) => LoadAvatar();
            _bs.ListChanged += (s, e) => UpdateInputState();

            LoadAvatar();
        }

        // ================= ẢNH AVATAR =================
        private void LoadAvatar()
        {
            string path = (_bs.Current as Student)?.AvatarPath ?? "";
            if (path == _loadedAvatarPath) return;
            _loadedAvatarPath = path;

            var old = picAvatar.Image;
            picAvatar.Image = null;
            old?.Dispose();

            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path)) return;

            try
            {
                // Đọc qua stream rồi copy để không khóa file ảnh
                using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                using var img = Image.FromStream(fs);
                picAvatar.Image = new Bitmap(img);
            }
            catch
            {
                picAvatar.Image = null;
            }
        }

        private void UpdateInputState()
        {
            bool has = _bs.Count > 0;
            txtId.Enabled = txtName.Enabled = txtAge.Enabled = cboGender.Enabled = has;
            btnChooseImage.Enabled = btnDelete.Enabled = btnUpdate.Enabled = has;
            btnExportCsv.Enabled = has;
            if (!has)
            {
                _loadedAvatarPath = "";
                picAvatar.Image?.Dispose();
                picAvatar.Image = null;
            }
        }

        // ================= CHỌN ẢNH (OpenFileDialog) =================
        private void BtnChooseImage_Click(object? sender, EventArgs e)
        {
            if (_bs.Current is not Student current) return;

            using var ofd = new OpenFileDialog
            {
                Title = "Chọn ảnh đại diện",
                Filter = "Ảnh (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                current.AvatarPath = ofd.FileName;
                LoadAvatar();
                dgvStudents.Refresh();
                Notify($"Đã chọn ảnh đại diện cho sinh viên \"{current.FullName}\".");
            }
        }

        // ================= THÊM / XÓA / CẬP NHẬT =================
        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            var st = new Student { Gender = "Nam", Age = 18 };
            DataStore.Students.Add(st);
            _bs.Position = _bs.IndexOf(st);
            txtId.Focus();
            Notify("Đã thêm dòng sinh viên mới, hãy nhập thông tin rồi bấm Cập nhật.");
        }

        private void DeleteCurrent()
        {
            if (_bs.Current is not Student st) return;

            var rs = MessageBox.Show($"Xóa sinh viên \"{st.FullName}\"?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs != DialogResult.Yes) return;

            _bs.RemoveCurrent();
            // Truyền thông điệp lên StatusStrip của FormMain
            Notify($"Xóa sinh viên \"{st.FullName}\" thành công.");
        }

        private void BtnUpdate_Click(object? sender, EventArgs e)
        {
            if (_bs.Current is not Student st) return;

            Validate();
            _bs.EndEdit();

            if (string.IsNullOrWhiteSpace(st.StudentId))
            { MessageBox.Show("Mã SV không được để trống."); txtId.Focus(); return; }

            if (string.IsNullOrWhiteSpace(st.FullName))
            { MessageBox.Show("Họ tên không được để trống."); txtName.Focus(); return; }

            if (st.Age <= 0 || st.Age > 120)
            { MessageBox.Show("Tuổi phải từ 1 đến 120."); txtAge.Focus(); return; }

            if (DataStore.Students.Any(x => !ReferenceEquals(x, st) &&
                    string.Equals(x.StudentId, st.StudentId, StringComparison.OrdinalIgnoreCase)))
            { MessageBox.Show("Mã SV đã tồn tại."); txtId.Focus(); return; }

            dgvStudents.Refresh();
            MessageBox.Show("Cập nhật thành công!", "Thông báo");
            Notify($"Cập nhật sinh viên \"{st.FullName}\" thành công.");
        }

        // ================= CONTEXT MENU =================
        private void ShowDetail()
        {
            if (_bs.Current is not Student st) return;

            MessageBox.Show(
                $"Mã SV: {st.StudentId}\nHọ tên: {st.FullName}\nTuổi: {st.Age}\nGiới tính: {st.Gender}\nẢnh: {(string.IsNullOrEmpty(st.AvatarPath) ? "(chưa có)" : st.AvatarPath)}",
                "Chi tiết sinh viên", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Notify($"Đã xem chi tiết sinh viên \"{st.FullName}\".");
        }

        // Được gọi từ ContextMenu hoặc từ Form cha (nút Làm mới trên ToolStrip)
        public override void RefreshData()
        {
            _bs.ResetBindings(false);
            dgvStudents.Refresh();
            _loadedAvatarPath = "\0"; // ép nạp lại ảnh
            LoadAvatar();
            Notify("Đã làm mới danh sách sinh viên.");
        }

        // ================= ASYNC / AWAIT + PROGRESSBAR =================
        // Nạp dữ liệu dạng async Task<List<T>>: dùng Task.Run + Thread.Sleep để giả lập tác vụ nặng (CSDL/File)
        private async Task<List<Student>> LoadStudentsAsync(IProgress<int> progress)
        {
            return await Task.Run(() =>
            {
                const int total = 100;
                var result = new List<Student>();
                for (int i = 1; i <= total; i++)
                {
                    Thread.Sleep(40); // giả lập tốn thời gian
                    result.Add(new Student
                    {
                        StudentId = $"SV{i:000}",
                        FullName = $"Sinh viên {i}",
                        Age = 18 + i % 8,
                        Gender = i % 2 == 0 ? "Nữ" : "Nam"
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
            Notify("Bắt đầu nạp danh sách sinh viên...");

            try
            {
                // Progress<int> tạo trên UI Thread nên callback luôn chạy trên UI Thread
                var progress = new Progress<int>(p =>
                {
                    if (IsDisposed) return;
                    progressBar.Value = p;
                    lblPercent.Text = p + "%";
                    ReportProgress(p); // cập nhật % lên StatusStrip của FormMain
                });

                var data = await LoadStudentsAsync(progress);

                DataStore.Students.RaiseListChangedEvents = false;
                DataStore.Students.Clear();
                foreach (var s in data) DataStore.Students.Add(s);
                DataStore.Students.RaiseListChangedEvents = true;
                DataStore.Students.ResetBindings();

                ReportProgress(100);
                Notify($"Nạp xong {data.Count} sinh viên.");
                if (!IsDisposed)
                    MessageBox.Show($"Nạp xong {data.Count} sinh viên!", "Thông báo");
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

        // ================= XUẤT CSV (SaveFileDialog) =================
        private void BtnExportCsv_Click(object? sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog
            {
                Title = "Xuất danh sách sinh viên",
                Filter = "CSV (*.csv)|*.csv",
                FileName = "DanhSachSinhVien.csv"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                var sb = new StringBuilder();
                sb.AppendLine(string.Join(CsvDelimiter, "StudentId", "FullName", "Age", "Gender", "AvatarPath"));

                foreach (var s in DataStore.Students)
                {
                    sb.AppendLine(string.Join(CsvDelimiter,
                        Csv(s.StudentId), Csv(s.FullName), s.Age.ToString(), Csv(s.Gender), Csv(s.AvatarPath)));
                }

                // UTF-8 có BOM để Excel đọc đúng tiếng Việt
                File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                MessageBox.Show("Xuất file CSV thành công!", "Thông báo");
                Notify($"Xuất file CSV thành công: {Path.GetFileName(sfd.FileName)}.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string Csv(string? value)
        {
            value ??= "";
            if (value.Contains(CsvDelimiter) || value.Contains('"') || value.Contains('\n') || value.Contains('\r'))
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            return value;
        }
    }
}