using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab6.DB;

namespace Lab6
{
    public partial class frmMain : Form
    {
        Model1 db = new Model1();
        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {

                List<LoaiSach> loaiSaches = db.LoaiSach.ToList();
                List<Sach> saches = db.Sach.ToList(); //lấy sinh viên
                FillFalcultyCombobox(loaiSaches);
                BindGrid(saches);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FillFalcultyCombobox(List<LoaiSach> loaiSaches)
        {
            this.cmbTheloai.DataSource = loaiSaches;
            this.cmbTheloai.DisplayMember = "TenLoai";
            this.cmbTheloai.ValueMember = "MaLoai";
        }
        private void BindGrid(List<Sach> saches)
        {
            dgvBook.Rows.Clear();

            foreach (var item in saches)
            {
                int index = dgvBook.Rows.Add();
                dgvBook.Rows[index].Cells[0].Value = item.MaSach;
                dgvBook.Rows[index].Cells[1].Value = item.TenSach;
                dgvBook.Rows[index].Cells[2].Value = item.NamXB;
                dgvBook.Rows[index].Cells[3].Value = item.LoaiSach.TenLoai;

            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (txtMaSach.Text == "" || txtTenSach.Text == "" || txtNamXB.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin sách!");
                    return;
                }
                if (txtMaSach.Text.Length != 6)
                {
                    MessageBox.Show("Mã sách phải có 6 ký tự!");
                    return;
                }
                Sach s = new Sach();
                s.MaSach = txtMaSach.Text;
                s.TenSach = txtTenSach.Text;
                s.NamXB = int.Parse(txtNamXB.Text);
                s.MaLoai = int.Parse(cmbTheloai.SelectedValue.ToString());
                db.Sach.Add(s);
                db.SaveChanges();
                MessageBox.Show("Thêm mới thành công!");
                List<Sach> saches = db.Sach.ToList();
                BindGrid(saches);
                txtMaSach.Text = "";
                txtTenSach.Text = "";
                txtNamXB.Text = "";
                cmbTheloai.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaSach.Text == "" || txtTenSach.Text == "" || txtNamXB.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin sách!");
                    return;
                }
                if (txtMaSach.Text.Length != 6)
                {
                    MessageBox.Show("Mã sách phải có 6 ký tự!");
                    return;
                }
                Sach s = db.Sach.Find(txtMaSach.Text);
                s.TenSach = txtTenSach.Text;
                s.NamXB = int.Parse(txtNamXB.Text);
                s.MaLoai = int.Parse(cmbTheloai.SelectedValue.ToString());
                db.SaveChanges();
                MessageBox.Show("Cập nhật thành công!");
                List<Sach> saches = db.Sach.ToList();
                BindGrid(saches);
                txtMaSach.Text = "";
                txtTenSach.Text = "";
                txtNamXB.Text = "";
                cmbTheloai.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void dgvBook_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //người dùng chọn 1 dòng thì thể hiện ngược lại thông tin của các sinh viên đã chọn ở phần nhập liệu(bên trái).

            if (e.RowIndex >= 0)

            {
                // Lấy dữ liệu của dòng được chọn
                DataGridViewRow selectedRow = dgvBook.Rows[e.RowIndex];

                // Gán giá trị từ DataGridView vào các trường nhập liệu
                txtMaSach.Text = selectedRow.Cells[0].Value.ToString();
                txtTenSach.Text = selectedRow.Cells[1].Value.ToString();
                cmbTheloai.Text = selectedRow.Cells[3].Value.ToString();
                txtNamXB.Text = selectedRow.Cells[2].Value.ToString();
                //hiển thị hình ảnh đại diện của sinh viên


            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Khi Click vào nút xóa (1d)

            // Nếu mã sách muốn xóa đã tồn tại trong CSDL, Hiển thị cảnh báo YES / NO “Bạn có muốn xóa không ?” (0.25đ). Nhấn YES: Xóa dữ liệu sách đã chọn(0.25đ) và cập nhật lại DataGridView(0.25đ)
            // Nếu mã sách muốn xóa không tồn tại trong CSDL, Hiển thị thông báo “Mã sách không tồn tại” (0.25đ)
            try
            {
                Sach s = db.Sach.Find(txtMaSach.Text);
                if (s != null)
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa không?", "Xóa sách", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        db.Sach.Remove(s);
                        db.SaveChanges();
                        MessageBox.Show("Xóa thành công!");
                        List<Sach> saches = db.Sach.ToList();
                        BindGrid(saches);
                        txtMaSach.Text = "";
                        txtTenSach.Text = "";
                        txtNamXB.Text = "";
                        cmbTheloai.SelectedIndex = 0;
                    }
                }
                else
                {
                    MessageBox.Show("Mã sách không tồn tại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //ìm kiếm theo mã sách, tên sách hoặc năm xuất bản nếu có chứa chuỗi tìm kiếm vừa nhập liệu(0.5 đ)
            //Hiển thị kết quả tìm kiếm trong DataGridView(0.5 đ)
            //không phân biệt chữ hoa chữ thường(0.5 đ)
            try
            {
                List<Sach> saches = db.Sach.ToList();
                List<Sach> sachesSearch = new List<Sach>();
                foreach (var item in saches)
                {
                    if (item.MaSach.ToLower().Contains(txtSearch.Text.ToLower()) || item.TenSach.ToLower().Contains(txtSearch.Text.ToLower()) || item.NamXB.ToString().Contains(txtSearch.Text))
                    {
                        sachesSearch.Add(item);
                    }
                }
                BindGrid(sachesSearch);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void thốngKêTheoNămToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // gửi tới form reprots
            reports frm = new reports();
            frm.ShowDialog();




        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
