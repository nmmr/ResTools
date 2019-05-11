using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResWindow
{
    public partial class ResWindow : Form
    {
        private List<ResWriter> reses = new List<ResWriter>();
        private bool stop = false;
        public ResWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            stop = true;
            reses.Clear();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.RestoreDirectory = true;
            dialog.Filter = "HTML|*.html;*.htm";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                stop = false;
                System.IO.Stream stream = dialog.OpenFile();
                System.IO.StreamReader sr = new System.IO.StreamReader(stream);
                string str = sr.ReadToEnd();
                sr.Close();
                stream.Close();

                foreach (string s in str.Split('\n'))
                {
                    if (s.StartsWith("<dt>"))
                    {
                        int dateStart = s.IndexOf("：") + 1;
                        int dateEnd = s.IndexOf("ID");
                        string dateStr = s.Substring(dateStart, dateEnd-dateStart);
                        DateTime date = DateTime.Parse(dateStr);
                        string res = s.Substring(s.IndexOf("<dd>") + 4);
                        res = res.Replace("<br>", "\n");
                        ResWriter rw = new ResWriter(date, res);
                        reses.Add(rw);
                    }
                }
                Task task = new Task(writeAction);
                task.Start();
            }
        }
        private void writeAction()
        {
            Random random = new Random();
            DateTime start = reses.First().date;
            DateTime end = reses.Last().date;
            DateTime now = start;
            while(now.CompareTo(end) == -1 && !stop)
            {
                //描画先とするImageオブジェクトを作成する
                Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                //ImageオブジェクトのGraphicsオブジェクトを作成する
                Graphics g = Graphics.FromImage(canvas);
                Font font = new Font("メイリオ", 24, FontStyle.Bold);
                foreach (ResWriter rw in reses)
                {
                    if (rw.date.CompareTo(now) == 1)
                    {
                        break;
                    }
                    if (rw.width == 1)
                    {
                        rw.x = rw.width = this.Width;
                        rw.y = random.Next(this.Height);
                    }
                    rw.write(g, font);
                }


                //リソースを解放する
                font.Dispose();
                g.Dispose();
                //PictureBox1に表示する
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                }
                pictureBox1.Image = canvas;
                now = now.AddMilliseconds(1000 / 30);
                System.Threading.Thread.Sleep(1000/30);
            }
        }
    }
    public class ResWriter
    {
        public int width = 1;
        public float x = 0;
        public float y = 0;
        public DateTime date;
        public string res = "";
        public ResWriter(DateTime date, string res)
        {
            this.date = date;
            this.res = res;
        }
        public void write(Graphics g, Font font)
        {
            if (x > -200)
            {
                x -= width/150;
                g.DrawString(res, font, Brushes.White, x, y);
            }
        }
    }
}
