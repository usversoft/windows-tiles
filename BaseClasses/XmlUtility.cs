using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace BaseClasses
{

    /*internal*/ public class XmlUtility
    {
        static public bool WriteObj(string fileName, System.Xml.Serialization.XmlAttributeOverrides overrides, object value, string nameSpace, ref ActionProvider information)
        {
            if (fileName == null)
                return false;
            try
            {
                PathDefault.GetDirectory(IOMy.Path.GetDirectoryName(fileName));
                XmlSerializer writer;
                if (overrides == null) {
                    writer = new XmlSerializer(value.GetType());
                } else {
                  writer  = new XmlSerializer(value.GetType(), overrides);
                }
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
                {
                    writer.Serialize(file, value, new XmlSerializerNamespaces(new XmlQualifiedName[] { new XmlQualifiedName("", nameSpace) }));
                    file.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                information.AddError2(ex, "Ошибка при сохранении файла " + fileName, "Ошибка при записи!", MessageBoxIcon.Error);
                return false;
            }
        }
        static public bool WriteObj(string fileName, System.Xml.Serialization.XmlAttributeOverrides overrides, object value, ref ActionProvider information)
        {
            if (fileName == null)
                return false;
            try
            {
                PathDefault.GetDirectory(IOMy.Path.GetDirectoryName(fileName));
                XmlSerializer writer;
                if (overrides == null)
                {
                    writer = new XmlSerializer(value.GetType());
                }
                else
                {
                    writer = new XmlSerializer(value.GetType(), overrides);
                }
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
                {
                    writer.Serialize(file, value, new XmlSerializerNamespaces(new XmlQualifiedName[] { new XmlQualifiedName(string.Empty) }));
                    file.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                information.AddError2(ex, "Ошибка при сохранении файла " + fileName, "Ошибка при записи!", MessageBoxIcon.Error);
                return false;
            }
        }
        static public object ReadObj(string fileName, Type type, ref ActionProvider information)
        {
            if ((fileName == null))
                return null;
            if ((fileName == string.Empty))
                return Activator.CreateInstance(type);
            if (IOMy.File.Exists(fileName, information))
            {
                try
                {
                    object readObj = null;
                    XmlSerializer reader = new XmlSerializer(type);
                    using (System.IO.StreamReader file1 = new System.IO.StreamReader(fileName))
                    {
                        readObj = reader.Deserialize(file1);
                        file1.Close();
                    }
                    return readObj;
                }
                catch (Exception ex)
                {
                    information.AddError2(ex, "Ошибка при чтении файла " + fileName, "Ошибка при чтении!", MessageBoxIcon.Error);
                    return Activator.CreateInstance(type);
                }
            }
            else
            {
                return Activator.CreateInstance(type);
            }
        }
    }

}
