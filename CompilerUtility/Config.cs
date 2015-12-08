using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using DaveSoftware.Common;

namespace Studiofarma.CompilerUtility
{
    [XmlRootAttribute("compilerUtility")]
    public class CompilerUtilityConfig : XMLSerializableObject<CompilerUtilityConfig>
    {
        #region Const
        public const String DESTINATION_PATTERN = "wfar*";
        public const String COMPILE_BAT_NAME = @"compile.bat";
        public const String COMPILE_BAT_PAUSE_NAME = @"compileNoPause.bat";

        private const String BASE_DESTINATION_PATH_DEFAULT = @"c:\";
        private const String COMPILE_BAT_PATH_DEFAULT = @"compile.bat";
        private const String COMPILE_BAT_PAUSE_PATH_DEFAULT = @"compileNoPause.bat";
        private const String STARTEAM_BASE_PATH_DEFAULT = @"D:\workspace_starteam\Wingesfar\";
        #endregion

        #region Static Fields
        private static CompilerUtilityConfig _config;
        #endregion

        #region Static Properties
        public static CompilerUtilityConfig Config { get { return _config; } }
        public static String ConfigurationFilePath { get { return Utils.GetAbsolutePathFromExecutable("compilerUtilityConfig.xml"); } }
        #endregion

        #region Fields
        private String _scriptPath;
        private String _scriptPausePath;
        #endregion

        #region Properties
        [XmlElement("scriptFolder")]
        public String ScriptFolder { get; set; }

        [XmlElement("baseDestinationPath")]
        public String BaseDestinationPath { get; set; }

        [XmlElement("starteamBasePath")]
        public String StarteamBasePath { get; set; }

        [XmlIgnore]
        public String ScriptPath { get { return _scriptPath; } }
        [XmlIgnore]
        public String ScriptPausePath { get { return _scriptPausePath; } }
        #endregion

        #region Constructors (static & no)
        /// <summary>
        /// Carica da file XML la confiugrazione. Setta l'XMLSerializer per indentare
        /// e non omettere il tga xml
        /// Lancia e logga eccezioni nel caso in cui la lettura fallisca
        /// </summary>
        static CompilerUtilityConfig()
        {
            // Configuro l'oggetto PADRE
            _indentation = true;
            _omitXmlDeclaration = false;
            _encoding = Encoding.Unicode;

            // Leggo la configurazione
            Read();
        }
        #endregion

        #region Public APIs (Check)
        public Boolean Check()
        {
            // Controllo esitenza parametri
            if (ScriptFolder == null || BaseDestinationPath == null || StarteamBasePath == null)
                throw new Exception(String.Format("One or more config parameters are empty"));

            // Controllo esistenza BAT di compilazione
            _scriptPath = Path.Combine(ScriptFolder, COMPILE_BAT_NAME);
            if (!File.Exists(_scriptPath))
                throw new Exception(String.Format("Cannot find the compile script: '{0}'.", _scriptPath));

            // Controllo esistenza BAT-PAUSA di compilazione
            _scriptPausePath = Path.Combine(ScriptFolder, COMPILE_BAT_PAUSE_NAME);
            if (!File.Exists(_scriptPausePath))
                throw new Exception(String.Format("Cannot find the compile script: '{0}'.", _scriptPausePath));

            // Controllo esistenza percorso base di Starteam
            if (!Directory.Exists(StarteamBasePath))
                throw new Exception(String.Format("Cannot find the specified path: '{0}'.", StarteamBasePath));

            // Controllo esistenza percorso di destinazione
            if (!Directory.Exists(BaseDestinationPath))
                throw new Exception(String.Format("Cannot find the specified path '{0}'.", BaseDestinationPath));

            // Controllo esistenza di WFAR* nella cartella di destinazione
            String[] destinations = Directory.GetDirectories(BaseDestinationPath, CompilerUtilityConfig.DESTINATION_PATTERN);
            if (destinations.Length == 0)
                throw new Exception(String.Format("No WFAR* found in the specified path: '{0}'", BaseDestinationPath));

            return true;
        }
        #endregion

        #region Static APIs (Read & Write)
        /// <summary>
        /// Rilegge e carica da file XML la confiugrazione usando il percorso
        /// passato come file XML.
        /// Lancia e logga eccezioni nel caso in cui la lettura fallisca
        /// </summary>
        public static void Read(String path)
        {
            try
            {
                // Carico il file di configurazione dell'intera applicazione
                if (File.Exists(path) == false)
                    throw new FileNotFoundException(String.Format("The configuration file {0} does not exists", path));

                FileStream f = new FileStream(Path.GetFullPath(path), FileMode.Open, FileAccess.Read);

                _config = CompilerUtilityConfig.FromXml(f);
                f.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Rilegge e carica da file XML la confiugrazione.
        /// Lancia e logga eccezioni nel caso in cui la lettura fallisca
        /// </summary>
        public static void Read()
        {
            try
            {
                // Carico il file di configurazione dell'intera applicazione
                if (File.Exists(ConfigurationFilePath) == false)
                    throw new FileNotFoundException(String.Format("The configuration file {0} does not exists", ConfigurationFilePath));

                FileStream f = new FileStream(Path.GetFullPath(ConfigurationFilePath), FileMode.Open, FileAccess.Read);

                _config = CompilerUtilityConfig.FromXml(f);
                f.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Scrive su file XML la confiugrazione attuale
        /// </summary>
        /// <returns>TRUE se e solo se la scrittura è riuscita con successo</returns>
        public static Boolean Write()
        {
            // Scrivo il file di confiogurazione dell'intera applicazione
            try
            {
                FileStream f = new FileStream(ConfigurationFilePath, FileMode.Create, FileAccess.Write);

                CompilerUtilityConfig.ToXml(Config, f);
                f.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

    }
}
