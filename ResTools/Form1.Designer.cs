namespace ResTools
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ThreadList = new System.Windows.Forms.ListView();
            this.startend = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.threadListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.open = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ResList = new System.Windows.Forms.ListView();
            this.date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.res = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.resListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EndBar = new System.Windows.Forms.TrackBar();
            this.StartLabel = new System.Windows.Forms.Label();
            this.EndLabel = new System.Windows.Forms.Label();
            this.ResetButton = new System.Windows.Forms.Button();
            this.openCheckbox = new System.Windows.Forms.CheckBox();
            this.tempCheckBox = new System.Windows.Forms.CheckBox();
            this.sort = new System.Windows.Forms.CheckBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.ListButton = new System.Windows.Forms.Button();
            this.sameTime = new System.Windows.Forms.CheckBox();
            this.tab = new System.Windows.Forms.TabControl();
            this.weekly = new System.Windows.Forms.TabPage();
            this.livebs2 = new System.Windows.Forms.TabPage();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.livebs = new System.Windows.Forms.TabPage();
            this.treeView3 = new System.Windows.Forms.TreeView();
            this.livenhk = new System.Windows.Forms.TabPage();
            this.treeView4 = new System.Windows.Forms.TreeView();
            this.liveetv = new System.Windows.Forms.TabPage();
            this.treeView5 = new System.Windows.Forms.TreeView();
            this.livecx = new System.Windows.Forms.TabPage();
            this.treeView6 = new System.Windows.Forms.TreeView();
            this.livetbs = new System.Windows.Forms.TabPage();
            this.treeView7 = new System.Windows.Forms.TreeView();
            this.liveabs = new System.Windows.Forms.TabPage();
            this.treeView8 = new System.Windows.Forms.TreeView();
            this.liventv = new System.Windows.Forms.TabPage();
            this.treeView9 = new System.Windows.Forms.TreeView();
            this.livetx = new System.Windows.Forms.TabPage();
            this.treeView10 = new System.Windows.Forms.TreeView();
            this.liveradio = new System.Windows.Forms.TabPage();
            this.treeView11 = new System.Windows.Forms.TreeView();
            this.testCheckBox = new System.Windows.Forms.CheckBox();
            this.minutes5 = new System.Windows.Forms.RadioButton();
            this.minutes10 = new System.Windows.Forms.RadioButton();
            this.minutes15 = new System.Windows.Forms.RadioButton();
            this.minutes30 = new System.Windows.Forms.RadioButton();
            this.minutes60 = new System.Windows.Forms.RadioButton();
            this.minutes120 = new System.Windows.Forms.RadioButton();
            this.next = new System.Windows.Forms.Button();
            this.nglist = new System.Windows.Forms.Button();
            this.transform = new System.Windows.Forms.Button();
            this.radioJane = new System.Windows.Forms.RadioButton();
            this.radioDat = new System.Windows.Forms.RadioButton();
            this.radioHtml = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.threadListMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EndBar)).BeginInit();
            this.tab.SuspendLayout();
            this.weekly.SuspendLayout();
            this.livebs2.SuspendLayout();
            this.livebs.SuspendLayout();
            this.livenhk.SuspendLayout();
            this.liveetv.SuspendLayout();
            this.livecx.SuspendLayout();
            this.livetbs.SuspendLayout();
            this.liveabs.SuspendLayout();
            this.liventv.SuspendLayout();
            this.livetx.SuspendLayout();
            this.liveradio.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ThreadList
            // 
            this.ThreadList.AllowDrop = true;
            this.ThreadList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ThreadList.CheckBoxes = true;
            this.ThreadList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.startend});
            this.ThreadList.ContextMenuStrip = this.threadListMenu;
            this.ThreadList.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ThreadList.FullRowSelect = true;
            this.ThreadList.HideSelection = false;
            this.ThreadList.Location = new System.Drawing.Point(596, 4);
            this.ThreadList.MultiSelect = false;
            this.ThreadList.Name = "ThreadList";
            this.ThreadList.ShowItemToolTips = true;
            this.ThreadList.Size = new System.Drawing.Size(416, 216);
            this.ThreadList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ThreadList.TabIndex = 0;
            this.ThreadList.UseCompatibleStateImageBehavior = false;
            this.ThreadList.View = System.Windows.Forms.View.Details;
            this.ThreadList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ThreadList_ItemChecked);
            this.ThreadList.DragDrop += new System.Windows.Forms.DragEventHandler(this.ThreadList_DragDrop);
            this.ThreadList.DragEnter += new System.Windows.Forms.DragEventHandler(this.ThreadList_DragEnter);
            this.ThreadList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ThreadList_MouseDoubleClick);
            // 
            // startend
            // 
            this.startend.Text = "開始-終了";
            this.startend.Width = 1000;
            // 
            // threadListMenu
            // 
            this.threadListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.open});
            this.threadListMenu.Name = "threadListMenu";
            this.threadListMenu.Size = new System.Drawing.Size(140, 26);
            this.threadListMenu.Opening += new System.ComponentModel.CancelEventHandler(this.threadListMenu_Opening);
            this.threadListMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.threadListMenu_ItemClicked);
            // 
            // open
            // 
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(139, 22);
            this.open.Text = "ブラウザで開く";
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.Location = new System.Drawing.Point(864, 712);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 21);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "保存";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ResList
            // 
            this.ResList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.date,
            this.res});
            this.ResList.ContextMenuStrip = this.resListMenu;
            this.ResList.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ResList.FullRowSelect = true;
            this.ResList.HideSelection = false;
            this.ResList.Location = new System.Drawing.Point(596, 224);
            this.ResList.MultiSelect = false;
            this.ResList.Name = "ResList";
            this.ResList.Size = new System.Drawing.Size(416, 360);
            this.ResList.TabIndex = 3;
            this.ResList.UseCompatibleStateImageBehavior = false;
            this.ResList.View = System.Windows.Forms.View.Details;
            this.ResList.SelectedIndexChanged += new System.EventHandler(this.ResList_SelectedIndexChanged);
            this.ResList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ResList_KeyDown);
            // 
            // date
            // 
            this.date.Text = "時間";
            this.date.Width = 50;
            // 
            // res
            // 
            this.res.Text = "レス";
            this.res.Width = 400;
            // 
            // resListMenu
            // 
            this.resListMenu.Name = "contextMenuStrip1";
            this.resListMenu.Size = new System.Drawing.Size(61, 4);
            this.resListMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.resListMenu_ItemClicked);
            // 
            // EndBar
            // 
            this.EndBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.EndBar.Location = new System.Drawing.Point(600, 620);
            this.EndBar.Name = "EndBar";
            this.EndBar.Size = new System.Drawing.Size(402, 45);
            this.EndBar.TabIndex = 4;
            this.EndBar.TickFrequency = 0;
            this.EndBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.EndBar.Value = 10;
            this.EndBar.Scroll += new System.EventHandler(this.StartBar_Scroll);
            // 
            // StartLabel
            // 
            this.StartLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StartLabel.AutoSize = true;
            this.StartLabel.BackColor = System.Drawing.Color.Transparent;
            this.StartLabel.Location = new System.Drawing.Point(600, 606);
            this.StartLabel.Name = "StartLabel";
            this.StartLabel.Size = new System.Drawing.Size(132, 11);
            this.StartLabel.TabIndex = 5;
            this.StartLabel.Text = "開始:2015/09/01 00:00:00";
            // 
            // EndLabel
            // 
            this.EndLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.EndLabel.AutoSize = true;
            this.EndLabel.BackColor = System.Drawing.Color.Transparent;
            this.EndLabel.Location = new System.Drawing.Point(738, 606);
            this.EndLabel.Name = "EndLabel";
            this.EndLabel.Size = new System.Drawing.Size(132, 11);
            this.EndLabel.TabIndex = 5;
            this.EndLabel.Text = "終了:2015/09/01 00:00:00";
            this.EndLabel.Click += new System.EventHandler(this.EndLabel_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetButton.Location = new System.Drawing.Point(940, 712);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 21);
            this.ResetButton.TabIndex = 2;
            this.ResetButton.Text = "リセット";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // openCheckbox
            // 
            this.openCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openCheckbox.AutoSize = true;
            this.openCheckbox.Location = new System.Drawing.Point(600, 684);
            this.openCheckbox.Name = "openCheckbox";
            this.openCheckbox.Size = new System.Drawing.Size(83, 15);
            this.openCheckbox.TabIndex = 6;
            this.openCheckbox.Text = "保存後に開く";
            this.openCheckbox.UseVisualStyleBackColor = true;
            // 
            // tempCheckBox
            // 
            this.tempCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tempCheckBox.AutoSize = true;
            this.tempCheckBox.Location = new System.Drawing.Point(600, 704);
            this.tempCheckBox.Name = "tempCheckBox";
            this.tempCheckBox.Size = new System.Drawing.Size(68, 15);
            this.tempCheckBox.TabIndex = 6;
            this.tempCheckBox.Text = "一次保存";
            this.tempCheckBox.UseVisualStyleBackColor = true;
            this.tempCheckBox.CheckedChanged += new System.EventHandler(this.tempCheckBox_CheckedChanged);
            // 
            // sort
            // 
            this.sort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sort.AutoSize = true;
            this.sort.Checked = true;
            this.sort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sort.Location = new System.Drawing.Point(600, 663);
            this.sort.Name = "sort";
            this.sort.Size = new System.Drawing.Size(65, 15);
            this.sort.TabIndex = 6;
            this.sort.Text = "レスソート";
            this.sort.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(584, 707);
            this.treeView1.TabIndex = 7;
            // 
            // ListButton
            // 
            this.ListButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ListButton.Location = new System.Drawing.Point(940, 688);
            this.ListButton.Name = "ListButton";
            this.ListButton.Size = new System.Drawing.Size(75, 21);
            this.ListButton.TabIndex = 2;
            this.ListButton.Text = "リスト作成";
            this.ListButton.UseVisualStyleBackColor = true;
            this.ListButton.Click += new System.EventHandler(this.ListButton_Click);
            // 
            // sameTime
            // 
            this.sameTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sameTime.AutoSize = true;
            this.sameTime.Location = new System.Drawing.Point(684, 664);
            this.sameTime.Name = "sameTime";
            this.sameTime.Size = new System.Drawing.Size(65, 15);
            this.sameTime.TabIndex = 6;
            this.sameTime.Text = "同じ時間";
            this.sameTime.UseVisualStyleBackColor = true;
            // 
            // tab
            // 
            this.tab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tab.Controls.Add(this.weekly);
            this.tab.Controls.Add(this.livebs2);
            this.tab.Controls.Add(this.livebs);
            this.tab.Controls.Add(this.livenhk);
            this.tab.Controls.Add(this.liveetv);
            this.tab.Controls.Add(this.livecx);
            this.tab.Controls.Add(this.livetbs);
            this.tab.Controls.Add(this.liveabs);
            this.tab.Controls.Add(this.liventv);
            this.tab.Controls.Add(this.livetx);
            this.tab.Controls.Add(this.liveradio);
            this.tab.Location = new System.Drawing.Point(4, 4);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(592, 732);
            this.tab.TabIndex = 8;
            // 
            // weekly
            // 
            this.weekly.Controls.Add(this.treeView1);
            this.weekly.Location = new System.Drawing.Point(4, 21);
            this.weekly.Name = "weekly";
            this.weekly.Size = new System.Drawing.Size(584, 707);
            this.weekly.TabIndex = 0;
            this.weekly.Text = "番組ch";
            this.weekly.UseVisualStyleBackColor = true;
            // 
            // livebs2
            // 
            this.livebs2.Controls.Add(this.treeView2);
            this.livebs2.Location = new System.Drawing.Point(4, 21);
            this.livebs2.Name = "livebs2";
            this.livebs2.Size = new System.Drawing.Size(584, 707);
            this.livebs2.TabIndex = 1;
            this.livebs2.Text = "BS無料";
            this.livebs2.UseVisualStyleBackColor = true;
            // 
            // treeView2
            // 
            this.treeView2.CheckBoxes = true;
            this.treeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.treeView2.Location = new System.Drawing.Point(0, 0);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(584, 707);
            this.treeView2.TabIndex = 8;
            // 
            // livebs
            // 
            this.livebs.Controls.Add(this.treeView3);
            this.livebs.Location = new System.Drawing.Point(4, 21);
            this.livebs.Name = "livebs";
            this.livebs.Size = new System.Drawing.Size(584, 707);
            this.livebs.TabIndex = 2;
            this.livebs.Text = "BSNHK";
            this.livebs.UseVisualStyleBackColor = true;
            // 
            // treeView3
            // 
            this.treeView3.CheckBoxes = true;
            this.treeView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.treeView3.Location = new System.Drawing.Point(0, 0);
            this.treeView3.Name = "treeView3";
            this.treeView3.Size = new System.Drawing.Size(584, 707);
            this.treeView3.TabIndex = 8;
            // 
            // livenhk
            // 
            this.livenhk.Controls.Add(this.treeView4);
            this.livenhk.Location = new System.Drawing.Point(4, 21);
            this.livenhk.Name = "livenhk";
            this.livenhk.Size = new System.Drawing.Size(584, 707);
            this.livenhk.TabIndex = 3;
            this.livenhk.Text = "NHK";
            this.livenhk.UseVisualStyleBackColor = true;
            // 
            // treeView4
            // 
            this.treeView4.CheckBoxes = true;
            this.treeView4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView4.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.treeView4.Location = new System.Drawing.Point(0, 0);
            this.treeView4.Name = "treeView4";
            this.treeView4.Size = new System.Drawing.Size(584, 707);
            this.treeView4.TabIndex = 8;
            // 
            // liveetv
            // 
            this.liveetv.Controls.Add(this.treeView5);
            this.liveetv.Location = new System.Drawing.Point(4, 21);
            this.liveetv.Name = "liveetv";
            this.liveetv.Size = new System.Drawing.Size(584, 707);
            this.liveetv.TabIndex = 4;
            this.liveetv.Text = "教育";
            this.liveetv.UseVisualStyleBackColor = true;
            // 
            // treeView5
            // 
            this.treeView5.CheckBoxes = true;
            this.treeView5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView5.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.treeView5.Location = new System.Drawing.Point(0, 0);
            this.treeView5.Name = "treeView5";
            this.treeView5.Size = new System.Drawing.Size(584, 707);
            this.treeView5.TabIndex = 8;
            // 
            // livecx
            // 
            this.livecx.Controls.Add(this.treeView6);
            this.livecx.Location = new System.Drawing.Point(4, 21);
            this.livecx.Name = "livecx";
            this.livecx.Size = new System.Drawing.Size(584, 707);
            this.livecx.TabIndex = 5;
            this.livecx.Text = "フジ";
            this.livecx.UseVisualStyleBackColor = true;
            // 
            // treeView6
            // 
            this.treeView6.CheckBoxes = true;
            this.treeView6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView6.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.treeView6.Location = new System.Drawing.Point(0, 0);
            this.treeView6.Name = "treeView6";
            this.treeView6.Size = new System.Drawing.Size(584, 707);
            this.treeView6.TabIndex = 8;
            // 
            // livetbs
            // 
            this.livetbs.Controls.Add(this.treeView7);
            this.livetbs.Location = new System.Drawing.Point(4, 21);
            this.livetbs.Name = "livetbs";
            this.livetbs.Size = new System.Drawing.Size(584, 707);
            this.livetbs.TabIndex = 6;
            this.livetbs.Text = "TBS";
            this.livetbs.UseVisualStyleBackColor = true;
            // 
            // treeView7
            // 
            this.treeView7.CheckBoxes = true;
            this.treeView7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView7.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.treeView7.Location = new System.Drawing.Point(0, 0);
            this.treeView7.Name = "treeView7";
            this.treeView7.Size = new System.Drawing.Size(584, 707);
            this.treeView7.TabIndex = 8;
            // 
            // liveabs
            // 
            this.liveabs.Controls.Add(this.treeView8);
            this.liveabs.Location = new System.Drawing.Point(4, 21);
            this.liveabs.Name = "liveabs";
            this.liveabs.Size = new System.Drawing.Size(584, 707);
            this.liveabs.TabIndex = 7;
            this.liveabs.Text = "朝日";
            this.liveabs.UseVisualStyleBackColor = true;
            // 
            // treeView8
            // 
            this.treeView8.CheckBoxes = true;
            this.treeView8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView8.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.treeView8.Location = new System.Drawing.Point(0, 0);
            this.treeView8.Name = "treeView8";
            this.treeView8.Size = new System.Drawing.Size(584, 707);
            this.treeView8.TabIndex = 8;
            // 
            // liventv
            // 
            this.liventv.Controls.Add(this.treeView9);
            this.liventv.Location = new System.Drawing.Point(4, 21);
            this.liventv.Name = "liventv";
            this.liventv.Size = new System.Drawing.Size(584, 707);
            this.liventv.TabIndex = 8;
            this.liventv.Text = "NTV";
            this.liventv.UseVisualStyleBackColor = true;
            // 
            // treeView9
            // 
            this.treeView9.CheckBoxes = true;
            this.treeView9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView9.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.treeView9.Location = new System.Drawing.Point(0, 0);
            this.treeView9.Name = "treeView9";
            this.treeView9.Size = new System.Drawing.Size(584, 707);
            this.treeView9.TabIndex = 8;
            // 
            // livetx
            // 
            this.livetx.Controls.Add(this.treeView10);
            this.livetx.Location = new System.Drawing.Point(4, 21);
            this.livetx.Name = "livetx";
            this.livetx.Size = new System.Drawing.Size(584, 707);
            this.livetx.TabIndex = 9;
            this.livetx.Text = "テレビ東京";
            this.livetx.UseVisualStyleBackColor = true;
            // 
            // treeView10
            // 
            this.treeView10.CheckBoxes = true;
            this.treeView10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView10.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.treeView10.Location = new System.Drawing.Point(0, 0);
            this.treeView10.Name = "treeView10";
            this.treeView10.Size = new System.Drawing.Size(584, 707);
            this.treeView10.TabIndex = 8;
            // 
            // liveradio
            // 
            this.liveradio.Controls.Add(this.treeView11);
            this.liveradio.Location = new System.Drawing.Point(4, 21);
            this.liveradio.Name = "liveradio";
            this.liveradio.Size = new System.Drawing.Size(584, 707);
            this.liveradio.TabIndex = 10;
            this.liveradio.Text = "ラジオ実況";
            this.liveradio.UseVisualStyleBackColor = true;
            // 
            // treeView11
            // 
            this.treeView11.CheckBoxes = true;
            this.treeView11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView11.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.treeView11.Location = new System.Drawing.Point(0, 0);
            this.treeView11.Name = "treeView11";
            this.treeView11.Size = new System.Drawing.Size(584, 707);
            this.treeView11.TabIndex = 8;
            // 
            // testCheckBox
            // 
            this.testCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.testCheckBox.AutoSize = true;
            this.testCheckBox.Location = new System.Drawing.Point(684, 684);
            this.testCheckBox.Name = "testCheckBox";
            this.testCheckBox.Size = new System.Drawing.Size(48, 15);
            this.testCheckBox.TabIndex = 6;
            this.testCheckBox.Text = "テスト";
            this.testCheckBox.UseVisualStyleBackColor = true;
            // 
            // minutes5
            // 
            this.minutes5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.minutes5.AutoSize = true;
            this.minutes5.Location = new System.Drawing.Point(600, 588);
            this.minutes5.Name = "minutes5";
            this.minutes5.Size = new System.Drawing.Size(29, 15);
            this.minutes5.TabIndex = 9;
            this.minutes5.Text = "5";
            this.minutes5.UseVisualStyleBackColor = true;
            this.minutes5.CheckedChanged += new System.EventHandler(this.minutes_CheckedChanged);
            // 
            // minutes10
            // 
            this.minutes10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.minutes10.AutoSize = true;
            this.minutes10.Location = new System.Drawing.Point(632, 588);
            this.minutes10.Name = "minutes10";
            this.minutes10.Size = new System.Drawing.Size(35, 15);
            this.minutes10.TabIndex = 9;
            this.minutes10.Text = "10";
            this.minutes10.UseVisualStyleBackColor = true;
            this.minutes10.CheckedChanged += new System.EventHandler(this.minutes_CheckedChanged);
            // 
            // minutes15
            // 
            this.minutes15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.minutes15.AutoSize = true;
            this.minutes15.Location = new System.Drawing.Point(664, 588);
            this.minutes15.Name = "minutes15";
            this.minutes15.Size = new System.Drawing.Size(35, 15);
            this.minutes15.TabIndex = 9;
            this.minutes15.Text = "15";
            this.minutes15.UseVisualStyleBackColor = true;
            this.minutes15.CheckedChanged += new System.EventHandler(this.minutes_CheckedChanged);
            // 
            // minutes30
            // 
            this.minutes30.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.minutes30.AutoSize = true;
            this.minutes30.Checked = true;
            this.minutes30.Location = new System.Drawing.Point(696, 588);
            this.minutes30.Name = "minutes30";
            this.minutes30.Size = new System.Drawing.Size(35, 15);
            this.minutes30.TabIndex = 9;
            this.minutes30.TabStop = true;
            this.minutes30.Text = "30";
            this.minutes30.UseVisualStyleBackColor = true;
            this.minutes30.CheckedChanged += new System.EventHandler(this.minutes_CheckedChanged);
            // 
            // minutes60
            // 
            this.minutes60.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.minutes60.AutoSize = true;
            this.minutes60.Location = new System.Drawing.Point(728, 588);
            this.minutes60.Name = "minutes60";
            this.minutes60.Size = new System.Drawing.Size(35, 15);
            this.minutes60.TabIndex = 9;
            this.minutes60.Text = "60";
            this.minutes60.UseVisualStyleBackColor = true;
            this.minutes60.CheckedChanged += new System.EventHandler(this.minutes_CheckedChanged);
            // 
            // minutes120
            // 
            this.minutes120.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.minutes120.AutoSize = true;
            this.minutes120.Location = new System.Drawing.Point(760, 588);
            this.minutes120.Name = "minutes120";
            this.minutes120.Size = new System.Drawing.Size(41, 15);
            this.minutes120.TabIndex = 9;
            this.minutes120.Text = "120";
            this.minutes120.UseVisualStyleBackColor = true;
            this.minutes120.CheckedChanged += new System.EventHandler(this.minutes_CheckedChanged);
            // 
            // next
            // 
            this.next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.next.Location = new System.Drawing.Point(940, 664);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(75, 23);
            this.next.TabIndex = 1;
            this.next.Text = "進む";
            this.next.UseVisualStyleBackColor = true;
            this.next.Click += new System.EventHandler(this.next_Click);
            // 
            // nglist
            // 
            this.nglist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nglist.Location = new System.Drawing.Point(864, 664);
            this.nglist.Name = "nglist";
            this.nglist.Size = new System.Drawing.Size(75, 23);
            this.nglist.TabIndex = 10;
            this.nglist.Text = "NGリストを開く";
            this.nglist.UseVisualStyleBackColor = true;
            this.nglist.Click += new System.EventHandler(this.nglist_Click);
            // 
            // transform
            // 
            this.transform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.transform.Location = new System.Drawing.Point(864, 688);
            this.transform.Name = "transform";
            this.transform.Size = new System.Drawing.Size(75, 23);
            this.transform.TabIndex = 10;
            this.transform.Text = "変換";
            this.transform.UseVisualStyleBackColor = true;
            this.transform.Click += new System.EventHandler(this.transform_Click);
            // 
            // radioJane
            // 
            this.radioJane.AutoSize = true;
            this.radioJane.Checked = true;
            this.radioJane.Location = new System.Drawing.Point(6, 43);
            this.radioJane.Name = "radioJane";
            this.radioJane.Size = new System.Drawing.Size(42, 15);
            this.radioJane.TabIndex = 11;
            this.radioJane.TabStop = true;
            this.radioJane.Text = "jane";
            this.radioJane.UseVisualStyleBackColor = true;
            // 
            // radioDat
            // 
            this.radioDat.AutoSize = true;
            this.radioDat.Location = new System.Drawing.Point(6, 27);
            this.radioDat.Name = "radioDat";
            this.radioDat.Size = new System.Drawing.Size(37, 15);
            this.radioDat.TabIndex = 12;
            this.radioDat.Text = "dat";
            this.radioDat.UseVisualStyleBackColor = true;
            // 
            // radioHtml
            // 
            this.radioHtml.AutoSize = true;
            this.radioHtml.Location = new System.Drawing.Point(6, 11);
            this.radioHtml.Name = "radioHtml";
            this.radioHtml.Size = new System.Drawing.Size(43, 15);
            this.radioHtml.TabIndex = 13;
            this.radioHtml.Text = "html";
            this.radioHtml.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioHtml);
            this.groupBox1.Controls.Add(this.radioJane);
            this.groupBox1.Controls.Add(this.radioDat);
            this.groupBox1.Location = new System.Drawing.Point(757, 661);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(67, 70);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "取得方法";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 11F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 736);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.transform);
            this.Controls.Add(this.nglist);
            this.Controls.Add(this.next);
            this.Controls.Add(this.minutes120);
            this.Controls.Add(this.minutes60);
            this.Controls.Add(this.minutes30);
            this.Controls.Add(this.minutes15);
            this.Controls.Add(this.minutes10);
            this.Controls.Add(this.minutes5);
            this.Controls.Add(this.tab);
            this.Controls.Add(this.tempCheckBox);
            this.Controls.Add(this.testCheckBox);
            this.Controls.Add(this.sameTime);
            this.Controls.Add(this.sort);
            this.Controls.Add(this.openCheckbox);
            this.Controls.Add(this.EndLabel);
            this.Controls.Add(this.StartLabel);
            this.Controls.Add(this.EndBar);
            this.Controls.Add(this.ResList);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.ListButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.ThreadList);
            this.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "Form1";
            this.Text = "ResTools";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.threadListMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EndBar)).EndInit();
            this.tab.ResumeLayout(false);
            this.weekly.ResumeLayout(false);
            this.livebs2.ResumeLayout(false);
            this.livebs.ResumeLayout(false);
            this.livenhk.ResumeLayout(false);
            this.liveetv.ResumeLayout(false);
            this.livecx.ResumeLayout(false);
            this.livetbs.ResumeLayout(false);
            this.liveabs.ResumeLayout(false);
            this.liventv.ResumeLayout(false);
            this.livetx.ResumeLayout(false);
            this.liveradio.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListView ThreadList;
        public System.Windows.Forms.Button SaveButton;
        public System.Windows.Forms.ListView ResList;
        public System.Windows.Forms.ColumnHeader date;
        public System.Windows.Forms.ColumnHeader res;
        public System.Windows.Forms.TrackBar EndBar;
        public System.Windows.Forms.Label StartLabel;
        public System.Windows.Forms.Label EndLabel;
        public System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.ColumnHeader startend;
        private System.Windows.Forms.CheckBox openCheckbox;
        private System.Windows.Forms.CheckBox tempCheckBox;
        private System.Windows.Forms.CheckBox sort;
        private System.Windows.Forms.TreeView treeView1;
        public System.Windows.Forms.Button ListButton;
        private System.Windows.Forms.CheckBox sameTime;
        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage weekly;
        private System.Windows.Forms.TabPage livebs2;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.TabPage livebs;
        private System.Windows.Forms.TabPage livenhk;
        private System.Windows.Forms.TabPage liveetv;
        private System.Windows.Forms.TabPage livecx;
        private System.Windows.Forms.TabPage livetbs;
        private System.Windows.Forms.TabPage liveabs;
        private System.Windows.Forms.TabPage liventv;
        private System.Windows.Forms.TabPage livetx;
        private System.Windows.Forms.TreeView treeView3;
        private System.Windows.Forms.TreeView treeView4;
        private System.Windows.Forms.TreeView treeView5;
        private System.Windows.Forms.TreeView treeView6;
        private System.Windows.Forms.TreeView treeView7;
        private System.Windows.Forms.TreeView treeView8;
        private System.Windows.Forms.TreeView treeView9;
        private System.Windows.Forms.TreeView treeView10;
        private System.Windows.Forms.TabPage liveradio;
        private System.Windows.Forms.TreeView treeView11;
        private System.Windows.Forms.CheckBox testCheckBox;
        private System.Windows.Forms.RadioButton minutes5;
        private System.Windows.Forms.RadioButton minutes10;
        private System.Windows.Forms.RadioButton minutes15;
        private System.Windows.Forms.RadioButton minutes30;
        private System.Windows.Forms.RadioButton minutes60;
        private System.Windows.Forms.RadioButton minutes120;
        private System.Windows.Forms.Button next;
        private System.Windows.Forms.Button nglist;
        private System.Windows.Forms.Button transform;
        private System.Windows.Forms.ContextMenuStrip resListMenu;
        private System.Windows.Forms.ContextMenuStrip threadListMenu;
        private System.Windows.Forms.ToolStripMenuItem open;
        private System.Windows.Forms.RadioButton radioJane;
        private System.Windows.Forms.RadioButton radioDat;
        private System.Windows.Forms.RadioButton radioHtml;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

