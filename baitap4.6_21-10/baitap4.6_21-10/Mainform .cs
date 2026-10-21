using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StudentManager
{
    public class MainForm : Form
    {
        // Dấu phân cách CSV: "," hoặc ";" (Excel tiếng Việt thường dùng ";")
        private const string CsvDelimiter = ",";

        // ===== Dữ liệu & BindingSource =====
        private readonly BindingList<Student> _students = new BindingList<Student>();
        private readonly BindingSource _bs = new BindingSource();

        // ===== Controls =====
        private DataGridView dgvStudents = null!;
        private GroupBox grpInput = null!;
        private TextBox txtId = null!, txtName = null!, txtAge = null!;
        private ComboBox cboGender = null!;
        private PictureBox picAvatar = null!;
        private Button btnChooseImage = null!, btnAdd = null!, btnDelete = null!, btnUpdate = null!, btnExportCsv = null!;
        private ErrorProvider _err = new ErrorProvider();

        private string _loadedAvatarPath = "";

        public MainForm()
        {
            InitializeUI();
            SetupBindingSource();
            UpdateInputState();
        }

        // ================= GIAO DIỆN =================
        private void InitializeUI()
        {
            Text = "Quản lý Sinh viên - BindingSource & Xử lý File";
            Size = new Size(960, 640);
            StartPosition = FormStartPosition.CenterScreen;
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
            btnDelete.Click += BtnDelete_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnExportCsv.Click += BtnExportCsv_Click;

            grpInput.Controls.AddRange(new Control[]
            {
                lblId, txtId, lblName, txtName, lblAge, txtAge, lblGender, cboGender,
                picAvatar, btnChooseImage, btnAdd, btnDelete, btnUpdate, btnExportCsv
            });

            Controls.Add(dgvStudents);
            Controls.Add(grpInput);
        }

        // ================= BINDINGSOURCE (2 chiều) =================
        private void SetupBindingSource()
        {
            // BindingSource làm trung gian giữa BindingList<Student> và các control
            _bs.DataSource = _students;
            dgvStudents.DataSource = _bs;

            // Đổi tên cột cho thân thiện
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

            // Báo lỗi nếu nhập tuổi không phải số
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
                // Gán đường dẫn vào sinh viên đang chọn -> hiển thị lên PictureBox
                current.AvatarPath = ofd.FileName;
                LoadAvatar();
                dgvStudents.Refresh();
            }
        }

        // ================= THÊM / XÓA / CẬP NHẬT =================
        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            // Thêm sinh viên mới, chọn dòng đó để nhập thông tin
            var st = new Student { Gender = "Nam", Age = 18 };
            _students.Add(st);
            _bs.Position = _bs.IndexOf(st);
            txtId.Focus();
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (_bs.Current is not Student st) return;

            var rs = MessageBox.Show($"Xóa sinh viên \"{st.FullName}\"?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
                _bs.RemoveCurrent();
        }

        private void BtnUpdate_Click(object? sender, EventArgs e)
        {
            if (_bs.Current is not Student st) return;

            // Đẩy dữ liệu từ control về đối tượng
            Validate();
            _bs.EndEdit();

            if (string.IsNullOrWhiteSpace(st.StudentId))
            { MessageBox.Show("Mã SV không được để trống."); txtId.Focus(); return; }

            if (string.IsNullOrWhiteSpace(st.FullName))
            { MessageBox.Show("Họ tên không được để trống."); txtName.Focus(); return; }

            if (st.Age <= 0 || st.Age > 120)
            { MessageBox.Show("Tuổi phải từ 1 đến 120."); txtAge.Focus(); return; }

            if (_students.Any(x => !ReferenceEquals(x, st) &&
                    string.Equals(x.StudentId, st.StudentId, StringComparison.OrdinalIgnoreCase)))
            { MessageBox.Show("Mã SV đã tồn tại."); txtId.Focus(); return; }

            dgvStudents.Refresh();
            MessageBox.Show("Cập nhật thành công!", "Thông báo");
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

                foreach (var s in _students)
                {
                    sb.AppendLine(string.Join(CsvDelimiter,
                        Csv(s.StudentId), Csv(s.FullName), s.Age.ToString(), Csv(s.Gender), Csv(s.AvatarPath)));
                }

                // UTF-8 có BOM để Excel đọc đúng tiếng Việt
                File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                MessageBox.Show("Xuất file CSV thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Escape giá trị CSV (có dấu phân cách, nháy kép, xuống dòng)
        private static string Csv(string? value)
        {
            value ??= "";
            if (value.Contains(CsvDelimiter) || value.Contains('"') || value.Contains('\n') || value.Contains('\r'))
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            return value;
        }
    }
}