using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Hieu_Thuoc
{
    public partial class frmTKBanHang : Form
    {
        connect con = new connect();
        string str = "";
        DataTable dt = new DataTable();
        public frmTKBanHang()
        {
            InitializeComponent();
        }
        private void frmTKBanHang_Load(object sender, EventArgs e)
        {
            str = "select * from HoaDon";
            dt = con.Execute(str);
            grvTK.DataSource = dt;

        }

        private void cbTenthuoc_CheckedChanged_1(object sender, EventArgs e)
        {
            if (cbTenthuoc.Checked == true)
            {
                btnTimkiem.Enabled = true;
                txtTenthuoc.Enabled = true;
            }
            else
            {
                txtTenthuoc.Text = "";
                txtTenthuoc.Enabled = false;
                btnTimkiem.Enabled = false;
                frmTKBanHang_Load(sender, e);
            }
        }

        private void cbthoigian_CheckedChanged(object sender, EventArgs e)
        {
            if (cbthoigian.Checked == true)
            {
                btnTimkiem.Enabled = true;
                dtTime.Enabled = true;
            }
            else
            {
                dtTime.Enabled = false;
                frmTKBanHang_Load(sender, e);
            }
        }

        private void cbTenKH_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTenKH.Checked == true)
            {
                btnTimkiem.Enabled = true;
                txtTenKH.Enabled = true;
            }
            else
            {
                txtTenKH.Text = "";
                txtTenKH.Enabled = false;
                btnTimkiem.Enabled = false;
                frmTKBanHang_Load(sender, e);
            }
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if(cbTenthuoc.Checked == true)
            {
                str = "select * from HoaDon where tenthuoc = '" + txtTenthuoc.Text + "'";
                dt = con.Execute(str);
                grvTK.DataSource = dt;
            }
            else if(cbTenKH.Checked == true)
            {
                str = string.Format("select * from HoaDon where tenkh = N'{0}'", txtTenKH.Text);
                dt = con.Execute(str);
                grvTK.DataSource = dt;
            }
            else if (cbthoigian.Checked == true)
            {
                str = "select * from HoaDon where thoigian = '" + dtTime.Text + "'";
                dt = con.Execute(str);
                grvTK.DataSource = dt;
            }
        }
    }
}
