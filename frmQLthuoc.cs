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
    public partial class frmQLthuoc : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=HieuThuoc04_N05;Integrated Security=True");
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter adt;
        string query = "";
        public frmQLthuoc()
        {
            InitializeComponent();
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            txtMathuoc.Text = "";
            txtTenthuoc.Text = "";
            txtMancc.Text = "";
            txtHamluong.Text = "";
            txtDonggoi.Text = "";
            txtMaDV.Text = "";
            txtGiaban.Text = "";
            txtGianhap.Text = "";
            txttonkho.Text = "";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                query = string.Format("delete from Thuoc where mathuoc = {0}", Convert.ToInt32(txtMathuoc.Text));
                cmd = new SqlCommand(query, sqlcon);
                sqlcon.Open();
                cmd.ExecuteNonQuery();
                sqlcon.Close();
                frmQLthuoc_Load_1(sender, e);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if(cbmathuoc.Checked == true)
            {
                sqlcon.Open();
                query = "select * from Thuoc where mathuoc = '" + int.Parse(txtMathuocS.Text) + "'";
                cmd = new SqlCommand(query, sqlcon);
                cmd.CommandType = CommandType.Text;
                adt = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adt.Fill(dt);
                sqlcon.Close();
                grv1.DataSource = dt;
            }
            else if (cbTenthuoc.Checked == true)
            {
                sqlcon.Open();
                query = "select * from Thuoc where tenthuoc = '" + txtTenthuocS.Text + "'";
                cmd = new SqlCommand(query, sqlcon);
                cmd.CommandType = CommandType.Text;
                adt = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adt.Fill(dt);
                sqlcon.Close();
                grv1.DataSource = dt;
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                query = string.Format("update Thuoc set tenthuoc = N'{0}', mancc = {1}," +
                    "hamluong = N'{2}',donggoi = N'{3}',giaban = N'{4}',gianhap = N'{5}',madv = {6}," +
                    "soluongton = {7} where mathuoc = {8}", txtTenthuoc.Text, Convert.ToInt32(txtMancc.Text), txtHamluong.Text, txtDonggoi.Text,Convert.ToInt32(txtGiaban.Text),Convert.ToInt32(txtGianhap.Text), Convert.ToInt32(txtMaDV.Text),Convert.ToInt32(txttonkho.Text),Convert.ToInt32(txtMathuoc.Text));
                cmd = new SqlCommand(query, sqlcon);
                sqlcon.Open();
                cmd.ExecuteNonQuery();
                sqlcon.Close();
                frmQLthuoc_Load_1(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmQLthuoc_Load_1(object sender, EventArgs e)
        {
            try
            {
                sqlcon.Open();
                query = "select mathuoc as 'Mã thuốc', tenthuoc as 'Tên thuốc', mancc as 'Mã NCC', hamluong as 'Hàm lượng'," +
                    "donggoi as 'Đóng gói', giaban as 'Giá bán', gianhap as 'Giá nhập', madv as 'Mã đơn vị', soluongton as 'Tồn kho' from Thuoc";
                cmd = new SqlCommand(query, sqlcon);
                cmd.CommandType = CommandType.Text;
                adt = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adt.Fill(dt);
                grv1.DataSource = dt;
                sqlcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow r = new DataGridViewRow();
            r = grv1.Rows[e.RowIndex];
            txtMathuoc.Text = Convert.ToString(r.Cells[0].Value);
            txtTenthuoc.Text = Convert.ToString(r.Cells[1].Value);
            txtMancc.Text = Convert.ToString(r.Cells[2].Value);
            txtHamluong.Text = Convert.ToString(r.Cells[3].Value);
            txtDonggoi.Text = Convert.ToString(r.Cells[4].Value);
            txtGiaban.Text = Convert.ToString(r.Cells[5].Value);
            txtGianhap.Text = Convert.ToString(r.Cells[6].Value);
            txtMaDV.Text = Convert.ToString(r.Cells[7].Value);
            txttonkho.Text = Convert.ToString(r.Cells[8].Value);
        }

        private void cbTenthuoc_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTenthuoc.Checked == true)
            {
                txtTenthuocS.Enabled = true;
                btnTimkiem.Enabled = true;
            }
            else
            {
                txtTenthuocS.Text = string.Empty;
                btnTimkiem.Enabled = false;
                txtTenthuocS.Enabled = false;
                frmQLthuoc_Load_1(sender, e);
            }
        }

        private void cbmathuoc_CheckedChanged(object sender, EventArgs e)
        {
            if (cbmathuoc.Checked == true)
            {
                txtMathuocS.Enabled = true;
                btnTimkiem.Enabled = true;
            }
            else
            {
                txtMathuocS.Text = string.Empty;
                btnTimkiem.Enabled = false;
                txtMathuocS.Enabled = false;
                frmQLthuoc_Load_1(sender, e);
            }
        }
    }
}
