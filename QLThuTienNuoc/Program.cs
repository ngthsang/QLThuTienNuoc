using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLThuTienNuoc
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frm_login());
        }
    }
    public static class ThemeManager
    {
        public static void ApplyTheme(Control parent, Color dgvColor, Color btnColor, Color lblColor, Color cboColor)
        {

            foreach (Control control in parent.Controls)
            {
                if (control is DataGridView dgv)  // Đổi màu DataGridView
                {
                    dgv.BackgroundColor = dgvColor;
                    dgv.DefaultCellStyle.BackColor = dgvColor;
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgv.Cursor = Cursors.Hand;
                }
                else if (control is Button btn)  // Đổi màu Button
                {
                    btn.BackColor = btnColor;
                    btn.ForeColor = Color.Maroon; // Chữ đỏ
                }
                else if (control is Label lbl)  // Đổi màu Label
                {
                    lbl.ForeColor = lblColor;
                }
                else if (control is ComboBox cbo)  // Đổi màu ComboBox
                {
                    cbo.BackColor = cboColor;
                    cbo.ForeColor = Color.AliceBlue;
                    cbo.Cursor = Cursors.Hand;
                    cbo.Height = 35; // Thiết lập chiều cao
                }
                else if (control is DateTimePicker dtp) // Đổi màu DateTimePicker
                {
                    //dtp.CalendarMonthBackground = dtpColor;
                    dtp.Height = 35; // Thiết lập chiều cao
                }
                else if (control is TextBox txt)
                {
                    txt.Cursor = Cursors.Hand;
                    txt.ForeColor = Color.Blue;
                    txt.Height = 35;
                }
                else if (control.HasChildren)  // Kiểm tra container như Panel, GroupBox
                {
                    ApplyTheme(control, dgvColor, btnColor, lblColor, cboColor);
                }
            }
        }
    }
}
