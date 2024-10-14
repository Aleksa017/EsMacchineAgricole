using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace EsMacchineAgricole
{
    public partial class Form1 : Form
    {
        private List<CAggiustao> riparazioni = new List<CAggiustao>();
        private PrintDocument printDocument;

        public Form1()
        {
            InitializeComponent();
            CaricaRiparazioniDaJson();
            printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
        }

        private void btnAddRiparazione_Click(object sender, EventArgs e)
        {
            // Controlla se ci sono già 5 riparazioni
            if (listBoxRiparazioni.Items.Count >= 5)
            {
                MessageBox.Show("Non puoi aggiungere più di 5 riparazioni in corso.", "Limite Raggiunto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Esci dalla funzione se il limite è raggiunto
            }

            FormAddRiparazione formAdd = new FormAddRiparazione();
            if (formAdd.ShowDialog() == DialogResult.OK)
            {
                riparazioni.Add(formAdd.NuovaRiparazione);
                listBoxRiparazioni.Items.Add(formAdd.NuovaRiparazione);
            }
        }

        private void CaricaRiparazioniDaJson()
        {
            try
            {
                string json = File.ReadAllText("riparazioni.json");
                riparazioni = JsonConvert.DeserializeObject<List<CAggiustao>>(json);

                if (riparazioni != null && riparazioni.Count > 0)
                {
                    // Serve per trovare numero d'ordine massimo
                    int maxNumeroOrdine = riparazioni.Max(r => r.NumeroOrdine);

                    // Inizializza il contatore statico con il valore massimo
                    CAggiustao.numero = maxNumeroOrdine;
                }
                else
                {
                    // Se non ci sono riparazioni mette a 0
                    CAggiustao.numero = 0;
                }

                foreach (var riparazione in riparazioni)
                {
                    listBoxRiparazioni.Items.Add(riparazione);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante il caricamento del file JSON: {ex.Message}");
            }
        }

        private void btnDescrizione_Click(object sender, EventArgs e)
        {
            if (listBoxRiparazioni.SelectedItem != null)
            {
                CAggiustao riparazione = (CAggiustao)listBoxRiparazioni.SelectedItem;
                MessageBox.Show(riparazione.Descrizione, "Descrizione Riparazione");
            }
        }

        private void btnCosto_Click(object sender, EventArgs e)
        {
            // Verifica se il numero d'ordine è valido
            if (!TryGetNumeroOrdine(out int numeroOrdine))
            {
                MessageBox.Show("Inserisci un numero d'ordine valido.", "Errore");
                return; // Esci dal metodo se il numero d'ordine non è valido
            }

            // Cerca la riparazione corrispondente al numero d'ordine
            CAggiustao riparazione = TrovaRiparazione(numeroOrdine);

            if (riparazione != null)
            {
                // Mostra la finestra di stampa
                using (PrintDialog printDialog = new PrintDialog())
                {
                    printDialog.Document = printDocument;

                    if (printDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Avvia la stampa
                        printDocument.Print();
                    }
                }
            }
            else
            {
                MessageBox.Show("Nessuna riparazione trovata con questo numero d'ordine.", "Errore");
            }

            // Pulisce il campo di input del numero d'ordine
            txtNumeroOrdine.Clear();
        }

        // Metodo per verificare se il numero d'ordine è valido
        private bool TryGetNumeroOrdine(out int numeroOrdine)
        {
            return int.TryParse(txtNumeroOrdine.Text, out numeroOrdine);
        }

        // Metodo per trovare una riparazione in base al numero d'ordine
        private CAggiustao TrovaRiparazione(int numeroOrdine)
        {
            return riparazioni.FirstOrDefault(r => r.NumeroOrdine == numeroOrdine);
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            int yPos = 20;
            int leftMargin = e.MarginBounds.Left;
            string scontrino = $"Scontrino\n\nNumero Ordine: {txtNumeroOrdine.Text}\n\n";

            if (int.TryParse(txtNumeroOrdine.Text, out int numeroOrdine))
            {
                CAggiustao riparazione = riparazioni.FirstOrDefault(r => r.NumeroOrdine == numeroOrdine);
                if (riparazione != null)
                {
                    // Aggiungi dettagli della riparazione allo scontrino
                    scontrino += $"Descrizione: {riparazione.Descrizione}\n";
                    scontrino += $"Costo: {riparazione.Costo} €\n";
                    scontrino += $"Data Accettazione: {riparazione.DataAccettazione.ToShortDateString()}\n";
                }
            }

            // Stampa il contenuto dello scontrino
            e.Graphics.DrawString(scontrino, new Font("Arial", 12), Brushes.Black, leftMargin, yPos);
        }

       

        private void btnRimuovi_Click(object sender, EventArgs e)
        {
            if (listBoxRiparazioni.SelectedItem != null)
            {
                CAggiustao riparazioneSelezionata = (CAggiustao)listBoxRiparazioni.SelectedItem;
                riparazioni.Remove(riparazioneSelezionata);
                listBoxRiparazioni.Items.Remove(riparazioneSelezionata);

                // Aggiorna il file JSON
                RiparazioniJson();

                MessageBox.Show("Riparazione rimossa.");
            }
            else
            {
                MessageBox.Show("Errore, seleziona riparazione.");
            }
        }

        private void RiparazioniJson()
        {
            try
            {
                // Serializza la lista di riparazioni in JSON
                string jsonContent = JsonConvert.SerializeObject(riparazioni, Formatting.Indented);
                File.WriteAllText("riparazioni.json", jsonContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore : {ex.Message}");
            }
        }

   
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
