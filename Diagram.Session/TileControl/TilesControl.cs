using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using static TilesControl.TileControl;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using BaseClasses;
using System.Xml.Serialization;

namespace TilesControl
{
    public partial class TilesControl : UserControl
    {
        public enum GradientTypes
        {
            None,
            Horizontal,
            Vertical
        }

        public TilesControl()
        {
            InitializeComponent();

            label1.Visible = false;

            viewPanel.Visible = false;

            CellAsSquare = true;
            CellSize = new Size(64, 64);
            Padding = new Padding(0);
            PaddingTile = new Padding(5);
            MarginTile = new Padding(5);
            UseUIEffectsSpace = true;
            UseAnimation = true;
            AutoSizeCell = false;
            AutoSizeColumnCount = 4;
            FixedColumnCount = false;
            MoveAtSameTime = false;
            MoveEvenly = true;
            MoveAnimation = true;
            UseMove = true;
            UseRow = true;
            UseContextMenu = false;
            EnableScrollTileText = false;
            ViewPanelBackColor = Color.YellowGreen;
            ViewPanelBackColor2 = Color.Violet;
            ViewPanelForeColor = Color.Black;
            GradientType = GradientTypes.Vertical;
            SetSpecialTiles = false;

            SelectTileControl = null;
            SelectTilesControl = null;

            RowTemplate = new List<Size>();
            RowTemplate.Add(new Size(1, 1));
            RowTemplate.Add(new Size(2, 1));
            RowTemplate.Add(new Size(1, 1));
            RowTemplate.Add(new Size(2, 2));
            RowTemplate.Add(new Size(1, 1));
            RowTemplate.Add(new Size(1, 1));
            RowTemplate.Add(new Size(1, 1));
            RowTemplate.Add(new Size(1, 1));
            RowTemplate.Add(new Size(1, 1));
            RowTemplate.Add(new Size(1, 1));
            RowTemplate.Add(new Size(2, 1));
            RowTemplate.Add(new Size(1, 1));
            RowTemplate.Add(new Size(1, 1));
            RowTemplate.Add(new Size(1, 1));
            RowTemplate.Add(new Size(1, 1));

            this.MouseWheel += TilesControl_MouseWheel;
            this.Click += TilesControl_Click;
            this.ParentChanged += TilesControl_ParentChanged;
            this.VisibleChanged += TilesControl_VisibleChanged;

            this.Load += TilesControl_Load;

            viewPanel.SizeChanged += ViewPanel_SizeChanged;         

            mySetLeftDelegate = new SetLeftDelegate(SetLeft);
            mySetVisibleDelegate = new SetVisibleDelegate(SetVisible);
        }

        TileControl SelectTileControl { get; set; }
        TilesControl SelectTilesControl { get; set; }
        public BaseClasses.ActionProvider Information = Session.Session.Information;

        private void ViewPanel_SizeChanged(object sender, EventArgs e)
        {
            RefreshBackgroundImageGradient();
        }

        public void RefreshBackgroundImageGradient()
        {
            SetBackgroundImage(toolboxPanel);
            SetBackgroundImage(closeButton);
            SetBackgroundImage(saveButton);
            SetBackgroundImage(cancelButton);
            SetBackgroundImage(editButton);
            SetBackgroundImage(viewTextLabel);
            //SetBackgroundImage(imagePictureBox);
            //SetBackgroundImage(SelectTile);
        }

        public void SetBackgroundImage(Control c)
        {
            if (c.BackgroundImage != null)
            {
                c.BackgroundImage.Dispose();
            }
            if (GradientType == GradientTypes.None)
            {
                return;
            }
            Rectangle rect = c.ClientRectangle;
            if (!c.Equals(toolboxPanel) && c.Parent.Equals(toolboxPanel))
            {
                rect = toolboxPanel.ClientRectangle;
                rect.Location = new Point(-c.Left, -c.Top);
            }
            Image img = new Bitmap(c.Size.Width, c.Size.Height);
            using (Graphics g = Graphics.FromImage(img))
            {
                if (GradientType == GradientTypes.Horizontal)
                {
                    g.FillRectangle(new System.Drawing.Drawing2D.LinearGradientBrush(new Point(-c.Left + toolboxPanel.Left, 0), new Point(-c.Left + toolboxPanel.Width, 0), Color.Transparent, ViewPanelBackColor2), rect);
                }
                else if (GradientType == GradientTypes.Vertical)
                {
                    g.FillRectangle(new System.Drawing.Drawing2D.LinearGradientBrush(new Point(0, -c.Top + toolboxPanel.Top), new Point(0, -c.Top + toolboxPanel.Height), ViewPanelBackColor2, Color.Transparent), rect);
                }
            }
            c.BackgroundImage = img;
        }

