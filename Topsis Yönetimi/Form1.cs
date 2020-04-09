using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Topsis_Yönetimi
{
    public partial class Form1 : Form
    {
        #region Tanımlamalar

        Stopwatch sw = new Stopwatch();

        Random r = new Random();

        TextBox[] TümTextBox = new TextBox[49];
        TextBox[] TextBox1 = new TextBox[49];
        TextBox[] TextBox2 = new TextBox[49];

        #endregion

        #region Değişkenler

        double[] Ağırlık = new double[7];
        double[] Sayılar1 = new double[49];
        double[] Sayılar2 = new double[49];
        double[] Sayılar3 = new double[49];

        double[] ss1 = new double[7];
        double[] ss2 = new double[7];
        double[] ss3 = new double[7];
        double[] ss4 = new double[7];
        double[] ss5 = new double[7];
        double[] ss6 = new double[7];

        double[] sz1 = new double[7];
        double[] sz2 = new double[7];
       
        int değer;
        int toplam1 = 0;
        int t1 = 0;

        double enbüyük = 1;
        double enküçük = 0;

        double toplam = 0;
        double toplam2 = 0;

        #endregion

        #region Metodlar

        public void TextboxÜret(TableLayoutPanel TableLayoutPanel1, TextBox[] TxtBox,string y)
        {
            for (int i = 0; i < 49; i++)
            {
                TextBox Textbox = new TextBox();
                Textbox.Name = y + i;
                Textbox.Text = "";
                Textbox.Multiline = true;
                Textbox.Size = new Size(136,45);
                Textbox.Font = new Font("Century Gothic", 10, FontStyle.Regular);
                Textbox.TextAlign = HorizontalAlignment.Center;
                TxtBox[i] = Textbox;
                TableLayoutPanel1.Controls.Add(Textbox);
            }
        }

        public void RasgeleDeğerAta(int x1,int x2,TextBox Toplamtext)
        {
            toplam1 = 0;
            t1 = 0;

            for (int i = x1; i < x2; i++)
            {
                t1++;

                if (t1<7)
                {
                    do
                    {
                        değer = r.Next(1, 20);
                        toplam1 += değer;
                    } while (toplam1 >= 100);
                }

                else if (t1==7)
                {
                    değer = (100-toplam1);
                    toplam1 += değer;
                    Toplamtext.Text = (double.Parse(Toplamtext.Text) - toplam1).ToString();
                    
                }
                TümTextBox[i].Text = değer.ToString();
                
            }
        
        }

        public void StandartAğırlıkBul(int x1)
        {
            for (int i = x1; i < 49; i = i + 7)
            {
                TextBox1[i].Text = String.Format("{0:0.0000}", ((Convert.ToDouble(Sayılar1[i])) / (Math.Sqrt(Math.Pow(Sayılar1[x1], 2) + Math.Pow(Sayılar1[x1+7], 2) + Math.Pow(Sayılar1[x1+14], 2) + Math.Pow(Sayılar1[x1+21], 2) + Math.Pow(Sayılar1[x1+28], 2) + Math.Pow(Sayılar1[x1+35], 2) + Math.Pow(Sayılar1[x1+42], 2)))));
                Sayılar2[i] = Convert.ToDouble(TextBox1[i].Text);
            }
        }

        public void AğırlıklıKararMatrisi(int x1,int x2)
        {
            for (int i = x1; i < 49; i =i+7)
            {
                TextBox2[i].Text = (Sayılar2[i] * Ağırlık[x2]).ToString();
                Sayılar3[i] = Convert.ToDouble(TextBox2[i].Text);
            }
        
        }

        public void İdealNegatifİdeal(int x1, double[] s1,double[] s2,double[] s3,double[] s4)
        {
            enbüyük=1;
            enküçük=0;

            for (int i = x1; i < 49; i = i + 7)
            {
                if (Sayılar3[i] > enküçük)
                {
                    enküçük = Sayılar3[i];
                    s1[x1] = Sayılar3[i];
                    s2[x1] = i;
                }

                if (Sayılar3[i] < enbüyük)
                {
                    enbüyük = Sayılar3[i];
                    s3[x1] = Sayılar3[i];
                    s4[x1] = i;
                }
            }
        
        
        }

        public void İdealNegatifİdeal2(int z1,double[] x1,double[] x2,double[] x3, double[] x4,TextBox tx1,TextBox tx2)
        {
            toplam = 0;
            toplam2 = 0;

            for (int i = z1, j = 0; i < 49 && j < 7; i = i + 7, j++)
            {
                toplam += Math.Pow((Sayılar3[i] - x1[j]), 2);
                toplam2 += Math.Pow((Sayılar3[i] - x2[j]), 2);
            }

            x3[z1] = toplam;
            x4[z1] = toplam2;

            tx1.Text = toplam.ToString();
            tx2.Text = toplam2.ToString();
        
        }

        public void Kontrol(int a, int x1,int x2,int x3,int x4, int x5, int x6,int x7,TextBox txt1)
        {
            if (radioButton1.Checked==true)
            {
                if (a == x1 || a == x2 || a == x3 || a == x4 || a == x5 || a == x6 || a == x7)
                {
                    if (TümTextBox[x1].Text != "" && TümTextBox[x2].Text != "" && TümTextBox[x3].Text != "" && TümTextBox[x4].Text != "" && TümTextBox[x5].Text != "" && TümTextBox[x6].Text != "" && TümTextBox[x7].Text != "")
                    {
                        txt1.Text = (100 - (int.Parse(TümTextBox[x1].Text) + int.Parse(TümTextBox[x2].Text) + int.Parse(TümTextBox[x3].Text) + int.Parse(TümTextBox[x4].Text) + int.Parse(TümTextBox[x5].Text) + int.Parse(TümTextBox[x6].Text) + int.Parse(TümTextBox[x7].Text))).ToString();
                    }
                }
            }

            if (radioButton1.Checked == false)
            {
                if (a == x1 || a == x2 || a == x3 || a == x4 || a == x5 || a == x6 || a == x7)
                {
                    if (TümTextBox[x1].Text != "" && TümTextBox[x2].Text != "" && TümTextBox[x3].Text != "" && TümTextBox[x4].Text != "" && TümTextBox[x5].Text != "" && TümTextBox[x6].Text != "" && TümTextBox[x7].Text != "")
                    {
                        txt1.Text = (50 - (int.Parse(TümTextBox[x1].Text) + int.Parse(TümTextBox[x2].Text) + int.Parse(TümTextBox[x3].Text) + int.Parse(TümTextBox[x4].Text) + int.Parse(TümTextBox[x5].Text) + int.Parse(TümTextBox[x6].Text) + int.Parse(TümTextBox[x7].Text))).ToString();
                    }
                }
            }
        }
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region Ağırlık Kaydedildi

            Ağırlık[0] = Convert.ToDouble(textBox23.Text);
            Ağırlık[1] = Convert.ToDouble(textBox22.Text);
            Ağırlık[2] = Convert.ToDouble(textBox21.Text);
            Ağırlık[3] = Convert.ToDouble(textBox20.Text);
            Ağırlık[4] = Convert.ToDouble(textBox19.Text);
            Ağırlık[5] = Convert.ToDouble(textBox18.Text);
            Ağırlık[6] = Convert.ToDouble(textBox17.Text);

            label4.Text = "Ağırlık Değerleri Kaydedildi...";

            #endregion
        }

        void Textbox_TextChanged(object sender, EventArgs e)
        {
            int a = Convert.ToInt16((sender as TextBox).Name.Substring(7));

            Kontrol(a, 0, 1, 2, 3, 4, 5, 6, textBox9);
            Kontrol(a, 7, 8, 9, 10, 11, 12, 13, textBox11);
            Kontrol(a, 14, 15, 16, 17, 18, 19, 20, textBox12);
            Kontrol(a, 21, 22, 23, 24, 25, 26, 27, textBox13);
            Kontrol(a, 28, 29, 30, 31, 32, 33, 34, textBox14);
            Kontrol(a, 35, 36, 37, 38, 39, 40, 41, textBox15);
            Kontrol(a, 42, 43, 44, 45, 46, 47, 48, textBox10);
        }


        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                #region Textboxlar Üret

                tableLayoutPanel1.Controls.Clear();

                for (int i = 0; i < 49; i++)
                {
                    TextBox Textbox = new TextBox();
                    Textbox.Name = "TextBox" + i;
                    Textbox.Text = "0";
                    Textbox.ReadOnly = false;
                    Textbox.Multiline = true;
                    Textbox.Size = new Size(110, 45);
                    Textbox.Font = new Font("Century Gothic", 12, FontStyle.Regular);
                    Textbox.TextAlign = HorizontalAlignment.Center;
                    Textbox.TextChanged += new EventHandler(Textbox_TextChanged);
                    TümTextBox[i] = Textbox;
                    tableLayoutPanel1.Controls.Add(Textbox);
                }
                #endregion

                textBox9.Text = "100";
                textBox11.Text = "100";
                textBox12.Text = "100";
                textBox13.Text = "100";
                textBox14.Text = "100";
                textBox15.Text = "100";
                textBox10.Text = "100";

            }
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                button2.Visible = true;

                #region Textboxlar Üret

                tableLayoutPanel1.Controls.Clear();
                tableLayoutPanel1.Visible = false;
                for (int i = 0; i < 49; i++)
                {
                    TextBox Textbox = new TextBox();
                    Textbox.Name = "TextBox" + i;
                    Textbox.Text = "";
                    Textbox.ReadOnly = true;
                    Textbox.Multiline = true;
                    Textbox.Size = new Size(110, 45);
                    Textbox.Font = new Font("Century Gothic", 12, FontStyle.Regular);
                    Textbox.TextAlign = HorizontalAlignment.Center;
                    Textbox.TextChanged += new EventHandler(Textbox_TextChanged);
                    TümTextBox[i] = Textbox;
                    tableLayoutPanel1.Controls.Add(Textbox);
                }

                tableLayoutPanel1.Visible = true;
                #endregion

                textBox9.Text = "100";
                textBox11.Text = "100";
                textBox12.Text = "100";
                textBox13.Text = "100";
                textBox14.Text = "100";
                textBox15.Text = "100";
                textBox10.Text = "100";
            }

            else if (radioButton1.Checked == false)
            {
                button2.Visible = false;
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            #region Random Değerler Ata

            label5.Visible = true;
            label6.Visible = true;
            sw.Start();

            RasgeleDeğerAta(0, 7, textBox9);
            RasgeleDeğerAta(7, 14, textBox11);
            RasgeleDeğerAta(14, 21, textBox12);
            RasgeleDeğerAta(21, 28, textBox13);
            RasgeleDeğerAta(28, 35, textBox14);
            RasgeleDeğerAta(35, 42, textBox15);
            RasgeleDeğerAta(42, 49, textBox10);

            label5.Text = sw.Elapsed.ToString();
            sw.Stop();


            label4.Text = "Rastgele Değerler Atandı";

            #endregion
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox9.Text == "0" && textBox10.Text == "0" && textBox11.Text == "0" && textBox12.Text == "0" && textBox13.Text == "0" && textBox14.Text == "0" && textBox15.Text == "0")
            {
                for (int i = 0; i < 49; i++)
                {
                    Sayılar1[i] = double.Parse(TümTextBox[i].Text);
                }

                MessageBox.Show("Veriler Kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                label4.Text = "Veriler Kaydedildi";

                TextboxÜret(tableLayoutPanel3, TextBox1, "TextBox1");


            }

            else
            {
                MessageBox.Show("Girmiş olduğunuz değerler şartları sağlamamaktadır", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            StandartAğırlıkBul(0);
            StandartAğırlıkBul(1);
            StandartAğırlıkBul(2);
            StandartAğırlıkBul(3);
            StandartAğırlıkBul(4);
            StandartAğırlıkBul(5);
            StandartAğırlıkBul(6);

            label9.Text = "Standart Karar Matrisi Oluşturuldu";

            TextboxÜret(tableLayoutPanel4, TextBox2, "TextBox2");

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            AğırlıklıKararMatrisi(0, 0);
            AğırlıklıKararMatrisi(1, 1);
            AğırlıklıKararMatrisi(2, 2);
            AğırlıklıKararMatrisi(3, 3);
            AğırlıklıKararMatrisi(4, 4);
            AğırlıklıKararMatrisi(5, 5);
            AğırlıklıKararMatrisi(6, 6);

            label8.Text = "Ağırlıklı Standart Karar Matrisi Oluşturuldu";

            
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            İdealNegatifİdeal(0, ss1, sz1, ss2, sz2);
            İdealNegatifİdeal(1, ss1, sz1, ss2, sz2);
            İdealNegatifİdeal(2, ss1, sz1, ss2, sz2);
            İdealNegatifİdeal(3, ss1, sz1, ss2, sz2);
            İdealNegatifİdeal(4, ss1, sz1, ss2, sz2);
            İdealNegatifİdeal(5, ss1, sz1, ss2, sz2);
            İdealNegatifİdeal(6, ss1, sz1, ss2, sz2);

            textBox45.Text = ss1[0].ToString();
            textBox46.Text = ss1[1].ToString();
            textBox47.Text = ss1[2].ToString();
            textBox48.Text = ss1[3].ToString();
            textBox49.Text = ss1[4].ToString();
            textBox50.Text = ss1[5].ToString();
            textBox51.Text = ss1[6].ToString();


            textBox52.Text = ss2[0].ToString();
            textBox53.Text = ss2[1].ToString();
            textBox54.Text = ss2[2].ToString();
            textBox55.Text = ss2[3].ToString();
            textBox56.Text = ss2[4].ToString();
            textBox57.Text = ss2[5].ToString();
            textBox58.Text = ss2[6].ToString();


            İdealNegatifİdeal2(0, ss1, ss2, ss3, ss4, textBox59, textBox72);
            İdealNegatifİdeal2(1, ss1, ss2, ss3, ss4, textBox60, textBox71);
            İdealNegatifİdeal2(2, ss1, ss2, ss3, ss4, textBox61, textBox70);
            İdealNegatifİdeal2(3, ss1, ss2, ss3, ss4, textBox62, textBox69);
            İdealNegatifİdeal2(4, ss1, ss2, ss3, ss4, textBox63, textBox68);
            İdealNegatifİdeal2(5, ss1, ss2, ss3, ss4, textBox64, textBox67);
            İdealNegatifİdeal2(6, ss1, ss2, ss3, ss4, textBox65, textBox66);

            for (int i = 0; i < 7; i++)
            {
                ss5[i] = (ss4[i]) / (ss4[i] + ss3[i]);
                ss6[i] = (ss4[i]) / (ss4[i] + ss3[i]);
            }

            textBox79.Text = ss5[0].ToString();
            textBox78.Text = ss5[1].ToString();
            textBox77.Text = ss5[2].ToString();
            textBox76.Text = ss5[3].ToString();
            textBox75.Text = ss5[4].ToString();
            textBox74.Text = ss5[5].ToString();
            textBox73.Text = ss5[6].ToString();

            Array.Sort(ss5);
            Array.Reverse(ss5);

            int z1 = Array.IndexOf(ss6, ss5[0]);

            if (z1 == 0)
            {
                pictureBox33.Image = Image.FromFile("A.jpg");
            }

            else if (z1 == 1)
            {
                pictureBox33.Image = Image.FromFile("B.jpg");
            }

            else if (z1 == 2)
            {
                pictureBox33.Image = Image.FromFile("C.jpg");
            }

            else if (z1 == 3)
            {
                pictureBox33.Image = Image.FromFile("D.jpg");
            }

            else if (z1 == 4)
            {
                pictureBox33.Image = Image.FromFile("E.jpg");
            }

            else if (z1 == 5)
            {
                pictureBox33.Image = Image.FromFile("F.jpg");
            }

            else if (z1 == 6)
            {
                pictureBox33.Image = Image.FromFile("G.jpg");
            }

        }
    }
}
