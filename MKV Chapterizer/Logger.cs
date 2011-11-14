using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Reflection;

namespace Logger
{
    public class Log
    {

        public enum Type
        {
            [Description(" [INFO]")]
            Info,
            [Description("[ERROR]")]
            Error,
        }

        private string GetEnumDescription(Enum value)
        {
            // Get the Description attribute value for the enum value
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        private string pPath;

        public string Path
        {
            get { return pPath; }
            set { pPath = value; }
        }

        public Log (string path)
        {
            //Check if a log already exists at the path
            if (File.Exists(path))
            {
                //Rename the old log to .old and declare new log
                try
                {
                    File.Delete(path + ".old");
                    File.Move(path, path + ".old");
                    pPath = path;
                }
                catch (Exception)
                {
                }
            }
            else
            {
                pPath = path;
            }
        }

        public void Write(string message)
        {
            WriteLog(message, Type.Info);
        }

        public void Write(string message, Type type)
        {
            WriteLog(message, type);
        }

        private void WriteLog(string message, Type type)
        {
            string newLine;
            if (File.Exists(Path))
            {
                newLine = "\r\n";
            }
            else
            {
                newLine = string.Empty;
            }

            string value = string.Format("{0}{1:HH:mm:ss} {2} {3}", newLine, DateTime.Now, GetEnumDescription(type), message);

            try
            {
                File.AppendAllText(Path, value);
            }
            catch (Exception)
            {
            }
        }
    }
}
