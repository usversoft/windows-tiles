using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaseClasses
{
    [System.ComponentModel.TypeConverterAttribute(typeof(PropertySorter))]
    public interface I_BaseClass
    {
        event EventHandler WasChangesChanged;

        event EventHandler WasChangesSecondsChanged;

        [PropertyOrder(10)]
        [DynamicBrowsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.Editor(typeof(BooleanEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [System.ComponentModel.TypeConverter(typeof(BooleanTypeConverter))]
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("0. Базовые значения")]
        [System.ComponentModel.DisplayName("Были изменения")]
        [System.ComponentModel.Description("Показывает были ли изменения в значениях свойств.")]
        bool WasChanges { get; set; }

        [PropertyOrder(11)]
        [DynamicBrowsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.Editor(typeof(BooleanEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [System.ComponentModel.TypeConverter(typeof(BooleanTypeConverter))]
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("0. Базовые значения")]
        [System.ComponentModel.DisplayName("Были изменения второстепенные")]
        [System.ComponentModel.Description("Показывает были ли второстепенные изменения в значениях свойств.")]
        bool WasChangesSoconds { get; set; }

        void WasChangesSetFalse();
        void WasChangesSecondsSetFalse();
        string VersionDefault();
        bool VersionIsDefault();
        bool VersionIsNext();
        bool VersionIsNextOrDefault();
        bool VersionIsPrevious();
        [DynamicBrowsableAttribute(false)]
        [PropertyOrder(40)]
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.Category("0. Базовые значения")]
        [System.ComponentModel.DisplayName("Версия файла")]
        [System.ComponentModel.Description("Версия файла.")]
        string Version { get; set; }
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DefaultValue(System.Windows.Forms.PropertySort.Categorized)]
        [PropertyOrder(-1000)]
        [System.ComponentModel.Category("Не для дизайнера")]
        [System.ComponentModel.DisplayName("Сортировка свойств")]
        [System.ComponentModel.Description("Тип сортировки свойств")]
        PropertySort PropertySort { get; set; }
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DefaultValue(true)]
        [PropertyOrder(-1000)]
        [System.ComponentModel.Category("Не для дизайнера")]
        [System.ComponentModel.DisplayName("Динамическое отображение свойств")]
        [System.ComponentModel.Description("Динамическое отображение свойств True - отображать все свойства; False - отображать согласно атрибуту DynamicBrowsableAttribute")]
        bool DynamicBrowsable { get; set; }
        string GetReplacerText(string source, string prefix = null);
    }

}
