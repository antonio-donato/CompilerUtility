using System;
using System.IO;
using System.Reflection;

namespace Studiofarma
{
    public static class Utils
    {
        /// <summary>
        /// Get the assembly version for the executable entry assembly
        /// </summary>
        /// <returns>The assembly version in MAJOR.MINOR.BUILD.REVISION format</returns>
        public static string GetVersion()
        {
            return (GetVersion(Assembly.GetEntryAssembly()));
        }

        /// <summary>
        /// Get the assembly version for the specified assembly
        /// </summary>
        /// <returns>The assembly version in MAJOR.MINOR.BUILD.REVISION format</returns>
        public static String GetVersion(Assembly ass)
        {
            Version ver = ass.GetName().Version;
            return (string.Format("v{0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision));
        }

        /// <summary>
        /// Partendo da un percorso se è già assoluto lo restituisce
        /// altrimenti lo rende assoluto a partire dalla cartella dell'eseguibile al
        /// posto che dalla working directory
        /// </summary>
        public static String GetAbsolutePathFromExecutable(String path)
        {
            return Path.IsPathRooted(path) ? path : Path.Combine(Utils.GetExecutablePath(), path);
        }

        /// <summary>
        /// Get the path to the executable file of the application entry-point
        /// </summary>
        /// <returns>The path of the application executable </returns>
        public static String GetExecutablePath()
        {
            return (Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
        }
    }
}
