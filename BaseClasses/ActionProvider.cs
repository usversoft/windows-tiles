using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaseClasses
{
    [Serializable()]
    public class ActionProvider
    {
        public event EventHandler ErrorAdded;

        public event EventHandler InfoAdded;

        public event EventHandler WaitBegined;

        public event EventHandler WaitEnded;

        private delegate void WithoutParams();
        private delegate bool WaitEvent_InfoAddedDelegate(int millisecondsTimeout, string infoMessage, Form ff);
        private delegate string Delegate_GetText();
        public ActionProvider()
        {
            ActionProviderNew();
            this.InfoMessages.Add(null);
        }
        public ActionProvider(ShowErrorType showError)
        {
            ActionProviderNew();
            this.InfoMessages.Add(null);
            this.ShowError = showError;
        }
        public void ActionProviderNew()
        {
            ErrorWhatMaybeAdd = null;
            showErrorNowNeed = false;
            EnabledShowError = true;
            ShowError = ShowErrorType.ErrorMessageOnly;
            errors = new System.Collections.ObjectModel.Collection<ErrorInfo>();
            infoMessages = new System.Collections.ObjectModel.Collection<string>();
            InfoMessagesWaitEvent_InfoAdded = false;
            InfoMessagesWaitEvent_InfoAddedCount = 1000;
            EnabledShowMassegeBox = true;
        }
        public enum ShowErrorType
        {
            No,
            ErrorMessageOnly,
            ErrorOnly,
            ErrorAndErrorMessage
        }

        public ErrorInfo ErrorWhatMaybeAdd { get; set; }
        private bool showErrorNowNeed = false;
        public bool EnabledShowError { get; set; }
        public ShowErrorType ShowError { get; set; }
        private System.Collections.ObjectModel.Collection<ErrorInfo> errors;
        public System.Collections.ObjectModel.Collection<ErrorInfo> Errors
        {
            get { return errors; }
        }
        private System.Collections.ObjectModel.Collection<string> infoMessages;
        public System.Collections.ObjectModel.Collection<string> InfoMessages
        {
            get { return infoMessages; }
        }
        public bool InfoMessagesWaitEvent_InfoAdded { get; set; }
        public int InfoMessagesWaitEvent_InfoAddedCount { get; set; }


        public void AddErrorFromErrorWhatMaybeAdd(Exception errorIF)
        {
            if (this.ErrorWhatMaybeAdd.Error == null)
            {
                this.ErrorWhatMaybeAdd.Error = errorIF;
            }
            AddError(this.ErrorWhatMaybeAdd);
        }
        public void AddError2(Exception errorIF, string errorMessageIF, string errorCaptionIF, MessageBoxIcon errorIconIF)
        {
            if (this.ErrorWhatMaybeAdd == null)
            {
                AddError(errorIF, errorMessageIF, errorCaptionIF, errorIconIF);
            }
            else
            {
                AddErrorFromErrorWhatMaybeAdd(errorIF);
            }
        }
        public void AddError2(ErrorInfo errorInfoIF)
        {
            if (this.ErrorWhatMaybeAdd == null)
            {
                AddError(errorInfoIF);
            }
            else
            {
                AddErrorFromErrorWhatMaybeAdd(errorInfoIF.Error);
            }
        }
        public void AddError(Exception error, string errorMessage, string errorCaption, MessageBoxIcon errorIcon)
        {
            ErrorInfo _errorInfo = new ErrorInfo();
            _errorInfo.Error = error;
            _errorInfo.ErrorMessage = errorMessage;
            _errorInfo.ErrorCaption = errorCaption;
            _errorInfo.ErrorIcon = errorIcon;
            AddError(_errorInfo);
        }
        public void AddError(ErrorInfo errorInfo)
        {
            this.Errors.Add(errorInfo);
            ErrorAdded?.Invoke(this, new System.EventArgs());
            if (!this.EnabledShowError)
            {
                return;
            }
            string _message = null;
            switch (this.ShowError)
            {
                case ShowErrorType.No:
                    return;

                case ShowErrorType.ErrorMessageOnly:
                    _message = errorInfo.ErrorMessage;
                    break;
                case ShowErrorType.ErrorOnly:
                    if (errorInfo.Error == null)
                    {
                        _message = "";
                    }
                    else
                    {
                        _message = errorInfo.Error.Message;
                    }
                    break;
                case ShowErrorType.ErrorAndErrorMessage:
                    if (errorInfo.Error == null)
                    {
                        _message = errorInfo.ErrorMessage;
                    }
                    else
                    {
                        _message = errorInfo.ErrorMessage + "\r\n" + errorInfo.Error.Message;
                    }
                    break;
            }
            showErrorNowNeed = true;
            Show(_message, errorInfo.ErrorCaption, MessageBoxButtons.OK, errorInfo.ErrorIcon);
        }
        public void AddInfo(string infoMessage)
        {
            this.InfoMessages.Add(infoMessage);
            InfoAdded?.Invoke(this, new System.EventArgs());
        }
        public void WaitBegin()
        {
            WaitBegined?.Invoke(this, new System.EventArgs());
        }
        public void WaitEnd()
        {
            WaitEnded?.Invoke(this, new System.EventArgs());
        }
        public bool CanShowMassegeBox()
        {
            if (showErrorNowNeed)
            {
                showErrorNowNeed = false;
                return true;
            }
            else
            {
                return this.EnabledShowMassegeBox;
            }
        }

        public bool EnabledShowMassegeBox { get; set; }

        public System.Windows.Forms.DialogResult Show(string text)
        {
            System.Windows.Forms.DialogResult show;
            if (this.CanShowMassegeBox())
            {
                WaitBegin();
                show = MessageBox.Show(text);
                WaitEnd();
            }
            else
            {
                return DialogResult.None;
            }
            return show;
        }
        public System.Windows.Forms.DialogResult Show(string text, string caption)
        {
            System.Windows.Forms.DialogResult show;
            if (this.CanShowMassegeBox())
            {
                WaitBegin();
                show = MessageBox.Show(text, caption);
                WaitEnd();
            }
            else
            {
                return DialogResult.None;
            }
            return show;
        }
        public System.Windows.Forms.DialogResult Show(string text, string caption, System.Windows.Forms.MessageBoxButtons buttons)
        {
            System.Windows.Forms.DialogResult show;
            if (this.CanShowMassegeBox())
            {
                WaitBegin();
                show = MessageBox.Show(text, caption, buttons);
                WaitEnd();
            }
            else
            {
                return DialogResult.None;
            }
            return show;
        }
        public System.Windows.Forms.DialogResult Show(string text, string caption, System.Windows.Forms.MessageBoxButtons buttons, System.Windows.Forms.MessageBoxIcon icon)
        {
            System.Windows.Forms.DialogResult show;
            if (this.CanShowMassegeBox())
            {
                WaitBegin();
                show = MessageBox.Show(text, caption, buttons, icon);
                WaitEnd();
            }
            else
            {
                return DialogResult.None;
            }
            return show;
        }
        public System.Windows.Forms.DialogResult Show(string text, string caption, System.Windows.Forms.MessageBoxButtons buttons, System.Windows.Forms.MessageBoxIcon icon, System.Windows.Forms.MessageBoxDefaultButton defaultButton)
        {
            System.Windows.Forms.DialogResult show;
            if (this.CanShowMassegeBox())
            {
                WaitBegin();
                show = MessageBox.Show(text, caption, buttons, icon, defaultButton);
                WaitEnd();
            }
            else
            {
                return DialogResult.None;
            }
            return show;
        }
        public System.Windows.Forms.DialogResult Show(string text, string caption, System.Windows.Forms.MessageBoxButtons buttons, System.Windows.Forms.MessageBoxIcon icon, System.Windows.Forms.MessageBoxDefaultButton defaultButton, System.Windows.Forms.MessageBoxOptions options)
        {
            System.Windows.Forms.DialogResult show;
            if (this.CanShowMassegeBox())
            {
                WaitBegin();
                show = MessageBox.Show(text, caption, buttons, icon, defaultButton, options);
                WaitEnd();
            }
            else
            {
                return DialogResult.None;
            }
            return show;
        }
        public System.Windows.Forms.DialogResult Show(string text, string caption, System.Windows.Forms.MessageBoxButtons buttons, System.Windows.Forms.MessageBoxIcon icon, System.Windows.Forms.MessageBoxDefaultButton defaultButton, System.Windows.Forms.MessageBoxOptions options, bool displayHelpButton)
        {
            System.Windows.Forms.DialogResult show;
            if (this.CanShowMassegeBox())
            {
                WaitBegin();
                show = MessageBox.Show(text, caption, buttons, icon, defaultButton, options, displayHelpButton);
                WaitEnd();
            }
            else
            {
                return DialogResult.None;
            }
            return show;
        }
        public System.Windows.Forms.DialogResult Show(string text, string caption, System.Windows.Forms.MessageBoxButtons buttons, System.Windows.Forms.MessageBoxIcon icon, System.Windows.Forms.MessageBoxDefaultButton defaultButton, System.Windows.Forms.MessageBoxOptions options, string helpFilePath)
        {
            System.Windows.Forms.DialogResult show;
            if (this.CanShowMassegeBox())
            {
                WaitBegin();
                show = MessageBox.Show(text, caption, buttons, icon, defaultButton, options, helpFilePath);
                WaitEnd();
            }
            else
            {
                return DialogResult.None;
            }
            return show;
        }
        public System.Windows.Forms.DialogResult Show(string text, string caption, System.Windows.Forms.MessageBoxButtons buttons, System.Windows.Forms.MessageBoxIcon icon, System.Windows.Forms.MessageBoxDefaultButton defaultButton, System.Windows.Forms.MessageBoxOptions options, string helpFilePath, string keyword)
        {
            System.Windows.Forms.DialogResult show;
            if (this.CanShowMassegeBox())
            {
                WaitBegin();
                show = MessageBox.Show(text, caption, buttons, icon, defaultButton, options, helpFilePath, keyword);
                WaitEnd();
            }
            else
            {
                return DialogResult.None;
            }
            return show;
        }
        public System.Windows.Forms.DialogResult Show(string text, string caption, System.Windows.Forms.MessageBoxButtons buttons, System.Windows.Forms.MessageBoxIcon icon, System.Windows.Forms.MessageBoxDefaultButton defaultButton, System.Windows.Forms.MessageBoxOptions options, string helpFilePath, System.Windows.Forms.HelpNavigator navigator)
        {
            System.Windows.Forms.DialogResult show;
            if (this.CanShowMassegeBox())
            {
                WaitBegin();
                show = MessageBox.Show(text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator);
                WaitEnd();
            }
            else
            {
                return DialogResult.None;
            }
            return show;
        }
        public System.Windows.Forms.DialogResult Show(string text, string caption, System.Windows.Forms.MessageBoxButtons buttons, System.Windows.Forms.MessageBoxIcon icon, System.Windows.Forms.MessageBoxDefaultButton defaultButton, System.Windows.Forms.MessageBoxOptions options, string helpFilePath, System.Windows.Forms.HelpNavigator navigator, object param)
        {
            System.Windows.Forms.DialogResult show;
            if (this.CanShowMassegeBox())
            {
                WaitBegin();
                show = MessageBox.Show(text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator, param);
                WaitEnd();
            }
            else
            {
                return DialogResult.None;
            }
            return show;
        }
        public System.Windows.Forms.DialogResult Show(System.Windows.Forms.IWin32Window owner, string text)
        {
            System.Windows.Forms.DialogResult show;
            if (this.CanShowMassegeBox())
            {
                WaitBegin();
                show = MessageBox.Show(owner, text);
                WaitEnd();
            }
            else
            {
                return DialogResult.None;
            }
            return show;
        }
        public System.Windows.Forms.DialogResult Show(System.Windows.Forms.IWin32Window owner, string text, string caption)
        {
            System.Windows.Forms.DialogResult show;
            if (this.CanShowMassegeBox())
            {
                WaitBegin();
                show = MessageBox.Show(owner, text, caption);
                WaitEnd();
            }
            else
            {
                return DialogResult.None;
            }
            return show;
        }
        public System.Windows.Forms.DialogResult Show(System.Windows.Forms.IWin32Window owner, string text, string caption, System.Windows.Forms.MessageBoxButtons buttons)
        {
            System.Windows.Forms.DialogResult show;
            if (this.CanShowMassegeBox())
            {
                WaitBegin();
                show = MessageBox.Show(owner, text, caption, buttons);
                WaitEnd();
            }
            else
            {
                return DialogResult.None;
            }
            return show;
        }
        public System.Windows.Forms.DialogResult Show(System.Windows.Forms.IWin32Window owner, string text, string caption, System.Windows.Forms.MessageBoxButtons buttons, System.Windows.Forms.MessageBoxIcon icon)
        {
            System.Windows.Forms.DialogResult show;
            if (this.CanShowMassegeBox())
            {
                WaitBegin();
                show = MessageBox.Show(owner, text, caption, buttons, icon);
                WaitEnd();
            }
            else
            {
                return DialogResult.None;
            }
            return show;
        }
        public System.Windows.Forms.DialogResult Show(System.Windows.Forms.IWin32Window owner, string text, string caption, System.Windows.Forms.MessageBoxButtons buttons, System.Windows.Forms.MessageBoxIcon icon, System.Windows.Forms.MessageBoxDefaultButton defaultButton)
        {
            System.Windows.Forms.DialogResult show;
            if (this.CanShowMassegeBox())
            {
                WaitBegin();
                show = MessageBox.Show(owner, text, caption, buttons, icon, defaultButton);
                WaitEnd();
            }
            else
            {
                return DialogResult.None;
            }
            return show;
        }
        public System.Windows.Forms.DialogResult Show(System.Windows.Forms.IWin32Window owner, string text, string caption, System.Windows.Forms.MessageBoxButtons buttons, System.Windows.Forms.MessageBoxIcon icon, System.Windows.Forms.MessageBoxDefaultButton defaultButton, System.Windows.Forms.MessageBoxOptions options)
        {
            System.Windows.Forms.DialogResult show;
            if (this.CanShowMassegeBox())
            {
                WaitBegin();
                show = MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options);
                WaitEnd();
            }
            else
            {
                return DialogResult.None;
            }
            return show;
        }
        public System.Windows.Forms.DialogResult Show(System.Windows.Forms.IWin32Window owner, string text, string caption, System.Windows.Forms.MessageBoxButtons buttons, System.Windows.Forms.MessageBoxIcon icon, System.Windows.Forms.MessageBoxDefaultButton defaultButton, System.Windows.Forms.MessageBoxOptions options, string helpFilePath)
        {
            System.Windows.Forms.DialogResult show;
            if (this.CanShowMassegeBox())
            {
                WaitBegin();
                show = MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options, helpFilePath);
                WaitEnd();
            }
            else
            {
                return DialogResult.None;
            }
            return show;
        }
        public System.Windows.Forms.DialogResult Show(System.Windows.Forms.IWin32Window owner, string text, string caption, System.Windows.Forms.MessageBoxButtons buttons, System.Windows.Forms.MessageBoxIcon icon, System.Windows.Forms.MessageBoxDefaultButton defaultButton, System.Windows.Forms.MessageBoxOptions options, string helpFilePath, string keyword)
        {
            System.Windows.Forms.DialogResult show;
            if (this.CanShowMassegeBox())
            {
                WaitBegin();
                show = MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options, helpFilePath, keyword);
                WaitEnd();
            }
            else
            {
                return DialogResult.None;
            }
            return show;
        }
        public System.Windows.Forms.DialogResult Show(System.Windows.Forms.IWin32Window owner, string text, string caption, System.Windows.Forms.MessageBoxButtons buttons, System.Windows.Forms.MessageBoxIcon icon, System.Windows.Forms.MessageBoxDefaultButton defaultButton, System.Windows.Forms.MessageBoxOptions options, string helpFilePath, System.Windows.Forms.HelpNavigator navigator)
        {
            System.Windows.Forms.DialogResult show;
            if (this.CanShowMassegeBox())
            {
                WaitBegin();
                show = MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator);
                WaitEnd();
            }
            else
            {
                return DialogResult.None;
            }
            return show;
        }
        public System.Windows.Forms.DialogResult Show(System.Windows.Forms.IWin32Window owner, string text, string caption, System.Windows.Forms.MessageBoxButtons buttons, System.Windows.Forms.MessageBoxIcon icon, System.Windows.Forms.MessageBoxDefaultButton defaultButton, System.Windows.Forms.MessageBoxOptions options, string helpFilePath, System.Windows.Forms.HelpNavigator navigator, object param)
        {
            System.Windows.Forms.DialogResult show;
            if (this.CanShowMassegeBox())
            {
                WaitBegin();
                show = MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator, param);
                WaitEnd();
            }
            else
            {
                return DialogResult.None;
            }
            return show;
        }
    }

}
