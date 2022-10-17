using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections;

namespace BaseClasses
{
    public class PropertySorter : System.ComponentModel.ExpandableObjectConverter
    {
        public override bool GetPropertiesSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, System.Attribute[] attributes)
        {
            PropertyDescriptorCollection pdc;
            if (((_BaseClass)value).PropertySort == PropertySort.Alphabetical || ((_BaseClass)value).PropertySort == PropertySort.CategorizedAlphabetical)
            {
                pdc = TypeDescriptor.GetProperties(value, attributes);
                pdc = pdc.Sort();
            }
            else
            {
                pdc = TypeDescriptor.GetProperties(value, attributes);
                ArrayList orderedProperties = new ArrayList();
                foreach (PropertyDescriptor pd in pdc)
                {
                    //+++???
                    //Attribute attribute = pd.Attributes(typeof(PropertyOrderAttribute));
                    //if ((attribute != null))
                    //{
                    //    PropertyOrderAttribute poa = (PropertyOrderAttribute)attribute;
                    //    orderedProperties.Add(new PropertyOrderPair(pd.Name, poa.Order));
                    //}
                    //else
                    //{
                    //    orderedProperties.Add(new PropertyOrderPair(pd.Name, 0));
                    //}
                }
                orderedProperties.Sort();
                ArrayList propertyNames = new ArrayList();
                foreach (PropertyOrderPair pop in orderedProperties)
                {
                    propertyNames.Add(pop.Name);
                }
                pdc = pdc.Sort((string[])propertyNames.ToArray(typeof(string)));
            }
            if (!((_BaseClass)value).DynamicBrowsable)
            {
                for (int i = pdc.Count - 1; i >= 0; i += -1)
                {
                    //+++???
                    //Attribute attribute = pdc[i].Attributes(typeof(DynamicBrowsableAttribute));
                    //if ((attribute != null))
                    //{
                    //    DynamicBrowsableAttribute dba = (DynamicBrowsableAttribute)attribute;
                    //    if (!dba.BrowsableAlways)
                    //    {
                    //        pdc.RemoveAt(i);
                    //    }
                    //}
                }
            }
            return pdc;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyOrderAttribute : Attribute
    {
        private int _order;
        public PropertyOrderAttribute(int order)
        {
            _order = order;
        }
        public int Order
        {
            get { return _order; }
        }
    }
    public class PropertyOrderPair : IComparable
    {
        private int _order;
        private string _name;
        public string Name
        {
            get { return _name; }
        }
        public PropertyOrderPair(string name, int order)
        {
            _order = order;
            _name = name;
        }
        int IComparable.CompareTo(object obj)
        {
            // ERROR: Not supported in C#: WithStatement
            //    With CType(obj, PropertyOrderPair)
            //    Dim otherOrder As Integer = ._order
            //    If(otherOrder = _order) Then
            //        'если Order одинаковый - сортируем по именам
            //        Dim otherName As String = ._name
            //        Return String.Compare(_name, otherName)
            //    ElseIf(otherOrder > _order) Then
            //        Return - 1
            //    End If
            //    Return 1
            //End With
            PropertyOrderPair obj2 = (PropertyOrderPair)obj;
            int otherOrder = obj2._order;
            if (otherOrder == _order)
            {
                //если Order одинаковый - сортируем по именам
                string otherName = obj2._name;
                return string.Compare(_name, otherName);
            }
            else if (otherOrder > _order)
            {
                return -1;
            }
            return 1;
        }
    }

}
