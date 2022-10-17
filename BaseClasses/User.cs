using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    [System.Xml.Serialization.XmlInclude(typeof(System.Collections.ObjectModel.ObservableCollection<UserПравоДоступа>))]
    [Serializable()]
    public sealed class User
    {
        public enum eПраваДоступа
        {
            None,
            ReadOnly,
            AllRoot,
            Резерв8,
            Резерв9,
            Резерв10,
            Резерв11,
            Резерв12,
            Резерв13,
            Резерв14,
            Резерв15
        }
        public User()
        {
        }
        public User(bool callSetNull)
        {
            if (callSetNull)
            {
                SetNull();
            }
        }
        public void SetNull()
        {
            ПраваДоступа.Clear();
            ПраваДоступа.Add(new UserПравоДоступа(eПраваДоступа.ReadOnly, false));
            ПраваДоступа.Add(new UserПравоДоступа(eПраваДоступа.AllRoot, false));
            ПраваДоступа.Add(new UserПравоДоступа(eПраваДоступа.Резерв8, false));
            ПраваДоступа.Add(new UserПравоДоступа(eПраваДоступа.Резерв9, false));
            ПраваДоступа.Add(new UserПравоДоступа(eПраваДоступа.Резерв10, false));
            ПраваДоступа.Add(new UserПравоДоступа(eПраваДоступа.Резерв11, false));
            ПраваДоступа.Add(new UserПравоДоступа(eПраваДоступа.Резерв12, false));
            ПраваДоступа.Add(new UserПравоДоступа(eПраваДоступа.Резерв13, false));
            ПраваДоступа.Add(new UserПравоДоступа(eПраваДоступа.Резерв14, false));
            ПраваДоступа.Add(new UserПравоДоступа(eПраваДоступа.Резерв15, false));
        }
        public User Clone()
        {
            return (User)MemberwiseClone();
        }
        public override string ToString()
        {
            return this.Login;
        }
        internal string Login
        {
            get
            {
                if (this.Authorized)
                {
                    string login = "";
                    login = this.Фамилия;
                    if (this.Имя != "-")
                    {
                        login += " " + this.Имя;
                    }
                    if (this.Отчество != "-")
                    {
                        login += " " + this.Отчество;
                    }
                    if (this.ТабельныйНомер != "-" && this.ТабельныйНомер != "--")
                    {
                        login += " [" + this.ТабельныйНомер + "]";
                    }
                    if (this.ReadOnly)
                    {
                        login += " [только для чтения]";
                    }
                    return login;
                }
                else
                {
                    return "Авторизация не произведена";
                }
            }
        }
        internal bool Authorized
        {
            get { return (this.ТабельныйНомер != null); }
        }
        private bool FReadOnly = true;
        public bool ReadOnly
        {
            get { return FReadOnly; }
            set { FReadOnly = value; }
        }
        private string FТабельныйНомер = null;
        public string ТабельныйНомер
        {
            get { return FТабельныйНомер; }
            set { FТабельныйНомер = value; }
        }
        private string FФамилия = null;
        public string Фамилия
        {
            get { return FФамилия; }
            set { FФамилия = value; }
        }
        private string FИмя = null;
        public string Имя
        {
            get { return FИмя; }
            set { FИмя = value; }
        }
        private string FОтчество = null;
        public string Отчество
        {
            get { return FОтчество; }
            set { FОтчество = value; }
        }
        private string FPassword = null;
        public string Password
        {
            get { return FPassword; }
            set { FPassword = value; }
        }
        private System.Collections.ObjectModel.ObservableCollection<UserПравоДоступа> FПраваДоступа = new System.Collections.ObjectModel.ObservableCollection<UserПравоДоступа>();
        public System.Collections.ObjectModel.ObservableCollection<UserПравоДоступа> ПраваДоступа
        {
            get
            {
                if (FПраваДоступа == null)
                {
                    FПраваДоступа = new System.Collections.ObjectModel.ObservableCollection<UserПравоДоступа>();
                    SetNull();
                }
                return FПраваДоступа;
            }
            set { FПраваДоступа = value; }
        }
        internal bool GetПравоДоступа(eПраваДоступа типПраваДоступа)
        {
            foreach (UserПравоДоступа _item in this.ПраваДоступа)
            {
                if (_item.Право2 == типПраваДоступа)
                {
                    return _item.Разрешено;
                }
            }
            return false;
        }
        public string GetФИО()
        {
            return this.Фамилия + " " + this.Имя + " " + this.Отчество;
        }
        public string GetФамилияИнициалы()
        {
            string s = this.Фамилия + " ";
            if (this.Имя != null)
            {
                s += this.Имя.ToCharArray()[0] + ".";
            }
            if (this.Отчество != null)
            {
                s += this.Отчество.ToCharArray()[0] + ".";
            }
            return s;
        }
        [System.Serializable()]
        public sealed class UserПравоДоступа
        {
            public UserПравоДоступа()
            {
            }
            public UserПравоДоступа(eПраваДоступа правоДоступа, bool разрешено)
            {
                this.Право2 = правоДоступа;
                this.Разрешено = разрешено;
            }
            public override string ToString()
            {
                return this.Право2.ToString() + "=" + Разрешено.ToString();
            }
            private eПраваДоступа FПраво2 = eПраваДоступа.None;
            [System.Xml.Serialization.XmlIgnore()]
            public eПраваДоступа Право2
            {
                get { return FПраво2; }
                set { FПраво2 = value; }
            }
            public string Право
            {
                get { return FПраво2.ToString(); }
                set
                {
                    foreach (eПраваДоступа _item in Enum.GetValues(typeof(eПраваДоступа)))
                    {
                        if (_item.ToString() == value)
                        {
                            this.Право2 = _item;
                            return;
                        }
                    }
                    this.Право2 = eПраваДоступа.None;
                }
            }
            private bool FРазрешено = false;
            public bool Разрешено
            {
                get { return FРазрешено; }
                set { FРазрешено = value; }
            }
        }
    }

}
