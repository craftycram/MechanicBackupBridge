using System;
using Rage;
using LSPD_First_Response.Mod.API;
using System.Reflection;

namespace MechanicBackupBridge
{
    public class Main : Plugin
    {
        public override void Initialize()
        {

            Version currentVersion = new Version("1.0.0");
            Version newVersion = new Version();
 
            Game.DisplayNotification("MechanicBackupBridge " + currentVersion + " loaded successfully");
            Game.LogTrivial("MechanicBackupBridge " + currentVersion + " loaded successfully");

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += new ResolveEventHandler(MyResolveEventHandler);
            InstantiateAPIBridge(currentDomain);
        }

        public override void Finally()
        {
            Game.LogTrivial("MechanicBackupBridge unloaded");
        }

        private static void InstantiateAPIBridge(AppDomain domain)
        {
            try
            {
                string asmname = Assembly.GetCallingAssembly().FullName;
                domain.CreateInstance(asmname, "MechanicBackup");
            }
            catch (Exception e)
            {
                Game.LogTrivial("");
                Game.LogTrivial(e.Message);
            }
        }

        private static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            Game.LogTrivial("Resolving...");
            return typeof(MechanicBackup.API).Assembly;
        }

    }
}
