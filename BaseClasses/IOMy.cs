using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaseClasses
{
    public class IOMy
    {
        public class Path
        {
            public static string Combine(string path1, string path2)
            {
                path1 = PathDefault.ReplaceErrorInPath(path1);
                path2 = PathDefault.ReplaceErrorInPath(path2);
                if (path1 == null)
                {
                    return path2;
                }
                else
                {
                    if (path2 == null)
                    {
                        return path1;
                    }
                    else
                    {
                        return System.IO.Path.Combine(path1, path2);
                    }
                }
            }
            public static string GetDirectoryName(string path)
            {
                if (path == null)
                {
                    return null;
                }
                else
                {
                    return System.IO.Path.GetDirectoryName(path);
                }
            }
            public static string GetFileNameWithoutExtension(string path)
            {
                if (path == null)
                {
                    return null;
                }
                else
                {
                    return System.IO.Path.GetFileNameWithoutExtension(path);
                }
            }
        }
        public class File
        {
            public static bool Exists(string path, ActionProvider information)
            {
                return Exists(path);
            }
            public static bool Exists(string path)
            {
                try
                {
                    return System.IO.File.Exists(path);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            public static bool DeleteInMyTrash(string path, string trashDir, string trashSubDir, bool showErrorMessage = true, string messegeIfError = null)
            {
                if (IOMy.File.Exists(path) && IOMy.Directory.Exists(trashDir))
                {
                    try
                    {
                        trashDir = PathDefault.GetDirectory(IOMy.Path.Combine(trashDir, trashSubDir));
                        trashDir = System.IO.Path.Combine(trashDir, System.IO.Path.GetFileName(path));
                        int n = 0;
                        while (true)
                        {
                            if (IOMy.File.Exists(trashDir + "_" + n))
                            {
                                n += 1;
                            }
                            else
                            {
                                trashDir += "_" + n;
                                break; // TODO: might not be correct. Was : Exit While
                            }
                        }
                        System.IO.File.Move(path, trashDir);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        if (showErrorMessage)
                        {
                            if (messegeIfError == null)
                            {
                                messegeIfError = ex.Message;
                            }
                            MessageBox.Show(messegeIfError, "Ошибка удаления в свою корзину!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            public static bool Delete(string path, bool showErrorMessage = true, string messegeIfError = null)
            {
                if (IOMy.File.Exists(path))
                {
                    try
                    {
                        System.IO.File.Delete(path);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        if (showErrorMessage)
                        {
                            if (messegeIfError == null)
                            {
                                messegeIfError = ex.Message;
                            }
                            MessageBox.Show(messegeIfError, "Ошибка удаления!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            public static bool Copy(string sourceFileName, string destFileName, bool overwrite, bool askOverwrite, ActionProvider information, bool showErrorMessage = true, string messegeIfError = null)
            {
                try
                {
                    if (IOMy.File.Exists(sourceFileName))
                    {
                        if (IOMy.File.Exists(destFileName))
                        {
                            if (askOverwrite)
                            {
                                System.DateTime _sourceDate = System.IO.File.GetLastWriteTime(sourceFileName);
                                System.DateTime _destDate = System.IO.File.GetLastWriteTime(destFileName);
                                string _timeString = "Заменяемый файл ";
                                if (_destDate.Ticks > _sourceDate.Ticks)
                                {
                                    _timeString += "НОВЕЕ";
                                }
                                else if (_destDate.Ticks < _sourceDate.Ticks)
                                {
                                    _timeString += "СТАРЕЕ";
                                }
                                else if (_destDate.Ticks == _sourceDate.Ticks)
                                {
                                    _timeString += "ТАКОЙ ЖЕ ДАТЫ";
                                }

                                _timeString += "\r\n" + "(заменяемый файл " + _destDate.ToString("dd.MM.yyyy hh:mm:ss") + "; новый файл " + _sourceDate.ToString("dd.MM.yyyy hh:mm:ss") + ")";
                                MessageBoxDefaultButton _defaultButton;
                                if (_timeString == "СТАРЕЕ")
                                {
                                    _defaultButton = MessageBoxDefaultButton.Button1;
                                }
                                else
                                {
                                    _defaultButton = MessageBoxDefaultButton.Button2;
                                }
                                if ((information.Show("Файл " + System.IO.Path.GetFileName(destFileName) + " уже существует!" + "\r\n" + _timeString + "\r\n" + "Заменить его новым?", "Файл уже существует", MessageBoxButtons.YesNo, MessageBoxIcon.Question, _defaultButton) == DialogResult.Yes))
                                {
                                    System.IO.File.Copy(sourceFileName, destFileName, true);
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        System.IO.File.Copy(sourceFileName, destFileName, overwrite);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Файл не найден!" + "\r\n" + sourceFileName, "Ошибка копирования!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    if (showErrorMessage)
                    {
                        if (messegeIfError == null)
                        {
                            messegeIfError = ex.Message;
                        }
                        MessageBox.Show(messegeIfError, "Ошибка копирования!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return false;
                }
            }
            public static System.DateTime GetCreationTime(string path)
            {
                try
                {
                    return System.IO.File.GetCreationTime(path);
                }
                catch (Exception ex)
                {
                    return new System.DateTime();
                }
            }
            public static System.DateTime GetLastWriteTime(string path)
            {
                try
                {
                    return System.IO.File.GetLastWriteTime(path);
                }
                catch (Exception ex)
                {
                    return new System.DateTime();
                }
            }
        }
        public class Directory
        {
            public static bool Exists(string path, ActionProvider information)
            {
                return Exists(path);
            }
            public static bool Exists(string path)
            {
                try
                {
                    return System.IO.Directory.Exists(path);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            public static bool DeleteInMyTrash(string path, string trashDir, string trashSubDir, bool showErrorMessage = true, string messegeIfError = null)
            {
                if (IOMy.Directory.Exists(path) && IOMy.Directory.Exists(trashDir))
                {
                    try
                    {
                        trashDir = PathDefault.GetDirectory(IOMy.Path.Combine(trashDir, trashSubDir));
                        trashDir = System.IO.Path.Combine(trashDir, System.IO.Path.GetFileName(path));
                        int n = 0;
                        while (true)
                        {
                            if (IOMy.Directory.Exists(trashDir + "_" + n))
                            {
                                n += 1;
                            }
                            else
                            {
                                trashDir += "_" + n;
                                break; // TODO: might not be correct. Was : Exit While
                            }
                        }
                        System.IO.Directory.Move(path, trashDir);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        if (showErrorMessage)
                        {
                            if (messegeIfError == null)
                            {
                                messegeIfError = ex.Message;
                            }
                            MessageBox.Show(messegeIfError, "Ошибка удаления в свою корзину!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            public static bool Delete(string path, bool showErrorMessage = true, string messegeIfError = null)
            {
                if (IOMy.Directory.Exists(path))
                {
                    try
                    {
                        System.IO.Directory.Delete(path, true);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        if (showErrorMessage)
                        {
                            if (messegeIfError == null)
                            {
                                messegeIfError = ex.Message;
                            }
                            MessageBox.Show(messegeIfError, "Ошибка удаления!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            public static bool CreateDirectory(string path, bool showErrorMessage = true, string messegeIfError = null)
            {
                try
                {
                    System.IO.Directory.CreateDirectory(path);
                    return true;
                }
                catch (Exception ex)
                {
                    if (showErrorMessage)
                    {
                        if (messegeIfError == null)
                        {
                            messegeIfError = ex.Message;
                        }
                        MessageBox.Show(messegeIfError, "Ошибка при создании директории!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return false;
                }
            }
            public static System.DateTime GetCreationTime(string path)
            {
                try
                {
                    return System.IO.Directory.GetCreationTime(path);
                }
                catch (Exception ex)
                {
                    return new System.DateTime();
                }
            }
            public static System.DateTime GetLastWriteTime(string path)
            {
                try
                {
                    return System.IO.Directory.GetLastWriteTime(path);
                }
                catch (Exception ex)
                {
                    return new System.DateTime();
                }
            }
        }
    }

}
