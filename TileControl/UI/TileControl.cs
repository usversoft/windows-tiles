using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net;
using BaseClasses;
using Newtonsoft.Json;

namespace TilesControl
{
    public partial class TileControl : UserControl
    {
        public enum CellOrientationType
        {
            Square,
            Horizontal,
            Vertical
        }

        public TileControl()
        {
            InitializeComponent();

            this.ParentChanged += TileControl_ParentChanged;
            this.MouseDown += TileControl_MouseDown;
            this.MouseMove += TileControl_MouseMove;
            this.MouseUp += TileControl_MouseUp;
            this.LocationChanged += TileControl_LocationChanged;
            this.MouseDoubleClick += TileControl_MouseDoubleClick;
            this.DoubleClick += TileControl_DoubleClick;
            this.Click += TileControl_Click;
            this.MouseEnter += TileControl_MouseEnter;
            this.MouseLeave += TileControl_MouseLeave;
            //this.MouseWheel += TileControl_MouseWheel;

            this.pictureBox1.MouseDown += TileControl_MouseDown;
            this.pictureBox1.MouseMove += TileControl_MouseMove;
            this.pictureBox1.MouseUp += TileControl_MouseUp;
            this.pictureBox1.MouseDoubleClick += TileControl_MouseDoubleClick;
            this.pictureBox1.DoubleClick += TileControl_DoubleClick;
            this.pictureBox1.Click += TileControl_Click;
            this.pictureBox1.MouseEnter += PictureBox1_MouseEnter;
            this.pictureBox1.MouseEnter += TileControl_MouseEnter;
            this.pictureBox1.MouseLeave += PictureBox1_MouseLeave;
            this.pictureBox1.MouseLeave += TileControl_MouseLeave;
            //this.pictureBox1.MouseLeave += TileControl_MouseLeave;
            //this.pictureBox1.MouseWheel += PictureBox1_MouseWheel;
            this.pictureBox1.MouseWheel += TileControl_MouseWheel;

            this.pictureBox2.MouseDown += TileControl_MouseDown;
            this.pictureBox2.MouseMove += TileControl_MouseMove;
            this.pictureBox2.MouseUp += TileControl_MouseUp;
            this.pictureBox2.MouseDoubleClick += TileControl_MouseDoubleClick;
            this.pictureBox2.DoubleClick += TileControl_DoubleClick;
            this.pictureBox2.Click += TileControl_Click;
            this.pictureBox2.MouseEnter += PictureBox2_MouseEnter;
            this.pictureBox2.MouseEnter += TileControl_MouseEnter;
            this.pictureBox2.MouseLeave += PictureBox2_MouseLeave;
            this.pictureBox2.MouseLeave += TileControl_MouseLeave;
            this.pictureBox2.MouseWheel += PictureBox2_MouseWheel;
            this.pictureBox2.Paint += PictureBox2_Paint;

            DoubleBuffered = true;
            ATextBox = new TextBox();
            ATextBox.Visible = false;
            ATextBox.Multiline = true;
            ATextBox.VisibleChanged += ATextBox_VisibleChanged;
            ATextBox.LostFocus += ATextBox_LostFocus;
            ATextBox.KeyDown += ATextBox_KeyDown;
            this.Controls.Add(ATextBox);
            IsEditText = false;
            IsEditLink = false;
            UseContextMenu = false;
        }

