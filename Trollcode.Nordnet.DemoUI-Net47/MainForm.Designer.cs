namespace Trollcode.Nordnet.DemoUI_Net47
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nordnetSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.personalSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbEnvironmentCombo = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsLblTxtLoggedIn = new System.Windows.Forms.ToolStripLabel();
            this.tsUserLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabCtrlApi = new System.Windows.Forms.TabControl();
            this.tabPageAccount = new System.Windows.Forms.TabPage();
            this.gbAccountInfo = new System.Windows.Forms.GroupBox();
            this.lblAccountLoanLimit = new System.Windows.Forms.Label();
            this.lblAccountTradeAvail = new System.Windows.Forms.Label();
            this.lblTxtCurrency3 = new System.Windows.Forms.Label();
            this.lblTxtCurrency2 = new System.Windows.Forms.Label();
            this.lblTxtCurrency1 = new System.Windows.Forms.Label();
            this.lblAccountValue = new System.Windows.Forms.Label();
            this.lblTxtLoanLimit = new System.Windows.Forms.Label();
            this.lblTxtAwailableForTrade = new System.Windows.Forms.Label();
            this.lvlTxtAccountValut = new System.Windows.Forms.Label();
            this.tabControlAccount = new System.Windows.Forms.TabControl();
            this.tabPageAccountPositions = new System.Windows.Forms.TabPage();
            this.tabPageAccountLedgers = new System.Windows.Forms.TabPage();
            this.tabPageAccountOrders = new System.Windows.Forms.TabPage();
            this.tabPageAccountTrades = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.cbAccountsList = new System.Windows.Forms.ComboBox();
            this.tabPageIndicators = new System.Windows.Forms.TabPage();
            this.tabPageInstruments = new System.Windows.Forms.TabPage();
            this.tabPageLists = new System.Windows.Forms.TabPage();
            this.tabPageMarkets = new System.Windows.Forms.TabPage();
            this.tabCtrlFeeds = new System.Windows.Forms.TabControl();
            this.tabPagePublicFeed = new System.Windows.Forms.TabPage();
            this.lvPublicFeed = new System.Windows.Forms.ListView();
            this.colHeaderTimestamp1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeaderData1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPagePrivateFeed = new System.Windows.Forms.TabPage();
            this.lvPrivateFeed = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeaderType1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabCtrlApi.SuspendLayout();
            this.tabPageAccount.SuspendLayout();
            this.gbAccountInfo.SuspendLayout();
            this.tabControlAccount.SuspendLayout();
            this.tabCtrlFeeds.SuspendLayout();
            this.tabPagePublicFeed.SuspendLayout();
            this.tabPagePrivateFeed.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1749, 33);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(141, 34);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nordnetSettingsToolStripMenuItem,
            this.personalSettingsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(92, 29);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // nordnetSettingsToolStripMenuItem
            // 
            this.nordnetSettingsToolStripMenuItem.Name = "nordnetSettingsToolStripMenuItem";
            this.nordnetSettingsToolStripMenuItem.Size = new System.Drawing.Size(247, 34);
            this.nordnetSettingsToolStripMenuItem.Text = "Nordnet settings";
            // 
            // personalSettingsToolStripMenuItem
            // 
            this.personalSettingsToolStripMenuItem.Name = "personalSettingsToolStripMenuItem";
            this.personalSettingsToolStripMenuItem.Size = new System.Drawing.Size(247, 34);
            this.personalSettingsToolStripMenuItem.Text = "Personal settings";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 965);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1749, 32);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsStatusLabel
            // 
            this.tsStatusLabel.Name = "tsStatusLabel";
            this.tsStatusLabel.Size = new System.Drawing.Size(179, 25);
            this.tsStatusLabel.Text = "toolStripStatusLabel1";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1749, 4);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 33);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1749, 37);
            this.toolStripContainer1.TabIndex = 3;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbEnvironmentCombo,
            this.toolStripSeparator1,
            this.tsLblTxtLoggedIn,
            this.tsUserLabel,
            this.toolStripSeparator2});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(4, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(407, 33);
            this.toolStrip1.TabIndex = 0;
            // 
            // tbEnvironmentCombo
            // 
            this.tbEnvironmentCombo.Items.AddRange(new object[] {
            "Test",
            "Production"});
            this.tbEnvironmentCombo.Name = "tbEnvironmentCombo";
            this.tbEnvironmentCombo.Size = new System.Drawing.Size(200, 33);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // tsLblTxtLoggedIn
            // 
            this.tsLblTxtLoggedIn.Name = "tsLblTxtLoggedIn";
            this.tsLblTxtLoggedIn.Size = new System.Drawing.Size(118, 25);
            this.tsLblTxtLoggedIn.Text = "Logged in as:";
            // 
            // tsUserLabel
            // 
            this.tsUserLabel.Name = "tsUserLabel";
            this.tsUserLabel.Size = new System.Drawing.Size(71, 25);
            this.tsUserLabel.Text = "<User>";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.92339F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.07661F));
            this.tableLayoutPanel1.Controls.Add(this.tabCtrlApi, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabCtrlFeeds, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 70);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1749, 895);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // tabCtrlApi
            // 
            this.tabCtrlApi.Controls.Add(this.tabPageAccount);
            this.tabCtrlApi.Controls.Add(this.tabPageIndicators);
            this.tabCtrlApi.Controls.Add(this.tabPageInstruments);
            this.tabCtrlApi.Controls.Add(this.tabPageLists);
            this.tabCtrlApi.Controls.Add(this.tabPageMarkets);
            this.tabCtrlApi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlApi.Location = new System.Drawing.Point(3, 3);
            this.tabCtrlApi.Name = "tabCtrlApi";
            this.tabCtrlApi.SelectedIndex = 0;
            this.tabCtrlApi.Size = new System.Drawing.Size(1147, 889);
            this.tabCtrlApi.TabIndex = 1;
            // 
            // tabPageAccount
            // 
            this.tabPageAccount.Controls.Add(this.gbAccountInfo);
            this.tabPageAccount.Controls.Add(this.tabControlAccount);
            this.tabPageAccount.Controls.Add(this.label1);
            this.tabPageAccount.Controls.Add(this.cbAccountsList);
            this.tabPageAccount.Location = new System.Drawing.Point(4, 29);
            this.tabPageAccount.Name = "tabPageAccount";
            this.tabPageAccount.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAccount.Size = new System.Drawing.Size(1139, 856);
            this.tabPageAccount.TabIndex = 0;
            this.tabPageAccount.Text = "Account";
            // 
            // gbAccountInfo
            // 
            this.gbAccountInfo.Controls.Add(this.lblAccountLoanLimit);
            this.gbAccountInfo.Controls.Add(this.lblAccountTradeAvail);
            this.gbAccountInfo.Controls.Add(this.lblTxtCurrency3);
            this.gbAccountInfo.Controls.Add(this.lblTxtCurrency2);
            this.gbAccountInfo.Controls.Add(this.lblTxtCurrency1);
            this.gbAccountInfo.Controls.Add(this.lblAccountValue);
            this.gbAccountInfo.Controls.Add(this.lblTxtLoanLimit);
            this.gbAccountInfo.Controls.Add(this.lblTxtAwailableForTrade);
            this.gbAccountInfo.Controls.Add(this.lvlTxtAccountValut);
            this.gbAccountInfo.Location = new System.Drawing.Point(19, 110);
            this.gbAccountInfo.Name = "gbAccountInfo";
            this.gbAccountInfo.Size = new System.Drawing.Size(364, 720);
            this.gbAccountInfo.TabIndex = 3;
            this.gbAccountInfo.TabStop = false;
            this.gbAccountInfo.Text = "groupBox1";
            // 
            // lblAccountLoanLimit
            // 
            this.lblAccountLoanLimit.AutoSize = true;
            this.lblAccountLoanLimit.Location = new System.Drawing.Point(224, 116);
            this.lblAccountLoanLimit.Name = "lblAccountLoanLimit";
            this.lblAccountLoanLimit.Size = new System.Drawing.Size(109, 20);
            this.lblAccountLoanLimit.TabIndex = 10;
            this.lblAccountLoanLimit.Text = "<placeholder>";
            // 
            // lblAccountTradeAvail
            // 
            this.lblAccountTradeAvail.AutoSize = true;
            this.lblAccountTradeAvail.Location = new System.Drawing.Point(224, 90);
            this.lblAccountTradeAvail.Name = "lblAccountTradeAvail";
            this.lblAccountTradeAvail.Size = new System.Drawing.Size(109, 20);
            this.lblAccountTradeAvail.TabIndex = 9;
            this.lblAccountTradeAvail.Text = "<placeholder>";
            // 
            // lblTxtCurrency3
            // 
            this.lblTxtCurrency3.AutoSize = true;
            this.lblTxtCurrency3.Location = new System.Drawing.Point(178, 116);
            this.lblTxtCurrency3.Name = "lblTxtCurrency3";
            this.lblTxtCurrency3.Size = new System.Drawing.Size(42, 20);
            this.lblTxtCurrency3.TabIndex = 8;
            this.lblTxtCurrency3.Text = "NOK";
            // 
            // lblTxtCurrency2
            // 
            this.lblTxtCurrency2.AutoSize = true;
            this.lblTxtCurrency2.Location = new System.Drawing.Point(178, 90);
            this.lblTxtCurrency2.Name = "lblTxtCurrency2";
            this.lblTxtCurrency2.Size = new System.Drawing.Size(42, 20);
            this.lblTxtCurrency2.TabIndex = 7;
            this.lblTxtCurrency2.Text = "NOK";
            // 
            // lblTxtCurrency1
            // 
            this.lblTxtCurrency1.AutoSize = true;
            this.lblTxtCurrency1.Location = new System.Drawing.Point(178, 63);
            this.lblTxtCurrency1.Name = "lblTxtCurrency1";
            this.lblTxtCurrency1.Size = new System.Drawing.Size(42, 20);
            this.lblTxtCurrency1.TabIndex = 6;
            this.lblTxtCurrency1.Text = "NOK";
            // 
            // lblAccountValue
            // 
            this.lblAccountValue.AutoSize = true;
            this.lblAccountValue.Location = new System.Drawing.Point(224, 63);
            this.lblAccountValue.Name = "lblAccountValue";
            this.lblAccountValue.Size = new System.Drawing.Size(109, 20);
            this.lblAccountValue.TabIndex = 5;
            this.lblAccountValue.Text = "<placeholder>";
            // 
            // lblTxtLoanLimit
            // 
            this.lblTxtLoanLimit.AutoSize = true;
            this.lblTxtLoanLimit.Location = new System.Drawing.Point(8, 116);
            this.lblTxtLoanLimit.Name = "lblTxtLoanLimit";
            this.lblTxtLoanLimit.Size = new System.Drawing.Size(80, 20);
            this.lblTxtLoanLimit.TabIndex = 4;
            this.lblTxtLoanLimit.Text = "Loan limit:";
            // 
            // lblTxtAwailableForTrade
            // 
            this.lblTxtAwailableForTrade.AutoSize = true;
            this.lblTxtAwailableForTrade.Location = new System.Drawing.Point(7, 90);
            this.lblTxtAwailableForTrade.Name = "lblTxtAwailableForTrade";
            this.lblTxtAwailableForTrade.Size = new System.Drawing.Size(156, 20);
            this.lblTxtAwailableForTrade.TabIndex = 3;
            this.lblTxtAwailableForTrade.Text = "Available for Trading:";
            // 
            // lvlTxtAccountValut
            // 
            this.lvlTxtAccountValut.AutoSize = true;
            this.lvlTxtAccountValut.Location = new System.Drawing.Point(7, 63);
            this.lvlTxtAccountValut.Name = "lvlTxtAccountValut";
            this.lvlTxtAccountValut.Size = new System.Drawing.Size(54, 20);
            this.lvlTxtAccountValut.TabIndex = 0;
            this.lvlTxtAccountValut.Text = "Value:";
            // 
            // tabControlAccount
            // 
            this.tabControlAccount.Controls.Add(this.tabPageAccountPositions);
            this.tabControlAccount.Controls.Add(this.tabPageAccountLedgers);
            this.tabControlAccount.Controls.Add(this.tabPageAccountOrders);
            this.tabControlAccount.Controls.Add(this.tabPageAccountTrades);
            this.tabControlAccount.Location = new System.Drawing.Point(389, 77);
            this.tabControlAccount.Name = "tabControlAccount";
            this.tabControlAccount.SelectedIndex = 0;
            this.tabControlAccount.Size = new System.Drawing.Size(725, 753);
            this.tabControlAccount.TabIndex = 2;
            // 
            // tabPageAccountPositions
            // 
            this.tabPageAccountPositions.Location = new System.Drawing.Point(4, 29);
            this.tabPageAccountPositions.Name = "tabPageAccountPositions";
            this.tabPageAccountPositions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAccountPositions.Size = new System.Drawing.Size(717, 720);
            this.tabPageAccountPositions.TabIndex = 0;
            this.tabPageAccountPositions.Text = "Positions";
            this.tabPageAccountPositions.UseVisualStyleBackColor = true;
            // 
            // tabPageAccountLedgers
            // 
            this.tabPageAccountLedgers.Location = new System.Drawing.Point(4, 29);
            this.tabPageAccountLedgers.Name = "tabPageAccountLedgers";
            this.tabPageAccountLedgers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAccountLedgers.Size = new System.Drawing.Size(717, 720);
            this.tabPageAccountLedgers.TabIndex = 1;
            this.tabPageAccountLedgers.Text = "Ledgers";
            this.tabPageAccountLedgers.UseVisualStyleBackColor = true;
            // 
            // tabPageAccountOrders
            // 
            this.tabPageAccountOrders.Location = new System.Drawing.Point(4, 29);
            this.tabPageAccountOrders.Name = "tabPageAccountOrders";
            this.tabPageAccountOrders.Size = new System.Drawing.Size(717, 720);
            this.tabPageAccountOrders.TabIndex = 2;
            this.tabPageAccountOrders.Text = "Orders";
            this.tabPageAccountOrders.UseVisualStyleBackColor = true;
            // 
            // tabPageAccountTrades
            // 
            this.tabPageAccountTrades.Location = new System.Drawing.Point(4, 29);
            this.tabPageAccountTrades.Name = "tabPageAccountTrades";
            this.tabPageAccountTrades.Size = new System.Drawing.Size(717, 720);
            this.tabPageAccountTrades.TabIndex = 3;
            this.tabPageAccountTrades.Text = "Trades";
            this.tabPageAccountTrades.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Accounts:";
            // 
            // cbAccountsList
            // 
            this.cbAccountsList.FormattingEnabled = true;
            this.cbAccountsList.Location = new System.Drawing.Point(15, 38);
            this.cbAccountsList.Name = "cbAccountsList";
            this.cbAccountsList.Size = new System.Drawing.Size(557, 28);
            this.cbAccountsList.TabIndex = 0;
            this.cbAccountsList.SelectedIndexChanged += new System.EventHandler(this.cbAccountsList_SelectedIndexChanged);
            // 
            // tabPageIndicators
            // 
            this.tabPageIndicators.Location = new System.Drawing.Point(4, 29);
            this.tabPageIndicators.Name = "tabPageIndicators";
            this.tabPageIndicators.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageIndicators.Size = new System.Drawing.Size(1139, 853);
            this.tabPageIndicators.TabIndex = 1;
            this.tabPageIndicators.Text = "Indicators";
            // 
            // tabPageInstruments
            // 
            this.tabPageInstruments.Location = new System.Drawing.Point(4, 29);
            this.tabPageInstruments.Name = "tabPageInstruments";
            this.tabPageInstruments.Size = new System.Drawing.Size(1139, 853);
            this.tabPageInstruments.TabIndex = 2;
            this.tabPageInstruments.Text = "Instruments";
            // 
            // tabPageLists
            // 
            this.tabPageLists.Location = new System.Drawing.Point(4, 29);
            this.tabPageLists.Name = "tabPageLists";
            this.tabPageLists.Size = new System.Drawing.Size(1139, 853);
            this.tabPageLists.TabIndex = 3;
            this.tabPageLists.Text = "Lists";
            // 
            // tabPageMarkets
            // 
            this.tabPageMarkets.Location = new System.Drawing.Point(4, 29);
            this.tabPageMarkets.Name = "tabPageMarkets";
            this.tabPageMarkets.Size = new System.Drawing.Size(1139, 853);
            this.tabPageMarkets.TabIndex = 4;
            this.tabPageMarkets.Text = "Markets";
            // 
            // tabCtrlFeeds
            // 
            this.tabCtrlFeeds.Controls.Add(this.tabPagePublicFeed);
            this.tabCtrlFeeds.Controls.Add(this.tabPagePrivateFeed);
            this.tabCtrlFeeds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlFeeds.Location = new System.Drawing.Point(1156, 3);
            this.tabCtrlFeeds.Name = "tabCtrlFeeds";
            this.tabCtrlFeeds.SelectedIndex = 0;
            this.tabCtrlFeeds.Size = new System.Drawing.Size(590, 889);
            this.tabCtrlFeeds.TabIndex = 2;
            // 
            // tabPagePublicFeed
            // 
            this.tabPagePublicFeed.Controls.Add(this.lvPublicFeed);
            this.tabPagePublicFeed.Location = new System.Drawing.Point(4, 29);
            this.tabPagePublicFeed.Name = "tabPagePublicFeed";
            this.tabPagePublicFeed.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePublicFeed.Size = new System.Drawing.Size(582, 856);
            this.tabPagePublicFeed.TabIndex = 0;
            this.tabPagePublicFeed.Text = "Public Feed";
            this.tabPagePublicFeed.UseVisualStyleBackColor = true;
            // 
            // lvPublicFeed
            // 
            this.lvPublicFeed.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHeaderTimestamp1,
            this.colHeaderType1,
            this.colHeaderData1});
            this.lvPublicFeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPublicFeed.HideSelection = false;
            this.lvPublicFeed.Location = new System.Drawing.Point(3, 3);
            this.lvPublicFeed.MultiSelect = false;
            this.lvPublicFeed.Name = "lvPublicFeed";
            this.lvPublicFeed.Size = new System.Drawing.Size(576, 850);
            this.lvPublicFeed.TabIndex = 0;
            this.lvPublicFeed.UseCompatibleStateImageBehavior = false;
            this.lvPublicFeed.View = System.Windows.Forms.View.Details;
            // 
            // colHeaderTimestamp1
            // 
            this.colHeaderTimestamp1.Text = "Timestamp";
            this.colHeaderTimestamp1.Width = 135;
            // 
            // colHeaderData1
            // 
            this.colHeaderData1.Text = "Data";
            this.colHeaderData1.Width = 324;
            // 
            // tabPagePrivateFeed
            // 
            this.tabPagePrivateFeed.Controls.Add(this.lvPrivateFeed);
            this.tabPagePrivateFeed.Location = new System.Drawing.Point(4, 29);
            this.tabPagePrivateFeed.Name = "tabPagePrivateFeed";
            this.tabPagePrivateFeed.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePrivateFeed.Size = new System.Drawing.Size(582, 856);
            this.tabPagePrivateFeed.TabIndex = 1;
            this.tabPagePrivateFeed.Text = "Private Feed";
            this.tabPagePrivateFeed.UseVisualStyleBackColor = true;
            // 
            // lvPrivateFeed
            // 
            this.lvPrivateFeed.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvPrivateFeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPrivateFeed.HideSelection = false;
            this.lvPrivateFeed.Location = new System.Drawing.Point(3, 3);
            this.lvPrivateFeed.Name = "lvPrivateFeed";
            this.lvPrivateFeed.Size = new System.Drawing.Size(576, 850);
            this.lvPrivateFeed.TabIndex = 0;
            this.lvPrivateFeed.UseCompatibleStateImageBehavior = false;
            this.lvPrivateFeed.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Timestamp";
            this.columnHeader1.Width = 135;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Data";
            this.columnHeader2.Width = 406;
            // 
            // colHeaderType1
            // 
            this.colHeaderType1.Text = "Type";
            this.colHeaderType1.Width = 93;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1749, 997);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "NordnetAPI Demo UI - Framework 4.7";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabCtrlApi.ResumeLayout(false);
            this.tabPageAccount.ResumeLayout(false);
            this.tabPageAccount.PerformLayout();
            this.gbAccountInfo.ResumeLayout(false);
            this.gbAccountInfo.PerformLayout();
            this.tabControlAccount.ResumeLayout(false);
            this.tabCtrlFeeds.ResumeLayout(false);
            this.tabPagePublicFeed.ResumeLayout(false);
            this.tabPagePrivateFeed.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox tbEnvironmentCombo;
        private System.Windows.Forms.ToolStripLabel tsLblTxtLoggedIn;
        private System.Windows.Forms.ToolStripLabel tsUserLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nordnetSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem personalSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabCtrlApi;
        private System.Windows.Forms.TabPage tabPageAccount;
        private System.Windows.Forms.TabPage tabPageIndicators;
        private System.Windows.Forms.TabPage tabPageInstruments;
        private System.Windows.Forms.TabPage tabPageLists;
        private System.Windows.Forms.TabPage tabPageMarkets;
        private System.Windows.Forms.TabControl tabCtrlFeeds;
        private System.Windows.Forms.TabPage tabPagePublicFeed;
        private System.Windows.Forms.TabPage tabPagePrivateFeed;
        private System.Windows.Forms.ListView lvPublicFeed;
        private System.Windows.Forms.ColumnHeader colHeaderTimestamp1;
        private System.Windows.Forms.ColumnHeader colHeaderData1;
        private System.Windows.Forms.ListView lvPrivateFeed;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbAccountsList;
        private System.Windows.Forms.TabControl tabControlAccount;
        private System.Windows.Forms.TabPage tabPageAccountPositions;
        private System.Windows.Forms.TabPage tabPageAccountLedgers;
        private System.Windows.Forms.GroupBox gbAccountInfo;
        private System.Windows.Forms.TabPage tabPageAccountOrders;
        private System.Windows.Forms.TabPage tabPageAccountTrades;
        private System.Windows.Forms.Label lvlTxtAccountValut;
        private System.Windows.Forms.Label lblTxtAwailableForTrade;
        private System.Windows.Forms.Label lblAccountValue;
        private System.Windows.Forms.Label lblTxtLoanLimit;
        private System.Windows.Forms.Label lblTxtCurrency1;
        private System.Windows.Forms.Label lblAccountLoanLimit;
        private System.Windows.Forms.Label lblAccountTradeAvail;
        private System.Windows.Forms.Label lblTxtCurrency3;
        private System.Windows.Forms.Label lblTxtCurrency2;
        private System.Windows.Forms.ColumnHeader colHeaderType1;
    }
}

