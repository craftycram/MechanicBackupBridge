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
            AppDomain rageDomain = AppDomain.CreateDomain("rageDomain");

            currentDomain.AssemblyResolve += new ResolveEventHandler(MyResolveEventHandler);

            // This call will succeed in creating an instance of MyType since the
            // assembly name is valid.
            InstantiateMyTypeSucceed(currentDomain);

        }

        public override void Finally()
        {
            Game.LogTrivial("MechanicBackupBridge unloaded");
        }

        private static void InstantiateMyTypeSucceed(AppDomain domain)
        {
            try
            {
                string asmname = Assembly.GetCallingAssembly().FullName;
                domain.CreateInstance(asmname, "MechanicBackup.API");
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
        }

        private static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            Console.WriteLine("Resolving...");
            return typeof(MechanicBackup.API).Assembly;
        }
    }
}
