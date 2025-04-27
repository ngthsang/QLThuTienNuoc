using QLThuTienNuoc.model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QLThuTienNuoc
{
    public partial class frm_nhanvien : frm_config
    {
        public frm_nhanvien()
        {
            InitializeComponent();
        }
        common cm = new common();
        int code = 0;
        private void frm_nhanvien_Load(object sender, EventArgs e)
        {
            load_nv();
            if (dataGridView2.DataSource != null)
            {
                // Thêm cột khôi phục
                DataGridViewButtonColumn btnkhoiphuc = new DataGridViewButtonColumn();
                btnkhoiphuc.Name = "btn_rs";
                btnkhoiphuc.HeaderText = "Phục hồi";
                btnkhoiphuc.Text = "Phục hồi";
                btnkhoiphuc.UseColumnTextForButtonValue = true;
                dataGridView2.Columns.Add(btnkhoiphuc);

                dataGridView2.Columns[0].HeaderText = "Nhân viên id";
                dataGridView2.Columns[1].HeaderText = "Tài khoản";
                dataGridView2.Columns[2].HeaderText = "Họ Tên";
                dataGridView2.Columns[3].HeaderText = "Chức vụ";
                dataGridView2.Columns[4].HeaderText = "Email";
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

                dataGridView1.Columns[0].HeaderText = "Nhân viên id";
                dataGridView1.Columns[1].HeaderText = "Tài khoản";
                dataGridView1.Columns[2].HeaderText = "Họ Tên";
                dataGridView1.Columns[3].HeaderText = "Chức vụ";
                dataGridView1.Columns[4].HeaderText = "Email";
                dataGridView1.Columns[5].HeaderText = "Ngày tạo";
            }
        }

        private void load_nv()
        {
            cm.LoadDataToGridView(dataGridView1, "select * from view_nhanvien_active");
            cm.LoadDataToGridView(dataGridView2, "select * from view_nhanvien_inactive");
            cbb_loai.SelectedIndex = 1;
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // e.RowIndex là chỉ số hàng được double click
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                code = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["TaiKhoanId"].Value.ToString());
                txt_user.Text = selectedRow.Cells["TenDangNhap"].Value?.ToString();
                txt_ten.Text = selectedRow.Cells["HoTen"].Value?.ToString();
                txt_email.Text = selectedRow.Cells["Email"].Value?.ToString();
                cbb_loai.Text = selectedRow.Cells["ChucVu"].Value?.ToString();
                int TaiKhoangId = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["TaiKhoanId"].Value.ToString());
                if (dataGridView1.Columns[e.ColumnIndex].Name == "btn_xoa")
                {
                    // Nếu người dùng nhấn nút Hủy
                    // Gọi stored procedure toggle_trangthai_hoadon
                    Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@TaiKhoanId", TaiKhoangId }
                    };
                    bool isCancelled = cm.ExecuteStoredProcedure("toggle_trangthai_nhanvien", parameters);

                    if (isCancelled)
                    {
                        MessageBox.Show("nhân viên đã bị xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        load_nv();
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
                int TaiKhoangId = int.Parse(dataGridView2.Rows[e.RowIndex].Cells["TaiKhoanId"].Value.ToString());
                if (dataGridView2.Columns[e.ColumnIndex].Name == "btn_rs")
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@TaiKhoanId", TaiKhoangId }
                    };
                    bool isCancelled = cm.ExecuteStoredProcedure("toggle_trangthai_nhanvien", parameters);

                    if (isCancelled)
                    {
                        MessageBox.Show("Nhân viên đã được phục hồi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        load_nv();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi phục hồi ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            string ten = txt_ten.Text.Trim();
            string email = txt_email.Text.Trim();
            string tk = txt_user.Text.Trim();
            string pass = txt_pass.Text.Trim();

            // 1. Validate
            if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(tk) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin nhân viên.");
                return;
            }

            // 2. Tạo danh sách parameters
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@TenDangNhap", tk },
                { "@MatKhau", pass },
                { "@HoTen", ten },
                { "@ChucVu", cbb_loai.SelectedItem.ToString() },
                { "@Email", email }
            };

            // 3. Gọi procedure
            bool isSuccess = cm.ExecuteStoredProcedure("insert_nhanvien", parameters);

            // 4. Thông báo kết quả
            if (isSuccess)
            {
                MessageBox.Show("Thêm nhân viên thành công!");
                load_nv();
            }
            else
            {
                MessageBox.Show("Thêm nhân viên thất bại.");
            }
        }

        private void btn_capnhat_Click(object sender, EventArgs e)
        {
            if(code != 0)
            {
                string ten = txt_ten.Text.Trim();
                string email = txt_email.Text.Trim();
                string pass = txt_pass.Text.Trim();
                string loai = cbb_loai.SelectedItem.ToString();
                // 1. Validate
                if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(email)  || string.IsNullOrEmpty(pass))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin nhân viên.");
                    return;
                }

                // 2. Tạo danh sách parameters
                Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@MatKhau", pass },
                        { "@HoTen", ten },
                        { "@ChucVu", loai },
                        { "@Email", email },
                            {"@TaiKhoanId", code }
                    };

                // 3. Gọi procedure
                bool isSuccess = cm.ExecuteStoredProcedure("update_nhanvien", parameters);

                // 4. Thông báo kết quả
                if (isSuccess)
                {
                    MessageBox.Show("Update nhân viên hàng thành công!");
                    load_nv();
                }
                else
                {
                    MessageBox.Show("Update nhân viên hàng thất bại.");
                }
            }    
        }
    }
}
