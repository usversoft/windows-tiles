//using Pad.UserConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    [System.ComponentModel.TypeConverterAttribute(typeof(PropertySorter))]
    public interface I_FileBaseClass : I_BaseClass
    {
        _FileBaseClass Clone();
        bool SaveAs(string fileName, System.Xml.Serialization.XmlAttributeOverrides overrides, ref ActionProvider information);
        _FileBaseClass Read(string fileName, ref ActionProvider information);
        [DynamicBrowsableAttribute(false)]
        [PropertyOrder(20)]
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.Category("0. Базовые значения")]
        [System.ComponentModel.DisplayName("Название программы")]
        [System.ComponentModel.Description("Название программы, сгенерирововшей этот файл.")]
        string ProgramName { get; set; }
        [DynamicBrowsableAttribute(false)]
        [PropertyOrder(30)]
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.Category("0. Базовые значения")]
        [System.ComponentModel.DisplayName("Версия программы")]
        [System.ComponentModel.Description("Версия программы, сгенерирововшая этот файл.")]
        string ProgramVersion { get; set; }
        [DynamicBrowsable(false)]
        [PropertyOrder(50)]
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.Browsable(true)]
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.Category("0. Базовые значения")]
        [System.ComponentModel.DisplayName("Файл")]
        [System.ComponentModel.Description("Файл, из которго берётся информация.")]
        string FileName { get; set; }
        [PropertyOrder(52)]
        [System.Xml.Serialization.XmlIgnore()]
        [DynamicBrowsable(false)]
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.Category("0. Базовые значения")]
        [System.ComponentModel.DisplayName("Папка файла")]
        [System.ComponentModel.Description("Папка, в которой находится файл.")]
        string Directory { get; }
        //[PropertyOrder(60)]
        //[System.Xml.Serialization.XmlIgnore()]
        //[DynamicBrowsable(false)]
        //[System.ComponentModel.Browsable(false)]
        //[System.ComponentModel.ReadOnly(true)]
        //[System.ComponentModel.Category("0. Базовые значения")]
        //[System.ComponentModel.DisplayName("Пользователь")]
        //[System.ComponentModel.Description("Пользователь, управляющий этим файлом в данный момент.")]
        //UserConfigChat User { get; set; }
        [PropertyOrder(62)]
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.Category("0. Базовые значения")]
        [System.ComponentModel.DisplayName("Автор")]
        [System.ComponentModel.Description("Автор файла.")]
        string Author { get; set; }
        [PropertyOrder(64)]
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.Category("0. Базовые значения")]
        [System.ComponentModel.DisplayName("Автор сохранения")]
        [System.ComponentModel.Description("Автор последнего сохранения файла.")]
        string AuthorLastSave { get; set; }
        [PropertyOrder(-1000)]
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DefaultValue(true)]
        [System.ComponentModel.Category("Не для дизайнера")]
        [System.ComponentModel.DisplayName("Авто вызов WasSave")]
        [System.ComponentModel.Description("Устанавливает надо ли автоматически вызывать WasSave после сохранения.")]
        bool WasChangesSetFalseCallAfterSave { get; set; }
    }

}
