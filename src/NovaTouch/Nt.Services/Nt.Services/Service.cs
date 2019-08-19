using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceProcess;
using System.Diagnostics;

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
            var builder = new StringBuilder();
            builder.Append(" create ").Append(serviceName);
            builder.Append(" binPath=\"").Append(binPath).Append("\"");
            RunServiceControl(builder.ToString());
        }

        public static void Uninstall(string serviceName)
        {
            var builder = new StringBuilder();
            builder.Append(" delete ").Append(serviceName);
            RunServiceControl(builder.ToString());
        }

        public static void Start(string serviceName, string[] args)
        {
                var serviceController = new ServiceController(serviceName);
                serviceController.Start(args);
        }

        public static void Stop(string serviceName)
        {
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
                process.Start();
                process.WaitForExit();
                var message = process.StandardOutput.ReadToEnd();
            }
        }
    }
}