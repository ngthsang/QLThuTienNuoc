using QLThuTienNuoc.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace QLThuTienNuoc
{
    public partial class frm_baocao : frm_config
    {
        public frm_baocao()
        {
            InitializeComponent();
        }
        common cm = new common();
        DataTable dt;
        public void load_hoadon()
        {
            cm.LoadDataToGridView(dataGridView1,"select * from view_hoadon");
        }
        public void load_cbb_khach()
        {
            string _s = @"select * from cbb_khach";
            cm.LoadComboBoxWithQuery(cbb_khach, _s, "HoTen", "HopDongId");
        }
        private void frm_baocao_Load(object sender, EventArgs e)
        {
            dtp1.MaxDate = DateTime.Now.Date;
            dtp2.MaxDate = DateTime.Now.Date;
            load_hoadon();
            load_cbb_khach();

        }

        private void btn_filter_Click(object sender, EventArgs e)
        {

            DateTime fromDate = dtp1.Value.Date;
            DateTime toDate = dtp2.Value.Date;
            if (toDate < fromDate)
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = $"SELECT * FROM view_hoadon WHERE NgayLapHoaDon BETWEEN '{fromDate:yyyy-MM-dd}' AND '{toDate:yyyy-MM-dd}'";
            dt = cm.LoadDataQuery(query);
            dataGridView1.DataSource = dt;
        }

        private void btn_in_Click(object sender, EventArgs e)
        {
            if(dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    rpt_mothoadon rpt_report_xn = new rpt_mothoadon();
                    rpt_report_xn.SetDataSource(dt);
                    frm_in form_in_pt = new frm_in();
                    form_in_pt.crystalReportViewer1.ReportSource = rpt_report_xn;
                    form_in_pt.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }    
            else
            {
                string query = $"SELECT * FROM view_hoadon";
                dt = cm.LoadDataQuery(query);
                if (dt.Rows.Count > 0)
                {
                    rpt_mothoadon rpt_report_xn = new rpt_mothoadon();
                    rpt_report_xn.SetDataSource(dt);
                    frm_in form_in_pt = new frm_in();
                    form_in_pt.crystalReportViewer1.ReportSource = rpt_report_xn;
                    form_in_pt.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }    
        }

        private void cbb_khach_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string query = $"SELECT * FROM view_hoadon WHERE HopDongId=" + int.Parse(cbb_khach.SelectedValue.ToString());
            dt = cm.LoadDataQuery(query);
            dataGridView1.DataSource = dt;
        }

        private void cbb_trangthai_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string query = $"SELECT * FROM view_hoadon WHERE TrangThaiThanhToan=N'" + cbb_khach.Text + "'";
            dt = cm.LoadDataQuery(query);
            dataGridView1.DataSource = dt;
        }
    }
}
