using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaseClasses
{
    [Serializable()]
    public class ErrorInfo
    {
        public ErrorInfo()
        {
            Error = null;
            ErrorMessage = null;
            ErrorCaption = null;
            ErrorIcon = MessageBoxIcon.None;
        }

        public Exception Error { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCaption { get; set; }
        public MessageBoxIcon ErrorIcon { get; set; }
    }

}
