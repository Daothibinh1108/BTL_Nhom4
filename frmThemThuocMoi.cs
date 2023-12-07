using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Quan_Ly_Hieu_Thuoc
{
    public partial class frmThemThuocMoi : Form
    {
        connect con = new connect();
        SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-CU7TOL9\SQLEXPRESS;Initial Catalog=HieuThuoc;Integrated Security=True");
        SqlCommand cmd;
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        public frmThemThuocMoi()
        {
            InitializeComponent();
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            txtTenthuoc.Text = "";
            cbxNhacc.Text = "";
            cbDonvi.Text = "";
            txtDongGoi.Text = "";
            cbxhamluong.Text = "";
            txtgiaban.Text = "";
            txtGiaNhap.Text = "";
            txtSoluongton.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtTenthuoc.Text == "")
            {
                MessageBox.Show("Nhập tên thuốc!");
            }
            else if (txtDongGoi.Text == "")
            {
                MessageBox.Show("Nhập cách đóng gói!");
            }
            else if (txtGiaNhap.Text == "")
            {
                MessageBox.Show("Nhập giá nhập!");
            }
            else if (txtgiaban.Text == "")
            {
                MessageBox.Show("Chọn giá bán!");
            }
            else if (cbxhamluong.Text == "")
            {
                MessageBox.Show("Nhập hàm lượng!");
            }
            else if (cbDonvi.Text == "")
            {
                MessageBox.Show("Chọn đơn vị!");
            }
            else if (cbxNhacc.Text == "")
            {
                MessageBox.Show("Chọn nhà cung cấp!");
            }
            else if (txtSoluongton.Text == "")
            {
                MessageBox.Show("Nhập số lượng tồn!");
            }
            else
            {
                try
                {
                    cmd = new SqlCommand("insert into Thuoc values(@tenthuoc,@mancc," +
                    "@hamluong,@donggoi,@giaban,@gianhap,@madv,@soluongton)", sqlcon);
                    cmd.Parameters.AddWithValue("tenthuoc", txtTenthuoc.Text);
                    cmd.Parameters.AddWithValue("mancc", cbxNhacc.Text);
                    cmd.Parameters.AddWithValue("hamluong", cbxhamluong.Text);
                    cmd.Parameters.AddWithValue("donggoi", txtDongGoi.Text);
                    cmd.Parameters.AddWithValue("gianhap", txtGiaNhap.Text);
                    cmd.Parameters.AddWithValue("giaban", txtgiaban.Text);
                    cmd.Parameters.AddWithValue("madv", cbDonvi.Text);
                    cmd.Parameters.AddWithValue("soluongton", txtSoluongton.Text);
                    sqlcon.Open();
                    cmd.ExecuteNonQuery();
                    sqlcon.Close();
                    MessageBox.Show("Thành công");
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void frmThemThuocMoi_Load(object sender, EventArgs e)
        {
            dt1 = con.Execute("select * from DonVi");
            cbDonvi.DataSource = dt1;
            cbDonvi.DisplayMember = "madonvi";
            dt2 = con.Execute("select * from NhaCungCap");
            cbxNhacc.DataSource = dt2;
            cbxNhacc.DisplayMember = "mancc";
        }

        private void cbDonvi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                    sqlcon.Open();
                int dv;
                int.TryParse(cbDonvi.Text, out dv);
                string query = string.Format("select * from DonVi where  madonvi = {0}", dv);
                cmd = new SqlCommand(query, sqlcon);
                cmd.ExecuteNonQuery();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    txtDongGoi.Text = (string)rd["tendonvi"].ToString();
                }
                if (sqlcon.State == ConnectionState.Open)
                    sqlcon.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
