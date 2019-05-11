using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Webdll
{
    public class Lib
    {
        public static DateTime UNIXTIME = new DateTime(1970, 1, 1, 9, 0, 0, 0);
        public async static Task<string> read(string uri, string charset="shift_jis")
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                HttpResponseMessage response = await client.GetAsync(uri);
                HttpContent content = response.Content;
                content.Headers.ContentType.CharSet = charset;
                return await content.ReadAsStringAsync();
            }
        }
        public static string addSpace(string src, int size)
        {
            string result = src;
            while (result.Length < size)
            {
                result += "_";
            }
            return result;
        }
        public static int LevenshteinDistance(string s, string t)
        {
            return LevenshteinDistance(s.ToUpper().ToArray(), s.Length, t.ToUpper().ToArray(), t.Length);
        }
        public static int LevenshteinDistance(char[] s, int len_s, char[] t, int len_t)
        {
            int cost = 0;

            /* base case: empty strings */
            if (len_s == 0) return len_t;
            if (len_t == 0) return len_s;

            /* test if last characters of the strings match */
            if (s[len_s - 1] == t[len_t - 1])
                cost = 0;
            else
                cost = 1;

            /* return minimum of delete char from s, delete char from t, and delete char from both */
            return Math.Min(LevenshteinDistance(s, len_s - 1, t, len_t) + 1,
                           Math.Min(LevenshteinDistance(s, len_s, t, len_t - 1) + 1,
                           LevenshteinDistance(s, len_s - 1, t, len_t - 1) + cost));
        }
        public static double toUnixTime(DateTime time)
        {
            return new TimeSpan(time.Ticks - UNIXTIME.Ticks).TotalSeconds;
        }
        public static string toHankaku(string str)
        {
            if (str == null || str.Length == 0)
            {
                return str;
            }

            char[] cs = str.ToCharArray();
            int f = cs.Length;

            for (int i = 0; i < f; i++)
            {
                char c = cs[i];
                // ！(0xFF01) ～ ～(0xFF5E)
                if ('～' != c && '＼' != c && '！' <= c && c <= '～')
                {
                    cs[i] = (char)(c - 0xFEE0);
                }
                // 全角スペース(0x3000) -> 半角スペース(0x0020)
                else if (c == '　')
                {
                    cs[i] = ' ';
                }
            }

            return new string(cs);
            /*
            if (str == null || str.Length == 0)
            {
                return str;
            }

            char[] cs = str.ToCharArray();
            int f = cs.Length;

            for (int i = 0; i < f; i++)
            {
                char c = cs[i];
                // 全角英数だけ半角にする
                if ('０' <= c && c <= '９' ||
                    'ａ' <= c && c <= 'ｚ' ||
                    'Ａ' <= c && c <= 'Ｚ')
                {
                    cs[i] = (char)(c - 0xFEE0);
                }
            }
            return new string(cs);
            */
        }
        public static string getConfig(string path)
        {
            string result = "";
            try
            {
                System.IO.StreamReader sr = System.IO.File.OpenText(path);
                while (!sr.EndOfStream)
                {
                    result += sr.ReadLine() + ((sr.EndOfStream) ? "" : "|");
                }
                sr.Close();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            return result;
        }
        public static int count(string src, string target)
        {
            if (target == "")
            {
                return -1;
            }
            return (src.Length - src.Replace(target, "").Length) / target.Length;
        }
    }
    public abstract class Thread : IComparable<Thread>
    {
        public static Regex ngMatch = new Regex(Lib.getConfig("ng.txt"), RegexOptions.IgnoreCase);
        public static Regex aTagRegex = new Regex("<a href=\"(?<url>.*?)\">.*?</a>", RegexOptions.IgnoreCase);

        public const string NYAN = "NY:AN:NY";
        public string url = "";
        public string title = "";
        public List<Res> resList = new List<Res>();
        public string charset = "shift_jis";
        public List<string> preThread = new List<string>();

        public Thread(string url)
        {
            this.url = url;
        }
            /*
        public async void read()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");

            HttpResponseMessage response = await client.GetAsync(url);
            HttpContent content = response.Content;
            content.Headers.ContentType.CharSet = "shift_jis";
            string str = await content.ReadAsStringAsync();
            */
        public async void read(Action<Thread> afterAction)
        {
            string str = await Lib.read(url, charset);
            if (str != null)
            {
                parser(str);
                if (resList.Count > 10)
                {
                    bool notSecond = true;
                    foreach (Res r in resList)
                    {
                        if (r.nyan)
                        {
                            notSecond = false;
                            break;
                        }

                        notSecond = r.date.Second == 0;
                        if (!notSecond)
                        {
                            break;
                        }
                    }
                    // 秒が無い場合、分単位で均等に秒をつける
                    if (notSecond)
                    {
                        Res before = null;
                        List<Res> minutes = new List<Res>();
                        foreach (Res r in resList)
                        {
                            if (before != null && before.date.Minute != r.date.Minute)
                            {
                                avgSeccond(minutes);
                                minutes.Clear();
                            }
                            minutes.Add(r);
                            before = r;
                        }
                        avgSeccond(minutes);
                    }
                }
                afterAction(this);
            }
        }
        private void avgSeccond(List<Res> res)
        {
            if (res.Count <= 1)
            {
                return;
            }

            for (int i = 1; i < res.Count; i++)
            {
                res[i].date = res[i].date.AddMilliseconds((60000 / res.Count) * i);
            }
        }
        public abstract void parser(string input);
        public override String ToString()
        {
            return url + ":" + title;
        }

        public int CompareTo(Thread other)
        {
            return url.CompareTo(other.url);
        }
        public void addRes(int number, DateTime date, string res, string id, bool nyan=false)
        {
            // 1レス目は実況じゃないのではぶく
            // 3行以上のレスは省く、ただしレスの場合分追加
            if (number > 1 &&
                Lib.count(res, "<br>") <= Math.Min(3 + Lib.count(res, "target=\"_blank\">&gt;&gt;"), 5) &&
                !ngMatch.IsMatch(res.Replace(" ", "").Replace("　", "")))
            {
                Res r = new Res(this, number, date, res, id);
                r.nyan = nyan;
                resList.Add(r);
            }

            // 前スレを探す
            if (number == 1)
            {
                Console.WriteLine(res);
                for (Match m = aTagRegex.Match(res); m.Success; m = m.NextMatch())
                {
                    string link = m.Groups["url"].Value;
                    if (link.StartsWith(url.Substring(0, 10)))
                    {
                        preThread.Add(link);
                    }
                }
            }
        }
        public bool findResNumber(int number, DateTime start)
        {
            foreach (Res res in resList)
            {
                if (res.number == number && start <= res.date)
                {
                    return true;
                }
            }
            return false;
        }
        public DateTime getStartOrEndDate(bool start)
        {
            if (resList.Count == 0)
            {
                return Lib.UNIXTIME;
            }
            if (start)
            {
                if (resList.Count > 30)
                {
                    return resList[30].date;
                }
                else
                {
                    return resList.First().date;
                }
            }
            else
            {
                return resList.Last().date;
            }
        }
    }
    public class TwoChannelThread : Thread
    {
        public TwoChannelThread(string url) : base(url)
        {
            /*
            string[] split = url.Split('/');
            if (split.Length > 1)
            {
                create = Lib.UNIXTIME.AddSeconds(long.Parse(split[6]));
            }*/
        }
        public override void parser(string input)
        {
            if (input != null)
            {
                Regex titleRegex = new Regex("<title>(?<title>.*?)</title>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                this.title = titleRegex.Match(input).Groups["title"].Value;
                if (input.Contains("<dt>"))
                {
                    foreach (string str in input.Split('\n'))
                    {
                        if (str.StartsWith("<dt>"))
                        {
                            try
                            {
                                string id = "ID:???";
                                bool nyan = false;
                                int number = int.Parse(str.Substring(4, str.IndexOf(" ") - 4).Trim());
                                int dateStart = str.IndexOf(">：") + 2;
                                // IDが無いスレ対策
                                int dateEnd = str.IndexOf("<dd>");
                                if (str.Contains("ID:"))
                                {
                                    dateEnd = str.IndexOf("ID:");
                                    id = str.Substring(dateEnd, str.IndexOf("<dd>") - dateEnd).Trim();
                                }
                                string dateStr = str.Substring(dateStart,
                                     dateEnd - dateStart).Trim();
                                if (dateStr.IndexOf("/") < 4)
                                {
                                    // 二桁西暦の場合上二桁を追加
                                    dateStr = "20" + dateStr;
                                }
                                // 時刻がおかしい場合
                                if (dateStr.Contains(NYAN))
                                {
                                    dateStr = dateStr.Substring(0, dateStr.IndexOf('('));
                                    nyan = true;
                                }
                                DateTime date = DateTime.Parse(dateStr);
                                date = date.AddMilliseconds(-date.Millisecond);

                                string res = str.Substring(str.IndexOf("<dd>")+4);
                                addRes(number, date, res, id, nyan);
                            }
                            catch (Exception e)
                            {
                                System.Console.WriteLine(e.Message);
                            }
                        }
                    }
                }
                else
                {
                    //Regex resRegex = new Regex("<div class=\"post\" id=\"(?<number>.*?)\".*?<div class=\"name\">(?<name>.*?)</div><div class=\"date\">(?<date>.*?)ID:(?<uid>.*?)</div><div class=\"message\">(?<message>.*?)</div></div>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    Regex resRegex = new Regex("<div class=\"post\" id=\"(?<number>.*?)\"(?<message>.*?)</div></div>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    Regex messageRegex = new Regex("data-userid=\"(?<uid>.*?)\" data-id.*?class=\"date\">(?<date>.*?)</.*?class=\"message\"><span class=\"escaped\">(?<message>.*?)</span>");
                        for (Match m = resRegex.Match(input); m.Success; m = m.NextMatch())
                    {
                        try
                        {
                            int number = int.Parse(m.Groups["number"].Value.Split(' ')[0]);
                            string res = m.Groups["message"].Value.Trim();
                            Match m2 = messageRegex.Match(res);
                            string[] dateStr = m2.Groups["date"].Value.Split(' ');
                            string message = m2.Groups["message"].Value.Trim() + "<br><br>";
                            string id = m2.Groups["uid"].Value.Trim().Replace("ID:", "");
                            bool nyan = dateStr.Contains(NYAN);
                            DateTime date = (nyan) ? DateTime.Parse(dateStr[0].Substring(0, dateStr[0].IndexOf('('))) : DateTime.Parse(dateStr[0] + dateStr[1]);
                            date = date.AddMilliseconds(-date.Millisecond);
                            addRes(number, date, message, id, nyan);
                        }
                        catch (Exception e)
                        {
                            //MessageBox.Show(m.Groups["message"].Value.Trim());
                        }
                    }
                }
                if (resList.Count > 0 && resList[0].nyan)
                {
                    MessageBox.Show("時刻がおかしいスレッドです。\nものすごいで取得しますか？（未実装）");
                }
            }
        }
    }
    public class JikkyouThread : Thread
    {
        public JikkyouThread(string url) : base(url)
        {
            /*
            string[] split = url.Split('/');
            if (split.Length > 1)
            {
                create = Lib.UNIXTIME.AddSeconds(long.Parse(split[6]));
            }*/
        }
        public override void parser(string input)
        {
            if (input != null)
            {
                Regex titleRegex = new Regex("<title>(?<title>.*?)</title>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                this.title = titleRegex.Match(input).Groups["title"].Value;
                Regex resRegex = new Regex("<dt><a name=\".*?\">(?<number>.*?)</a>.*?</a>：(?<date>.*?)<dd>(?<message>.*?)<br><br>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
//                Regex messageRegex = new Regex("data-userid=\"(?<uid>.*?)\" data-id.*?class=\"date\">(?<date>.*?)</.*?class=\"message\"><span class=\"escaped\">(?<message>.*?)</span>");
                for (Match m = resRegex.Match(input); m.Success; m = m.NextMatch())
                {
                    try
                    {
                        int number = int.Parse(m.Groups["number"].Value.Split(' ')[0]);
                        string res = m.Groups["message"].Value.Trim();
                        string[] dateStr = m.Groups["date"].Value.Split(' ');
                        string message = m.Groups["message"].Value.Trim() + "<br><br>";
                        string id = dateStr[2].Trim().Replace("ID:", "");
                        bool nyan = dateStr.Contains(NYAN);
                        DateTime date = DateTime.Parse(dateStr[0] + dateStr[1]);
                        date = date.AddMilliseconds(-date.Millisecond);
                        addRes(number, date, message, id, nyan);
                    }
                    catch (Exception e)
                    {
                        //MessageBox.Show(m.Groups["message"].Value.Trim());
                    }
                }
            }
        }
    }
    public class ChatChannelThread : Thread
    {
        public ChatChannelThread(string url) : base(url)
        {
            /*
            string[] split = url.Split('/');
            if (split.Length > 1)
            {
                create = Lib.UNIXTIME.AddSeconds(long.Parse(split[6]));
            }*/
        }
        public override void parser(string input)
        {
            if (input != null)
            {
                foreach (string str in input.Split(new string[] { "<dt>" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (str.StartsWith("<a"))
                    {
                        try
                        {
                            int number = int.Parse(str.Substring(str.IndexOf(">") + 1, 5).Trim().Split('<')[0]);
                            int dateStart = str.IndexOf("投稿日：") + 4;
                            string dateStr = str.Substring(dateStart,
                                str.IndexOf("ID:") - dateStart).Trim().Replace("\\(.*\\)", "");
                            if (dateStr.IndexOf("/") < 4)
                            {
                                // 二桁西暦の場合上二桁を追加
                                dateStr = "20" + dateStr;
                            }
                            DateTime date = DateTime.Parse(dateStr);
                            date = date.AddMilliseconds(-date.Millisecond);

                            string res = str.Substring(str.IndexOf("<dd>")+4);

                            addRes(number, date, res, "ID:???");
                        }
                        catch (Exception e)
                        {
                            System.Console.WriteLine(e.Message);
                        }
                    }
                    else
                    {
                        foreach (string str2 in str.Split('\n'))
                        {
                            if (str2.StartsWith("<title>"))
                            {
                                this.title = str2.Replace("<title>", "").Replace("</title>", "");
                            }
                        }
                    }
                }
            }
        }
    }
    public class ChatChannelThread2 : Thread
    {
        public ChatChannelThread2(string url) : base(url)
        {
            /*
            string[] split = url.Split('/');
            if (split.Length > 1)
            {
                create = Lib.UNIXTIME.AddSeconds(long.Parse(split[6]));
            }*/
        }
        public override void parser(string input)
        {
            if (input != null)
            {
                foreach (string str in input.Split(new string[] { "<dt>" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (str.Contains("名前："))
                    {
                        try
                        {
                            int number = int.Parse(str.Substring(0, str.IndexOf(" ")).Trim());
                            int dateStart = str.IndexOf("投稿日：") + 4;
                            string dateStr = str.Substring(dateStart,
                                str.IndexOf("ID:") - dateStart).Trim().Replace("\\(.*\\)", "");
                            if (dateStr.IndexOf("/") < 4)
                            {
                                // 二桁西暦の場合上二桁を追加
                                dateStr = "20" + dateStr;
                            }
                            DateTime date = DateTime.Parse(dateStr);
                            date = date.AddMilliseconds(-date.Millisecond);

                            string res = str.Substring(str.IndexOf("<dd>")+4);
                            addRes(number, date, res, "ID:???");
                        }
                        catch (Exception e)
                        {
                            System.Console.WriteLine(e.Message);
                        }
                    }
                    else
                    {
                        foreach (string str2 in str.Split('\n'))
                        {
                            if (str2.StartsWith("<title>"))
                            {
                                this.title = str2.Replace("<title>", "").Replace("</title>", "");
                            }
                        }
                    }
                }
            }
        }
    }
    public class DatThread : Thread
    {
        public DatThread(string url) : base(url)
        {
            /*
            string[] split = url.Split('/');
            if (split.Length > 1)
            {
                create = Lib.UNIXTIME.AddSeconds(long.Parse(split[split.Length-1].Replace(".dat", "")));
            }*/
        }
        public override void parser(string input)
        {
            if (input != null)
            {
                Regex resRegex = new Regex("(?<name>.*?)<>(?<mail>.*?)<>(?<date>.*?)<>(?<message>.*?)<>(?<title>.*?)\n", RegexOptions.IgnoreCase);
                int number = 1;
                for (Match m = resRegex.Match(input); m.Success; m = m.NextMatch())
                {
                    try
                    {
                        string id = "";
                        string dateStr = m.Groups["date"].Value;
                        if (dateStr.Contains(" ID:"))//ID が含まれないスレッド対策
                        {
                            id += dateStr.Substring(dateStr.IndexOf(" ID:")).Trim();
                            dateStr = dateStr.Substring(0, dateStr.IndexOf(" ID:"));
                        }
                        string message = m.Groups["message"].Value.Trim() + "<br><br>";
                        string title = m.Groups["title"].Value.Trim();
                        if (title != "")
                        {
                            this.title = title;
                        }
                        bool nyan = dateStr.Contains(NYAN);
                        DateTime date = DateTime.Parse((nyan) ? dateStr.Substring(0, dateStr.IndexOf('(')) : dateStr);
                        date = date.AddMilliseconds(-date.Millisecond);
                        addRes(number, date, message, id, nyan);
                    }
                    catch { }
                    number++;
                }
                if (resList.Count > 0 && resList[0].nyan)
                {
                    MessageBox.Show("時刻がおかしいスレッドです。\nものすごいで取得しますか？（未実装）");
                }
            }
        }
    }
    //<dl id="70"> <dt> 70 ： <span class="em"><b>名無しステーション </b></span>[sage] 投稿日：2019/03/31(日) 09:30:21.76 ID:<a class="id_color10" href="/r/2ch.sc/liveanb/1553861463/ID:oZaJK3960.net" rel="nofollow">oZaJK3960.net</a> [3/23回] </dt> <dd> ぐふっふっふー </dd> </dl>
    public class LogsokuThread : Thread
    {
        public static Regex titleRegex = new Regex("<div class=\"thread_title\"> <table> <tr> <td class=\"title\"> <h1> <a .*?title=\"(?<title>.*?)\">", RegexOptions.IgnoreCase);
        //        public static Regex resRegex = new Regex("<dl id=\"(?<number>.*?)\">.*?：(?<date>.*?)<dd> (?<message>.*?) </dd></dl>", RegexOptions.IgnoreCase);
        public static Regex resRegex = new Regex("<dl id=\"(?<number>.*?)\">.*? 投稿日：(?<date>.*?) ID:.*?rel=\"nofollow\">(?<id>.*?)</a>.*?<dd> (?<message>.*?) </dd> </dl>", RegexOptions.IgnoreCase);
        public LogsokuThread(string url) : base(url)
        {
            this.charset = "utf-8";
/*            string[] split = url.Split('/');
            if (split.Length > 1)
            {
                create = Lib.UNIXTIME.AddSeconds(long.Parse(split[split.Length-2]));
            }*/
        }
        public override void parser(string input)
        {
            if (input != null)
            {
                this.title = titleRegex.Match(input).Groups["title"].Value;
                for (Match m = resRegex.Match(input); m.Success; m = m.NextMatch())
                {
                    try
                    {
                        string dateStr = m.Groups["date"].Value;
                        string message = m.Groups["message"].Value.Trim() + "<br><br>";
                        string id = "ID:" + m.Groups["id"].Value;
                        int number = int.Parse(m.Groups["number"].Value.Trim());
                        bool nyan = dateStr.Contains(NYAN) || dateStr.Contains("??");
                        DateTime date = DateTime.Parse((nyan) ? dateStr.Substring(0, dateStr.IndexOf('(')) : dateStr);
                        date = date.AddMilliseconds(-date.Millisecond);
                        addRes(number, date, message, id, nyan);
                    }
                    catch (Exception e) { System.Console.WriteLine(e); }
                }
            }
            if (resList.Count > 0 && resList[0].nyan)
            {
                MessageBox.Show("時刻がおかしいスレッドです。\nものすごいで取得しますか？（未実装）");
            }
        }
    }
    public class Res : IComparable<Res>
    {
        public Thread owner;
        public int number = 0;
        public DateTime date = DateTime.Now;
        public string res = "";
        public string id = "";
        public bool nyan = false;
        public Res(Thread owner, int number, DateTime date, string res, string id)
        {
            this.owner = owner;
            this.number = number;
            this.date = date;
            this.res = res;
            this.id = id;
        }

        public int CompareTo(Res other)
        {
            if (this.date == null)
            {
                return -1;
            }
            else if (other == null)
            {
                return 1;
            }
            else
            {
                int result = date.CompareTo(other.date);
                if (result == 0)
                {
                    result = owner.url.CompareTo(other.owner.url);
                    if (result == 0)
                    {
                        return number.CompareTo(other.number);
                    }
                    return result;
                }
                return result;
            }
        }
    }
    [System.Xml.Serialization.XmlRoot("j")]
    public class JikkyouXML
    {
        [System.Xml.Serialization.XmlAttribute("s")]
        public double start = 0;
        [System.Xml.Serialization.XmlElement("t")]
        public List<ThreadXML> threadList = new List<ThreadXML>();
        [System.Xml.Serialization.XmlElement("r")]
        public List<ResXML> resList = new List<ResXML>();

        private List<string> idList = new List<string>();
        private double end = 0;
        public class ResXML
        {
            [System.Xml.Serialization.XmlAttribute("n")]
            public string number = "";
            [System.Xml.Serialization.XmlAttribute("i")]
            public string id = "";
            [System.Xml.Serialization.XmlAttribute("t")]
            public string text = "";
            [System.Xml.Serialization.XmlAttribute("e")]
            public double elapsed = 0;
        }
        public class ThreadXML
        {
            [System.Xml.Serialization.XmlAttribute("t")]
            public string title = "";
            [System.Xml.Serialization.XmlAttribute("u")]
            public string url = "";
        }
        public void addRes(string number, string id, string text, DateTime date)
        {
            if (!idList.Contains(id))
            {
                idList.Add(id);
            }
            if (start == 0)
            {
                setStart(date);
            }
            ResXML x = new ResXML();
            x.number = number;
            x.id = "" + idList.IndexOf(id);
            x.text = text;
            x.elapsed = Lib.toUnixTime(date) - end;
            if (x.elapsed < 0)
            {
                MessageBox.Show("経過時間マイナス");
                return;
            }
            end += x.elapsed; // 経過時間は前回レスとの差秒
            resList.Add(x);
        }
        public void addThread(string title, string url)
        {
            // 同じのが無いかチェック
            foreach (ThreadXML e in threadList)
            {
                if (e.url == url)
                {
                    return;
                }
            }
            ThreadXML t = new ThreadXML();
            t.title = title;
            t.url = url;
            threadList.Add(t);
        }
        public void setStart(DateTime date)
        {
            start = Lib.toUnixTime(date);
            end = start;
        }
        public bool containsRes(string number)
        {
            foreach (ResXML r in resList)
            {
                if (r.number == number)
                {
                    return true;
                }
            }
            return false;
        }
    }
    public abstract class AbstractTreeNode : System.Windows.Forms.TreeNode
    {
        public const string DUMMY = "dummy";
        public string charset = "shift_jis";
        public string url = "";
        public bool isRead = true;
        public bool isClear = false;
        public string id = "";
        public AbstractTreeNode(string url)
        {
            this.url = url;
            this.Nodes.Add(DUMMY);
        }
        public virtual async void refreshChild()
        {
            if (isClear || this.Nodes.Count <= 1)
            {
                this.Nodes.Clear();
                string input = "";
                if (isRead)
                {
                    input = await Lib.read(url, charset);
                }
                this.TreeView.BeginUpdate();
                try
                {

                afterread(input);
                }
                catch { };
                this.TreeView.EndUpdate();
            }
        }
        protected abstract void afterread(string input);
        protected virtual void filter(string str)
        {
            foreach (TreeNode node in Nodes)
            {
                if (node is AbstractTreeNode)
                {
                    ((AbstractTreeNode)node).filter(str);
                }
            }
        }
    }
    public class BoardTreeNode : AbstractTreeNode
    {
        public BoardTreeNode(string url, string id) : base (url)
        {
            this.id = id;
            Text = "最新スレッド";
            this.isClear = true;
        }
        protected override void afterread(string input)
        {
            foreach (string str in input.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (str.StartsWith("<a") && str.Contains("l50") && !str.Contains("浪人はこんなに便利"))
                {
                    int start = str.IndexOf("\"")+1;
                    string dateStr = str.Substring(start, str.IndexOf("/")-start);
                    string[] split = url.Split('/');
                    DateTime create = Lib.UNIXTIME.AddSeconds(long.Parse(dateStr));
                    AbstractTreeNode child = new ThreadTreeNode("http://" + split[2] + "/test/read.cgi/" + split[3] + "/" + dateStr + "/",
                        create, str.Substring(str.LastIndexOf("(")).Replace("</a>", ""), str.Substring(str.IndexOf(":") + 1).Trim());
                    this.Nodes.Add(child);
                }
            }
        }
    }
    public class MonoTreeNode : AbstractTreeNode
    {
        public MonoTreeNode(string url, string id) : base(url)
        {
            this.id = id;
            Text = "ものすごい";
            isClear = true;
            isRead = false;
        }
        protected override void afterread(string input)
        {
            int nowYear = DateTime.Now.Year;
            for (int i = DateTime.Now.Year; i > 2000; i--)
            {
                this.Nodes.Add(new YearTreeNode(id, i));
            }
        }
    }
    public class LogsokuTreeNode : AbstractTreeNode
    {
        public static Regex THREAD = new Regex("<td class=\"length\">(?<size>.*?)</td>.*?<td class=\"title\"> <a href=\"(?<url>.*?)\" title=.*?>(?<title>.*?)</a> </td> <td class=\"date\"> (?<date>.*?) </td>", RegexOptions.IgnoreCase);
        public static Regex SIZE = new Regex("<h3 style=\"text-align:center;\">(?<size>.*?)件中", RegexOptions.IgnoreCase);

        public bool root = true;
        public LogsokuTreeNode(string id, bool root=true, string page="") : base("http://www.logsoku.com/bbs/2ch.net/"　+ id + "/" + page + "?sort=create&order=desc&sr=10&active=0")
        {
            this.id = id;
            this.root = root;
            this.Text = page.Equals("") ? "ログ速" : page;
            this.charset = "utf-8";
        }
        protected override void afterread(string input)
        {
            try
            {
                List<TreeNode> child = new List<TreeNode>();
                for (Match m = THREAD.Match(input); m.Success; m = m.NextMatch())
                {
                    string size = m.Groups["size"].Value;
                    string url = m.Groups["url"].Value;
                    string title = m.Groups["title"].Value;
                    string date = m.Groups["date"].Value;
                    TreeNode add = new ThreadTreeNode("http://www.logsoku.com" + url, DateTime.Parse(date), size, title);
                    child.Add(add);
                }
                if (root)
                {
                    TreeNode node1 = new TreeNode("1");
                    node1.Nodes.AddRange(child.ToArray());
                    this.Nodes.Add(node1);

                    string size = SIZE.Match(input).Groups["size"].Value;
                    for (int i = 1; i < (int.Parse(size) / 50); i++)
                    {
                        this.Nodes.Add(new LogsokuTreeNode(id, false, "" + (i+1)));
                    }
                }
                else
                {
                    this.Nodes.AddRange(child.ToArray());
                }
            }
            catch (Exception e) { System.Console.WriteLine(e); }
        }
    }
    public class KakoAllTreeNode : AbstractTreeNode
    {
        public KakoAllTreeNode(string url) : base(url)
        {

        }
        protected override void afterread(string input)
        {
            try
            {
                foreach (string str in input.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string kako = str.Substring(0, str.IndexOf("<"));
                    AbstractTreeNode child = new KakoTreeNode(url.Replace("subject.txt", "") + kako + "/subject.txt");
                    child.Text = Lib.UNIXTIME.AddSeconds(long.Parse(kako.Replace("o", "")) * 1000000).ToString();
                    this.Nodes.Add(child);
                }

            }
            catch
            {

            }
        }
    }
    public class KakoTreeNode : AbstractTreeNode
    {
        public KakoTreeNode(string url) : base(url)
        {

        }
        protected override void afterread(string input)
        {
            List<TreeNode> children = new List<TreeNode>();
            foreach (string str in input.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                string resNum = str.Substring(str.LastIndexOf("("));
                // レス数が少ない場合は作らない
                if (long.Parse(resNum.Replace("(", "").Replace(")", "")) > 50)
                {
                    string dateStr = str.Substring(0, str.IndexOf(".dat"));
                    string[] split = url.Split('/');
                    DateTime create = Lib.UNIXTIME.AddSeconds(long.Parse(dateStr));
                    AbstractTreeNode child = new ThreadTreeNode("http://" + split[2] + "/test/read.cgi/" + split[3] + "/" + dateStr + "/",
                        create, resNum, str.Substring(str.IndexOf(">")+1).Trim());
                    children.Add(child);
                }
            }
            children.Sort(new TreeNodeSoreter());
            this.Nodes.AddRange(children.ToArray());
        }
    }
    public class KakoTreeNode2 : AbstractTreeNode
    {
        public static Regex THREAD = new Regex("<p class=.*?<span class=\"filename\"><a href=\"(?<url>.*?)\">.*?</a></span><span class=\"title\">(?<title>.*?)</span><span class=\"lines\">(?<num>.*?)</span></p>", RegexOptions.IgnoreCase);
        public static Regex THREAD2 = new Regex("<p class=.*?<span class=\"filename\">.*<a href=\"(?<url>.*?)\">(?<title>.*?)</a></span><span class=\"lines\">(?<num>.*?)</span></p>", RegexOptions.IgnoreCase);
        public static Regex MENU = new Regex("<p class=\"menu_link\"><a href=\"(?<url>.*?)\">(?<range>.*?)</a></p>", RegexOptions.IgnoreCase);
        public static Regex HERE = new Regex("<p class=\"menu_here\">(?<text>.*?) <span class=\"menu_here\">HERE</span></p>", RegexOptions.IgnoreCase);
        public static Regex URLREP = new Regex("net/.*", RegexOptions.IgnoreCase);
        public static Regex MENUREP = new Regex("kako0000.*", RegexOptions.IgnoreCase);
        public KakoTreeNode2(string url) : base(url)
        {
            charset = "utf-8";
            isClear = true;
        }
        protected override void afterread(string input)
        {
            List<TreeNode> children = new List<TreeNode>();
            Regex regexThis = (THREAD.IsMatch(input)) ? THREAD : THREAD2;

            for (Match m = regexThis.Match(input); m.Success; m = m.NextMatch())
            {
                try
                {
                    string cUrl = m.Groups["url"].Value;
                    string num = m.Groups["num"].Value;
                    string title = m.Groups["title"].Value;
                    string[] split = cUrl.Split('/');
                    string dateStr = split[4];
                    // レス数が少ない場合は作らない
                    if (long.Parse(num) < 50)
                    {
                        continue;
                    }
                    DateTime cCreate = Lib.UNIXTIME.AddSeconds(long.Parse(dateStr));
                    AbstractTreeNode child = new ThreadTreeNode(URLREP.Replace(url, "net" + cUrl), cCreate, "(" + num + ")", title);
                    children.Add(child);
                }
                catch { }
            }

            if (MENUREP.IsMatch(url))
            {
                for (Match m = MENU.Match(input); m.Success; m = m.NextMatch())
                {
                    try
                    {
                        string cUrl = m.Groups["url"].Value.Substring(2);
                        string[] range = m.Groups["range"].Value.Split('-');
                        KakoTreeNode2 child = new KakoTreeNode2(MENUREP.Replace(url, cUrl));
                        child.isClear = false;
                        child.Text = Lib.UNIXTIME.AddSeconds(long.Parse(range[1])).ToString() + "-" + Lib.UNIXTIME.AddSeconds(long.Parse(range[0])).ToString();
                        this.Nodes.Add(child);
                    }
                    catch { }
                }
 
                TreeNode kako0001 = new TreeNode();
                string[] hereRange = HERE.Match(input).Groups["text"].Value.Split('-');
                kako0001.Text = Lib.UNIXTIME.AddSeconds(long.Parse(hereRange[1])).ToString() + "-" + Lib.UNIXTIME.AddSeconds(long.Parse(hereRange[0])).ToString();
                kako0001.Nodes.AddRange(children.ToArray());
                this.Nodes.Insert(0, kako0001);
            }
            else
            {
                this.Nodes.AddRange(children.ToArray());
            }
        }
    }
    public class ThreadTreeNode :AbstractTreeNode
    {
        public static Regex DELETE = new Regex("<.*?>|\\[.*?\\]|\\(.*?\\)|【.*?】|\\{.*?\\}|「.*?」", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        public DateTime create = DateTime.Now;
        public string resNum = "(0)";
        public string title = "";
        public ThreadTreeNode(string url, DateTime date, string resNum, string title) : base(url)
        {
            this.create = date;
            this.resNum = resNum;
            this.title = title;
            this.Text = create.ToString("yy/MM/dd HH:mm[ddd]") + resNum + title;
            isRead = false;
        }
        protected override void afterread(string input)
        {
            // 既に読み込んでいるものでタイトルが似ているものを子ノードに追加
            string repTitle = DELETE.Replace(title, "");
            addSimilarNode(repTitle.Substring(0, (repTitle.Length >= 5) ? 5 : repTitle.Length), this.TreeView.Nodes);
        }
        private void addSimilarNode(string s, TreeNodeCollection nodes)
        {
            foreach (TreeNode sibling in nodes)
            {
                if (sibling is ThreadTreeNode)
                {
                    ThreadTreeNode target = sibling as ThreadTreeNode;
                    string repTitle = DELETE.Replace(target.title, "");
                    string t = repTitle.Substring(0, (repTitle.Length >= 5) ? 5 : repTitle.Length);
                    if (2 > Lib.LevenshteinDistance(s, t))
                    {
                        ThreadTreeNode add = ThreadTreeNode.copy((ThreadTreeNode)sibling);
                        add.Nodes.Clear();
                        this.Nodes.Add(add);
                    }
                }
                else
                {
                    addSimilarNode(s, sibling.Nodes);
                }

            }
        }
        public static ThreadTreeNode copy(ThreadTreeNode src)
        {
            ThreadTreeNode result = new ThreadTreeNode(src.url, src.create, src.resNum, src.title);
            result.Text = src.Text;
            return result;
        }
    }
    public class YearTreeNode : AbstractTreeNode
    {
        public int year = 0;
        public YearTreeNode(string url, int year) : base(url)
        {
            this.isRead = false;
            this.year = year;
            this.Text = year.ToString();
            this.id = url;
        }
        protected override void afterread(string input)
        {
            for (int i = 12; i >= 1; i--)
            {
                MonthTreeNode add = new MonthTreeNode(url, year, i);
                if (DateTime.Now.CompareTo(DateTime.Parse(year+"/"+i)) >= 0)
                {
                    this.Nodes.Add(add);
                }
            }
        }
    }
    public class MonthTreeNode : AbstractTreeNode
    {
        public int year = 0;
        public int month = 0;
        public MonthTreeNode(string url, int year, int month) : base(url)
        {
            this.isRead = false;
            this.year = year;
            this.month = month;
            this.Text = month.ToString();
            this.id = url;
        }
        protected override void afterread(string input)
        {
            for (int i = 31; i >= 1; i--)
            {
                try
                {
                    DateTime addDate = DateTime.Parse("" + year + "/" + month + "/" + i);
                    DateTreeNode add = new DateTreeNode(url, addDate);
                    if (DateTime.Now.CompareTo(addDate) >= 0)
                    {
                        this.Nodes.Add(add);
                    }
                }
                catch
                {

                }
            }
        }
    }
    public class DateTreeNode : AbstractTreeNode
    {
        public DateTime create = DateTime.Now;
        public DateTreeNode(string url, DateTime date) : base(url)
        {
            this.create = date;
            this.Text = date.ToString("dd(ddd)");
            this.id = url;
            this.url = "http://2chlog.com/2ch/live/makeimghtml_m.php?date=" + create.ToShortDateString().Replace("/", "") + "&ita=" + url;
        }
        protected override void afterread(string input)
        {
            Regex resRegex = new Regex("<a href=(?<url>.*?)>(?<title>.*?)</a>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            List<ThreadTreeNode> datList = new List<ThreadTreeNode>();
            for (Match m = resRegex.Match(input); m.Success; m = m.NextMatch())
            {
                try
                {
                    string cUrl = m.Groups["url"].Value;
                    if (cUrl.Contains("2ch.net") || cUrl.Contains("5ch.net"))
                    {
                        string[] split = cUrl.Split('/');
                        string dateStr = split[6];
                        string title = Regex.Replace(m.Groups["title"].Value, "<font.*</font>", "").Trim();
                        string datUrl = (DateTime.Parse("2017/06/01").CompareTo(create) <= 0) ?
                            "http://2chlog.com/2ch/live/" + id + "/dat/" + dateStr + ".dat" : // 201706以降は年月日なし
                            "http://2chlog.com/2ch/live/" + id+"/dat" + create.Year + "/" + (create.Month < 10 ? "0" + create.Month : "" + create.Month) + "/" + dateStr + ".dat";
                        // レス数が少ない場合は作らない
                        if (title.Contains("(") && long.Parse(title.Substring(title.LastIndexOf("(")).Replace("(", "").Replace(")", "")) < 50)
                        {
                            continue;
                        }
                        DateTime cCreate = Lib.UNIXTIME.AddSeconds(long.Parse(dateStr));
                        datList.Add(new ThreadTreeNode(datUrl, cCreate, "(0)", "DAT:"+title));
                        this.Nodes.Add(new ThreadTreeNode(cUrl, cCreate, "(0)", "2CH:"+title));
                    }
                }
                catch { }
            }
            this.Nodes.AddRange(datList.ToArray());
        }
    }
    public class TreeNodeSoreter : IComparer<TreeNode>
    {
        public int Compare(TreeNode x, TreeNode y)
        {
            return -x.Text.CompareTo(y.Text);
        }
    }
}