        private void TilesControl_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                viewPanel.Visible = false;
            }
        }

        private void TilesControl_ParentChanged(object sender, EventArgs e)
        {
            if (this.Parent != null && this.Parent.Parent != null)
            {
                if (typeof(ScrollAblePanel.ScrollAblePanelControl) == this.Parent.Parent.GetType())
                {
                    this.ScrollAblePanelControl = (ScrollAblePanel.ScrollAblePanelControl)this.Parent.Parent;
                }
            }
        }

        public string webBrowser1Version = null;
        private void TilesControl_Load(object sender, EventArgs e)
        {
            Information = Session.Session.Information;

            string appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe";

            try
            {
                Microsoft.Win32.RegistryKey Regkey;
                if (Environment.Is64BitOperatingSystem)
                {
                    // For 64 bit machine
                    Regkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Internet Explorer\\MAIN\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
                }
                else
                {
                    //For 32 bit machine
                    Regkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
                }

                string FindAppkey = Convert.ToString(Regkey.GetValue(appName));
                //сразу присвоим значение IE11(наверно нужно сделать проверку на наличие IE11)
                if (String.IsNullOrEmpty(FindAppkey))
                {
                    //CInt(&H2AF8)
                    Regkey.SetValue(appName, Convert.ToInt32(0x2AF8), Microsoft.Win32.RegistryValueKind.DWord);
                    webBrowser1Version = null;
                }

                if (FindAppkey != "11000")
                {
                    Regkey.SetValue(appName, Convert.ToInt32(0x2AF8), Microsoft.Win32.RegistryValueKind.DWord);
                    webBrowser1Version = null;
                }
            }
            catch (Exception ex)
            {
                Microsoft.Win32.RegistryKey RegkeyRead;
                if (Environment.Is64BitOperatingSystem)
                {
                    // For 64 bit machine
                    RegkeyRead = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Internet Explorer\\MAIN\\FeatureControl\\FEATURE_BROWSER_EMULATION", false);
                }
                else
                {
                    //For 32 bit machine
                    RegkeyRead = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", false);
                }

                string FindAppkey = Convert.ToString(RegkeyRead.GetValue(appName));
                //сразу присвоим значение IE11(наверно нужно сделать проверку на наличие IE11)
                if (String.IsNullOrEmpty(FindAppkey) || FindAppkey != "11000")
                {
                    webBrowser1Version = "7";
                }
            }

            if (ScrollAblePanelControl != null)
            {
                viewPanel.Parent = ScrollAblePanelControl;
            }

            this.SelectTilesControl = new TilesControl();
            this.SelectTilesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Controls.Add(this.SelectTilesControl);
            this.SelectTilesControl.BringToFront();
            this.SelectTilesControl.SizeChanged += SelectTilesControl_SizeChanged;


            this.SelectTileControl = new TileControl();

            uiEffects = new UIEffectsSpace.Animator();

            if (SetSpecialTiles)
            {
                AddAddTileControl();
                AddPublishTileControl();
            }

            RefreshTiles();
        }

        public bool SetSpecialTiles { get; set; }

        public void AddAddTileControl()
        {
            Tile tile = new Tile();
            tile.ImageCut = false;
            tile.IsSpecial = true;
            tile.SpecilaType = SpecilaType.Add;
            tile.Font = this.Font;
            tile.BackColor = this.DefaultTileBackColor;
            tile.TextForeColor = this.DefaultTileTextForeColor;
            tile.CellsCount = new Size(1, 1);
            tile.SetImages(Properties.Resources.Add, System.Drawing.Imaging.ImageFormat.Png);

            AddTileControl = AddTile(tile);
            AddTileControl.SpecialStandartAction = true;
        }

        public void AddPublishTileControl()
        {
            Tile tile = new Tile();
            tile.ImageCut = false;
            tile.IsSpecial = true;
            tile.SpecilaType = SpecilaType.Publish;
            tile.Font = this.Font;
            tile.BackColor = this.DefaultTileBackColor;
            tile.TextForeColor = this.DefaultTileTextForeColor;
            tile.CellsCount = new Size(1, 1);
            tile.Text = "Publish";

            PublishTileControl = AddTile(tile);
            PublishTileControl.SpecialStandartAction = true;
        }
        public event EventHandler PublishTileControlClick;
        private void PublishTileControl_Click(object sender, EventArgs e)
        {
            if (PublishTileControl.SpecialStandartAction)
            {

            }
            PublishTileControlClick?.Invoke(PublishTileControl, new System.EventArgs());
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            cancelButtonClick();

            if (UseTask)
            {
                Action<object> action = (object obj) =>
                {
                    RunAimationViewPanel(viewPanel, uiEffects, true);
                };

                Task task = new Task(action, new object());
                task.Start();
                task.ContinueWith(t =>
                {
                   viewPanel.Visible = false;
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                RunAimationViewPanel(viewPanel, uiEffects, true);
            }
        }

        public void RefreshWebBrowser1()
        {
            viewPanel.Visible = false;
        }

        public TileControl TileControlView = null;
        private bool rightViewPanel;
        private void editButton_Click(object sender, EventArgs e)
        {
            if (TileControlView != null)
            {
                editTextBox.Text = TileControlView.Tile.Text;
                editTextBox.Visible = true;
                SelectTilesControl.Visible = false;
                saveButton.Visible = true;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            TileControlView.Tile.Text = editTextBox.Text;
            TileControlView.RefreshTile();
            SelectTileControlLoadData(TileControlView);
            editTextBox.Visible = false;
            SelectTilesControl.Visible = true;
            saveButton.Visible = false;
            cancelButton.Visible = false;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            cancelButtonClick();
        }
        private void cancelButtonClick()
        {
            editTextBox.Visible = false;
            SelectTilesControl.Visible = true;
            saveButton.Visible = false;
            cancelButton.Visible = false;
        }
        public bool UseTask = true;
        public void SelectTileControlLoadData(TileControl tc)
        {
            cancelButtonClick();

            TileControlView = tc;
            editTextBox.Text = tc.Tile.Text;

            if (rightViewPanel)
            {
                viewPanel.Left = this.Width - viewPanel.Width;
            }
            else
            {
                viewPanel.Left = 0;
            }
            viewPanel.Top = 0;
            viewPanel.Height = this.Parent.Parent.Height;
            viewPanel.BringToFront();

            if (SelectTileControl != null)
            {
                SelectTileControl.DeleteTile(false);
            }

            Tile tile = new Tile();
            tile.CellsTypeValue = Tile.CellsType.minSquare;
            tile.Font = TileControlView.Font;
            tile.Text = TileControlView.Tile.Text;
            tile.TextForeColor = ViewPanelForeColor;
            tile.BackColor = ViewPanelBackColor;
            tile.BackColor2 = ViewPanelBackColor;
            tile.BackColorMouse = ViewPanelBackColor;

            tile.IsSpecial = true;
            tile.SpecilaType = SpecilaType.View;

            SelectTilesControl.ScrollAblePanelControl = this.ScrollAblePanelControl;
            SelectTilesControl.EnableScrollTileText = true;
            SelectTilesControl.BackColor = ViewPanelBackColor;
            SelectTilesControl.MarginTile = new Padding(0);
            SelectTilesControl.PaddingTile = new Padding(5);
            SelectTilesControl.CellSize = SelectTilesControl.Size;
            SelectTilesControl.UseRow = false;
            SelectTileControl = SelectTilesControl.AddTile(tile);
            SelectTilesControl.RefreshTiles();
 
            WebClient webClient = new WebClient();
            tile.ImageOriginal = GetTileImage(WebSite, tile.ImageFileName, "original", webClient);
            webClient.Dispose();


            if (imagePictureBox.BackgroundImage != null)
            {
                Image img = imagePictureBox.BackgroundImage;
                imagePictureBox.BackgroundImage = null;
                img.Dispose();
            }
            imagePictureBox.BackgroundImageLayout = ImageLayout.Zoom;
            if (tc.Tile.ImageOriginal == null)
            {
                imagePictureBox.BackgroundImage = null;
            }
            else
            {
                imagePictureBox.BackgroundImage = (Image)tc.Tile.ImageOriginal.Clone();
            }

            imagePictureBox.Visible = (tc.Tile.Image != null);            

            if (!viewPanel.Visible)
            {
                rightViewPanel = (tc.Location.X + tc.Width / 2 < this.Width / 2);
                if (rightViewPanel)
                {
                    viewPanel.Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
                else
                {
                    viewPanel.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
                }
                if (UseTask)
                {
                    Action<object> action = (object obj) =>
                    {
                        RunAimationViewPanel(viewPanel, uiEffects);
                    };

                    Task task = new Task(action, new object());
                    task.Start();
                    task.ContinueWith(t =>
                    {
                        //SelectTilesControl.Visible = true;
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }
                else
                {
                    RunAimationViewPanel(viewPanel, uiEffects);
                }
            }
        }
        private void SelectTilesControl_SizeChanged(object sender, EventArgs e)
        {
            SelectTilesControl.CellSize = SelectTilesControl.Size;
            SelectTilesControl.RefreshTiles();
        }

        public delegate void SetLeftDelegate(int value);
        public SetLeftDelegate mySetLeftDelegate;
        private void SetLeft(int value)
        {
            viewPanel.Left = value;
        }
        public delegate void SetVisibleDelegate(bool value);
        public SetVisibleDelegate mySetVisibleDelegate;
        private void SetVisible(bool value)
        {
            viewPanel.Visible = value;
        }

        private void RunAimationViewPanel(Panel viewPanel, UIEffectsSpace.Animator uiEffects, bool isClose = false)
        {
            if (!(doAnimations == null || doAnimations.Count == 0))
            {
                return;
            }
            RefreshTilesNow = true;

            bool right = rightViewPanel;
            if (UseUIEffectsSpace)
            {
                UIEffectsSpace.Animation anim = UIEffectsSpace.Animation.HorizSlide;
                if (right)
                {
                    if (isClose)
                    {
                        anim.SlideCoeff = new PointF(-1, 0);                      
                    }
                    else
                    {
                        anim.SlideCoeff = new PointF(-1, 0);
                        this.Invoke(mySetLeftDelegate, this.Width - viewPanel.Width);
                    }
                }
                else
                {
                    if (isClose)
                    {
                        anim.SlideCoeff = new PointF(1, 0);
                    }
                    else
                    {
                        anim.SlideCoeff = new PointF(1, 0);
                        this.Invoke(mySetLeftDelegate, 0);
                    }
                }
                if (isClose)
                {
                    uiEffects.HideSync(viewPanel, true, anim);
                }
                else
                {
                    uiEffects.ShowSync(viewPanel, true, anim);
                }
            }
            else
            {
                int animationDelay = 300;
                int n = viewPanel.Width / 100;
                int animationStep = animationDelay / 100;
                if (isClose)
                {
                    if (right)
                    {
                        this.Invoke(mySetLeftDelegate, this.Width);
                        this.Invoke(mySetVisibleDelegate, true);
                        for (int i = viewPanel.Width; i >= 1; i = i - n)
                        {
                            DateTime dateStart = DateTime.Now;
                            this.Invoke(mySetLeftDelegate, this.Width - i);
                            if (!UseTask)
                            {
                                Application.DoEvents();
                            }
                            DateTime dateFinish = DateTime.Now;
                            long timeResult = dateFinish.Ticks - dateStart.Ticks;
                            TimeSpan ms = new TimeSpan((int)(animationStep * 10000 - timeResult));
                            if (ms.Ticks > 0)
                            {
                                System.Threading.Thread.Sleep(ms);
                            }
                        }
                        this.Invoke(mySetLeftDelegate, this.Width);
                        this.Invoke(mySetVisibleDelegate, false);
                    }
                    else
                    {
                        this.Invoke(mySetLeftDelegate, -viewPanel.Width);
                        this.Invoke(mySetVisibleDelegate, true);
                        for (int i = viewPanel.Width; i >= 1; i = i - n)
                        {
                            DateTime dateStart = DateTime.Now;
                            this.Invoke(mySetLeftDelegate, i - viewPanel.Width);
                            if (!UseTask)
                            {
                                Application.DoEvents();
                            }
                            DateTime dateFinish = DateTime.Now;
                            long timeResult = dateFinish.Ticks - dateStart.Ticks;
                            TimeSpan ms = new TimeSpan((int)(animationStep * 10000 - timeResult));
                            if (ms.Ticks > 0)
                            {
                                System.Threading.Thread.Sleep(ms);
                            }
                        }
                        this.Invoke(mySetLeftDelegate, -viewPanel.Width);
                        this.Invoke(mySetVisibleDelegate, false);
                    }
                }
                else
                {
                    if (right)
                    {
                        this.Invoke(mySetLeftDelegate, this.Width);
                        this.Invoke(mySetVisibleDelegate, true);
                        for (int i = 1; i <= viewPanel.Width; i = i + n)
                        {
                            DateTime dateStart = DateTime.Now;
                            this.Invoke(mySetLeftDelegate, this.Width - i);
                            if (!UseTask)
                            {
                                Application.DoEvents();
                            }
                            DateTime dateFinish = DateTime.Now;
                            long timeResult = dateFinish.Ticks - dateStart.Ticks;
                            TimeSpan ms = new TimeSpan((int)(animationStep * 10000 - timeResult));
                            if (ms.Ticks > 0)
                            {
                                System.Threading.Thread.Sleep(ms);
                            }
                        }
                        this.Invoke(mySetLeftDelegate, this.Width - viewPanel.Width);
                    }
                    else
                    {
                        this.Invoke(mySetLeftDelegate, -viewPanel.Width);
                        this.Invoke(mySetVisibleDelegate, true);
                        for (int i = 1; i <= viewPanel.Width; i = i + n)
                        {
                            DateTime dateStart = DateTime.Now;
                            this.Invoke(mySetLeftDelegate, i - viewPanel.Width);
                            if (!UseTask)
                            {
                                Application.DoEvents();
                            }
                            DateTime dateFinish = DateTime.Now;
                            long timeResult = dateFinish.Ticks - dateStart.Ticks;
                            TimeSpan ms = new TimeSpan((int)(animationStep * 10000 - timeResult));
                            if (ms.Ticks > 0)
                            {
                                System.Threading.Thread.Sleep(ms);
                            }
                        }
                        this.Invoke(mySetLeftDelegate, 0);
                    }
                }
            }

            RefreshTilesNow = false;
        }

        private void TilesControl_Click(object sender, EventArgs e)
        {
            this.Focus();
        }

        public ScrollAblePanel.ScrollAblePanelControl ScrollAblePanelControl { get; set; }

        [DefaultValue(false)]
        public bool EnableScrollTileText { get; set; }

        [DefaultValue(typeof(Color), "YellowGreen")]
        public Color ViewPanelBackColor
        {
            get => viewPanelBackColor;
            set
            {
                viewPanelBackColor = value;
                imagePictureBox.BackColor = viewPanelBackColor;
                toolboxPanel.BackColor = viewPanelBackColor;
                editButton.BackColor = viewPanelBackColor;
                closeButton.BackColor = viewPanelBackColor;
                viewTextLabel.BackColor = viewPanelBackColor;
                editTextBox.BackColor = viewPanelBackColor;
                viewPanel.BackColor = viewPanelBackColor;
                saveButton.BackColor = viewPanelBackColor;
                cancelButton.BackColor = viewPanelBackColor;
            }
        }
        [DefaultValue(typeof(Color), "White")]
        public Color ViewPanelBackColor2
        {
            get => viewPanelBackColor2;
            set
            {
                viewPanelBackColor2 = value;
                RefreshBackgroundImageGradient();
            }
        }

        [DefaultValue(typeof(GradientTypes), "Vertical")]
        public GradientTypes GradientType { get; set; }

        private Color viewPanelForeColor;
        [DefaultValue(typeof(Color), "Black")]
        public Color ViewPanelForeColor
        {
            get => viewPanelForeColor;
            set
            {
                viewPanelForeColor = value;
                viewTextLabel.ForeColor = viewPanelForeColor;
                saveButton.ForeColor = viewPanelForeColor;
                cancelButton.ForeColor = viewPanelForeColor;
            }
        }
        [DefaultValue(false)]
        public bool NowMouseWheelStop = false;
        private DateTime nowMouseWheelSetTime;
        [DefaultValue(false)]
        public bool NowMouseWheel
        {
            get => nowMouseWheel;
            set
            {
                if (nowMouseWheel != value)
                {
                    nowMouseWheel = value;
                    nowMouseWheelSetTime = DateTime.Now;
                }
                nowMouseWheelSetTime = DateTime.Now;
            }
        }
        public bool GetNowMouseWheelTimeout()
        {
            if (NowMouseWheelStop)
            {
                return false;
            }
            if (NowMouseWheel)
            {
                //return true;

                DateTime d = DateTime.Now;
                TimeSpan timeResult = d - nowMouseWheelSetTime;
                if (timeResult.TotalMilliseconds >= 1000)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;

                //DateTime d = DateTime.Now;
                //TimeSpan timeResult = d - nowMouseWheelSetTime;
                //if (timeResult.TotalMilliseconds >= 1000)
                //{
                //    return false;
                //}
                //else
                //{
                //    return true;
                //}
            }
        }
        private bool nowMouseWheelBreak = false;
        private void TilesControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Session.Session.ControlPressed)
            {
                bool oldValue = MoveAnimation;
                MoveAnimation = false;
                if (e.Delta > 0)
                {
                    CellSize = new Size(Math.Min(640, CellSize.Width + 10), Math.Min(640, CellSize.Height + 10));
                }
                else
                {
                    CellSize = new Size(Math.Max(10, CellSize.Width - 10), Math.Max(10, CellSize.Height - 10));
                }

                nowMouseWheelBreak = NowMouseWheel;
                NowMouseWheel = true;
                RefreshTiles(true, true);
                NowMouseWheel = false;
                MoveAnimation = oldValue;
                Refresh();
            }
            else
            {
                NowMouseWheel = true;
            }
        }

        private UIEffectsSpace.Animator uiEffects;
        [DefaultValue(true)]
        public bool UseAnimation { get; set; }
        [DefaultValue(true)]
        public bool UseUIEffectsSpace { get; set; }
        [DefaultValue(true)]
        public bool UseMove { get; set; }
        [DefaultValue(null)]
        public TileControl AddTileControl { get; set; }
        [DefaultValue(null)]
        public TileControl PublishTileControl { get; set; }
        [DefaultValue(true)]
        public bool MoveAnimation { get; set; }
        [DefaultValue(false)]
        public bool MoveAtSameTime { get; set; }
        [DefaultValue(true)]
        public bool MoveEvenly { get; set; }
        [DefaultValue(true)]
        public bool CellAsSquare { get; set; }
        [DefaultValue(typeof(Size), "64,64")]
        public Size CellSize { get; set; }
        [DefaultValue(typeof(Padding), "5")]
        public Padding PaddingTile { get; set; }
        [DefaultValue(typeof(Padding), "5")]
        public Padding MarginTile { get; set; }
        [DefaultValue(false)]
        public bool AutoSizeCell
        {
            get => autoSizeCell;
            set
            {
                if (autoSizeCell != value)
                {
                    autoSizeCell = value;
                    //RefreshTiles(); 
                }
            }
        }
        [DefaultValue(4)]
        public int AutoSizeColumnCount { get; set; }
        [DefaultValue(false)]
        public bool FixedColumnCount { get; set; }
        [DefaultValue(true)]
        public bool UseRow { get; set; }
        public List<Size> RowTemplate { get; set; }
        [DefaultValue(false)]
        public bool UseContextMenu
        {
            get => useContextMenu;
            set
            {
                useContextMenu = value;
                if (useContextMenu)
                {
                    this.ContextMenuStrip = contextMenuStrip1;
                }
                else
                {
                    this.ContextMenuStrip = null;
                }
                if (Tiles != null)
                {
                    foreach (TileControl tc in Tiles)
                    {
                        tc.UseContextMenu = useContextMenu;
                    }
                }
            }
        }
        public Size GetAutoSizeCellSize()
        {
            int w = (int)((float)this.Width / AutoSizeColumnCount - this.MarginTile.Left - this.MarginTile.Right);
            return new Size(w, w);
        }
        public Size GetCellSize()
        {
            if (AutoSizeCell)
            {
                return GetAutoSizeCellSize();
            }
            else
            {
                return CellSize;
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        [DefaultValue(null)]
        public List<TileControl> Tiles { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        [DefaultValue(typeof(Color), "Black")]
        public Color DefaultTileTextForeColor { get; set; } = Color.Black;
        [DefaultValue("Black")]
        public string DefaultTileTextForeColorWeb
        {
            get
            {
                return ColorTranslator.ToHtml(DefaultTileTextForeColor);
            }
            set
            {
                DefaultTileTextForeColor = ColorTranslator.FromHtml(value);
            }
        }
        [System.Xml.Serialization.XmlIgnore]
        [DefaultValue(typeof(Color), "AliceBlue")]
        public Color DefaultTileBackColor { get; set; } = Color.AliceBlue;
        [DefaultValue("AliceBlue")]
        public string DefaultTileBackColorWeb
        {
            get
            {
                return ColorTranslator.ToHtml(DefaultTileBackColor);
            }
            set
            {
                DefaultTileBackColor = ColorTranslator.FromHtml(value);
            }
        }

        private int widthOld = -1;
        private void TilesControl_SizeChanged(object sender, System.EventArgs e)
        {
            if (widthOld != this.Width)
            {
                widthOld = this.Width;
                if (this.AutoSizeCell)
                {
                    RefreshTiles();
                }
            }
        }

        [DefaultValue(false)]
        public bool RefreshTilesNow = false;
        public void RefreshTiles(bool animationNo = true, bool needDoEvents = false)
        {
            if (this.Tiles != null)
            {
                TileDragAndDrop = null;
                RefreshTilesNow = true;
                int isSpecialCount = 0;
                List<TileControl> isSpecialTc = new List<TileControl>();
                for (int i = 0; i < Tiles.Count - isSpecialCount; i++)
                {
                    TileControl tc = Tiles[i];
                    if (tc.IsSpecial && !isSpecialTc.Contains(tc))
                    {
                        isSpecialCount++;
                        isSpecialTc.Add(tc);
                        Tiles.Remove(tc);
                        Tiles.Add(tc);
                        i--;
                    }
                }
                for (int i = 0; i < Tiles.Count; i++)
                {
                    TileControl tc = Tiles[i];
                    if (!animationNo)
                    {
                        tc.Location = new Point(-tc.Width, -tc.Height);
                    }
                    tc.MouseIn = false;
                    tc.RefreshTile();
                    if (!animationNo)
                    {
                        tc.Location = new Point(-tc.Width, -tc.Height);
                    }
                    if (needDoEvents)
                    {
                        Application.DoEvents();
                    }
                    if (nowMouseWheelBreak)
                    {
                        nowMouseWheelBreak = false;
                        break;
                    }
                }
                if (!nowMouseWheelBreak)
                {
                    RefreshTilesLocation(animationNo);
                }
                RefreshTilesNow = false;
            }
        }
        public int GetCellsCountX()
        {
            int cellsCountX = AutoSizeColumnCount;
            if (!AutoSizeCell && !FixedColumnCount)
            {
                cellsCountX = (int)Math.Floor((float)this.Width / (float)(CellSize.Width + this.MarginTile.Left + this.MarginTile.Right));
            }
            return Math.Max(1, cellsCountX);
        }
        public void RefreshTilesLocation(bool animationNo = false)
        {

            List<Point> newLocations = new List<Point>();
            int newHeight = 1;
            int cellsCountX = GetCellsCountX();
            List<List<bool>> cellUsed = new List<List<bool>>();
            foreach (TileControl tc in Tiles)
            {
                if (!tc.Tile.Visible)
                {
                    newLocations.Add(new Point());
                    continue;
                }
                Point newLocation = new Point();
                bool nextTC = false;
                int y = 0;
                while (!nextTC)
                {
                    if (cellUsed.Count - 1 < y)
                    {
                        List<bool> rowItem = new List<bool>();
                        for (int i = 0; i < cellsCountX; i++)
                        {
                            rowItem.Add(false);
                        }
                        cellUsed.Add(rowItem);
                    }
                    int x;
                    for (x = 0; x < cellsCountX; x++)
                    {
                        bool canUse = true;
                        if (tc.GetCellsCount().Width <= cellsCountX)
                        {
                            for (int ix = 0; ix < tc.GetCellsCount().Width; ix++)
                            {
                                if (cellUsed[y].Count - 1 < x + ix)
                                {
                                    canUse = false;
                                    break;
                                }
                                canUse = canUse && !cellUsed[y][x + ix];
                            }
                        }
                        else
                        {
                            canUse = canUse && !cellUsed[y][0];
                        }
                        if (canUse)
                        {

                            for (int iy = 0; iy < tc.GetCellsCount().Height; iy++)
                            {
                                if (cellUsed.Count - 1 < y + iy)
                                {
                                    List<bool> rowItem = new List<bool>();
                                    for (int i = 0; i < cellsCountX; i++)
                                    {
                                        rowItem.Add(false);
                                    }
                                    cellUsed.Add(rowItem);
                                }
                                for (int ix = 0; ix < Math.Min(tc.GetCellsCount().Width, cellsCountX); ix++)
                                {
                                    cellUsed[y + iy][x + ix] = true;
                                }
                            }

                            Size cellSize = GetCellSize();                            
                            newLocation = new Point(x * (cellSize.Width + this.MarginTile.Left + this.MarginTile.Right) + this.MarginTile.Left, y * (cellSize.Height + this.MarginTile.Top + this.MarginTile.Bottom) + this.MarginTile.Top);
                            newHeight = Math.Max(newHeight, (y + tc.GetCellsCount().Height) * (cellSize.Height + this.MarginTile.Top + this.MarginTile.Bottom));
                            nextTC = true;
                            break;
                        }
                    }
                    if (x == cellsCountX)
                    {
                        y++;
                    }
                }
                newLocation = new Point(newLocation.X - this.MarginTile.Left + this.Padding.Left, newLocation.Y - this.MarginTile.Top + this.Padding.Top);
                newLocations.Add(newLocation);
            }
            newHeight = newHeight - this.MarginTile.Top - this.MarginTile.Bottom + this.Padding.Top + this.Padding.Bottom;

            this.Height = newHeight;
            if (this.MoveAtSameTime && this.MoveAnimation && !animationNo)
            {
                SetNewLocations(newLocations);
            }
            else
            {
                NowSetNewLocations = true;
                for (var i = 0; i < Tiles.Count; i++)
                {
                    TileControl tc = Tiles[i];
                    if (this.TileDragAndDrop != null && tc.Equals(this.TileDragAndDrop))
                    {
                        continue;
                    }
                    Point newLocation = newLocations[i];
                    if (this.MoveAnimation && !animationNo)
                    {
                        SetNewLocation(tc, newLocation);
                    }
                    else
                    {
                        tc.Location = newLocation;
                    }
                }
                NowSetNewLocations = false;
            }
        }

        private List<Point> newLocations;
        private bool autoSizeCell;

        public List<Size> TileSizeOld = new List<Size>();
        private void RefreshTileSize(TileControl tileControl)
        {
            if (UseRow)
            {
                Size size = tileControl.Size;
                int index = Tiles.IndexOf(tileControl);
                if (index != -1)
                {
                    int i = index;
                    while (i > TileSizeOld.Count - 1)
                    {
                        TileSizeOld.Add(new Size());
                        i--;
                    }

                    if (size != TileSizeOld[index])
                    {
                        tileControl.RefreshTile();
                        TileSizeOld[index] = tileControl.Size;
                    }
                }
            }
        }
        private void SetNewLocation(TileControl tileControl, Point newLocation)
        {
            if (this.TileDragAndDrop == null)
            {
                tileControl.BringToFront();
            }
            RefreshTileSize(tileControl);

            Point oldLocation = tileControl.Location;
            for (int i = 0; i < 1000; i++)
            {
                int x;
                int y;
                Double x1 = oldLocation.X, y1 = oldLocation.Y, x2 = newLocation.X, y2 = newLocation.Y, k, b;
                //x1, y1, x2. y2 - Координаты концов прямой, целые числа
                //y1 = k * x1 + b;
                //y2 = k * x2 + b;
                //(y2 - y1) = k * (x2 - x1);
                // Теперь получаем угловой коффициент прямой
                if ((x2 - x1) == 0)
                {

                    if ((y2 - y1) == 0)
                    {
                        // Теперь для любой точки прямой должно выполнятся это уравнение
                        //x = (int)x1;
                        //y = (int)y1;
                        break;//
                    }
                    else
                    {
                        k = (x2 - x1) / (y2 - y1);
                        // определяем b и конвертируем его до целого значения
                        b = x1 - k * y1;
                        // Теперь для любой точки прямой должно выполнятся это уравнение
                        y = (int)(y2 + (y1 - y2) / (i + 1));
                        x = (int)(k * y + b);
                    }
                }
                else
                {
                    k = (y2 - y1) / (x2 - x1);
                    // определяем b и конвертируем его до целого значения
                    b = y1 - k * x1;
                    // Теперь для любой точки прямой должно выполнятся это уравнение
                    x = (int)(x2 + (x1 - x2) / (i + 1));
                    y = (int)(k * x + b);
                }


                tileControl.Location = new Point(x, y);
            }
            tileControl.Location = newLocation;
        }

        public bool NowSetNewLocations = false;
        private void SetNewLocations(List<Point> newLocation)
        {
            if (NowSetNewLocations)
            {
                return;
            }
            NowSetNewLocations = true;
            Application.DoEvents();

            List<Point> oldLocation = new List<Point>();
            for (int index = 0; index < Tiles.Count; index++)
            {
                TileControl tc = Tiles[index];
                oldLocation.Add(tc.Location);
                RefreshTileSize(tc);
            }

            int n = 10;
            int animationDelay = 300;
            if (!MoveEvenly)
            {
                n = 250;
                animationDelay = 500;
            }

            float animationStep = animationDelay / n;
            DateTime dateBegin = DateTime.Now;
            for (int i = 0; i < n; i++)
            {
                DateTime dateStart = DateTime.Now;
                int inPlaceCount = 0;
                for (int index = 0; index < Tiles.Count; index++)
                {
                    TileControl tc = Tiles[index];
                    if (this.TileDragAndDrop != null && tc.Equals(this.TileDragAndDrop))
                    {
                        inPlaceCount++;
                        continue;
                    }
                    if (tc.Location == newLocation[index])
                    {
                        inPlaceCount++;
                        continue;
                    }
                    int x;
                    int y;
                    Double x1 = oldLocation[index].X, y1 = oldLocation[index].Y, x2 = newLocation[index].X, y2 = newLocation[index].Y, k, b;
                    //x1, y1, x2. y2 - Координаты концов прямой, целые числа
                    //y1 = k * x1 + b;
                    //y2 = k * x2 + b;
                    //(y2 - y1) = k * (x2 - x1);

                    if ((x2 - x1) == 0)
                    {

                        if ((y2 - y1) == 0)
                        {
                            // Теперь для любой точки прямой должно выполнятся это уравнение
                            x = (int)x1;
                            y = (int)y1;
                            //break;//
                        }
                        else
                        {
                            // Теперь получаем угловой коффициент прямой
                            k = (x2 - x1) / (y2 - y1);
                            // определяем b и конвертируем его до целого значения
                            b = x1 - k * y1;
                            // Теперь для любой точки прямой должно выполнятся это уравнение
                            if (MoveEvenly)
                            {
                                y = (int)(y1 + (y2 - y1) / n * (i + 1));
                            }
                            else
                            {
                                y = (int)(y2 + (y1 - y2) / (i + 1));
                            }
                            x = (int)(k * y + b);

                        }
                    }
                    else
                    {
                        // Теперь получаем угловой коффициент прямой
                        k = (y2 - y1) / (x2 - x1);
                        // определяем b и конвертируем его до целого значения
                        b = y1 - k * x1;
                        // Теперь для любой точки прямой должно выполнятся это уравнение
                        if (MoveEvenly)
                        {
                            x = (int)(x1 + (x2 - x1) / n * (i + 1));
                        }
                        else
                        {
                            x = (int)(x2 + (x1 - x2) / (i + 1));
                        }
                        y = (int)(k * x + b);
                        if (index == 5)
                        {
                            index = 5;
                        }
                    }

                    tc.Location = new Point(x, y);
                }

                Application.DoEvents();

                if (inPlaceCount == Tiles.Count)
                {
                    break;
                }

                DateTime dateFinish = DateTime.Now;
                TimeSpan timeResultAll = dateFinish - dateBegin;
                if (timeResultAll.TotalMilliseconds > animationDelay)
                {
                    //break;
                }
                long timeResult = dateFinish.Ticks - dateStart.Ticks;
                TimeSpan ms = new TimeSpan((int)(animationStep * 10000 - timeResult));
                if (ms.Ticks > 0)
                {
                    System.Threading.Thread.Sleep(ms);
                }
            }
            for (int index = 0; index < Tiles.Count; index++)
            {
                TileControl tc = Tiles[index];
                if (this.TileDragAndDrop != null && tc.Equals(this.TileDragAndDrop))
                {
                    continue;
                }
                tc.Location = newLocation[index];
            }

            NowSetNewLocations = false;
        }

        //public void SaveTilesServer(string filename)
        //{
        //    TilesServerForSave tfs = new TilesServerForSave();
        //    tfs.BackColor = this.BackColor;
        //    tfs.DefaultTileBackColor = this.DefaultTileBackColor;
        //    tfs.DefaultTileTextForeColor = this.DefaultTileTextForeColor;
        //    tfs.Height = this.Height;
        //    tfs.CellSize = this.CellSize;
        //    tfs.AutoSizeCell = this.AutoSizeCell;
        //    tfs.AutoSizeColumnCount = this.AutoSizeColumnCount;
        //    tfs.FixedColumnCount = this.FixedColumnCount;
        //    tfs.UseRow = this.UseRow;
        //    tfs.RowTemplate = this.RowTemplate;

        //    foreach (TileControl tc in this.Tiles)
        //    {
        //        tfs.TilesServer.Add(tc.Tile.GetTileServer());
        //    }
        //    tfs.SaveAs(filename, null, ref Information);
        //}

        public void SaveTiles(string filename)
        {
            SaveTiles(filename, null);
        }
        public void SaveTilesForServer(string filename)
        {
            XmlAttributeOverrides overrides = new OverrideXml()
            .Override<TilesForSave>()
            .Member("Tiles")
                .Override<Tile>()
                .Member("ImageSerialized").XmlIgnore()
                .Member("ImageOriginalSerialized").XmlIgnore()
            .Commit();

            SaveTiles(filename, overrides);
        }

        private void SaveTiles(string filename, XmlAttributeOverrides overrides)
        {
            TilesForSave tfs = new TilesForSave();
            tfs.BackColor = this.BackColor;
            tfs.DefaultTileBackColor = this.DefaultTileBackColor;
            tfs.DefaultTileTextForeColor = this.DefaultTileTextForeColor;
            tfs.Height = this.Height;
            tfs.CellSize = this.CellSize;
            tfs.AutoSizeCell = this.AutoSizeCell;
            tfs.AutoSizeColumnCount = this.AutoSizeColumnCount;
            tfs.FixedColumnCount = this.FixedColumnCount;
            tfs.UseRow = this.UseRow;
            tfs.RowTemplate = this.RowTemplate;

            foreach (TileControl tc in this.Tiles)
            {
                tfs.Tiles.Add(tc.Tile);
            }

            tfs.SaveAs(filename, overrides, ref Information);
        }

        public string WebSite { get; set; }
        public Image GetTileImage(string webSite, string filename, string img_size_directory, WebClient webClient)
        {
            if (filename != null && filename != "")
            {
                try
                {
                    string localPath = Path.Combine(GetImageDirectory(img_size_directory), filename);
                    if (!System.IO.File.Exists(localPath))
                    {
                        webClient.DownloadFile(webSite + "/img/tile/main_imgs/" + img_size_directory + "/" + filename, localPath);
                    }
                    return Image.FromFile(localPath);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        public void ReadTilesForServer(string filename)
        {
            TilesForSave tfs = TilesForSave.Read(filename, ref Information);

            ClearAllTiles(true);

            this.BackColor = tfs.BackColor;
            this.DefaultTileBackColor = tfs.DefaultTileBackColor;
            this.DefaultTileTextForeColor = tfs.DefaultTileTextForeColor;
            this.Height = tfs.Height;
            this.CellSize = tfs.CellSize;
            this.AutoSizeCell = tfs.AutoSizeCell;
            this.AutoSizeColumnCount = tfs.AutoSizeColumnCount;
            this.FixedColumnCount = tfs.FixedColumnCount;
            this.RowTemplate = tfs.RowTemplate;
            this.UseRow = tfs.UseRow;

            WebClient webClient = new WebClient();
            foreach (Tile tile in tfs.Tiles)
            {
                tile.Font = this.Font;
                tile.Image = GetTileImage(WebSite, tile.ImageFileName, "max", webClient);
                //tile.ImageOriginal = GetTileImage(WebSite, tile.ImageFileName, "original", webClient);
                TileControl tc = AddTile(tile);
                tc.RefreshTile();
            }
            webClient.Dispose();
            RefreshTiles(false);
        }

        public void ReadTiles(string filename)
        {
            TilesForSave tfs = TilesForSave.Read(filename, ref Information);

            ClearAllTiles(true);

            this.BackColor = tfs.BackColor;
            this.DefaultTileBackColor = tfs.DefaultTileBackColor;
            this.DefaultTileTextForeColor = tfs.DefaultTileTextForeColor;
            this.Height = tfs.Height;
            this.CellSize = tfs.CellSize;
            this.AutoSizeCell = tfs.AutoSizeCell;
            this.AutoSizeColumnCount = tfs.AutoSizeColumnCount;
            this.FixedColumnCount = tfs.FixedColumnCount;
            this.RowTemplate = tfs.RowTemplate;
            this.UseRow = tfs.UseRow;
            foreach (Tile tile in tfs.Tiles)
            {
                tile.Font = this.Font;
                TileControl tc = AddTile(tile);
                tc.RefreshTile();
            }
            RefreshTiles(false);
        }

        [DefaultValue(typeof(Point), "0,0")]
        public Point TileLocationBegin { get; set; }
        [DefaultValue(null)]
        public TileControl TileDragAndDrop { get; set; }
        public int indexFirst = -1;
        public int indexPrev = -1;
        private bool useContextMenu;

        private void TilesControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (TileDragAndDrop != null)
            {

            }
        }

        private void addTileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AddTileForm frm = new AddTileForm())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    TileControl tc = new TileControl();
                    this.Controls.Add(tc);
                    this.Tiles.Add(tc);
                    Tile tile = new Tile();
                    tile.Image = frm.ImageTile;
                    tile.CellsTypeValue = Tile.CellsType.minSquare;
                    tile.Font = this.Font;
                    tile.Text = frm.TextTile;
                    tile.TextForeColor = frm.TextForeColorTile;
                    tile.BackColor = frm.BackColorTile;
                    tc.Tile = tile;
                    RefreshTiles();
                }
            }
        }

        public event EventHandler AddTileControlClick;
        private void AddTileControl_Click(object sender, EventArgs e)
        {
            if (AddTileControl.SpecialStandartAction)
            {
                TileControl tc = new TileControl();
                this.Controls.Add(tc);
                this.Tiles.Add(tc);
                Tile tile = new Tile();
                tile.Image = null;
                tile.CellsTypeValue = AddTileControl.Tile.CellsTypeValue;
                tile.Font = this.Font;
                tile.Text = "";
                tile.TextForeColor = AddTileControl.Tile.TextForeColor;
                tile.BackColor = AddTileControl.Tile.BackColor;
                tile.BackColor2 = AddTileControl.Tile.BackColor2;
                tile.BackColorMouse = AddTileControl.Tile.BackColorMouse;
                tc.Tile = tile;
                RefreshTiles();
            }

            AddTileControlClick?.Invoke(AddTileControl, new System.EventArgs());
        }

        public List<DoAnimation> doAnimations = new List<DoAnimation>();
        private bool breakAnimation = false;
        private bool nowMouseWheel;
        private Color viewPanelBackColor;
        private Color viewPanelBackColor2;
        public bool UseAnimationZoom = false;
        private ActionProvider information;

        public void DoAnimation(TileControl tileControl, bool reverse)
        {
            if (tileControl.Tile.Image == null && !tileControl.Tile.IsSpecial || tileControl.Tile.SpecilaType == SpecilaType.View)
            {
                return;
            }
            if (!UseAnimation)
            {
                return;
            }
            if (!RefreshTilesNow && !tileControl.IsEditText && !tileControl.IsEditLink && !((tileControl.Tile.Text == null || tileControl.Tile.Text == "") && tileControl.Tile.Image == null))
            {
                int wasI = -1;
                for (int i = 0; i < doAnimations.Count; i++)
                {
                    if (tileControl.Equals(doAnimations[i].TC))
                    {
                        if (doAnimations[i].Reverse != reverse)
                        {
                            //Заменяем
                            wasI = i;
                        }
                        else
                        {
                            //Не добавляем
                            label1.Text += "\r\nwas" + Tiles.IndexOf(tileControl);
                            return;
                        }
                    }
                }

                AnimationType animationType = AnimationType.Vertical;
                int n = tileControl.Height / 2;
                if (tileControl.CellOrientation == CellOrientationType.Horizontal && tileControl.Tile.Image != null)
                {
                    animationType = AnimationType.Horizontal;
                    n = tileControl.Width / 2;
                }
                if (tileControl.Tile.OnlyImage() || (tileControl.Tile.IsSpecial && tileControl.Tile.SpecilaType != SpecilaType.View && tileControl.Tile.OnlyText()))
                {
                    if (!UseAnimationZoom)
                    {
                        return;
                    }

                    animationType = AnimationType.Zoom;
                    n = (int)(tileControl.Height * 0.2);
                    tileControl.pictureBox2.BackgroundImage = tileControl.Image2Zoom;
                    tileControl.pictureBox2.Image = null;
                    tileControl.pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
                }
                if (tileControl.Tile.OnlyText() && !tileControl.Tile.IsSpecial)
                {
                    animationType = AnimationType.VerticalAndHeight;
                    n = tileControl.Height / 4;
                }
                int animationDelay = 300;
                int animationStep = animationDelay / 25;

                if ((TileDragAndDrop == null))
                {
                    float valueNew = 0;
                    float value2New = 0;
                    switch (animationType)
                    {
                        case AnimationType.Vertical:
                            valueNew = tileControl.pictureBox1.Height;
                            break;
                        case AnimationType.Horizontal:
                            valueNew = tileControl.pictureBox1.Width;
                            break;
                        case AnimationType.VerticalAndHeight:
                            value2New = tileControl.pictureBox1.Top;
                            valueNew = tileControl.pictureBox1.Height;
                            break;
                        case AnimationType.Zoom:
                            break;
                    }
                    float iNew = 0;
                    if (wasI != -1)
                    {
                        value2New = doAnimations[wasI].Value2;
                        valueNew = doAnimations[wasI].Value;
                        iNew = n - doAnimations[wasI].I;
                    }
                    DoAnimation DoAnimationNew = new DoAnimation()
                    {
                        TC = tileControl,
                        Reverse = reverse,
                        NowAnimation = true,
                        AnimationDelay = animationDelay,
                        AnimationStep = animationStep,
                        AnimationType = animationType,
                        N = n,
                        I = iNew,
                        IPlus = n / animationStep,
                        Value = valueNew,
                        Value2 = value2New,
                    };

                    breakAnimation = false;
                    bool nowDoAnimation = doAnimations.Count != 0;
                    int nnn = 0;
                    for (int i = 0; i < label1.Text.Length; i++)
                    {
                        i = label1.Text.IndexOf("\r\n", i);
                        if (i == -1)
                        {
                            break;
                        }
                        nnn++;
                    }
                    if (nnn > 30)
                    {
                        label1.Text = "";
                    }
                    label1.Text += "\r\n" + Tiles.IndexOf(tileControl) + nowDoAnimation.ToString();
                    if (reverse)
                    {
                        label1.Text += "-R-";
                    }
                    if (wasI == -1)
                    {
                        doAnimations.Add(DoAnimationNew);
                        label1.Text += "-Add";
                    }
                    else
                    {
                        doAnimations[wasI] = DoAnimationNew;
                        label1.Text += "-" + wasI + "-Replace";
                    }

                    if (true || !nowDoAnimation)
                    {
                        while (doAnimations.Count != 0)
                        {
                            DateTime dateStart = DateTime.Now;

                            int i = 0;
                            while (i < doAnimations.Count)
                            {
                                DoAnimation doAnimation = doAnimations[i];
                                TileControl tc = doAnimations[i].TC;
                                Tile Tile = tc.Tile;
                                PictureBox pictureBox1 = tc.pictureBox1;
                                PictureBox pictureBox2 = tc.pictureBox2;

                                if (true)
                                {
                                    if (doAnimation.Reverse)
                                    {
                                        switch (doAnimation.AnimationType)
                                        {
                                            case AnimationType.Vertical:
                                                doAnimation.Value = Math.Max(0, doAnimation.Value - doAnimation.IPlus);
                                                pictureBox1.Height = (int)doAnimation.Value;
                                                break;
                                            case AnimationType.Horizontal:
                                                doAnimation.Value = Math.Max(0, doAnimation.Value - doAnimation.IPlus);
                                                pictureBox1.Width = (int)doAnimation.Value;
                                                break;
                                            case AnimationType.VerticalAndHeight:
                                                doAnimation.Value2 = Math.Min(tc.Height / 4, doAnimation.Value2 + doAnimation.IPlus);
                                                pictureBox1.Top = (int)doAnimation.Value2;
                                                doAnimation.Value = Math.Max(tc.Height / 2, doAnimation.Value - 2 * doAnimation.IPlus);
                                                pictureBox1.Height = (int)doAnimation.Value;
                                                break;
                                            case AnimationType.Zoom:
                                                float k = tc.Height / 4 - doAnimation.I;
                                                Point pictureBox_Location = new Point(-(int)((k / 2) * ((float)tc.Width / tc.Height)), -(int)((k / 2)));
                                                Size pictureBox_Size = new Size(tc.Width - 2 * pictureBox_Location.X, tc.Height - 2 * pictureBox_Location.Y);

                                                pictureBox2.Location = pictureBox_Location;
                                                pictureBox2.Size = pictureBox_Size;                      
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (doAnimation.AnimationType)
                                        {
                                            case AnimationType.Vertical:
                                                doAnimation.Value = Math.Min(tc.Height / 2, doAnimation.Value + doAnimation.IPlus);
                                                pictureBox1.Height = (int)doAnimation.Value;
                                                break;
                                            case AnimationType.Horizontal:
                                                doAnimation.Value = Math.Min(tc.Width / 2, doAnimation.Value + doAnimation.IPlus);
                                                pictureBox1.Width = (int)doAnimation.Value;
                                                break;
                                            case AnimationType.VerticalAndHeight:
                                                doAnimation.Value2 = Math.Min(tc.Height / 4, doAnimation.Value2 - doAnimation.IPlus);
                                                pictureBox1.Top = (int)doAnimation.Value2;
                                                doAnimation.Value = Math.Max(tc.Height / 2, doAnimation.Value + 2 * doAnimation.IPlus);
                                                pictureBox1.Height = (int)doAnimation.Value;
                                                break;
                                            case AnimationType.Zoom:
                                                float k = doAnimation.I;
                                                Point pictureBox_Location = new Point(-(int)((k / 2) * ((float)tc.Width / tc.Height)), -(int)((k / 2)));
                                                Size pictureBox_Size = new Size(tc.Width - 2 * pictureBox_Location.X, tc.Height - 2 * pictureBox_Location.Y);

                                                pictureBox2.Location = pictureBox_Location;
                                                pictureBox2.Size = pictureBox_Size;
                                                break;
                                        }
                                    }
                                }
                                pictureBox2.Refresh();
                                pictureBox1.Refresh();

                                doAnimation.I += doAnimation.IPlus;
                                if (doAnimation.I >= doAnimation.N)
                                {
                                    doAnimation.NowAnimation = false;
                                }
                                if (!doAnimation.NowAnimation)
                                {
                                    if (doAnimation.Reverse)
                                    {
                                        switch (doAnimation.AnimationType)
                                        {
                                            case AnimationType.Vertical:
                                                pictureBox1.Height = 0;
                                                break;
                                            case AnimationType.Horizontal:
                                                pictureBox1.Width = 0;
                                                break;
                                            case AnimationType.VerticalAndHeight:
                                                pictureBox1.Height = tc.Height / 2;
                                                pictureBox1.Top = tc.Height / 4;
                                                break;
                                            case AnimationType.Zoom:
                                                pictureBox2.Location = new Point(0, 0);
                                                pictureBox2.Size = tc.Size;
                                                pictureBox2.Image = tc.Image2;
                                                pictureBox2.BackgroundImage = null;                                               
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (doAnimation.AnimationType)
                                        {
                                            case AnimationType.Vertical:
                                                pictureBox1.Height = tc.Height / 2;
                                                break;
                                            case AnimationType.Horizontal:
                                                pictureBox1.Width = tc.Width / 2;
                                                break;
                                            case AnimationType.VerticalAndHeight:
                                                pictureBox1.Height = tc.Height;
                                                pictureBox1.Top = 0;
                                                break;
                                            case AnimationType.Zoom:
                                                Point pictureBox_Location = new Point(-(int)(tc.Width * 0.1), -(int)(tc.Height * 0.1));
                                                Size pictureBox_Size = new Size((int)(tc.Width * 1.2), (int)(tc.Height * 1.2));
                                                pictureBox2.Location = pictureBox_Location;
                                                pictureBox2.Size = pictureBox_Size;                                               
                                                break;
                                        }
                                    }
                                    doAnimations.Remove(doAnimation);
                                    i--;
                                }
                                i++;

                            }

                            Application.DoEvents();
                            if (breakAnimation)
                            {
                                breakAnimation = false;
                                label1.Text += "\r\nbreak";
                                break;
                            }
                            DateTime dateFinish = DateTime.Now;
                            long timeResult = dateFinish.Ticks - dateStart.Ticks;
                            TimeSpan ms = new TimeSpan((int)(animationStep * 10000 - timeResult));
                            if (ms.Ticks > 0)
                            {
                                System.Threading.Thread.Sleep(ms);
                            }

                        }
                        label1.Text += "\r\nEND";
                        breakAnimation = true;
                    }
                }

            }
        }

        public TileControl AddTile(Tile tile)
        {
            if (this.Tiles == null)
            {
                this.Tiles = new List<TileControl>();
            }
            TileControl tc = new TileControl();
            this.Controls.Add(tc);
            this.Tiles.Add(tc);
            tc.Tile = tile;

            tc.Location = tile.Location;
            tc.Visible = tile.Visible;

            if (tc.SpecilaType == SpecilaType.Add)
            {
                AddTileControl = tc;
                AddTileControl.Click += AddTileControl_Click;
                AddTileControl.pictureBox1.Click += AddTileControl_Click;
                AddTileControl.pictureBox2.Click += AddTileControl_Click;
            }
            if (tc.SpecilaType == SpecilaType.Publish)
            {
                PublishTileControl = tc;
                PublishTileControl.Click += PublishTileControl_Click;
                PublishTileControl.pictureBox1.Click += PublishTileControl_Click;
                PublishTileControl.pictureBox2.Click += PublishTileControl_Click;
            }

            return tc;
        }

        public void RemoveTile(TileControl tileControl, bool needRefresh)
        {
            tileControl.DeleteTile(needRefresh);
        }
        public void ClearTiles(bool needRefresh)
        {
            for (int i = Tiles.Count - 1; i >= 0; i--)
            {
                if (!Tiles[i].IsSpecial)
                {
                    Tiles[i].DeleteTile(false);
                }
            }
            if (needRefresh)
            {
                RefreshTiles();
            }
        }
        public void ClearAllTiles(bool needRefresh)
        {
            for (int i = Tiles.Count - 1; i >= 0; i--)
            {
                Tiles[i].DeleteTile(false);
            }
            if (needRefresh)
            {
                RefreshTiles();
            }
        }

        public string GetImageDirectory(string tileSizeDirectory)
        {
            string folder = Path.Combine(Application.StartupPath, Path.Combine("img", Path.Combine("tile", Path.Combine("main_imgs", tileSizeDirectory))));
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            return folder;
        }

        public void TilesToServer(string webSite, string auth_string)
        {
            WebClient webClient = new WebClient();
            foreach (TileControl tileControl in Tiles)
            {
                if (!TileToServer(tileControl.Tile, webSite, auth_string, webClient, ref Information))
                {
                    break;
                }
            }

            webClient.Dispose();
        }
        public bool TileToServer(Tile tile, string webSite, string auth_string, WebClient webClient, ref BaseClasses.ActionProvider information)
        {
            bool result = true;

            string _prev_information = information.InfoMessages.Last();
            information.AddInfo("Сохранение картинок плиток на сервер...");
            information.ErrorWhatMaybeAdd = new ErrorInfo();
            information.ErrorWhatMaybeAdd.ErrorIcon = MessageBoxIcon.Error;
            information.ErrorWhatMaybeAdd.ErrorCaption = "Ошибка!";
            information.ErrorWhatMaybeAdd.ErrorMessage = "Ошибка при сохранении картинок плиток на сервер.";

            try
            {
                if (tile.Image == null)
                {
                    result = true;
                }
                else
                {
                    string filename = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "_" + Guid.NewGuid() + "." + tile.ImageFormatSerialized.ToLower();
                    if (tile.ImageFileName != null && tile.ImageFileName != "")
                    {
                        filename = tile.ImageFileName;
                    }
                    else
                    {
                        string[] img_size_directories = { "max", "original" };
                        foreach (string img_size_directory in img_size_directories)
                        {
                            string localPath = Path.Combine(GetImageDirectory(img_size_directory), filename);
                            tile.Image.Save(localPath);

                            string uri = webSite + "/free-for-program/load-img";
                            uri += "/" + Session.Session.auth_session_string + "/" + img_size_directory;
                            //var parameters = new System.Collections.Specialized.NameValueCollection()
                            //            {
                            //                { "login", login },
                            //                { "password", password},
                            //                { "filename", filename},
                            //                { "img_size_directory", img_size_directory}
                            //            };

                            webClient.Headers[HttpRequestHeader.ContentType] = "binary / octet - stream";// "multipart/form-data; boundary=something";// "application/x-www-form-urlencoded";
                            webClient.Headers[HttpRequestHeader.ContentLanguage] = "ru-RU";
                            //webClient.QueryString = parameters;
                            // MessageBox.Show(  Encoding.UTF8.GetString(client.UploadValues(uri, parameters)));
                            byte[] responseBytes = webClient.UploadFile(uri, "POST", localPath);
                            string response = Encoding.UTF8.GetString(responseBytes);
                            //MessageBox.Show(response);
                            resp responseJson = JsonConvert.DeserializeObject<resp>(response);
                            if (responseJson.error)
                            {
                                information.AddError(null, responseJson.message, "Error!", MessageBoxIcon.Error);
                                result = false;
                                break;
                            }
                            Session.Session.auth_session_string = responseJson.auth_session_string;
                        }
                        if (result)
                        {
                            tile.ImageFileName = filename;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                information.AddError2(ex, "", "Error!", MessageBoxIcon.Error);
                result = false;
            }
            information.AddInfo(_prev_information);
            return result;
        }

        public void TileServerLogin(string webSite, string login, string password)
        {
            WebClient webClient = new WebClient();
            resp resp = TileServerLogin(webSite, login, password, webClient, ref Information);
            if (resp != null && resp.success)
            {
                Session.Session.auth_string = resp.auth_string;
                Session.Session.auth_session_string = resp.auth_session_string;
            }

            webClient.Dispose();
        }
        public resp TileServerLogin(string webSite, string login, string password, WebClient webClient, ref BaseClasses.ActionProvider information)
        {
            bool result = true;

            WebSite = webSite;
            string _prev_information = information.InfoMessages.Last();
            information.AddInfo("Авторизация на сервере...");
            information.ErrorWhatMaybeAdd = new ErrorInfo();
            information.ErrorWhatMaybeAdd.ErrorIcon = MessageBoxIcon.Error;
            information.ErrorWhatMaybeAdd.ErrorCaption = "Ошибка!";
            information.ErrorWhatMaybeAdd.ErrorMessage = "Ошибка при авторизации на сервере.";

            resp responseJson = null;
            try
            {
                string uri = webSite + "/free-for-program/login";
                uri += "/" + login + "/" + password;

                webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";// "binary / octet - stream";// "multipart/form-data; boundary=something";// "application/x-www-form-urlencoded";
                webClient.Headers[HttpRequestHeader.ContentLanguage] = "ru-RU";

                var parameters = new System.Collections.Specialized.NameValueCollection();

                byte[] responseBytes = webClient.UploadValues(uri, "POST", parameters);
                string response = Encoding.UTF8.GetString(responseBytes);

                responseJson = JsonConvert.DeserializeObject<resp>(response);
                if (responseJson.error)
                {
                    information.AddError(null, responseJson.message, "Error!", MessageBoxIcon.Error);
                    result = false;
                }

            }
            catch (Exception ex)
            {
                information.AddError2(ex, "", "Error!", MessageBoxIcon.Error);
                result = false;
            }
            information.AddInfo(_prev_information);
            return responseJson;
        }
    }

    public class resp
    {
        public bool success { get; set; }
        public bool error { get; set; }
        public string message { get; set; }
        public string auth_string { get; set; }
        public string auth_session_string { get; set; }
    }
}
