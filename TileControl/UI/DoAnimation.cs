using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static TilesControl.TileControl;

namespace TilesControl
{
    [Serializable]
    public class DoAnimation
    {
        [System.Xml.Serialization.XmlIgnore]
        public TileControl TC { get; set; }
        public bool Reverse { get; set; }
        public bool NowAnimation { get; set; }
        public int N { get; set; }
        public float I { get; set; }
        public float IPlus  { get; set; }
        public float Value { get; set; }
        public float Value2 { get; set; }
        public int AnimationDelay { get; set; }
        public int AnimationStep { get; set; }
        public AnimationType AnimationType { get; set; }
    }
}
