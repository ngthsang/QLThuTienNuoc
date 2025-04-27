using QLThuTienNuoc.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace QLThuTienNuoc
{
    public partial class frm_home : frm_config
    {
        public frm_home(string role)
        {
            InitializeComponent();
        }
        common cm = new common();
        public void load_cbb_khach()
        {
            string _s = @"select * from cbb_khach";
            cm.LoadComboBoxWithQuery(cbb_khach,_s,"HoTen", "KhachHangId");
        }
        public void load_hoadon()
        {
            cm.LoadDataToGridView(dataGridView1, "select * from view_hoadon_active");
            cm.LoadDataToGridView(dataGridView2, "select * from view_hoadon_inactive");
            cm.LoadDataToGridView(dataGridView3, "select * from view_hoadon_canceled");
        }
        private void AddButtonsToDataGridView()
        {
            // Thêm cột Thanh toán
            DataGridViewButtonColumn btnThanhToan = new DataGridViewButtonColumn();
            btnThanhToan.Name = "ThanhToan";
            btnThanhToan.HeaderText = "Thanh Toán";
            btnThanhToan.Text = "Thanh Toán";
            btnThanhToan.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btnThanhToan);

            // Thêm cột Hủy
            DataGridViewButtonColumn btnHuy = new DataGridViewButtonColumn();
            btnHuy.Name = "Huy";
            btnHuy.HeaderText = "Hủy";
            btnHuy.Text = "Hủy";
            btnHuy.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btnHuy);
            // nut in
            DataGridViewButtonColumn btnIn = new DataGridViewButtonColumn();
            btnIn.Name = "btnIn";
            btnIn.HeaderText = "In";
            btnIn.Text = "In";
            btnIn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btnIn);
        }
        private void frm_home_Load(object sender, EventArgs e)
        {
            load_cbb_khach();
            load_hoadon();
            dateTimePicker1.MaxDate = DateTime.Today;
            if (dataGridView1.DataSource != null)
            {
                AddButtonsToDataGridView();
                dataGridView1.Columns[0].HeaderText = "Hóa đơn id";
                dataGridView1.Columns[1].HeaderText = "Hợp đồng id";
                dataGridView1.Columns[2].HeaderText = "Tên khách";
                dataGridView1.Columns[3].HeaderText = "Ngày lập";
                dataGridView1.Columns[4].HeaderText = "Chỉ số đầu";
                dataGridView1.Columns[5].HeaderText = "Chỉ số cuối";
                dataGridView1.Columns[6].HeaderText = "Tổng khối";
                dataGridView1.Columns[7].HeaderText = "Tổng tiền";
                dataGridView1.Columns[8].HeaderText = "Thanh toán";
                dataGridView1.Columns[9].HeaderText = "Ngày thanh toán";
                dataGridView1.Columns[10].HeaderText = "Nhân viên lập HĐon";
                dataGridView1.Columns[11].HeaderText = "Ngày tạo";
            }
            if (dataGridView2.DataSource != null)
            {
                DataGridViewButtonColumn btnIn = new DataGridViewButtonColumn();
                btnIn.Name = "btnIn";
                btnIn.HeaderText = "In";
                btnIn.Text = "In";
                btnIn.UseColumnTextForButtonValue = true;
                dataGridView2.Columns.Add(btnIn);

                dataGridView2.Columns[0].HeaderText = "Hóa đơn id";
                dataGridView2.Columns[1].HeaderText = "Hợp đồng id";
                dataGridView2.Columns[2].HeaderText = "Tên khách";
                dataGridView2.Columns[3].HeaderText = "Ngày lập";
                dataGridView2.Columns[4].HeaderText = "Chỉ số đầu";
                dataGridView2.Columns[5].HeaderText = "Chỉ số cuối";
                dataGridView2.Columns[6].HeaderText = "Tổng khối";
                dataGridView2.Columns[7].HeaderText = "Tổng tiền";
                dataGridView2.Columns[8].HeaderText = "Thanh toán";
                dataGridView2.Columns[9].HeaderText = "Ngày thanh toán";
                dataGridView2.Columns[10].HeaderText = "Nhân viên lập HĐon";
                dataGridView2.Columns[11].HeaderText = "Ngày tạo";
            }
            if(dataGridView3.DataSource != null)
            {
                // Thêm cột khôi phục
                DataGridViewButtonColumn btnkhoiphuc = new DataGridViewButtonColumn();
                btnkhoiphuc.Name = "btn_rs";
                btnkhoiphuc.HeaderText = "Phục hồi";
                btnkhoiphuc.Text = "Phục hồi";
                btnkhoiphuc.UseColumnTextForButtonValue = true;
                dataGridView3.Columns.Add(btnkhoiphuc);

                dataGridView3.Columns[0].HeaderText = "Hóa đơn id";
                dataGridView3.Columns[1].HeaderText = "Hợp đồng id";
                dataGridView3.Columns[2].HeaderText = "Tên khách";
                dataGridView3.Columns[3].HeaderText = "Ngày lập";
                dataGridView3.Columns[4].HeaderText = "Chỉ số đầu";
                dataGridView3.Columns[5].HeaderText = "Chỉ số cuối";
                dataGridView3.Columns[6].HeaderText = "Tổng khối";
                dataGridView3.Columns[7].HeaderText = "Tổng tiền";
                dataGridView3.Columns[8].HeaderText = "Thanh toán";
                dataGridView3.Columns[9].HeaderText = "Ngày thanh toán";
                dataGridView3.Columns[10].HeaderText = "Nhân viên lập HĐon";
                dataGridView3.Columns[11].HeaderText = "Ngày tạo";
            }    
            
        }

        private void cbb_khach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_khach.SelectedItem is DataRowView selectedRow)
            {
                string loaiKH = selectedRow["TenLoaiKH"].ToString();
                string _hdid = selectedRow["HopDongId"].ToString();
                txt_dc.Text = selectedRow["Diachi"].ToString();
                txt_loaikh.Text = loaiKH;
                txt_mahd.Text = _hdid;
                if (!string.IsNullOrEmpty(loaiKH))
                {
                    string query = "SELECT Gia FROM BangGia WHERE LoaiKH = @LoaiKH";
                    var parameters = new Dictionary<string, object>
                    {
                        { "@LoaiKH", loaiKH }
                    };
                    string gia = cm.ExecuteScalar(query, parameters);
                    txt_gia.Text = gia ?? "0";
                }
                if(!string.IsNullOrEmpty(_hdid))
                {
                    int hd_id = int.Parse(_hdid);
                    string _s = @"SELECT TOP 1 CSC FROM LichSuThanhToan
                                    WHERE HopDongId = @hd_id ORDER BY NgayThanhToan DESC";
                    var _param = new Dictionary<string, object>
                    {
                        { "@hd_id", hd_id }
                    };
                    string csc = cm.ExecuteScalar(_s, _param);
                    csntt.Text = csc;
                }    
            }
        }

        private void txt_csnht_TextChanged(object sender, EventArgs e)
        {
            if(int.Parse(csntt.Text) < int.Parse(txt_csnht.Text))
            {
                int a = int.Parse(csntt.Text);
                int b = int.Parse(txt_csnht.Text);
                int c = b - a;
                txt_tong.Text = c.ToString();
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            int cs_cu = int.Parse(csntt.Text);     // Chỉ số cũ (cuối kỳ tháng trước)
            int cs_moi = int.Parse(txt_csnht.Text); // Chỉ số mới (đầu kỳ tháng này)

            if (cs_moi < cs_cu)
            {
                MessageBox.Show("Chỉ số nước hiện tại phải lớn hơn chỉ số nước tháng trước", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string payment = "Chưa thanh toán";
            int tongKhoiNuoc = cs_moi - cs_cu;
            int gia = int.Parse(txt_gia.Text);
            int tongTien = tongKhoiNuoc * gia;
            int hopDongId = int.Parse(txt_mahd.Text);
            int khach_id = int.Parse(cbb_khach.SelectedValue.ToString());
            DateTime ngayLap = dateTimePicker1.Value;
            DateTime? ngayThanhToan = null;  // Khai báo để chứa giá trị ngày thanh toán

            if (ck_tt.Checked == true)
            {
                payment = "Đã thanh toán";
                ngayThanhToan = DateTime.Now.Date;  // Nếu đã thanh toán, gán ngày thanh toán là ngày hiện tại
                Dictionary<string, object> _parameters = new Dictionary<string, object>
                    {
                        { "@HoaDonId", hopDongId },
                        { "@CSD", cs_cu },
                        { "@CSC", cs_moi },
                        { "@SoTienThanhToan", tongTien },
                    };
                cm.ExecuteStoredProcedure("insert_lichsuthanhtoan", _parameters);
            }
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@HopDongId", hopDongId },
                { "@KhachHangId", khach_id },
                { "@NgayLapHoaDon", ngayLap },
                { "@CSD", cs_cu },
                { "@CSC", cs_moi },
                { "@TongKhoiNuoc", tongKhoiNuoc },
                { "@TongTien", tongTien },
                { "@TrangThaiThanhToan", payment },
                { "@NgayThanhToan", ngayThanhToan },
                { "@diachi", txt_dc.Text },
                { "@TaiKhoanId", Session.user_id }
            };
            bool isSuccess = cm.ExecuteStoredProcedure("insert_hoadon", parameters);
              
            if (isSuccess)
            {
                MessageBox.Show("Thêm hóa đơn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load_hoadon();
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            int cs_cu = int.Parse(csntt.Text);     // Chỉ số cũ (cuối kỳ tháng trước)
            int cs_moi = int.Parse(txt_csnht.Text); // Chỉ số mới (đầu kỳ tháng này)

            if (cs_moi < cs_cu)
            {
                MessageBox.Show("Chỉ số nước hiện tại phải lớn hơn chỉ số nước tháng trước", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int _hdid = int.Parse(txt_sohieuhoadon.Text);
            string payment = "Chưa thanh toán";
            int tongKhoiNuoc = cs_moi - cs_cu;
            int gia = int.Parse(txt_gia.Text);
            int tongTien = tongKhoiNuoc * gia;
            int hopDongId = int.Parse(txt_mahd.Text);
            int khach_id = int.Parse(cbb_khach.SelectedValue.ToString());
            DateTime ngayLap = dateTimePicker1.Value;
            DateTime? ngayThanhToan = null;  // Khai báo để chứa giá trị ngày thanh toán

            if (ck_tt.Checked == true)
            {
                payment = "Đã thanh toán";
                ngayThanhToan = DateTime.Now.Date;  // Nếu đã thanh toán, gán ngày thanh toán là ngày hiện tại
            }
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@HoaDonId", _hdid },
                { "@HopDongId", hopDongId },
                { "@KhachHangId", khach_id },
                { "@NgayLapHoaDon", ngayLap },
                { "@CSD", cs_cu },
                { "@CSC", cs_moi },
                { "@TongKhoiNuoc", tongKhoiNuoc },
                { "@TongTien", tongTien },
                { "@TrangThaiThanhToan", payment },
                { "@NgayThanhToan", ngayThanhToan },
                { "@diachi", txt_dc.Text },
                { "@TaiKhoanId", Session.user_id }
            };
            bool isSuccess = cm.ExecuteStoredProcedure("update_hoadon", parameters);

            if (isSuccess)
            {
                MessageBox.Show("Cập nhật hóa đơn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load_hoadon();
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // e.RowIndex là chỉ số hàng được double click
            {
                int hoaDonId = int.Parse(dataGridView3.Rows[e.RowIndex].Cells["HoaDonId"].Value.ToString());
                if (dataGridView3.Columns[e.ColumnIndex].Name == "btn_rs")
                {
                    // Nếu người dùng nhấn nút Hủy
                    // Gọi stored procedure toggle_trangthai_hoadon
                    Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@HoaDonId", hoaDonId }
                    };
                    bool isCancelled = cm.ExecuteStoredProcedure("toggle_trangthai_hoadon", parameters); // Giả sử ExecuteStoredProcedure là một phương thức chung trong class cm

                    if (isCancelled)
                    {
                        MessageBox.Show("Hóa đơn đã phục hồi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        load_hoadon();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi phục hồi hóa đơn", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // e.RowIndex là chỉ số hàng được double click
            {
                int hoaDonId = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["HoaDonId"].Value.ToString());
                // Lấy hàng được chọn
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string _payment = selectedRow.Cells["TrangThaiThanhToan"].Value?.ToString();
                if (_payment == "Chưa thanh toán")
                {
                    string tenKhachHang = selectedRow.Cells["TenKhachHang"].Value?.ToString();
                    foreach (DataRowView item in cbb_khach.Items)
                    {
                        if (item["HoTen"].ToString() == tenKhachHang)
                        {
                            cbb_khach.SelectedItem = item;
                            break;
                        }
                    }
                    dateTimePicker1.Value = Convert.ToDateTime(selectedRow.Cells["NgayLapHoaDon"].Value);
                    txt_sohieuhoadon.Text = selectedRow.Cells["HoaDonId"].Value?.ToString();
                    txt_mahd.Text = selectedRow.Cells["HopDongId"].Value?.ToString();
                    txt_csnht.Text = selectedRow.Cells["CSC"].Value?.ToString();
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "ThanhToan")
                {
                    // Nếu người dùng nhấn nút Thanh Toán
                    // Cập nhật trạng thái thanh toán trong cơ sở dữ liệu
                    string updateQuery = "UPDATE HoaDon SET TrangThaiThanhToan = 'Đã thanh toán' WHERE HoaDonId = @HoaDonId";
                    var parameters = new Dictionary<string, object>
                        {
                            { "@HoaDonId", hoaDonId }
                        };
                    bool isUpdated = cm.ExecuteQuery(updateQuery, parameters); // Giả sử ExecuteQuery là một phương thức chung trong class cm

                    if (isUpdated)
                    {
                        MessageBox.Show("Đã thanh toán hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        load_hoadon();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi cập nhật trạng thái thanh toán", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Huy")
                {
                    // Nếu người dùng nhấn nút Hủy
                    // Gọi stored procedure toggle_trangthai_hoadon
                    Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@HoaDonId", hoaDonId }
                    };
                    bool isCancelled = cm.ExecuteStoredProcedure("toggle_trangthai_hoadon", parameters); // Giả sử ExecuteStoredProcedure là một phương thức chung trong class cm

                    if (isCancelled)
                    {
                        MessageBox.Show("Hóa đơn đã bị hủy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        load_hoadon();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi hủy hóa đơn", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
            }
        }
        
       

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView2.Columns["btnIn"].Index && e.RowIndex >= 0)
            {
                // Lấy dữ liệu từ cột "Mã phiếu" (có thể là cột 1)
                int maPhieu = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[1].Value); // Lấy từ cột "Mã phiếu"
                // Kiểm tra xem maPhieu có giá trị hợp lệ hay không
                if (maPhieu > 0)
                {
                    DataTable hoadon = new DataTable("mothoadon");

                    hoadon = cm.LoadDataQuery("select * from view_hoadon_inactive where HoaDonId =" + maPhieu);

                    if (hoadon.Rows.Count > 0)
                    {
                        // Dữ liệu có, tiếp tục xử lý
                        rpt_mothoadon rpt_report_xn = new rpt_mothoadon();
                        rpt_report_xn.SetDataSource(hoadon);
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
                    MessageBox.Show("Dữ liệu không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["btnIn"].Index && e.RowIndex >= 0)
            {
                int maPhieu = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                if (maPhieu > 0)
                {
                    DataTable hoadon = new DataTable("mothoadon");

                    hoadon = cm.LoadDataQuery("select * from view_hoadon_active where HoaDonId =" + maPhieu);

                    if (hoadon.Rows.Count > 0)
                    {
                        // Dữ liệu có, tiếp tục xử lý
                        rpt_mothoadon rpt_report_xn = new rpt_mothoadon();
                        rpt_report_xn.SetDataSource(hoadon);
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
                    MessageBox.Show("Dữ liệu không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void báoCáoHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_baocao bc = new frm_baocao();
            bc.ShowDialog();
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_khachhang frm_kh = new frm_khachhang();
            frm_kh.ShowDialog();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_nhanvien fr_nv = new frm_nhanvien();
            fr_nv.ShowDialog();
        }

        private void hợpĐồngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_hopdong fr_hd = new frm_hopdong();
            fr_hd.ShowDialog();
        }

        private void bảngGiáToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_banggia bg = new frm_banggia();
            bg.ShowDialog();
        }

        private void btn_report_Click(object sender, EventArgs e)
        {
            frm_baocao bc = new frm_baocao();
            bc.ShowDialog();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            load_cbb_khach();
            load_hoadon();
        }
    }
}
