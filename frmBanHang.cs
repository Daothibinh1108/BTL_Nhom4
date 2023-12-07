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

namespace Quan_Ly_Hieu_Thuoc
{
    public partial class frmBanHang : Form
    {
        public frmBanHang()
        {
            InitializeComponent();
        }
        connect con = new connect();
        DataTable dt1 = new DataTable();
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-A5LD9LVE\SQLEXPRESS;Initial Catalog=HieuThuoc04_N05;Integrated Security=True");
        SqlCommand cmd;
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if(cbxChonthuoc.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn thuốc");
            }
            else if(txtSoLuong.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số lượng bán");
            }
            else if (txtKhachhang.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên khách hàng");
            }
            else
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    cmd = new SqlCommand("insert into HoaDon values(@thoigian, @mathuoc, @tenthuoc, @tenkh, @soluongban, @donvi, @giaban, @thanhtien)", conn);
                    cmd.Parameters.AddWithValue("thoigian", txtngayban.Text);
                    cmd.Parameters.AddWithValue("mathuoc", txtmathuoc.Text);
                    cmd.Parameters.AddWithValue("tenthuoc", cbxChonthuoc.Text);
                    cmd.Parameters.AddWithValue("tenkh", txtKhachhang.Text);
                    cmd.Parameters.AddWithValue("soluongban", txtSoLuong.Text);
                    cmd.Parameters.AddWithValue("donvi", txtmdv.Text);
                    cmd.Parameters.AddWithValue("giaban", txtGiaban.Text);
                    cmd.Parameters.AddWithValue("thanhtien", txtThanhtien.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thành công!");
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    ListViewItem item = new ListViewItem(txtmathuoc.Text);
                    item.SubItems.Add(cbxChonthuoc.Text);
                    item.SubItems.Add(txtngayban.Text);
                    item.SubItems.Add(txtGiaban.Text);
                    item.SubItems.Add(txtSoLuong.Text);
                    item.SubItems.Add(txtdonvi.Text);
                    item.SubItems.Add(txtThanhtien.Text);
                    item.SubItems.Add(txtKhachhang.Text);
                    listView1.Items.Add(item);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            
        }
        private void frmBanHang_Load(object sender, EventArgs e)
        {
            txtngayban.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dt1=con.Execute("select * from Thuoc");
            cbxChonthuoc.DataSource = dt1;
            cbxChonthuoc.DisplayMember = "tenthuoc";
        }

        private void cbxChonthuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                cmd = new SqlCommand("select * from Thuoc inner join DonVi on Thuoc.madv=DonVi.madonvi where tenthuoc = '" + cbxChonthuoc.Text + "'", conn);
                cmd.ExecuteNonQuery();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    txtmathuoc.Text = (string)rd["mathuoc"].ToString();
                    txtGiaban.Text = (string)rd["giaban"].ToString();
                    txtdonvi.Text = (string)rd["tendonvi"].ToString();
                    txtmdv.Text = (string)rd["madonvi"].ToString();
                }
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            if (txtSoLuong.Text != "")
            {
                int soluong = int.Parse(txtSoLuong.Text);
                float thanhtien;
                float giaban = int.Parse(txtGiaban.Text);
                thanhtien = soluong * giaban;
                txtThanhtien.Text = thanhtien.ToString();
            }
        }
    }
}
