using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginManageTool.Common
{
    public class VSOpenCommand
    {
        public static string devenvExeFile = @"C:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\devenv.exe";

        private static string OpenArgs(string name)
        {
            return string.Format("{0}", name);
        }

        public static string Open(string name)
        {
            if (name.IndexOf(" ") > -1)
                System.Diagnostics.Process.Start(devenvExeFile, "\"" + name + "\"");
            else
                System.Diagnostics.Process.Start(devenvExeFile, name);
            return "";
        }
    }
}
