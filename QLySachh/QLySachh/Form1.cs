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

namespace QLySachh
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GN3V8MM\DUONG;Initial Catalog=QLsach;Integrated Security=True");
        private void KetNoiCSDL()
        {
            string sql = "select * from SanPham"; //lấy hết dữ liệu trong bảng hàng tồn
            SqlCommand com = new SqlCommand(sql, con);// chúng ta bắt đầu truy vấn
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);//chuyển dữ liệu về
            SqlDataReader dr = com.ExecuteReader();
            DataTable dt = new DataTable();
            //tạo kho  ảo để lưu dữ liệu
            dt.Load(dr);//đổ dữ liệu vào kho
            dataGridViewSP.DataSource = dt;
        }

        private void LoadData()
        {
            txtMaSP.DataBindings.Clear();
            txtMaSP.DataBindings.Add("Text", dataGridViewSP.DataSource, "MaSP");
            txtTenSP.DataBindings.Clear();
            txtTenSP.DataBindings.Add("Text", dataGridViewSP.DataSource, "TenSP");
            txtSL.DataBindings.Clear();
            txtSL.DataBindings.Add("Text", dataGridViewSP.DataSource, "SoLuong");
            txtGia.DataBindings.Clear();
            txtGia.DataBindings.Add("Text", dataGridViewSP.DataSource, "Gia");
            txtMaDM.DataBindings.Clear();
            txtMaDM.DataBindings.Add("Text", dataGridViewSP.DataSource, "MaDanhMuc");
            txtMaNXB.DataBindings.Clear();
            txtMaNXB.DataBindings.Add("Text", dataGridViewSP.DataSource, "MaNXB");
            txtMaMT.DataBindings.Clear();
            txtMaMT.DataBindings.Add("Text", dataGridViewSP.DataSource, "MaMT");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool masp()
        {
            if (txtMaSP.Text == "")
            {
                MessageBox.Show("Mã sản phẩm không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    
        private bool kiemtra()
        {
            if (masp())
            {
                return true;
            }
            return false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            con.Open();//chúng ta mở kết nối
            KetNoiCSDL();//gọi lại hàm kết nối
            LoadData();//Gọi lại hàm load dữ lieu
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String Them = "INSERT INTO SanPham VALUES (@MaSP,@TenSP,@SoLuong,@Gia,@MaDanhMuc,@MaNXB,@MaMT)";
            SqlCommand add = new SqlCommand(Them, con);
            if (kiemtra())
            {
                add.Parameters.AddWithValue("MaSP", txtMaSP.Text);
                add.Parameters.AddWithValue("TenSP", txtTenSP.Text);
                add.Parameters.AddWithValue("SoLuong", txtSL.Text);
                add.Parameters.AddWithValue("Gia", txtGia.Text);
                add.Parameters.AddWithValue("MaDanhMuc", txtMaDM.Text);
                add.Parameters.AddWithValue("MaNXB", txtMaNXB.Text);
                add.Parameters.AddWithValue("MaMT", txtMaMT.Text);
            }
            try
            {
                add.ExecuteNonQuery();
                MessageBox.Show("Thành công", "Xuất hàng trong kho", MessageBoxButtons.OK, MessageBoxIcon.Information);
                KetNoiCSDL();
                LoadData();
                btnSave.Enabled = true;
                btnSua.Enabled = true;
            }
            catch (SqlException exc)
            {
                if (exc.Number == 2627)
                {
                    MessageBox.Show("Mã hàng đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Lỗi không xác định:\n" + exc.Message, "Lỗi" + exc.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            btnSave.Enabled = false;
            btnXoa.Enabled = true;
        }
        private void Clear()
        {
            txtMaSP.Text = "";
            txtGia.Text = "";
            txtMaDM.Text = "";
            txtMaMT.Text = "";
            txtMaNXB.Text = "";
            txtTenSP.Text = "";
            txtSL.Text = "";
            
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            Clear();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;    
            btnSave.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            String Sua = "UPDATE SanPham SET TenSP = @TenSP,SoLuong = @SoLuong,Gia = @Gia,MaDanhMuc = @MaDanhMuc,MaNXB = @MaNXB,MaMT = @MaMT WHERE MaSP = @MaSP";
            SqlCommand upd = new SqlCommand(Sua, con);
            if (kiemtra())
            {
                upd.Parameters.AddWithValue("MaSP", txtMaSP.Text);
                upd.Parameters.AddWithValue("TenSP", txtTenSP.Text);
                upd.Parameters.AddWithValue("SoLuong", txtSL.Text);
                upd.Parameters.AddWithValue("Gia", txtGia.Text);
                upd.Parameters.AddWithValue("MaDanhMuc", txtMaDM.Text);
                upd.Parameters.AddWithValue("MaNXB", txtMaNXB.Text);
                upd.Parameters.AddWithValue("MaMT", txtMaMT.Text);
            }
            try
            {
                upd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công", "Sửa thông tin sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                KetNoiCSDL();
                LoadData();
            }
            catch (SqlException exc)
            {
                if (exc.Number == 2627)
                {
                    MessageBox.Show("Mã sản phẩm đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Lỗi không xác định:\n" + exc.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            String Sua = "DELETE FROM SanPham WHERE MaSP = @MaSP";
            SqlCommand del = new SqlCommand(Sua, con);
            if (kiemtra())
            {
                del.Parameters.AddWithValue("MaSP", txtMaSP.Text);
                del.Parameters.AddWithValue("TenSP", txtTenSP.Text);
                del.Parameters.AddWithValue("SoLuong", txtSL.Text);
                del.Parameters.AddWithValue("Gia", txtGia.Text);
                del.Parameters.AddWithValue("MaDanhMuc", txtMaDM.Text);
                del.Parameters.AddWithValue("MaNXB", txtMaNXB.Text);
                del.Parameters.AddWithValue("MaMT", txtMaMT.Text);
                try
                {
                    del.ExecuteNonQuery();
                    MessageBox.Show("Xóa thành công", "Xóa nhà cung cấp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    KetNoiCSDL();
                    LoadData();
                }
                catch (SqlException exc)
                {
                    MessageBox.Show("Lỗi không xác định:\n" + exc.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Chọn sản phẩm cần xóa", "Xóa hàng", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            String Timkiem = "SELECT * FROM SanPham WHERE TenSP LIKE '%'+@TenSP+'%'";
            SqlCommand find = new SqlCommand(Timkiem, con);
            find.Parameters.AddWithValue("MaSP", txtMaSP.Text);
            find.Parameters.AddWithValue("TenSP", txtTimKiem.Text);
            find.Parameters.AddWithValue("SoLuong", txtSL.Text);
            find.Parameters.AddWithValue("Gia", txtGia.Text);
            find.Parameters.AddWithValue("MaDanhMuc", txtMaDM.Text);
            find.Parameters.AddWithValue("MaNXB", txtMaNXB.Text);
            find.Parameters.AddWithValue("MaMT", txtMaMT.Text);
            SqlDataReader dr = find.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewSP.DataSource = dt;
            LoadData();
        }
    }
}
