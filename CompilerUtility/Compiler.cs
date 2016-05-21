using System;
using System.Diagnostics;
using System.IO;

namespace Studiofarma.CompilerUtility
{
    public enum CompileStatus
    {
        Compiled,
        Warning,
        Error,
    };

    class Compiler
    {
        private const String ERROR_FOLDER = "err";

        #region Fields
        private String _view;
        private String _destination;
        private Boolean _debug;
        private CompilerUtilityConfig _config;
        #endregion        

        public Compiler(String view, String destination, Boolean debug)
        {
            _view = view;
            _debug = debug;
            _destination = destination; //destination.Substring(4); // Tolgo dalla destinazione 'wfar'

            // Estraggo la configurazione
            _config = CompilerUtilityConfig.Config;
        }

        #region Public APIs
        public CompileStatus compile(String file)
        {
            return compile(file, _config.ScriptPausePath, true);
        }
        
        public CompileStatus compileWithScreen(String file)
        {
            return compile(file, _config.ScriptPath, false);
        }
        #endregion

        #region Private
        private CompileStatus compile(String file, String fileBat, Boolean blockOutput)
        {
            // Verifico se esiste la cartella 'err'
            checkErrorDirectory();
            
            // Creo gli argomenti per lanciare il BAT
            String args = String.Format("{0} {1} \"\" \"\" \"{2}\" {3}", file, _view, _destination, _debug ? "-Ga" : "");

            // Creo il processo da avviare
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    Arguments = args,
                    FileName = fileBat,
                    WorkingDirectory = _config.ScriptFolder,
                }
            };

            // Se devo bloccare l'output indico di non mostrare la finestra
            if (blockOutput)
            {
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;
            }
            

            // Lo lancio e ne aspetto la terminazione
            process.Start();

            if (blockOutput)
                while (process.StandardOutput.EndOfStream == false)
                    process.StandardOutput.ReadLine();

            process.WaitForExit();

#if DEBUG
            lock(this)
            {
                using (StreamWriter sw =  File.AppendText("compiledList.txt"))
                {
                    sw.WriteLine(String.Format("{0} - {1}", file, ((CompileStatus)process.ExitCode).ToString()));
                    sw.Flush();
                    sw.AutoFlush = true;
                }
            }
#endif
            // Tutti i valori sopra il 2 sono cmq degli errori
            Int32 exitCode = Math.Min(process.ExitCode, 2);
            CompileStatus status = (CompileStatus) exitCode;

            // Se è warning rinomino il file di errore in .war
            if (status == CompileStatus.Warning)
            {
                String filePath =  String.Format(@"{0}\{1}\err\{2}", _config.ScriptFolder, _view, Path.GetFileNameWithoutExtension(file));
                String src = String.Format("{0}.er", filePath);
                String dst = String.Format("{0}.war", filePath);

                // Copio con sovrascrittura
                File.Copy(src, dst, true);
                
                // Elimino il .er
                File.Delete(src);
            }

            // Ritorno lo stato di uscita dello script
            return status;
        }

        /// <summary>
        /// Verifico se esiste la cartella ERR nella destinazione, altrimenti la creo
        /// </summary>
        private void checkErrorDirectory()
        {
            String path = Path.Combine(_config.StarteamBasePath, _view, ERROR_FOLDER);
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
        }
        #endregion
    }
}
