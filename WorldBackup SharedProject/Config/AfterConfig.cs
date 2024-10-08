﻿using com.Lavaver.WorldBackup.Core;
using com.Lavaver.WorldBackup.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace com.Lavaver.WorldBackup
{
    internal class AfterConfig
    {
        public static void Run()
        {
            while (true)
            {
                LogConsole.Log("配置","请选择一个选项",ConsoleColor.Blue);
                Console.WriteLine("1. 来源文件夹");
                Console.WriteLine("2. 备份到");
                Console.WriteLine("3. NTP 服务器");
                Console.WriteLine("4. WebDAV 用户名");
                Console.WriteLine("5. WebDAV 密码");
                Console.WriteLine("6. WebDAV 服务器地址");
                Console.WriteLine("0. 退出");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        HandleChoice("source");
                        break;
                    case "2":
                        HandleChoice("backupto");
                        break;
                    case "3":
                        HandleChoice("NTP-Server");
                        break;
                    case "4":
                        HandleChoice("DAVUserName");
                        break;
                    case "5":
                        HandleChoice("DAVPassword");
                        break;
                    case "6":
                        HandleChoice("DAVHost");
                        break;
                    case "0":
                        return;
                    default:
                        LogConsole.Log("配置", "选项无效", ConsoleColor.Red);
                        break;
                }
            }
        }

        public static void HandleChoice(string elementName)
        {
            try
            {
                XDocument doc = XDocument.Load(GlobalString.SoftwareConfigLocation);
                XElement element = doc.Root.Element(elementName);

                if (element != null)
                {
                    LogConsole.Log("配置", $"当前配置为 {element.Value}", ConsoleColor.Blue);
                    Console.Write("输入新路径：");
                    string newPath = Console.ReadLine();

                    if (!string.IsNullOrEmpty(newPath))
                    {
                        element.Value = newPath;
                        doc.Save(GlobalString.SoftwareConfigLocation);
                        Console.WriteLine("配置已更新。");
                    }
                    else
                    {
                        LogConsole.Log("配置", "配置未更新。因为你没有填写配置", ConsoleColor.Red);
                    }
                }
                else
                {
                    LogConsole.Log("配置", $"没有元素 {elementName}", ConsoleColor.Red);
                }
            }
            catch (Exception ex)
            {
                LogConsole.Log("配置", $"{ex.Message}", ConsoleColor.Red);
            }
        }
    }
}
