using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace ScrollAblePanel
{
    //Properties to use by the Visual Studio Sesigner
    [Designer(typeof(ScrollAblePanelDesigner))] //Set the desiner to the custom ScrollAblePanel designer
    [Docking(DockingBehavior.Ask)]              //propts the user to dock the control
    public partial class ScrollAblePanelControl : UserControl
    {
        //VARIABLES
        private bool _IsMouseDown;          //If the mouse is down or not (Main Panel)
        private Point _LastMouseMove;       //The Position of the last mouse position recorded
        private bool _IsMouseVDown;         //if the mouse is down or not (Scroll Bar)
        private bool _IsMouseHDown;
        private bool _DisableScrollingV;     //if the panel is too small, turn off the scrolling          
        private bool _DisableScrollingH;

        public event EventHandler ScrollingVEnd;
        public event EventHandler ScrollingHEnd;

        #region Properties
        [DefaultValue(false)]
        public bool AddUp { get; set; }
        [DefaultValue(typeof(Size), "0, 0")]
        public Size Otstup { get; set; }        
        [DefaultValue(true)]
        public bool AutoSetMinSize { get; set; }
        [DefaultValue(20)]
        public int ScrollMin { get; set; }
        [DefaultValue(60)]
        public int ScrollMax { get; set; }
        [DefaultValue(4)]
        public int ScrollWhellStep { get; set; }

        [DefaultValue(0)]
        public int ScrollAtMarginBegin { get; set; }
        [DefaultValue(0)]
        public int ScrollAtMarginEnd { get; set; }

        public int ScrollHorizontalMaxValue
        {
            get
            {
                return ScrollContainterH.Width + ScrollContainterH.Location.X - ScrollAtX.Width;
            }
        }
        public int ScrollVerticalMaxValue
        {
            get
            {
                return ScrollContainterV.Height + ScrollContainterV.Location.Y - ScrollAtY.Height;
            }
        }
        public int ScrollHorizontalMinValue
        {
            get
            {
                return 0;
            }
        }
        public int ScrollVerticalMinValue
        {
            get
            {
                return 0;
            }
        }

        public int ScrollHorizontalMaxValueWithMargin
        {
            get
            {
                return ScrollContainterH.Width + ScrollContainterH.Location.X - ScrollAtX.Width - ScrollAtMarginEnd;
            }
        }
        public int ScrollVerticalMaxValueWithMargin
        {
            get
            {
                return ScrollContainterV.Height + ScrollContainterV.Location.Y - ScrollAtY.Height - ScrollAtMarginEnd;
            }
        }
        public int ScrollHorizontalMinValueWithMargin
        {
            get
            {
                return ScrollAtMarginBegin;
            }
        }
        public int ScrollVerticalMinValueWithMargin
        {
            get
            {
                return ScrollAtMarginBegin;
            }
        }
        private bool ScrollHorizontalMinNowPrev = false;
        public bool ScrollHorizontalMinNow
        {
            get
            {
                return (ScrollAtX.Left <= ScrollHorizontalMinValueWithMargin);
            }
        }
        private bool ScrollHorizontalMaxNowPrev = false;
        public bool ScrollHorizontalMaxNow
        {
            get
            {
                return (ScrollAtX.Left >= ScrollHorizontalMaxValueWithMargin);
            }
        }
        private bool ScrollVerticalMinNowPrev = false;
        public bool ScrollVerticalMinNow
        {
            get
            {
                return (ScrollAtY.Top <= ScrollVerticalMinValueWithMargin);
            }
        }
        private bool ScrollVerticalMaxNowPrev = false;
        public bool ScrollVerticalMaxNow
        {
            get
            {
                return (ScrollAtY.Top >= ScrollVerticalMaxValueWithMargin);
            }
        }

        private TreeView treeView = null;
        [DefaultValue(null)]
        public TreeView TreeView
        {
            get
            {
                return treeView;
            }
            set
            {
                if (treeView != null)
                {
                    treeView.MouseWheel -= treeView1_MouseWheel;
                    treeView.NodeMouseClick -= treeView1_NodeMouseClick;
                    treeView.NodeMouseDoubleClick -= treeView1_NodeMouseClick;
                }
                treeView = value;
                if (treeView != null)
                {
                    RefreshTreeViewHeight();
                    treeView.MouseWheel += treeView1_MouseWheel;
                    treeView.NodeMouseClick += treeView1_NodeMouseClick;
                    treeView.NodeMouseDoubleClick += treeView1_NodeMouseClick;
                }
            }
        }

        ScrollValue _VerticalScroll = new ScrollValue();
        new public ScrollValue VerticalScroll
        {
            get
            {
                return _VerticalScroll;
            }
            set
            {
                _VerticalScroll = value;
                VerticalScroll.ValueChanged += VerticalScroll_ValueChanged;
            }
        }
        ScrollValue _HorizontalScroll = new ScrollValue();
        new public ScrollValue HorizontalScroll
        {
            get
            {
                return _HorizontalScroll;
            }
            set
            {
                _HorizontalScroll = value;
                HorizontalScroll.ValueChanged += HorizontalScroll_ValueChanged;
            }
        }

        [DefaultValue(true)]
        public bool AutoVerticalScroll { get; set; }
        [DefaultValue(false)]
        public bool AutoHorizontalScroll { get; set; }

        [DefaultValue(typeof(Color), "Black")]
        public Color ScrollBarBackColor
        {
            get
            {
                return ScrollContainterV.BackColor;
            }
            set
            {
                ScrollContainterV.BackColor = value;
                ScrollContainterH.BackColor = value;
            }
        }
        private Color _ScrollAtBackColor = Color.Red;
        [DefaultValue(typeof(Color), "Red")]
        public Color ScrollAtBackColor
        {
            get
            {
                return _ScrollAtBackColor;
            }
            set
            {
                _ScrollAtBackColor = value;
                RefreshScrollBarDrawing();
            }
        }


        private bool nowDisableScrollingV = false;
        public event EventHandler DisableScrollingVChanged;
        [DefaultValue(false)]
        public bool DisableScrollingV
        {
            get
            {
                return _DisableScrollingV;
            }
            set
            {
                //if (_DisableScrollingV != value)
                //{
                _DisableScrollingV = value;
                if (DisableScrollingVChanged != null)
                {
                    nowDisableScrollingV = true;
                    DisableScrollingVChanged(this, new EventArgs());
                    nowDisableScrollingV = false;
                }
                //}
            }
        }

        private bool nowDisableScrollingH = false;
        public event EventHandler DisableScrollingHChanged;
        [DefaultValue(false)]
        public bool DisableScrollingH
        {
            get
            {
                return _DisableScrollingH;
            }
            set
            {
                //if (_DisableScrollingH != value)
                //{
                _DisableScrollingH = value;
                if (DisableScrollingHChanged != null)
                {
                    nowDisableScrollingH = true;
                    DisableScrollingHChanged(this, new EventArgs());
                    nowDisableScrollingH = false;
                }
                //}
            }
        }
        // Defines the property EditablePanel, where the scroll content can be edited
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel EditablePanel
        {
            get { return ScrollPanel; }
        }

        // ScrollAt Bar Size Control
        [Category("Appearance")]
        [Description("Gets or sets the size the scroll bar widget")]
        [DefaultValue(32)]
        public int ScrollBarSize
        {
            get { return ScrollAtY.Height; }
            set
            {
                ScrollAtY.Height = value;
                ScrollAtX.Width = value;

                RefreshScrollBarDrawing();

                CalculateScrollBarV();
                CalculateScrollBarH();
            }
        }

        [DefaultValue(8)]
        public int ScrollBarWidth
        {
            get { return ScrollAtY.Width; }
            set
            {
                ScrollAtY.Width = value;
                ScrollAtX.Height = value;

                ScrollContainterV.Width = value;
                ScrollContainterH.Height = value;

                RefreshScrollAblePanelControl();

                RefreshScrollBarDrawing();

                CalculateScrollBarV();
                CalculateScrollBarH();
            }
        }

        private bool _ScrollBarIsRound = true;
        [DefaultValue(true)]
        public bool ScrollBarIsRound
        {
            get
            {
                return _ScrollBarIsRound;
            }
            set
            {
                _ScrollBarIsRound = value;
                RefreshScrollBarDrawing();
            }
        }
        
        #endregion

        private GraphicsUtils _grUtils = new GraphicsUtils();

        #region Constructors
        public ScrollAblePanelControl()
        {
            ScrollAblePanelControl_construcror(false, null);
        }

        public ScrollAblePanelControl(bool isTypicalOptions = false, Control ctrl = null)
        {
            ScrollAblePanelControl_construcror(isTypicalOptions, ctrl);
        } 

        private void ScrollAblePanelControl_construcror(bool isTypicalOptions = false, Control ctrl = null)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            AutoSetMinSize = true;
            Otstup = new Size(0, 0);
            ScrollMin = 20;
            ScrollMax = 60;
            ScrollWhellStep = 4;
            ScrollAtMarginBegin = 0;
            ScrollAtMarginEnd = 0;
            ScrollBarWidth = 8;

            this.DoubleBuffered = true;

            ScrollAtBackColor = Color.Red;
            AutoVerticalScroll = true;
            AutoHorizontalScroll = false;

            VerticalScroll = new ScrollValue();
            HorizontalScroll = new ScrollValue();

            ScrollPanel.SizeChanged += ScrollPanel_SizeChanged;

            ScrollAtY.Paint += ScrollAtY_Paint;
            ScrollAtX.Paint += ScrollAtX_Paint;
            ScrollAtY.LocationChanged += ScrollAtY_LocationChanged;
            ScrollAtX.LocationChanged += ScrollAtX_LocationChanged;

            if (isTypicalOptions)
            {
                IniTypical(ctrl);
            }
            RefreshScrollAblePanelControl();
            RefreshScrollBarDrawing();
            RefreshScrollAtX();
            RefreshScrollAtY();

            this.Load += ScrollAblePanelControl_Load;
        }

        private void ScrollAblePanelControl_Load(object sender, EventArgs e)
        {
            this.Width += 1;
            this.Height += 1;
        }

        private int WidthPrev = -1;
        private int HeightPrev = -1;
        private void ScrollPanel_SizeChanged(object sender, EventArgs e)
        {
            if (HeightPrev != ScrollPanel.Height)
            {
                if (HeightPrev != -1)
                {
                    if (AddUp)
                    {
                        ScrollPanel.Top = Math.Min(0, Math.Max(-GetScrollVerticalAria(), ScrollPanel.Top + HeightPrev - ScrollPanel.Height));
                        CalculateScrollBarV();
                    }
                }
                _IsMouseVDown = false;//+-
            }
            if (WidthPrev != ScrollPanel.Width)
            {
                if (WidthPrev != -1)
                {
                    if (AddUp)
                    {
                        ScrollPanel.Left = Math.Min(0, Math.Max(-GetScrollHorizontalAria(), ScrollPanel.Left + WidthPrev - ScrollPanel.Width));
                        CalculateScrollBarH();
                    }
                }
                _IsMouseHDown = false;//+-
            }
            HeightPrev = ScrollPanel.Height;
            WidthPrev = ScrollPanel.Width;
        }

        private int GetScrollHorizontalMinValueWithMargin(int x)
        {
            return Math.Max(ScrollHorizontalMinValue, Math.Min(ScrollHorizontalMaxValue,x));
        }
        private void ScrollAtX_LocationChanged(object sender, EventArgs e)
        {
            ScrollAtX.Left = GetScrollHorizontalMinValueWithMargin(ScrollAtX.Location.X);
        }

        private void ScrollAtY_LocationChanged(object sender, EventArgs e)
        {
            ScrollAtY.Top = GetScrollVerticalMinValueWithMargin(ScrollAtY.Location.Y);
        }
        private int GetScrollVerticalMinValueWithMargin(int y)
        {
            return Math.Max(ScrollVerticalMinValueWithMargin, Math.Min(ScrollVerticalMaxValueWithMargin, y));
        }
        #endregion

        #region Ini
        private void IniTypical(Control ctrl)
        {
            // 
            // panelMessages
            // 
            AutoHorizontalScroll = false;
            AutoVerticalScroll = true;
            BackColor = System.Drawing.Color.Transparent;
            Cursor = System.Windows.Forms.Cursors.Default;
            DisableScrollingH = true;
            DisableScrollingV = true;
            Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // panelMessages.EditablePanel
            // 
            ScrollPanel.AutoScroll = true;
            ScrollPanel.BackColor = System.Drawing.Color.Transparent;

            if (ctrl != null)
            {
                ScrollPanel.Controls.Add(ctrl);//(this.flowLayoutPanelMessages);
            }

            ScrollPanel.Location = new System.Drawing.Point(0, 0);
            ScrollPanel.Name = "EditablePanel";
            ScrollPanel.Size = new System.Drawing.Size(472, 329);
            ScrollPanel.TabIndex = 2;

            HorizontalScroll = new ScrollValue()
            {
                Maximum = 0,
                Minimum = 0,
                Value = 0
            };

            ImeMode = System.Windows.Forms.ImeMode.NoControl;
            Location = new System.Drawing.Point(0, 0);
            Name = "panelMessages";
            Otstup = new System.Drawing.Size(0, 0);
            ScrollAtBackColor = System.Drawing.Color.Red;//.SteelBlue;
            ScrollBarBackColor = System.Drawing.Color.Transparent;
            ScrollBarSize = 120;
            ScrollMax = 60;
            ScrollMin = 20;
            Size = new System.Drawing.Size(600, 450);
            TabIndex = 0;
            TreeView = null;

            VerticalScroll = new ScrollValue()
            {
                Maximum = 0,
                Minimum = 0,
                Value = 0
            };

            Start();
        }

        public void Start()
        {
            //ScrollPanel.AutoScroll = false;
            //ScrollPanel.AutoSize = true;
            //ScrollPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            EditablePanel.AutoScroll = false;
            EditablePanel.AutoSize = true;
            EditablePanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        } 
        #endregion

        public void RefreshScrollAtX()
        {
            if (ScrollAtX.BackgroundImage != null)
            {
                ScrollAtX.BackgroundImage.Dispose();
            }
            Image img = new Bitmap(ScrollAtX.Width,ScrollAtX.Height);
            using (Graphics g = Graphics.FromImage(img))
            {
                DrawScrollAtX(g);
            }
            ScrollAtX.BackgroundImage = img;
        }
        public void RefreshScrollAtY()
        {
            if (ScrollAtY.BackgroundImage != null)
            {
                ScrollAtY.BackgroundImage.Dispose();
            }
            Image img = new Bitmap(ScrollAtY.Width, ScrollAtY.Height);
            using (Graphics g = Graphics.FromImage(img))
            {
                DrawScrollAtY(g);
            }
            ScrollAtY.BackgroundImage = img;
        }
        private void DrawScrollAtX(Graphics g)
        {
            //RefreshScrollBarDrawing();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int h = ScrollAtX.Height - 1;
            System.Drawing.Drawing2D.GraphicsPath pathH = new System.Drawing.Drawing2D.GraphicsPath();
            pathH.AddArc(0, 0, h, h, 90, 180);
            pathH.AddLine(h / 2, 0, ScrollAtX.Width - 1 - h / 2, 0);
            pathH.AddArc(ScrollAtX.Width - 1 - h, 0, h, h, 270, 180);
            pathH.AddLine(ScrollAtX.Width - 1 - h / 2, h, h / 2, h);

            g.FillPath(new SolidBrush(_ScrollAtBackColor), pathH);
            g.DrawPath(new Pen(_ScrollAtBackColor), pathH);
        }
        private void DrawScrollAtY(Graphics g)
        {
            //RefreshScrollBarDrawing();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int w = ScrollAtY.Width - 1;// - 1;
            System.Drawing.Drawing2D.GraphicsPath pathV = new System.Drawing.Drawing2D.GraphicsPath();
            pathV.AddArc(0, 0, w, w, 180, 180);
            pathV.AddLine(w, w / 2, w, ScrollAtY.Height - 1 - w / 2);
            pathV.AddArc(0, ScrollAtY.Height - 1 - w, w, w, 0, 180);
            pathV.AddLine(0, ScrollAtY.Height - 1 - w / 2, 0, w / 2);

            g.FillPath(new SolidBrush(_ScrollAtBackColor), pathV);
            g.DrawPath(new Pen(_ScrollAtBackColor), pathV);
            //_grUtils.DrawCounter(e, new Rectangle(0, 0, w, ScrollAtY.Height - 2),
            //     Color.FromArgb(255, ScrollAtY.BackColor.R, ScrollAtY.BackColor.G, ScrollAtY.BackColor.B), 4);

        }
        private void ScrollAtX_Paint(object sender, PaintEventArgs e)
        {
            //DrawScrollAtX(e.Graphics);
        }

        private void ScrollAtY_Paint(object sender, PaintEventArgs e)
        {
            //DrawScrollAtY(e.Graphics);
        }



        private void RefreshScrollBarDrawing()
        {
            //if (ScrollBarIsRound)
            //{
            //    ScrollAtY.BackColor = Color.FromArgb(0, _ScrollAtBackColor.R, _ScrollAtBackColor.G, _ScrollAtBackColor.B);
            //    ScrollAtX.BackColor = Color.FromArgb(0, _ScrollAtBackColor.R, _ScrollAtBackColor.G, _ScrollAtBackColor.B);
            //}
            //else
            //{
            //    ScrollAtY.BackColor = Color.FromArgb(255, _ScrollAtBackColor.R, _ScrollAtBackColor.G, _ScrollAtBackColor.B);
            //    ScrollAtX.BackColor = Color.FromArgb(255, _ScrollAtBackColor.R, _ScrollAtBackColor.G, _ScrollAtBackColor.B);
            //}
            if (ScrollBarIsRound)
            {
                ScrollAtY.BackColor = ScrollBarBackColor;
                ScrollAtX.BackColor = ScrollBarBackColor;
                RefreshScrollAtX();
                RefreshScrollAtY();
            }
            else
            {
                ScrollAtY.BackColor = ScrollAtBackColor;
                ScrollAtX.BackColor = ScrollAtBackColor;
            }
        }

        private bool nowScrollPanelLocationChanged = false;
        private void VerticalScroll_ValueChanged(object sender, EventArgs e)
        {
            if (!nowScrollPanelLocationChanged)
            {
                ScrollPanel.Top = -VerticalScroll.Value;
                CalculateScrollBarV();
                //CalculateScrollPanelV();
            }
        }
        private void HorizontalScroll_ValueChanged(object sender, EventArgs e)
        {
            if (!nowScrollPanelLocationChanged)
            {
                ScrollPanel.Left = -HorizontalScroll.Value;
                CalculateScrollBarH();
                //CalculateScrollPanelH();
            }
        }

        private void ScrollPanel_LocationChanged(object sender, EventArgs e)
        {
            nowScrollPanelLocationChanged = true;
            VerticalScroll.Value = -ScrollPanel.Top;
            HorizontalScroll.Value = -ScrollPanel.Left;
            nowScrollPanelLocationChanged = false;
        }

        //METHODS
        private Point GetMousePosition()
        {
            //Returns the positon of the mouse within the screen
            return (this.PointToScreen(System.Windows.Forms.Control.MousePosition));
        }

        public int GetScrollVerticalAria()
        {
            int d = Otstup.Height;
            if (ScrollContainterH.Visible)
            {
                d += ScrollContainterH.Height;
            }
            float ScrollArea = (ScrollPanel.Height - this.Height + d);
            return (int)ScrollArea;
        }
        //CACULATIONS
        //Calculates the position of the scroll bar based off the position of the main panel
        public void CalculateScrollBarV()
        {
            //Get the Y position currently at the top of the panel, being looked at
            float CurrentlyLookingAt = Math.Abs(ScrollPanel.Location.Y);
            //Find out what percent it is through the document, getting rid of the height of the panel so we go from 0-100
            float Percent = 0;
            if ((ScrollPanel.Height - this.Height) != 0)
            {
                Percent = (CurrentlyLookingAt / (ScrollPanel.Height - this.Height)) * 100;
            }
            //get the maximum movement area up and down for the ScrollAt panel
            float ScrollMovementArea = ScrollContainterV.Height - ScrollAtY.Height;
            //Translate the percentage looked at to the percentage along the scroll bar
            ScrollAtY.Top = GetScrollVerticalMinValueWithMargin(Convert.ToInt32((ScrollMovementArea/100) * Percent) + ScrollContainterV.Location.Y);
        }
        //Calculates the position of the main panel based off the position of the scroll bar
        public void CalculateScrollPanelV()
        {
            //if (ScrollAtMarginBegin > ScrollVerticalMinValue && (ScrollAtY.Location.Y <= ScrollVerticalMinValueWithMargin))
            //{
            //    ScrollPanel.Location = new Point(ScrollPanel.Location.X, 0);
            //}
            //else if (ScrollAtMarginEnd > 0 && (ScrollAtY.Location.Y >= ScrollVerticalMaxValueWithMargin))
            //{
            //    ScrollPanel.Location = new Point(ScrollPanel.Location.X, this.Height - ScrollPanel.Height);
            //}
            //else
            //{
            //get the maximum movement area up and down for the ScrollAt panel
            float ScrollMovementArea = ScrollContainterV.Height - ScrollAtY.Height;
            //Find out how along the scroll bar we currently are
            float Percent = ((ScrollAtY.Location.Y - ScrollContainterV.Location.Y) / ScrollMovementArea) * 100;
            //Get the maximum movement area for the scroll panel
            float ScrollArea = GetScrollVerticalAria();
            //Translate the percentage along the scroll bar to the percentage along the scroll panel
            try
            {
                ScrollPanel.Location = new Point(ScrollPanel.Location.X, Convert.ToInt32((ScrollArea / 100) * Percent) * -1);
            }
            catch
            {
                //бывает, что вылетает баг, когда значение недопустимо мало в строке ...
                //ScrollPanel.Location = new Point(...);
            }
            //}
        }

        public int GetScrollHorizontalAria()
        {
            int d = Otstup.Height;
            if (ScrollContainterV.Visible)
            {
                d += ScrollContainterV.Width;
            }
            float ScrollArea = (ScrollPanel.Width - this.Width + d);
            return (int)ScrollArea;
        }

        public void CalculateScrollBarH()
        {
            //Get the Y position currently at the top of the panel, being looked at
            float CurrentlyLookingAt = Math.Abs(ScrollPanel.Location.X);
            //Find out what percent it is through the document, getting rid of the height of the panel so we go from 0-100
            float Percent = 0;
            if((ScrollPanel.Width - this.Width) != 0)
            {
                Percent = (CurrentlyLookingAt / (ScrollPanel.Width - this.Width)) * 100;
            }
            
            //get the maximum movement area up and down for the ScrollAt panel
            float ScrollMovementArea = ScrollContainterH.Width - ScrollAtX.Width;
            //Translate the percentage looked at to the percentage along the scroll bar
            ScrollAtX.Left = GetScrollHorizontalMinValueWithMargin((Convert.ToInt32((ScrollMovementArea / 100) * Percent) + ScrollContainterH.Location.X));
        }
        //Calculates the position of the main panel based off the position of the scroll bar
        public void CalculateScrollPanelH()
        {
            //if (ScrollAtMarginBegin > 0 && (ScrollAtX.Location.X <= ScrollHorizontalMinValueWithMargin))
            //{
            //    ScrollPanel.Location = new Point(0, ScrollPanel.Location.Y);
            //}
            //else if (ScrollAtMarginEnd > 0 && (ScrollAtX.Location.X >= ScrollHorizontalMaxValueWithMargin))            
            //{
            //    ScrollPanel.Location = new Point(this.Width - ScrollPanel.Width, ScrollPanel.Location.Y);
            //}
            //else
            //{
                //get the maximum movement area up and down for the ScrollAt panel
                float ScrollMovementArea = ScrollContainterH.Width - ScrollAtX.Width;
                //Find out how along the scroll bar we currently are
                float Percent = ((ScrollAtX.Location.X - ScrollContainterH.Location.X) / ScrollMovementArea) * 100;
            //Get the maximum movement area for the scroll panel
                float ScrollArea = GetScrollHorizontalAria();
                //Translate the percentage along the scroll bar to the percentage along the scroll panel
                try
                {
                    ScrollPanel.Location = new Point(Convert.ToInt32((ScrollArea / 100) * Percent) * -1, ScrollPanel.Location.Y);
                }
                catch
                {
                    //бывает, что вылетает баг, когда значение недопустимо мало в строке ...
                    //ScrollPanel.Location = new Point(...);
                }
            //}
        }


        //SCROLL PANEL EVENTS

        private void ScrollAblePanelControl_MouseDown(object sender, MouseEventArgs e)
        {
            //if the mouse is not already pressed and scrolling isnt not disabled
            if ((!_IsMouseDown) && (!_DisableScrollingV))
            {
                //set the mouse to down (Main panel)
                _IsMouseDown = true;
                //Record the position of the mouse down
                _LastMouseMove = GetMousePosition();
            }
        }

        private void ScrollAblePanelControl_MouseMove(object sender, MouseEventArgs e)
        {
            //if the mouse is down, aka we're scrolling
            if (_IsMouseDown)
            {
                //grab the current mouse position and see if it has moved
                Point currentlMouse = GetMousePosition();
                if (_LastMouseMove != currentlMouse)
                {
                    //check if it would be going over the top of the panel
                    if (ScrollPanel.Location.Y + (currentlMouse.Y - _LastMouseMove.Y) > 0)
                    {
                        //if it is, set it to the top
                        ScrollPanel.Top = 0;
                    }
                    else
                    {
                        //check if it would be going past the bottom of the panel
                        if (ScrollPanel.Location.Y + (currentlMouse.Y - _LastMouseMove.Y) < (ScrollPanel.Height - this.Height) * -1)
                        {
                            //if it is, set it to the bottom
                            ScrollPanel.Location = new Point(ScrollPanel.Location.X, (ScrollPanel.Height - this.Height) * -1);
                        }
                        else
                        {
                            //other wise move it based off the change in mouse positon
                            ScrollPanel.Location = new Point(ScrollPanel.Location.X, ScrollPanel.Location.Y + (currentlMouse.Y - _LastMouseMove.Y));
                        }
                    }
                    //re-calculate the scroll bar based off our new main position
                    //ScrollPanel.Location = new Point(ScrollPanel.Location.X, Math.Max(0, ScrollPanel.Location.Y));
                    CalculateScrollBarV();
                    CalculateScrollBarH();
                }
                //record the current mouse as the last mouse
                _LastMouseMove = GetMousePosition();
            }
        }

        private void ScrollAblePanelControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (_IsMouseDown)
            {
                //finished scrolling
                _IsMouseDown = false;
            }
        }

        private void ScrollPanel_MouseEnter(object sender, EventArgs e)
        {
            //If the mouse enters the panel, change the cursor so the user knows they can scroll
            Cursor = System.Windows.Forms.Cursors.Hand;
        }

        private void ScrollPanel_MouseLeave(object sender, EventArgs e)
        {
            //restore the cursor to defualt when out of the panel
            Cursor = System.Windows.Forms.Cursors.Default;
        }

        //SCROLL BAR EVENTS
        private void ScrollAtY_MouseDown(object sender, MouseEventArgs e)
        {
            //if the mouse is not already pressed and scrolling isnt not disabled
            if ((!_IsMouseVDown) && (!_DisableScrollingV))
            {
                //set the mouse to down (Scroll Bars)
                _IsMouseVDown = true;
                //Record the position of the mouse down
                _LastMouseMove = GetMousePosition();
            }
        }

        private void ScrollAtX_MouseDown(object sender, MouseEventArgs e)
        {
            //if the mouse is not already pressed and scrolling isnt not disabled
            if ((!_IsMouseHDown) && (!_DisableScrollingH))
            {
                //set the mouse to down (Scroll Bars)
                _IsMouseHDown = true;
                //Record the position of the mouse down
                _LastMouseMove = GetMousePosition();
            }
        }

        public void RefreshScroll()
        {
            bool wasH = _IsMouseHDown;
            bool wasV = _IsMouseVDown;
            _IsMouseVDown = true;
            _IsMouseHDown = true;
            Point p = GetMousePosition();
            ScrollAtY_MouseMove(this, new MouseEventArgs(MouseButtons.None, 1, p.X, p.Y, 0));
            ScrollAtX_MouseMove(this, new MouseEventArgs(MouseButtons.None, 1, p.X, p.Y, 0));
            _IsMouseVDown = wasV;
            _IsMouseHDown = wasH;
        }

        public void MoveStep(int deltaX, int deltaY)
        {
            ScrollAtY.Location = new Point(ScrollAtY.Location.X, ScrollAtY.Location.Y + deltaY);
            CalculateScrollPanelV();

            ScrollAtX.Location = new Point(ScrollAtX.Location.X+deltaX, ScrollAtX.Location.Y);
            CalculateScrollPanelH();            
        }

        private Point ScrollAtX_Location = new Point(0, 0);
        private Point ScrollAtX_LocationLast = new Point(0, 0);
        private void ScrollAtX_MouseMove(object sender, MouseEventArgs e)
        {
            ScrollAtX_LocationLast = ScrollAtX.Location;
            int mouseW = 0;
            if (e.Delta > 0)
            {
                mouseW = -ScrollMin;
            }
            else if (e.Delta < 0)
            {
                mouseW = ScrollMin;
            }
            //mouseW = -e.Delta;
            //if the mouse is down, aka we're scrolling with the bar
            if (_IsMouseHDown)
            {
                //grab the current mouse position and see if it has moved
                Point currentlMouse = GetMousePosition();
                if (_LastMouseMove.IsEmpty)
                {
                    _LastMouseMove = currentlMouse;
                }
                if (true || _LastMouseMove != currentlMouse)
                {
                    //check if it would be going over the top of the scroll bar
                    if (ScrollAtX.Location.X + (currentlMouse.X - _LastMouseMove.X) + mouseW < ScrollContainterH.Location.X)
                    {
                        //if it is, set it to the top
                        ScrollAtX_Location = new Point(ScrollContainterH.Location.X, ScrollAtX.Location.Y);
                    }
                    else
                    {
                        //check if it would be going past the bottom of the scroll bar
                        if (ScrollAtX.Location.X + (currentlMouse.X - _LastMouseMove.X + mouseW) > ScrollHorizontalMaxValue)
                        {
                            //if it is, set it to the bottom
                            ScrollAtX_Location = new Point(ScrollHorizontalMaxValue, ScrollAtX.Location.Y);
                        }
                        else
                        {
                            //other wise move it based off the change in mouse positon
                            if (mouseW == 0)
                            {
                                ScrollAtX_Location = new Point(ScrollAtX.Location.X + (currentlMouse.X - _LastMouseMove.X), ScrollAtX.Location.Y);
                            }
                            else
                            {
                                ScrollAtX_Location = new Point(ScrollAtX.Location.X + mouseW, ScrollAtX.Location.Y);
                            }
                        }
                    }
                    ScrollAtX_Location = new Point(Math.Min(Math.Max(0, ScrollAtX_Location.X), ScrollHorizontalMaxValue), ScrollAtX_Location.Y);
                    //other wise move it based off the change in mouse positon
                    if (mouseW == 0)
                    {
                        ScrollAtX.Location = ScrollAtX_Location;
                        ScrollAtX_LocationLast = ScrollAtX.Location;
                        CalculateScrollPanelH();
                    }
                    else
                    {
                        int step = ScrollWhellStep;
                        int i = 0;
                        if (mouseW > 0)
                        {
                            //Вниз
                            //step = step;
                            for (i = ScrollAtX.Location.X; i < ScrollAtX_Location.X; i = i + step)
                            {
                                if (ScrollAtX_LocationLast.X > i)
                                {
                                    break;
                                }
                                ScrollAtX.Location = new Point(i, ScrollAtX_Location.Y);
                                ScrollAtX_LocationLast = ScrollAtX.Location;
                                CalculateScrollPanelH();
                                Application.DoEvents();
                            }
                            if (ScrollAtX_Location.X < i + step)
                            {
                                ScrollAtX.Location = ScrollAtX_Location;
                                ScrollAtX_LocationLast = ScrollAtX.Location;
                                CalculateScrollPanelH();
                            }
                        }
                        else if (mouseW < 0)
                        {
                            //Вверх
                            step = -step;
                            for (i = ScrollAtX.Location.X; i > ScrollAtX_Location.X; i = i + step)
                            {
                                if (ScrollAtX_LocationLast.X < i)
                                {
                                    break;
                                }
                                ScrollAtX.Location = new Point(i, ScrollAtX_Location.Y);
                                ScrollAtX_LocationLast = ScrollAtX.Location;
                                CalculateScrollPanelH();
                                Application.DoEvents();
                            }

                            if (ScrollAtX_Location.X < -step)
                            {
                                ScrollAtX.Location = ScrollAtX_Location;
                                ScrollAtX_LocationLast = ScrollAtX.Location;
                                CalculateScrollPanelH();
                            }
                        }
                    }
                }
                //record the current mouse as the last mouse
                _LastMouseMove = GetMousePosition();

                ScrollAtX.Location = new Point(Math.Max(ScrollHorizontalMinValueWithMargin, Math.Min(ScrollHorizontalMaxValueWithMargin, ScrollAtX.Location.X)), ScrollAtX.Location.Y);

                ScrollingHEnd?.Invoke(this, new EventArgs());
            }            
        }

        public void ScrollHorizontalMin()
        {
            //ScrollAtX.Location = new Point(ScrollHorizontalMinValue, ScrollAtX.Location.Y);
            //CalculateScrollPanelH();
            //ScrollAtX.Location = new Point(ScrollHorizontalMinValue, ScrollAtX.Location.Y);
            //CalculateScrollPanelH();
            ScrollPanel.Left = 0;
            CalculateScrollBarH();
        }
        public void ScrollHorizontalMax()
        {
            //ScrollAtX.Location = new Point(ScrollHorizontalMaxValue, ScrollAtX.Location.Y);
            //CalculateScrollPanelH();
            //ScrollAtX.Location = new Point(ScrollHorizontalMaxValue, ScrollAtX.Location.Y);
            //CalculateScrollPanelH();
            ScrollPanel.Left = -ScrollPanel.Width + this.Width;
            CalculateScrollBarH();
        }
        public void ScrollHorizontalMinWithMargin()
        {
            ScrollHorizontalMin();
            ScrollAtX.Location = new Point(ScrollHorizontalMinValueWithMargin+1, ScrollAtX.Location.Y);
        }
        public void ScrollHorizontalMaxWithMargin()
        {
            ScrollHorizontalMax();
            ScrollAtX.Location = new Point(ScrollHorizontalMaxValueWithMargin-1, ScrollAtX.Location.Y);
        }
        private void ScrollAtX_MouseUp(object sender, MouseEventArgs e)
        {
            if (_IsMouseHDown)
            {
                //finished scrolling
                _IsMouseHDown = false;
            }
        }

        private Point ScrollAtY_Location = new Point(0, 0);
        private Point ScrollAtY_LocationLast = new Point(0, 0);
        private void ScrollAtY_MouseMove(object sender, MouseEventArgs e)
        {
            ScrollAtY_LocationLast = ScrollAtY.Location;
            int mouseW = 0;
            if (e.Delta > 0)
            {
                mouseW = -ScrollMin;
            }else if (e.Delta < 0)
            { 
                mouseW = ScrollMin;
            }
            //mouseW = -e.Delta;
            //if the mouse is down, aka we're scrolling with the bar
            if (_IsMouseVDown)
            {
                //ScrollAtY_Location = ScrollAtY.Location;
                //grab the current mouse position and see if it has moved
                Point currentlMouse = GetMousePosition();
                if (_LastMouseMove.IsEmpty)
                {
                    _LastMouseMove = currentlMouse;
                }
                if (true || _LastMouseMove != currentlMouse)
                {
                    //check if it would be going over the top of the scroll bar
                    if (ScrollAtY.Location.Y + (currentlMouse.Y - _LastMouseMove.Y) + mouseW <= ScrollContainterV.Location.Y)
                    {
                        //if it is, set it to the top
                        ScrollAtY_Location = new Point(ScrollAtY.Location.X, ScrollContainterV.Location.Y);
                    }
                    else
                    {
                        //check if it would be going past the bottom of the scroll bar
                        if (ScrollAtY.Location.Y + (currentlMouse.Y - _LastMouseMove.Y + mouseW) > ScrollVerticalMaxValue)
                        {
                            //if it is, set it to the bottom
                            ScrollAtY_Location = new Point(ScrollAtY.Location.X, ScrollVerticalMaxValue);
                        }
                        else
                        {
                            //other wise move it based off the change in mouse positon
                            if (mouseW == 0)
                            {
                                ScrollAtY_Location = new Point(ScrollAtY.Location.X, ScrollAtY.Location.Y + (currentlMouse.Y - _LastMouseMove.Y));
                            }
                            else
                            {
                                ScrollAtY_Location = new Point(ScrollAtY.Location.X, ScrollAtY.Location.Y + mouseW);
                            }
                        }
                    }
                    ScrollAtY_Location = new Point(ScrollAtY_Location.X, Math.Min(Math.Max(0, ScrollAtY_Location.Y), ScrollVerticalMaxValue));
                    //other wise move it based off the change in mouse positon
                    if (mouseW == 0)
                    {
                        ScrollAtY.Location = ScrollAtY_Location;
                        ScrollAtY_LocationLast = ScrollAtY.Location;
                        CalculateScrollPanelV();
                    }
                    else
                    {
                        int step = ScrollWhellStep;
                        int i = 0;                        
                        if (mouseW > 0)
                        {
                            //Вниз
                            //step = step;
                            for (i = ScrollAtY.Location.Y; i < ScrollAtY_Location.Y; i = i + step)
                            {
                                if (ScrollAtY_LocationLast.Y > i)
                                {
                                    break;
                                }
                                ScrollAtY.Location = new Point(ScrollAtY_Location.X, i);
                                ScrollAtY_LocationLast = ScrollAtY.Location;
                                CalculateScrollPanelV();
                                Application.DoEvents();
                            }
                            if (ScrollAtY_Location.Y < i + step)
                            {
                                ScrollAtY.Location = ScrollAtY_Location;
                                ScrollAtY_LocationLast = ScrollAtY.Location;
                                CalculateScrollPanelV();
                            }
                        }
                        else if (mouseW < 0)
                        {
                            //Вверх
                            step = -step;
                            for (i = ScrollAtY.Location.Y; i > ScrollAtY_Location.Y; i = i + step)
                            {
                                if (ScrollAtY_LocationLast.Y < i)
                                {
                                    break;
                                }
                                ScrollAtY.Location = new Point(ScrollAtY_Location.X, i);
                                ScrollAtY_LocationLast = ScrollAtY.Location;
                                CalculateScrollPanelV();
                                Application.DoEvents();
                            }

                            if (ScrollAtY_Location.Y < -step)
                            {
                                ScrollAtY.Location = ScrollAtY_Location;
                                ScrollAtY_LocationLast = ScrollAtY.Location;
                                CalculateScrollPanelV();
                            }
                        }
                    }


                }
                //record the current mouse as the last mouse
                _LastMouseMove = GetMousePosition();

                ScrollAtY.Top = Math.Max(ScrollVerticalMinValueWithMargin, Math.Min(ScrollVerticalMaxValueWithMargin, ScrollAtY.Location.Y));

                ScrollingVEnd?.Invoke(this, new EventArgs());
            }                                      
        }
        public void ScrollVerticalMin()
        {
            //ScrollAtY.Location = new Point(ScrollAtY.Location.X, ScrollVerticalMinValue);
            //CalculateScrollPanelV();
            //ScrollAtY.Location = new Point(ScrollAtY.Location.X, ScrollVerticalMinValue);
            //CalculateScrollPanelV();
            ScrollPanel.Top = 0;
            CalculateScrollBarV();
        }
        public void ScrollVerticalMax()
        {
            //ScrollAtY.Location = new Point(ScrollAtY.Location.X, ScrollVerticalMaxValue);
            //CalculateScrollPanelV();
            //ScrollAtY.Location = new Point(ScrollAtY.Location.X, ScrollVerticalMaxValue);
            //CalculateScrollPanelV();
            ScrollPanel.Top = -ScrollPanel.Height + this.Height;
            CalculateScrollBarV();
        }
        public void ScrollVerticalMinWithMargin()
        {
            //ScrollAtY.Location = new Point(ScrollAtY.Location.X, ScrollVerticalMinValueWithMargin + 1);
            //CalculateScrollPanelV();
            //ScrollAtY.Location = new Point(ScrollAtY.Location.X, ScrollVerticalMinValueWithMargin + 1);
            //CalculateScrollPanelV();

            ScrollVerticalMin();
            ScrollAtY.Location = new Point(ScrollAtY.Location.X, ScrollVerticalMinValueWithMargin);// +1);
        }
        public void ScrollVerticalMaxWithMargin()
        {
            //ScrollAtY.Location = new Point(ScrollAtY.Location.X, ScrollVerticalMaxValueWithMargin - 1);
            //CalculateScrollPanelV();
            //ScrollAtY.Location = new Point(ScrollAtY.Location.X, ScrollVerticalMaxValueWithMargin - 1);
            //CalculateScrollPanelV();

            ScrollVerticalMax();
            ScrollAtY.Location = new Point(ScrollAtY.Location.X, ScrollVerticalMaxValueWithMargin);// -1);
        }
        private void ScrollAtY_MouseUp(object sender, MouseEventArgs e)
        {
            //ScrollBarIsRound = !ScrollBarIsRound;
            if (_IsMouseVDown)
            {
                //finished scrolling
                _IsMouseVDown = false;
            }
        }

        public void ScrollAblePanelControl_MouseWheel(object sender, MouseEventArgs e)
        {
            int mouseW = 0;
            if (e.Delta > 0)
            {
                mouseW = ScrollMin;
            }
            else if (e.Delta < 0)
            {
                mouseW = -ScrollMin;
            }
            if (Session.Session.ControlPressed)
            {
                if (false || !_DisableScrollingH)
                {
                    //_IsMouseHDown = true;
                    //ScrollAtX_MouseMove(sender, e);
                    //_IsMouseHDown = false;

                    bool bMin = ScrollHorizontalMinNowPrev;
                    bool bMax = ScrollHorizontalMaxNowPrev;
                    ScrollPanel.Left =Math.Min(0,Math.Max(-GetScrollHorizontalAria(), ScrollPanel.Left + mouseW));
                    CalculateScrollBarH();
                    ScrollHorizontalMinNowPrev = ScrollHorizontalMinNow;
                    ScrollHorizontalMaxNowPrev = ScrollHorizontalMaxNow;

                    if (mouseW < 0)
                    {
                        if (bMax)
                        {
                            ScrollingHEnd?.Invoke(this, new EventArgs());
                        }
                    }
                    else
                    {
                        if (bMin)
                        {
                            ScrollingHEnd?.Invoke(this, new EventArgs());
                        }
                    }

                    //ScrollAtY.Left = Math.Max(ScrollHorizontalMinValueWithMargin + 1, Math.Min(ScrollHorizontalMaxValueWithMargin - 1, ScrollAtY.Left));
                }
            }
            else
            {
                if (false || !_DisableScrollingV)
                {
                    //_IsMouseVDown = true;
                    //ScrollAtY_MouseMove(sender, e);
                    //_IsMouseVDown = false;
                    bool bMin = ScrollVerticalMinNowPrev;
                    bool bMax = ScrollVerticalMaxNowPrev;

                    ScrollPanel.Top  = Math.Min(0, Math.Max(-GetScrollVerticalAria(), ScrollPanel.Top + mouseW));
                    CalculateScrollBarV();
                    ScrollVerticalMinNowPrev = ScrollVerticalMinNow;
                    ScrollVerticalMaxNowPrev = ScrollVerticalMaxNow;
                   
                    if (mouseW < 0)
                    {
                        if (bMax)
                        {
                            ScrollingVEnd?.Invoke(this, new EventArgs());
                        }
                    }
                    else
                    {
                        if (bMin)
                        {
                            ScrollingVEnd?.Invoke(this, new EventArgs());
                        }
                    }

                    //ScrollAtY.Top = Math.Max(ScrollVerticalMinValueWithMargin + 1, Math.Min(ScrollVerticalMaxValueWithMargin - 1, ScrollAtY.Top));

                    //ScrollVerticalMinNowPrev = false;
                    //ScrollVerticalMaxNowPrev = false;
                }
            }
        }

        public void RefreshScrollAblePanelControl()
        {
            //if (nowDisableScrollingV)
            //{
            //Set the X Position of the Scroll Bar
            ScrollContainterV.Left = this.Width - ScrollContainterV.Width - Otstup.Width;           
            ScrollAtY.Left = this.Width - ScrollContainterV.Width - Otstup.Width;
            //Set the Height of the scroll bar
            ScrollContainterV.Top = Otstup.Height;
            ScrollContainterV.Height = this.Height - (ScrollContainterV.Location.Y * 2);

            //If the panel created to scroll is too small, turn off scrolling and disable the scroll bars.
            if (ScrollPanel.Height <= this.Height)
            {
                DisableScrollingV = true;
                ScrollPanel.Top = 0;
                CalculateScrollBarV();
                CalculateScrollPanelV();

                ScrollAtY.Enabled = false;
                ScrollContainterV.Enabled = false;

                ScrollAtY.Visible = false;
                ScrollContainterV.Visible = false;

                DisableScrollingV = true;
            }
            else
            {
                DisableScrollingV = false;
                //otherwise allow scrolling
                ScrollAtY.Enabled = true;
                ScrollContainterV.Enabled = true;

                ScrollAtY.Visible = true && AutoVerticalScroll;
                ScrollContainterV.Visible = true && AutoVerticalScroll;

                CalculateScrollBarV();
                CalculateScrollPanelV();

                DisableScrollingV = false;
            }
            //}

            //if (nowDisableScrollingH)
            //{
            //Set the Y Position of the Scroll Bar
            ScrollContainterH.Top = this.Height - ScrollContainterH.Height - Otstup.Width;
            ScrollAtX.Top = this.Height - ScrollContainterH.Height - Otstup.Width + 1;
            //Set the Height of the scroll bar
            //ScrollContainter.Height = this.Height - (ScrollContainter.Location.Y * 2);
            ScrollContainterH.Left = Otstup.Height;
            ScrollContainterH.Width = this.Width - (ScrollContainterH.Location.X * 2);

            //If the panel created to scroll is too small, turn off scrolling and disable the scroll bars.
            if (ScrollPanel.Width <= this.Width)
            {
                DisableScrollingH = true;
                ScrollPanel.Left = 0;
                CalculateScrollBarH();
                CalculateScrollPanelH();

                ScrollAtX.Enabled = false;
                ScrollContainterH.Enabled = false;

                ScrollAtX.Visible = false;
                ScrollContainterH.Visible = false;

                DisableScrollingH = true;
            }
            else
            {
                DisableScrollingH = false;
                //otherwise allow scrolling
                ScrollAtX.Enabled = true;
                ScrollContainterH.Enabled = true;

                ScrollAtX.Visible = true && AutoHorizontalScroll;
                ScrollContainterH.Visible = true && AutoHorizontalScroll;

                CalculateScrollBarH();
                CalculateScrollPanelH();

                DisableScrollingH = false;
            }
            //}

            int dx = 0;
            int dy = 0;
            if (ScrollAtY.Visible)
            {
                dx = this.Width - ScrollContainterV.Left;
            }
            if (ScrollAtX.Visible)
            {
                dy = this.Height - ScrollContainterH.Top;
            }
            if (AutoSetMinSize)
            {
                EditablePanel.MinimumSize = new Size(this.Width - dx, this.Height - dy);
            }
        }

        public void ScrollAblePanelControl_SizeChanged(object sender, EventArgs e)
        {
            RefreshScrollAblePanelControl();
        }

        private void ScrollContainterH_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int mouseW = 0;
            if (e.X < ScrollAtX.Left)
            {
                mouseW = -ScrollMax;
            }
            else
            {
                mouseW = ScrollMax;
            }
            //mouseW = -e.Delta;
            //if the mouse is down, aka we're scrolling with the bar
            if (true || _IsMouseHDown)
            {
                //grab the current mouse position and see if it has moved
                Point currentlMouse = GetMousePosition();
                if (true || _LastMouseMove != currentlMouse)
                {
                    //check if it would be going over the top of the scroll bar
                    if (ScrollAtX.Location.X + (currentlMouse.X - _LastMouseMove.X) + mouseW < ScrollContainterH.Location.X)
                    {
                        //if it is, set it to the top
                        ScrollAtX.Location = new Point(ScrollContainterH.Location.X, ScrollAtX.Location.Y);
                    }
                    else
                    {
                        //check if it would be going past the bottom of the scroll bar
                        if (ScrollAtX.Location.X + (currentlMouse.X - _LastMouseMove.X + mouseW) > ScrollHorizontalMaxValue)
                        {
                            //if it is, set it to the bottom
                            ScrollAtX.Location = new Point(ScrollHorizontalMaxValue, ScrollAtX.Location.Y);
                        }
                        else
                        {
                            //other wise move it based off the change in mouse positon
                            if (mouseW == 0)
                            {
                                ScrollAtX.Location = new Point(ScrollAtX.Location.X + (currentlMouse.X - _LastMouseMove.X), ScrollAtX.Location.Y);
                            }
                            else
                            {
                                ScrollAtX.Location = new Point(ScrollAtX.Location.X + mouseW, ScrollAtX.Location.Y);
                            }
                        }
                    }
                    ScrollAtX.Location = new Point(Math.Max(0, ScrollAtX.Location.X), ScrollAtX.Location.Y);
                    //other wise move it based off the change in mouse positon
                    CalculateScrollPanelH();
                }
                //record the current mouse as the last mouse
                _LastMouseMove = GetMousePosition();
            }
        }

        private void ScrollContainterV_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int mouseW = 0;
            if (e.Y < ScrollAtY.Top)
            {
                mouseW = -ScrollMax;
            }
            else
            {
                mouseW = ScrollMax;
            }
            //mouseW = -e.Delta;
            //if the mouse is down, aka we're scrolling with the bar
            if (true || _IsMouseVDown)
            {
                //grab the current mouse position and see if it has moved
                Point currentlMouse = GetMousePosition();
                if (true || _LastMouseMove != currentlMouse)
                {
                    //check if it would be going over the top of the scroll bar
                    if (ScrollAtY.Location.Y + (currentlMouse.Y - _LastMouseMove.Y) + mouseW < ScrollContainterV.Location.Y)
                    {
                        //if it is, set it to the top
                        ScrollAtY.Location = new Point(ScrollAtY.Location.X, ScrollContainterV.Location.Y);
                    }
                    else
                    {
                        //check if it would be going past the bottom of the scroll bar
                        if (ScrollAtY.Location.Y + (currentlMouse.Y - _LastMouseMove.Y + mouseW) > ScrollVerticalMaxValue)
                        {
                            //if it is, set it to the bottom
                            ScrollAtY.Location = new Point(ScrollAtY.Location.X, ScrollVerticalMaxValue);
                        }
                        else
                        {
                            //other wise move it based off the change in mouse positon
                            if (mouseW == 0)
                            {
                                ScrollAtY.Location = new Point(ScrollAtY.Location.X, ScrollAtY.Location.Y + (currentlMouse.Y - _LastMouseMove.Y));
                            }
                            else
                            {
                                ScrollAtY.Location = new Point(ScrollAtY.Location.X, ScrollAtY.Location.Y + mouseW);
                            }
                        }
                    }
                    ScrollAtY.Location = new Point(ScrollAtY.Location.X, Math.Max(0, ScrollAtY.Location.Y));
                    //other wise move it based off the change in mouse positon
                    CalculateScrollPanelV();
                }
                //record the current mouse as the last mouse
                _LastMouseMove = GetMousePosition();
            }
        }

        private void treeView1_MouseWheel(object sender, MouseEventArgs e)
        {
            ScrollAblePanelControl_MouseWheel(sender, e);
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            RefreshTreeViewHeight();
        }

        public void RefreshTreeViewHeight()
        {
            if (this.TreeView != null)
            {
                int count = 0;
                CallRecursiveTreeView(this.TreeView, ref count);
                this.TreeView.Height = this.TreeView.ItemHeight * count + 4;
            }
        }

        private void PrintRecursiveTreeView(TreeNode treeNode, ref int count)
        {
            count++;  
            if (treeNode.IsExpanded)
            {
                foreach (TreeNode tn in treeNode.Nodes)
                {
                    PrintRecursiveTreeView(tn, ref count);
                }
            }
            
        }

        private void CallRecursiveTreeView(TreeView treeView, ref int count)
        {            
            TreeNodeCollection nodes = treeView.Nodes;
            foreach (TreeNode n in nodes)
            {
                PrintRecursiveTreeView(n, ref count);
            }
        }
    }


    //The Desinger Class
    internal class ScrollAblePanelDesigner : System.Windows.Forms.Design.ParentControlDesigner
    {
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);

            if (this.Control is ScrollAblePanelControl)
            {
                this.EnableDesignMode((
                    //get the EditablePanel attritubute from the class ScrollAblePanelControl
                   (ScrollAblePanelControl)this.Control).EditablePanel, "EditablePanel");
            }
        }
    }
}
