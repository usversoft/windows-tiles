using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TilesControl;

namespace WindowsTile
{
    public partial class Form1 : Form
    {
        TilesControl.TilesControl tsc = new TilesControl.TilesControl();

        public Form1()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;

            blindPictureBox.Visible = false;
            this.Controls.Add(blindPictureBox);

            BuildAllTiles();

            tsc.Information = Session.Session.Information;
            loginToolStripMenuItem.PerformClick();
        }

        private void LoginToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            tsc.TileServerLogin("http://auth.your-website.ru", "email@inbox.ru", "123");
        }

        private ScrollAblePanel.ScrollAblePanelControl scrollAblePanelControl1;

        private void BuildAllTiles()
        {
            scrollAblePanelControl1 = new ScrollAblePanel.ScrollAblePanelControl();
            this.Controls.Add(scrollAblePanelControl1);
            scrollAblePanelControl1.Dock = DockStyle.Fill;
            scrollAblePanelControl1.BringToFront();
            scrollAblePanelControl1.EditablePanel.MinimumSize = new Size(0, 0);

            tsc.Visible = false;
            if (tsc.Tiles != null)
                tsc.ClearAllTiles(false);

            tsc.SetSpecialTiles = true;
            tsc.UseMove = true;
            tsc.AutoScroll = false;
            tsc.Width = 640;
            tsc.Height = 640;
            tsc.Dock = DockStyle.Top;
            tsc.Location = new Point(0, 0);
            tsc.CellSize = new Size(140, 140);
            tsc.Padding = new Padding(0);
            tsc.PaddingTile = new Padding(5);
            tsc.MarginTile = new Padding(5);
            tsc.BackColor = Color.Blue;
            tsc.AutoSizeColumnCount = 4;
            tsc.AutoSizeCell = false;
            tsc.FixedColumnCount = true;
            tsc.MoveAtSameTime = true;
            tsc.UseRow = true;
            tsc.ScrollAblePanelControl = scrollAblePanelControl1;
            tsc.Font = new Font("Lato", 10);
            tsc.PublishTileControlClick += Tsc_PublishTileControlClick;


            scrollAblePanelControl1.EditablePanel.BackColor = tsc.BackColor;
            scrollAblePanelControl1.EditablePanel.AutoScroll = false;
            scrollAblePanelControl1.EditablePanel.AutoSize = true;
            scrollAblePanelControl1.EditablePanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            scrollAblePanelControl1.ScrollMin = 100;
            scrollAblePanelControl1.ScrollBarSize = 50;
            scrollAblePanelControl1.ScrollBarIsRound = true;
            scrollAblePanelControl1.ScrollBarWidth = 8;
            scrollAblePanelControl1.AutoHorizontalScroll = false;
            scrollAblePanelControl1.EditablePanel.Controls.Add(tsc);
            scrollAblePanelControl1.DisableScrollingVChanged += ScrollAblePanelControl1_DisableScrollingVChanged; ;
            scrollAblePanelControl1.ScrollingVEnd += ScrollAblePanelControl1_ScrollingVEnd;

            if (tsc.Tiles == null)
            {
                tsc.Tiles = new List<TileControl>();
            }
            for (int i = 0; i < 6; i++)
            {
                Tile tile = new Tile();
                tile.SetImages(new Bitmap(Application.StartupPath + "/img/1.png"));
                tile.CellsTypeValue = Tile.CellsType.minSquare;
                tile.Font = tsc.Font;
                tile.Text = "<h1>Заголовок</h1><h2>hhh2</h2><a href=\"http://ryazantsev-ia.ru/\">Перейти</a>Текст 1 Текст 1 Текст 1 Текст 1 Текст 1 Текст 1 Текст 1 Текст 1 Текст 1 Текст 1 Текст 1 Текст 1<footer>Подвал</footer>";
                tile.TextForeColor = Color.White;
                tile.BackColor = Color.Red;
                tile.BackColorMouse = Color.BlueViolet;
                tsc.AddTile(tile);
                //tile.Link = "http://ryazantsev-ia.ru/";

                Tile tile2 = new Tile();
                tile2.SetImages(new Bitmap(Application.StartupPath + "/img/2.jpg"));
                tile2.CellsTypeValue = Tile.CellsType.minSquare;
                tile2.Font = tsc.Font;
                tile2.Text = "<h1>This.scrollAblePanelControl1.AutoHorizontalScroll = false;</h1>this.scrollAblePanelControl1.AutoVerticalScroll = true; this.scrollAblePanelControl1.DisableScrollingH = true; this.scrollAblePanelControl1.DisableScrollingV = true;";
                tile2.TextForeColor = Color.Black;
                tile2.BackColor = Color.LightYellow;
                tile2.BackColorMouse = Color.BlueViolet;
                tsc.AddTile(tile2);

                Tile tile3 = new Tile();
                tile3.SetImages(new Bitmap(Application.StartupPath + "/img/3.jpg"));
                tile3.CellsTypeValue = Tile.CellsType.minSquare;
                tile3.Font = tsc.Font;
                tile3.Text = "<h1>Результаты поиска Выделенное описание</h1>из Интернета The RichTextBox has no padding property. Quick and dirty padding can be achieved by putting the RichTextBox in a Panel, which has the same BackColor property as the RichTextBox (usually Color. White ). Then, set the Dock property of the RichTextBox to Fill , and play with the Padding properties of the Panel control.5 нояб. 2015 г. Rich Text Box padding between text and";
                tile3.TextForeColor = Color.Black;
                tile3.BackColor = Color.Violet;
                tile3.BackColorMouse = Color.BlueViolet;
                tsc.AddTile(tile3);


                Tile tile4 = new Tile();
                tile4.SetImages(new Bitmap(Application.StartupPath + "/img/4.jpg"));
                tile4.CellsTypeValue = Tile.CellsType.minSquare;
                tile4.Font = tsc.Font;
                tile4.Text = "Текст 4";
                tile4.TextForeColor = Color.Black;
                tile4.BackColor = Color.Azure;
                tile4.BackColorMouse = Color.BlueViolet;
                tsc.AddTile(tile4);

                Tile tile5 = new Tile();
                //tile5.Image = new Bitmap(Application.StartupPath + "/img/5.jpg");
                tile5.CellsTypeValue = Tile.CellsType.minSquare;
                tile5.Font = tsc.Font;
                tile5.Text = "<h1>Заголовок</h1>Текст 5 Текст 5 Текст 555\r\n Текст 5 Текст 5 Текст 555\r\nТекст 5 Текст 5 Текст 5 Текст 5 Текст 5 Текст 5 Текст 5 Текст 5 Текст 5 Текст 5 Текст 5 Текст 5 Текст 5 Текст 5<footer>Подвал</footer>";
                tile5.TextForeColor = Color.Black;
                tile5.BackColor = Color.Azure;
                tile5.BackColorMouse = Color.BlueViolet;
                tsc.AddTile(tile5);

                Tile tile6 = new Tile();
                tile6.SetImages(new Bitmap(Application.StartupPath + "/img/6.jpg"));
                tile6.CellsTypeValue = Tile.CellsType.minSquare;
                tile6.Font = tsc.Font;
                tile6.Text = "Текст 6";
                tile6.TextForeColor = Color.Black;
                tile6.BackColor = Color.Azure;
                tile6.BackColorMouse = Color.BlueViolet;
                tsc.AddTile(tile6);
            }

            tsc.Visible = true;
            tsc.BringToFront();
            tsc.RefreshTiles(false);
        }

        private void Tsc_PublishTileControlClick(object sender, EventArgs e)
        {
            //Обработчи нажатия на PublishTileControl

        }

        private void ScrollAblePanelControl1_DisableScrollingVChanged(object sender, EventArgs e)
        {
            if (scrollAblePanelControl1.DisableScrollingV)
            {
                //tsc.nowMouseWheel = false;
            }
        }

        private void ScrollAblePanelControl1_ScrollingVEnd(object sender, EventArgs e)
        {
            //tsc.NowMouseWheel = false;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Session.Session.ControlPressed = e.Control;
            Session.Session.AltPressed = e.Alt;
            Session.Session.ShiftPressed = e.Shift;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Session.Session.ControlPressed = e.Control;
            Session.Session.AltPressed = e.Alt;
            Session.Session.ShiftPressed = e.Shift;
        }

        private void Form1_SizeChanged(object sender, System.EventArgs e)
        {

        }

        private bool ThemeDark = false;
        private PictureBox blindPictureBox = new PictureBox();
        private void UiEffects_AnimationCompleted(object sender, UIEffectsSpace.AnimationCompletedEventArg e)
        {
            blindPictureBox.Visible = false;
        }

        private int pictureBoxNum = 0;
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            //tsc.ClearTiles();

            //Bitmap bm = getControlScreenshot(this);
            //if(blindPictureBox.Image != null)
            //{
            //    blindPictureBox.Image.Dispose();
            //}
            //blindPictureBox.Image = bm;
            //blindPictureBox.Location = Point.Empty; // new Point(-this.PointToScreen(new Point(0, 0)).X + this.Location.X, -this.PointToScreen(new Point(0, 0)).Y + this.Location.Y);
            //blindPictureBox.Size = this.Size;
            //blindPictureBox.BringToFront();
            //blindPictureBox.Visible = true;

            //ThemeDark = !ThemeDark;
            //if (ThemeDark)
            //{
            //    button1.Text = "Yellow";
            //    this.BackColor = Color.Yellow;
            //    tsc.BackColor = Color.Yellow;
            //}
            //else
            //{
            //    button1.Text = "Blue";
            //    this.BackColor = Color.Blue;
            //    tsc.BackColor = Color.Blue;
            //}

            //UIEffectsSpace.Animator uiEffects = new UIEffectsSpace.Animator();
            //uiEffects.AnimationCompleted += UiEffects_AnimationCompleted;


            ////UIEffectsSpace.Animation anim = UIEffectsSpace.Animation.HorizSlideAndRotate;
            //UIEffectsSpace.Animation anim = UIEffectsSpace.Animation.HorizBlind;
            //anim.TimeCoeff = (float)0.5;
            ////anim.SlideCoeff = new PointF(-1, 0);
            //anim.BlindCoeff = new PointF(1, 1);
            ////anim.RotateCoeff = 180;
            //uiEffects.HideSync(blindPictureBox, true, anim);
            ////uiEffects.WaitAllAnimations();
            ////blindPictureBox.Visible = false;


            tsc.RefreshTiles(false);

            //Control panel = null;// (i == 0) ? this.UserPanel1 : this.UserPanel2;// null;
            //if (pictureBoxNum == 0)
            //{
            //    panel = tsc.Tiles[0].pictureBox1;
            //    pictureBoxNum = 1;
            //}
            //else if (pictureBoxNum == 1)
            //{
            //    panel = tsc.Tiles[0].pictureBox2;
            //    pictureBoxNum = 0;
            //}


            //if (panel != null)
            //{
            //    panel.BringToFront();
            //    animator.ShowSync(panel, true, tsc.Tiles[0].Animation);//.VertSlide);
            //}

            //foreach (Control ctrl in tsc.Tiles[0].Controls)//pnLeft.Controls)
            //    if (ctrl.Visible && ctrl != panel)
            //        if (ctrl == tsc.Tiles[0].pictureBox1 || ctrl == tsc.Tiles[0].pictureBox2)// predefinedList2 || ctrl == pg || ctrl == tbCode)
            //            ctrl.Hide();
        }



        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog frm = new OpenFileDialog())
            {
                frm.Filter = "Tile|*.til";
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    tsc.ReadTilesForServer(frm.FileName);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog frm = new SaveFileDialog())
            {
                frm.Filter = "Tile|*.til";
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    tsc.SaveTilesForServer(frm.FileName);
                }
            }
        }

        private void cellSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SizeCellForm frm = new SizeCellForm())
            {
                frm.CellSize = tsc.CellSize;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    tsc.CellSize = frm.CellSize;
                    tsc.RefreshTiles();
                }
            }
        }

        private void useAnimationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsc.UseAnimation = useAnimationToolStripMenuItem.Checked;
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuildAllTiles();
        }

        public Bitmap getControlScreenshot(Control c, bool formInner = true)
        {
            Bitmap res;
            res = new Bitmap(c.Width, c.Height);
            c.DrawToBitmap(res, new Rectangle(Point.Empty, c.Size));
            if (formInner && c.GetType().BaseType == typeof(Form))
            {
                Form f = (Form)c;
                Point p = new Point(-f.PointToScreen(new Point(0, 0)).X + f.Location.X, -f.PointToScreen(new Point(0, 0)).Y + f.Location.Y);
                Bitmap res2 = new Bitmap(f.ClientRectangle.Width, f.ClientRectangle.Height);
                using (Graphics g = Graphics.FromImage(res2))
                {
                    g.DrawImage(res, p);
                }
                res.Dispose();
                return res2;
            }

            return res;
        }

        private void moveEvenlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsc.MoveEvenly = moveEvenlyToolStripMenuItem.Checked;
        }

        private void LoadImagesToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            tsc.TilesToServer("http://auth.ryazantsev-ia.ru", Session.Session.auth_string);
        }
    }
}
