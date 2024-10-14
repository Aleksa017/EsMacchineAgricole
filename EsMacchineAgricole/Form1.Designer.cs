namespace EsMacchineAgricole
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnAddRiparazione = new Button();
            btnDescrizione = new Button();
            listBoxRiparazioni = new ListBox();
            btnNumeroOrdine = new Button();
            txtNumeroOrdine = new TextBox();
            label1 = new Label();
            btnClose = new Button();
            btnRimuovi = new Button();
            SuspendLayout();
            // 
            // btnAddRiparazione
            // 
            btnAddRiparazione.Location = new Point(660, 12);
            btnAddRiparazione.Name = "btnAddRiparazione";
            btnAddRiparazione.Size = new Size(110, 23);
            btnAddRiparazione.TabIndex = 0;
            btnAddRiparazione.Text = "AddRiparazione";
            btnAddRiparazione.UseVisualStyleBackColor = true;
            btnAddRiparazione.Click += btnAddRiparazione_Click;
            // 
            // btnDescrizione
            // 
            btnDescrizione.Location = new Point(660, 70);
            btnDescrizione.Name = "btnDescrizione";
            btnDescrizione.Size = new Size(110, 23);
            btnDescrizione.TabIndex = 1;
            btnDescrizione.Text = "Descrizione";
            btnDescrizione.UseVisualStyleBackColor = true;
            btnDescrizione.Click += btnDescrizione_Click;
            // 
            // listBoxRiparazioni
            // 
            listBoxRiparazioni.FormattingEnabled = true;
            listBoxRiparazioni.ItemHeight = 15;
            listBoxRiparazioni.Location = new Point(12, 12);
            listBoxRiparazioni.Name = "listBoxRiparazioni";
            listBoxRiparazioni.Size = new Size(642, 349);
            listBoxRiparazioni.TabIndex = 2;
            // 
            // btnNumeroOrdine
            // 
            btnNumeroOrdine.Location = new Point(600, 415);
            btnNumeroOrdine.Name = "btnNumeroOrdine";
            btnNumeroOrdine.Size = new Size(75, 23);
            btnNumeroOrdine.TabIndex = 3;
            btnNumeroOrdine.Text = "Scontrino ";
            btnNumeroOrdine.UseVisualStyleBackColor = true;
            btnNumeroOrdine.Click += btnCosto_Click;
            // 
            // txtNumeroOrdine
            // 
            txtNumeroOrdine.Location = new Point(380, 415);
            txtNumeroOrdine.Name = "txtNumeroOrdine";
            txtNumeroOrdine.Size = new Size(100, 23);
            txtNumeroOrdine.TabIndex = 4;
     
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(486, 419);
            label1.Name = "label1";
            label1.Size = new Size(87, 15);
            label1.TabIndex = 5;
            label1.Text = "NumeroOrdine";
            // 
            // btnClose
            // 
            btnClose.Location = new Point(681, 415);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 23);
            btnClose.TabIndex = 6;
            btnClose.Text = "Chiudi";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnRimuovi
            // 
            btnRimuovi.Location = new Point(660, 41);
            btnRimuovi.Name = "btnRimuovi";
            btnRimuovi.Size = new Size(110, 23);
            btnRimuovi.TabIndex = 8;
            btnRimuovi.Text = "Rimuovi riparazione";
            btnRimuovi.UseVisualStyleBackColor = true;
            btnRimuovi.Click += btnRimuovi_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnRimuovi);
            Controls.Add(btnClose);
            Controls.Add(label1);
            Controls.Add(txtNumeroOrdine);
            Controls.Add(btnNumeroOrdine);
            Controls.Add(listBoxRiparazioni);
            Controls.Add(btnDescrizione);
            Controls.Add(btnAddRiparazione);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAddRiparazione;
        private Button btnDescrizione;
        private ListBox listBoxRiparazioni;
        private Button btnNumeroOrdine;
        private TextBox txtNumeroOrdine;
        private Label label1;
        private Button btnClose;
        private Button btnRimuovi;
    }
}
