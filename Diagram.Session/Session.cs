using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session
{
    public class Session
    {
        public static event EventHandler ControlPressedChanged;
        private static bool _ControlPressed = false;
        public static bool ControlPressed
        {
            get
            {
                return _ControlPressed;
            }
            set
            {
                if (_ControlPressed != value)
                {
                    _ControlPressed = value;
                    ControlPressedChanged?.Invoke(_ControlPressed, new EventArgs());
                }
            }
        }

        public static event EventHandler ShiftPressedChanged;
        private static bool _ShiftPressed = false;
        public static bool ShiftPressed
        {
            get
            {
                return _ShiftPressed;
            }
            set
            {
                if (_ShiftPressed != value)
                {
                    _ShiftPressed = value;
                    ShiftPressedChanged?.Invoke(_ShiftPressed, new EventArgs());
                }
            }
        }

        public static event EventHandler AltPressedChanged;
        private static bool _AltPressed = false;
        public static bool AltPressed
        {
            get
            {
                return _AltPressed;
            }
            set
            {
                if (_AltPressed != value)
                {
                    _AltPressed = value;
                    AltPressedChanged?.Invoke(_AltPressed, new EventArgs());
                }
            }
        }

        public static BaseClasses.ActionProvider Information { get; set; } = new BaseClasses.ActionProvider(BaseClasses.ActionProvider.ShowErrorType.ErrorAndErrorMessage);

        public static string auth_string { get; set; }
        public static string auth_session_string { get; set; }
    }
}
