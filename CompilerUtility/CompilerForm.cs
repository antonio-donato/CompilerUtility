using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace Studiofarma.CompilerUtility
{   
    public partial class MainForm : Form
    {
        #region Fields
        private Boolean _stopCompileAll;
        private CompilerUtilityConfig _config;
        private Dictionary<CompileStatus, Int32> _counters;
        private AutoCompleteStringCollection _suggestedPrograms;
        #endregion

        public MainForm()
        {
            InitializeComponent();

            Text = String.Format("Compiler Utility {0}", Utils.GetVersion());
        }

        #region Event Methods
        private void MainForm_Shown(object sender, EventArgs e)
        {
            // Leggo e verifico la configurazione
            try
            {
                _config = CompilerUtilityConfig.Config;
                _config.Check();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Compiler Utility", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                Application.Exit();
                return;
            }

            // Inzializzo i contatori Ok/WARN/ERR
            initializeCounters();

            // Nascondi i controlli per la compilazione totale
            showAllGroup(false);

            // Crea la lista dei programmi suggeriti
            _suggestedPrograms = new AutoCompleteStringCollection();

            // Riempio la COMBO con tutte le VIEW disponibili e seleziono la prima
            String[] views = Directory.GetDirectories(_config.StarteamBasePath);
            foreach (String str in views)
                cmbViews.Items.Add(Path.GetFileName(str));
            cmbViews.SelectedIndex = 0;

            // Riempio la COMBO con tutte le WAFR presenti
            DriveInfo[] unita = DriveInfo.GetDrives();
            foreach (DriveInfo driveFound in unita)
            {
                if (driveFound.IsReady == true && driveFound.DriveType == DriveType.Fixed)
                {
                    String[] destinations = Directory.GetDirectories(driveFound.Name, CompilerUtilityConfig.DESTINATION_PATTERN);
                    foreach (String str in destinations)
                    {
                        cmbDestination.Items.Add(driveFound.Name + Path.GetFileName(str));
                    }

                }
            }
            cmbDestination.SelectedIndex = 0;


            //String[] destinations = Directory.GetDirectories(_config.BaseDestinationPath, CompilerUtilityConfig.DESTINATION_PATTERN);
            //foreach (String str in destinations)
            //    cmbDestination.Items.Add(Path.GetFileName(str));
            //cmbDestination.SelectedIndex = 0;

            // Attivo i controlli di default e seleziono il testo della combo delle View
            this.ActiveControl = cmbViews;
            this.AcceptButton = btnCompile;
            this.cmbViews.SelectAll();
        }

        private void btnCompile_Click(object sender, EventArgs e)
        {
            // Verifico le selezioni sulle combo
            if (checkCombo() == false)
                return;

            // Avvio la compilazione
            Compiler compiler = createCompiler();
            compiler.compileWithScreen(txtProgram.Text);
        }

        private void cmbViews_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ricalcolo i suggerimenti per la view selezionata
            findSuggestedPrograms(_config.StarteamBasePath + cmbViews.Items[cmbViews.SelectedIndex] + "\\");
        }

        private void txtProgram_TextChanged(object sender, EventArgs e)
        {
            btnCompile.Enabled = !String.IsNullOrEmpty(txtProgram.Text);
        }

        private void btnCompileAll_Click(object sender, EventArgs e)
        {
            compileAllFiles();
        }
        
        private void stopCompileAll_Click(object sender, EventArgs e)
        {
            // Fermo la compilazione di tutto il ramo
            _stopCompileAll = true;
            btnCompile.Enabled = true;
            btnCompileAll.Enabled = true;
        }
        #endregion

        #region Private
        /// <summary>
        /// Avvia la compilazione di tutti file del ramo selezionato
        /// </summary>
        private void compileAllFiles()
        {
            // Verifico la view e la destinazione selezionata
            if (checkCombo() == false)
                return;

            // Elenco i file da compilare
            String[] filesList = Directory.GetFiles(Path.Combine(_config.StarteamBasePath, cmbViews.Items[cmbViews.SelectedIndex].ToString()), "*.cbl");

#if DEBUG
            File.WriteAllText("compiledList.txt", "");
#endif
            

            // Disabilito i pulsanti mentre sto compilando tutti i file
            btnCompile.Enabled = false;
            btnCompileAll.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            showAllGroup(true);
            compileAllLabel.Text = String.Format("Found {0} CBL files", filesList.Length);
            compileAllProgressBar.Value = 0;
            compileAllProgressBar.Minimum = 0;
            compileAllProgressBar.Maximum = filesList.Length;

            // Resetto le variabili di inizio compilazione gruppo
            initializeCounters();
            _stopCompileAll = false;
            
            // Creo il compilatore
            Compiler compiler = createCompiler();
            
#if DEBUG
            File.WriteAllText("compiledList.txt", "");
#endif
            
            // Avvio la compilazione in un thread a parte così da poterlo stoppare
            new Thread(delegate()
            {
                // Avvio in parallelo al compilazione di tutti i file
                Parallel.ForEach(filesList, /*new ParallelOptions { MaxDegreeOfParallelism = 8 },*/ (fileName, state) =>
                {
                    if (_stopCompileAll)
                        state.Break();

                    CompileStatus result = compiler.compile(fileName);
                    _counters[result]++;

                    // Aumento il valore della barra progressiva
                    if (compileAllProgressBar.Value < compileAllProgressBar.Maximum)
                        this.Invoke(() => compileAllProgressBar.Value++);
                });
                this.Invoke(() =>
                {
                    this.Cursor = Cursors.Default;

                    // Messaggio di riepilogo della compilazione
                    MessageBox.Show(String.Format("Compiled {0}/{1} files. Warnings {2}. Errors {3} ", _counters[CompileStatus.Compiled], filesList.Length, _counters[CompileStatus.Warning], _counters[CompileStatus.Error]), "Compiler Utility", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Riattivo i controlli grafici
                    showAllGroup(false);
                    btnCompile.Enabled = true;
                    btnCompileAll.Enabled = true;
                });
            }).Start();
        }

        /// <summary>
        /// Verifico i valori delle due combo
        /// </summary>
        private Boolean checkCombo()
        {
            // Verifico la view selezionata
            if (cmbViews.SelectedIndex == -1)
            {
                MessageBox.Show("No correct view is selected", "Compiler Utility", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Verifico la destinazione selezionata
            if (cmbDestination.SelectedIndex == -1)
            {
                MessageBox.Show("No correct destination is selected", "Compiler Utility", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Crea un compilatore in base alle impostazioni sulla screen
        /// </summary>
        private Compiler createCompiler()
        {
            Boolean debug = chkDebug.Checked;
            String view = cmbViews.Items[cmbViews.SelectedIndex].ToString();
            String destination = cmbDestination.Items[cmbDestination.SelectedIndex].ToString();
            
            return new Compiler(view, destination, debug);
        }

        /// <summary>
        /// Carica la lista dei programmi suggeriti
        /// </summary>
        private void findSuggestedPrograms(String basePath)
        {
            // Stacco la lista di autocompletamento dalla textbox
            txtProgram.AutoCompleteSource = AutoCompleteSource.None;
            _suggestedPrograms.Clear();

            // Popolo la lista
            foreach (String file in Directory.GetFiles(basePath, "*.cbl", SearchOption.TopDirectoryOnly))
                _suggestedPrograms.Add(Path.GetFileName(file));

            // Ricollego la lista alla textbox
            txtProgram.AutoCompleteCustomSource = _suggestedPrograms;
            txtProgram.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtProgram.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        /// <summary>
        /// Mostra/nasconde i controlli per la compilazione di gruppo
        /// </summary>
        private void showAllGroup(Boolean show)
        {
            if (show)
            {
                compileAllGroup.Visible = true;
                this.Height = 251;
            }
            else
            {
                compileAllGroup.Visible = false;
                this.Height = 190;
            }
        }

        /// <summary>
        /// Inizializza i contatori dei risultati delle compilazioni
        /// </summary>
        private void initializeCounters()
        {
            _counters = new Dictionary<CompileStatus, Int32>
            {
                {CompileStatus.Error, 0},
                {CompileStatus.Warning, 0},
                {CompileStatus.Compiled, 0},
            };
        }
        #endregion
    }
}
