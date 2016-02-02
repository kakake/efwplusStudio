using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginManageTool.Common
{
    public class MsBuildCommand
    {
        public const string MsBuildFileName = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe";

        private static string BuildArgs(string name,string args)
        {
            return string.Format(@"{0} {1}", name,args);
        }

        public static string Build(string name)
        {
            FileInfo file = new FileInfo(name);
            if (file.Exists == false) throw new Exception("需要编译的源代码项目不存在！");

            var p = new System.Diagnostics.Process();
            // Redirect the output and error information here.
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = VSOpenCommand.devenvExeFile;
            // Build the arguments string.
            if (file.Extension == ".proj")
            {
                p.StartInfo.Arguments = BuildArgs(name, "/t:Clean;ReBuild");
            }
            else if (file.Extension == ".sln")
            {
                p.StartInfo.Arguments = BuildArgs(name, "/Rebuild");
            }
            
            p.StartInfo.UseShellExecute = false;
            // Execute svn command.
            p.Start();
            p.StandardInput.WriteLine("exit");
            // Get result.
            var result = p.StandardOutput.ReadToEnd();
            //var error = p.StandardError.ReadToEnd();
            //var exit = p.ExitCode;

            return result.ToString();
        }
    }
}
