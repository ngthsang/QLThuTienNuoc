using System;
using QLThuTienNuoc.model;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QLThuTienNuoc
{
    public partial class frm_banggia : frm_config
    {
        public frm_banggia()
        {
            InitializeComponent();
        }
        common cm = new common();
        int code = 0;
        private void frm_banggia_Load(object sender, EventArgs e)
        {
            load_bg();
            if (dataGridView2.DataSource != null)
            {
                // Thêm cột khôi phục
                DataGridViewButtonColumn btnkhoiphuc = new DataGridViewButtonColumn();
                btnkhoiphuc.Name = "btn_rs";
                btnkhoiphuc.HeaderText = "Phục hồi";
                btnkhoiphuc.Text = "Phục hồi";
                btnkhoiphuc.UseColumnTextForButtonValue = true;
                dataGridView2.Columns.Add(btnkhoiphuc);

                dataGridView2.Columns[0].HeaderText = "Bảng giá id";
                dataGridView2.Columns[1].HeaderText = "Loại khách hàng";
                dataGridView2.Columns[2].HeaderText = "Giá";
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

                dataGridView1.Columns[0].HeaderText = "Bảng giá id";
                dataGridView1.Columns[1].HeaderText = "Loại khách hàng";
                dataGridView1.Columns[2].HeaderText = "Giá";
            }
        }

        private void load_bg()
        {
            cm.LoadDataToGridView(dataGridView1, "select * from v_banggia");
            cm.LoadDataToGridView(dataGridView2, "select * from view_banggia_inactive");
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
                code = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["BangGiaId"].Value.ToString());
                txt_loai.Text = selectedRow.Cells["LoaiKH"].Value?.ToString();
                txt_gia.Text = selectedRow.Cells["Gia"].Value?.ToString();
                int BangGiaId = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["BangGiaId"].Value.ToString());
                if (dataGridView1.Columns[e.ColumnIndex].Name == "btn_xoa")
                {
                    // Nếu người dùng nhấn nút Hủy
                    // Gọi stored procedure toggle_trangthai_hoadon
                    Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@BangGiaId", BangGiaId }
                    };
                    bool isCancelled = cm.ExecuteStoredProcedure("toggle_trangthai_banggia", parameters);

                    if (isCancelled)
                    {
                        MessageBox.Show("Bảng giá đã bị ngừng áp dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        load_bg();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // e.RowIndex là chỉ số hàng được double click
            {
                int BangGiaId = int.Parse(dataGridView2.Rows[e.RowIndex].Cells["BangGiaId"].Value.ToString());
                if (dataGridView2.Columns[e.ColumnIndex].Name == "btn_rs")
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@BangGiaId", BangGiaId }
                    };
                    bool isCancelled = cm.ExecuteStoredProcedure("toggle_trangthai_banggia", parameters);

                    if (isCancelled)
                    {
                        MessageBox.Show("bảng giá đã được phục hồi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        load_bg();
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
            string loai = txt_loai.Text.Trim();
            int gia = int.Parse(txt_gia.Text.Trim());

            // 1. Validate
            if (string.IsNullOrEmpty(loai) || gia == null )
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            // 2. Tạo danh sách parameters
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@LoaiKH", loai },
                { "@Gia", gia },
            };

            // 3. Gọi procedure
            bool isSuccess = cm.ExecuteStoredProcedure("insert_banggia", parameters);

            // 4. Thông báo kết quả
            if (isSuccess)
            {
                MessageBox.Show("Thêm thành công!");
                load_bg();
            }
            else
            {
                MessageBox.Show("Thêm thất bại.");
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if(code != 0)
            {
                string loai = txt_loai.Text.Trim();
                int gia = int.Parse(txt_gia.Text.Trim());

                // 1. Validate
                if (string.IsNullOrEmpty(loai) || gia == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                    return;
                }

                // 2. Tạo danh sách parameters
                Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@LoaiKH", loai },
                        { "@Gia", gia },
                    {"@BangGiaId",code }
                    };

                // 3. Gọi procedure
                bool isSuccess = cm.ExecuteStoredProcedure("update_banggia", parameters);

                // 4. Thông báo kết quả
                if (isSuccess)
                {
                    MessageBox.Show("cập nhật thành công!");
                    load_bg();
                }
                else
                {
                    MessageBox.Show("cập nhật thất bại.");
                }
            }    
        }
    }
}