        private void PictureBox2_Paint(object sender, PaintEventArgs e)
        {
            if (IsSpecial && SpecilaType != SpecilaType.View && !MouseIn)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(128, 0, 0, 0)), pictureBox2.ClientRectangle);
            }
        }

        private void TileControl_ParentChanged(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                if (typeof(TilesControl) == this.Parent.GetType())
                {
                    this.TilesControl = (TilesControl)this.Parent;
                }
            }
        }

        private void PictureBox2_MouseWheel(object sender, MouseEventArgs e)
        {
            SetScrollAblePanelDisableScrollingV(false);
        }

        private void PictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Tile.BackColor;

            nowAnimation = false;
            if (!nowAnimation)
            {
                MouseEnterRectTextNow = false;
                if (Tile.OnlyImage() || (Tile.IsSpecial && Tile.OnlyText()))
                {
                    DoAnimation(1, true);
                }
                else
                {
                    if (Tile.OnlyText())
                    {
                        //DoAnimation(1, true);
                    }
                    else
                    {
                        DoAnimation(1, false);
                    }
                }
            }
        }

        private void PictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Tile.BackColorMouse;
            Tile.Link = null;

            nowAnimation = false;
            if (Tile.OnlyImage() || (Tile.IsSpecial && Tile.OnlyText()))
            {
                DoAnimation(1, false);
            }
            else if (Tile.OnlyText())
            {

            }
            else
            {
                DoAnimation(1, true);
            }
        }

        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {

        }

        private void PictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Tile.BackColor;

            nowAnimation = false;
            if (!nowAnimation)
            {
                MouseEnterRectTextNow = false;
                if (Tile.OnlyText())
                {
                    DoAnimation(1, true);
                }
                //DoAnimation(1, false);
            }
        }

        private void PictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Tile.BackColorMouse;

            //nowAnimation = false;
            // MouseEnterRectTextNow = true;
            //DoAnimation(1, true);
            if (Tile.OnlyText())
            {
                DoAnimation(1, false);
                SetScrollAblePanelDisableScrollingV(Tile.canScrollText);
            }
            else
            {
                MouseEnterRectTextNow = true;
            }

        }

        private void SetScrollAblePanelDisableScrollingV(bool value)
        {
            if (!TilesControl.EnableScrollTileText)
            {
                return;
            }
            if (this.TilesControl.ScrollAblePanelControl != null)
            {
                this.TilesControl.ScrollAblePanelControl.DisableScrollingV = value;
            }
            //if (!TilesControl.GetNowMouseWheelTimeout() && !value)
            //{
            //    try
            //    {
            //        this.TilesControl.ScrollAblePanelControl.DisableScrollingV = value;
            //    }
            //    catch
            //    {

            //    }
            //}
            //else
            //{
            //    try
            //    {
            //        this.TilesControl.ScrollAblePanelControl.DisableScrollingV = value;
            //    }
            //    catch
            //    {

            //    }
            //}
        }

        private void TileControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!TilesControl.EnableScrollTileText)
            {
                return;
            }
            if (this.Tile.canScrollText && !TilesControl.GetNowMouseWheelTimeout())
            {
                if (e.Delta > 0)
                {
                    if (this.Tile.beginY < 0)
                    {
                        this.Tile.beginY = this.Tile.beginY + 5;
                        SetScrollAblePanelDisableScrollingV(true);
                        TilesControl.NowMouseWheelStop = true;
                    }
                    else
                    {
                        this.Tile.beginY = 0;
                        SetScrollAblePanelDisableScrollingV(false);
                        TilesControl.NowMouseWheelStop = false;
                    }
                }
                else
                {
                    if (-this.Tile.beginYMax < this.Tile.beginY)
                    {
                        this.Tile.beginY = this.Tile.beginY - 5;
                        SetScrollAblePanelDisableScrollingV(true);
                        TilesControl.NowMouseWheelStop = true;
                    }
                    else
                    {
                        this.Tile.beginY = -this.Tile.beginYMax;
                        SetScrollAblePanelDisableScrollingV(false);
                        TilesControl.NowMouseWheelStop = false;
                    }
                }
                Size cellsCount = GetCellsCount();
                Size cellSize = TilesControl.GetCellSize();
                this.Image1 = Tile.GetTile1(cellsCount, cellSize, TilesControl.MarginTile, TilesControl.PaddingTile, MouseIn, CellOrientation);
                pictureBox1.Image = this.Image1;
                //RefreshTile();
                MouseEnterNow = true;
            }
            else
            {
                SetScrollAblePanelDisableScrollingV(false);
            }
        }
        private bool MouseEnterNow = false;
        private void TileControl_MouseLeave(object sender, EventArgs e)
        {
            MouseIn = false;
            SetScrollAblePanelDisableScrollingV(false);
            MouseEnterNow = false;
            if (this.Tile.beginY != 0)
            {
                this.Tile.beginY = 0;
                RefreshTile();
            }
            //this.BackgroundImage = this.ImageMouseNot;
            //DoAnimationOld(2);
        }

        private void TileControl_MouseEnter(object sender, EventArgs e)
        {
            MouseIn = true;
            //try
            //{
            //    ((ScrollAblePanel.ScrollAblePanelControl)this.TilesControl.Parent.Parent).DisableScrollingV = Tile.canScrollText;
            //}
            //catch
            //{
            //
            //}
            //MouseEnterNow = true;
            //this.BackgroundImage = this.ImageMouse;
        }

        [DefaultValue(false)]
        public bool MouseIn
        {
            get => mouseIn;
            set
            {
                if (mouseIn != value)
                {
                    mouseIn = value;
                    if (Tile != null && Tile.BackColor != Tile.BackColorMouse)
                    {
                        if (mouseIn)
                        {
                            //pictureBox1.BackColor = Tile.BackColorMouse;
                            //pictureBox2.BackColor = Tile.BackColorMouse;
                        }
                        else
                        {
                            //pictureBox1.BackColor = Tile.BackColor;
                            //pictureBox2.BackColor = Tile.BackColor;
                        }
                        //RefreshTile();
                    }
                }
            }
        }

        [DefaultValue(false)]
        public bool MouseEnterRectTextNow = false;
        [DefaultValue(null)]
        public Image ImageMouseNot { get; set; }
        [DefaultValue(null)]
        public Image ImageMouse { get; set; }
        [DefaultValue(null)]
        public Image Image1 { get; set; }
        [DefaultValue(null)]
        public Image Image2 { get; set; }
        [DefaultValue(null)]
        public Image Image2Zoom { get; set; }
        [DefaultValue(null)]
        public Image ImageBackground { get; set; }
        //public Image ImageMouseText { get; set; }

        public CellOrientationType CellOrientation
        {
            get
            {
                if (TilesControl.CellAsSquare)
                {
                    Size cc = GetCellsCount();
                    if (cc.Width == cc.Height)
                    {
                        return CellOrientationType.Square;
                    }
                    else if (cc.Width > cc.Height)
                    {
                        return CellOrientationType.Horizontal;
                    }
                    else
                    {
                        return CellOrientationType.Vertical;
                    }
                }
                else
                {
                    if (this.Width == this.Height)
                    {
                        return CellOrientationType.Square;
                    }
                    else if (this.Width > this.Height)
                    {
                        return CellOrientationType.Horizontal;
                    }
                    else
                    {
                        return CellOrientationType.Vertical;
                    }
                }
            }
        }

        private bool useContextMenu;
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
            }
        }
        private bool isEditText;
        private bool isEditLink;
        [DefaultValue(false)]
        public bool SpecialStandartAction { get; set; }

        [DefaultValue(false)]
        public bool IsSpecial
        {
            get
            {
                if (Tile == null)
                {
                    return false;
                }
                else
                {
                    return Tile.IsSpecial;
                }
            }
            set
            {
                if (Tile == null)
                {
                    //this.ContextMenuStrip = contextMenuStrip1;
                }
                else
                {
                    Tile.IsSpecial = value;
                    //if (Tile.IsSpecial)
                    //{
                    //this.ContextMenuStrip = null;
                    //}
                    //else
                    //{
                    //this.ContextMenuStrip = contextMenuStrip1;
                    //}
                }

            }
        }
        [DefaultValue(SpecilaType.None)]
        public SpecilaType SpecilaType
        {
            get
            {
                if (Tile == null)
                {
                    return SpecilaType.None;
                }
                else
                {
                    return Tile.SpecilaType;
                }
            }
            set
            {
                if (Tile != null)
                {
                    Tile.SpecilaType = value;
                }
            }
        }
        [DefaultValue(false)]
        public bool IsEditText
        {
            get => isEditText;
            set
            {
                if (isEditLink || IsSpecial)
                {
                    isEditText = false;
                }
                else
                {
                    if (isEditText != value)
                    {
                        isEditText = value;
                        ATextBox.Visible = isEditText;
                        if (isEditText)
                        {
                            ATextBox.Text = Tile.Text;
                            ATextBox.BringToFront();
                        }
                        else
                        {
                            Tile.Text = ATextBox.Text;
                        }
                    }
                }
            }
        }
        [DefaultValue(false)]
        public bool IsEditLink
        {
            get => isEditLink;
            set
            {
                if (isEditText || IsSpecial)
                {
                    isEditLink = false;
                }
                else
                {
                    if (isEditLink != value)
                    {
                        isEditLink = value;
                        ATextBox.Visible = isEditLink;
                        if (isEditLink)
                        {
                            ATextBox.Text = Tile.Link;
                            ATextBox.BringToFront();
                        }
                        else
                        {
                            Tile.Link = ATextBox.Text;
                        }
                    }
                }
            }
        }

        private TextBox ATextBox;

        public TilesControl tilesControl;
        [DefaultValue(null)]
        public TilesControl TilesControl
        {
            get
            {
                return tilesControl;
            }
            set
            {
                tilesControl = value;
                RefreshTile();
            }
        }
        private Tile tile;
        [DefaultValue(null)]
        public Tile Tile
        {
            get
            {
                return tile;
            }
            set
            {
                tile = value;
                if (tile != null)
                {
                    tile.LinkChanged += Tile_LinkChanged;
                    tile.CellsCountChanged += Tile_CellsCountChanged;
                    pictureBox1.BackColor = tile.BackColor;
                    pictureBox2.BackColor = tile.BackColor;
                    if (tile.IsSpecial && tile.SpecilaType != SpecilaType.View)
                    {
                        this.Cursor = Cursors.Hand;
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
                RefreshTile();
            }
        }

        public Size GetCellsCount()
        {
            if (Tile != null)
            {
                if (this.TilesControl != null)
                {
                    if (this.TilesControl.UseRow)
                    {
                        int index = this.TilesControl.Tiles.IndexOf(this);
                        if (index != -1)
                        {
                            while (index >= this.TilesControl.RowTemplate.Count)
                            {
                                index -= (this.TilesControl.RowTemplate.Count);
                            }
                            return this.TilesControl.RowTemplate[index];
                        }
                    }
                }
                return Tile.CellsCount;
            }
            return new Size(1, 1);
        }

        private void ATextBox_LostFocus(object sender, EventArgs e)
        {
            IsEditText = false;
            IsEditLink = false;
            Tile.IsEdit = false;
            RefreshTile();
        }
        private void ATextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                IsEditText = false;
                IsEditLink = false;
                Tile.IsEdit = false;
                RefreshTile();
            }
        }
        private void ATextBox_VisibleChanged(object sender, EventArgs e)
        {
            if (Tile != null)
            {
                if (ATextBox.Visible)
                {
                    ATextBox.Location = new Point(0, 0);
                    ATextBox.Size = this.Size;
                    ATextBox.Font = Tile.Font;
                    this.BackColor = Tile.BackColor;
                    ATextBox.BackColor = Tile.BackColor;
                    ATextBox.ForeColor = Tile.TextForeColor;
                    ATextBox.Focus();
                    Tile.IsEdit = true;
                    pictureBox1.Visible = false;
                    pictureBox2.Visible = false;
                }
                else
                {
                    Tile.IsEdit = false;
                    pictureBox1.Visible = !Tile.OnlyImage();
                    pictureBox2.Visible = true;
                }
            }
        }

        private void TileControl_LocationChanged(object sender, System.EventArgs e)
        {
            if (Tile != null)
            {
                Tile.Location = this.Location;
            }
            //if (this.TilesControl.TileDragAndDrop != null)
            //{
            //    if (!this.TilesControl.TileDragAndDrop.Equals(this))
            //    {
            //        return;
            //    }
            //    RefreshDragAndDrop(false);
            //}
        }

        private void Tile_LinkChanged(object sender, EventArgs e)
        {
            if (Tile.Link != null && Tile.Link != "")
            {
                this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void Tile_CellsCountChanged(object sender, EventArgs e)
        {
            RefreshTile();
        }

        public Size RefreshTileSize()
        {
            this.Size = Tile.GetTileSize(GetCellsCount(), TilesControl.GetCellSize(), TilesControl.MarginTile);
            return this.Size;
        }
        public void RefreshTile()
        {
            if (TilesControl != null && Tile != null)
            {
                RefreshTileSize();
                if (this.Image1 != null)
                {
                    this.Image1.Dispose();
                }
                if (this.Image2 != null)
                {
                    this.Image2.Dispose();
                }
                if (this.Image2Zoom != null)
                {
                    this.Image2Zoom.Dispose();
                }
                if (this.ImageBackground != null)
                {
                    this.ImageBackground.Dispose();
                }
                Size cellsCount = GetCellsCount();
                Size cellSize = TilesControl.GetCellSize();
                this.Image1 = Tile.GetTile1(cellsCount, cellSize, TilesControl.MarginTile, TilesControl.PaddingTile, MouseIn, CellOrientation);
                if ((Tile.IsSpecial && Tile.OnlyText()))
                {
                    this.Image2 = Tile.GetTile1(cellsCount, cellSize, TilesControl.MarginTile, TilesControl.PaddingTile, MouseIn, CellOrientation);
                }
                else
                {
                    this.Image2 = Tile.GetTile2(cellsCount, cellSize, TilesControl.MarginTile, TilesControl.PaddingTile, MouseIn);
                }
                if (Tile.IsSpecial)
                {
                    if (Tile.OnlyText())
                    {
                        this.Image2Zoom = Tile.GetTile1(cellsCount, new Size((int)(cellSize.Width * 1), (int)(cellSize.Height * 1)), TilesControl.MarginTile, TilesControl.PaddingTile, MouseIn, CellOrientation, (float)1.2);
                    }
                    else
                    {
                        this.Image2Zoom = Tile.GetTile2(cellsCount, new Size((int)(cellSize.Width * 1.3), (int)(cellSize.Height * 1.3)), TilesControl.MarginTile, TilesControl.PaddingTile, MouseIn);
                    }
                }
                else
                {
                    this.Image2Zoom = Tile.GetTile2(cellsCount, new Size((int)(cellSize.Width * 1.3), (int)(cellSize.Height * 1.3)), TilesControl.MarginTile, TilesControl.PaddingTile, MouseIn);
                }
                this.ImageBackground = Tile.GetTileBackground(cellsCount, cellSize, TilesControl.MarginTile, TilesControl.PaddingTile, MouseIn, CellOrientation);

                if (SpecilaType == SpecilaType.View)
                {
                    //if (this.BackgroundImage != null)
                    //{
                    //    this.BackgroundImage.Dispose();
                    //}
                    //this.BackgroundImage = this.Image1;
                }

                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;

                if (MouseIn)
                {
                    pictureBox1.BackColor = Tile.BackColorMouse;
                    pictureBox2.BackColor = Tile.BackColorMouse;
                }
                else
                {
                    pictureBox1.BackColor = Tile.BackColor;
                    pictureBox2.BackColor = Tile.BackColor;
                }
                pictureBox1.BackgroundImage = this.ImageBackground;
                this.pictureBox1.Image = this.Image1;

                //pictureBox1.BackColor = Color.Transparent;               
                if ((Tile.OnlyImage() || (Tile.IsSpecial && Tile.OnlyText())) && MouseIn && TilesControl.UseAnimationZoom)
                {
                    this.pictureBox2.Image = null;
                    this.pictureBox2.BackgroundImage = this.Image2Zoom;
                    this.pictureBox2.Refresh();
                }
                else
                {
                    this.pictureBox2.BackgroundImage = null;
                    this.pictureBox2.Image = this.Image2;
                    this.pictureBox2.Refresh();
                }

                //if (Tile.OnlyText() && Tile.IsSpecial && MouseIn)
                //{
                //    this.pictureBox1.Image = null;
                //    this.pictureBox1.BackgroundImage = this.Image2Zoom;
                //    this.pictureBox1.Refresh();
                //}
                //else
                //{
                //    pictureBox1.BackgroundImage = this.ImageBackground;
                //    this.pictureBox1.Image = this.Image1;
                //    this.pictureBox1.Refresh();
                //}

                this.pictureBox1.Dock = DockStyle.None;
                this.pictureBox2.Dock = DockStyle.None;

                if ((this.Tile.beginY == 0 && !MouseEnterRectTextNow) || TilesControl.TileDragAndDrop != null)
                {
                    this.pictureBox1.Location = new Point(0, 0);
                    if (Tile.Image == null)
                    {
                        //this.pictureBox1.Location = new Point(0, this.Height / 4);
                        //this.pictureBox1.Size = new Size(this.Width, this.Height / 2);
                        this.pictureBox1.Location = new Point(0, 0);
                        this.pictureBox1.Size = new Size(this.Width, this.Height);
                    }
                    else
                    {
                        if (CellOrientation == CellOrientationType.Horizontal)
                        {
                            this.pictureBox1.Size = new Size(this.Width / 2, this.Height);
                        }
                        else
                        {
                            this.pictureBox1.Size = new Size(this.Width, this.Height / 2);
                        }
                    }

                    this.pictureBox1.Visible = !(Tile.OnlyImage() || (Tile.IsSpecial && Tile.SpecilaType != SpecilaType.View));
                    if (Tile.SpecilaType == SpecilaType.View)
                    {
                        this.pictureBox1.Size = new Size(this.Width, this.Height);
                    }
                    this.pictureBox1.BringToFront();

                    this.pictureBox2.Location = new Point(0, 0);
                    this.pictureBox2.Size = this.Image2.Size;
                }
            }
        }

        public void RefreshTileOld()
        {
            if (TilesControl != null)
            {
                RefreshTileSize();
                if (this.ImageMouseNot != null)
                {
                    this.ImageMouseNot.Dispose();
                }
                if (this.ImageMouse != null)
                {
                    this.ImageMouse.Dispose();
                }
                this.ImageMouseNot = Tile.GetTile(GetCellsCount(), TilesControl.GetCellSize(), TilesControl.MarginTile, TilesControl.PaddingTile, CellOrientation);
                this.ImageMouse = Tile.GetTileMouse(GetCellsCount(), TilesControl.GetCellSize(), TilesControl.MarginTile, TilesControl.PaddingTile);
                this.pictureBox1.Image = this.ImageMouse;
                this.pictureBox2.Image = this.ImageMouseNot;
                //this.ImageMouseText = Tile.GetTile(GetCellsCount(), TilesControl.GetCellSize(), TilesControl.Margin, TilesControl.Padding);
                if (MouseEnterNow)
                {
                    if (MouseEnterRectTextNow && Tile.Image != null)
                    {
                        this.BackgroundImage = this.ImageMouseNot;
                        this.pictureBox2.BringToFront();
                        pictureBoxNum = 2;
                        //DoAnimation(2);
                    }
                    else
                    {
                        this.BackgroundImage = this.ImageMouse;
                        this.pictureBox1.BringToFront();
                        pictureBoxNum = 1;
                        //DoAnimation(1);
                    }
                }
                else
                {
                    this.BackgroundImage = this.ImageMouseNot;
                    this.pictureBox2.BringToFront();
                    pictureBoxNum = 2;
                    //DoAnimation(2);
                }
            }
        }

        public void SetVisible(bool value)
        {
            this.Visible = value;
            this.Tile.Visible = value;
        }

        private void smallSquareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Tile.CellsTypeValue = Tile.CellsType.minSquare;
            this.RefreshTile();
            this.TilesControl.RefreshTilesLocation();
        }

        private void bigSquareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Tile.CellsTypeValue = Tile.CellsType.bigSquare;
            this.RefreshTile();
            this.TilesControl.RefreshTilesLocation();
        }

        private void smallRectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Tile.CellsTypeValue = Tile.CellsType.minRectangle;
            this.RefreshTile();
            this.TilesControl.RefreshTilesLocation();
        }

        private void bigRectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Tile.CellsTypeValue = Tile.CellsType.bigRectangle;
            this.RefreshTile();
            this.TilesControl.RefreshTilesLocation();
        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SizeForm frm = new SizeForm())
            {
                frm.CellsCount = this.Tile.CellsCount;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.Tile.CellsCount = frm.CellsCount;
                    this.RefreshTile();
                    this.TilesControl.RefreshTilesLocation();
                }
            }
        }
        private void editTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.IsEditText = true;
            //using (EditTextForm frm = new EditTextForm())
            //{
            //    frm.TextEditable = this.Tile.Text;
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        this.Tile.Text = frm.TextEditable;
            //        this.RefreshTile();
            //        this.TilesControl.RefreshTilesLocation();
            //    }
            //}
        }

        private void editLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.IsEditLink = true;
            //using (EditTextForm frm = new EditTextForm())
            //{
            //    frm.TextEditable = this.Tile.Link;
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        this.Tile.Link = frm.TextEditable;
            //        this.RefreshTile();
            //        this.TilesControl.RefreshTilesLocation();
            //    }
            //}
        }

        private void levelUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = this.TilesControl.Tiles.IndexOf(this);
            if (index > 0)
            {
                this.TilesControl.Tiles.RemoveAt(index);
                this.TilesControl.Tiles.Insert(index - 1, this);
                this.TilesControl.Controls.SetChildIndex(this, index - 1);

                this.TilesControl.RefreshTilesLocation();
            }
        }

        private void levelDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = this.TilesControl.Tiles.IndexOf(this);
            if (index < this.TilesControl.Tiles.Count - 1)
            {
                this.TilesControl.Tiles.RemoveAt(index);
                this.TilesControl.Tiles.Insert(index + 1, this);
                this.TilesControl.Controls.SetChildIndex(this, index + 1);

                this.TilesControl.RefreshTilesLocation();
            }
        }

        private bool nowDragAndDrop = false;
        private AnimationType animationType;

        public event EventHandler TileControlClick;
        private void TileControl_Click(object sender, System.EventArgs e)
        {
            this.Focus();
            TileControlClick?.Invoke(this, new System.EventArgs());
        }

        private void TileControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //if (e.Clicks == 1)
            //{
            //    if (!nowDragAndDrop)
            //    {
            //        if (Tile != null && Tile.Link != null && Tile.Link != "")
            //        {
            //            try
            //            {
            //                Process.Start(Tile.Link);
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            }
            //        }
            //    }
            //}
            if (TilesControl != null && TilesControl.UseMove)
            {
                if (!IsSpecial)
                {
                    this.TilesControl.indexFirst = -1;
                    this.TilesControl.indexPrev = -1;
                    LocationOld = this.Location;
                    this.TilesControl.TileLocationBegin = e.Location;
                    this.TilesControl.TileDragAndDrop = this;
                }
            }
        }
        Point LocationOld;
        private void TileControl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!nowDragAndDrop ||
                (this.TilesControl.TileDragAndDrop != null
                && this.TilesControl.TileDragAndDrop.Location == this.TilesControl.TileDragAndDrop.LocationOld))
            //(nowDragAndDrop && this.TilesControl.TileLocationBegin == MouseMovePoint))
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (Tile != null && Tile.Link != null && Tile.Link != "")
                    {
                        try
                        {
                            Process.Start(Tile.Link);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else if (Tile != null) // && Tile.Text != null && Tile.Text != "")
                    {
                        if (!this.Tile.IsSpecial)
                        {
                            TilesControl.SelectTileControlLoadData(this);
                        }
                    }
                }
            }
            if (this.TilesControl.TileDragAndDrop != null)
            {
                this.TilesControl.TileDragAndDrop = null;
                this.TilesControl.TileLocationBegin = new Point();
                nowDragAndDrop = false;
                RefreshDragAndDrop(false);
                this.TilesControl.RefreshTilesLocation(true);
            }
        }

        public bool PointInRectangle(Rectangle rect, Point point)
        {
            if ((point.X > rect.Left && point.X < rect.Right
               && point.Y > rect.Top && point.Y < rect.Bottom))
            {
                return true;
            }
            return false;
        }

        Point MouseMovePoint;
        private void TileControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MouseMovePoint = e.Location;
            if (this.TilesControl.TileDragAndDrop != null)
            {
                if (!this.TilesControl.TileDragAndDrop.Equals(this))
                {
                    return;
                }
                if (!nowDragAndDrop)
                {
                    this.BringToFront();
                }
                nowDragAndDrop = true;
                this.Location = new Point(this.Location.X - this.TilesControl.TileLocationBegin.X + e.X, this.Location.Y - this.TilesControl.TileLocationBegin.Y + e.Y);

                if (!TilesControl.NowSetNewLocations)
                {
                    RefreshDragAndDrop(false);
                }
            }
            else
            {
                if (!nowAnimation && !Tile.OnlyImage())
                {
                    if (Tile.OnlyText())
                    {
                        if (pictureBox1.Size == this.Size)
                        {
                            Tile.RectTextForMouse = new Rectangle(0, this.Height / 4, this.Width, this.Height / 2);
                        }
                        else
                        {
                            Tile.RectTextForMouse = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);
                        }
                    }
                    else
                    {
                        if (CellOrientation == CellOrientationType.Horizontal)
                        {
                            Tile.RectTextForMouse = new Rectangle(0, 0, this.Width / 2, this.Height);
                        }
                        else
                        {
                            Tile.RectTextForMouse = new Rectangle(0, 0, this.Width, this.Height / 2);
                        }
                    }

                    if (PointInRectangle(Tile.RectTextForMouse, e.Location))
                    {
                        SetScrollAblePanelDisableScrollingV(Tile.canScrollText);
                        if (!MouseEnterRectTextNow)
                        {
                            MouseEnterRectTextNow = true;
                            if (Tile.Image == null)
                            {
                                MouseEnterNow = true;
                                //this.BackgroundImage = this.ImageMouse;
                                ////DoAnimationOld(1);
                                //DoAnimation(1, false);
                            }
                            else
                            {
                                //TileControl_MouseLeave(this, new System.EventArgs());
                                bool haveLink = false;
                                foreach (Teg teg in Tile.tegs)
                                {
                                    if (teg.Href != "" && teg.Href != null)
                                    {
                                        haveLink = true;
                                        break;
                                    }
                                }
                                if (Tile.canScrollText || haveLink)
                                {
                                    MouseEnterNow = true;
                                    ////this.BackgroundImage = this.ImageMouseNot;
                                    //DoAnimationOld(2);
                                    DoAnimation(1, false);
                                }
                                else
                                {
                                    MouseEnterNow = true;
                                    ///this.BackgroundImage = this.ImageMouse;
                                    //DoAnimationOld(1);
                                    DoAnimation(1, false);
                                }
                                //if (CellOrientation == CellOrientationType.Horizontal)
                                //{
                                //    //DoAnimation(1, pictureBox1.Width != 0);
                                //}
                                //else
                                //{
                                //    //DoAnimation(1, pictureBox1.Height != 0);
                                //}
                            }
                        }
                        bool linkIsSet = false;
                        foreach (Teg teg in Tile.tegs)
                        {
                            if (teg.TegName == "a")
                            {
                                if (PointInRectangle(teg.Rect, e.Location))
                                {
                                    linkIsSet = true;
                                    Tile.Link = teg.Href;
                                    break;
                                }
                            }
                        }
                        if (!linkIsSet)
                        {
                            Tile.Link = null;
                        }
                    }
                    else
                    {
                        if (MouseEnterRectTextNow)
                        {
                            MouseEnterRectTextNow = false;
                            if (Tile.Image == null)
                            {
                                //DoAnimation(1, true);
                                ////TileControl_MouseLeave(this, new System.EventArgs());
                            }
                            else
                            {
                                MouseEnterNow = true;
                                //// this.BackgroundImage = this.ImageMouse;
                                ////DoAnimationOld(1);
                                DoAnimation(1, true);
                            }
                        }
                    }
                }
            }
        }

        private void RefreshDragAndDrop(bool animationNo)
        {
            if (TilesControl.TileDragAndDrop == null ||
                !TilesControl.TileDragAndDrop.Equals(this))
            {
                return;
            }
            int index = -1;
            int indexOld = -1;
            if (this.TilesControl.indexFirst == -1)
            {
                this.TilesControl.indexFirst = this.TilesControl.Tiles.IndexOf(this);
            }
            if (this.TilesControl.UseRow)
            {
                indexOld = this.TilesControl.Tiles.IndexOf(this);
                index = indexOld;
                for (int i = 0; i < this.TilesControl.Tiles.Count; i++)
                {
                    TileControl tc = this.TilesControl.Tiles[i];
                    if (tc.Equals(this) || tc.IsSpecial)
                    {
                        continue;
                    }
                    Rectangle rect = new Rectangle(tc.Location, tc.Size);
                    //this.TilesControl.label1.BringToFront();
                    //this.TilesControl.label1.Text = rect.ToString();
                    if (PointInRectangle(rect, this.Location))
                    {
                        index = i;
                        //this.TilesControl.label1.Text += "=true+" + (index).ToString() + "!=" + indexOld;
                        break;
                    }
                }
                //if (this.TilesControl.indexFirst == -1)
                //{
                //    this.TilesControl.indexFirst = this.TilesControl.Tiles.IndexOf(this);
                //}
                //indexOld = this.TilesControl.indexFirst;// this.TilesControl.Tiles.IndexOf(this);
                //int x = 0;
                //int y = 0;
                //int wasYcellsCountX = 0;
                //int cellsCountX = this.TilesControl.GetCellsCountX();
                //int wasYcellsCountY = 0;
                //for (int i = 0; i< this.TilesControl.RowTemplate.Count; i++)
                //{

                //    x = (this.TilesControl.GetCellSize().Width *(wasYcellsCountX - wasYcellsCountY*cellsCountX));
                //    if ((int)Math.Floor((float)wasYcellsCountX /(wasYcellsCountY + 1)) > cellsCountX)
                //    {
                //        wasYcellsCountY++;

                //    }
                //    wasYcellsCountY = ((int)Math.Floor((float)(wasYcellsCountX) / (cellsCountX)));
                //    y = this.TilesControl.GetCellSize().Height * wasYcellsCountY;

                //    int i2 = i;
                //    if (indexOld < i)
                //    {
                //        //i2--;
                //    }

                //    wasYcellsCountX += this.TilesControl.RowTemplate[i].Width;

                //    Rectangle rect = new Rectangle(new Point(x, y), Tile.GetTileSize(this.TilesControl.RowTemplate[i], this.TilesControl.GetCellSize(), TilesControl.Margin));
                //    if (i <= 3)
                //    {
                //        this.TilesControl.label1.Text = rect.ToString();
                //    }
                //    this.TilesControl.label1.BringToFront();
                //    if (this.Location.X>rect.Left && this.Location.X<rect.Right )
                //    {
                //        if (this.Location.Y > rect.Top && this.Location.Y < rect.Bottom)
                //        {
                //             index = i;
                //            this.TilesControl.label1.Text += "=true+"+(index).ToString() + "!=" + indexOld;

                //            break;
                //        }
                //    }
                //    this.TilesControl.label1.Text += "=false";
                //}
            }
            else
            {
                indexOld = this.TilesControl.Tiles.IndexOf(this);
                Size tileSize = this.TilesControl.GetCellSize();
                int xIndex = (int)Math.Round((float)this.Location.X / tileSize.Width);
                int yIndex = (int)Math.Round((float)this.Location.Y / tileSize.Height);
                index = (yIndex) * this.TilesControl.GetCellsCountX() + (xIndex);
                index = Math.Max(0, Math.Min(this.TilesControl.Tiles.Count, index));
                for (int i = 0; i < index; i++)
                {
                    if (this.TilesControl.Tiles[i].GetCellsCount().Width != 1)
                    {
                        if (this.TilesControl.Tiles.Count != index)
                        {
                            index -= this.TilesControl.Tiles[i].GetCellsCount().Width - 1;
                        }
                        index = Math.Max(0, Math.Min(this.TilesControl.Tiles.Count, index));
                    }
                    //if (this.TilesControl.Tiles[i].tile.CellsCount.Height != 1)
                    //{
                    //    if (this.TilesControl.Tiles.Count != index)
                    //    {
                    //        index -= (this.TilesControl.Tiles[i].tile.CellsCount.Height - 1) * this.TilesControl.Tiles[i].tile.CellsCount.Width;
                    //    }
                    //    index = Math.Max(0, Math.Min(this.TilesControl.Tiles.Count, index));
                    //}
                }
            }
            index = Math.Max(0, Math.Min(this.TilesControl.Tiles.Count, index));
            if (indexOld != index)// && indexOld!=-1)
            {
                if (!this.TilesControl.UseRow)
                {
                    //var buf = this.TilesControl.Tiles[this.TilesControl.Tiles.IndexOf(this)];
                    //this.TilesControl.Tiles[this.TilesControl.Tiles.IndexOf(this)] = this.TilesControl.Tiles[index];
                    //this.TilesControl.Tiles[index] = buf;

                    this.TilesControl.Tiles.Remove(this);
                    if (indexOld < index)
                    {
                        index--;
                    }
                    this.TilesControl.Tiles.Insert(index, this);
                }
                else
                {
                    var buf = this.TilesControl.Tiles[this.TilesControl.Tiles.IndexOf(this)];
                    this.TilesControl.Tiles[this.TilesControl.Tiles.IndexOf(this)] = this.TilesControl.Tiles[index];
                    //this.RefreshTile();
                    this.TilesControl.Tiles[index] = buf;
                    this.TilesControl.Tiles[index].BringToFront();
                    //buf.RefreshTile();

                    if (this.TilesControl.indexPrev != -1)
                    {
                        var buf2 = this.TilesControl.Tiles[this.TilesControl.indexFirst];
                        this.TilesControl.Tiles[this.TilesControl.indexFirst] = this.TilesControl.Tiles[this.TilesControl.indexPrev];
                        //this.TilesControl.Tiles[this.TilesControl.indexFirst].RefreshTile();
                        this.TilesControl.Tiles[this.TilesControl.indexPrev] = buf2;
                        this.TilesControl.Tiles[this.TilesControl.indexPrev].BringToFront();
                        // buf2.RefreshTile();

                        var buf3 = this.TilesControl.Tiles[index];
                        this.TilesControl.Tiles[index] = this.TilesControl.Tiles[this.TilesControl.indexFirst];
                        // this.TilesControl.Tiles[index].RefreshTile();
                        this.TilesControl.Tiles[index] = buf3;
                        this.TilesControl.Tiles[index].BringToFront();
                        //buf3.RefreshTile();
                    }

                    this.TilesControl.indexPrev = index;

                }
                this.TilesControl.RefreshTilesLocation(animationNo);
            }

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            smallSquareToolStripMenuItem.Visible = !TilesControl.UseRow;
            smallRectangleToolStripMenuItem.Visible = !TilesControl.UseRow;
            bigSquareToolStripMenuItem.Visible = !TilesControl.UseRow;
            bigRectangleToolStripMenuItem.Visible = !TilesControl.UseRow;
            customToolStripMenuItem.Visible = !TilesControl.UseRow;
            toolStripSeparator1.Visible = !TilesControl.UseRow;
        }

        private void TileControl_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsEditText = true;
            }
            else if (e.Button == MouseButtons.Middle)
            {
                IsEditLink = true;
            }
        }

        private void TileControl_DoubleClick(object sender, System.EventArgs e)
        {
            //IsEditText = true;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteTile();
        }

        public void DeleteTile(bool needRefresh = true)
        {
            if (this.TilesControl != null)
            {
                if (DeleteTileToServer(TilesControl.WebSite))
                {
                    this.TilesControl.Tiles.Remove(this);
                    if (needRefresh)
                    {
                        this.TilesControl.RefreshTiles();
                    }
                    this.TilesControl.Controls.Remove(this);
                }
            }
            this.Dispose();
        }

        public bool DeleteTileToServer(string webSite)
        {
            WebClient webClient = new WebClient();
            bool result = DeleteTileToServer(webSite, webClient, ref TilesControl.Information);
            webClient.Dispose();
            return result;
        }
        public bool DeleteTileToServer(string webSite, WebClient webClient, ref BaseClasses.ActionProvider information)
        {
            bool result = true;

            Tile tile = this.Tile;
            string filename = tile.ImageFileName;

            string _prev_information = information.InfoMessages.Last();
            information.AddInfo("Удаление картинок плиток с сервера...");
            information.ErrorWhatMaybeAdd = new ErrorInfo();
            information.ErrorWhatMaybeAdd.ErrorIcon = MessageBoxIcon.Error;
            information.ErrorWhatMaybeAdd.ErrorCaption = "Ошибка!";
            information.ErrorWhatMaybeAdd.ErrorMessage = "Ошибка при удалении картинок плиток с сервера.";

            try
            {
                if (tile.Image == null || filename == null || filename == "")
                {
                    result = true;
                }
                else
                {
                    string[] img_size_directories = { "max", "original" };
                    foreach (string img_size_directory in img_size_directories)
                    {
                        string localPath = Path.Combine(this.TilesControl.GetImageDirectory(img_size_directory), filename);

                    }

                    string uri = webSite + "/free-for-program/delete-img";
                    uri += "/" + Session.Session.auth_session_string + "/" + filename;

                    webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";// "binary / octet - stream";// "multipart/form-data; boundary=something";// "application/x-www-form-urlencoded";
                    webClient.Headers[HttpRequestHeader.ContentLanguage] = "ru-RU";

                    var parameters = new System.Collections.Specialized.NameValueCollection();
                    byte[] responseBytes = webClient.UploadValues(uri, "POST", parameters);
                    string response = Encoding.UTF8.GetString(responseBytes);
                    //MessageBox.Show(response);
                    resp responseJson = JsonConvert.DeserializeObject<resp>(response);
                    if (responseJson.error)
                    {
                        information.AddError(null, responseJson.message, "Error!", MessageBoxIcon.Error);
                        result = false;
                    }
                    Session.Session.auth_session_string = responseJson.auth_session_string;
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

        int pictureBoxNum = -1;
        bool nowAnimation = false;
        public void DoAnimationOld(int pictureBoxNumNow)
        {
            Control panel = null;// (i == 0) ? this.UserPanel1 : this.UserPanel2;// null;
            if (pictureBoxNumNow == 1)
            {
                panel = this.pictureBox1;
                //pictureBoxNumNow = 2;
            }
            else if (pictureBoxNumNow == 2)
            {
                panel = this.pictureBox2;
                //pictureBoxNumNow = 1;
            }
            //if(pictureBoxNum == -1)
            //{
            //    pictureBoxNum = pictureBoxNumNow;
            //}

            if (panel != null)
            {
                if (!nowAnimation && !IsEditText && !IsEditLink && (false || pictureBoxNumNow != pictureBoxNum))
                {
                    //if (TilesControl.UseAnimator)
                    //{
                    //    foreach (Control ctrl in this.Controls)//pnLeft.Controls)
                    //        if (ctrl.Visible && ctrl != panel)
                    //            if (ctrl == this.pictureBox1 || ctrl == this.pictureBox2)// predefinedList2 || ctrl == pg || ctrl == tbCode)
                    //                ctrl.Hide();
                    //}

                    panel.BringToFront();
                    pictureBoxNum = pictureBoxNumNow;
                    nowAnimation = true;
                    //if (TilesControl.UseAnimator)
                    //{
                    //    TilesControl.animator.ShowSync(panel, true, this.Animation);//.VertSlide);
                    //}

                    ((PictureBox)panel).Image = (Image)((PictureBox)panel).Image.Clone();
                    int n = this.Height / 2;
                    if (CellOrientation == CellOrientationType.Horizontal && Tile.Image != null)
                    {
                        n = this.Width / 2;
                    }
                    bool onlyImage = Tile.OnlyImage();
                    if (onlyImage)
                    {
                        n = 50;
                    }
                    int animationDelay = 500;
                    int animationStep = animationDelay / n;
                    if (pictureBoxNumNow == 2)
                    {
                        n = 0;
                    }
                    if (onlyImage)
                    {
                        n = 1;
                    }

                    bool animationBreak = false;
                    while (!animationBreak)
                    {
                        DateTime dateStart = DateTime.Now;
                        Image imgResult = null;
                        Image imgBegin = null;
                        if (pictureBoxNumNow == 1)
                        {
                            imgResult = this.ImageMouse;
                            imgBegin = this.ImageMouseNot;
                            ((PictureBox)panel).Image.Dispose();
                            ((PictureBox)panel).Image = Tile.GetTileAnim(ref animationBreak, n, imgResult, imgBegin, true, Tile.Image == null, onlyImage, CellOrientation);
                            n++;
                        }
                        else if (pictureBoxNumNow == 2)
                        {
                            imgResult = this.ImageMouse;
                            imgBegin = this.ImageMouseNot;
                            ((PictureBox)panel).Image.Dispose();
                            ((PictureBox)panel).Image = Tile.GetTileAnim(ref animationBreak, n, imgResult, imgBegin, false, Tile.Image == null, onlyImage, CellOrientation);
                            n++;
                        }
                        DateTime dateFinish = DateTime.Now;
                        TimeSpan timeResult = dateFinish - dateStart;
                        if (timeResult.Milliseconds < animationStep)
                        {
                            System.Threading.Thread.Sleep(animationStep - timeResult.Milliseconds);
                        }
                        ((PictureBox)panel).Refresh();
                        //Application.DoEvents();
                    }
                    if (pictureBoxNumNow == 1)
                    {
                        ((PictureBox)panel).Image = this.ImageMouse;
                    }
                    else if (pictureBoxNumNow == 2)
                    {
                        ((PictureBox)panel).Image = this.ImageMouseNot;
                    }
                    Application.DoEvents();
                    nowAnimation = false;
                }
            }
        }

        public enum AnimationType
        {
            Vertical,
            Horizontal,
            Zoom,
            VerticalAndHeight
        }

        private bool reversePrev = false;
        private bool mouseIn;
        public void DoAnimation(int pictureBoxNumNow, bool reverse)
        {
            TilesControl.DoAnimation(this, reverse);
        }
        public void DoAnimation2(int pictureBoxNumNow, bool reverse)
        {
            if (!TilesControl.RefreshTilesNow && !nowAnimation && !IsEditText && !IsEditLink && !((Tile.Text == null || Tile.Text == "") && Tile.Image == null))
            {
                pictureBoxNum = pictureBoxNumNow;
                nowAnimation = true;

                AnimationType animationType = AnimationType.Vertical;
                int n = this.Height / 2;
                if (CellOrientation == CellOrientationType.Horizontal && Tile.Image != null)
                {
                    animationType = AnimationType.Horizontal;
                    n = this.Width / 2;
                }
                if (Tile.OnlyImage())
                {
                    animationType = AnimationType.Zoom;
                    n = (int)(this.Height * 0.2);
                    pictureBox2.BackgroundImage = Image2Zoom;
                    pictureBox2.Image = null;
                    pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
                }
                if (Tile.OnlyText())
                {
                    animationType = AnimationType.VerticalAndHeight;
                    n = this.Height / 4;
                }
                int animationDelay = 200;
                int animationStep = animationDelay / n;

                if (!TilesControl.GetNowMouseWheelTimeout() && (TilesControl.TileDragAndDrop == null))
                {
                    //reverse = this.Size != pictureBox1.Size;
                    if (TilesControl != null && TilesControl.UseAnimation)
                    {
                        for (int i = 1; i <= n; i++)
                        {
                            if (!nowAnimation)
                            {
                                break;
                            }
                            DateTime dateStart = DateTime.Now;
                            if (pictureBoxNumNow == 1)
                            {
                                if (reverse)
                                {
                                    switch (animationType)
                                    {
                                        case AnimationType.Vertical:
                                            pictureBox1.Height = Math.Max(0, pictureBox1.Height - 1);
                                            break;
                                        case AnimationType.Horizontal:
                                            pictureBox1.Width = Math.Max(0, pictureBox1.Width - 1);
                                            break;
                                        case AnimationType.VerticalAndHeight:
                                            pictureBox1.Top = Math.Min(this.Height / 4, pictureBox1.Top + 1);
                                            pictureBox1.Height = Math.Max(this.Height / 2, pictureBox1.Height - 2);
                                            break;
                                        case AnimationType.Zoom:
                                            int k = this.Height / 4 - i;
                                            pictureBox2.Location = new Point(-(int)((k / 2) * ((float)this.Width / this.Height)), -(int)((k / 2)));
                                            pictureBox2.Size = new Size(this.Width + (int)((k) * ((float)this.Width / this.Height)), this.Height + k);
                                            //pictureBox2.Scale(1 + (float)((float)k / n * 0.2));
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (animationType)
                                    {
                                        case AnimationType.Vertical:
                                            pictureBox1.Height = Math.Min(this.Height / 2, pictureBox1.Height + 1);
                                            break;
                                        case AnimationType.Horizontal:
                                            pictureBox1.Width = Math.Min(this.Width / 2, pictureBox1.Width + 1);
                                            break;
                                        case AnimationType.VerticalAndHeight:
                                            pictureBox1.Top = Math.Max(0, pictureBox1.Top - 1);
                                            pictureBox1.Height = Math.Min(this.Height, pictureBox1.Height + 2);
                                            break;
                                        case AnimationType.Zoom:
                                            int k = i;
                                            pictureBox2.Location = new Point(-(int)((k / 2) * ((float)this.Width / this.Height)), -(int)((k / 2)));
                                            pictureBox2.Size = new Size(this.Width + (int)((k) * ((float)this.Width / this.Height)), this.Height + k);
                                            //pictureBox2.Scale(1 + (float)((float)k / n * 0.2));
                                            break;
                                    }
                                }
                            }
                            DateTime dateFinish = DateTime.Now;
                            TimeSpan timeResult = dateFinish - dateStart;
                            if (timeResult.Milliseconds < animationStep)
                            {
                                System.Threading.Thread.Sleep(animationStep - timeResult.Milliseconds);
                            }
                            pictureBox2.Refresh();
                            pictureBox1.Refresh();
                            //Application.DoEvents();
                            if (!nowAnimation)
                            {
                                break;
                            }
                        }
                    }
                }

                if (!TilesControl.GetNowMouseWheelTimeout())
                {
                    if (reverse)
                    {
                        switch (animationType)
                        {
                            case AnimationType.Vertical:
                                pictureBox1.Height = 0;
                                break;
                            case AnimationType.Horizontal:
                                pictureBox1.Width = 0;
                                break;
                            case AnimationType.VerticalAndHeight:
                                pictureBox1.Height = this.Height / 2;
                                pictureBox1.Top = this.Height / 4;
                                break;
                            case AnimationType.Zoom:
                                pictureBox2.Location = new Point(0, 0);
                                pictureBox2.Size = this.Size;
                                //pictureBox2.Scale(1);
                                pictureBox2.Image = Image2;
                                pictureBox2.BackgroundImage = null;
                                break;
                        }
                    }
                    else
                    {

                        switch (animationType)
                        {
                            case AnimationType.Vertical:
                                pictureBox1.Height = this.Height / 2;
                                break;
                            case AnimationType.Horizontal:
                                pictureBox1.Width = this.Width / 2;
                                break;
                            case AnimationType.VerticalAndHeight:
                                pictureBox1.Height = this.Height;
                                pictureBox1.Top = 0;
                                break;
                            case AnimationType.Zoom:
                                pictureBox2.Location = new Point(-(int)(this.Width * 0.1), -(int)(this.Height * 0.1));
                                pictureBox2.Size = new Size((int)(this.Width * 1.2), (int)(this.Height * 1.2));
                                //pictureBox2.Scale((float)1.2);
                                break;

                        }
                    }
                }
                else
                {
                    switch (animationType)
                    {
                        case AnimationType.Vertical:
                            pictureBox1.Height = this.Height / 2;
                            break;
                        case AnimationType.Horizontal:
                            pictureBox1.Width = this.Width / 2;
                            break;
                        case AnimationType.VerticalAndHeight:
                            pictureBox1.Height = this.Height / 2;
                            pictureBox1.Top = this.Height / 4;
                            break;
                        case AnimationType.Zoom:
                            pictureBox2.Location = new Point(0, 0);
                            pictureBox2.Size = this.Size;
                            //pictureBox2.Scale(1);
                            pictureBox2.Image = Image2;
                            pictureBox2.BackgroundImage = null;
                            break;
                    }
                }
                reversePrev = reverse;
                //Application.DoEvents();
                nowAnimation = false;
            }
        }
    }
}
