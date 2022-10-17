using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaseClasses
{
    [System.ComponentModel.TypeConverterAttribute(typeof(PropertySorter))]
    [System.ComponentModel.DefaultEvent("WasChangesChanged")]
    [Serializable()]
    public class _BaseClass : I_BaseClass
    {
        public _BaseClass()
        {
            wasChanges = false;
            wasChangesSoconds = false;
            dynamicBrowsable = false;
            propertySort = System.Windows.Forms.PropertySort.Categorized;
            version = VersionDefault();
        }

        public virtual string VersionDefault()
        {
            return "1.0";
        }
        public event EventHandler WasChangesChanged;

        public event EventHandler WasChangesSecondsChanged;

        private bool wasChanges;
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
        public bool WasChanges
        {
            get { return wasChanges; }
            set
            {
                wasChanges = value;
                WasChangesChanged?.Invoke(this, new System.EventArgs());
            }
        }
        private bool wasChangesSoconds;
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
        public bool WasChangesSoconds
        {
            get { return wasChangesSoconds; }
            set
            {
                wasChangesSoconds = value;
                WasChangesSecondsChanged?.Invoke(this, new System.EventArgs());
            }
        }
        public void WasChangesSetFalse()
        {
            this.WasChanges = false;
        }
        public void WasChangesSecondsSetFalse()
        {
            this.WasChangesSoconds = false;
        }
        private string version;
        [DynamicBrowsableAttribute(false)]
        [PropertyOrder(40)]
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.Category("0. Базовые значения")]
        [System.ComponentModel.DisplayName("Версия файла")]
        [System.ComponentModel.Description("Версия файла.")]
        public string Version
        {
            get { return version; }
            set
            {
                if (version != value)
                {
                    version = value;
                    this.WasChanges = true;
                }
            }
        }
        public bool VersionIsDefault()
        {
            return (this.Version == VersionDefault());
        }
        public bool VersionIsNext()
        {
            return (this.Version.CompareTo(VersionDefault()) > 0);
        }
        public bool VersionIsNextOrDefault()
        {
            return (this.Version.CompareTo(VersionDefault()) >= 0);
        }
        public bool VersionIsPrevious()
        {
            return (this.Version.CompareTo(VersionDefault()) < 0);
        }
        private bool dynamicBrowsable;
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DefaultValue(true)]
        [PropertyOrder(-1000)]
        [System.ComponentModel.Category("Не для дизайнера")]
        [System.ComponentModel.DisplayName("Динамическое отображение свойств")]
        [System.ComponentModel.Description("Динамическое отображение свойств True - отображать все свойства; False - отображать согласно атрибуту DynamicBrowsableAttribute")]
        public bool DynamicBrowsable
        {
            get { return dynamicBrowsable; }
            set { dynamicBrowsable = value; }
        }
        private PropertySort propertySort;
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DefaultValue(System.Windows.Forms.PropertySort.Categorized)]
        [PropertyOrder(-1000)]
        [System.ComponentModel.Category("Не для дизайнера")]
        [System.ComponentModel.DisplayName("Сортировка свойств")]
        [System.ComponentModel.Description("Тип сортировки свойств")]
        public System.Windows.Forms.PropertySort PropertySort
        {
            get { return propertySort; }
            set { propertySort = value; }
        }
        internal virtual string GetReplacerText(string source, string prefix = null)
        {
            string getReplacerText = source;
            return getReplacerText;
        }
        public void OnPartClass_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (_BaseClass _item in e.NewItems)
                {
                    _item.WasChangesChanged += БылиИзмененияChanged_CollectionChanged;
                    _item.WasChangesSecondsChanged += БылиИзмененияВторостепенныеChanged_CollectionChanged;
                }
            }
            if (e.OldItems != null)
            {
                foreach (_BaseClass _item in e.OldItems)
                {
                    _item.WasChangesChanged -= БылиИзмененияChanged_CollectionChanged;
                    _item.WasChangesSecondsChanged -= БылиИзмененияВторостепенныеChanged_CollectionChanged;
                }
            }
            this.WasChanges = true;
        }
        private void БылиИзмененияChanged_CollectionChanged(object sender, System.EventArgs e)
        {
            WasChangesChanged?.Invoke(sender, e);
        }
        private void БылиИзмененияВторостепенныеChanged_CollectionChanged(object sender, System.EventArgs e)
        {
            WasChangesSecondsChanged?.Invoke(sender, e);
        }

        string I_BaseClass.GetReplacerText(string source, string prefix)
        {
            throw new NotImplementedException();
        }
    }

}
