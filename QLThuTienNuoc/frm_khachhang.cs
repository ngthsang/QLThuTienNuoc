using QLThuTienNuoc.model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QLThuTienNuoc
{
    public partial class frm_khachhang : frm_config
    {
        public frm_khachhang()
        {
            InitializeComponent();
        }
        common cm = new common();
        int code = 0;
        private void load_kh()
        {
            cm.LoadComboBoxWithQuery(cbb_loai, "Select DISTINCT LoaiKH from BangGia", "LoaiKH", "LoaiKH");
            cm.LoadDataToGridView(dataGridView1,"select * from view_khach_active");
            cm.LoadDataToGridView(dataGridView2, "select * from view_khach_inactive");
            cbb_loai.SelectedIndex = 1;
        }
        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            string ten = txt_ten.Text.Trim();
            string sdt = txt_sdt.Text.Trim();
            string dc = txt_dc.Text.Trim();
            string loai = cbb_loai.SelectedValue.ToString();
            // 1. Validate
            if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(dc))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng.");
                return;
            }

            // 2. Tạo danh sách parameters
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@HoTen", ten },
                { "@DiaChi", dc },
                { "@SoDienThoai", sdt },
                { "@TenLoaiKH", loai },
                { "@TaiKhoanId", Session.user_id }
            };

            // 3. Gọi procedure
            bool isSuccess = cm.ExecuteStoredProcedure("insert_khach", parameters);

            // 4. Thông báo kết quả
            if (isSuccess)
            {
                MessageBox.Show("Thêm khách hàng thành công!");
                load_kh();
            }
            else
            {
                MessageBox.Show("Thêm khách hàng thất bại.");
            }
        }

        private void frm_khachhang_Load(object sender, EventArgs e)
        {
            load_kh();
            if (dataGridView2.DataSource != null)
            {
                // Thêm cột khôi phục
                DataGridViewButtonColumn btnkhoiphuc = new DataGridViewButtonColumn();
                btnkhoiphuc.Name = "btn_rs";
                btnkhoiphuc.HeaderText = "Phục hồi";
                btnkhoiphuc.Text = "Phục hồi";
                btnkhoiphuc.UseColumnTextForButtonValue = true;
                dataGridView2.Columns.Add(btnkhoiphuc);

                dataGridView2.Columns[0].HeaderText = "Khách id";
                dataGridView2.Columns[1].HeaderText = "Ho Tên";
                dataGridView2.Columns[2].HeaderText = "Địa chỉ";
                dataGridView2.Columns[3].HeaderText = "Số điện thoại";
                dataGridView2.Columns[4].HeaderText = "Loại";
                dataGridView2.Columns[5].HeaderText = "Ngày tạo";
                dataGridView2.Columns[6].HeaderText = "Người tạo";
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

                dataGridView1.Columns[0].HeaderText = "Khách id";
                dataGridView1.Columns[1].HeaderText = "Ho Tên";
                dataGridView1.Columns[2].HeaderText = "Địa chỉ";
                dataGridView1.Columns[3].HeaderText = "Số điện thoại";
                dataGridView1.Columns[4].HeaderText = "Loại";
                dataGridView1.Columns[5].HeaderText = "Ngày tạo";
                dataGridView1.Columns[6].HeaderText = "Người tạo";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // e.RowIndex là chỉ số hàng được double click
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                code = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["KhachHangId"].Value.ToString());
                txt_ten.Text = selectedRow.Cells["HoTen"].Value?.ToString();
                txt_sdt.Text = selectedRow.Cells["SoDienThoai"].Value?.ToString();
                txt_dc.Text = selectedRow.Cells["DiaChi"].Value?.ToString();
                cbb_loai.Text = selectedRow.Cells["TenLoaiKH"].Value?.ToString();
                int KhachHangId = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["KhachHangId"].Value.ToString());
                if (dataGridView1.Columns[e.ColumnIndex].Name == "btn_xoa")
                {
                    // Nếu người dùng nhấn nút Hủy
                    // Gọi stored procedure toggle_trangthai_hoadon
                    Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@KhachHangId", KhachHangId }
                    };
                    bool isCancelled = cm.ExecuteStoredProcedure("toggle_trangthai_khach", parameters);

                    if (isCancelled)
                    {
                        MessageBox.Show("Khách đã bị xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        load_kh();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi xóa ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // e.RowIndex là chỉ số hàng được double click
            {
                int KhachHangId = int.Parse(dataGridView2.Rows[e.RowIndex].Cells["KhachHangId"].Value.ToString());
                if (dataGridView2.Columns[e.ColumnIndex].Name == "btn_rs")
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@KhachHangId", KhachHangId }
                    };
                    bool isCancelled = cm.ExecuteStoredProcedure("toggle_trangthai_khach", parameters); 

                    if (isCancelled)
                    {
                        MessageBox.Show("Khách đã phục hồi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        load_kh();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi phục hồi ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btn_capnhat_Click(object sender, EventArgs e)
        {
            string ten = txt_ten.Text.Trim();
            string sdt = txt_sdt.Text.Trim();
            string dc = txt_dc.Text.Trim();
            string loai = cbb_loai.SelectedValue.ToString();
            if (code != 0)
            {
                if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(dc))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng.");
                    return;
                }

                Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@HoTen", ten },
                        { "@DiaChi", dc },
                        { "@SoDienThoai", sdt },
                        { "@TenLoaiKH", loai },
                        { "@TaiKhoanId", Session.user_id },
                        {"@KhachHangId", code }
                    };

                bool isSuccess = cm.ExecuteStoredProcedure("update_khach", parameters);

                if (isSuccess)
                {
                    MessageBox.Show("Cập nhật khách hàng thành công!");
                    load_kh();
                }
                else
                {
                    MessageBox.Show("Cập nhật khách hàng thất bại.");
                }
            }    
            
        }
    }
}
