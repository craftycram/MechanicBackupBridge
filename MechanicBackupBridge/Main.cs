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

            AppDomain rageDomain = AppDomain.CurrentDomain;
            Type mechanicBackupType = typeof(MechanicBackup.API);
            rageDomain.CreateInstanceAndUnwrap(mechanicBackupType.Assembly.FullName, mechanicBackupType.FullName);

        }

        public override void Finally()
        {
            Game.LogTrivial("MechanicBackupBridge unloaded");
        }

    }
}
