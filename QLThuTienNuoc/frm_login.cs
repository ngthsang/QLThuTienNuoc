using QLThuTienNuoc.model;
using System;
using System.Windows.Forms;

namespace QLThuTienNuoc
{
    public partial class frm_login : frm_config
    {
        public frm_login()
        {
            InitializeComponent();
        }
        common cm = new common();
        private void frm_login_Load(object sender, EventArgs e)
        {
            cm.AddPlaceholder(txt_tk, "Nhập tài khoản của bạn");
            cm.AddPlaceholder(txt_mk, "Nhập mật khẩu của bạn");
        }

        private void btn_dangnhap_Click(object sender, EventArgs e)
        {
            if(cm.login(txt_tk.Text.Trim(), txt_mk.Text.Trim())== true)
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                frm_home h = new frm_home(Session.chucvu);
                h.Show();
            }    
            else
            {
                MessageBox.Show("Tên tài khoản hoặc mật khẩu không chính xác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    

        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
