using System;
using System.Globalization;
using System.Windows.Forms;
using HRM.BUS;

namespace HRM.GUI
{
    public static class GuiHelper
    {
        public static void Error(Exception ex)
        {
            MessageBox.Show(ErrorHelper.GetMessage(ex), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Info(string msg)
        {
            MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Warn(string msg)
        {
            MessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static bool Confirm(string msg)
        {
            return MessageBox.Show(msg, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        /// <summary>Đặt tiêu đề (và định dạng số) cho cột của DataGridView.</summary>
        public static void Header(DataGridView g, string col, string text, string format = null)
        {
            if (!g.Columns.Contains(col)) return;
            DataGridViewColumn c = g.Columns[col];
            c.HeaderText = text;
            if (format != null)
            {
                c.DefaultCellStyle.Format = format;
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        public static void Hide(DataGridView g, string col)
        {
            if (g.Columns.Contains(col)) g.Columns[col].Visible = false;
        }

        public static bool TryParseDecimal(string s, out decimal value)
        {
            return decimal.TryParse((s ?? "").Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out value);
        }

        public static string Money(decimal d)
        {
            return d.ToString("N0", CultureInfo.CurrentCulture) + " đ";
        }
    }
}
