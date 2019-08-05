using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceProcess;

namespace Nt.Services
{

    class Service
    {
        public enum Status
        {
            Running = 0,
            Stopped = 1,
            Paused = 2,
            StartPending =3,
            StopPending = 4,
            Uninstalled = 5,
            Unknown=6
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

        public static bool Install(string serviceName)
        {
            var args = new string[] { serviceName };
            return InstallService(args);
        }

        public static bool Uninstall(string serviceName)
        {
            var args = new string[] { "/u", serviceName };
            return InstallService(args);
        }

        private static bool InstallService(string[] args)
        {
            try
            {
                System.Configuration.Install.ManagedInstallerClass.InstallHelper(args);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}