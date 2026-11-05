using System;
using System.Drawing;
using System.Windows.Forms;

namespace RepairServiceTracker
{
    partial class MainForm
    {
        // ----- Controls -----
        private BindingSource bs;
        private ErrorProvider errorProvider1;

        private MenuStrip menuStrip1;
        private ToolStripMenuItem mnuFile, mnuExport, mnuExit;

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblCount, lblTotal;

        private TableLayoutPanel tlpRoot, tlpTop, tlpInput, tlpImage, tlpFilter;
        private FlowLayoutPanel flpButtons, flpImageButtons;
        private GroupBox grpImage;

        private TextBox txtId, txtCustomer, txtPhone, txtCost, txtProblem, txtSearch, txtImagePath;
        private ComboBox cboDevice, cboStatus, cboFilterStatus;
        private DateTimePicker dtpReceived;
        private PictureBox picDevice;
        private Button btnAdd, btnUpdate, btnDelete, btnClear, btnChooseImage, btnRemoveImage, btnClearFilter;
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

        private static Button MakeButton(string text)
        {
            return new Button { Text = text, AutoSize = true, MinimumSize = new Size(90, 30) };
        }

        private static TextBox MakeTextBox()
        {
            return new TextBox { Dock = DockStyle.Fill, Margin = new Padding(3, 4, 3, 4) };
        }

        private void BuildUi()
        {
            bs = new BindingSource(components);
            errorProvider1 = new ErrorProvider(components);
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            // ----- Input controls -----
            txtId = MakeTextBox();
            txtCustomer = MakeTextBox();
            txtPhone = MakeTextBox();
            txtCost = MakeTextBox();
            txtCost.TextAlign = HorizontalAlignment.Right;
            txtProblem = new TextBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(3, 4, 3, 4),
                Multiline = true,
                Height = 64,
                ScrollBars = ScrollBars.Vertical
            };

            cboDevice = new ComboBox { Dock = DockStyle.Fill, Margin = new Padding(3, 4, 3, 4), DropDownStyle = ComboBoxStyle.DropDownList };
            cboStatus = new ComboBox { Dock = DockStyle.Fill, Margin = new Padding(3, 4, 3, 4), DropDownStyle = ComboBoxStyle.DropDownList };
            dtpReceived = new DateTimePicker
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(3, 4, 3, 4),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy"
            };

            btnAdd = MakeButton("Them");
            btnUpdate = MakeButton("Cap nhat");
            btnDelete = MakeButton("Xoa");
            btnClear = MakeButton("Lam moi");
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
                Dock = DockStyle.Fill,
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

            tlpInput.Controls.Add(MakeLabel("Ma don:"), 0, 0);
            tlpInput.Controls.Add(txtId, 1, 0);
            tlpInput.Controls.Add(MakeLabel("Khach hang:"), 2, 0);
            tlpInput.Controls.Add(txtCustomer, 3, 0);

            tlpInput.Controls.Add(MakeLabel("So dien thoai:"), 0, 1);
            tlpInput.Controls.Add(txtPhone, 1, 1);
            tlpInput.Controls.Add(MakeLabel("Loai thiet bi:"), 2, 1);
            tlpInput.Controls.Add(cboDevice, 3, 1);

            tlpInput.Controls.Add(MakeLabel("Ngay tiep nhan:"), 0, 2);
            tlpInput.Controls.Add(dtpReceived, 1, 2);
            tlpInput.Controls.Add(MakeLabel("Chi phi du kien:"), 2, 2);
            tlpInput.Controls.Add(txtCost, 3, 2);

            tlpInput.Controls.Add(MakeLabel("Trang thai:"), 0, 3);
            tlpInput.Controls.Add(cboStatus, 1, 3);

            tlpInput.Controls.Add(MakeLabel("Mo ta loi:"), 0, 4);
            tlpInput.Controls.Add(txtProblem, 1, 4);
            tlpInput.SetColumnSpan(txtProblem, 3);

            tlpInput.Controls.Add(flpButtons, 1, 5);
            tlpInput.SetColumnSpan(flpButtons, 3);

