using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QLThuTienNuoc.model
{
    public class common
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        public common()
        {

        }
        public DataTable LoadDataQuery(string query)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
        public void LoadComboBoxWithQuery(ComboBox comboBox, string query, string displayMember, string valueMember)
        {
            DataTable data = LoadDataQuery(query);
            if (data != null)
            {
                comboBox.DataSource = data;
                comboBox.DisplayMember = displayMember;  // Cột hiển thị trong ComboBox
                comboBox.ValueMember = valueMember;      // Cột giá trị trong ComboBox
            }
        }
        public void LoadDataToGridView(DataGridView dgv, string query)
        {
            try
            {
                // Lấy dữ liệu từ cơ sở dữ liệu
                DataTable dataTable = LoadDataQuery(query);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    // Gắn dữ liệu vào DataGridView
                    dgv.DataSource = dataTable;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void FixHeader(DataGridView dgv, params string[] columnNames)
        {
            try
            {
                // Kiểm tra xem số lượng tên cột có khớp với số lượng cột trong DataGridView không
                if (dgv.Columns.Count == columnNames.Length)
                {
                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        dgv.Columns[i].HeaderText = columnNames[i];
                    }
                }
                else
                {
                    MessageBox.Show("Số lượng tiêu đề cột không khớp với số lượng cột trong DataGridView.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thay đổi tiêu đề cột: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
        public void AddPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;

            textBox.GotFocus += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            textBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                }
            };
        }

        /*
         bool isDuplicate = CheckDuplicateUpdate("KhachHang", "SoDienThoai", newPhoneNumber, "KhachHangId", khachHangId);
        if (isDuplicate)
        {
            MessageBox.Show("Số điện thoại đã tồn tại trong hệ thống.");
        }
         */
        public bool CheckDuplicateUpdate(string table, string column, string value, string idColumn, int id)
        {
            // Câu truy vấn kiểm tra: kiểm tra số điện thoại có tồn tại và không phải của chính người đang cập nhật
            string query = $"SELECT COUNT(*) FROM {table} WHERE {column} = @Value AND {idColumn} != @Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm tham số vào truy vấn
                        command.Parameters.AddWithValue("@Value", value);   // Tham số cho giá trị cần kiểm tra
                        command.Parameters.AddWithValue("@Id", id);         // Tham số cho ID của người cần cập nhật

                        // Thực thi truy vấn và lấy kết quả
                        int count = Convert.ToInt32(command.ExecuteScalar());

                        return count > 0; // Trả về true nếu số điện thoại đã tồn tại và không phải của chính người đó
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public bool ExecuteQuery(string query, Dictionary<string, object> parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;

                    // Thêm các tham số vào câu lệnh
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery(); // Thực thi câu lệnh và trả về số dòng bị ảnh hưởng

                    // Kiểm tra nếu có dòng bị ảnh hưởng
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Bạn có thể log lỗi ở đây nếu cần thiết
                MessageBox.Show($"Lỗi khi thực thi truy vấn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public bool ExecuteStoredProcedure(string procedureName, Dictionary<string, object> parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thực thi thủ tục {procedureName}: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public string ExecuteScalar(string query, Dictionary<string, object> parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Thêm tham số nếu có
                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                            }
                        }
                        object result = cmd.ExecuteScalar();

                        return result != null ? result.ToString() : null;
                    }
                }    
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thực thi truy vấn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public bool login(string _taikhoan, string _matkhau)
        {
            // Câu truy vấn kiểm tra
            string query = $"SELECT COUNT(*) FROM nhanvien WHERE tendangnhap = @_taikhoan and matkhau = @_matkhau";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm tham số vào truy vấn
                        command.Parameters.AddWithValue("@_taikhoan", _taikhoan);
                        command.Parameters.AddWithValue("@_matkhau", _matkhau);

                        // Thực thi truy vấn và lấy dữ liệu
                        int count = Convert.ToInt32(command.ExecuteScalar());

                        // Kiểm tra nếu có dữ liệu trả về
                        if (count > 0)
                        {
                            // Gắn thông tin vào Session (giả sử bạn có thêm câu truy vấn khác để lấy thông tin chi tiết của người dùng)
                            string queryUserInfo = "SELECT TaiKhoanId, hoten, chucvu FROM nhanvien WHERE TenDangNhap = @_taikhoan and MatKhau = @_matkhau";
                            using (SqlCommand cmd = new SqlCommand(queryUserInfo, connection))
                            {
                                cmd.Parameters.AddWithValue("@_taikhoan", _taikhoan);
                                cmd.Parameters.AddWithValue("@_matkhau", _matkhau);

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        // Gắn thông tin vào Session
                                        Session.user_id = reader.GetInt32(0);  // nhanvienid
                                        Session.name = reader.GetString(1);    // hoten
                                        Session.chucvu = reader.GetString(2);  // chucvu
                                    }
                                }
                            }
                            return true;  // Đăng nhập thành công
                        }
                        else
                        {
                            return false;  // Đăng nhập thất bại
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;  // Lỗi kết nối hoặc vấn đề khác
            }
        }
    }
}
