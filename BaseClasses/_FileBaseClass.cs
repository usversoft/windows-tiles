using System;
using System.Linq;
using System.Windows.Forms;
//using Pad.UserConfig;

namespace BaseClasses
{
    [System.ComponentModel.TypeConverterAttribute(typeof(PropertySorter))]
    [System.ComponentModel.DefaultEvent("WaschangesChanged")]
    [Serializable()]
    public class _FileBaseClass : _BaseClass, I_FileBaseClass
    {
        public _FileBaseClass Clone()
        {
            return (_FileBaseClass)MemberwiseClone();
        }
        public _FileBaseClass()
        {
            programName = Application.ProductName;
            programVersion = Application.ProductVersion;
            fileName = "";
            //user = Session.User;
            //author = Session.User.UserName;
            author = Environment.UserName;
            authorLastSave = "";
            wasChangesSetFalseCallAfterSave = true;
        }

        public virtual bool SaveAs(string fileName, System.Xml.Serialization.XmlAttributeOverrides overrides, ref ActionProvider information)
        {
            bool saveAs = false;
            string _prev_information = information.InfoMessages.Last();
            information.AddInfo("Сохранение информации о базовом классе...");
            information.ErrorWhatMaybeAdd = new ErrorInfo();
            information.ErrorWhatMaybeAdd.ErrorMessage = "Ошибка при сохранении информации о базовом классе";
            saveAs = SaveAs(fileName, overrides, this, ref information);
            information.ErrorWhatMaybeAdd = null;
            information.AddInfo(_prev_information);
            return saveAs;
        }
        static public bool SaveAs(string fileName, System.Xml.Serialization.XmlAttributeOverrides overrides, object value, ref ActionProvider information)
        {
            bool saveAs = false;
            information.ErrorWhatMaybeAdd.ErrorIcon = MessageBoxIcon.Error;
            information.ErrorWhatMaybeAdd.ErrorCaption = "Ошибка!";

            _FileBaseClass value2 = (_FileBaseClass)value;
            if (value2.VersionIsDefault())
            {

            }
            if (value2.VersionIsNext())
            {
                if (information.Show("Файл " + System.IO.Path.GetFileName(fileName) + " более ПОЗДНЕЙ версии!" + "\r\n" + "Версия файла " + value2.Version + "\r\n" + "Установленная версия " + value2.VersionDefault() + "\r\n" + "При сохранении возможно будет потеряна информация, не поддерживаемая установленной версией!" + "\r\n" + "Продолжить сохранение?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return false;
                }
                value2.Version = value2.VersionDefault();
            }
            if (value2.VersionIsPrevious())
            {
                value2.Version = value2.VersionDefault();
            }

            value2.ProgramName = Application.ProductName;
            value2.ProgramVersion = Application.ProductVersion;
            value2.authorLastSave = Environment.UserName; //Session.User.UserName;
            if (value2.author == "")
            {
                value2.author = value2.authorLastSave;
            }


            saveAs = XmlUtility.WriteObj(fileName,  overrides, value, ref information);
            if (value2.WasChangesSetFalseCallAfterSave && saveAs)
            {
                value2.WasChangesSetFalse();
                value2.WasChangesSecondsSetFalse();
            }

            return saveAs;
        }
        public virtual _FileBaseClass Read(string fileName, ref ActionProvider information)
        {
            _FileBaseClass read = null;
            string _prev_information = information.InfoMessages.Last();
            information.AddInfo("Чтение информации о базовом классе...");
            information.ErrorWhatMaybeAdd = new ErrorInfo();
            information.ErrorWhatMaybeAdd.ErrorMessage = "Ошибка при чтении информации о базовом классе";
            read = Read(fileName, typeof(_FileBaseClass), ref information);
            information.ErrorWhatMaybeAdd = null;
            information.AddInfo(_prev_information);
            return read;
        }
        static public _FileBaseClass Read(string fileName, Type type, ref ActionProvider information)
        {
            _FileBaseClass read = null;
            information.ErrorWhatMaybeAdd.ErrorIcon = MessageBoxIcon.Error;
            information.ErrorWhatMaybeAdd.ErrorCaption = "Ошибка!";
            read = (_FileBaseClass)XmlUtility.ReadObj(fileName, type, ref information);
            read.FileName = fileName;
            read.WasChangesSetFalse();
            read.WasChangesSecondsSetFalse();
            if (read.VersionIsDefault())
            {

            }
            if (read.VersionIsNext())
            {
                information.Show("Файл " + System.IO.Path.GetFileName(fileName) + " более ПОЗДНЕЙ версии!" + "\r\n" + "Версия файла " + read.Version + "\r\n" + "Установленная версия " + read.VersionDefault() + "\r\n" + "Будет произведена попытка открыть файл только для чтения!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            if (read.VersionIsPrevious())
            {
                information.Show("Файл " + System.IO.Path.GetFileName(fileName) + " более РАННЕЙ версии!" + "\r\n" + "Версия файла " + read.Version + "\r\n" + "Установленная версия " + read.VersionDefault() + "\r\n" + "При сохранении файл будет преобразован к формату текущей версии!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return read;
        }

        public override string VersionDefault()
        {
            return "1.0";
        }

        private string programName;
        [DynamicBrowsableAttribute(false)]
        [PropertyOrder(20)]
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.Category("0. Базовые значения")]
        [System.ComponentModel.DisplayName("Название программы")]
        [System.ComponentModel.Description("Название программы, сгенерирововшей этот файл.")]
        public string ProgramName
        {
            get { return programName; }
            set
            {
                if (programName != value)
                {
                    programName = value;
                    this.WasChanges = true;
                }
            }
        }
        private string programVersion;
        [DynamicBrowsableAttribute(false)]
        [PropertyOrder(30)]
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.Category("0. Базовые значения")]
        [System.ComponentModel.DisplayName("Версия программы")]
        [System.ComponentModel.Description("Версия программы, сгенерирововшая этот файл.")]
        public string ProgramVersion
        {
            get { return programVersion; }
            set
            {
                if (programVersion != value)
                {
                    programVersion = value;
                    this.WasChanges = true;
                }
            }
        }
        private string fileName;
        [DynamicBrowsable(false)]
        [PropertyOrder(50)]
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.Browsable(true)]
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.Category("0. Базовые значения")]
        [System.ComponentModel.DisplayName("Файл")]
        [System.ComponentModel.Description("Файл, из которго берётся информация.")]
        public string FileName
        {
            get { return fileName; }
            set
            {
                if (fileName != value)
                {
                    fileName = value;
                    this.WasChangesSoconds = true;
                }
            }
        }
        [PropertyOrder(52)]
        [System.Xml.Serialization.XmlIgnore()]
        [DynamicBrowsable(false)]
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.Category("0. Базовые значения")]
        [System.ComponentModel.DisplayName("Папка файла")]
        [System.ComponentModel.Description("Папка, в которой находится файл.")]
        public string Directory
        {
            get
            {
                if (FileName == "")
                {
                    return "";
                }
                else
                {
                    return IOMy.Path.GetDirectoryName(FileName);
                }
            }
        }
        //private UserConfigChat user;
        //[PropertyOrder(60)]
        //[System.Xml.Serialization.XmlIgnore()]
        //[DynamicBrowsable(false)]
        //[System.ComponentModel.Browsable(false)]
        //[System.ComponentModel.ReadOnly(true)]
        //[System.ComponentModel.Category("0. Базовые значения")]
        //[System.ComponentModel.DisplayName("Пользователь")]
        //[System.ComponentModel.Description("Пользователь, управляющий этим файлом в данный момент.")]
        //UserConfigChat I_FileBaseClass.User
        //{
        //    get { return user; }
        //    set { user = value; }
        //}
        private string author;
        [PropertyOrder(62)]
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.Category("1. Основное")]
        [System.ComponentModel.DisplayName("Автор")]
        [System.ComponentModel.Description("Автор проекта.")]
        public string Author
        {
            get { return author; }
            set
            {
                if (author != value)
                {
                    author = value;
                    this.WasChangesSoconds = true;
                }
            }
        }
        private string authorLastSave;
        [PropertyOrder(64)]
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.Category("1. Основное")]
        [System.ComponentModel.DisplayName("Автор сохранения")]
        [System.ComponentModel.Description("Автор последнего сохранения проекта.")]
        public string AuthorLastSave
        {
            get { return authorLastSave; }
            set
            {
                if (authorLastSave != value)
                {
                    authorLastSave = value;
                    this.WasChangesSoconds = true;
                }
            }
        }
        private bool wasChangesSetFalseCallAfterSave;
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DefaultValue(true)]
        [System.ComponentModel.Category("Не для дизайнера")]
        [System.ComponentModel.DisplayName("Авто вызов WasSave")]
        [System.ComponentModel.Description("Устанавливает надо ли автоматически вызывать WasSave после сохранения.")]
        public bool WasChangesSetFalseCallAfterSave
        {
            get { return wasChangesSetFalseCallAfterSave; }
            set { wasChangesSetFalseCallAfterSave = value; }
        }
    }
}
