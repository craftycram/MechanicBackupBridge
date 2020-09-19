using System:
using Rage;
using LSPD_First_Response.Mod.API;

namespace MechanicBackupBridge
{
    public class Main : Plugin
    {
        public override void Finally()
        {

            Version currentVersion = new Version("1.0.0");
            Version newVersion = new Version();

            Functions.OnOnDutyStateChanged += OnDutyStateChangedHandler;
 
            Game.DisplayNotification("MechanicBackupBridge " + currentVersion + " loaded successfully");
            Game.LogTrivial("MechanicBackupBridge " + currentVersion + " loaded successfully");

        }

        private static void OnDutyStateChangedHandler(bool OnDuty)
        {

        }

        public override void Initialize()
        {
            Game.LogTrivial("MechanicBackupBridge unloaded");
        }
    }
}
