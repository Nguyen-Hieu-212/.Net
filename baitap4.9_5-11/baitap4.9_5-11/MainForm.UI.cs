using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace LabEquipmentCheckout
{
    partial class MainForm
    {
        // ----- Controls -----
        private BindingSource bs;
        private ErrorProvider errorProvider1;

        private MenuStrip menuStrip1;
        private ToolStripMenuItem mnuFile, mnuExport, mnuExit;

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblCount;

        private TableLayoutPanel tlpRoot, tlpInput, tlpFilter;
        private FlowLayoutPanel flpButtons;

        private TextBox txtId, txtStudentId, txtStudentName, txtStatus, txtSearch;
        private ComboBox cboEquipment, cboFilterStatus;
        private NumericUpDown nudQty;
        private DateTimePicker dtpBorrow, dtpDue, dtpReturned;
        private Button btnAdd, btnUpdate, btnDelete, btnReturn, btnClear, btnClearFilter;
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

        private static DateTimePicker MakeDatePicker()
        {
            return new DateTimePicker
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(3, 4, 3, 4),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy"
            };
        }

        private static Button MakeButton(string text)
        {
            return new Button { Text = text, AutoSize = true, MinimumSize = new Size(90, 30) };
        }

        private void BuildUi()
        {
            bs = new BindingSource(components);
            errorProvider1 = new ErrorProvider(components);
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            // ----- Input controls -----
            txtId = new TextBox { Dock = DockStyle.Fill, Margin = new Padding(3, 4, 3, 4) };
            txtStudentId = new TextBox { Dock = DockStyle.Fill, Margin = new Padding(3, 4, 3, 4) };
            txtStudentName = new TextBox { Dock = DockStyle.Fill, Margin = new Padding(3, 4, 3, 4) };
            txtStatus = new TextBox { Dock = DockStyle.Fill, Margin = new Padding(3, 4, 3, 4), ReadOnly = true, TabStop = false };

            cboEquipment = new ComboBox { Dock = DockStyle.Fill, Margin = new Padding(3, 4, 3, 4), DropDownStyle = ComboBoxStyle.DropDownList };
            nudQty = new NumericUpDown { Dock = DockStyle.Fill, Margin = new Padding(3, 4, 3, 4), Minimum = 0, Maximum = 1000, Value = 1 };

            dtpBorrow = MakeDatePicker();
            dtpDue = MakeDatePicker();
            dtpReturned = MakeDatePicker();
            dtpReturned.ShowCheckBox = true;   // bo tick = chua tra
            dtpReturned.Checked = false;

            btnAdd = MakeButton("Them");
            btnUpdate = MakeButton("Cap nhat");
            btnDelete = MakeButton("Xoa");
            btnReturn = MakeButton("Danh dau da tra");
            btnClear = MakeButton("Lam moi");
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnReturn.Click += btnReturn_Click;
            btnClear.Click += btnClear_Click;

            flpButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true
            };
            flpButtons.Controls.AddRange(new Control[] { btnAdd, btnUpdate, btnDelete, btnReturn, btnClear });

            // ----- Input TableLayoutPanel (4 cot: nhan | o nhap | nhan | o nhap) -----
            tlpInput = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 4,
                RowCount = 6,
                Padding = new Padding(8)
            };
            tlpInput.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpInput.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            for (int i = 0; i < 6; i++)
                tlpInput.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            tlpInput.Controls.Add(MakeLabel("Ma phieu:"), 0, 0);
            tlpInput.Controls.Add(txtId, 1, 0);
            tlpInput.Controls.Add(MakeLabel("Ma sinh vien:"), 2, 0);
            tlpInput.Controls.Add(txtStudentId, 3, 0);

            tlpInput.Controls.Add(MakeLabel("Ho ten sinh vien:"), 0, 1);
            tlpInput.Controls.Add(txtStudentName, 1, 1);
            tlpInput.Controls.Add(MakeLabel("Thiet bi:"), 2, 1);
            tlpInput.Controls.Add(cboEquipment, 3, 1);

            tlpInput.Controls.Add(MakeLabel("So luong:"), 0, 2);
            tlpInput.Controls.Add(nudQty, 1, 2);
            tlpInput.Controls.Add(MakeLabel("Tinh trang:"), 2, 2);
            tlpInput.Controls.Add(txtStatus, 3, 2);

            tlpInput.Controls.Add(MakeLabel("Ngay muon:"), 0, 3);
            tlpInput.Controls.Add(dtpBorrow, 1, 3);
            tlpInput.Controls.Add(MakeLabel("Han tra:"), 2, 3);
            tlpInput.Controls.Add(dtpDue, 3, 3);

            tlpInput.Controls.Add(MakeLabel("Ngay tra thuc te:"), 0, 4);
            tlpInput.Controls.Add(dtpReturned, 1, 4);

            tlpInput.Controls.Add(flpButtons, 1, 5);
            tlpInput.SetColumnSpan(flpButtons, 3);

            // ----- Filter bar -----
            txtSearch = new TextBox { Dock = DockStyle.Fill, Margin = new Padding(3, 4, 3, 4) };
            cboFilterStatus = new ComboBox { Width = 130, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(3, 4, 3, 4) };
            btnClearFilter = new Button { Text = "Bo loc", AutoSize = true, MinimumSize = new Size(80, 28) };

            txtSearch.TextChanged += FilterChanged;
            cboFilterStatus.SelectedIndexChanged += FilterChanged;
            btnClearFilter.Click += btnClearFilter_Click;

            tlpFilter = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 5,
                RowCount = 1,
                Padding = new Padding(8, 0, 8, 4)
            };
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));   // o tim kiem gian ra
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpFilter.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFilter.Controls.Add(MakeLabel("Tim (ma SV / thiet bi):"), 0, 0);
            tlpFilter.Controls.Add(txtSearch, 1, 0);
            tlpFilter.Controls.Add(MakeLabel("Loc:"), 2, 0);
            tlpFilter.Controls.Add(cboFilterStatus, 3, 0);
            tlpFilter.Controls.Add(btnClearFilter, 4, 0);

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
            var dateStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" };
            var returnedStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy", NullValue = "" };

            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "Ma phieu", DataPropertyName = "Id", FillWeight = 9 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStudentId", HeaderText = "Ma SV", DataPropertyName = "StudentId", FillWeight = 10 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStudentName", HeaderText = "Ho ten", DataPropertyName = "StudentName", FillWeight = 15 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEquipment", HeaderText = "Thiet bi", DataPropertyName = "Equipment", FillWeight = 20 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colQty", HeaderText = "SL", DataPropertyName = "Quantity", FillWeight = 5 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colBorrow", HeaderText = "Ngay muon", DataPropertyName = "BorrowDate", FillWeight = 10, DefaultCellStyle = dateStyle });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDue", HeaderText = "Han tra", DataPropertyName = "DueDate", FillWeight = 10, DefaultCellStyle = dateStyle });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colReturned", HeaderText = "Ngay tra thuc te", DataPropertyName = "ReturnedDate", FillWeight = 11, DefaultCellStyle = returnedStyle });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStatus", HeaderText = "Tinh trang", DataPropertyName = "Status", FillWeight = 10 });

            // To mau do cho phieu qua han
            dgv.CellFormatting += (s, e) =>
            {
                if (e.RowIndex >= 0 && dgv.Rows[e.RowIndex].DataBoundItem is Loan l && l.Status == Loan.Overdue)
                    e.CellStyle.ForeColor = Color.Firebrick;
            };

            // ----- Root TableLayoutPanel: nhap phieu / loc / bang danh sach -----
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
            lblCount = new ToolStripStatusLabel("Tong so phieu: 0");
            statusStrip1.Items.Add(lblCount);

            // ----- Form -----
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 660);
            MinimumSize = new Size(850, 560);
            Text = "Lab Equipment Checkout";
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(tlpRoot);      // Fill: them truoc
            Controls.Add(statusStrip1); // Bottom
            Controls.Add(menuStrip1);   // Top
            MainMenuStrip = menuStrip1;

            // ----- BindingSource + su kien -----
            dgv.DataSource = bs;
            bs.CurrentChanged += bs_CurrentChanged;

            txtId.TextChanged += ClearErrorOnEdit;
            txtStudentId.TextChanged += ClearErrorOnEdit;
            txtStudentName.TextChanged += ClearErrorOnEdit;
            cboEquipment.SelectedIndexChanged += ClearErrorOnEdit;
            nudQty.ValueChanged += ClearErrorOnEdit;
            dtpBorrow.ValueChanged += DateInputChanged;
            dtpDue.ValueChanged += DateInputChanged;
            dtpReturned.ValueChanged += DateInputChanged;

            Load += MainForm_Load;
        }
    }
}