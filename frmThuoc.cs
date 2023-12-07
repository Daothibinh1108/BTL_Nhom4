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
    public partial class frmThuoc : Form
    {
        public frmThuoc()
        {
            InitializeComponent();
        }
        private Form activeForm = null;
        private void formDe(Form mainForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = mainForm;
            mainForm.TopLevel = false;
            mainForm.FormBorderStyle = FormBorderStyle.None;
            mainForm.Dock = DockStyle.Fill;
            panelThuoc.Controls.Add(mainForm);
            panelThuoc.Tag = mainForm;
            mainForm.BringToFront();
            mainForm.Show();
        }

        private void btnThuco_Click(object sender, EventArgs e)
        {
            formDe(new frmQLthuoc());
        }

        private void btnNhapthemthuoc_Click(object sender, EventArgs e)
        {
            formDe(new frmThemThuocMoi());
        }

        private void sadasdToolStripMenuItem_Click(object sender, EventArgs e)
        {
           menuStrip1.Hide();
            formDe(new frmThuoc());
        }
        DonViTinh dvt = new DonViTinh();
        private void frmThuoc_Load(object sender, EventArgs e)
        {
            DataTable tb = dvt.DSDonvitinh();
            gridViewDonViTinh.DataSource = tb;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(txttendonvi.Text == "")
            {
                MessageBox.Show("Nhập tên đơn vị!");
            }
            else
            {
                dvt.AddDVT(txttendonvi.Text);
                MessageBox.Show("Thêm thành công");
                frmThuoc_Load(sender, e);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txttendonvi.Text == "")
            {
                MessageBox.Show("Nhập tên đơn vị!");
            }
            else if (txtmadv.Text == "")
            {
                MessageBox.Show("Chọn mã để sửa!");
            }
            else
            {
                dvt.EditDVT(txttendonvi.Text, int.Parse(txtmadv.Text));
                MessageBox.Show("Sửa thành công");
                frmThuoc_Load(sender, e);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(txtmadv.Text == "")
            {
                MessageBox.Show("Chọn mã để xóa!");
            }
            else
            {
                dvt.DelDVT(int.Parse(txtmadv.Text));
                MessageBox.Show("Xóa thành công");
                frmThuoc_Load(sender, e);
            }
        }

        private void gridViewDonViTinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = gridViewDonViTinh.Rows[e.RowIndex];
            txtmadv.Text = Convert.ToString(row.Cells["Mã đơn vị"].Value);
            txttendonvi.Text = Convert.ToString(row.Cells["Tên đơn vị"].Value);
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (cbTKmadv.Checked)
            {
                if(txtmadvs.Text == "")
                {
                    MessageBox.Show("Nhập mã để tìm!");
                }
                else
                {
                    DataTable tb = dvt.searchMaDV(int.Parse(txtmadvs.Text));
                    gridViewDonViTinh.DataSource = tb;
                }
            }
            if (cbTKtendv.Checked)
            {
                if (txttendvs.Text == "")
                {
                    MessageBox.Show("Nhập tên để tìm!");
                }
                else 
                {
                    DataTable tb = dvt.searchTenDV(txttendvs.Text);
                    gridViewDonViTinh.DataSource = tb;
                }
            }
        }

        private void cbTKmaloai_CheckedChanged(object sender, EventArgs e)
        {
            
            if (cbTKmadv.Checked)
            {
                btnTimkiem.Enabled = true;
                txtmadvs.Enabled = true;
            }
            else
            {
                btnTimkiem.Enabled = false;
                txtmadvs.Enabled = false;
            }
        }

        private void cbTKtendv_CheckedChanged_1(object sender, EventArgs e)
        {
            if (cbTKtendv.Checked)
            {
                btnTimkiem.Enabled = true;
                txttendvs.Enabled = true;
            }
            else
            {
                btnTimkiem.Enabled = false;
                txttendvs.Enabled = false;
            }
        }
    }
}
