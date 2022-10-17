using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace TilesControl
{
    public enum SpecilaType
    {
        None,
        View,
        Add,
        Publish
    }


    [Serializable]
    public class Tile : BaseClasses._BaseClass
    {
        public enum CellsType
        {
            other,
            minSquare,
            bigSquare,
            minRectangle,
            bigRectangle
        }
        public override string VersionDefault()
        {
            return "1.0";
        }

        //public TileServer GetTileServer()
        //{
        //    TileServer ts = new TileServer();
        //    var props = this.GetType().GetProperties();
        //    foreach(System.Reflection.PropertyInfo prop in props)
        //    {
        //        System.Reflection.PropertyInfo propNew = ts.GetType().GetProperty(prop.Name);
        //        propNew.SetValue(ts, prop.GetValue(this, null), null);                
        //    }

        //    return ts;
        //}

        public event EventHandler CellsCountChanged;
        public event EventHandler LinkChanged;

        public bool Visible { get; set; } = true;

        public bool ImageCut { get => imageCut; set => imageCut = value; }
        public bool IsSpecial { get; set; }
        public SpecilaType SpecilaType { get => specilaType; set => specilaType = value; }
        [System.Xml.Serialization.XmlIgnore]
        public bool IsEdit { get; set; }
        public string Text
        {
            get => text; set
            {
                text = value;
                SplitText(text, new string[] { "h1", "a", "footer" });
            }
        }
        public bool ViewText { get; set; } = true;
        [System.Xml.Serialization.XmlIgnore]
        public Color TextForeColor { get; set; } = Color.Black;
        public string TextForeColorWeb
        {
            get
            {
                return ColorTranslator.ToHtml(TextForeColor);
            }
            set
            {
                TextForeColor = ColorTranslator.FromHtml(value);
            }
        }
        private Color backColor = Color.White;
        [System.Xml.Serialization.XmlIgnore]
        public Color BackColor
        {
            get => backColor;
            set
            {
                backColor = value;
            }
        }
        public string BackColorWeb
        {
            get
            {
                return ColorTranslator.ToHtml(BackColor);
            }
            set
            {
                BackColor = ColorTranslator.FromHtml(value);
            }
        }
        private Color backColor2 = Color.White;
        [System.Xml.Serialization.XmlIgnore]
        public Color BackColor2
        {
            get => backColor2;
            set
            {
                backColor2 = value;
            }
        }
        public string BackColor2Web
        {
            get
            {
                return ColorTranslator.ToHtml(BackColor2);
            }
            set
            {
                BackColor2 = ColorTranslator.FromHtml(value);
            }
        }
        [System.Xml.Serialization.XmlIgnore]
        public Color BackColorMouse { get; set; } = Color.BlueViolet;
        public string BackColorMouseWeb
        {
            get
            {
                return ColorTranslator.ToHtml(BackColorMouse);
            }
            set
            {
                BackColorMouse = ColorTranslator.FromHtml(value);
            }
        }
        [System.Xml.Serialization.XmlIgnore]
        public Font Font
        {
            get => font; set
            {
                font = value;
                FontH1 = new Font(value.FontFamily, value.Size + 4);
                FontFooter = new Font(value.FontFamily, value.Size - 2);
            }
        }
        [System.Xml.Serialization.XmlIgnore]
        public Font FontH1 { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public Font FontFooter { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public string Link
        {
            get => link;
            set
            {
                if (link != value)
                {
                    link = value;
                    LinkChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        public bool ImageSame(Image image1, Image image2)
        {
            return ImageSame(new Bitmap(image1), new Bitmap(image2));
        }
        public bool ImageSame(Bitmap image1, Bitmap image2)
        {
            if (image1.Size != image2.Size)
            {
                return false;
            }
            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    if (image1.GetPixel(x, y) != image2.GetPixel(x, y))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public string ImageFileName { get; set; }
        private ImageFormat imageFormat = null;
        [System.Xml.Serialization.XmlIgnore]
        public System.Drawing.Imaging.ImageFormat ImageFormat
        {
            get => imageFormat;
            set
            {
                imageFormat = value;
                if (imageFormat == System.Drawing.Imaging.ImageFormat.Bmp)
                {
                    imageFormat = System.Drawing.Imaging.ImageFormat.Png;
                }
            }
        }
        [System.Xml.Serialization.XmlElement("ImageFormat")]
        public string ImageFormatSerialized
        {
            get
            {
                if (ImageFormat == null)
                {
                    return null;
                }
                return ImageFormat.ToString();
            }
            set
            {
                if (value == null)
                {
                    ImageFormat = null;
                }
                else
                {
                    if(value == System.Drawing.Imaging.ImageFormat.Bmp.ToString())
                    {
                        ImageFormat = System.Drawing.Imaging.ImageFormat.Bmp;
                    }
                    else if (value == System.Drawing.Imaging.ImageFormat.Emf.ToString())
                    {
                        ImageFormat = System.Drawing.Imaging.ImageFormat.Emf;
                    }
                    else if (value == System.Drawing.Imaging.ImageFormat.Exif.ToString())
                    {
                        ImageFormat = System.Drawing.Imaging.ImageFormat.Exif;
                    }
                    else if (value == System.Drawing.Imaging.ImageFormat.Gif.ToString())
                    {
                        ImageFormat = System.Drawing.Imaging.ImageFormat.Gif;
                    }
                    else if (value == System.Drawing.Imaging.ImageFormat.Icon.ToString())
                    {
                        ImageFormat = System.Drawing.Imaging.ImageFormat.Icon;
                    }
                    else if (value == System.Drawing.Imaging.ImageFormat.Jpeg.ToString())
                    {
                        ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
                    }
                    else if (value == System.Drawing.Imaging.ImageFormat.MemoryBmp.ToString())
                    {
                        ImageFormat = System.Drawing.Imaging.ImageFormat.MemoryBmp;
                    }
                    else if (value == System.Drawing.Imaging.ImageFormat.Png.ToString())
                    {
                        ImageFormat = System.Drawing.Imaging.ImageFormat.Png;
                    }
                    else if (value == System.Drawing.Imaging.ImageFormat.Tiff.ToString())
                    {
                        ImageFormat = System.Drawing.Imaging.ImageFormat.Tiff;
                    }
                    else if (value == System.Drawing.Imaging.ImageFormat.Wmf.ToString())
                    {
                        ImageFormat = System.Drawing.Imaging.ImageFormat.Wmf;
                    } 
                }
            }
        }
        [System.Xml.Serialization.XmlIgnore]
        public Image Image { get; set; }
        [System.Xml.Serialization.XmlElement("Image")]
        public virtual byte[] ImageSerialized
        {
            get
            { // serialize
                if (Image == null) return null;
                using (MemoryStream ms = new MemoryStream())
                {
                    Image.Save(ms, ImageFormat);
                    return ms.ToArray();
                }
            }
            set
            { // deserialize
                if (value == null)
                {
                    Image = null;
                }
                else
                {
                    using (MemoryStream ms = new MemoryStream(value))
                    {
                        Image = new Bitmap(ms);
                    }
                }
            }
        }
        [System.Xml.Serialization.XmlIgnore]
        public Image ImageOriginal { get; set; }
        [System.Xml.Serialization.XmlElement("ImageOriginal")]
        public virtual byte[] ImageOriginalSerialized
        {
            get
            { // serialize
                if (ImageOriginal == null) return null;
                using (MemoryStream ms = new MemoryStream())
                {
                    ImageOriginal.Save(ms, ImageFormat);
                    return ms.ToArray();
                }
            }
            set
            { // deserialize
                if (value == null)
                {
                    ImageOriginal = null;
                }
                else
                {
                    using (MemoryStream ms = new MemoryStream(value))
                    {
                        ImageOriginal = new Bitmap(ms);
                    }
                }
            }
        }
        private Size cellsCount = new Size(1, 1);
        public Size CellsCount
        {
            get
            {
                return cellsCount;
            }
            set
            {
                if (cellsCount != value)
                {
                    cellsCount = value;
                    if (cellsCount == new Size(1, 1))
                    {
                        cellsTypeValue = CellsType.minSquare;
                    }
                    else if (cellsCount == new Size(2, 2))
                    {
                        cellsTypeValue = CellsType.bigSquare;
                    }
                    else if (cellsCount == new Size(2, 1))
                    {
                        cellsTypeValue = CellsType.minRectangle;
                    }
                    else if (cellsCount == new Size(4, 2))
                    {
                        cellsTypeValue = CellsType.bigRectangle;
                    }
                    else
                    {
                        cellsTypeValue = CellsType.other;
                    }
                    CellsCountChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        private CellsType cellsTypeValue = Tile.CellsType.minSquare;

        public CellsType CellsTypeValue
        {
            get
            {
                return cellsTypeValue;
            }
            set
            {
                cellsTypeValue = value;
                switch (cellsTypeValue)
                {
                    case CellsType.minSquare:
                        CellsCount = new Size(1, 1);
                        break;
                    case CellsType.bigSquare:
                        CellsCount = new Size(2, 2);
                        break;
                    case CellsType.minRectangle:
                        CellsCount = new Size(2, 1);
                        break;
                    case CellsType.bigRectangle:
                        CellsCount = new Size(4, 2);
                        break;
                    case CellsType.other:
                        break;
                }
            }
        }

        public Point Location { get; set; }

        public bool OnlyNull()
        {
            return (Image == null && (Text == null || Text == ""));
        }
        public bool OnlyText()
        {
            return (Image == null && (Text != null || Text != ""));
        }
        public bool OnlyImage()
        {
            return (Image != null && (Text == null || Text == ""));
        }

        public static Size GetTileSize(Size cellsCount, Size cellSize, Padding margin)
        {
            return new Size(cellsCount.Width * cellSize.Width + (cellsCount.Width - 1) * (margin.Left + margin.Right),
                cellsCount.Height * cellSize.Height + (cellsCount.Height - 1) * (margin.Top + margin.Bottom));
        }

        public Image GetTileBackground(Size cellsCount, Size cellSize, Padding margin, Padding padding, bool mouseIn, TileControl.CellOrientationType cellOrientation)
        {
            Size imgSize = GetTileSize(cellsCount, cellSize, margin);
            if (imgSize.Height <= 0 || imgSize.Width <= 0)
            {
                return new Bitmap(1, 1);
            }
            Image img = new Bitmap(imgSize.Width, imgSize.Height);

            using (Graphics g = Graphics.FromImage(img))
            {
                Color bgColor2 = BackColor2;
                Color bgColor = BackColor;
                if (mouseIn)
                {
                    bgColor = BackColorMouse;
                }
                if (cellOrientation == TileControl.CellOrientationType.Horizontal)
                {
                    g.FillRectangle(new LinearGradientBrush(new Point(0, 0), new Point(imgSize.Width, 0), bgColor2, Color.Transparent), new Rectangle(new Point(0, 0), imgSize));
                }
                else
                {
                    g.FillRectangle(new LinearGradientBrush(new Point(0, 0), new Point(0, imgSize.Height), bgColor2, Color.Transparent), new Rectangle(new Point(0, 0), imgSize));
                }

                g.Dispose();
            }

            return img;
        }
        public Image GetTile1(Size cellsCount, Size cellSize, Padding margin, Padding padding, bool mouseIn, TileControl.CellOrientationType cellOrientation, float scale = 1)
        {
            if (scale != 1)
            {
                cellSize = new Size((int)(cellSize.Width * scale), (int)(cellSize.Height * scale));
            }

            Size imgSize = GetTileSize(cellsCount, cellSize, margin);
            if (imgSize.Height <= 0 || imgSize.Width <= 0)
            {
                return new Bitmap(1, 1);
            }
            Image img = new Bitmap(imgSize.Width, imgSize.Height);

            using (Graphics g = Graphics.FromImage(img))
            {
                Color bgColor = BackColor;
                if (mouseIn)
                {
                    bgColor = BackColorMouse;
                }

                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                Size imgSizeFull = imgSize;
                imgSize.Width -= (padding.Left + padding.Right);
                imgSize.Height -= (padding.Top + padding.Bottom);
                if (!IsEdit)
                {
                    RectTextForMouse = new Rectangle(0, 0, imgSizeFull.Width, imgSizeFull.Height / 2);
                    if (ViewText && !IsEdit)
                    {
                        Rectangle textRect = new Rectangle(padding.Left, padding.Top, imgSize.Width, imgSize.Height);
                        if (!OnlyText() && cellOrientation == TileControl.CellOrientationType.Horizontal)
                        {
                            textRect.Width = imgSizeFull.Width / 2 - (padding.Left + padding.Right);
                        }
                        else if (Image != null && Text != null && Text != "")
                        {
                            textRect.Height = imgSizeFull.Height / 2 - (padding.Top + padding.Bottom);
                        }
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Near;
                        if (this.Image == null)
                        {
                            //sf.LineAlignment = StringAlignment.Center;
                        }
                        else
                        {
                            sf.LineAlignment = StringAlignment.Near;
                        }
                        if (IsSpecial && SpecilaType != SpecilaType.View)
                        {
                            sf.LineAlignment = StringAlignment.Center;
                            sf.Alignment = StringAlignment.Center;
                        }
                        //sf.LineAlignment = StringAlignment.Near;
                        sf.Trimming = StringTrimming.EllipsisWord;
                        DrawText(g, textRect, sf, scale);
                    }
                }
                g.Dispose();
            }

            return img;
        }
        public Image GetTile2(Size cellsCount, Size cellSize, Padding margin, Padding padding, bool mouseIn)
        {
            Size imgSize = GetTileSize(cellsCount, cellSize, margin);
            if (imgSize.Height <= 0 || imgSize.Width <= 0)
            {
                return new Bitmap(1, 1);
            }
            Image img = new Bitmap(imgSize.Width, imgSize.Height);

            using (Graphics g = Graphics.FromImage(img))
            {
                Color bgColor = BackColor;
                if (mouseIn)
                {
                    bgColor = BackColorMouse;
                }
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                Size imgSizeFull = imgSize;
                imgSize.Width -= (padding.Left + padding.Right);
                imgSize.Height -= (padding.Top + padding.Bottom);
                if (!IsEdit)
                {
                    if (Image != null)
                    {
                        Rectangle imgRect;
                        if (ImageCut)
                        {
                            imgRect = GetImageRectangleCut(Image, imgSizeFull);
                        }
                        else
                        {
                            imgRect = GetImageRectangle(Image, imgSizeFull);
                        }
                        g.DrawImage(Image, imgRect);
                    }
                }
                g.Dispose();
            }

            return img;
        }


        public Image GetTile(Size cellsCount, Size cellSize, Padding margin, Padding padding, TileControl.CellOrientationType cellOrientation)
        {
            Size imgSize = GetTileSize(cellsCount, cellSize, margin);
            if (imgSize.Height <= 0 || imgSize.Width <= 0)
            {
                return new Bitmap(1, 1);
            }
            Image img = new Bitmap(imgSize.Width, imgSize.Height);

            using (Graphics g = Graphics.FromImage(img))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                g.FillRectangle(new SolidBrush(BackColor), new Rectangle(new Point(0, 0), imgSize));
                Size imgSizeFull = imgSize;
                imgSize.Width -= (padding.Left + padding.Right);
                imgSize.Height -= (padding.Top + padding.Bottom);
                if (!IsEdit)
                {
                    if (Image != null)
                    {
                        Rectangle imgRect;
                        if (ImageCut)
                        {
                            imgRect = GetImageRectangleCut(Image, imgSizeFull);
                        }
                        else
                        {
                            imgRect = GetImageRectangle(Image, imgSizeFull);
                        }

                        g.DrawImage(Image, imgRect);
                        if (ViewText && !IsEdit && Text != "" && Text != null)
                        {
                            Rectangle textRect = new Rectangle(padding.Left, padding.Top, imgSize.Width, imgSize.Height / 2);
                            if (cellOrientation == TileControl.CellOrientationType.Horizontal)
                            {
                                textRect = new Rectangle(padding.Left, padding.Top, imgSize.Width / 2, imgSize.Height);
                                g.FillRectangle(new SolidBrush(BackColor), new Rectangle(-5, -5, 5 + imgSizeFull.Width / 2, imgSizeFull.Height + 10));
                                RectTextForMouse = new Rectangle(0, 0, imgSizeFull.Width / 2, imgSizeFull.Height);
                            }
                            else
                            {
                                g.FillRectangle(new SolidBrush(BackColor), new Rectangle(-5, -5, imgSizeFull.Width + 10, 5 + imgSizeFull.Height / 2));
                                RectTextForMouse = new Rectangle(0, 0, imgSizeFull.Width, imgSizeFull.Height / 2);
                            }
                            g.ResetClip();
                            g.Clip = new Region(textRect);
                            StringFormat sf = new StringFormat();
                            sf.Alignment = StringAlignment.Near;
                            sf.LineAlignment = StringAlignment.Far;
                            sf.Trimming = StringTrimming.EllipsisWord;

                            DrawText(g, textRect, sf);
                        }
                        else
                        {
                            RectTextForMouse = new Rectangle(0, 0, imgSizeFull.Width, imgSizeFull.Height);
                        }
                    }
                    else
                    {
                        RectTextForMouse = new Rectangle(0, 0, imgSizeFull.Width, imgSizeFull.Height);
                        if (ViewText && !IsEdit)
                        {
                            Rectangle textRect = new Rectangle(padding.Left, padding.Top + imgSize.Height / 4, imgSize.Width, imgSize.Height / 2);
                            g.ResetClip();
                            g.Clip = new Region(textRect);
                            StringFormat sf = new StringFormat();
                            sf.Alignment = StringAlignment.Near;
                            sf.LineAlignment = StringAlignment.Center;
                            sf.Trimming = StringTrimming.EllipsisWord;

                            DrawText(g, textRect, sf);
                        }
                    }
                }
                g.Dispose();
            }

            return img;
        }

        public Image GetTileMouse(Size cellsCount, Size cellSize, Padding margin, Padding padding)
        {
            Size imgSize = GetTileSize(cellsCount, cellSize, margin);
            if (imgSize.Height <= 0 || imgSize.Width <= 0)
            {
                return new Bitmap(1, 1);
            }
            Image img = new Bitmap(imgSize.Width, imgSize.Height);

            using (Graphics g = Graphics.FromImage(img))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                g.FillRectangle(new SolidBrush(BackColor), new Rectangle(new Point(0, 0), imgSize));
                Size imgSizeFull = imgSize;
                imgSize.Width -= (padding.Left + padding.Right);
                imgSize.Height -= (padding.Top + padding.Bottom);
                if (!IsEdit)
                {
                    if (Image != null)
                    {
                        if (ViewText && Text != null & Text != "")
                        {
                            Rectangle imgRect;
                            if (ImageCut)
                            {
                                imgRect = GetImageRectangleCut(Image, imgSizeFull);
                            }
                            else
                            {
                                imgRect = GetImageRectangle(Image, imgSizeFull);
                            }
                            g.DrawImage(Image, imgRect);
                        }
                        else
                        {
                            Size imgSize2 = new Size((int)(imgSizeFull.Width * 1.2), (int)(imgSizeFull.Height * 1.2));
                            Rectangle imgRect;
                            if (ImageCut)
                            {
                                imgRect = GetImageRectangleCut(Image, imgSize2);
                            }
                            else
                            {
                                imgRect = GetImageRectangle(Image, imgSize2);
                            }
                            imgRect.X -= (int)(imgSizeFull.Width * 0.1);
                            imgRect.Y -= (int)(imgSizeFull.Height * 0.1);
                            g.DrawImage(Image, imgRect);
                        }
                    }
                    else
                    {
                        if (ViewText && !IsEdit)
                        {
                            g.ResetClip();
                            g.Clip = new Region(new Rectangle(padding.Left, padding.Top, imgSizeFull.Width - padding.Left - padding.Right, imgSizeFull.Height - padding.Top - padding.Bottom));

                            Rectangle textRect = new Rectangle(padding.Left, padding.Top, imgSize.Width, imgSize.Height);
                            StringFormat sf = new StringFormat();
                            sf.Alignment = StringAlignment.Near;
                            sf.LineAlignment = StringAlignment.Center;
                            sf.Trimming = StringTrimming.EllipsisWord;

                            DrawText(g, textRect, sf);
                        }
                    }
                }
                g.Dispose();
            }

            return img;
        }

        public Image GetTileAnim(ref bool animationBreak, int n, Image imgResult, Image imgBegin, bool MouseEnter, bool onlyText, bool onlyImage, TileControl.CellOrientationType cellOrientation)
        {
            Image img = null;
            if (onlyImage)
            {
                img = (Image)imgBegin.Clone();
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                    n = n * 2;
                    float k = (float)(0.2 * ((float)n / 100));
                    if (MouseEnter)
                    {
                        Size imgSizeFull = img.Size;
                        Size imgSize = new Size((int)(imgSizeFull.Width * (1 + k)), (int)(imgSizeFull.Height * (1 + k)));
                        Rectangle imgRect = new Rectangle(new Point(0, 0), imgSize);
                        imgRect.X -= (int)(imgSizeFull.Width * (k / 2));
                        imgRect.Y -= (int)(imgSizeFull.Height * (k / 2));
                        g.DrawImage(imgBegin, imgRect);

                        if (n == 100)
                        {
                            animationBreak = true;
                            img = (Image)imgResult.Clone();
                        }
                    }
                    else
                    {
                        Size imgSizeFull = img.Size;
                        imgSizeFull = new Size((int)(imgSizeFull.Width * (1.2 - k)), (int)(imgSizeFull.Height * (1.2 - k)));
                        Rectangle imgRect = new Rectangle(new Point(0, 0), imgSizeFull);
                        imgRect.X -= (int)(imgSizeFull.Width * ((0.2 - k) / 2));
                        imgRect.Y -= (int)(imgSizeFull.Height * ((0.2 - k) / 2));
                        g.DrawImage(imgBegin, imgRect);

                        if (n == 100)
                        {
                            animationBreak = true;
                            img = (Image)imgBegin.Clone();
                        }
                    }
                }
            }
            else
            {
                Size imgSize = imgResult.Size;
                img = (Image)imgResult.Clone();
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                    int h = imgSize.Height;
                    if (cellOrientation == TileControl.CellOrientationType.Horizontal && !onlyText)
                    {
                        h = imgSize.Width;
                    }

                    if (MouseEnter)
                    {
                        if (h > n)
                        {
                            if (onlyText)
                            {
                                g.Clip = new Region(new Rectangle(new Point(0, h / 4 - (imgSize.Height - n)), new Size(imgSize.Width, h / 2 + 2 * (imgSize.Height - n))));
                            }
                            else
                            {
                                if (cellOrientation == TileControl.CellOrientationType.Horizontal)
                                {
                                    g.Clip = new Region(new Rectangle(new Point(0, 0), new Size(imgSize.Width - n, imgSize.Height)));
                                }
                                else
                                {
                                    g.Clip = new Region(new Rectangle(new Point(0, 0), new Size(imgSize.Width, imgSize.Height - n)));
                                }
                            }
                            g.DrawImage(imgBegin, new Point(0, 0));
                        }
                        else
                        {
                            animationBreak = true;
                        }
                    }
                    else
                    {

                        if (h / 2 > n)
                        {
                            if (onlyText)
                            {
                                g.Clip = new Region(new Rectangle(new Point(0, h / 4 - n), new Size(imgSize.Width, h / 2 + 2 * n)));
                            }
                            else
                            {
                                if (cellOrientation == TileControl.CellOrientationType.Horizontal)
                                {
                                    g.Clip = new Region(new Rectangle(new Point(0, 0), new Size(n, imgSize.Height)));
                                }
                                else
                                {
                                    g.Clip = new Region(new Rectangle(new Point(0, 0), new Size(imgSize.Width, n)));
                                }
                            }
                            g.DrawImage(imgBegin, new Point(0, 0));
                        }
                        else
                        {
                            g.DrawImage(imgBegin, new Point(0, 0));
                            animationBreak = true;
                        }
                    }
                }
            }
            return img;
        }

        [System.Xml.Serialization.XmlIgnore]
        public List<Teg> tegs = new List<Teg>();
        private string link;
        private string text;
        private Font font;
        [System.Xml.Serialization.XmlIgnore]
        public bool canScrollText = false;
        [System.Xml.Serialization.XmlIgnore]
        public int beginY = 0;
        [System.Xml.Serialization.XmlIgnore]
        public int beginYMax = 0;

        public string GetOnlyText()
        {
            string result = "";
            foreach (Teg teg in tegs)
            {
                switch (teg.TegName)
                {
                    case "h1":
                        result += "\r\n" + teg.Text + "\r\n\r\n";
                        break;
                    case "a":
                        result += " " + teg.Href + " ";
                        break;
                    default:
                        result += " " + teg.Text + " ";
                        break;
                }
            }
            while (result.IndexOf("  ") != -1)
            {
                result = result.Replace("  ", " ");
            }
            while (result.IndexOf("\r\n ") != -1)
            {
                result = result.Replace("\r\n ", "\r\n");
            }
            return result.Trim();
        }

        public void SplitText(string text, string[] useTegs)
        {
            tegs = new List<Teg>();
            if (text != null && text != "")
            {
                int prevI = 0;
                for (int i = 0; i < text.Length; i++)
                {
                    int tegBegin = text.IndexOf("<", i);
                    if (tegBegin == -1)
                    {
                        Teg teg = new Teg();
                        teg.TegName = "";
                        teg.Text = text.Substring(prevI);
                        tegs.Add(teg);
                        break;
                    }
                    else
                    {
                        int tegEnd = text.IndexOf(">", tegBegin);
                        if (tegEnd == -1)
                        {
                            Teg teg = new Teg();
                            teg.TegName = "";
                            teg.Text = text.Substring(prevI);
                            tegs.Add(teg);
                            break;
                        }
                        else
                        {
                            string tegName = text.Substring(tegBegin + 1, tegEnd - tegBegin - 1);
                            int tegSpace = tegName.IndexOf(" ");
                            string href = "";
                            if (tegSpace != -1)
                            {
                                string tegNameFull = tegName;
                                tegName = tegName.Substring(0, tegSpace);
                                if (tegName == "a")
                                {
                                    int tegHrefBegin = tegNameFull.IndexOf("href=\"");
                                    if (tegHrefBegin != -1)
                                    {
                                        int tegHrefEnd = tegNameFull.IndexOf("\"", tegHrefBegin + 6);
                                        if (tegHrefEnd != -1)
                                        {
                                            href = tegNameFull.Substring(tegHrefBegin + 6, tegHrefEnd - (tegHrefBegin + 6));
                                        }
                                    }
                                }
                            }
                            if (useTegs != null && !useTegs.Contains(tegName))
                            {
                                continue;
                            }
                            int tegClose = text.IndexOf("</" + tegName + ">", tegEnd);
                            if (tegClose == -1)
                            {
                                //Teg teg = new Teg();
                                //teg.TegName = "";
                                //teg.Text = text.Substring(i);
                                //tegs.Add(teg);
                                //break;
                            }
                            else
                            {
                                if (tegBegin - prevI > 0)
                                {
                                    Teg teg2 = new Teg();
                                    teg2.TegName = "";
                                    teg2.Text = text.Substring(prevI, tegBegin - prevI);
                                    tegs.Add(teg2);
                                }
                                Teg teg = new Teg();
                                teg.TegName = tegName;
                                teg.Href = href;
                                teg.Text = text.Substring(tegEnd + 1, tegClose - tegEnd - 1);
                                tegs.Add(teg);
                                i = tegClose + ("</" + tegName + ">").Length - 1;
                                prevI = i + 1;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < tegs.Count; i++)
            {
                tegs[i].Text = tegs[i].Text.Trim();
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public Rectangle RectTextForMouse = new Rectangle();
        private SpecilaType specilaType = SpecilaType.None;
        private bool imageCut = true;        

        private void DrawText(Graphics g, Rectangle rect, StringFormat sf, float scale = 1)
        {
            canScrollText = false;
            int nextY = 0;
            for (int i = 0; i < tegs.Count; i++)
            {
                string text = tegs[i].Text;
                string tegName = tegs[i].TegName;
                Font font = this.Font;
                switch (tegName)
                {
                    case "h1":
                        font = FontH1;
                        break;
                    case "footer":
                        font = FontFooter;
                        break;
                    case "a":

                        break;
                }
                if (scale != 1)
                {
                    font = new Font(font.FontFamily, font.Size * scale, font.Style);
                }

                canScrollText = nextY + font.Height > rect.Height;

                var tr = TextRenderer.MeasureText(text, font, rect.Size, TextFormatFlags.WordBreak);
                tr.Width = Math.Min(rect.Width, tr.Width);
                int yPlus = tr.Height;
                Rectangle textRect = new Rectangle(rect.X, beginY + rect.Y + nextY, tr.Width, tr.Height);
                if (!canScrollText)
                {
                    if (textRect.Bottom > rect.Bottom)
                    {
                        textRect.Height -= textRect.Bottom - rect.Bottom;
                    }
                }

                if (tegName == "h1")
                {
                    textRect.Height = Math.Min(textRect.Height, font.Height * 2);
                }
                textRect.Height = (int)Math.Floor((float)(textRect.Height) / (font.Height - 1)) * (font.Height - 1);
                if (textRect.Height == 0)
                {
                    textRect.Height = font.Height;
                }
                tegs[i].Rect = textRect;
                int hButtonPlus = 0;
                if (tegName == "a")
                {
                    nextY += 3;
                    textRect.Y += 3;

                    hButtonPlus = 3;
                    int hButtonPlusW = 0;
                    var tr2 = TextRenderer.MeasureText(text, font);
                    if (tr2.Width < rect.Width)
                    {
                        tr = TextRenderer.MeasureText(text, font, new Size(tr2.Width, rect.Size.Height), TextFormatFlags.WordBreak);
                        hButtonPlusW = 0;
                    }
                    else
                    {
                        tr.Width = rect.Width;
                    }
                    Rectangle buttonRect = new Rectangle(rect.X, beginY + rect.Y + nextY - hButtonPlus / 2, tr.Width + hButtonPlusW, tr.Height + hButtonPlus);
                    g.DrawRectangle(new Pen(TextForeColor, 1), buttonRect);
                    textRect = new Rectangle(rect.X + hButtonPlus / 2, beginY + rect.Y + nextY, tr.Width, tr.Height);
                    tegs[i].Rect = buttonRect;
                }

                if (IsSpecial && SpecilaType != SpecilaType.View && tegs.Count == 1)
                {
                    textRect = rect;
                    tegs[i].Rect = textRect;
                }

                if (beginY!=0 || !canScrollText)
                {
                    g.DrawString(text, font, new SolidBrush(TextForeColor), textRect, sf);
                }
                nextY += yPlus + hButtonPlus;
                canScrollText = nextY >= rect.Height;
                beginYMax = nextY - rect.Height;
            }
        }

        public void SetImages(Image beginImage, System.Drawing.Imaging.ImageFormat imageFormat = null)
        {
            this.Image = GetTileImageMax(beginImage, new Size(290, 290));
            this.ImageOriginal = GetTileImageOriginal(beginImage, new Size(1920, 1920));
            this.ImageFileName = "";

            if (imageFormat == null)
            {
                if (System.Drawing.Imaging.ImageFormat.Bmp.Equals(beginImage.RawFormat))
                {
                    ImageFormat = System.Drawing.Imaging.ImageFormat.Bmp;
                }
                else if (System.Drawing.Imaging.ImageFormat.Emf.Equals(beginImage.RawFormat))
                {
                    ImageFormat = System.Drawing.Imaging.ImageFormat.Emf;
                }
                else if (System.Drawing.Imaging.ImageFormat.Gif.Equals(beginImage.RawFormat))
                {
                    ImageFormat = System.Drawing.Imaging.ImageFormat.Gif;
                }
                else if (System.Drawing.Imaging.ImageFormat.Icon.Equals(beginImage.RawFormat))
                {
                    ImageFormat = System.Drawing.Imaging.ImageFormat.Icon;
                }
                else if (System.Drawing.Imaging.ImageFormat.Jpeg.Equals(beginImage.RawFormat))
                {
                    ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
                }
                else if (System.Drawing.Imaging.ImageFormat.Png.Equals(beginImage.RawFormat))
                {
                    ImageFormat = System.Drawing.Imaging.ImageFormat.Png;
                }
                else if (System.Drawing.Imaging.ImageFormat.Tiff.Equals(beginImage.RawFormat))
                {
                    ImageFormat = System.Drawing.Imaging.ImageFormat.Tiff;
                }
                else if (System.Drawing.Imaging.ImageFormat.Wmf.Equals(beginImage.RawFormat))
                {
                    ImageFormat = System.Drawing.Imaging.ImageFormat.Wmf;
                }
                else
                {
                    using (Image img = new Bitmap(Image.Width, Image.Height))
                    {
                        using (Graphics g = Graphics.FromImage(img))
                        {
                            g.FillRectangle(new SolidBrush(Color.Magenta), new Rectangle(new Point(0, 0), new Size(img.Width, img.Height)));
                            g.DrawImage(Image, new Point(0, 0));
                        }

                        if (ImageSame(Image, img))
                        {
                            ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
                        }
                        else
                        {
                            ImageFormat = System.Drawing.Imaging.ImageFormat.Png;
                        }
                    }
                }
            }
            else
            {
                ImageFormat = imageFormat;
            }
        }

        private Image GetTileImageMax(Image beginImage, Size newSize)
        {
            if (beginImage == null)
            {
                return null;
            }
            Rectangle imgRect = GetImageRectangleCut(beginImage, newSize);
            Image newImg = new Bitmap(newSize.Width, newSize.Height);
            using (Graphics g = Graphics.FromImage(newImg))
            {
                g.DrawImage(beginImage, imgRect);
            }
            return newImg;
        }
        private Image GetTileImageOriginal(Image beginImage, Size newSize)
        {
            if (beginImage == null)
            {
                return null;
            }
            Rectangle imgRect = GetImageRectangle(beginImage, newSize);
            Image newImg = new Bitmap(imgRect.Width, imgRect.Height);
            using (Graphics g = Graphics.FromImage(newImg))
            {
                g.DrawImage(beginImage, new Point(0, 0));
            }
            return newImg;
        }

        private Rectangle GetImageRectangle(Image imgOriginal, Size newSize)
        {
            int w = imgOriginal.Width;
            int h = imgOriginal.Height;
            if (newSize.Width != 0)
                w = newSize.Width;
            if (newSize.Height != 0)
                h = newSize.Height;
            double kw = Math.Min(1, w / (double)imgOriginal.Width);
            double kh = Math.Min(1, h / (double)imgOriginal.Height);
            double k = Math.Min(kw, kh);
            Rectangle rect = new Rectangle();
            rect.Width = System.Convert.ToInt32(k * imgOriginal.Width);
            rect.Height = System.Convert.ToInt32(k * imgOriginal.Height);
            rect.X = (w - rect.Width) / 2;
            rect.Y = (h - rect.Height) / 2;

            return rect;
        }
        private Rectangle GetImageRectangleCut(Image imgOriginal, Size newSize)
        {
            int w = imgOriginal.Width;
            int h = imgOriginal.Height;
            if (newSize.Width != 0)
                w = newSize.Width;
            if (newSize.Height != 0)
                h = newSize.Height;
            double kw = Math.Min(1, w / (double)imgOriginal.Width);
            double kh = Math.Min(1, h / (double)imgOriginal.Height);
            double k = Math.Max(kw, kh);
            Rectangle rect = new Rectangle();
            rect.Width = System.Convert.ToInt32(k * imgOriginal.Width);
            rect.Height = System.Convert.ToInt32(k * imgOriginal.Height);
            rect.X = (w - rect.Width) / 2;
            rect.Y = (h - rect.Height) / 2;

            return rect;
        }
    }
}
