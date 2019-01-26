using System;
using System.Drawing;
using System.Text;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Media;
using System.Text.RegularExpressions;
using System.Net;
using Update;


namespace pingCheck
{
    public partial class Form1 : Form
    {
        //Декларация нативного WinApi метода
        //Применяем using System.Runtime.InteropServices; для вызова функций из C++ библиотек
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);
        

        bool state = false;
        bool window = true;
        bool noconf = false;
        bool siteExist = false;
        bool timeExist = false;
        bool advanced = false;
        bool log = false;
        bool badConnect = false;
        short pingUpSpring = 0;
        short pingDownSpring = 0;


        DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(settings[]));
        Font fontToUse = new Font("Microsoft Sans Serif", 16, FontStyle.Regular, GraphicsUnit.Pixel);
        Brush brushToUse = new SolidBrush(Color.White);
        Bitmap bitmapText = new Bitmap(16, 16);
        IntPtr hIcon;
        Ping pingSender = new Ping();
        PingOptions options = new PingOptions();
        SoundPlayer pingDownSound = new SoundPlayer(stream: Properties.Resources.pingdown);
        SoundPlayer pingUpSound = new SoundPlayer(stream: Properties.Resources.pingup1);


        public Form1(string[] param)
        {
            InitializeComponent();
            //this.ShowInTaskbar = false;
            hIcon = IntPtr.Zero;
            this.Height = 132;
            this.Width = 350;

            if (param.Length > 0)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    switch (param[i].ToString())
                    {
                        case "/ip":
                            textBox1.Text = param[i+1].ToString();
                            siteExist = true;
                            break;
                        case ("/time"):
                            numericUpDown1.Value = int.Parse(param[i+1].ToString());
                            timeExist = true;
                            break;
                        case "/noconfig":
                            noconf = true;
                            break;
                        case "/clean":
                            settings st = new settings();
                            //st.site = "195.82.50.9";
                            st.site = "";
                            st.timer = 4;
                            
                            settings[] file = new settings[] { st };
                            using (FileStream fs = new FileStream("settings.json", FileMode.Create)) //запись в файл
                            {
                                jsonFormatter.WriteObject(fs, file);
                            }
                            break;
                        default:
                            
                            break;
                    }
                }
            }
            if (siteExist && timeExist) //если были переданы и сайт и таймер
            {
                CheckPing(textBox1.Text); //первый запуск + задание состояния окна + его скрытие
                window = false;
                timer1.Enabled = true;
                state = true;
                button2.Text = "Стоп";
                toolStripMenuItem1.Text = "Стоп";
                textBox1.Enabled = false;
                numericUpDown1.Enabled = false;
                timer1.Interval = int.Parse(numericUpDown1.Value.ToString()) * 1000;
                if (!noconf)
                {
                    //тут уже запись пошла
                    settings st = new settings();
                    st.site = textBox1.Text;
                    st.timer = int.Parse(numericUpDown1.Value.ToString());
                    settings[] file = new settings[] { st };
                    using (FileStream fs = new FileStream("settings.json", FileMode.Create)) //запись в файл
                    {
                        jsonFormatter.WriteObject(fs, file);
                    }
                }
            }
            if (!noconf)
            {
                if (!File.Exists("settings.json")) //если файла нет, то создать новый
                {
                    settings st = new settings();
                    st.site = textBox1.Text;
                    st.timer = int.Parse(numericUpDown1.Value.ToString());
                    settings[] file = new settings[] { st };
                    using (FileStream fs = new FileStream("settings.json", FileMode.Create)) //запись в файл
                    {
                        jsonFormatter.WriteObject(fs, file);
                    }
                }
                using (FileStream fs = new FileStream("settings.json", FileMode.Open)) //читаем из файла и присваеваем
                {
                    settings[] setts = (settings[])jsonFormatter.ReadObject(fs);

                    //если чето из этого было в параметрах, то пишем сюда
                    if (!siteExist)
                       textBox1.Text = setts[0].site;
                    if (!timeExist)
                       numericUpDown1.Value = setts[0].timer;
                }
            }

            //===============================================================================================================

            CheckAndUpdate chk = new CheckAndUpdate();
            chk.BeginCheckProcess();

            //===============================================================================================================
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.Activate();
            window = true;
            toolStripMenuItem2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateTextIcon("89");
        }

        public void CreateTextIcon(string number)
        {
            Graphics g = System.Drawing.Graphics.FromImage(bitmapText);
            g.Clear(Color.Transparent);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            g.DrawString(number, fontToUse, brushToUse, -4, -2);
            hIcon = bitmapText.GetHicon();

            /*
             Дело в том что hIcon указывает на нативную иконку, которая не удаляется автоматически с помощью сборщика мусора из C#
             Из-за этого приходится вызывать нативную функцию для удаления иконки из памяти
             Здесь так-же нужна дополнительная проверка, чтобы понять не вернула ли функция нулевой указатель
             IntPtr - внутренний класс C# для хранения указателей типа HWND, HMODULE, HANDLE и т.п. На нулевое значение он проверяется как 
             ptr == IntPtr.Zero, а НЕ ptr == null (так, для справки).
             */
            if (hIcon != IntPtr.Zero)
            {
                notifyIcon1.Icon = System.Drawing.Icon.FromHandle(hIcon);

                //Если наш указатель не равен 0
                //Удаляем его через вызов WinApi функции

                DestroyIcon(hIcon);
                hIcon = IntPtr.Zero;
            }
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            if (!state)
            {
                if (Regex.IsMatch(textBox1.Text.ToLower(), @"^(http[s]?:\/\/\[a-z]*\.[a-z]{3}\.[a-z]{2})|([a-z]*\.[a-z]{3})") || 
                    ipValid.IsValidIP(textBox1.Text.ToLower()))
                {
                    richTextBox1.Clear();
                    richTextBox1.AppendText("Запуск " + textBox1.Text + Environment.NewLine);
                    
                    timer1.Interval = int.Parse(numericUpDown1.Value.ToString()) * 1000;
                    timer1.Enabled = true;
                    button2.Text = "Стоп";
                    toolStripMenuItem1.Text = "Стоп";
                    state = true;
                    textBox1.Enabled = false;
                    numericUpDown1.Enabled = false;
                    checkBox1.Enabled = false;
                    groupBox1.Enabled = false;
                    checkBox4.Enabled = false;

                    if (!noconf)
                    {
                        //тут идет запись в файл
                        settings st = new settings();
                        st.site = textBox1.Text;
                        st.timer = int.Parse(numericUpDown1.Value.ToString());
                        settings[] file = new settings[] { st };
                        using (FileStream fs = new FileStream("settings.json", FileMode.Create)) //запись в файл
                        {
                            jsonFormatter.WriteObject(fs, file);
                        }
                    }
                    //this.Visible = false;
                    //window = false;
                    toolStripMenuItem2.Enabled = true;
                    CheckPing(textBox1.Text);
                } else
                {

                    //MessageBox.Show(Uri.CheckHostName(textBox1.Text.ToLower()).ToString());
                    //Uri.CheckHostName(textBox1.Text.ToLower());
                    //MessageBox.Show(Uri.TryCreate(("http://" + textBox1.Text).ToLower(), UriKind.Absolute, out Uri uri).ToString());
                    if (ipValid.IsValidIP(textBox1.Text.ToLower()))
                        MessageBox.Show("ну типа ок адресс");
                    MessageBox.Show("Введите корректный адрес!");
                }
            }
            else
            {
                timer1.Enabled = false;
                button2.Text = "Запуск";
                toolStripMenuItem1.Text = "Запуск";
                textBox1.Enabled = true;
                numericUpDown1.Enabled = true;
                checkBox1.Enabled = true;
                groupBox1.Enabled = true;
                checkBox4.Enabled = true;
                state = false;
                
                richTextBox1.AppendText("Остановлено \n");
            }
        }
        public void CheckPing(string adress)
        {
            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            PingReply reply = pingSender.Send(adress, timeout, buffer, options);
            //button2.Text = reply.RoundtripTime.ToString();
            notifyIcon1.Text = adress;
            if (reply.Status == IPStatus.Success) //если удачный отклик то
            {
                if (richTextBox1.Lines.Length > 20) //проверка на переполнение ртбкса
                {
                    richTextBox1.Text = richTextBox1.Text.Remove(0, richTextBox1.Lines[0].Length+1);
                    richTextBox1.ScrollToCaret();
                }
                if (checkBox1.Checked)
                    if (reply.RoundtripTime > numericUpDown2.Value) //проверка на превышение заданного порога
                    {
                        if (!badConnect)
                            pingUpSpring++;
                        pingDownSpring = 0;
                    }
                    else
                    {
                        pingUpSpring = 0;
                        if (badConnect)
                            pingDownSpring++;
                    }
                if (pingUpSpring >= numericUpDown3.Value)
                {
                    if (!badConnect)
                    {
                        pingUpSound.Play();
                        pingUpSpring = 0;
                    }
                    badConnect = true;
                } else
                {
                    if (pingDownSpring >= numericUpDown3.Value)
                    {
                        if (badConnect)
                        {
                            if(checkBox3.Checked)
                            pingDownSound.Play();
                            pingDownSpring = 0;
                            badConnect = false;
                        }
                    }
                }
                richTextBox1.AppendText(System.DateTime.Now.TimeOfDay.Hours + ":" + System.DateTime.Now.TimeOfDay.Minutes + ":" +
                  System.DateTime.Now.TimeOfDay.Seconds + "  " + "Ping: " + reply.RoundtripTime.ToString() + Environment.NewLine);
                CreateTextIcon(reply.RoundtripTime.ToString());
                richTextBox1.ScrollToCaret();
            } else
            {
                //MessageBox.Show(reply.Status.ToString());
                richTextBox1.AppendText(System.DateTime.Now.TimeOfDay.Hours + ":" + System.DateTime.Now.TimeOfDay.Minutes + ":" +
                    System.DateTime.Now.TimeOfDay.Seconds + "  " + reply.Status.ToString()+ Environment.NewLine);
                
                if (checkBox2.Checked && checkBox1.Checked)
                pingUpSound.Play();
                //Uri.IsWellFormedUriString
            }
        }

        //private bool 

        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckPing(textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            window = false;
            toolStripMenuItem2.Enabled = true;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Activate();
            this.Visible = true;
            window = true;
            toolStripMenuItem2.Enabled = false;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (!window)
            {
                this.Hide();
                toolStripMenuItem2.Enabled = true;
            }
            else
            {
                this.Show();
                toolStripMenuItem2.Enabled = false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!advanced)
            {
                this.Height = 314;
                if (checkBox4.Checked)
                this.Width = 580;
                advanced = true;
            } else
            {
                this.Height = 132;
                this.Width = 350;
                advanced = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                groupBox1.Enabled = true;
            else
                groupBox1.Enabled = false;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (!log)
            {
                this.Width = 580;
                log = true;
            }
            else
            {
                this.Width = 350;
                log = false;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                window = false;
                this.WindowState = FormWindowState.Normal;
                this.Hide();
                toolStripMenuItem2.Enabled = true;
            }
        }
    }
}
