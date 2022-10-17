using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using BaseClasses;

namespace TilesControl
{
    [Serializable]
    public class TilesServerForSave : TilesForSave
    {
        public TilesServerForSave()
        {
            TilesServer = new List<TileServer>();
        }        
       
        public List<TileServer> TilesServer { get; set; }

        [XmlIgnore]
        public override List<Tile> Tiles { get; set; }

        public override string VersionDefault()
        {
            return "1.0";
        }

        public override bool Save(System.Xml.Serialization.XmlAttributeOverrides overrides, ref ActionProvider information)
        {
            return SaveAs(FileName,overrides, ref information);
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
        public static new TilesServerForSave Read(string fileName, ref ActionProvider information)
        {
            string _prev_information = information.InfoMessages.Last();
            information.AddInfo("Чтение файла плиток...");
            information.ErrorWhatMaybeAdd = new ErrorInfo();
            information.ErrorWhatMaybeAdd.ErrorMessage = "Ошибка при чтении файла информации о плитках.";
            TilesServerForSave read = (TilesServerForSave)Read(fileName, typeof(TilesServerForSave), ref information);
            read.FileName = fileName;
            read.WasChangesSetFalse();
            read.WasChangesSecondsSetFalse();
            information.ErrorWhatMaybeAdd = null;
            information.AddInfo(_prev_information);
            return read;
        }
    }
}
