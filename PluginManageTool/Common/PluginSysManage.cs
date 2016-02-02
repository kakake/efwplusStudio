using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PluginManageTool.Common
{
    /// <summary>
    /// pluginsys.xml 解析
    /// </summary>
    public class PluginSysManage
    {
        private static System.Xml.XmlDocument xmlDoc = null;
        private static string _pluginsysFile;
        public static string pluginsysFile
        {
            get { return _pluginsysFile; }
            set
            {
                _pluginsysFile = value;
                xmlDoc = null;
            }
        }

        private static void InitConfig()
        {
            xmlDoc = new System.Xml.XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;//忽略文档里面的注释
            XmlReader reader = XmlReader.Create(pluginsysFile, settings);
            xmlDoc.Load(reader);
            reader.Close();
        }

        public static List<PluginClass> GetAllPlugin()
        {
            if (xmlDoc == null) InitConfig();
            List<PluginClass> plist = new List<PluginClass>();
            XmlNodeList nl = null;

            nl = xmlDoc.DocumentElement.SelectNodes("WebModulePlugin/Plugin");
            foreach (XmlNode n in nl)
            {
                PluginClass plugin = new PluginClass();
                plugin.plugintype = "WebModulePlugin";
                plugin.name = n.Attributes["name"].Value;
                plugin.title = n.Attributes["title"].Value;
                plugin.path = n.Attributes["path"].Value;
                plugin.isdevelopment = n.Attributes["isdevelopment"].Value;
                plist.Add(plugin);
            }
            nl = xmlDoc.DocumentElement.SelectNodes("WinformModulePlugin/Plugin");
            foreach (XmlNode n in nl)
            {
                PluginClass plugin = new PluginClass();
                plugin.plugintype = "WinformModulePlugin";
                plugin.name = n.Attributes["name"].Value;
                plugin.title = n.Attributes["title"].Value;
                plugin.path = n.Attributes["path"].Value;
                plugin.isdevelopment = n.Attributes["isdevelopment"].Value;
                plist.Add(plugin);
            }
            nl = xmlDoc.DocumentElement.SelectNodes("WcfModulePlugin/Plugin");
            foreach (XmlNode n in nl)
            {
                PluginClass plugin = new PluginClass();
                plugin.plugintype = "WcfModulePlugin";
                plugin.name = n.Attributes["name"].Value;
                plugin.title = n.Attributes["title"].Value;
                plugin.path = n.Attributes["path"].Value;
                plugin.isdevelopment = n.Attributes["isdevelopment"].Value;
                plist.Add(plugin);
            }
            return plist;
        }


        public static void AddPlugin(string pluginType, string name, string path, string title, string isdevelopment)
        {
            if (xmlDoc == null) InitConfig();
            XmlNode root = xmlDoc.DocumentElement.SelectSingleNode(pluginType);//查找   
            XmlElement xe1 = xmlDoc.CreateElement("Plugin");//创建一个节点   
            xe1.SetAttribute("name", name);//
            xe1.SetAttribute("path", path);//
            xe1.SetAttribute("title", title);//
            xe1.SetAttribute("isdevelopment", isdevelopment);//
            
            root.AppendChild(xe1);//添加到<bookstore>节点中   
            xmlDoc.Save(pluginsysFile);   
        }

        public static PluginClass GetPlugin(string pluginType, string name)
        {
            if (xmlDoc == null) InitConfig();
            XmlNodeList nl = xmlDoc.DocumentElement.SelectSingleNode(pluginType).SelectNodes("Plugin");
            foreach (XmlNode n in nl)
            {
                if (n.Attributes["name"].Value == name)
                {
                    PluginClass plugin = new PluginClass();
                    plugin.plugintype = pluginType;
                    plugin.name = n.Attributes["name"].Value;
                    plugin.title = n.Attributes["title"].Value;
                    plugin.path = n.Attributes["path"].Value;
                    plugin.isdevelopment = n.Attributes["isdevelopment"].Value;
                    return plugin;
                }
            }
            return null;
        }

        public static bool ContainsPlugin(string pluginType, string name)
        {
            if (xmlDoc == null) InitConfig();
            XmlNodeList nl = xmlDoc.DocumentElement.SelectSingleNode(pluginType).SelectNodes("Plugin");
            foreach (XmlNode n in nl)
            {
                if (n.Attributes["name"].Value == name)
                    return true;
            }
            return false;
        }

        public static void DeletePlugin(string pluginType)
        {
            if (xmlDoc == null) InitConfig();
            XmlNode xn = xmlDoc.DocumentElement.SelectSingleNode(pluginType);
            if (xn != null)
                xn.RemoveAll();
            xmlDoc.Save(pluginsysFile);
        }

        public static void DeletePlugin(string pluginType, string name)
        {
            if (xmlDoc == null) InitConfig();
            XmlNode xn = xmlDoc.DocumentElement.SelectSingleNode(pluginType + "/Plugin[@name='" + name + "']");
            if (xn != null)
                xn.ParentNode.RemoveChild(xn);
            xmlDoc.Save(pluginsysFile);
        }

        public static void GetWinformEntry(out string entryplugin, out string entrycontroller)
        {
            if (xmlDoc == null) InitConfig();

            entryplugin = xmlDoc.DocumentElement.SelectNodes("WinformModulePlugin")[0].Attributes["EntryPlugin"].Value.ToString();
            entrycontroller = xmlDoc.DocumentElement.SelectNodes("WinformModulePlugin")[0].Attributes["EntryController"].Value.ToString();
        }

        public static void SetWinformEntry( string entryplugin,  string entrycontroller)
        {
            if (xmlDoc == null) InitConfig();

            XmlNode node= xmlDoc.DocumentElement.SelectSingleNode("WinformModulePlugin");
            node.Attributes["EntryPlugin"].Value = entryplugin;
            node.Attributes["EntryController"].Value = entrycontroller;

            xmlDoc.Save(pluginsysFile);   
        }

        public static void GetWcfClientEntry(out string entryplugin, out string entrycontroller)
        {
            if (xmlDoc == null) InitConfig();

            entryplugin = xmlDoc.DocumentElement.SelectNodes("WcfModulePlugin")[0].Attributes["EntryPlugin"].Value.ToString();
            entrycontroller = xmlDoc.DocumentElement.SelectNodes("WcfModulePlugin")[0].Attributes["EntryController"].Value.ToString();
        }

        public static void SetWcfClientEntry(string entryplugin, string entrycontroller)
        {
            if (xmlDoc == null) InitConfig();

            XmlNode node = xmlDoc.DocumentElement.SelectSingleNode("WcfModulePlugin");
            node.Attributes["EntryPlugin"].Value = entryplugin;
            node.Attributes["EntryController"].Value = entrycontroller;

            xmlDoc.Save(pluginsysFile);
        }
    }

    public class PluginClass
    {
        public string plugintype { get; set; }

        public string name { get; set; }
        public string title { get; set; }
        public string path { get; set; }
        public string isdevelopment { get; set; }
    }

    /// <summary>
    /// plugin.xml 解析
    /// </summary>
    public class PluginXmlManage
    {
        private static System.Xml.XmlDocument xmlDoc = null;
        private static string _pluginfile;
        public static string pluginfile
        {
            get { return _pluginfile; }
            set
            {
                xmlDoc = null;
                _pluginfile = value;
            }
        }

        private static void InitConfig()
        {
            xmlDoc = new System.Xml.XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;//忽略文档里面的注释
            XmlReader reader = XmlReader.Create(pluginfile, settings);
            xmlDoc.Load(reader);
            reader.Close();
        }

        public static pluginxmlClass getpluginclass()
        {
            if (xmlDoc == null) InitConfig();


            pluginxmlClass plugin = new pluginxmlClass();
            plugin.data = new List<baseinfodataClass>();
            plugin.dll = new List<businessinfodllClass>();
            plugin.issue = new List<issueClass>();
            plugin.setup = new List<setupClass>();
            plugin.menu = new List<menuClass>();

            XmlNode node = xmlDoc.DocumentElement.SelectSingleNode("plugin");
            plugin.name = node.Attributes["name"].Value;
            plugin.title = node.Attributes["title"].Value;
            plugin.version = node.Attributes["version"].Value;
            plugin.author = node.Attributes["author"].Value;
            plugin.plugintype = node.Attributes["plugintype"].Value;
            plugin.defaultdbkey = node.Attributes["defaultdbkey"].Value;
            plugin.defaultcachekey = node.Attributes["defaultcachekey"].Value;
            plugin.isentry = node.Attributes["isentry"].Value;

            //node = xmlDoc.DocumentElement.SelectSingleNode("plugin/baseinfo/data[@key='introduction']");
            //plugin.introduction = node == null ? "" : node.Attributes["value"].Value;
            //node = xmlDoc.DocumentElement.SelectSingleNode("plugin/baseinfo/data[@key='updaterecord']");
            //plugin.updaterecord = node == null ? "" : node.Attributes["value"].Value;
            //node = xmlDoc.DocumentElement.SelectSingleNode("plugin/baseinfo/data[@key='headpic']");
            //plugin.headpic = node == null ? "" : node.Attributes["value"].Value;
            //node = xmlDoc.DocumentElement.SelectSingleNode("plugin/baseinfo/data[@key='StartItem']");
            //plugin.StartItem = node == null ? "" : node.Attributes["value"].Value;
            XmlNodeList nlist = null;
            if (xmlDoc.DocumentElement.SelectSingleNode("plugin/baseinfo") != null)
            {
                nlist = xmlDoc.DocumentElement.SelectSingleNode("plugin/baseinfo").ChildNodes;
                foreach (XmlNode n in nlist)
                {
                    baseinfodataClass data = new baseinfodataClass();
                    data.key = n.Attributes["key"].Value;
                    data.value = n.Attributes["value"].Value;

                    plugin.data.Add(data);
                }
            }
            if (xmlDoc.DocumentElement.SelectSingleNode("plugin/businessinfo") != null)
            {
                nlist = xmlDoc.DocumentElement.SelectSingleNode("plugin/businessinfo").ChildNodes;
                foreach (XmlNode n in nlist)
                {
                    businessinfodllClass dll = new businessinfodllClass();
                    dll.name = n.Attributes["name"].Value;
                    dll.version = n.Attributes["version"].Value;

                    plugin.dll.Add(dll);
                }
            }

            if (xmlDoc.DocumentElement.SelectSingleNode("plugin/issue") != null)
            {
                nlist = xmlDoc.DocumentElement.SelectSingleNode("plugin/issue").ChildNodes;
                foreach (XmlNode n in nlist)
                {
                    issueClass ic = new issueClass();
                    ic.type = n.Attributes["type"].Value;
                    ic.path = n.Attributes["path"].Value;
                    ic.source = n.Attributes["source"] == null ? "" : n.Attributes["source"].Value;

                    plugin.issue.Add(ic);
                }
            }

            if (xmlDoc.DocumentElement.SelectSingleNode("plugin/setup") != null)
            {
                nlist = xmlDoc.DocumentElement.SelectSingleNode("plugin/setup").ChildNodes;
                foreach (XmlNode n in nlist)
                {
                    setupClass sc = new setupClass();
                    sc.type = n.Attributes["type"].Value;
                    sc.path = n.Attributes["path"].Value;
                    sc.copyto = n.Attributes["copyto"] == null ? "" : n.Attributes["copyto"].Value;
                    plugin.setup.Add(sc);
                }
            }

            if (xmlDoc.DocumentElement.SelectSingleNode("plugin/menus") != null)
            {
                nlist = xmlDoc.DocumentElement.SelectSingleNode("plugin/menus").ChildNodes;
                foreach (XmlNode n in nlist)
                {
                    menuClass mc = new menuClass();
                    mc.menuname = n.Attributes["menuname"].Value;
                    mc.pluginname = n.Attributes["pluginname"].Value;
                    mc.controllername = n.Attributes["controllername"].Value;
                    mc.viewname = n.Attributes["viewname"].Value;
                    mc.menupath = n.Attributes["menupath"].Value;
                    mc.memo = n.Attributes["memo"].Value;
                    plugin.menu.Add(mc);
                }
            }
			
            return plugin;
        }

        public static void savepluginclass(pluginxmlClass pluginclass)
        {
            if (pluginclass == null) return;

            if (xmlDoc == null) InitConfig();

            XmlNode pluginN = xmlDoc.DocumentElement.SelectSingleNode("plugin");
            pluginN.Attributes["name"].Value = pluginclass.name;
            pluginN.Attributes["title"].Value = pluginclass.title;
            pluginN.Attributes["version"].Value = pluginclass.version;
            pluginN.Attributes["author"].Value = pluginclass.author;
            pluginN.Attributes["plugintype"].Value = pluginclass.plugintype;
            pluginN.Attributes["defaultdbkey"].Value = pluginclass.defaultdbkey;
            pluginN.Attributes["defaultcachekey"].Value = pluginclass.defaultcachekey;
            pluginN.Attributes["isentry"].Value = pluginclass.isentry;
            XmlNode dataN = pluginN.SelectSingleNode("baseinfo");
            if (dataN != null)
            {
                pluginN.RemoveChild(dataN);
            }

            XmlNode dllN = pluginN.SelectSingleNode("businessinfo");
            if (dllN != null)
            {
                pluginN.RemoveChild(dllN);
            }

            XmlNode issueN = pluginN.SelectSingleNode("issue");
            if (issueN != null)
            {
                pluginN.RemoveChild(issueN);
            }

            XmlNode setupN = pluginN.SelectSingleNode("setup");
            if (setupN != null)
            {
                pluginN.RemoveChild(setupN);
            }

            XmlNode menuN = pluginN.SelectSingleNode("menus");
            if (menuN != null)
            {
                pluginN.RemoveChild(menuN);
            }

            if (pluginclass.data.Count > 0)
            {
                XmlElement xe1 = xmlDoc.CreateElement("baseinfo");//创建一个节点   
                pluginN.AppendChild(xe1);
            }
            if (pluginclass.dll.Count > 0)
            {
                XmlElement xe1 = xmlDoc.CreateElement("businessinfo");//创建一个节点   
                pluginN.AppendChild(xe1);
            }
            if (pluginclass.issue.Count > 0)
            {
                XmlElement xe1 = xmlDoc.CreateElement("issue");//创建一个节点   
                pluginN.AppendChild(xe1);
            }
            if (pluginclass.setup.Count > 0)
            {
                XmlElement xe1 = xmlDoc.CreateElement("setup");//创建一个节点   
                pluginN.AppendChild(xe1);
            }
            if (pluginclass.menu.Count > 0)
            {
                XmlElement xe1 = xmlDoc.CreateElement("menus");//创建一个节点   
                pluginN.AppendChild(xe1);
            }

            for (int i = 0; i < pluginclass.data.Count; i++)
            {
                XmlElement xe1 = xmlDoc.CreateElement("data");//创建一个节点
                xe1.SetAttribute("key", pluginclass.data[i].key);
                xe1.SetAttribute("value", pluginclass.data[i].value);
                pluginN.SelectSingleNode("baseinfo").AppendChild(xe1);
            }

            for (int i = 0; i < pluginclass.dll.Count; i++)
            {
                XmlElement xe1 = xmlDoc.CreateElement("dll");//创建一个节点
                xe1.SetAttribute("name", pluginclass.dll[i].name);
                xe1.SetAttribute("version", pluginclass.dll[i].version);
                pluginN.SelectSingleNode("businessinfo").AppendChild(xe1);
            }

            for (int i = 0; i < pluginclass.issue.Count; i++)
            {
                XmlElement xe1 = xmlDoc.CreateElement("add");//创建一个节点
                xe1.SetAttribute("type", pluginclass.issue[i].type);
                xe1.SetAttribute("path", pluginclass.issue[i].path);
                xe1.SetAttribute("source", pluginclass.issue[i].source);
                pluginN.SelectSingleNode("issue").AppendChild(xe1);
            }

            for (int i = 0; i < pluginclass.setup.Count; i++)
            {
                XmlElement xe1 = xmlDoc.CreateElement("add");//创建一个节点
                xe1.SetAttribute("type", pluginclass.setup[i].type);
                xe1.SetAttribute("path", pluginclass.setup[i].path);
                xe1.SetAttribute("copyto", pluginclass.setup[i].copyto);
                pluginN.SelectSingleNode("setup").AppendChild(xe1);
            }

            for (int i = 0; i < pluginclass.menu.Count; i++)
            {
                XmlElement xe1 = xmlDoc.CreateElement("add");//创建一个节点
                xe1.SetAttribute("menuname", pluginclass.menu[i].menuname);
                xe1.SetAttribute("pluginname", pluginclass.menu[i].pluginname);
                xe1.SetAttribute("controllername", pluginclass.menu[i].controllername);
                xe1.SetAttribute("viewname", pluginclass.menu[i].viewname);
                xe1.SetAttribute("menupath", pluginclass.menu[i].menupath);
                xe1.SetAttribute("memo", pluginclass.menu[i].memo);
                pluginN.SelectSingleNode("menus").AppendChild(xe1);
            }
            xmlDoc.Save(pluginfile);
        }
    }

    public class pluginxmlClass{
        
        public string name { get; set; }
        public string title { get; set; }
        public string version { get; set; }
        public string author { get; set; }
        public string plugintype { get; set; }
        public string defaultdbkey { get; set; }
        public string defaultcachekey { get; set; }
        public string isentry { get; set; }

        //public string introduction { get; set; }
        //public string updaterecord { get; set; }
        //public string headpic { get; set; }
        //public string StartItem { get; set; }

        public List<baseinfodataClass> data { get; set; }
        public List<businessinfodllClass> dll { get; set; }
        public List<issueClass> issue { get; set; }
        public List<setupClass> setup { get; set; }
		public List<menuClass> menu { get; set; }
    }

    public class baseinfodataClass
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class businessinfodllClass
    {
        public string name { get; set; }
        public string version { get; set; }
    }

    public class issueClass
    {
        public string type { get; set; }
        public string path { get; set; }
        public string source { get; set; }
    }

    public class setupClass
    {
        public string type { get; set; }
        public string path { get; set; }
        public string copyto { get; set; }
    }

    public class menuClass
    {
        public string menuname { get; set; }
		public string pluginname { get; set; }
		public string controllername { get; set; }
		public string viewname { get; set; }
		public string menupath { get; set; }
		public string memo { get; set; }
    }
}
