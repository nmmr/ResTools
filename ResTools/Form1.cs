using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using Webdll;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace ResTools
{
    public partial class Form1 : Form
    {
        public static string START_FORMAT = "MM/dd HH:mm=";
        public static string END_FORMAT = "dd HH:mm ";
        public static Regex twoRegex = new Regex(".*5ch.*read.*\\d\\d\\d\\d\\d\\d\\d\\d\\d\\d.*|.*2ch.*read.*\\d\\d\\d\\d\\d\\d\\d\\d\\d\\d.*", RegexOptions.IgnoreCase);
        public static Regex jikkyou = new Regex(".*jikkyo.org/test/read.cgi/.*", RegexOptions.IgnoreCase);
        public static Regex ccRegex = new Regex(".*log.kuka.org/cha2/.*", RegexOptions.IgnoreCase);
        public static Regex ccRegex2 = new Regex(".*smile.cha2.org/test/read.cgi/bangumi/.*", RegexOptions.IgnoreCase);
        public static Regex ccRegex3 = new Regex(".*sun.cha2.org/test/read.cgi/bangumi/.*", RegexOptions.IgnoreCase);
        public static Regex monoRegex = new Regex(".*2chlog.com/2ch/.*.dat", RegexOptions.IgnoreCase);
        public static Regex logsokuRegex = new Regex(".*www.logsoku.com/r/2ch.net/.*", RegexOptions.IgnoreCase);
        public static Regex logsoku2Regex = new Regex(".*www.logsoku.com/r/2ch.sc/.*", RegexOptions.IgnoreCase);
        public static Regex aLinkRegex = new Regex("<a .*?>(?<content>.*?)</a>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        public static Regex aRep = new Regex(" <a.*?</a>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        public static Regex imgRep = new Regex("<img.*?>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        public static Regex wwwwwwRep = new Regex("wwwwww+", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        public static Regex lineRep = new Regex("━━+", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        public static Regex filenameRep = new Regex("[\\\\/:\\*\\?\"<>\n| 　#&;\t]", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static Regex tThreadRegex = new Regex("<font size=\\+1 color=red>(?<title>.*?)</font><br>.*?<a href=\"(?<url>.*?)\">.*?</a><br>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        public static Regex tResRegex = new Regex("<dt>(?<number>.*?) ：(?<date>.*?) ID:(?<id>.*?)<dd>(?<text>.*)\r", RegexOptions.IgnoreCase);

        public static String endWord = "l50";
        private List<Res> resultRes = new List<Res>();
        private string tempDir = "G:\\bangumi\\その他\\temp\\";
        private DateTime preStart = DateTime.Now;
        private List<ListViewItem> cacheThread = new List<ListViewItem>();
        private int preStartIndex = 0;
        private int preEndIndex = 0;
        private string preDir = "";
        private string version = "";

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        const UInt32 WM_CHAR = 0x0102;
        const int VK_N = 0x4E;

        public Form1()
        {
            InitializeComponent();

            // treeの構築
            initTreeView(treeView1, "http://himawari.5ch.net/", "weekly");
            initTreeView(treeView2, "http://tanuki.5ch.net/", "livebs2");
            initTreeView(treeView3, "http://nhk2.5ch.net/", "livebs");
            initTreeView(treeView4, "http://nhk2.5ch.net/", "livenhk");
            initTreeView(treeView5, "http://nhk2.5ch.net/", "liveetv");
            initTreeView(treeView6, "http://himawari.5ch.net/", "livecx");
            initTreeView(treeView7, "http://himawari.5ch.net/", "livetbs");
            initTreeView(treeView8, "http://himawari.5ch.net/", "liveanb");
            initTreeView(treeView9, "http://himawari.5ch.net/", "liventv");
            initTreeView(treeView10, "http://himawari.5ch.net/", "livetx");
            initTreeView(treeView11, "http://himawari.5ch.net/", "liveradio");

            version = GetType().Assembly.GetName().Version.ToString();

            resListMenu.Items.Add("同じ");
            resListMenu.Items.Add("ｷﾀ━");
            string[] hour = "22,23,00,01,02,03".Split(',');
            string[] minutes = "00,15,30,45".Split(',');
            foreach (string h in hour)
            {
                foreach (string m in minutes)
                {
                    resListMenu.Items.Add(h + ":" + m);
                }
            }
        }
        private void initTreeView(TreeView view, string domain, string name)
        {
            view.Nodes.Add(new MonoTreeNode(domain + name + "/subback.html", name));

            view.Nodes.Add(new BoardTreeNode(domain + name + "/subback.html", name));

            TreeNode kakoAll4 = new KakoTreeNode2(domain + name + "/kako/kako0000.html");
            kakoAll4.Text = "過去スレッド:201706～";
            view.Nodes.Add(kakoAll4);

            TreeNode kakoAll2 = new KakoTreeNode2("http://hayabusa7.5ch.net/" + name + "/kako/kako0000.html");
            kakoAll2.Text = "過去スレッド:201603～";
            view.Nodes.Add(kakoAll2);
            TreeNode kakoAll3 = new KakoTreeNode2("http://hayabusa7.5ch.net/" + name + "/kako/kako0001.html");
            kakoAll3.Text = "過去スレッド:0001";
            view.Nodes.Add(kakoAll3);

            if (domain.Contains("nhk2"))
            {
                TreeNode kakoAllnhk2 = new KakoTreeNode2(domain.Replace("nhk2", "nhk") + name + "/kako/kako0000.html");
                kakoAllnhk2.Text = "過去スレッド:201603～07";
                view.Nodes.Add(kakoAllnhk2);
                TreeNode kakoAll = new KakoTreeNode2(domain.Replace("nhk2", "nhk") + name + "/kako/kako0000.html");
                kakoAll.Text = "過去スレッド:～201603";
                view.Nodes.Add(kakoAll);
            }
            else
            {
                TreeNode kakoAll = new KakoTreeNode2(domain + name + "/kako/kako0000.html");
                kakoAll.Text = "過去スレッド:～201603";
                view.Nodes.Add(kakoAll);
            }

            TreeNode baseball = new KakoTreeNode2("http://baseball.5ch.net/" + name + "/kako/kako0000.html");
            baseball.Text = "過去スレッド：baseball";
            view.Nodes.Add(baseball);

            TreeNode hayabusa2 = new KakoTreeNode2("http://hayabusa2.5ch.net/" + name + "/kako/kako0000.html");
            hayabusa2.Text = "過去スレッド：hayabusa2";
            view.Nodes.Add(hayabusa2);

            TreeNode hayabusa = new KakoTreeNode2("http://hayabusa.5ch.net/" + name + "/kako/kako0000.html");
            hayabusa.Text = "過去スレッド：hayabusa";
            view.Nodes.Add(hayabusa);

            if (name.Equals("livebs2"))
            {
                TreeNode hayabusawowow = new KakoTreeNode2("http://hayabusa.5ch.net/" + "livewowow" + "/kako/kako0000.html");
                hayabusawowow.Text = "過去スレッド：hayabusawowow";
                view.Nodes.Add(hayabusawowow);
            }

            TreeNode live28 = new KakoTreeNode2("http://live28.5ch.net/" + (name.Equals("livebs2") ? "livewowow" : name) + "/kako/kako0000.html");
            live28.Text = "過去スレッド：live24";
            view.Nodes.Add(live28);

            TreeNode live24 = new KakoTreeNode2("http://live24.5ch.net/" + (name.Equals("livebs2") ? "livewowow" : name) + "/kako/kako0000.html");
            live24.Text = "過去スレッド：live24";
            view.Nodes.Add(live24);

            TreeNode live23 = new KakoTreeNode2("http://live23.5ch.net/" + name + "/kako/kako0000.html");
            live23.Text = "過去スレッド：live23";
            view.Nodes.Add(live23);

            TreeNode live22x = new KakoTreeNode2("http://live22x.5ch.net/" + (name.Equals("livebs2") ? "livewowow" : name) + "/kako/kako0000.html");
            live22x.Text = "過去スレッド：live22x";
            view.Nodes.Add(live22x);

            TreeNode live20 = new KakoTreeNode2("http://live20.5ch.net/" + name + "/kako/kako0000.html");
            live20.Text = "過去スレッド：live20";
            view.Nodes.Add(live20);

            TreeNode live17 = new KakoTreeNode2("http://live17.5ch.net/" + name + "/kako/kako0000.html");
            live17.Text = "過去スレッド：live17";
            view.Nodes.Add(live17);

            TreeNode live16 = new KakoTreeNode2("http://live16.5ch.net/" + name + "/kako/kako0000.html");
            live16.Text = "過去スレッド：live16";
            view.Nodes.Add(live16);

            TreeNode live15 = new KakoTreeNode2("http://live15.5ch.net/" + (name.Equals("livebs2") ? "livewowow" : name) + "/kako/kako0000.html");
            live15.Text = "過去スレッド：live15";
            view.Nodes.Add(live15);

            TreeNode live12 = new KakoTreeNode2("http://live12.5ch.net/" + (name.Equals("livebs2") ? "livewowow" : name) + "/kako/kako0000.html");
            live12.Text = "過去スレッド：live12";
            view.Nodes.Add(live12);

            TreeNode live10 = new KakoTreeNode2("http://live10.5ch.net/" + name + "/kako/kako0000.html");
            live10.Text = "過去スレッド：live10";
            view.Nodes.Add(live10);

            TreeNode live08 = new KakoTreeNode2("http://live8.5ch.net/" + name + "/kako/kako0000.html");
            live08.Text = "過去スレッド：live8";
            view.Nodes.Add(live08);

            TreeNode live05 = new KakoTreeNode2("http://live5.5ch.net/" + name + "/kako/kako0000.html");
            live05.Text = "過去スレッド：live5";
            view.Nodes.Add(live05);

            view.Nodes.Add(new LogsokuTreeNode(name));

            view.AfterExpand += TreeView1_AfterExpand;
            view.AfterCheck += TreeView1_AfterCheck;
            view.KeyDown += TreeView1_KeyDown;
        }
        private void TreeView1_KeyDown(object sender, KeyEventArgs e)
        {
            TreeNode node = ((TreeView)sender).SelectedNode;
            if (node is ThreadTreeNode)
            {
                ThreadTreeNode tNode = node as ThreadTreeNode;
                if (e.KeyCode == Keys.PageUp)
                {
                }
            }
        }

        private void TreeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Checked && (node is ThreadTreeNode))
            {
                ThreadTreeNode tNode = node as ThreadTreeNode;
                readThread(tNode.url);
            }
        }

        private void TreeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            if (node is AbstractTreeNode)
            {
                AbstractTreeNode aNode = node as AbstractTreeNode;
                aNode.refreshChild();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void ThreadList_DragEnter(object sender, DragEventArgs e)
        {
            String url = e.Data.GetData(DataFormats.Text).ToString();
            e.Effect = DragDropEffects.Copy;
        }
        private void ThreadList_DragDrop(object sender, DragEventArgs e)
        {
            string url = e.Data.GetData(DataFormats.Text).ToString();
            readThread(url);
        }
        /*
        title は dat読み込み時に必要
        */
        private void readThread(string url)
        {
            url = url.Replace(endWord, "").Trim();

            // 既にあるurlなら読み込まない
            foreach (Thread t in getReadedThread())
            {
                if (url.Equals(t.url))
                {
                    return;
                }
            }

            // Threadオブジェクト自体をItemにする。変える時は影響大
            Thread thread = null;
            if (twoRegex.Match(url).Success)
            {
                thread = new TwoChannelThread(url);
            }
            else if (jikkyou.Match(url).Success)
            {
                thread = new JikkyouThread(url);
            }
            else if (ccRegex2.Match(url).Success || ccRegex3.Match(url).Success)
            {
                thread = new ChatChannelThread2(url);
            }
            else if (ccRegex.Match(url).Success)
            {
                thread = new ChatChannelThread(url);
            }
            else if (monoRegex.Match(url).Success)
            {
                thread = new DatThread(url);
            }
            else if (logsokuRegex.Match(url).Success || logsoku2Regex.Match(url).Success)
            {
                thread = new LogsokuThread(url);
            }
            else
            {
                MessageBox.Show("対応していないURLです");
                return;
            }

            cacheThread.Clear();
            SaveButton.Text = "保存";

            this.Activate();
            thread.read(afterRead);
        }

        private void afterRead(Thread thread)
        {
            String label = "ERROR:レスがないよ";
            if (thread.resList.Count == 0)
            {

            }
            else if (thread.resList.Count < 50)
            {
                if (MessageBox.Show("レス数が(" + thread.resList.Count + ")ですが、追加しますか？", "", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }
            }

            label = thread.getStartOrEndDate(true).ToString(START_FORMAT) +
                    thread.getStartOrEndDate(false).ToString(END_FORMAT) +
                    thread.title;

            ListViewItem item = null;
            foreach (ListViewItem i in ThreadList.Items)
            {
                if (i.Tag == thread)
                {
                    item = i;
                    item.Text = label;
                    break;
                }
            }
            if (item == null)
            {
                item = new ListViewItem(label);
                item.Tag = thread;
                item.Checked = true;
                ThreadList.BeginUpdate();
                ThreadList.ItemChecked -= ThreadList_ItemChecked;
                ThreadList.Items.Add(item);
                ThreadList.ItemChecked += ThreadList_ItemChecked;
                ThreadList.EndUpdate();
            }

            RefreshResList();
            ResList.Select();
        }
        private void RefreshResList()
        {
            resultRes.Clear();
            foreach (ListViewItem item in ThreadList.CheckedItems)
            {
                Thread thread = item.Tag as Thread;
                resultRes.AddRange(thread.resList);
            }

            ResList.BeginUpdate();
            ResList.Items.Clear();
            ResList.EndUpdate();

            if (sort.Checked)
            {
                resultRes.Sort();
            }

            List<ListViewItem> items = new List<ListViewItem>();
            int kitacount = 0;
            ListViewItem select = null;
            ListViewItem pre = null;
            foreach (Res res in resultRes)
            {
                ListViewItem item = new ListViewItem(
                    new String[] { res.date.ToLongTimeString(), res.res });
                items.Add(item);

                // ｷﾀが三連続で選択か前回と同じ時間を探す
                if (pre == null && res.date.Hour >= preStart.Hour && res.date.Minute >= preStart.Minute && res.date.Second >= preStart.Second)
                {
                    pre = item;
                }
                if (select == null && res.res.Contains("ｷﾀ━"))
                {
                    kitacount++;
                    if (kitacount == 3)
                    {
                        select = item;
                    }
                }
                else
                {
                    kitacount = 0;
                }
            }

            EndBar.Maximum = (resultRes.Count == 0) ? 0 : resultRes.Count - 1;
            EndBar.Value = EndBar.Maximum;

            if (items.Count() > 0)
            {
                ResList.BeginUpdate();
                ResList.Items.AddRange(items.ToArray());
                ResList.EndUpdate();

                if (pre != null && sameTime.Checked)
                {
                    select = pre;
                }
                if (select != null)
                {
                    select.Selected = true;
                    ResList.EnsureVisible(items.IndexOf(select) + 15);
                }
            }
            RefreshLabel();
        }
        private void RefreshLabel()
        {
            if (resultRes.Count() > 0 && ResList.SelectedIndices.Count > 0)
            {
                StartLabel.Text = "開始：" + resultRes[ResList.SelectedIndices[0]].date.ToString();
                EndLabel.Text = "終了：" + resultRes[EndBar.Value].date.ToString() + ":" + resultRes[EndBar.Value].res;
            }
        }

        private void StartBar_Scroll(object sender, EventArgs e)
        {
            RefreshLabel();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            cacheThread.Clear();
            foreach (ListViewItem item in ThreadList.Items)
            {
                cacheThread.Add(item);
            }
            SaveButton.Text = "戻す";
            if (!sameTime.Checked)
            {
                minutes30.Checked = true;
            }
            treeView1.Focus();

            ThreadList.Items.Clear();
            RefreshResList();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // 戻す時の挙動
            if (cacheThread.Count > 0)
            {
                SaveButton.Text = "保存";
                ThreadList.BeginUpdate();
                ResList.BeginUpdate();
                ThreadList.ItemChecked -= ThreadList_ItemChecked;
                ThreadList.Items.AddRange(cacheThread.ToArray());
                ThreadList.ItemChecked += ThreadList_ItemChecked;
                ResList.EndUpdate();
                ThreadList.EndUpdate();
                RefreshResList();
                ResList.EnsureVisible(preEndIndex);
                ResList.Items[preEndIndex].Selected = true;
                cacheThread.Clear();
                return;
            }
            if (resultRes.Count() == 0)
            {
                MessageBox.Show("スレが読み込まれてないよ！");
                return;
            }
            // スレッド歯抜けチェック
            for (int i = 0; i < ThreadList.CheckedItems.Count; i++)
            {
                if (i > 0)
                {
                    DateTime pre = ((Thread) ThreadList.CheckedItems[i - 1].Tag).getStartOrEndDate(false);
                    DateTime current = ((Thread)ThreadList.CheckedItems[i].Tag).getStartOrEndDate(true);
                    if (pre.AddMinutes(3) < current && MessageBox.Show("スレッドが歯抜けかもだが保存する？", "", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    {
                        return;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (ResList.SelectedIndices.Count == 0 ||
                ResList.SelectedIndices[0] >= EndBar.Value)
            {
                MessageBox.Show("開始終了位置がおかしいいよ！");
                return;
            }

            int start = ResList.SelectedIndices[0];
            int end = EndBar.Value - 1;

            if (tempCheckBox.Checked)
            {
                Res res = resultRes[start];
                string dirName = tempDir + tab.SelectedTab.Name + filenameRep.Replace(res.owner.title, "") + res.date.ToString("MMdd") + ".xml";
                exportXML(new System.IO.FileStream(dirName, System.IO.FileMode.Create), start, end);
                if (openCheckbox.Checked)
                {
                    string fileName = dirName.Substring(dirName.IndexOf("bangumi") + 8).Replace("\\", "/");
                    System.Diagnostics.Process.Start("firefox.exe", "http://localhost/bangumi/xml.html?" + fileName);
                }
            }
            else
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.RestoreDirectory = true;
                if (!sameTime.Checked && preDir.Length > 0)
                {
                    dialog.InitialDirectory = preDir;
                }
                dialog.Filter = "XML|*.xml|HTML|*.html;*.htm";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.Stream stream = dialog.OpenFile();
                    if (testCheckBox.Checked)
                    {
                        exportJS(stream, start, end);
                    }
                    else if (dialog.FileName.EndsWith("xml"))
                    {
                        exportXML(stream, start, end);
                    }
                    else
                    {
                        exportHTML(stream, start, end);
                    }

                    if (openCheckbox.Checked)
                    {
                        string fileName = dialog.FileName.Substring(dialog.FileName.IndexOf("bangumi")+8).Replace("\\", "/");
                        System.Diagnostics.Process.Start("firefox.exe", "http://localhost/bangumi/xml.html?" + fileName);
                    }

                    preDir = dialog.FileName.Substring(0, dialog.FileName.LastIndexOf("\\"));
                    preDir = preDir.Substring(0, preDir.LastIndexOf("\\"));
                }
                else
                {
                    return;
                }
            }
            // スレ一覧を消す
            preStartIndex = ResList.SelectedItems[0].Index;
            preEndIndex = end;
            ResetButton_Click(sender, e);
        }

        private void exportHTML(System.IO.Stream stream, int start, int end)
        {
            if (stream != null)
            {
                List<string> exportRes = new List<string>();

                // 最初と最後のレスが同じ場合全部の時間が同じ（２ちゃんの不具合で時間がおかしい場合そうなる）、時間をレス数で割った時間をたす
                // 時間はチェックボックスから
                if (resultRes[start].date.Equals(resultRes[end].date))
                {
                    // TODO 日またぎの番組は無理・・・フラグをみて日付を統一等必要
                    int timelength = 60 * getMinutes() * 1000;
                    int addtime = timelength / (end - start);
                    for (int i = start; i <= end; i++)
                    {
                        resultRes[i].date = resultRes[i].date.AddMilliseconds(addtime * (i - start));
                    }
                }

                preStart = resultRes[start].date;
                string startDate = preStart.ToString();
                string endDate = resultRes[end].date.ToString();

                HashSet<string> id = new HashSet<string>();
                List<Thread> reff = new List<Thread>();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(stream);
                sw.WriteLine("<html>");
                sw.WriteLine("<head>");
                sw.WriteLine("<meta charset=\"utf-8\" version=\"" + version + "\">");//文字コード指定するなら変える
                sw.WriteLine("</head>");
                sw.WriteLine("<body bgcolor=#efefef text=black link=blue alink=red vlind=#660099>");
                // 引用元のチェックと書き込み数等のチェック
                for (int i = start; i < end; i++)
                {
                    Res res = resultRes[i];
                    id.Add(res.id);
                    if (!reff.Contains(res.owner))
                    {
                        reff.Add(res.owner);
                    }
                }
                reff.Sort();
                sw.WriteLine("<h2>引用元：</h2>");
                foreach (Thread tct in reff)
                {
                    sw.WriteLine(""+ (reff.IndexOf(tct)+1) + ":<font size=+1 color=red>" + tct.title + "</font><br>");
                    sw.WriteLine("<a href=\"" + tct.url + "\">" + tct.url + "</a><br>");
                }
                sw.WriteLine("<br><h2>統計(" + startDate + "～" + endDate + ")：</h2>");
                // TODO 実際の書き込みすうと異なる
                sw.WriteLine("　書き込み数：<font size=+1 color=red>" + (end - start) + "</font><br>　ID数　　　：<font size=+1 color=red>" + id.Count + "</font>");
                sw.WriteLine("<h2>実況レス：</h2>");
                sw.WriteLine("<dl>");
                for (int i = start; i < end; i++)
                {
                    Res res = resultRes[i];
                    res.id = aRep.Replace(res.id, "");
                    string convert = imgRep.Replace(res.res, "");
                    // レスへのリンクを変換
                    int ownerNum = (reff.IndexOf(res.owner) + 1);
                    try
                    {
                        for (Match m = aLinkRegex.Match(res.res); m.Success; m = m.NextMatch())
                        {
                            string content = m.Groups["content"].Value.Trim();
                            int targetNum = 0;
                            if (content.StartsWith("&gt;&gt;") && int.TryParse(content.Replace("&gt;&gt;", ""), out targetNum))
                            {
                                int targetOwnerNum = (targetNum > res.number ? (ownerNum - 1) : ownerNum);
                                // レス先があるかチェック
                                if (targetOwnerNum == 0 || !exportRes.Contains(targetOwnerNum + ":" + targetNum))
                                {
                                    goto Exit;
                                }
                                convert = aLinkRegex.Replace(convert, "<u>&gt;&gt;" + targetOwnerNum + ":" + targetNum + "</u>", 1);
                            }
                            else
                            {
                                convert = aLinkRegex.Replace(convert, content, 1);
                            }
                        }
                    }
                    catch (Exception e) { System.Console.WriteLine(e); }

                    // レス圧縮
                    convert = convert.Trim();
                    convert = convert.Replace("\"", ""); // js読み込み時にエラーになるのでダブルクォートを消す
                    convert = convert.Replace(" <br>", "<br>"); // 改行で挿入される空白を除去
                    convert = Lib.toHankaku(convert); // 全角英数カナを半角に
                    convert = wwwwwwRep.Replace(convert, "wwwww"); // 冗長な芝をカット
                    convert = lineRep.Replace(convert, "━"); // 冗長な━をカット

                    string writeNumber = ownerNum + ":" + res.number;
                    exportRes.Add(writeNumber);
                    sw.WriteLine("<dt>" + writeNumber + " ：" +
                    res.date.ToString() + " " + res.id + "<dd>" + convert);
                    

                    // ラベル
                    Exit: ;
                }
                sw.WriteLine("</dl>");
                sw.WriteLine("</body>");
                sw.WriteLine("<script type=\"text/javascript\" src=\"../../js/jikkyou.js\"></script>");
                sw.WriteLine("</html>");
                sw.Close();
                stream.Close();
            }
        }
        private void exportXML(System.IO.Stream stream, int start, int end)
        {
            if (stream != null)
            {
                JikkyouXML jikkyou = new JikkyouXML();

                // 最初と最後のレスが同じ場合全部の時間が同じ（２ちゃんの不具合で時間がおかしい場合そうなる）、時間をレス数で割った時間をたす
                // 時間はチェックボックスから
                if (resultRes[start].date.Equals(resultRes[end].date))
                {
                    // TODO 日またぎの番組は無理・・・フラグをみて日付を統一等必要
                    int timelength = 60 * getMinutes() * 1000;
                    int addtime = timelength / (end - start);
                    for (int i = start; i <= end; i++)
                    {
                        resultRes[i].date = resultRes[i].date.AddMilliseconds(addtime * (i - start));
                    }
                }

                preStart = resultRes[start].date;
                jikkyou.setStart(preStart);

                List<Thread> reff = new List<Thread>();
                for (int i = start; i < end; i++)
                {
                    Res res = resultRes[i];
                    if (!reff.Contains(res.owner))
                    {
                        reff.Add(res.owner);
                    }
                }
                for (int i = start; i < end; i++)
                {
                    Res res = resultRes[i];
                    res.id = aRep.Replace(res.id, "");
                    string convert = imgRep.Replace(res.res, "");
                    // レスへのリンクを変換
                    int ownerNum = (reff.IndexOf(res.owner) + 1);
                    try
                    {
                        for (Match m = aLinkRegex.Match(res.res); m.Success; m = m.NextMatch())
                        {
                            string content = m.Groups["content"].Value.Trim();
                            int targetNum = 0;
                            if (content.StartsWith("&gt;&gt;") && int.TryParse(content.Replace("&gt;&gt;", ""), out targetNum))
                            {
                                int targetOwnerNum = (targetNum > res.number ? (ownerNum - 1) : ownerNum);
                                // レス先があるかチェック
                                if (targetOwnerNum == 0 || !jikkyou.containsRes(targetOwnerNum + ":" + targetNum))
                                {
                                    goto Exit;
                                }
                                convert = aLinkRegex.Replace(convert, "<u>&gt;&gt;" + targetOwnerNum + ":" + targetNum + "</u>", 1);
                            }
                            else
                            {
                                convert = aLinkRegex.Replace(convert, content, 1);
                            }
                        }
                    }
                    catch (Exception e) { System.Console.WriteLine(e); }

                    // レス圧縮
                    convert = convert.Trim();
                    convert = convert.Substring(0, convert.Length - 8).Trim();// 最後の<br>をカットレス内容を変更したとき注意
                    convert = convert.Replace(" <br>", "<br>"); // 改行で挿入される空白を除去
                    convert = convert.Replace("&lt;", "{").Replace("&gt;", "}").Replace("<", "{").Replace(">", "}"); // タグを書けないため変換
                    convert = Lib.toHankaku(convert); // 全角英数カナを半角に
                    convert = wwwwwwRep.Replace(convert, "wwwww"); // 冗長な芝をカット
                    convert = lineRep.Replace(convert, "━"); // 冗長な━をカット

                    jikkyou.addThread(res.owner.title, res.owner.url);
                    jikkyou.addRes(ownerNum + ":" + res.number, res.id, convert, res.date);


                // ラベル
                Exit:;
                }
                XmlSerializer xml = new XmlSerializer(typeof(JikkyouXML));
                xml.Serialize(stream, jikkyou);
                stream.Close();
            }
        }
        /*
        変換TODO
        ・1レス目とそれだけで描かれた引用元を除外
        */
        private void exportJS(System.IO.Stream stream, int start, int end)
        {
            if (stream != null)
            {
                List<string> exportRes = new List<string>();

                // 最初と最後のレスが同じ場合全部の時間が同じ（２ちゃんの不具合で時間がおかしい場合そうなる）、時間をレス数で割った時間をたす
                // 時間はチェックボックスから
                if (resultRes[start].date.Equals(resultRes[end].date))
                {
                    // TODO 日またぎの番組は無理・・・フラグをみて日付を統一等必要
                    int timelength = 60 * getMinutes() * 1000;
                    int addtime = timelength / (end - start);
                    for (int i = start; i <= end; i++)
                    {
                        resultRes[i].date = resultRes[i].date.AddMilliseconds(addtime * (i - start));
                    }
                }

                preStart = resultRes[start].date;

                List<string> id = new List<string>();
                List<Thread> reff = new List<Thread>();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(stream);
                sw.WriteLine("var data = {\"version\":\"" + version + "\"};");
                sw.WriteLine("data[\"start\"] = \"" + Lib.toUnixTime(preStart) + "\";");
                sw.Write("var res = {");
                for (int i = start; i < end; i++)
                {
                    Res res = resultRes[i];
                    res.id = aRep.Replace(res.id, "");
                    string convert = imgRep.Replace(res.res, "");
                    if (!reff.Contains(res.owner))
                    {
                        reff.Add(res.owner);
                    }

                    if (i != start)
                    {
                        sw.Write(",");
                    }

                    if (!id.Contains(res.id))
                    {
                        id.Add(res.id);
                    }
                    // レスへのリンクを変換
                    int ownerNum = (reff.IndexOf(res.owner) + 1);
                    try
                    {
                        for (Match m = aLinkRegex.Match(res.res); m.Success; m = m.NextMatch())
                        {
                            string content = m.Groups["content"].Value.Trim();
                            int targetNum = 0;
                            if (content.StartsWith("&gt;&gt;") && int.TryParse(content.Replace("&gt;&gt;", ""), out targetNum))
                            {
                                int targetOwnerNum = (targetNum > res.number ? (ownerNum - 1) : ownerNum);
                                // レス先があるかチェック
                                if (targetOwnerNum == 0 || !exportRes.Contains(targetOwnerNum + ":" + targetNum))
                                {
                                    goto Exit;
                                }
                                convert = aLinkRegex.Replace(convert, "<u>&gt;&gt;" + targetOwnerNum + ":" + targetNum + "</u>", 1);
                            }
                            else
                            {
                                convert = aLinkRegex.Replace(convert, content, 1);
                            }
                        }
                    }
                    catch (Exception e) { System.Console.WriteLine(e); }

                    // レス圧縮
                    convert = convert.Trim();
                    convert = convert.Substring(0, convert.Length - 8).Trim();// 最初の<dd>と最後の<br>をカットレス内容を変更したとき注意
                    convert = convert.Replace("\"", ""); // js読み込み時にエラーになるのでダブルクォートを消す
                    convert = convert.Replace(" <br>", "<br>"); // 改行で挿入される空白を除去
                    convert = Lib.toHankaku(convert); // 全角英数カナを半角に
                    convert = wwwwwwRep.Replace(convert, "wwwww"); // 冗長な芝をカット
                    convert = lineRep.Replace(convert, "━"); // 冗長な━をカット

                    string writeNumber = ownerNum + ":" + res.number;
                    sw.Write("\"" + writeNumber +
                        "," + new TimeSpan(res.date.Ticks - preStart.Ticks).TotalSeconds +
                        "," + id.IndexOf(res.id) +
                        "\":\"" + convert + "\"");

                    // ラベル
                    Exit:;
                }
                sw.WriteLine("};");
                sw.WriteLine("data[\"res\"] = res;");

                sw.Write("var thread = {");
                foreach (Thread tct in reff)
                {
                    if (reff.IndexOf(tct) != 0)
                    {
                        sw.WriteLine(",");
                    }
                    sw.Write("\"" + tct.url + "\":\"" + tct.title + "\"");
                }
                sw.WriteLine("};");
                sw.WriteLine("data[\"thread\"] = thread;");
                sw.Close();
                stream.Close();
            }
        }
        private void ResList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // チェックボックスの時間似合わせて終了時間にする
            if (ResList.SelectedIndices.Count > 0)
            {
                ResList.EnsureVisible(ResList.SelectedIndices[0]);
                Res start = resultRes[ResList.SelectedIndices[0]];
                int minutes = getMinutes();
                DateTime endTime = start.date.AddMinutes(minutes);
                Res end = resultRes.Last();

                for (int i = resultRes.IndexOf(start); i < resultRes.Count; i++)
                {
                    Res res = resultRes[i];
                    if (endTime.CompareTo(res.date) < 0)
                    {
                        end = res;
                        break;
                    }
                }
                EndBar.Value = resultRes.IndexOf(end);
            }
            RefreshLabel();
        }

        private void ThreadList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (Thread t in getReadedThread())
            {
                if (t.resList.Count == 0)
                {
                    t.read(afterRead);
                }
            }
        }

        private void tempCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (tempCheckBox.Checked)
            {
                /*
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.FileName = tempDir;
                dialog.RestoreDirectory = true;
                dialog.Filter = "HTML|*.html;*.htm";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    tempDir = dialog.FileName;
                }
                */
            }
        }

        private void ListButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = "C:\\Users\\mamo\\Google ドライブ\\web\\bungumi";
            dialog.FileName = "index.html";
            dialog.Filter = "HTML|*.html;*.htm";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Stream stream = dialog.OpenFile();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(stream);
                sw.WriteLine("<html><body><a href=./その他/temp/temp.html target=_balnk>TEMP</a><br><br>");
                writeFileList(sw, dialog.InitialDirectory);
                sw.WriteLine("</body></body>");
                sw.Close();
                stream.Close();
            }
        }
        private void writeFileList(StreamWriter stream, string path)
        {
            if (!Directory.Exists(path))
            {
                return;
            }
            foreach (string child in Directory.GetDirectories(path))
            {
                string url = "./" + child.Substring(child.IndexOf("bungumi") + 8).Replace('\\', '/');
                stream.WriteLine("<a href=" + url + " target=_balnk>" + url + "</a><br>");
                writeFileList(stream, child);
            }
            /*
            foreach (string child in Directory.GetFiles(path))
            {
                if (child.EndsWith("html"))
                {
                    string url = "./" + child.Substring(child.IndexOf("bungumi")+8).Replace('\\',　'/');
                    stream.WriteLine("<a href=" + url + " target=_balnk>" + url + "</a><br>");
                }
            }
            */
        }

        private void ResList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveButton_Click(sender, e);
            }
        }

        private void minutes_CheckedChanged(object sender, EventArgs e)
        {
            ResList_SelectedIndexChanged(sender, e);
        }

        private void EndLabel_Click(object sender, EventArgs e)
        {
            ResList.Items[EndBar.Value].Selected = true;
        }

        private void next_Click(object sender, EventArgs e)
        {
            // プロセスを読み込む
            Process[] ps = Process.GetProcesses();
            foreach (Process p in ps)
            {
                if (p.ProcessName.StartsWith("DiXiM Digital TV plus") || p.ProcessName.StartsWith("firefox"))
                {
                    SetForegroundWindow(p.MainWindowHandle);
                    SendKeys.Send("n");
                }
            }
        }
        private int getMinutes()
        {
            foreach (Control c in this.Controls)
            {
                if (c is RadioButton && c.Name.StartsWith("minutes") && ((RadioButton)c).Checked)
                {
                    return int.Parse(c.Name.Replace("minutes", ""));
                }
            }
            return 30;
        }

        private void nglist_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start("ng.txt");
            p.WaitForExit(1000 * 300);
            Thread.ngMatch = new Regex(Lib.getConfig("ng.txt"), RegexOptions.IgnoreCase);
        }

        private void transform_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = "C:\\inetpub\\wwwroot\\bangumi";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                transformHTML(new DirectoryInfo(dialog.SelectedPath));
            }
        }

        private void transformHTML(DirectoryInfo dir)
        {
            if (dir.Name == "XML" || dir.Name == "temp")
            {
                MessageBox.Show(dir.Name + "は対象外です。");
                return;
            }
            foreach (DirectoryInfo child in dir.GetDirectories())
            {
                transformHTML(child);
            }
            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Extension == ".html")
                {
                    try
                    {
                        ResetButton_Click(this, EventArgs.Empty);

                        FileStream read = file.OpenRead();
                        StreamReader reader = new StreamReader(read);
                        string input = reader.ReadToEnd();
                        reader.Close();
                        read.Close();

                        List<Thread> threads = new List<Thread>();
                        string threadsStr = (input.Contains("<div>")) ? input.Substring(input.IndexOf("<div>"), input.Length - input.IndexOf("<div>")) : input.Substring(0, input.IndexOf("<dl>"));
                        for (Match m = tThreadRegex.Match(threadsStr); m.Success; m = m.NextMatch())
                        {
                            TwoChannelThread thread = new TwoChannelThread(m.Groups["url"].Value);
                            thread.title = m.Groups["title"].Value.Trim();
                            threads.Add(thread);
                        }

                        if (threads.Count > 0)
                        {
                            int owner = 0;
                            int number = 0;
                            for (Match m = tResRegex.Match(input); m.Success; m = m.NextMatch())
                            {
                                string numberStr = m.Groups["number"].Value;
                                if (numberStr.Contains(":"))
                                {
                                    string[] numberSp = numberStr.Split(':');
                                    owner = int.Parse(numberSp[0]) - 1;
                                    number = int.Parse(numberSp[1]);
                                }
                                else
                                {
                                    number = int.Parse(numberStr);

                                    List<int> pre = new List<int>();
                                    for (int i = (threads.Count-1); i >= 0; i--)
                                    {
                                        for (int j = (threads[i].resList.Count-1); j >= 0; j--)
                                        {
                                            if (pre.Count >= 3)
                                            {
                                                break;
                                            }
                                            pre.Add(threads[i].resList[j].number);
                                        }
                                        if (pre.Count >= 3)
                                        {
                                            break;
                                        }
                                    }

                                    if (pre.Count >= 3 && (owner + 1) < threads.Count)
                                    {
                                        if ((pre[2] - pre[0]) > 500 && (pre[2] - pre[1]) > 500)
                                        {
                                            owner++;
                                        }
                                    }
                                }
                                threads[owner].addRes(number,
                                    DateTime.Parse(m.Groups["date"].Value),
                                    m.Groups["text"].Value.Trim(),
                                    m.Groups["id"].Value.Trim());
                            }
                            foreach (Thread t in threads)
                            {
                                resultRes.AddRange(t.resList);
                            }
                            if (sort.Checked)
                            {
                                resultRes.Sort();
                            }
                            exportXML(File.Create(file.FullName.Replace(".html", ".xml")), 0, resultRes.Count-1);
                            file.Delete();
                        }
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(file.FullName);
                        System.Console.WriteLine(e.StackTrace);
                    }
                }
            }
        }

        private void resListMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string text = (string)e.ClickedItem.Text;
            if (text.Contains(":"))
            {
                string[] time = text.Split(':');
                //                for (int i = ResList.SelectedIndices[0]; i < ResList.Items.Count; i++)
                for (int i = 0; i < ResList.Items.Count; i++)
                {
                    ListViewItem item = ResList.Items[i];
                    string[] data = item.Text.Split(':');
                    if (int.Parse(time[0]) == int.Parse(data[0]) && int.Parse(time[1]) <= int.Parse(data[1]))
                    {
                        item.Selected = true;
                        item.EnsureVisible();
                        break;
                    }
                }
            }
        }

        private void open_Click(object sender, EventArgs e)
        {
            if (ThreadList.SelectedIndices.Count > 0)
            {
                urlOpen(getReadedThread()[ThreadList.SelectedIndices[0]].url);
            }
        }
        private void urlOpen(string url)
        {
            tab.SelectTab("web");
            webBrowser1.Url = new Uri(url);
        }

        private void threadListMenu_Opening(object sender, CancelEventArgs e)
        {
            ToolStripItem first = threadListMenu.Items[0];
            threadListMenu.Items.Clear();
            threadListMenu.Items.Add(first);
            if (ThreadList.SelectedIndices.Count > 0)
            {
                foreach (string u in getReadedThread()[ThreadList.SelectedIndices[0]].preThread)
                {
                    threadListMenu.Items.Add(u);
                }
            }
        }

        private void threadListMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text.StartsWith("http://"))
            {
                readThread(e.ClickedItem.Text);
            }
        }
        private List<Thread> getReadedThread()
        {
            List<Thread> result = new List<Thread>();
            foreach (ListViewItem i in ThreadList.Items)
            {
                result.Add((Thread)i.Tag);
            }
            return result;
        }

        private void ThreadList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            RefreshResList();
        }
    }
}
