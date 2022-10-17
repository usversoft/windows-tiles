using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TilesControl
{

    [Serializable]
    public class TileServer : Tile
    {
       
        public override string VersionDefault()
        {
            return "1.0";
        }

        [System.Xml.Serialization.XmlElement("Image")]
        [System.Xml.Serialization.XmlIgnore]
        public override byte[] ImageSerialized
        {
            get
            {
                return null;
                try
                {
                    return base.ImageSerialized;
                }
                catch (Exception ex)
                {
                    return null;
                }                
            }
            set
            {
                base.ImageSerialized = value;
            }
        }

        [System.Xml.Serialization.XmlElement("ImageOriginal")]
        [System.Xml.Serialization.XmlIgnore]
        public override byte[] ImageOriginalSerialized
        {
            get
            {
                return null;
                try
                {
                    return base.ImageOriginalSerialized;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            set
            {
                base.ImageOriginalSerialized = value;
            }
        }
       
    }
}
