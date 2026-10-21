using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MdiApp
{
    // MDI Parent
    public class FormMain : Form
    {
        // ===== Các control quản lý giao diện =====
        private readonly MenuStrip menuStrip = new MenuStrip();
        private readonly ToolStrip toolStrip = new ToolStrip();
        private readonly StatusStrip statusStrip = new StatusStrip();
        private readonly ContextMenuStrip ctxMain = new ContextMenuStrip();

        private readonly ToolStripStatusLabel lblUser = new ToolStripStatusLabel();
        private readonly ToolStripStatusLabel lblAction = new ToolStripStatusLabel { Spring = true, TextAlign = ContentAlignment.MiddleLeft };
        private readonly ToolStripStatusLabel lblWindows = new ToolStripStatusLabel();
        private readonly ToolStripStatusLabel lblTime = new ToolStripStatusLabel();
        private readonly System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer { Interval = 1000 };

        private bool _forceClose;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string UserName { get; set; } = "";

        public FormMain()
        {
            Text = "Ứng dụng quản lý - MDI";
            Size = new Size(1100, 700);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 10F);

            // Thiết lập Form chính là MDI Parent
            IsMdiContainer = true;

            BuildMenu();
            BuildToolStrip();
            BuildStatusStrip();
            BuildContextMenu();

            // Thứ tự add: StatusStrip, ToolStrip, MenuStrip (để menu nằm trên cùng)
            Controls.Add(statusStrip);
            Controls.Add(toolStrip);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;

            // Gắn ContextMenuStrip vào vùng nền MDI
            foreach (Control c in Controls)
                if (c is MdiClient) c.ContextMenuStrip = ctxMain;

            MdiChildActivate += (s, e) => UpdateWindowCount();
            FormClosing += FormMain_FormClosing;
            Load += (s, e) =>
            {
                lblUser.Text = "Người dùng: " + UserName;
                SetStatus("Đăng nhập thành công.");
                UpdateWindowCount();
                UpdateTime();
                timer.Tick += (a, b) => { UpdateTime(); UpdateWindowCount(); };
                timer.Start();
            };
        }

        // ===================== MenuStrip =====================
        private void BuildMenu()
        {
            // Menu Hệ thống
            var mnuSystem = new ToolStripMenuItem("Hệ &thống");
            mnuSystem.DropDownItems.Add(MenuItem("Đăng xuất", (s, e) => Logout(), Keys.Control | Keys.L));
            mnuSystem.DropDownItems.Add(new ToolStripSeparator());
            mnuSystem.DropDownItems.Add(MenuItem("Thoát", (s, e) => ExitApp(), Keys.Alt | Keys.F4));

            // Menu Danh mục
            var mnuCategory = new ToolStripMenuItem("&Danh mục");
            mnuCategory.DropDownItems.Add(MenuItem("Quản lý Sinh viên", (s, e) => OpenChild<FormStudent>(), Keys.Control | Keys.D1));
            mnuCategory.DropDownItems.Add(MenuItem("Quản lý Sản phẩm", (s, e) => OpenChild<FormProduct>(), Keys.Control | Keys.D2));

            // Menu Cửa sổ (Window)
            var mnuWindow = new ToolStripMenuItem("&Cửa sổ");
            mnuWindow.DropDownItems.Add(MenuItem("Cascade", (s, e) => LayoutMdi(MdiLayout.Cascade)));
            mnuWindow.DropDownItems.Add(MenuItem("Tile Horizontal", (s, e) => LayoutMdi(MdiLayout.TileHorizontal)));
            mnuWindow.DropDownItems.Add(MenuItem("Tile Vertical", (s, e) => LayoutMdi(MdiLayout.TileVertical)));
            mnuWindow.DropDownItems.Add(new ToolStripSeparator());
            mnuWindow.DropDownItems.Add(MenuItem("Đóng tất cả", (s, e) => CloseAllChildren()));

            menuStrip.Items.AddRange(new ToolStripItem[] { mnuSystem, mnuCategory, mnuWindow });
            menuStrip.MdiWindowListItem = mnuWindow; // liệt kê các cửa sổ con đang mở
        }

        private static ToolStripMenuItem MenuItem(string text, EventHandler onClick, Keys shortcut = Keys.None)
        {
            var item = new ToolStripMenuItem(text);
            item.Click += onClick;
            if (shortcut != Keys.None) item.ShortcutKeys = shortcut;
            return item;
        }

        // ===================== ToolStrip =====================
        private void BuildToolStrip()
        {
            toolStrip.ImageScalingSize = new Size(24, 24);

            toolStrip.Items.Add(ToolButton("Sinh viên", SystemIcons.Application, "Mở Quản lý Sinh viên (Ctrl+1)",
                (s, e) => OpenChild<FormStudent>()));
            toolStrip.Items.Add(ToolButton("Sản phẩm", SystemIcons.Information, "Mở Quản lý Sản phẩm (Ctrl+2)",
                (s, e) => OpenChild<FormProduct>()));
            toolStrip.Items.Add(new ToolStripSeparator());
            toolStrip.Items.Add(ToolButton("Làm mới", SystemIcons.Shield, "Làm mới dữ liệu form đang chọn",
                (s, e) => RefreshActiveChild()));
            toolStrip.Items.Add(ToolButton("Cascade", SystemIcons.Question, "Sắp xếp Cascade",
                (s, e) => LayoutMdi(MdiLayout.Cascade)));
            toolStrip.Items.Add(new ToolStripSeparator());
            toolStrip.Items.Add(ToolButton("Thoát", SystemIcons.Error, "Thoát chương trình",
                (s, e) => ExitApp()));
        }

        private static ToolStripButton ToolButton(string text, Icon icon, string tip, EventHandler onClick)
        {
            var btn = new ToolStripButton(text, new Bitmap(icon.ToBitmap(), 24, 24))
            {
                DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
                TextImageRelation = TextImageRelation.ImageAboveText,
                ToolTipText = tip
            };
            btn.Click += onClick;
            return btn;
        }

        // ===================== StatusStrip =====================
        private void BuildStatusStrip()
        {
            statusStrip.Items.AddRange(new ToolStripItem[]
            {
                lblUser,
                new ToolStripStatusLabel("|"),
                lblAction,
                lblWindows,
                new ToolStripStatusLabel("|"),
                lblTime
            });
        }

        // Form con gọi (qua event) để cập nhật thông báo thao tác lên StatusStrip
        public void SetStatus(string message)
        {
            lblAction.Text = $"{DateTime.Now:HH:mm:ss} - {message}";
        }

        private void UpdateTime() => lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        private void UpdateWindowCount() => lblWindows.Text = $"Cửa sổ đang mở: {MdiChildren.Length}";

        // ===================== ContextMenuStrip (vùng nền MDI) =====================
        private void BuildContextMenu()
        {
            ctxMain.Items.Add(MenuItem("Mở Quản lý Sinh viên", (s, e) => OpenChild<FormStudent>()));
            ctxMain.Items.Add(MenuItem("Mở Quản lý Sản phẩm", (s, e) => OpenChild<FormProduct>()));
            ctxMain.Items.Add(new ToolStripSeparator());
            ctxMain.Items.Add(MenuItem("Cascade", (s, e) => LayoutMdi(MdiLayout.Cascade)));
            ctxMain.Items.Add(MenuItem("Tile Horizontal", (s, e) => LayoutMdi(MdiLayout.TileHorizontal)));
            ctxMain.Items.Add(MenuItem("Tile Vertical", (s, e) => LayoutMdi(MdiLayout.TileVertical)));
        }

        // ===================== Quản lý Form con =====================
        // Nếu Form chưa mở -> khởi tạo & hiển thị; nếu đã mở -> chỉ Activate, không mở lặp
        private void OpenChild<T>() where T : BaseChildForm, new()
        {
            foreach (Form f in MdiChildren)
            {
                if (f is T)
                {
                    if (f.WindowState == FormWindowState.Minimized)
                        f.WindowState = FormWindowState.Normal;
                    f.Activate();
                    SetStatus($"Form \"{f.Text}\" đã mở, được đưa lên trên cùng.");
                    return;
                }
            }

            var child = new T { MdiParent = this };
            // Nhận thông điệp từ Form con để cập nhật StatusStrip
            child.ActionPerformed += (s, msg) => SetStatus(msg);
            child.FormClosed += (s, e) => SetStatus($"Đã đóng form \"{child.Text}\".");
            child.Show();
            SetStatus($"Đã mở form \"{child.Text}\".");
        }

        // Cha gọi hàm của con
        private void RefreshActiveChild()
        {
            if (ActiveMdiChild is BaseChildForm child)
                child.RefreshData();
            else
                SetStatus("Chưa có form nào đang chọn để làm mới.");
        }

        private void CloseAllChildren()
        {
            foreach (Form f in MdiChildren) f.Close();
            SetStatus("Đã đóng tất cả cửa sổ con.");
        }

        // ===================== Hệ thống: Đăng xuất / Thoát =====================
        private void Logout()
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            foreach (Form f in MdiChildren) f.Close();
            Hide();

            using var login = new FormLogin();
            if (login.ShowDialog() == DialogResult.OK)
            {
                UserName = login.UserName;
                lblUser.Text = "Người dùng: " + UserName;
                SetStatus("Đăng nhập thành công.");
                Show();
            }
            else
            {
                _forceClose = true;
                Close();
            }
        }

        private void ExitApp()
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát chương trình?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            _forceClose = true;
            Close();
        }

        private void FormMain_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (_forceClose) return;
            if (MessageBox.Show("Bạn có chắc muốn thoát chương trình?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                e.Cancel = true;
        }
    }
}