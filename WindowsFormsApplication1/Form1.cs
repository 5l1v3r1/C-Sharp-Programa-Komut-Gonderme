using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
[DllImport("user32.dll")]

        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public Form1()
        {
            InitializeComponent();
        }

        private void  button1_Click(object sender, EventArgs e)
        {

            // Burada hesap makinasını çalıştıralım

            // dilenirse parametrelerle oynanarak görünmez (invisible/hidden) olarak açılabilir

            Process p = Process.Start(openFileDialog1 .FileName );


            // Şimdi calc.exe tam olarak yüklendi mi bekleyelim...
            
            while (p.MainWindowHandle == IntPtr.Zero)
            {

                System.Threading.Thread.Sleep(10);

                p.Refresh();

            }



            // Eğer yüklenmiş ise

            if (p.MainWindowHandle != IntPtr.Zero)
            {

                // Hafızayı temizleyelim. Birazdan hesap makinası dolduracak

                Clipboard.Clear();

                SetForegroundWindow(p.MainWindowHandle);

                System.Windows.Forms.SendKeys.Send(textBox1 .Text );
                System.Windows.Forms.SendKeys.Send("{"+ textBox2 .Text.ToUpper () +"}");


                // Hafızada Text tipinde veri olana kadar bekleyelim

                while (!Clipboard.ContainsText())
                {

                    System.Threading.Thread.Sleep(10);

                    Application.DoEvents();

                }



                // Hafızadaki text tipi veriyi alalım ve pencerenin başlığına yazalım

                String clipText = Clipboard.GetData(DataFormats.Text).ToString();

                Text = clipText;



                // Hesap makinasını kapatabiliriz artık
                
                p.Kill();
                
                
               
            }

            else MessageBox.Show("Uygulama Bulunamadı!");

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
           Application.Exit();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Program Seçiniz|*.*";
            openFileDialog1.Title = "Program Seçiniz";
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName == "")
            {
                MessageBox.Show("Lüften Boş Bırakmayınız");
            }
            else
            {
                button1.Enabled = true;
            }
        }


    }
}

