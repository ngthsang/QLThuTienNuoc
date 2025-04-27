using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLThuTienNuoc
{
    public partial class frm_config : Form
    {
        public frm_config()
        {
            InitializeComponent();
            this.Load += frm_config_Load;
        }

        private void frm_config_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this, Color.LightBlue, Color.Yellow, Color.Maroon, Color.LightGreen);
        }
    }
}
