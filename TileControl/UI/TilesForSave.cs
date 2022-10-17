using System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaseClasses;

namespace TilesControl
{
    [Serializable]
    public class TilesForSave : BaseClasses._FileBaseClass
    {
        public TilesForSave()
        {
            Tiles = new List<Tile>();
        }        
        public int Height { get; set; }
        public Size CellSize { get; set; }
        public bool AutoSizeCell { get; set; }
        public int AutoSizeColumnCount { get; set; }
        public bool FixedColumnCount { get; set; }
        public bool UseRow { get; set; }
        public List<Size> RowTemplate { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public Color BackColor { get; set; } = Color.White;
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
        public Color DefaultTileTextForeColor { get; set; } = Color.Black;
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
        public Color DefaultTileBackColor { get; set; } = Color.AliceBlue;
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

        public virtual List<Tile> Tiles { get; set; }

        public override string VersionDefault()
        {
            return "1.0";
        }

        public virtual bool Save(System.Xml.Serialization.XmlAttributeOverrides overrides, ref ActionProvider information)
        {
            return SaveAs(FileName, overrides, ref information);
        }
        public override bool SaveAs(string fileName, System.Xml.Serialization.XmlAttributeOverrides overrides, ref ActionProvider information)
        {
            string _prev_information = information.InfoMessages.Last();
            information.AddInfo("Сохранение в файл плиток...");
            information.ErrorWhatMaybeAdd = new ErrorInfo();
            information.ErrorWhatMaybeAdd.ErrorIcon = MessageBoxIcon.Error;
            information.ErrorWhatMaybeAdd.ErrorCaption = "Ошибка!";
            information.ErrorWhatMaybeAdd.ErrorMessage = "Ошибка при сохранении файла информации о плитках.";
            bool saveAs = SaveAs(fileName, overrides, this, ref information);
            if (saveAs)
            {
                this.FileName = fileName;
                if (WasChangesSetFalseCallAfterSave)
                {
                    this.WasChangesSetFalse();
                    this.WasChangesSecondsSetFalse();
                }
            }
            information.AddInfo(_prev_information);
            return saveAs;
        }
        public static new TilesForSave Read(string fileName, ref ActionProvider information)
        {
            string _prev_information = information.InfoMessages.Last();
            information.AddInfo("Чтение файла плиток...");
            information.ErrorWhatMaybeAdd = new ErrorInfo();
            information.ErrorWhatMaybeAdd.ErrorMessage = "Ошибка при чтении файла информации о плитках.";
            TilesForSave read = (TilesForSave)Read(fileName, typeof(TilesForSave), ref information);
            read.FileName = fileName;
            read.WasChangesSetFalse();
            read.WasChangesSecondsSetFalse();
            information.ErrorWhatMaybeAdd = null;
            information.AddInfo(_prev_information);
            return read;
        }
    }
}
