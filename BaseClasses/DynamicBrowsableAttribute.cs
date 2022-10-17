using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    internal class DynamicBrowsableAttribute : Attribute
    {
        private bool FBrowsableAlways = true;
        public bool BrowsableAlways
        {
            get { return FBrowsableAlways; }
        }
        public DynamicBrowsableAttribute(bool browsableAlways)
        {
            FBrowsableAlways = browsableAlways;
        }
    }

}
