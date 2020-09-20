using System;
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

            Version currentVersion = new Version("1.0.0");
            Version newVersion = new Version();
 
            Game.DisplayNotification("MechanicBackupBridge " + currentVersion + " loaded successfully");
            Game.LogTrivial("MechanicBackupBridge " + currentVersion + " loaded successfully");

            AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;

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
