using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace Game_Xep_Hinh
{
    public partial class frmChinh : Form
    {
        private int So_Hang = 22, So_Cot = 12, Kich_Thuoc = 20, So_Mau = 8, Thua, Time_Thua, So_Time_Thua = 50, Cho_Phep, Dinh_Hang, Dinh_Hinh, Time_MacDinh, Time_Max = 20, Nhap_Nhay, Tang_Toc = 5, CapDo_Max = 100;
        private int Phut, Giay, Choi_Nhac, Diem, Diem_Max = 600, Cap_Do, So_Hinh = 5, So_Gach = 4, ran_Mau, ran_Hinh, Loai_Hinh, Loai_Mau, Dai_Hinh, Bat_Dau, Xuong_Di, Dang_Choi, Tiep;
        private int[,] Mau_Gach, Trang_Thai, Mau_Cu;
        private int[] ViTri_X, ViTri_Y, arrHang_An;
        private Rectangle[,] Gach;
        private Rectangle[] Gach_Tiep;
        private SolidBrush Co_Ve_Xoa, Co_Ve_Mau, Co_Ve_Tiep;
        private Graphics Gra, Gra_Tiep;
        private Random ran;
        private string Duong_Dan = System.IO.Directory.GetCurrentDirectory() + "\\Da_Phuong_Tien\\";

        public frmChinh()
        {
            InitializeComponent();
        }

        private void frmChinh_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = new Bitmap(Duong_Dan + "Anh\\Nen.png");
            Gach = new Rectangle[So_Hang, So_Cot]; Trang_Thai = new int[So_Hang, So_Cot]; Mau_Cu = new int[So_Hang, So_Cot];
            Gach_Tiep = new Rectangle[So_Gach]; ViTri_X = new int[So_Gach]; ViTri_Y = new int[So_Gach];
            ran = new Random(); arrHang_An = new int[4];
            for (int i = 0; i < So_Hang; i++)
            {
                for (int j = 0; j < So_Cot; j++)
                {
                    Gach[i, j] = new Rectangle((j + 1) * Kich_Thuoc, i * Kich_Thuoc, Kich_Thuoc, Kich_Thuoc);
                    Trang_Thai[i, j] = 0; Mau_Cu[i, j] = -1;
                }
            }

            #region Mau gach

            Mau_Gach = new int[So_Mau, 3];
            Mau_Gach[0, 0] = 253; Mau_Gach[0, 1] = 217; Mau_Gach[0, 2] = 41;
            Mau_Gach[1, 0] = 237; Mau_Gach[1, 1] = 33; Mau_Gach[1, 2] = 36;
            Mau_Gach[2, 0] = 63; Mau_Gach[2, 1] = 195; Mau_Gach[2, 2] = 216;
            Mau_Gach[3, 0] = 84; Mau_Gach[3, 1] = 168; Mau_Gach[3, 2] = 72;
            Mau_Gach[4, 0] = 127; Mau_Gach[4, 1] = 55; Mau_Gach[4, 2] = 137;
            Mau_Gach[5, 0] = 202; Mau_Gach[5, 1] = 123; Mau_Gach[5, 2] = 180;
            Mau_Gach[6, 0] = 248; Mau_Gach[6, 1] = 151; Mau_Gach[6, 2] = 44;
            Mau_Gach[7, 0] = 22; Mau_Gach[7, 1] = 72; Mau_Gach[7, 2] = 143;

            #endregion

            Co_Ve_Xoa = new SolidBrush(Color.Black);
            Co_Ve_Mau = new SolidBrush(Color.Black); Co_Ve_Tiep = new SolidBrush(Color.Black);//khoi tao tranh loi
            Gra = panel_Chinh.CreateGraphics(); Gra_Tiep = panel_Tiep.CreateGraphics();
            lblCap_Do.Location = new Point(20, 40); lblTime.Location = new Point(20, 80);
            lblDiem.Location = new Point(20, 120); panel_Tiep.Location = new Point(20, 160);
            lblHelp.Location = new Point(20, 280); lblAm_Thanh.Location = new Point(80, 280);
            lblHelp.Image = new Bitmap(Duong_Dan + "Anh\\Help.png");
            Huy_Game(); Tiep = Doc_TC();
            if (Choi_Nhac == 0)
            {
                lblAm_Thanh.Image = new Bitmap(Duong_Dan + "Anh\\Co_Am.png");
            }
            else
            {
                lblAm_Thanh.Image = new Bitmap(Duong_Dan + "Anh\\Ko_Am.png");
            }
        }

        private void New_Game()
        {
            if (Tiep == 0)
            {
                Phut = 0; Giay = 0; lblTime.Text = "00 : 00";
                Diem = 0; lblDiem.Text = "0";
                Cap_Do = 1; lblCap_Do.Text = "LEVEL : 01";
                for (int i = 0; i < So_Hang; i++)
                {
                    for (int j = 0; j < So_Cot; j++)
                    {
                        Gra.FillRectangle(Co_Ve_Xoa, Gach[i, j]);
                    }
                }
                ran_Hinh = ran.Next(0, So_Hinh); ran_Mau = ran.Next(0, So_Mau);
                Co_Ve_Tiep = new SolidBrush(Color.FromArgb(255, Mau_Gach[ran_Mau, 0], Mau_Gach[ran_Mau, 1], Mau_Gach[ran_Mau, 2]));
                Load_Tiep(ran_Hinh); Chon_Hinh(); Load_Tam(); Load_Hinh();
            }
            else
            {
                Doc_DL(); string Time;
                if (Cap_Do < 10) { lblCap_Do.Text = "LEVEL : 0" + Cap_Do.ToString(); }
                else { lblCap_Do.Text = "LEVEL : " + Cap_Do.ToString(); }
                if (Phut < 10) { Time = "0"; }
                else { Time = ""; }
                Time = Time + Phut.ToString() + " : ";
                if (Giay < 10) { Time = Time + "0"; }
                lblTime.Text = Time + Giay.ToString();
                lblDiem.Text = Diem.ToString();
                Co_Ve_Tiep = new SolidBrush(Color.FromArgb(255, Mau_Gach[ran_Mau, 0], Mau_Gach[ran_Mau, 1], Mau_Gach[ran_Mau, 2]));
                Load_Tiep(ran_Hinh);
                Co_Ve_Mau = new SolidBrush(Color.FromArgb(255, Mau_Gach[Loai_Mau, 0], Mau_Gach[Loai_Mau, 1], Mau_Gach[Loai_Mau, 2]));
            }
            Time_MacDinh = 600; Time_MacDinh = Time_MacDinh - ((Cap_Do - 1) * Tang_Toc); timer_Gach.Interval = Time_MacDinh;
            Bat_Dau = 1; Cho_Phep = 1; Dang_Choi = 1; Tiep = 1; Dinh_Hang = So_Hang - 1; Thua = 0;
            timer_Gach.Start(); timer_Time.Start(); if (Choi_Nhac == 1) { Am_Thanh("NewGame"); }
        }

        private void Huy_Game()
        {
            Bat_Dau = 0; Cho_Phep = 0; Dang_Choi = 0; Tiep = 0; Thua = 0;
            timer_Gach.Stop(); timer_Time.Stop();
            Diem = 0; lblDiem.Text = "0";
            Cap_Do = 0; lblCap_Do.Text = "LEVEL : 00";
            Phut = 0; Giay = 0; lblTime.Text = "00 : 00";
            for (int i = 0; i < So_Hang; i++)
            {
                for (int j = 0; j < So_Cot; j++)
                {
                    Trang_Thai[i, j] = 0; Mau_Cu[i, j] = -1;
                    Gra.FillRectangle(Co_Ve_Xoa, Gach[i, j]);
                }
            }
            for (int i = 0; i < So_Gach; i++)
            {
                Gra_Tiep.FillRectangle(Co_Ve_Xoa, Gach_Tiep[i]);
            }
        }

        private void panel_Chinh_Paint(object sender, PaintEventArgs e_Chinh)
        {
            Giao_Dien();
        }

        private void timer_Time_Tick(object sender, EventArgs e)
        {
            if (Thua == 0)
            {
                string Time;
                Giay++; if (Giay == 60) { Giay = 0; Phut++; }
                if (Phut < 10) { Time = "0"; }
                else { Time = ""; }
                Time = Time + Phut.ToString() + " : ";
                if (Giay < 10) { Time = Time + "0"; }
                lblTime.Text = Time + Giay.ToString();
            }
            else
            {
                Time_Thua--; Thua_Nhap_Nhay();
                if (Time_Thua == 0)
                {
                    timer_Time.Stop();
                    if (Cap_Do > CapDo_Max)
                    {
                        MessageBox.Show("               Chúc Mừng !\nBạn đã Phá Đảo trò chơi này");
                    }
                    else
                    {
                        MessageBox.Show("                             Rất Tiếc !\nBạn đã thua, chúc Bạn may mắn lần sau");
                    }
                    Huy_Game(); Giao_Dien();
                }
            }
        }

        private void timer_Gach_Tick(object sender, EventArgs e)
        {
            if ((Xuong_Di == 1) && (Cho_Phep == 1))
            {
                Cho_Phep = 0; Xuong_Duoi();
                if (KiemTra_Duoi() == 0)
                {
                    Xuong_Di = 0; if (Choi_Nhac == 1) { Am_Thanh("Click"); }
                }
                else { Xuong_Di = 1; }
                if (Dai_Hinh > 0)
                {
                    Dai_Hinh--;
                    if (Dai_Hinh == 0)
                    {
                        ran_Hinh = ran.Next(0, So_Hinh); ran_Mau = ran.Next(0, So_Mau);
                        Co_Ve_Tiep = new SolidBrush(Color.FromArgb(255, Mau_Gach[ran_Mau, 0], Mau_Gach[ran_Mau, 1], Mau_Gach[ran_Mau, 2]));
                        Load_Tiep(ran_Hinh);
                    }
                }
                Cho_Phep = 1;
            }
            else
            {
                if (Dai_Hinh > 0)
                {
                    if (Choi_Nhac == 1) { Am_Thanh("Thua_KT"); }
                    Thua = 1; Tiep = 0; Time_Thua = So_Time_Thua; timer_Gach.Stop(); Cho_Phep = 0; Nhap_Nhay = 1; timer_Time.Interval = 100; Bat_Dau = 0; Dang_Choi = 0;
                }
                else
                {
                    Xoa_An();
                    if (Thua == 0)
                    {
                        Chon_Hinh(); Load_Tam(); Load_Hinh();
                        if (Thua == 0)
                        {
                            timer_Gach.Interval = Time_MacDinh; timer_Gach.Start(); Cho_Phep = 1;
                        }
                    }
                }
            }
        }

        private void frmChinh_KeyPress(object sender, KeyPressEventArgs e_Press)
        {
            #region ENTER : Bat dau - Huy

            if (e_Press.KeyChar == 13)
            {
                if (Bat_Dau == 0)
                {
                    if (Tiep == 1)
                    {
                        if (MessageBox.Show("Bạn có muốn tiếp tục lượt chơi trước ?", "Nhắc Nhở", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            Tiep = 0;
                        }
                    }
                    New_Game();
                }
                else
                {
                    timer_Gach.Stop(); timer_Time.Stop(); Cho_Phep = 0;
                    if (MessageBox.Show("Lượt chơi hiện tại của bạn sẽ không được Lưu lại\nNhấn Yes để Hủy\nNhấn No để Tiếp tục", "Xác Nhận Hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        Huy_Game(); Giao_Dien();
                    }
                    else
                    {
                        if (Dang_Choi == 1)
                        {
                            timer_Gach.Start(); timer_Time.Start(); Cho_Phep = 1;
                        }
                    }
                }
            }

            #endregion

            #region ESC : Thoat

            if (e_Press.KeyChar == 27)
            {
                if (Bat_Dau == 1)
                {
                    if (Tiep == 1)
                    {
                        timer_Gach.Stop(); timer_Time.Stop(); Cho_Phep = 0;
                        DialogResult Dre = MessageBox.Show("Bạn có muốn Lưu lại lượt chơi hiện tại không ?\nNhấn Yes để Lưu và Thoát\nNhấn No để Không Lưu và Thoát\nNhấn Cancel để Tiếp tục chơi", "Xác Nhận Thoát", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (DialogResult.Cancel == Dre)
                        {
                            if (Dang_Choi == 1)
                            {
                                timer_Gach.Start(); timer_Time.Start(); Cho_Phep = 1;
                            }
                        }
                        else
                        {
                            if (DialogResult.No == Dre) { Tiep = 0; }
                            Co_Ve_Xoa.Dispose(); Co_Ve_Mau.Dispose(); Co_Ve_Tiep.Dispose();
                            Gra.Dispose(); Gra_Tiep.Dispose();
                            Ghi_TC(Tiep); if (Tiep == 1) { Ghi_DL(); }
                            this.Close();
                        }
                    }
                    else
                    {
                        Co_Ve_Xoa.Dispose(); Co_Ve_Mau.Dispose(); Co_Ve_Tiep.Dispose();
                        Gra.Dispose(); Gra_Tiep.Dispose();
                        Ghi_TC(Tiep); this.Close();
                    }
                }
                else
                {
                    Co_Ve_Xoa.Dispose(); Co_Ve_Mau.Dispose(); Co_Ve_Tiep.Dispose();
                    Gra.Dispose(); Gra_Tiep.Dispose();
                    Ghi_TC(Tiep); this.Close();
                }
            }

            #endregion

            #region SPACE : Tiep - Dung

            if ((Bat_Dau == 1) && (e_Press.KeyChar == 32))
            {
                if (Dang_Choi == 1)
                {
                    timer_Gach.Stop(); timer_Time.Stop(); Dang_Choi = 0; Cho_Phep = 0;
                }
                else
                {
                    timer_Gach.Start(); timer_Time.Start(); Dang_Choi = 1; Cho_Phep = 1;
                }
            }

            #endregion
        }

        private void lblAm_Thanh_Click(object sender, EventArgs e)
        {
            if (Choi_Nhac == 0)
            {
                Choi_Nhac = 1; Am_Thanh("Menu");
                lblAm_Thanh.Image = new Bitmap(Duong_Dan + "Anh\\Ko_Am.png");
            }
            else
            {
                Choi_Nhac = 0;
                lblAm_Thanh.Image = new Bitmap(Duong_Dan + "Anh\\Co_Am.png");
            }
        }

        private void Am_Thanh(string Ten_File)
        {
            SoundPlayer Nhac = new SoundPlayer(Duong_Dan + "Am_Thanh\\" + Ten_File + ".wav");
            Nhac.LoadAsync();
            Nhac.Play();
        }

        private void lblHelp_Click(object sender, EventArgs e)
        {
            if (Choi_Nhac == 1) { Am_Thanh("Menu"); }
            MessageBox.Show("Bạn hãy sử dụng 3 phím Mũi Tên để di chuyển các ô gạch\nMũi Tên Trái để di chuyển ô gạch Sang Trái\nMũi Tên Phải để di chuyển ô gạch Sang Phải\nMũi Tên Xuống Dưới để di chuyển ô gach xuống dưới thật nhanh\nMũi Tên Lên để xoay ô gạch\nNếu xếp được 1 hàng gạch kín, Bạn được 12 điểm và hàng gạch đó sẽ tự động biến mất\nPhím ENTER để Bắt Đàu - Hủy lượt chơi\nPhím SPACE để Tiếp Tục - Tạm Dừng lượt chơi\nPhím ESC để Thoát\nCách chơi vô cùng đơn giản. Mọi góp ý vui lòng gửi về địa chỉ :\nNguyễn Tùng Anh\nEmail : tunganh0025492@gmail.com", "Hướng Dẫn Chơi");
        }

        private void Ghi_DL()
        {
            FileStream File_Ghi = new FileStream(Duong_Dan + "Text\\Luu.txt", FileMode.Create, FileAccess.Write, FileShare.None);
            StreamWriter Ghi_Luu = new StreamWriter(File_Ghi);
            for (int i = 0; i < So_Hang; i++)
            {
                for (int j = 0; j < So_Cot; j++)
                {
                    Ghi_Luu.WriteLine(Mau_Cu[i, j]);
                }
            }
            Ghi_Luu.Flush(); Ghi_Luu.Close(); File_Ghi.Close();
        }

        private void Doc_DL()
        {
            FileStream File_Doc = new FileStream(Duong_Dan + "Text\\Luu.txt", FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader Doc_Luu = new StreamReader(File_Doc);
            string Dong_Doc = Doc_Luu.ReadLine();
            for (int i = 0; i < So_Hang; i++)
            {
                for (int j = 0; j < So_Cot; j++)
                {
                    Mau_Cu[i, j] = Convert.ToInt16(Dong_Doc.Trim());
                    if (Mau_Cu[i, j] != -1)
                    {
                        Trang_Thai[i, j] = 1;
                        Gra.FillRectangle(new SolidBrush(Color.FromArgb(255, Mau_Gach[Mau_Cu[i, j], 0], Mau_Gach[Mau_Cu[i, j], 1], Mau_Gach[Mau_Cu[i, j], 2])), Gach[i, j]);
                    }
                    else
                    {
                        Trang_Thai[i, j] = 0;
                        Gra.FillRectangle(Co_Ve_Xoa, Gach[i, j]);
                    }
                    Dong_Doc = Doc_Luu.ReadLine();
                }
            }
            Doc_Luu.Close(); File_Doc.Close();
        }

        private void Ghi_TC(int t)
        {
            FileStream File_Ghi_TC = new FileStream(Duong_Dan + "Text\\Tuy_Chinh.txt", FileMode.Create, FileAccess.Write, FileShare.None);
            StreamWriter Ghi_TC = new StreamWriter(File_Ghi_TC);
            if (t == 1)
            {
                Ghi_TC.WriteLine(Choi_Nhac); Ghi_TC.WriteLine(Diem);
                Ghi_TC.WriteLine(Phut); Ghi_TC.WriteLine(Giay); Ghi_TC.WriteLine(Cap_Do);
                Ghi_TC.WriteLine(Loai_Hinh); Ghi_TC.WriteLine(Loai_Mau);
                Ghi_TC.WriteLine(ViTri_Y[0]); Ghi_TC.WriteLine(ViTri_X[0]);
                Ghi_TC.WriteLine(ViTri_Y[1]); Ghi_TC.WriteLine(ViTri_X[1]);
                Ghi_TC.WriteLine(ViTri_Y[2]); Ghi_TC.WriteLine(ViTri_X[2]);
                Ghi_TC.WriteLine(ViTri_Y[3]); Ghi_TC.WriteLine(ViTri_X[3]);
                Ghi_TC.WriteLine(ran_Hinh); Ghi_TC.WriteLine(ran_Mau);
                Ghi_TC.WriteLine(Xuong_Di);
            }
            else { Ghi_TC.WriteLine("a"); Ghi_TC.WriteLine(Choi_Nhac); }
            Ghi_TC.Flush(); Ghi_TC.Close(); File_Ghi_TC.Close();
        }

        private int Doc_TC()
        {
            int KQ_Tiep = 0;
            FileStream File_Doc_TC = new FileStream(Duong_Dan + "Text\\Tuy_Chinh.txt", FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader Doc_TC = new StreamReader(File_Doc_TC);
            string Dong_Doc_TC = Doc_TC.ReadLine();
            if (Dong_Doc_TC.Trim() != "a")
            {
                KQ_Tiep = 1; Choi_Nhac = Convert.ToInt16(Dong_Doc_TC.Trim());
                Dong_Doc_TC = Doc_TC.ReadLine(); Diem = Convert.ToInt16(Dong_Doc_TC.Trim());
                Dong_Doc_TC = Doc_TC.ReadLine(); Phut = Convert.ToInt16(Dong_Doc_TC.Trim());
                Dong_Doc_TC = Doc_TC.ReadLine(); Giay = Convert.ToInt16(Dong_Doc_TC.Trim());
                Dong_Doc_TC = Doc_TC.ReadLine(); Cap_Do = Convert.ToInt16(Dong_Doc_TC.Trim());
                Dong_Doc_TC = Doc_TC.ReadLine(); Loai_Hinh = Convert.ToInt16(Dong_Doc_TC.Trim());
                Dong_Doc_TC = Doc_TC.ReadLine(); Loai_Mau = Convert.ToInt16(Dong_Doc_TC.Trim());
                Dong_Doc_TC = Doc_TC.ReadLine(); ViTri_Y[0] = Convert.ToInt16(Dong_Doc_TC.Trim());
                Dong_Doc_TC = Doc_TC.ReadLine(); ViTri_X[0] = Convert.ToInt16(Dong_Doc_TC.Trim());
                Dong_Doc_TC = Doc_TC.ReadLine(); ViTri_Y[1] = Convert.ToInt16(Dong_Doc_TC.Trim());
                Dong_Doc_TC = Doc_TC.ReadLine(); ViTri_X[1] = Convert.ToInt16(Dong_Doc_TC.Trim());
                Dong_Doc_TC = Doc_TC.ReadLine(); ViTri_Y[2] = Convert.ToInt16(Dong_Doc_TC.Trim());
                Dong_Doc_TC = Doc_TC.ReadLine(); ViTri_X[2] = Convert.ToInt16(Dong_Doc_TC.Trim());
                Dong_Doc_TC = Doc_TC.ReadLine(); ViTri_Y[3] = Convert.ToInt16(Dong_Doc_TC.Trim());
                Dong_Doc_TC = Doc_TC.ReadLine(); ViTri_X[3] = Convert.ToInt16(Dong_Doc_TC.Trim());
                Dong_Doc_TC = Doc_TC.ReadLine(); ran_Hinh = Convert.ToInt16(Dong_Doc_TC.Trim());
                Dong_Doc_TC = Doc_TC.ReadLine(); ran_Mau = Convert.ToInt16(Dong_Doc_TC.Trim());
                Dong_Doc_TC = Doc_TC.ReadLine(); Xuong_Di = Convert.ToInt16(Dong_Doc_TC.Trim());
            }
            else
            {
                Dong_Doc_TC = Doc_TC.ReadLine(); Choi_Nhac = Convert.ToInt16(Dong_Doc_TC.Trim());
            }
            Doc_TC.Close(); File_Doc_TC.Close();
            return KQ_Tiep;
        }

        private void Xoa_Gach(int xoa)
        {
            Trang_Thai[ViTri_Y[xoa], ViTri_X[xoa]] = 0; Mau_Cu[ViTri_Y[xoa], ViTri_X[xoa]] = -1;
            Gra.FillRectangle(Co_Ve_Xoa, Gach[ViTri_Y[xoa], ViTri_X[xoa]]);
        }

        private void Ve_Gach(int ve)
        {
            Trang_Thai[ViTri_Y[ve], ViTri_X[ve]] = 1; Mau_Cu[ViTri_Y[ve], ViTri_X[ve]] = Loai_Mau;
            Gra.FillRectangle(Co_Ve_Mau, Gach[ViTri_Y[ve], ViTri_X[ve]]);
        }

        private void Chon_Hinh()
        {
            Loai_Hinh = ran_Hinh; Loai_Mau = ran_Mau;
            Co_Ve_Mau = new SolidBrush(Color.FromArgb(255, Mau_Gach[Loai_Mau, 0], Mau_Gach[Loai_Mau, 1], Mau_Gach[Loai_Mau, 2]));
            if (Loai_Hinh == 0) { Dai_Hinh = 2; }//chu T
            if (Loai_Hinh == 1) { Dai_Hinh = 3; }//chu Z
            if (Loai_Hinh == 2) { Dai_Hinh = 2; }//hinh Vuong
            if (Loai_Hinh == 3) { Dai_Hinh = 4; }//thanh Doc
            if (Loai_Hinh == 4) { Dai_Hinh = 3; }//chu L
        }

        private void Load_Tam()
        {
            if (Loai_Hinh == 0)//chu T
            {
                ViTri_X[0] = ran.Next(1, So_Cot - 1); ViTri_Y[0] = -1;
            }
            if (Loai_Hinh == 1)//chu Z
            {
                ViTri_X[0] = ran.Next(0, So_Cot - 1); ViTri_Y[0] = -1;
            }
            if (Loai_Hinh == 2)//hinh Vuong
            {
                ViTri_X[0] = ran.Next(0, So_Cot - 1); ViTri_Y[0] = -1;
            }
            if (Loai_Hinh == 3)//thanh Doc
            {
                ViTri_X[0] = ran.Next(0, So_Cot); ViTri_Y[0] = -2;
            }
            if (Loai_Hinh == 4)//chu L
            {
                ViTri_X[0] = ran.Next(0, So_Cot - 1); ViTri_Y[0] = -1;
            }
        }

        private void Load_Hinh()
        {
            if (Loai_Hinh == 0)//chu T
            {
                ViTri_X[1] = ViTri_X[0] - 1; ViTri_Y[1] = ViTri_Y[0];//toa do Trai
                ViTri_X[2] = ViTri_X[0] + 1; ViTri_Y[2] = ViTri_Y[0];//toa do Phai
                ViTri_X[3] = ViTri_X[0]; ViTri_Y[3] = ViTri_Y[0] + 1;//toa do Duoi
                if (Trang_Thai[ViTri_Y[3], ViTri_X[3]] == 0)
                {
                    Ve_Gach(3); Dai_Hinh--;
                }
                else
                {
                    if (Choi_Nhac == 1) { Am_Thanh("Thua_KT"); }
                    Thua = 1; Tiep = 0; Time_Thua = So_Time_Thua; timer_Gach.Stop(); Cho_Phep = 0; Nhap_Nhay = 1; timer_Time.Interval = 100; Bat_Dau = 0; Dang_Choi = 0;
                }
            }
            if (Loai_Hinh == 1)//chu Z
            {
                ViTri_X[1] = ViTri_X[0]; ViTri_Y[1] = ViTri_Y[0] - 1;//toa do Tren
                ViTri_X[2] = ViTri_X[0] + 1; ViTri_Y[2] = ViTri_Y[0];//toa do Phai
                ViTri_X[3] = ViTri_X[0] + 1; ViTri_Y[3] = ViTri_Y[0] + 1;//toa do Duoi Phai
                if (Trang_Thai[ViTri_Y[3], ViTri_X[3]] == 0)
                {
                    Ve_Gach(3); Dai_Hinh--;
                }
                else
                {
                    if (Choi_Nhac == 1) { Am_Thanh("Thua_KT"); }
                    Thua = 1; Tiep = 0; Time_Thua = So_Time_Thua; timer_Gach.Stop(); Cho_Phep = 0; Nhap_Nhay = 1; timer_Time.Interval = 100; Bat_Dau = 0; Dang_Choi = 0;
                }
            }
            if (Loai_Hinh == 2)//hinh Vuong
            {
                ViTri_X[1] = ViTri_X[0] + 1; ViTri_Y[1] = ViTri_Y[0];//toa do Phai
                ViTri_X[2] = ViTri_X[0] + 1; ViTri_Y[2] = ViTri_Y[0] + 1;//toa do Duoi Phai
                ViTri_X[3] = ViTri_X[0]; ViTri_Y[3] = ViTri_Y[0] + 1;//toa do Duoi
                if ((Trang_Thai[ViTri_Y[2], ViTri_X[2]] == 0) && (Trang_Thai[ViTri_Y[3], ViTri_X[3]] == 0))
                {
                    Ve_Gach(2); Ve_Gach(3); Dai_Hinh--;
                }
                else
                {
                    if (Choi_Nhac == 1) { Am_Thanh("Thua_KT"); }
                    Thua = 1; Tiep = 0; Time_Thua = So_Time_Thua; timer_Gach.Stop(); Cho_Phep = 0; Nhap_Nhay = 1; timer_Time.Interval = 100; Bat_Dau = 0; Dang_Choi = 0;
                }
            }
            if (Loai_Hinh == 3)//thanh Doc
            {
                ViTri_X[1] = ViTri_X[0]; ViTri_Y[1] = ViTri_Y[0] - 1;//toa do Tren
                ViTri_X[2] = ViTri_X[0]; ViTri_Y[2] = ViTri_Y[0] + 1;//toa do Duoi
                ViTri_X[3] = ViTri_X[0]; ViTri_Y[3] = ViTri_Y[0] + 2;//toa do Duoi Cung
                if (Trang_Thai[ViTri_Y[3], ViTri_X[3]] == 0)
                {
                    Ve_Gach(3); Dai_Hinh--;
                }
                else
                {
                    if (Choi_Nhac == 1) { Am_Thanh("Thua_KT"); }
                    Thua = 1; Tiep = 0; Time_Thua = So_Time_Thua; timer_Gach.Stop(); Cho_Phep = 0; Nhap_Nhay = 1; timer_Time.Interval = 100; Bat_Dau = 0; Dang_Choi = 0;
                }
            }
            if (Loai_Hinh == 4)//chu L
            {
                ViTri_X[1] = ViTri_X[0]; ViTri_Y[1] = ViTri_Y[0] - 1;//toa do Tren
                ViTri_X[2] = ViTri_X[0]; ViTri_Y[2] = ViTri_Y[0] + 1;//toa do Duoi
                ViTri_X[3] = ViTri_X[0] + 1; ViTri_Y[3] = ViTri_Y[0] + 1;//toa do Phai
                if ((Trang_Thai[ViTri_Y[2], ViTri_X[2]] == 0) && (Trang_Thai[ViTri_Y[3], ViTri_X[3]] == 0))
                {
                    Ve_Gach(2); Ve_Gach(3); Dai_Hinh--;
                }
                else
                {
                    if (Choi_Nhac == 1) { Am_Thanh("Thua_KT"); }
                    Thua = 1; Tiep = 0; Time_Thua = So_Time_Thua; timer_Gach.Stop(); Cho_Phep = 0; Nhap_Nhay = 1; timer_Time.Interval = 100; Bat_Dau = 0; Dang_Choi = 0;
                }
            }
            if (Thua == 0)
            {
                if (KiemTra_Duoi() == 0)
                {
                    Xuong_Di = 0; if (Choi_Nhac == 1) { Am_Thanh("Click"); }
                }
                else { Xuong_Di = 1; }
            }
        }//load Hang Gach Duoi Cung cua hinh

        private int KiemTra_Duoi()
        {
            int KQ_Duoi = 0;
            if (Loai_Hinh == 0)//chu T
            {
                if (ViTri_Y[0] < So_Hang - 2)
                {
                    if ((Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 1] == 0) && (Trang_Thai[ViTri_Y[0] + 2, ViTri_X[0]] == 0))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 1)//chu Z
            {
                if (ViTri_Y[0] < So_Hang - 2)
                {
                    if ((Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0]] == 0) && (Trang_Thai[ViTri_Y[0] + 2, ViTri_X[0] + 1] == 0))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 2)//hinh Vuong
            {
                if (ViTri_Y[0] < So_Hang - 2)
                {
                    if ((Trang_Thai[ViTri_Y[0] + 2, ViTri_X[0]] == 0) && (Trang_Thai[ViTri_Y[0] + 2, ViTri_X[0] + 1] == 0))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 3)//thanh Doc
            {
                if (ViTri_Y[0] < So_Hang - 3)
                {
                    if (Trang_Thai[ViTri_Y[0] + 3, ViTri_X[0]] == 0) { KQ_Duoi = 1; }
                }
            }
            if (Loai_Hinh == 4)//chu L
            {
                if (ViTri_Y[0] < So_Hang - 2)
                {
                    if ((Trang_Thai[ViTri_Y[0] + 2, ViTri_X[0]] == 0) && (Trang_Thai[ViTri_Y[0] + 2, ViTri_X[0] + 1] == 0))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 5)//chu T trai
            {
                if (ViTri_Y[0] < So_Hang - 2)
                {
                    if ((Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] + 2, ViTri_X[0]] == 0))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 6)//chu Z ngang
            {
                if (ViTri_Y[0] < So_Hang - 2)
                {
                    if ((Trang_Thai[ViTri_Y[0] + 2, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] + 2, ViTri_X[0]] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 1] == 0))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 8)//thanh Ngang
            {
                if (ViTri_Y[0] < So_Hang - 1)
                {
                    if ((Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 2] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0]] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 1] == 0))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 9)//chu L xap
            {
                if (ViTri_Y[0] < So_Hang - 2)
                {
                    if ((Trang_Thai[ViTri_Y[0] + 2, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0]] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 1] == 0))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 10)//chu T lon nguoc
            {
                if (ViTri_Y[0] < So_Hang - 1)
                {
                    if ((Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0]] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 1] == 0))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 14)//chu L trai
            {
                if (ViTri_Y[0] < So_Hang - 2)
                {
                    if ((Trang_Thai[ViTri_Y[0], ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] + 2, ViTri_X[0]] == 0))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 15)//chu T phai
            {
                if (ViTri_Y[0] < So_Hang - 2)
                {
                    if ((Trang_Thai[ViTri_Y[0] + 2, ViTri_X[0]] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 1] == 0))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 19)//chu L ngua
            {
                if (ViTri_Y[0] < So_Hang - 1)
                {
                    if ((Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0]] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 1] == 0))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            return KQ_Duoi;
        }

        private void Xuong_Duoi()
        {
            if (Loai_Hinh == 0)//chu T
            {
                if (ViTri_Y[0] >= 0) { Xoa_Gach(0); }//xoa Tam
                ViTri_Y[0]++;//dich Tam -> Duoi(khong xoa)
                if (ViTri_Y[1] >= 0) { Xoa_Gach(1); }//xoa Trai
                ViTri_Y[1]++; Ve_Gach(1);//dich Trai
                if (ViTri_Y[2] >= 0) { Xoa_Gach(2); }//xoa Phai
                ViTri_Y[2]++; Ve_Gach(2);//dich Phai
                ViTri_Y[3]++; Ve_Gach(3);//dich Duoi
                Dinh_Hinh = ViTri_Y[0];
            }
            if (Loai_Hinh == 1)//chu Z
            {
                if (ViTri_Y[1] >= 0) { Xoa_Gach(1); }//xoa Tren
                ViTri_Y[1]++;//dich Tren -> Tam(khong xoa)
                ViTri_Y[0]++; Ve_Gach(0);//dich Tam
                if (ViTri_Y[2] >= 0) { Xoa_Gach(2); }//xoa Phai
                ViTri_Y[2]++;//dich Phai -> Duoi Phai(khong xoa)
                ViTri_Y[3]++; Ve_Gach(3);//dich Duoi Phai
                Dinh_Hinh = ViTri_Y[1];
            }
            if (Loai_Hinh == 2)//hinh Vuong
            {
                if (ViTri_Y[0] >= 0) { Xoa_Gach(0); }//xoa Tam
                ViTri_Y[0]++;//dich Tam -> Duoi(khong xoa)
                if (ViTri_Y[1] >= 0) { Xoa_Gach(1); }//xoa Phai
                ViTri_Y[1]++;//dich Phai -> Duoi Phai(khong xoa)
                ViTri_Y[2]++; Ve_Gach(2);//dich Duoi Phai
                ViTri_Y[3]++; Ve_Gach(3);//dich Duoi
                Dinh_Hinh = ViTri_Y[0];
            }
            if (Loai_Hinh == 3)//thanh Doc
            {
                if (ViTri_Y[1] >= 0) { Xoa_Gach(1); }//xoa Tren
                ViTri_Y[1]++;//dich Tren -> Tam(khong xoa)
                ViTri_Y[0]++;//dich Tam -> Duoi(khong xoa)
                ViTri_Y[2]++;//dich Duoi -> Duoi Cung(khong xoa)
                ViTri_Y[3]++; Ve_Gach(3);//dich Duoi Cung
                Dinh_Hinh = ViTri_Y[1];
            }
            if (Loai_Hinh == 4)//chu L
            {
                if (ViTri_Y[1] >= 0) { Xoa_Gach(1); }//xoa Tren
                ViTri_Y[1]++;//dich Tren -> Tam(khong xoa)
                ViTri_Y[0]++;//dich Tam -> Duoi(khong xoa)
                ViTri_Y[2]++; Ve_Gach(2);//dich Duoi
                Xoa_Gach(3);//xoa Phai
                ViTri_Y[3]++; Ve_Gach(3);//dich Phai
                Dinh_Hinh = ViTri_Y[1];
            }
            if (Loai_Hinh == 5)//chu T trai
            {
                Xoa_Gach(1);//xoa Trai
                ViTri_Y[1]++; Ve_Gach(1);//dich Trai
                Xoa_Gach(2);//xoa Tren
                ViTri_Y[2]++;//dich Tren -> Tam(khong xoa)
                ViTri_Y[0]++;//dich Tam -> Duoi(khong xoa)
                ViTri_Y[3]++; Ve_Gach(3);//dich Duoi
                Dinh_Hinh = ViTri_Y[2];
            }
            if (Loai_Hinh == 6)//chu Z ngang
            {
                Xoa_Gach(0);//xoa Tam
                ViTri_Y[0]++;//dich Tam -> Duoi(khong xoa)
                Xoa_Gach(2);//xoa Phai
                ViTri_Y[2]++; Ve_Gach(2);//dich Phai
                Xoa_Gach(1);//xoa Trai
                ViTri_Y[1]++; Ve_Gach(1);//dich Trai
                ViTri_Y[3]++; Ve_Gach(3);//dich Duoi
                Dinh_Hinh = ViTri_Y[0];
            }
            if (Loai_Hinh == 8)//thanh Ngang
            {
                for (int i = 0; i < So_Gach; i++)
                {
                    Xoa_Gach(i); ViTri_Y[i]++; Ve_Gach(i);
                }
                Dinh_Hinh = ViTri_Y[0];
            }
            if (Loai_Hinh == 9)//chu L xap
            {
                Xoa_Gach(1);//xoa Phai
                ViTri_Y[1]++; Ve_Gach(1);//dich Phai
                Xoa_Gach(0);//xoa Tam
                ViTri_Y[0]++; Ve_Gach(0);//dich Tam
                Xoa_Gach(2);//xoa trai
                ViTri_Y[2]++;//dich Trai -> Duoi(khong xoa)
                ViTri_Y[3]++; Ve_Gach(3);//dich Duoi
                Dinh_Hinh = ViTri_Y[0];
            }
            if (Loai_Hinh == 10)//chu T lon nguoc
            {
                Xoa_Gach(1);//xoa Trai
                ViTri_Y[1]++; Ve_Gach(1);//dich Trai
                Xoa_Gach(3);//xoa Phai
                ViTri_Y[3]++; Ve_Gach(3);//dich Phai
                Xoa_Gach(2);//xoa Tren
                ViTri_Y[2]++;//dich Tren -> Tam(khong xoa)
                ViTri_Y[0]++; Ve_Gach(0);//dich Tam
                Dinh_Hinh = ViTri_Y[2];
            }
            if (Loai_Hinh == 14)//chu L trai
            {
                Xoa_Gach(3);//xoa Trai
                ViTri_Y[3]++; Ve_Gach(3);//dich Trai
                Xoa_Gach(2);//xoa Tren
                ViTri_Y[2]++;//dich Tren -> Tam(khong xoa)
                ViTri_Y[0]++;//dich Tam -> Duoi(khong xoa)
                ViTri_Y[1]++; Ve_Gach(1);//dich Duoi
                Dinh_Hinh = ViTri_Y[3];
            }
            if (Loai_Hinh == 15)//chu T phai
            {
                Xoa_Gach(3);//xoa Phai
                ViTri_Y[3]++; Ve_Gach(3);//dich Phai
                Xoa_Gach(2);//xoa Tren
                ViTri_Y[2]++;//dich Tren -> Tam(khong xoa)
                ViTri_Y[0]++;//dich Tam -> Duoi(khong xoa)
                ViTri_Y[1]++; Ve_Gach(1);//dich Duoi
                Dinh_Hinh = ViTri_Y[2];
            }
            if (Loai_Hinh == 19)//chu L ngua
            {
                Xoa_Gach(1);//xoa Trai
                ViTri_Y[1]++; Ve_Gach(1);//dich Trai
                Xoa_Gach(0);//xoa Tam
                ViTri_Y[0]++; Ve_Gach(0);//dich Tam
                Xoa_Gach(3);//xoa Tren
                ViTri_Y[3]++;//dich Tren -> Phai(khong xoa)
                ViTri_Y[2]++; Ve_Gach(2);//dich Phai
                Dinh_Hinh = ViTri_Y[3];
            }
        }

        private int KiemTra_Phai()
        {
            int KQ_Phai = 0;
            if (Loai_Hinh == 0)//chu T
            {
                if (ViTri_X[0] < So_Cot - 2)
                {
                    if ((Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 1] == 0) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] + 2] == 0))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 1)//chu Z
            {
                if (ViTri_X[0] < So_Cot - 2)
                {
                    if ((Trang_Thai[ViTri_Y[0], ViTri_X[0] + 2] == 0) && (Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] + 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 2] == 0))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 2)//hinh Vuong
            {
                if (ViTri_X[0] < So_Cot - 2)
                {
                    if ((Trang_Thai[ViTri_Y[0], ViTri_X[0] + 2] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 2] == 0))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 3)//thanh Doc
            {
                if (ViTri_X[0] < So_Cot - 1)
                {
                    if ((Trang_Thai[ViTri_Y[0], ViTri_X[0] + 1] == 0) && (Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] + 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 1] == 0) && (Trang_Thai[ViTri_Y[0] + 2, ViTri_X[0] + 1] == 0))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 4)//chu L
            {
                if (ViTri_X[0] < So_Cot - 2)
                {
                    if ((Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] + 1] == 0) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] + 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 2] == 0))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 5)//chu T trai
            {
                if (ViTri_X[0] < So_Cot - 1)
                {
                    if ((Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] + 1] == 0) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] + 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 1] == 0))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 6)//chu Z ngang
            {
                if (ViTri_X[0] < So_Cot - 2)
                {
                    if ((Trang_Thai[ViTri_Y[0], ViTri_X[0] + 2] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 1] == 0))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 8)//thanh Ngang
            {
                if (ViTri_X[0] < So_Cot - 2)
                {
                    if (Trang_Thai[ViTri_Y[0], ViTri_X[0] + 2] == 0)
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 9)//chu L xap
            {
                if (ViTri_X[0] < So_Cot - 2)
                {
                    if ((Trang_Thai[ViTri_Y[0], ViTri_X[0] + 2] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0]] == 0))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 10)//chu T lon nguoc
            {
                if (ViTri_X[0] < So_Cot - 2)
                {
                    if ((Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] + 1] == 0) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] + 2] == 0))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 14)//chu L trai
            {
                if (ViTri_X[0] < So_Cot - 1)
                {
                    if ((Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] + 1] == 0) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] + 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 1] == 0))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 15)//chu T phai
            {
                if (ViTri_X[0] < So_Cot - 2)
                {
                    if ((Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] + 1] == 0) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] + 2] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 1] == 0))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 19)//chu L ngua
            {
                if (ViTri_X[0] < So_Cot - 2)
                {
                    if ((Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] + 2] == 0) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] + 2] == 0))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            return KQ_Phai;
        }

        private void Sang_Phai()
        {
            if (Loai_Hinh == 0)//chu T
            {
                ViTri_X[2]++; Ve_Gach(2);//dich Phai
                ViTri_X[0]++;//dich Tam -> Phai(khong xoa)
                Xoa_Gach(1);//xoa Trai
                ViTri_X[1]++;//dich Trai -> Tam(khong xoa)
                Xoa_Gach(3);//xoa Duoi
                ViTri_X[3]++; Ve_Gach(3);//dich Duoi

            }
            if (Loai_Hinh == 1)//chu Z
            {
                Xoa_Gach(1);//xoa Tren
                ViTri_X[1]++; Ve_Gach(1);//dich Tren
                Xoa_Gach(0);//xoa Tam
                ViTri_X[0]++;//dich Tam -> Phai(khong xoa)
                ViTri_X[2]++; Ve_Gach(2);//dich Phai
                Xoa_Gach(3);//xoa Duoi Phai
                ViTri_X[3]++; Ve_Gach(3);//dich Duoi Phai
            }
            if (Loai_Hinh == 2)//hinh Vuong
            {
                Xoa_Gach(0);//xoa Tam
                ViTri_X[0]++;//dich Tam -> Phai(khong xoa)
                Xoa_Gach(3);//xoa Duoi
                ViTri_X[3]++;//dich Duoi -> Duoi Phai(khong xoa)
                ViTri_X[1]++; Ve_Gach(1);//dich Phai
                ViTri_X[2]++; Ve_Gach(2);//dich Duoi Phai
            }
            if (Loai_Hinh == 3)//thanh Doc
            {
                for (int i = 0; i < So_Gach; i++)
                {
                    Xoa_Gach(i); ViTri_X[i]++; Ve_Gach(i);
                }
            }
            if (Loai_Hinh == 4)//chu L
            {
                Xoa_Gach(1);//xoa Tren
                ViTri_X[1]++; Ve_Gach(1);//dich Tren
                Xoa_Gach(0);//xoa Tam
                ViTri_X[0]++; Ve_Gach(0);//dich Tam
                Xoa_Gach(2);//xoa Duoi
                ViTri_X[2]++;//dich Duoi -> Phai(khong xoa)
                ViTri_X[3]++; Ve_Gach(3);//dich Phai
            }
            if (Loai_Hinh == 5)//chu T trai
            {
                Xoa_Gach(2);//xoa Tren
                ViTri_X[2]++; Ve_Gach(2);//dich Tren
                Xoa_Gach(3);//xoa Duoi
                ViTri_X[3]++; Ve_Gach(3);//dich Duoi
                Xoa_Gach(1);//xoa Trai
                ViTri_X[1]++;//dich Trai -> Tam(khong xoa)
                ViTri_X[0]++; Ve_Gach(0);//dich Tam
            }
            if (Loai_Hinh == 6)//chu Z ngang
            {
                Xoa_Gach(0);//xoa Tam
                ViTri_X[0]++;//dich Tam -> Phai(khong xoa)
                ViTri_X[2]++; Ve_Gach(2);//dich Phai
                Xoa_Gach(1);//xoa Trai
                ViTri_X[1]++;//dich Trai -> Duoi(khong xoa)
                ViTri_X[3]++; Ve_Gach(3);//dich Duoi
            }
            if (Loai_Hinh == 8)//thanh Ngang
            {
                Xoa_Gach(3);//xoa Trai Cung
                ViTri_X[3]++;//dich Trai Cung -> Trai(khong xoa)
                ViTri_X[2]++;//dich Trai -> Tam(khong xoa)
                ViTri_X[0]++;//dich Tam -> Phai(khong xoa)
                ViTri_X[1]++; Ve_Gach(1);//dich Phai
            }
            if (Loai_Hinh == 9)//chu L xap
            {
                Xoa_Gach(3);//xoa Duoi
                ViTri_X[3]++; Ve_Gach(3);//dich Duoi
                Xoa_Gach(2);//xoa Trai
                ViTri_X[2]++;//dich Trai -> Tam(khong xoa)
                ViTri_X[0]++;//dich Tam -> Phai(khong xoa)
                ViTri_X[1]++; Ve_Gach(1);//dich Phai
            }
            if (Loai_Hinh == 10)//chu T lon nguoc
            {
                Xoa_Gach(2);//xoa Tren
                ViTri_X[2]++; Ve_Gach(2);//dich Tren
                Xoa_Gach(1);//xoa Trai
                ViTri_X[1]++;//dich Trai -> Tam(khong xoa)
                ViTri_X[0]++;//dich Tam -> Phai(khong xoa)
                ViTri_X[3]++; Ve_Gach(3);//dich Phai
            }
            if (Loai_Hinh == 14)//chu L trai
            {
                Xoa_Gach(3);//xoa Trai
                ViTri_X[3]++;//dich Trai -> Tren(khong xoa)
                ViTri_X[2]++; Ve_Gach(2);//dich Tren
                Xoa_Gach(0);//xoa Tam
                ViTri_X[0]++; Ve_Gach(0);//dich Tam
                Xoa_Gach(1);//xoa Duoi
                ViTri_X[1]++; Ve_Gach(1);//dich Duoi
            }
            if (Loai_Hinh == 15)//chu T phai
            {
                Xoa_Gach(1);//xoa Duoi
                ViTri_X[1]++; Ve_Gach(1);//dich Duoi
                Xoa_Gach(2);//xoa Tren
                ViTri_X[2]++; Ve_Gach(2);//dich Tren
                Xoa_Gach(0);//xoa Tam
                ViTri_X[0]++;//dich Tam -> Phai(khong xoa)
                ViTri_X[3]++; Ve_Gach(3);//dich Phai
            }
            if (Loai_Hinh == 19)//chu L ngua
            {
                Xoa_Gach(3);//xoa Tren
                ViTri_X[3]++; Ve_Gach(3);//dich Tren
                Xoa_Gach(1);//xoa Trai
                ViTri_X[1]++;//dich Trai -> Tam(khong xoa)
                ViTri_X[0]++;//dich Tam -> Phai(khong xoa)
                ViTri_X[2]++; Ve_Gach(2);//dich Phai
            }
        }

        private int KiemTra_Trai()
        {
            int KQ_Trai = 0;
            if (Loai_Hinh == 0)//chu T
            {
                if (ViTri_X[0] > 1)
                {
                    if ((Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] - 2] == 0))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 1)//chu Z
            {
                if (ViTri_X[0] > 0)
                {
                    if ((Trang_Thai[ViTri_Y[0], ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0]] == 0))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 2)//hinh Vuong
            {
                if (ViTri_X[0] > 0)
                {
                    if ((Trang_Thai[ViTri_Y[0], ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 1] == 0))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 3)//thanh Doc
            {
                if (ViTri_X[0] > 0)
                {
                    if ((Trang_Thai[ViTri_Y[0], ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] + 2, ViTri_X[0] - 1] == 0))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 4)//chu L
            {
                if (ViTri_X[0] > 0)
                {
                    if ((Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 1] == 0))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 5)//chu T trai
            {
                if (ViTri_X[0] > 1)
                {
                    if ((Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] - 2] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 1] == 0))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 6)//chu Z ngang
            {
                if (ViTri_X[0] > 1)
                {
                    if ((Trang_Thai[ViTri_Y[0], ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 2] == 0))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 8)//thanh Ngang
            {
                if (ViTri_X[0] > 2)
                {
                    if (Trang_Thai[ViTri_Y[0], ViTri_X[0] - 3] == 0)
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 9)//chu L xap
            {
                if (ViTri_X[0] > 1)
                {
                    if ((Trang_Thai[ViTri_Y[0], ViTri_X[0] - 2] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 2] == 0))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 10)//chu T lon nguoc
            {
                if (ViTri_X[0] > 1)
                {
                    if ((Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] - 2] == 0))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 14)//chu L trai
            {
                if (ViTri_X[0] > 1)
                {
                    if ((Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] - 2] == 0) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 1] == 0))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 15)//chu T phai
            {
                if (ViTri_X[0] > 0)
                {
                    if ((Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 1] == 0))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 19)//chu L ngua
            {
                if (ViTri_X[0] > 1)
                {
                    if ((Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0]] == 0) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] - 2] == 0))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            return KQ_Trai;
        }

        private void Sang_Trai()
        {
            if (Loai_Hinh == 0)//chu T
            {
                ViTri_X[1]--; Ve_Gach(1);//dich Trai
                ViTri_X[0]--;//dich Tam -> Trai(khong xoa)
                Xoa_Gach(2);//xoa Phai
                ViTri_X[2]--;//dich Phai -> Tam(khong xoa)
                Xoa_Gach(3);//xoa Duoi
                ViTri_X[3]--; Ve_Gach(3);//dich Duoi

            }
            if (Loai_Hinh == 1)//chu Z
            {
                Xoa_Gach(1);//xoa Tren
                ViTri_X[1]--; Ve_Gach(1);//dich Tren
                ViTri_X[0]--; Ve_Gach(0);//dich Tam
                Xoa_Gach(2);//xoa Phai
                ViTri_X[2]--;//dich Phai -> Tam(khong xoa)
                Xoa_Gach(3);//xoa Duoi Phai
                ViTri_X[3]--; Ve_Gach(3);//dich Duoi Phai
            }
            if (Loai_Hinh == 2)//hinh Vuong
            {
                Xoa_Gach(1);//xoa Phai
                ViTri_X[1]--;//dich Phai -> Tam(khong xoa)
                Xoa_Gach(2);//xoa Duoi Phai
                ViTri_X[2]--;//dich Duoi Phai -> Duoi(khong xoa)
                ViTri_X[0]--; Ve_Gach(0);//dich Tam
                ViTri_X[3]--; Ve_Gach(3);//dich Duoi
            }
            if (Loai_Hinh == 3)//thanh Doc
            {
                for (int i = 0; i < So_Gach; i++)
                {
                    Xoa_Gach(i); ViTri_X[i]--; Ve_Gach(i);
                }
            }
            if (Loai_Hinh == 4)//chu L
            {
                Xoa_Gach(1);//xoa Tren
                ViTri_X[1]--; Ve_Gach(1);//dich Tren
                Xoa_Gach(0);//xoa Tam
                ViTri_X[0]--; Ve_Gach(0);//dich Tam
                ViTri_X[2]--; Ve_Gach(2);//dich Duoi
                Xoa_Gach(3);//xoa Phai
                ViTri_X[3]--;//dich Phai -> Duoi(khong xoa)
            }
            if (Loai_Hinh == 5)//chu T trai
            {
                Xoa_Gach(2);//xoa Tren
                ViTri_X[2]--; Ve_Gach(2);//dich Tren
                Xoa_Gach(3);//xoa Duoi
                ViTri_X[3]--; Ve_Gach(3);//dich Duoi
                Xoa_Gach(0);//xoa Tam
                ViTri_X[0]--;//dich Tam -> Trai(khong xoa)
                ViTri_X[1]--; Ve_Gach(1);//dich Trai
            }
            if (Loai_Hinh == 6)//chu Z ngang
            {
                Xoa_Gach(2);//xoa Phai
                ViTri_X[2]--;//dich Phai -> Tam(khong xoa)
                ViTri_X[0]--; Ve_Gach(0);//dich Tam
                Xoa_Gach(3);//xoa Duoi
                ViTri_X[3]--;//dich Duoi -> Trai(khong xoa)
                ViTri_X[1]--; Ve_Gach(1);//dich Trai
            }
            if (Loai_Hinh == 8)//thanh Ngang
            {
                Xoa_Gach(1);//xoa Phai
                ViTri_X[1]--;//dich Phai -> Tam(khong xoa)
                ViTri_X[0]--;//dich Tam -> Trai(khong xoa)
                ViTri_X[2]--;//dich Trai -> Trai Cung(khong xoa)
                ViTri_X[3]--; Ve_Gach(3);//dich Trai Cung
            }
            if (Loai_Hinh == 9)//chu L xap
            {
                Xoa_Gach(3);//xoa Duoi
                ViTri_X[3]--; Ve_Gach(3);//dich Duoi
                Xoa_Gach(1);//xoa Phai
                ViTri_X[1]--;//dich Phai -> Tam(khong xoa)
                ViTri_X[0]--;//dich Tam -> Trai(khong xoa)
                ViTri_X[2]--; Ve_Gach(2);//dich Trai
            }
            if (Loai_Hinh == 10)//chu T lon nguoc
            {
                Xoa_Gach(2);//xoa Tren
                ViTri_X[2]--; Ve_Gach(2);//dich Tren
                Xoa_Gach(3);//xoa Phai
                ViTri_X[3]--;//dich Phai -> Tam(khong xoa)
                ViTri_X[0]--;//dich Tam -> Trai(khong xoa)
                ViTri_X[1]--; Ve_Gach(1);//dich Trai
            }
            if (Loai_Hinh == 14)//chu L trai
            {
                Xoa_Gach(1);//xoa Duoi
                ViTri_X[1]--; Ve_Gach(1);//dich Duoi
                Xoa_Gach(0);//xoa Tam
                ViTri_X[0]--; Ve_Gach(0);//dich Tam
                Xoa_Gach(2);//xoa Tren
                ViTri_X[2]--;//dich Tren -> Trai(khong xoa)
                ViTri_X[3]--; Ve_Gach(3);//dich Trai
            }
            if (Loai_Hinh == 15)//chu T phai
            {
                Xoa_Gach(2);//xoa Tren
                ViTri_X[2]--; Ve_Gach(2);//dich Tren
                Xoa_Gach(1);//xoa Duoi
                ViTri_X[1]--; Ve_Gach(1);//dich Duoi
                Xoa_Gach(3);//xoa Phai
                ViTri_X[3]--;//dich Phai -> Tam(khong xoa)
                ViTri_X[0]--; Ve_Gach(0);//dich Tam
            }
            if (Loai_Hinh == 19)//chu L ngua
            {
                Xoa_Gach(3);//xoa Tren
                ViTri_X[3]--; Ve_Gach(3);//dich Tren
                Xoa_Gach(2);//xoa Phai
                ViTri_X[2]--;//dich Phai -> Tam(khong xoa)
                ViTri_X[0]--;//dich Tam -> Trai(khong xoa)
                ViTri_X[1]--; Ve_Gach(1);//dich Trai
            }
        }

        private void Load_Tiep(int _Loai_Hinh)
        {
            for (int t = 0; t < So_Gach; t++)
            {
                Gra_Tiep.FillRectangle(Co_Ve_Xoa, Gach_Tiep[t]);
            }
            if (_Loai_Hinh == 0)//chu T
            {
                Gach_Tiep[0] = new Rectangle(Kich_Thuoc, 30, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[1] = new Rectangle(Kich_Thuoc * 2, 30, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[2] = new Rectangle(Kich_Thuoc * 3, 30, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[3] = new Rectangle(Kich_Thuoc * 2, 50, Kich_Thuoc, Kich_Thuoc);
            }
            if (_Loai_Hinh == 1)//chu Z
            {
                Gach_Tiep[0] = new Rectangle(30, Kich_Thuoc, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[1] = new Rectangle(30, Kich_Thuoc * 2, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[2] = new Rectangle(50, Kich_Thuoc * 2, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[3] = new Rectangle(50, Kich_Thuoc * 3, Kich_Thuoc, Kich_Thuoc);
            }
            if (_Loai_Hinh == 2)//hinh Vuong
            {
                Gach_Tiep[0] = new Rectangle(30, 30, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[1] = new Rectangle(30, 50, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[2] = new Rectangle(50, 30, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[3] = new Rectangle(50, 50, Kich_Thuoc, Kich_Thuoc);
            }
            if (_Loai_Hinh == 3)//thanh Doc
            {
                Gach_Tiep[0] = new Rectangle(Kich_Thuoc * 2, 10, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[1] = new Rectangle(Kich_Thuoc * 2, 30, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[2] = new Rectangle(Kich_Thuoc * 2, 50, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[3] = new Rectangle(Kich_Thuoc * 2, 70, Kich_Thuoc, Kich_Thuoc);
            }
            if (_Loai_Hinh == 4)//chu L
            {
                Gach_Tiep[0] = new Rectangle(30, Kich_Thuoc, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[1] = new Rectangle(30, Kich_Thuoc * 2, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[2] = new Rectangle(30, Kich_Thuoc * 3, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[3] = new Rectangle(50, Kich_Thuoc * 3, Kich_Thuoc, Kich_Thuoc);
            }
            for (int t = 0; t < So_Gach; t++)
            {
                Gra_Tiep.FillRectangle(Co_Ve_Tiep, Gach_Tiep[t]);
            }
        }

        private void frmChinh_KeyDown(object sender, KeyEventArgs e_Down)
        {
            if ((Cho_Phep == 1) && (Dai_Hinh == 0))
            {
                if (e_Down.KeyCode == Keys.Left)
                {
                    if (KiemTra_Trai() == 1)
                    {
                        Cho_Phep = 0; Sang_Trai();
                    }
                }
                if (e_Down.KeyCode == Keys.Right)
                {
                    if (KiemTra_Phai() == 1)
                    {
                        Cho_Phep = 0; Sang_Phai();
                    }
                }
                if (e_Down.KeyCode == Keys.Up)
                {
                    if (KiemTra_Xoay() == 1)
                    {
                        Cho_Phep = 0; Xoay_Hinh();
                    }
                }

                #region kiem tra Duoi

                if (KiemTra_Duoi() == 0)
                {
                    if (Choi_Nhac == 1) { Am_Thanh("Click"); }
                    Xoa_An();
                    if (Thua == 0)
                    {
                        Chon_Hinh(); Load_Tam(); Load_Hinh();
                        if (Thua == 0)
                        {
                            timer_Gach.Interval = Time_MacDinh; timer_Gach.Start(); Cho_Phep = 1;
                        }
                    }
                }
                else { Xuong_Di = 1; Cho_Phep = 1; }

                #endregion

                if (e_Down.KeyCode == Keys.Down)
                {
                    if (Xuong_Di == 1)
                    {
                        timer_Gach.Interval = Time_Max;
                    }
                    else
                    {
                        timer_Gach.Interval = Time_MacDinh;
                    }
                }
            }
        }

        private void frmChinh_KeyUp(object sender, KeyEventArgs e_Up)
        {
            if ((Cho_Phep == 1) && (Thua == 0) && (e_Up.KeyCode != Keys.Space))
            {
                if (e_Up.KeyCode == Keys.Down) { timer_Gach.Interval = Time_MacDinh; }
            }
        }

        private int KiemTra_Xoay()
        {
            int KQ_Xoay = 0;

            #region kiem tra chu T

            if (Loai_Hinh == 0)//chu T -> T trai
            {
                if ((ViTri_Y[0] > 0) && (Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0]] == 0))
                {
                    if ((Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] - 1] == 0))//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }
            if (Loai_Hinh == 5)//chu T trai -> T lon nguoc
            {
                if ((ViTri_X[0] < So_Cot - 1) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] + 1] == 0))
                {
                    if ((Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] + 1] == 0))//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }
            if (Loai_Hinh == 10)//chu T lon nguoc -> T phai
            {
                if ((ViTri_Y[0] < So_Hang - 1) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0]] == 0))
                {
                    if ((Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] + 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 1] == 0))//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }
            if (Loai_Hinh == 15)//chu T phai -> T cu
            {
                if ((ViTri_X[0] > 0) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] - 1] == 0))
                {
                    if ((Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] + 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 1] == 0))//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }

            #endregion

            #region kiem tra chu Z

            if (Loai_Hinh == 1)//chu Z -> Z ngang
            {
                if ((ViTri_X[0] > 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0]] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 1] == 0))
                {
                    if (Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] + 1] == 0)//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }
            if (Loai_Hinh == 6)//chu Z ngang -> Z
            {
                if ((ViTri_Y[0] > 0) && (Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0]] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 1] == 0))
                {
                    if (Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] + 1] == 0)//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }

            #endregion

            #region kiem tra thanh Doc + Ngang

            if (Loai_Hinh == 3)//thanh Doc -> Ngang
            {
                if ((ViTri_X[0] > 1) && (ViTri_X[0] < So_Cot - 1) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] - 2] == 0) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] + 1] == 0))
                {
                    if ((Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] + 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 2] == 0) && (Trang_Thai[ViTri_Y[0] + 2, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] + 2, ViTri_X[0] - 2] == 0))//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }
            if (Loai_Hinh == 8)//thanh Ngang -> Doc
            {
                if ((ViTri_Y[0] > 0) && (ViTri_Y[0] < So_Hang - 2) && (Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0]] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0]] == 0) && (Trang_Thai[ViTri_Y[0] + 2, ViTri_X[0]] == 0))
                {
                    if ((Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] + 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 2] == 0) && (Trang_Thai[ViTri_Y[0] + 2, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] + 2, ViTri_X[0] - 2] == 0))//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }

            #endregion

            #region kiem tra chu L

            if (Loai_Hinh == 4)//chu L -> L xap
            {
                if ((ViTri_X[0] > 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] + 1] == 0))
                {
                    if (Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] + 1] == 0)//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }
            if (Loai_Hinh == 9)//chu L xap -> L trai
            {
                if ((ViTri_Y[0] > 0) && (Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0]] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0]] == 0))
                {
                    if (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 1] == 0)//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }
            if (Loai_Hinh == 14)//chu L trai -> L ngua
            {
                if ((ViTri_X[0] < So_Cot - 1) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] - 1] == 0) && (Trang_Thai[ViTri_Y[0], ViTri_X[0] + 1] == 0) && (Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] + 1] == 0))
                {
                    if (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] - 1] == 0)//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }
            if (Loai_Hinh == 19)//chu L ngua -> nhu cu
            {
                if ((ViTri_Y[0] < So_Hang - 1) && (Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0]] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0]] == 0) && (Trang_Thai[ViTri_Y[0] + 1, ViTri_X[0] + 1] == 0))
                {
                    if (Trang_Thai[ViTri_Y[0] - 1, ViTri_X[0] - 1] == 0)
                    {
                        KQ_Xoay = 1;
                    }
                }
            }

            #endregion

            return KQ_Xoay;
        }

        private void Xoay_Hinh()
        {
            if (Loai_Hinh != 2) { Loai_Hinh = Loai_Hinh + So_Hinh; }

            #region xoay chu T

            if (Loai_Hinh % So_Hinh == 0)//chu T
            {
                if (Loai_Hinh == 5)//chu T trai
                {
                    Xoa_Gach(2);//xoa Phai
                    ViTri_X[2] = ViTri_X[0]; ViTri_Y[2] = ViTri_Y[0] - 1;//dich Phai -> vi tri moi = Tren
                    Ve_Gach(2);//load Phai -> vi tri moI = Tren
                }
                if (Loai_Hinh == 10)//chu T lon nguoc
                {
                    Xoa_Gach(3);//xoa Duoi
                    ViTri_Y[3] = ViTri_Y[0]; ViTri_X[3] = ViTri_X[0] + 1;//dich Duoi -> vi tri moi = Phai
                    Ve_Gach(3);//load Duoi -> vi tri moi = Phai
                }
                if (Loai_Hinh == 15)//chu T phai
                {
                    Xoa_Gach(1);//xoa Trai
                    ViTri_X[1] = ViTri_X[0]; ViTri_Y[1] = ViTri_Y[0] + 1;//dich Trai -> vi tri moi = Duoi
                    Ve_Gach(1);//load Trai -> vi tri moi = Duoi
                }
                if (Loai_Hinh == 20)//tro lai nhu cu
                {
                    Loai_Hinh = 0;
                    Xoa_Gach(2);//xoa Tren
                    ViTri_Y[1] = ViTri_Y[0]; ViTri_X[1] = ViTri_X[0] - 1;//dich Trai ve cho cu
                    ViTri_Y[2] = ViTri_Y[0]; ViTri_X[2] = ViTri_X[0] + 1;//dich Phai ve cho cu
                    ViTri_Y[3] = ViTri_Y[0] + 1; ViTri_X[3] = ViTri_X[0];//dich Duoi ve cho cu
                    Ve_Gach(1);//load Trai
                }
            }

            #endregion

            #region xoay chu Z

            if ((Loai_Hinh == 6) || (Loai_Hinh == 11))//chu Z
            {
                if (Loai_Hinh == 6)//chu Z ngang
                {
                    Xoa_Gach(1);//xoa Tren
                    ViTri_Y[1] = ViTri_Y[0] + 1; ViTri_X[1] = ViTri_X[0] - 1;//dich Tren -> Trai
                    Ve_Gach(1);//load Tren -> Trai
                    Xoa_Gach(3);//xoa Duoi Phai
                    ViTri_Y[3] = ViTri_Y[0] + 1; ViTri_X[3] = ViTri_X[0];//dich Duoi Phai -> Duoi
                    Ve_Gach(3);//load Duoi Phai -> Duoi
                }
                if (Loai_Hinh == 11)//tro lai nhu cu
                {
                    Loai_Hinh = 1;
                    Xoa_Gach(1);//xoa Trai
                    Xoa_Gach(3);//xoa Duoi
                    ViTri_Y[1] = ViTri_Y[0] - 1; ViTri_X[1] = ViTri_X[0];//dich Tren ve cho cu
                    ViTri_Y[3] = ViTri_Y[0] + 1; ViTri_X[3] = ViTri_X[0] + 1;//dich Duoi Phai ve cho cu
                    Ve_Gach(1);//load Tren ve cho cu
                    Ve_Gach(3);//load Phai ve cho cu
                }
            }

            #endregion

            #region xoay thanh Doc

            if ((Loai_Hinh == 8) || (Loai_Hinh == 13))//thanh Doc
            {
                if (Loai_Hinh == 8)//thanh Ngang
                {
                    Xoa_Gach(1);//xoa Tren
                    Xoa_Gach(2);//xoa Duoi
                    Xoa_Gach(3);//xoa Duoi Cung
                    ViTri_Y[1] = ViTri_Y[0]; ViTri_X[1] = ViTri_X[0] + 1;//dich Tren -> Phai
                    ViTri_Y[2] = ViTri_Y[0]; ViTri_X[2] = ViTri_X[0] - 1;//dich Duoi -> Trai
                    ViTri_Y[3] = ViTri_Y[0]; ViTri_X[3] = ViTri_X[0] - 2;//dich Duoi Cung -> Trai Cung
                    Ve_Gach(1);//load Tren -> Trai
                    Ve_Gach(2);//load Duoi -> Phai
                    Ve_Gach(3);//load Duoi Cung -> Phai Cung
                }
                if (Loai_Hinh == 13)//tro lai nhu cu
                {
                    Loai_Hinh = 3;
                    Xoa_Gach(1);//xoa Phai
                    Xoa_Gach(2);//xoa Trai
                    Xoa_Gach(3);//xoa Trai Cung
                    ViTri_Y[1] = ViTri_Y[0] - 1; ViTri_X[1] = ViTri_X[0];//dich Tren ve cho cu
                    ViTri_Y[2] = ViTri_Y[0] + 1; ViTri_X[2] = ViTri_X[0];//dich Duoi ve cho cu
                    ViTri_Y[3] = ViTri_Y[0] + 2; ViTri_X[3] = ViTri_X[0];//dich Duoi Cung ve cho cu
                    Ve_Gach(1);//load Tren ve cho cu
                    Ve_Gach(2);//load Duoi ve cho cu
                    Ve_Gach(3);//load Duoi Cung ve cho cu
                }
            }

            #endregion

            #region xoay chu L

            if ((Loai_Hinh == 9) || (Loai_Hinh == 14) || (Loai_Hinh == 19) || (Loai_Hinh == 24))//chu L
            {
                Xoa_Gach(1); Xoa_Gach(2); Xoa_Gach(3);
                if (Loai_Hinh == 9)//chu L xap
                {
                    ViTri_Y[1] = ViTri_Y[0]; ViTri_X[1] = ViTri_X[0] + 1;//dich Tren -> Phai
                    ViTri_Y[2] = ViTri_Y[0]; ViTri_X[2] = ViTri_X[0] - 1;//dich Duoi -> Trai
                    ViTri_Y[3] = ViTri_Y[0] + 1; ViTri_X[3] = ViTri_X[0] - 1;//dich Phai -> Duoi
                }
                if (Loai_Hinh == 14)//chu L trai
                {
                    ViTri_Y[1] = ViTri_Y[0] + 1; ViTri_X[1] = ViTri_X[0];//dich Phai -> Duoi
                    ViTri_Y[2] = ViTri_Y[0] - 1; ViTri_X[2] = ViTri_X[0];//dich Trai -> Tren
                    ViTri_Y[3] = ViTri_Y[0] - 1; ViTri_X[3] = ViTri_X[0] - 1;//dich Duoi -> Trai
                }
                if (Loai_Hinh == 19)//chu L ngua
                {
                    ViTri_Y[1] = ViTri_Y[0]; ViTri_X[1] = ViTri_X[0] - 1;//dich Duoi -> Trai
                    ViTri_Y[2] = ViTri_Y[0]; ViTri_X[2] = ViTri_X[0] + 1;//dich Tren -> Phai
                    ViTri_Y[3] = ViTri_Y[0] - 1; ViTri_X[3] = ViTri_X[0] + 1;//dich Trai -> Tren
                }
                if (Loai_Hinh == 24)//tro lai nhu cu
                {
                    Loai_Hinh = 4;
                    ViTri_Y[1] = ViTri_Y[0] - 1; ViTri_X[1] = ViTri_X[0];//dich Trai -> Tren
                    ViTri_Y[2] = ViTri_Y[0] + 1; ViTri_X[2] = ViTri_X[0];//dich Phai -> Duoi
                    ViTri_Y[3] = ViTri_Y[0] + 1; ViTri_X[3] = ViTri_X[0] + 1;//dich Tren -> Phai
                }
                Ve_Gach(1); Ve_Gach(2); Ve_Gach(3);
            }

            #endregion
        }

        private void Xoa_An()
        {
            int Cot_An, Hang_An = 0;
            timer_Gach.Stop(); Cho_Phep = 0;
            if (Dinh_Hinh < Dinh_Hang) { Dinh_Hang = Dinh_Hinh; }

            #region Tim hang an

            for (int i = Dinh_Hang; i < So_Hang; i++)
            {
                Cot_An = 0;
                for (int j = 0; j < So_Cot; j++)
                {
                    if (Trang_Thai[i, j] == 1) { Cot_An++; }
                    else { break; }
                }
                if (Cot_An == So_Cot)
                {
                    Diem = Diem + So_Cot;
                    arrHang_An[Hang_An] = i; Hang_An++;
                    if (Hang_An == 4) { break; }
                }
            }

            #endregion

            #region Dich hang an

            if (Hang_An > 0)
            {
                int Hien_Tai = Hang_An - 1, Da_Lay = arrHang_An[Hien_Tai], Dem_Khac = 0;
                for (int i = arrHang_An[Hang_An - 1]; i >= Dinh_Hang; i--)
                {
                    if (i > Dinh_Hang + (Hang_An - 1))
                    {
                        if (Hien_Tai > -1)
                        {
                            if (i == arrHang_An[Hien_Tai]) { Hien_Tai--; }
                            for (int h = Da_Lay - 1; h >= Dinh_Hang; h--)
                            {
                                Dem_Khac = 0;
                                for (int s = 0; s <= Hien_Tai; s++)
                                {
                                    if (h == arrHang_An[s]) { break; }
                                    else { Dem_Khac++; }
                                }
                                if (Dem_Khac == Hien_Tai + 1) { Da_Lay = h; break; }
                            }
                            if (Dem_Khac == Hien_Tai + 1)
                            {
                                for (int j = 0; j < So_Cot; j++)
                                {
                                    Trang_Thai[i, j] = Trang_Thai[Da_Lay, j]; Mau_Cu[i, j] = Mau_Cu[Da_Lay, j];
                                    if (Mau_Cu[i, j] != -1)
                                    {
                                        Gra.FillRectangle(new SolidBrush(Color.FromArgb(255, Mau_Gach[Mau_Cu[i, j], 0], Mau_Gach[Mau_Cu[i, j], 1], Mau_Gach[Mau_Cu[i, j], 2])), Gach[i, j]);
                                    }
                                    else { Gra.FillRectangle(Co_Ve_Xoa, Gach[i, j]); }
                                }
                            }
                            else
                            {
                                Da_Lay = i - 1;
                            }
                        }
                        else
                        {
                            Da_Lay = Da_Lay - 1;
                            for (int j = 0; j < So_Cot; j++)
                            {
                                Trang_Thai[i, j] = Trang_Thai[Da_Lay, j]; Mau_Cu[i, j] = Mau_Cu[Da_Lay, j];
                                if (Mau_Cu[i, j] != -1)
                                {
                                    Gra.FillRectangle(new SolidBrush(Color.FromArgb(255, Mau_Gach[Mau_Cu[i, j], 0], Mau_Gach[Mau_Cu[i, j], 1], Mau_Gach[Mau_Cu[i, j], 2])), Gach[i, j]);
                                }
                                else { Gra.FillRectangle(Co_Ve_Xoa, Gach[i, j]); }
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < So_Cot; j++)
                        {
                            Trang_Thai[i, j] = 0; Mau_Cu[i, j] = -1;
                            Gra.FillRectangle(Co_Ve_Xoa, Gach[i, j]);
                        }
                    }
                }
                if (Choi_Nhac == 1) { Am_Thanh("An"); }
                lblDiem.Text = Diem.ToString();
                if (Diem >= Diem_Max * Cap_Do)
                {
                    if (Choi_Nhac == 1) { Am_Thanh("Thang_KT"); }
                    Cap_Do++;
                    if (Cap_Do <= CapDo_Max)
                    {
                        Time_MacDinh = Time_MacDinh - ((Cap_Do - 1) * Tang_Toc); timer_Gach.Interval = Time_MacDinh;
                        if (Cap_Do < 10) { lblCap_Do.Text = "LEVEL : 0" + Cap_Do.ToString(); }
                        else { lblCap_Do.Text = "LEVEL : " + Cap_Do.ToString(); }
                    }
                    else
                    {
                        Thua = 1; Tiep = 0; Time_Thua = So_Time_Thua; timer_Gach.Stop(); Cho_Phep = 0; Nhap_Nhay = 1; timer_Time.Interval = 100; Bat_Dau = 0; Dang_Choi = 0;
                    }
                }
            }

            #endregion

        }

        private void Giao_Dien()
        {
            int Tam_Mau;

            #region chu X

            Tam_Mau = ran.Next(0, So_Mau);
            Co_Ve_Mau = new SolidBrush(Color.FromArgb(255, Mau_Gach[Tam_Mau, 0], Mau_Gach[Tam_Mau, 1], Mau_Gach[Tam_Mau, 2]));
            for (int i = 0; i < 5; i++)
            {
                Gra.FillRectangle(Co_Ve_Mau, Gach[i, i]); Gra.FillRectangle(Co_Ve_Mau, Gach[i, 4 - i]);
            }

            #endregion

            #region chu E

            Tam_Mau = ran.Next(0, So_Mau);
            Co_Ve_Mau = new SolidBrush(Color.FromArgb(255, Mau_Gach[Tam_Mau, 0], Mau_Gach[Tam_Mau, 1], Mau_Gach[Tam_Mau, 2]));
            for (int j = 6; j < 8; j++)
            {
                Gra.FillRectangle(Co_Ve_Mau, Gach[0, j]);
                Gra.FillRectangle(Co_Ve_Mau, Gach[2, j]);
                Gra.FillRectangle(Co_Ve_Mau, Gach[4, j]);
            }
            Gra.FillRectangle(Co_Ve_Mau, Gach[1, 6]); Gra.FillRectangle(Co_Ve_Mau, Gach[3, 6]);

            #endregion

            #region chu P

            Tam_Mau = ran.Next(0, So_Mau);
            Co_Ve_Mau = new SolidBrush(Color.FromArgb(255, Mau_Gach[Tam_Mau, 0], Mau_Gach[Tam_Mau, 1], Mau_Gach[Tam_Mau, 2]));
            for (int i = 0; i < 5; i++)
            {
                Gra.FillRectangle(Co_Ve_Mau, Gach[i, 9]);
                if (i < 3) { Gra.FillRectangle(Co_Ve_Mau, Gach[i, 11]); }
            }
            Gra.FillRectangle(Co_Ve_Mau, Gach[0, 10]); Gra.FillRectangle(Co_Ve_Mau, Gach[2, 10]);

            #endregion

            #region chu H

            Tam_Mau = ran.Next(0, So_Mau);
            Co_Ve_Mau = new SolidBrush(Color.FromArgb(255, Mau_Gach[Tam_Mau, 0], Mau_Gach[Tam_Mau, 1], Mau_Gach[Tam_Mau, 2]));
            for (int i = 6; i < 11; i++)
            {
                Gra.FillRectangle(Co_Ve_Mau, Gach[i, 0]); Gra.FillRectangle(Co_Ve_Mau, Gach[i, 2]);
            }
            Gra.FillRectangle(Co_Ve_Mau, Gach[8, 1]);

            #endregion

            #region chu I

            Tam_Mau = ran.Next(0, So_Mau);
            Co_Ve_Mau = new SolidBrush(Color.FromArgb(255, Mau_Gach[Tam_Mau, 0], Mau_Gach[Tam_Mau, 1], Mau_Gach[Tam_Mau, 2]));
            for (int i = 6; i < 11; i++) { Gra.FillRectangle(Co_Ve_Mau, Gach[i, 4]); }

            #endregion

            #region chu N

            Tam_Mau = ran.Next(0, So_Mau);
            Co_Ve_Mau = new SolidBrush(Color.FromArgb(255, Mau_Gach[Tam_Mau, 0], Mau_Gach[Tam_Mau, 1], Mau_Gach[Tam_Mau, 2]));
            for (int i = 6; i < 11; i++) { Gra.FillRectangle(Co_Ve_Mau, Gach[i, 6]); }
            Gra.FillRectangle(Co_Ve_Mau, Gach[7, 7]); Gra.FillRectangle(Co_Ve_Mau, Gach[8, 8]);

            #endregion

            #region chu H

            Tam_Mau = ran.Next(0, So_Mau);
            Co_Ve_Mau = new SolidBrush(Color.FromArgb(255, Mau_Gach[Tam_Mau, 0], Mau_Gach[Tam_Mau, 1], Mau_Gach[Tam_Mau, 2]));
            for (int i = 6; i < 11; i++)
            {
                Gra.FillRectangle(Co_Ve_Mau, Gach[i, 9]); Gra.FillRectangle(Co_Ve_Mau, Gach[i, 11]);
            }
            Gra.FillRectangle(Co_Ve_Mau, Gach[8, 10]);

            #endregion

            #region chu T

            Tam_Mau = ran.Next(0, So_Mau);
            Co_Ve_Mau = new SolidBrush(Color.FromArgb(255, Mau_Gach[Tam_Mau, 0], Mau_Gach[Tam_Mau, 1], Mau_Gach[Tam_Mau, 2]));
            for (int j = 1; j < 4; j++) { Gra.FillRectangle(Co_Ve_Mau, Gach[12, j]); }
            Gra.FillRectangle(Co_Ve_Mau, Gach[13, 2]);

            #endregion

            #region hinh Vuong

            Tam_Mau = ran.Next(0, So_Mau);
            Co_Ve_Mau = new SolidBrush(Color.FromArgb(255, Mau_Gach[Tam_Mau, 0], Mau_Gach[Tam_Mau, 1], Mau_Gach[Tam_Mau, 2]));
            for (int j = 9; j < So_Cot - 1; j++)
            {
                Gra.FillRectangle(Co_Ve_Mau, Gach[12, j]); Gra.FillRectangle(Co_Ve_Mau, Gach[13, j]);
            }

            #endregion

            #region thanh Doc

            Tam_Mau = ran.Next(0, So_Mau);
            Co_Ve_Mau = new SolidBrush(Color.FromArgb(255, Mau_Gach[Tam_Mau, 0], Mau_Gach[Tam_Mau, 1], Mau_Gach[Tam_Mau, 2]));
            for (int i = 14; i < 18; i++) { Gra.FillRectangle(Co_Ve_Mau, Gach[i, 6]); }

            #endregion

            #region chu Z

            Tam_Mau = ran.Next(0, So_Mau);
            Co_Ve_Mau = new SolidBrush(Color.FromArgb(255, Mau_Gach[Tam_Mau, 0], Mau_Gach[Tam_Mau, 1], Mau_Gach[Tam_Mau, 2]));
            for (int i = 18; i < 21; i++)
            {
                if (i < 20) { Gra.FillRectangle(Co_Ve_Mau, Gach[i, 1]); }
                if (i > 18) { Gra.FillRectangle(Co_Ve_Mau, Gach[i, 2]); }
            }

            #endregion

            #region chu L

            Tam_Mau = ran.Next(0, So_Mau);
            Co_Ve_Mau = new SolidBrush(Color.FromArgb(255, Mau_Gach[Tam_Mau, 0], Mau_Gach[Tam_Mau, 1], Mau_Gach[Tam_Mau, 2]));
            for (int i = 18; i < 21; i++) { Gra.FillRectangle(Co_Ve_Mau, Gach[i, 9]); }
            Gra.FillRectangle(Co_Ve_Mau, Gach[20, 10]);

            #endregion

        }

        private void Thua_Nhap_Nhay()
        {
            if (Nhap_Nhay == 1)
            {
                Nhap_Nhay = 0;
                for (int i = 0; i < So_Hang; i++)
                {
                    for (int j = 0; j < So_Cot; j++)
                    {
                        if (Trang_Thai[i, j] == 1)
                        {
                            Gra.FillRectangle(Co_Ve_Xoa, Gach[i, j]);
                        }
                    }
                }
            }
            else
            {
                Nhap_Nhay = 1;
                for (int i = 0; i < So_Hang; i++)
                {
                    for (int j = 0; j < So_Cot; j++)
                    {
                        if (Trang_Thai[i, j] == 1)
                        {
                            Gra.FillRectangle(new SolidBrush(Color.FromArgb(255, Mau_Gach[Mau_Cu[i, j], 0], Mau_Gach[Mau_Cu[i, j], 1], Mau_Gach[Mau_Cu[i, j], 2])), Gach[i, j]);
                        }
                    }
                }
            }
        }

    }
}