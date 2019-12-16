﻿using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

namespace Nt.Services
{

    class Service
    {
        public enum Status
        {
            Running = 0,
            Stopped = 1,
            Paused = 2,
            StartPending = 3,
            StopPending = 4,
            Uninstalled = 5,
            Unknown = 6
        }
        public static Status GetStatus(string serviceName)
        {
            var serviceController = new ServiceController(serviceName);

            try
            {
                switch (serviceController.Status)
                {
                    case ServiceControllerStatus.Running:
                        return Status.Running;
                    case ServiceControllerStatus.Stopped:
                        return Status.Stopped;
                    case ServiceControllerStatus.Paused:
                        return Status.Paused;
                    case ServiceControllerStatus.StartPending:
                        return Status.StartPending;
                    case ServiceControllerStatus.StopPending:
                        return Status.StopPending;
                    default:
                        return Status.Unknown;
                }
            }
            catch
            {
                return Status.Uninstalled;
            }
        }

        public static void Install(string serviceName, string binPath)
        {
            Logging.Log.Service.Info("install service " + serviceName + " at " + binPath);
            //install service
            var builder = new StringBuilder();
            builder.Append(" create \"").Append(serviceName).Append("\"");
            builder.Append(" binPath=\"").Append(binPath).Append("\"");
            RunServiceControl(builder.ToString());
            //configure service to auto - service automatically started at boot time, even if no user logs on
            builder = new StringBuilder();
            builder.Append(" create \"").Append(serviceName).Append("\"");
            builder.Append(" start=auto");
            RunServiceControl(builder.ToString());
        }

        public static void Uninstall(string serviceName)
        {
            Logging.Log.Service.Info("uninstall service \n" + serviceName + "\"");
            var builder = new StringBuilder();
            builder.Append(" delete \"").Append(serviceName).Append("\"");
            RunServiceControl(builder.ToString());
        }

        public static void Start(string serviceName)
        {
           Logging.Log.Service.Info("start service \n" + serviceName + "\"");
            //var serviceController = new ServiceController(serviceName);
            //serviceController.Start();
            var builder = new StringBuilder();
            builder.Append(" start \"").Append(serviceName).Append("\"");
            RunServiceControl(builder.ToString());
        }

        public static void Start(string serviceName, string[] args)
        {
            Logging.Log.Service.Info("start service " + serviceName + " " + string.Join(" ", args));
            var serviceController = new ServiceController(serviceName);
            serviceController.Start(args);
        }

        public static void Stop(string serviceName)
        {
            Logging.Log.Service.Info("stop service " + serviceName);
            var serviceController = new ServiceController(serviceName);
            serviceController.Stop();
        }

        private static void RunServiceControl(string argument)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = @"sc.exe";
                process.StartInfo.Arguments = argument;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.RedirectStandardOutput = true;
                Logging.Log.Service.Info("sc.exe" + argument);
                process.Start();
                process.WaitForExit();

                var message = process.StandardOutput.ReadToEnd();
                if (!string.IsNullOrEmpty(message))
                {
                    Logging.Log.Service.Info(message);
                }
            }
        }
    }
}