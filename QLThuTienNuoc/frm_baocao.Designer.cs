
namespace QLThuTienNuoc
{
    partial class frm_baocao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtp1 = new System.Windows.Forms.DateTimePicker();
            this.dtp2 = new System.Windows.Forms.DateTimePicker();
            this.btn_filter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbb_khach = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.cbb_trangthai = new System.Windows.Forms.ComboBox();
            this.btn_in = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtp1
            // 
            this.dtp1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp1.Location = new System.Drawing.Point(25, 68);
            this.dtp1.Name = "dtp1";
            this.dtp1.Size = new System.Drawing.Size(119, 20);
            this.dtp1.TabIndex = 0;
            // 
            // dtp2
            // 
            this.dtp2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp2.Location = new System.Drawing.Point(176, 68);
            this.dtp2.Name = "dtp2";
            this.dtp2.Size = new System.Drawing.Size(119, 20);
            this.dtp2.TabIndex = 0;
            // 
            // btn_filter
            // 
            this.btn_filter.Location = new System.Drawing.Point(322, 65);
            this.btn_filter.Name = "btn_filter";
            this.btn_filter.Size = new System.Drawing.Size(85, 23);
            this.btn_filter.TabIndex = 1;
            this.btn_filter.Text = "Lọc dữ liệu";
            this.btn_filter.UseVisualStyleBackColor = true;
            this.btn_filter.Click += new System.EventHandler(this.btn_filter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ngày tạo hóa đơn từ:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(173, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến:";
            // 
            // cbb_khach
            // 
            this.cbb_khach.FormattingEnabled = true;
            this.cbb_khach.Location = new System.Drawing.Point(479, 66);
            this.cbb_khach.Name = "cbb_khach";
            this.cbb_khach.Size = new System.Drawing.Size(152, 21);
            this.cbb_khach.TabIndex = 3;
            this.cbb_khach.SelectionChangeCommitted += new System.EventHandler(this.cbb_khach_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(476, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Lọc hóa đơn theo khách hàng:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(25, 123);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1091, 476);
            this.dataGridView1.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(672, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Lọc theo trạng thái thanh toán:";
            // 
            // cbb_trangthai
            // 
            this.cbb_trangthai.FormattingEnabled = true;
            this.cbb_trangthai.Items.AddRange(new object[] {
            "Chưa thanh toán",
            "Đã thanh toán"});
            this.cbb_trangthai.Location = new System.Drawing.Point(675, 66);
            this.cbb_trangthai.Name = "cbb_trangthai";
            this.cbb_trangthai.Size = new System.Drawing.Size(152, 21);
            this.cbb_trangthai.TabIndex = 3;
            this.cbb_trangthai.SelectionChangeCommitted += new System.EventHandler(this.cbb_trangthai_SelectionChangeCommitted);
            // 
            // btn_in
            // 
            this.btn_in.Location = new System.Drawing.Point(1031, 69);
            this.btn_in.Name = "btn_in";
            this.btn_in.Size = new System.Drawing.Size(85, 23);
            this.btn_in.TabIndex = 1;
            this.btn_in.Text = "In dữ liệu";
            this.btn_in.UseVisualStyleBackColor = true;
            this.btn_in.Click += new System.EventHandler(this.btn_in_Click);
            // 
            // frm_baocao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 631);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cbb_trangthai);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbb_khach);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_in);
            this.Controls.Add(this.btn_filter);
            this.Controls.Add(this.dtp2);
            this.Controls.Add(this.dtp1);
            this.Name = "frm_baocao";
            this.Text = "frm_baocao";
            this.Load += new System.EventHandler(this.frm_baocao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtp1;
        private System.Windows.Forms.DateTimePicker dtp2;
        private System.Windows.Forms.Button btn_filter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbb_khach;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbb_trangthai;
        private System.Windows.Forms.Button btn_in;
    }
}