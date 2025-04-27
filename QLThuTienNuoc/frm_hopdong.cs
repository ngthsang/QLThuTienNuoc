using QLThuTienNuoc.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace QLThuTienNuoc
{
    public partial class frm_hopdong : frm_config
    {
        public frm_hopdong()
        {
            InitializeComponent();
        }
        common cm = new common();
        int code = 0;
        private void load_kh()
        {
            cm.LoadComboBoxWithQuery(cbb_khach, "Select * from view_khach_active", "HoTen", "KhachHangId");
            cm.LoadDataToGridView(dataGridView1,"select * from Hopdong_active");
            cm.LoadDataToGridView(dataGridView2, "select * from Hopdong_inactive");
        }
        private void frm_hopdong_Load(object sender, EventArgs e)
        {
            load_kh();
            dateTimePicker1.MaxDate = DateTime.Now;
            if (dataGridView2.DataSource != null)
            {
                // Thêm cột khôi phục
                DataGridViewButtonColumn btnkhoiphuc = new DataGridViewButtonColumn();
                btnkhoiphuc.Name = "btn_rs";
                btnkhoiphuc.HeaderText = "Phục hồi";
                btnkhoiphuc.Text = "Phục hồi";
                btnkhoiphuc.UseColumnTextForButtonValue = true;
                dataGridView2.Columns.Add(btnkhoiphuc);

                dataGridView2.Columns[0].HeaderText = "Hợp đồng id";
                dataGridView2.Columns[1].HeaderText = "Tên khách hàng";
                dataGridView2.Columns[2].HeaderText = "Ngày đăng ký";
                dataGridView2.Columns[3].HeaderText = "Tài khoản";
                dataGridView2.Columns[4].HeaderText = "Nhân viên tạo";
                dataGridView2.Columns[5].HeaderText = "Ngày tạo";
            }
            if (dataGridView1.DataSource != null)
            {
                // Thêm cột khôi phục
                DataGridViewButtonColumn btn_xoa = new DataGridViewButtonColumn();
                btn_xoa.Name = "btn_xoa";
                btn_xoa.HeaderText = "Xóa";
                btn_xoa.Text = "Xóa";
                btn_xoa.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btn_xoa);

                dataGridView1.Columns[0].HeaderText = "Hợp đồng id";
                dataGridView1.Columns[1].HeaderText = "Tên khách hàng";
                dataGridView1.Columns[2].HeaderText = "Ngày đăng ký";
                dataGridView1.Columns[3].HeaderText = "Tài khoản";
                dataGridView1.Columns[4].HeaderText = "Nhân viên tạo";
                dataGridView1.Columns[5].HeaderText = "Ngày tạo";
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // e.RowIndex là chỉ số hàng được double click
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                code = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["HopDongId"].Value.ToString());
                string tenKhachHang = selectedRow.Cells["TenKhachHang"].Value?.ToString();
                foreach (DataRowView item in cbb_khach.Items)
                {
                    if (item["HoTen"].ToString() == tenKhachHang)
                    {
                        cbb_khach.SelectedItem = item;
                        break;
                    }
                }
                txt_dc.Text = selectedRow.Cells["Diachi"].Value?.ToString();
                dateTimePicker1.Value = DateTime.Parse(selectedRow.Cells["NgayDangKy"].Value?.ToString());

                int HopDongId = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["HopDongId"].Value.ToString());
                if (dataGridView1.Columns[e.ColumnIndex].Name == "btn_xoa")
                {
                    // Nếu người dùng nhấn nút Hủy
                    // Gọi stored procedure toggle_trangthai_hoadon
                    Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@HopDongId", HopDongId }
                    };
                    bool isCancelled = cm.ExecuteStoredProcedure("toggle_trangthai_hopdong", parameters);

                    if (isCancelled)
                    {
                        MessageBox.Show("hợp đồng đã bị hủy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        load_kh();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

                if (e.RowIndex >= 0) // e.RowIndex là chỉ số hàng được double click
                {
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                    int HopDongId = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["HopDongId"].Value.ToString());
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "btn_xoa")
                    {
                        // Nếu người dùng nhấn nút Hủy
                        // Gọi stored procedure toggle_trangthai_hoadon
                        Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@HopDongId", HopDongId }
                    };
                        bool isCancelled = cm.ExecuteStoredProcedure("toggle_trangthai_hopdong", parameters);

                        if (isCancelled)
                        {
                            MessageBox.Show("hợp đồng đã phuc hoi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        load_kh();
                        }
                        else
                        {
                            MessageBox.Show("Có lỗi xảy ra ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
        
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            int KhachHangId = int.Parse(cbb_khach.SelectedValue.ToString());
            string dc = txt_dc.Text;
            DateTime NgayDangKy = dateTimePicker1.Value;
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Diachi", dc },
                { "@KhachHangId", KhachHangId },
                { "@NgayDangKy", NgayDangKy },
                { "@TaiKhoanId", Session.user_id }
            };
            bool isSuccess = cm.ExecuteStoredProcedure("insert_hopdong", parameters);

            if (isSuccess)
            {
                MessageBox.Show("Add hop dong thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load_kh();
            }
        }

        private void btn_capnhat_Click(object sender, EventArgs e)
        {
            if(code != 0)
            {
                int KhachHangId = int.Parse(cbb_khach.SelectedValue.ToString());
                string dc = txt_dc.Text;
                DateTime NgayDangKy = dateTimePicker1.Value;
                Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@diachi", dc },
                        { "@KhachHangId", KhachHangId },
                        { "@NgayDangKy", NgayDangKy },
                        { "@@taikhoanid", Session.user_id },
                    {"@HopDongId",code }
                    };
                bool isSuccess = cm.ExecuteStoredProcedure("update_hopdong", parameters);

                if (isSuccess)
                {
                    MessageBox.Show("Cập nhật hop dong thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    load_kh();
                }
            }
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