            // ----- Khu vuc anh thiet bi -----
            picDevice = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,   // hien thi dung ty le
                BorderStyle = BorderStyle.FixedSingle,
                MinimumSize = new Size(180, 130),
                Margin = new Padding(3)
            };
            txtImagePath = new TextBox { Dock = DockStyle.Fill, ReadOnly = true, TabStop = false, Margin = new Padding(3) };
            btnChooseImage = MakeButton("Chon anh...");
            btnRemoveImage = MakeButton("Bo anh");
            btnChooseImage.Click += btnChooseImage_Click;
            btnRemoveImage.Click += btnRemoveImage_Click;

            flpImageButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true
            };
            flpImageButtons.Controls.AddRange(new Control[] { btnChooseImage, btnRemoveImage });

            tlpImage = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3
            };
            tlpImage.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpImage.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpImage.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpImage.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpImage.Controls.Add(picDevice, 0, 0);
            tlpImage.Controls.Add(txtImagePath, 0, 1);
            tlpImage.Controls.Add(flpImageButtons, 0, 2);

            grpImage = new GroupBox
            {
                Text = "Anh thiet bi",
                Dock = DockStyle.Fill,
                Margin = new Padding(3, 8, 8, 3),
                Padding = new Padding(6)
            };
            grpImage.Controls.Add(tlpImage);

            // ----- Khu vuc tren: thong tin don (trai) + anh (phai) -----
            tlpTop = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 2,
                RowCount = 1
            };
            tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlpTop.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTop.Controls.Add(tlpInput, 0, 0);
            tlpTop.Controls.Add(grpImage, 1, 0);

            // ----- Filter bar -----
            txtSearch = MakeTextBox();
            cboFilterStatus = new ComboBox { Width = 140, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(3, 4, 3, 4) };
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
            tlpFilter.Controls.Add(MakeLabel("Tim (ma don / ten / SDT):"), 0, 0);
            tlpFilter.Controls.Add(txtSearch, 1, 0);
            tlpFilter.Controls.Add(MakeLabel("Trang thai:"), 2, 0);
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
            var costStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight };

            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "Ma don", DataPropertyName = "Id", FillWeight = 9 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCustomer", HeaderText = "Khach hang", DataPropertyName = "CustomerName", FillWeight = 15 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPhone", HeaderText = "So dien thoai", DataPropertyName = "Phone", FillWeight = 12 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDevice", HeaderText = "Loai thiet bi", DataPropertyName = "DeviceType", FillWeight = 13 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colProblem", HeaderText = "Mo ta loi", DataPropertyName = "Problem", FillWeight = 20 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colReceived", HeaderText = "Ngay tiep nhan", DataPropertyName = "ReceivedDate", FillWeight = 11, DefaultCellStyle = dateStyle });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCost", HeaderText = "Chi phi du kien", DataPropertyName = "EstimatedCost", FillWeight = 12, DefaultCellStyle = costStyle });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStatus", HeaderText = "Trang thai", DataPropertyName = "Status", FillWeight = 11 });

            // ----- Root TableLayoutPanel: thong tin don + anh / loc / bang -----
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
            tlpRoot.Controls.Add(tlpTop, 0, 0);
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
            lblCount = new ToolStripStatusLabel("So don: 0");
            lblTotal = new ToolStripStatusLabel("");
            statusStrip1.Items.Add(lblCount);
            statusStrip1.Items.Add(lblTotal);

            // ----- Form -----
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1050, 700);
            MinimumSize = new Size(900, 600);
            Text = "Repair Service Tracker";
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(tlpRoot);      // Fill: them truoc
            Controls.Add(statusStrip1); // Bottom
            Controls.Add(menuStrip1);   // Top
            MainMenuStrip = menuStrip1;

            // ----- BindingSource + su kien -----
            dgv.DataSource = bs;
            bs.CurrentChanged += bs_CurrentChanged;

            txtId.TextChanged += ClearErrorOnEdit;
            txtCustomer.TextChanged += ClearErrorOnEdit;
            txtPhone.TextChanged += ClearErrorOnEdit;
            txtCost.TextChanged += ClearErrorOnEdit;
            txtProblem.TextChanged += ClearErrorOnEdit;
            cboDevice.SelectedIndexChanged += ClearErrorOnEdit;
            cboStatus.SelectedIndexChanged += ClearErrorOnEdit;
            dtpReceived.ValueChanged += ClearErrorOnEdit;

            Load += MainForm_Load;
        }
    }
}