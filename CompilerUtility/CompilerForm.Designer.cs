namespace Studiofarma.CompilerUtility
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnCompile = new System.Windows.Forms.Button();
            this.txtProgram = new System.Windows.Forms.TextBox();
            this.cmbViews = new System.Windows.Forms.ComboBox();
            this.chkDebug = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.logo = new System.Windows.Forms.PictureBox();
            this.btnCompileAll = new System.Windows.Forms.Button();
            this.compileAllProgressBar = new System.Windows.Forms.ProgressBar();
            this.compileAllGroup = new System.Windows.Forms.GroupBox();
            this.stopCompileAll = new System.Windows.Forms.Button();
            this.compileAllLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDestination = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.compileAllGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCompile
            // 
            this.btnCompile.Enabled = false;
            this.btnCompile.Location = new System.Drawing.Point(12, 69);
            this.btnCompile.Name = "btnCompile";
            this.btnCompile.Size = new System.Drawing.Size(75, 23);
            this.btnCompile.TabIndex = 3;
            this.btnCompile.TabStop = false;
            this.btnCompile.Text = "Compile";
            this.btnCompile.UseVisualStyleBackColor = true;
            this.btnCompile.Click += new System.EventHandler(this.btnCompile_Click);
            // 
            // txtProgram
            // 
            this.txtProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProgram.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtProgram.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProgram.Location = new System.Drawing.Point(254, 31);
            this.txtProgram.Name = "txtProgram";
            this.txtProgram.Size = new System.Drawing.Size(210, 22);
            this.txtProgram.TabIndex = 2;
            this.txtProgram.TextChanged += new System.EventHandler(this.txtProgram_TextChanged);
            // 
            // cmbViews
            // 
            this.cmbViews.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbViews.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbViews.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbViews.FormattingEnabled = true;
            this.cmbViews.Location = new System.Drawing.Point(12, 31);
            this.cmbViews.Name = "cmbViews";
            this.cmbViews.Size = new System.Drawing.Size(236, 24);
            this.cmbViews.Sorted = true;
            this.cmbViews.TabIndex = 1;
            this.cmbViews.SelectedIndexChanged += new System.EventHandler(this.cmbViews_SelectedIndexChanged);
            // 
            // chkDebug
            // 
            this.chkDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDebug.AutoSize = true;
            this.chkDebug.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDebug.Location = new System.Drawing.Point(190, 73);
            this.chkDebug.Name = "chkDebug";
            this.chkDebug.Size = new System.Drawing.Size(58, 17);
            this.chkDebug.TabIndex = 3;
            this.chkDebug.Text = "Debug";
            this.chkDebug.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "View";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(254, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "Program";
            // 
            // logo
            // 
            this.logo.Image = global::CompilerUtility.Properties.Resources.logo;
            this.logo.Location = new System.Drawing.Point(12, 98);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(129, 50);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logo.TabIndex = 7;
            this.logo.TabStop = false;
            // 
            // btnCompileAll
            // 
            this.btnCompileAll.Location = new System.Drawing.Point(93, 69);
            this.btnCompileAll.Name = "btnCompileAll";
            this.btnCompileAll.Size = new System.Drawing.Size(75, 23);
            this.btnCompileAll.TabIndex = 8;
            this.btnCompileAll.TabStop = false;
            this.btnCompileAll.Text = "CompileAll";
            this.btnCompileAll.UseVisualStyleBackColor = true;
            this.btnCompileAll.Click += new System.EventHandler(this.btnCompileAll_Click);
            // 
            // compileAllProgressBar
            // 
            this.compileAllProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.compileAllProgressBar.Location = new System.Drawing.Point(6, 36);
            this.compileAllProgressBar.Name = "compileAllProgressBar";
            this.compileAllProgressBar.Size = new System.Drawing.Size(368, 14);
            this.compileAllProgressBar.TabIndex = 9;
            // 
            // compileAllGroup
            // 
            this.compileAllGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.compileAllGroup.Controls.Add(this.stopCompileAll);
            this.compileAllGroup.Controls.Add(this.compileAllLabel);
            this.compileAllGroup.Controls.Add(this.compileAllProgressBar);
            this.compileAllGroup.Location = new System.Drawing.Point(12, 152);
            this.compileAllGroup.Name = "compileAllGroup";
            this.compileAllGroup.Size = new System.Drawing.Size(452, 57);
            this.compileAllGroup.TabIndex = 10;
            this.compileAllGroup.TabStop = false;
            this.compileAllGroup.Text = "Compile all";
            // 
            // stopCompileAll
            // 
            this.stopCompileAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.stopCompileAll.Location = new System.Drawing.Point(383, 16);
            this.stopCompileAll.Name = "stopCompileAll";
            this.stopCompileAll.Size = new System.Drawing.Size(59, 34);
            this.stopCompileAll.TabIndex = 12;
            this.stopCompileAll.Text = "Stop";
            this.stopCompileAll.UseVisualStyleBackColor = true;
            this.stopCompileAll.Click += new System.EventHandler(this.stopCompileAll_Click);
            // 
            // compileAllLabel
            // 
            this.compileAllLabel.AutoSize = true;
            this.compileAllLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.compileAllLabel.Location = new System.Drawing.Point(6, 17);
            this.compileAllLabel.Name = "compileAllLabel";
            this.compileAllLabel.Size = new System.Drawing.Size(46, 13);
            this.compileAllLabel.TabIndex = 11;
            this.compileAllLabel.Text = "Program";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(254, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 21);
            this.label3.TabIndex = 12;
            this.label3.Text = "Destination";
            // 
            // cmbDestination
            // 
            this.cmbDestination.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbDestination.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDestination.FormattingEnabled = true;
            this.cmbDestination.Location = new System.Drawing.Point(254, 99);
            this.cmbDestination.Name = "cmbDestination";
            this.cmbDestination.Size = new System.Drawing.Size(210, 24);
            this.cmbDestination.Sorted = true;
            this.cmbDestination.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 213);
            this.Controls.Add(this.cmbDestination);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.compileAllGroup);
            this.Controls.Add(this.btnCompileAll);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkDebug);
            this.Controls.Add(this.cmbViews);
            this.Controls.Add(this.txtProgram);
            this.Controls.Add(this.btnCompile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Compiler Utility";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.compileAllGroup.ResumeLayout(false);
            this.compileAllGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCompile;
        private System.Windows.Forms.TextBox txtProgram;
        private System.Windows.Forms.ComboBox cmbViews;
        private System.Windows.Forms.CheckBox chkDebug;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Button btnCompileAll;
        private System.Windows.Forms.ProgressBar compileAllProgressBar;
        private System.Windows.Forms.GroupBox compileAllGroup;
        private System.Windows.Forms.Label compileAllLabel;
        private System.Windows.Forms.Button stopCompileAll;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbDestination;
    }
}

