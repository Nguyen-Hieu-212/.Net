using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClinicAppointmentManager
{
    partial class MainForm
    {
        // ----- Controls -----
        private BindingSource bs;
        private ErrorProvider errorProvider1;

        private MenuStrip menuStrip1;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuExport;
        private ToolStripMenuItem mnuExit;

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblCount;

        private TableLayoutPanel tlpRoot;
        private TableLayoutPanel tlpInput;
        private TableLayoutPanel tlpFilter;
        private FlowLayoutPanel flpButtons;

        private TextBox txtId, txtPatientName, txtPhone, txtReason, txtSearch;
        private DateTimePicker dtpTime, dtpFilter;
        private ComboBox cboDoctor, cboStatus, cboFilterStatus;
        private CheckBox chkFilterDate;
        private Button btnAdd, btnUpdate, btnDelete, btnClear, btnClearFilter;
        private DataGridView dgv;

        private static Label MakeLabel(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(6, 6, 6, 6)
            };
        }

        private void BuildUi()
        {
            bs = new BindingSource(components);
            errorProvider1 = new ErrorProvider(components);
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            // ----- Input controls -----
            txtId = new TextBox { Dock = DockStyle.Fill, Margin = new Padding(3, 4, 3, 4) };
            txtPatientName = new TextBox { Dock = DockStyle.Fill, Margin = new Padding(3, 4, 3, 4) };
            txtPhone = new TextBox { Dock = DockStyle.Fill, Margin = new Padding(3, 4, 3, 4), MaxLength = 15 };
            txtReason = new TextBox { Dock = DockStyle.Fill, Margin = new Padding(3, 4, 3, 4) };

            dtpTime = new DateTimePicker
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(3, 4, 3, 4),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy HH:mm"
            };
            cboDoctor = new ComboBox { Dock = DockStyle.Fill, Margin = new Padding(3, 4, 3, 4), DropDownStyle = ComboBoxStyle.DropDownList };
            cboStatus = new ComboBox { Dock = DockStyle.Fill, Margin = new Padding(3, 4, 3, 4), DropDownStyle = ComboBoxStyle.DropDownList };

            btnAdd = new Button { Text = "Them", AutoSize = true, MinimumSize = new Size(90, 30) };
            btnUpdate = new Button { Text = "Cap nhat", AutoSize = true, MinimumSize = new Size(90, 30) };
            btnDelete = new Button { Text = "Xoa", AutoSize = true, MinimumSize = new Size(90, 30) };
            btnClear = new Button { Text = "Lam moi", AutoSize = true, MinimumSize = new Size(90, 30) };
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnClear.Click += btnClear_Click;

            flpButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true
            };
            flpButtons.Controls.AddRange(new Control[] { btnAdd, btnUpdate, btnDelete, btnClear });

            // ----- Input TableLayoutPanel (4 cot: nhan | o nhap | nhan | o nhap) -----
            tlpInput = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 4,
                RowCount = 5,
                Padding = new Padding(8)
            };
            tlpInput.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpInput.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            for (int i = 0; i < 5; i++)
                tlpInput.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            tlpInput.Controls.Add(MakeLabel("Ma lich hen:"), 0, 0);
            tlpInput.Controls.Add(txtId, 1, 0);
            tlpInput.Controls.Add(MakeLabel("Ho ten benh nhan:"), 2, 0);
            tlpInput.Controls.Add(txtPatientName, 3, 0);

            tlpInput.Controls.Add(MakeLabel("So dien thoai:"), 0, 1);
            tlpInput.Controls.Add(txtPhone, 1, 1);
            tlpInput.Controls.Add(MakeLabel("Ngay gio hen:"), 2, 1);
            tlpInput.Controls.Add(dtpTime, 3, 1);

            tlpInput.Controls.Add(MakeLabel("Bac si:"), 0, 2);
            tlpInput.Controls.Add(cboDoctor, 1, 2);
            tlpInput.Controls.Add(MakeLabel("Trang thai:"), 2, 2);
            tlpInput.Controls.Add(cboStatus, 3, 2);

            tlpInput.Controls.Add(MakeLabel("Ly do kham:"), 0, 3);
            tlpInput.Controls.Add(txtReason, 1, 3);
            tlpInput.SetColumnSpan(txtReason, 3);

            tlpInput.Controls.Add(flpButtons, 1, 4);
            tlpInput.SetColumnSpan(flpButtons, 3);

            // ----- Filter bar -----
            txtSearch = new TextBox { Dock = DockStyle.Fill, Margin = new Padding(3, 4, 3, 4) };
            chkFilterDate = new CheckBox { Text = "Loc ngay hen:", AutoSize = true, Anchor = AnchorStyles.Left, Margin = new Padding(6, 6, 3, 6) };
            dtpFilter = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Width = 110,
                Enabled = false,
                Margin = new Padding(3, 4, 3, 4)
            };
            cboFilterStatus = new ComboBox { Width = 120, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(3, 4, 3, 4) };
            btnClearFilter = new Button { Text = "Bo loc", AutoSize = true, MinimumSize = new Size(80, 28) };

            txtSearch.TextChanged += FilterChanged;
            chkFilterDate.CheckedChanged += chkFilterDate_CheckedChanged;
            dtpFilter.ValueChanged += FilterChanged;
            cboFilterStatus.SelectedIndexChanged += FilterChanged;
            btnClearFilter.Click += btnClearFilter_Click;

            tlpFilter = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 7,
                RowCount = 1,
                Padding = new Padding(8, 0, 8, 4)
            };
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));          // nhan Tim kiem
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));     // o tim kiem gian ra
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpFilter.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFilter.Controls.Add(MakeLabel("Tim (ten / SDT):"), 0, 0);
            tlpFilter.Controls.Add(txtSearch, 1, 0);
            tlpFilter.Controls.Add(chkFilterDate, 2, 0);
            tlpFilter.Controls.Add(dtpFilter, 3, 0);
            tlpFilter.Controls.Add(MakeLabel("Trang thai:"), 4, 0);
            tlpFilter.Controls.Add(cboFilterStatus, 5, 0);
            tlpFilter.Controls.Add(btnClearFilter, 6, 0);

            // ----- DataGridView -----
            dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = SystemColors.Window
            };
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "Ma LH", DataPropertyName = "Id", FillWeight = 10 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colName", HeaderText = "Benh nhan", DataPropertyName = "PatientName", FillWeight = 18 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPhone", HeaderText = "So dien thoai", DataPropertyName = "Phone", FillWeight = 13 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTime",
                HeaderText = "Ngay gio hen",
                DataPropertyName = "Time",
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDoctor", HeaderText = "Bac si", DataPropertyName = "Doctor", FillWeight = 16 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colReason", HeaderText = "Ly do kham", DataPropertyName = "Reason", FillWeight = 20 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStatus", HeaderText = "Trang thai", DataPropertyName = "Status", FillWeight = 10 });

            // ----- Root TableLayoutPanel: nhap lieu / loc / luoi -----
            tlpRoot = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3
            };
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRoot.Controls.Add(tlpInput, 0, 0);
            tlpRoot.Controls.Add(tlpFilter, 0, 1);
            tlpRoot.Controls.Add(dgv, 0, 2);

            // ----- MenuStrip -----
            menuStrip1 = new MenuStrip();
            mnuFile = new ToolStripMenuItem("&Tep");
            mnuExport = new ToolStripMenuItem("&Xuat CSV...") { ShortcutKeys = Keys.Control | Keys.E };
            mnuExit = new ToolStripMenuItem("T&hoat");
            mnuExport.Click += mnuExport_Click;
            mnuExit.Click += (s, e) => Close();
            mnuFile.DropDownItems.AddRange(new ToolStripItem[] { mnuExport, new ToolStripSeparator(), mnuExit });
            menuStrip1.Items.Add(mnuFile);

            // ----- StatusStrip -----
            statusStrip1 = new StatusStrip();
            lblCount = new ToolStripStatusLabel("So lich hen: 0");
            statusStrip1.Items.Add(lblCount);

            // ----- Form -----
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 640);
            MinimumSize = new Size(820, 520);
            Text = "Clinic Appointment Manager";
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(tlpRoot);      // Fill: them truoc
            Controls.Add(statusStrip1); // Bottom
            Controls.Add(menuStrip1);   // Top
            MainMenuStrip = menuStrip1;

            // ----- BindingSource + grid events -----
            dgv.DataSource = bs;
            bs.CurrentChanged += bs_CurrentChanged;
            txtId.TextChanged += ClearErrorOnEdit;
            txtPatientName.TextChanged += ClearErrorOnEdit;
            txtPhone.TextChanged += ClearErrorOnEdit;
            txtReason.TextChanged += ClearErrorOnEdit;
            dtpTime.ValueChanged += ClearErrorOnEdit;

            Load += MainForm_Load;
        }
    }
}