using System;
using System.Threading;
using System.Net;
using Rage;
using LSPD_First_Response.Mod.API;
using System.Reflection;
using System.IO;

namespace MechanicBackupBridge
{
    public class Main : Plugin
    {
        public override void Initialize()
        {

            Version currentVersion = new Version("1.1.0");
            Version newVersion = new Version();
 
            Game.DisplayNotification("MechanicBackupBridge " + currentVersion + " loaded successfully");
            Game.LogTrivial("MechanicBackupBridge " + currentVersion + " loaded successfully");

            AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;

            try
            {
                Thread FetchVersionThread = new Thread(() =>
                {

                    using (WebClient client = new WebClient())
                    {
                        try
                        {
                            string s = client.DownloadString("http://www.lcpdfr.com/applications/downloadsng/interface/api.php?do=checkForUpdates&fileId=29806&textOnly=1");

                            newVersion = new Version(s);
                        }
                        catch (Exception e) { Game.LogTrivial("MechanicBackupBridge: LSPDFR Update API down. Aborting checks."); }
                    }
                });
                FetchVersionThread.Start();
                while (FetchVersionThread.ThreadState != System.Threading.ThreadState.Stopped)
                {
                    GameFiber.Yield();
                }

                // compare the versions  
                if (currentVersion.CompareTo(newVersion) < 0)
                {
                    Game.LogTrivial("MechanicBackupBridge: Update Available for MechanicBackupBridge. Installed Version " + currentVersion + "New Version " + newVersion);
                    Game.DisplayNotification("~g~Update Available~w~ for ~b~MechanicBackupBridge~w~.\nInstalled Version: ~y~" + currentVersion + "\n~w~New Version~y~ " + newVersion);
                }
            }
            catch (System.Threading.ThreadAbortException e)
            {
                Game.LogTrivial("MechanicBackupBridge: Error while checking for updates.");
            }
            catch (Exception e)
            {
                Game.LogTrivial("MechanicBackupBridge: Error while checking for updates.");
            }

        }

        public override void Finally()
        {
            Game.LogTrivial("MechanicBackupBridge unloaded");
        }

        private static Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith("MechanicBackup"))
            {
                return Assembly.Load(File.ReadAllBytes(@"Plugins\MechanicBackup.dll"));
            }

            return null;
        }

    }
}
