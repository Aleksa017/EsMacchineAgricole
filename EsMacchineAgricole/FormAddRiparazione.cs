using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace EsMacchineAgricole
{
    public partial class FormAddRiparazione : Form
    {
        public CAggiustao NuovaRiparazione { get; private set; }

        public FormAddRiparazione()
        {
            InitializeComponent();
            InitComboBox();
         
        }

        private void InitComboBox()
        {
            comboBoxTipologia.Items.Add("Tosaerba");
            comboBoxTipologia.Items.Add("Motozappa");
            comboBoxTipologia.Items.Add("Decespugliatore");
            comboBoxTipologia.SelectedIndexChanged += ComboBoxTipologia_SelectedIndexChanged;

        }

        private void ComboBoxTipologia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipologiaSelez = comboBoxTipologia.SelectedItem.ToString();
            lblNumeroRuote.Visible = false;
            txtNumeroRuote.Visible = false;
            lblTipoCarburante.Visible = false;
            txtTipoCarburante.Visible = false;
            lblAScoppio.Visible = false;
            checkBoxAScoppio.Visible = false;
            lblTipoSupporto.Visible = false;
            comboBoxTipoSupporto.Visible = false;
            // Mostra solo i campi rilevanti per la tipologia selezionata
            switch (tipologiaSelez)
            {
                case "Tosaerba":
                    lblNumeroRuote.Visible = true;
                    txtNumeroRuote.Visible = true;
                    lblTipoCarburante.Visible = true;
                    txtTipoCarburante.Visible = true;
                    break;
                case "Motozappa":
                    lblNumeroRuote.Visible = true;
                    txtNumeroRuote.Visible = true;
                    break;
                case "Decespugliatore":
                    comboBoxTipoSupporto.Items.Clear(); // Clear previous items
                    comboBoxTipoSupporto.Items.Add("Tracolla");
                    comboBoxTipoSupporto.Items.Add("Zainetto");
                    lblAScoppio.Visible = true;
                    checkBoxAScoppio.Visible = true;
                    lblTipoSupporto.Visible = true;
                    comboBoxTipoSupporto.Visible = true;
                    break;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string marca = txtMarca.Text;
            string numeroSerie = txtNumeroSerie.Text;
            string nominativo = txtNominativo.Text;
            string telefono = txtTelefono.Text;
            DateTime dataAccettazione = datePickerAccettazione.Value;
            DateTime dataInizio = datePickerInizio.Value;

            string selectedType = comboBoxTipologia.SelectedItem.ToString();
            if (selectedType == "Tosaerba")
            {
                if (int.TryParse(txtNumeroRuote.Text, out int numeroRuote) && !string.IsNullOrEmpty(txtTipoCarburante.Text))
                {
                    CMacchine nuovaApparecchiatura = new CTosaerba(
                        marca,
                        numeroSerie,
                        nominativo,
                        telefono,
                        dataAccettazione,
                        dataInizio,
                        false,
                        numeroRuote,
                        txtTipoCarburante.Text
                    );
                }
                else
                {
                    MessageBox.Show("Inserisci correttamente i dati del tosaerba.");
                    return;
                }
            }
            else if (selectedType == "Motozappa")
            {
                if (int.TryParse(txtNumeroRuote.Text, out int numeroRuote))
                {
                    CMacchine nuovaApparecchiatura = new CMotozappa(
                        marca,
                        numeroSerie,
                        nominativo,
                        telefono,
                        dataAccettazione,
                        dataInizio,
                        false,
                        numeroRuote
                    );
                }
                else
                {
                    MessageBox.Show("Inserisci correttamente il numero di ruote per la motozappa.");
                    return;
                }
            }
            else if (selectedType == "Decespugliatore")
            {
                if (comboBoxTipoSupporto.SelectedItem != null)
                {
                    bool aScoppio = checkBoxAScoppio.Checked; // Assunto che ci sia una Checkbox per "a scoppio"
                    string ?tipoSupporto = comboBoxTipoSupporto.SelectedItem.ToString();

                    CMacchine nuovaApparecchiatura = new CDecespugliatore(
                        marca,
                        numeroSerie,
                        nominativo,
                        telefono,
                        dataAccettazione,
                        dataInizio,
                        false,
                        aScoppio,
                        tipoSupporto
                    );
                }
                else
                {
                    MessageBox.Show("Seleziona un tipo di supporto per il decespugliatore.");
                    return;
                }
            }
            if (Controlli())
            {
                FileJSSSon();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void FileJSSSon()
        {
            string filePath = "riparazioni.json";
            DateTime dataAccettazione = datePickerAccettazione.Value;
            string descrizione = txtDescrizione.Text;
            double costo = double.Parse(txtCosto.Text);

            CAggiustao nuovaRiparazione = new CAggiustao(descrizione, costo, dataAccettazione);

            // Lista per tenere tutte le riparazioni esistenti
            List<CAggiustao> riparazioni = new List<CAggiustao>();

            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                riparazioni = JsonConvert.DeserializeObject<List<CAggiustao>>(jsonContent) ?? new List<CAggiustao>();
            }

            // Aggiungi la nuova riparazione alla lista
            riparazioni.Add(nuovaRiparazione);

            // Serializza di nuovo la lista e sovrascrivi il file JSON
            string nuovoJsonContent = JsonConvert.SerializeObject(riparazioni, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, nuovoJsonContent);

            MessageBox.Show("Riparazione salvata con successo!");
            NuovaRiparazione = nuovaRiparazione;
            this.Close();
        }

        private bool Controlli()
        {
            if (string.IsNullOrEmpty(txtDescrizione.Text))
            {
                MessageBox.Show("Inserisci una descrizione valida.");
                return false;
            }

            if (string.IsNullOrEmpty(txtCosto.Text) || !double.TryParse(txtCosto.Text, out _))
            {
                MessageBox.Show("Inserisci un costo valido.");
                return false;
            }

            string tipologiaSelezionata = comboBoxTipologia.SelectedItem.ToString();

            if (tipologiaSelezionata == "Tosaerba")
            {
                if (string.IsNullOrEmpty(txtNumeroRuote.Text) || !int.TryParse(txtNumeroRuote.Text, out _))
                {
                    MessageBox.Show("Inserisci un numero di ruote valido per il tosaerba.");
                    return false;
                }

                if (string.IsNullOrEmpty(txtTipoCarburante.Text))
                {
                    MessageBox.Show("Inserisci il tipo di carburante per il tosaerba.");
                    return false;
                }
            }
            else if (tipologiaSelezionata == "Motozappa")
            {
                if (string.IsNullOrEmpty(txtNumeroRuote.Text) || !int.TryParse(txtNumeroRuote.Text, out _))
                {
                    MessageBox.Show("Inserisci un numero di ruote valido per la motozappa.");
                    return false;
                }
            }
            else if (tipologiaSelezionata == "Decespugliatore")
            {
                if (string.IsNullOrEmpty(checkBoxAScoppio.Text))
                {
                    MessageBox.Show("Seleziona il tipo di decespugliatore .");
                    return false;
                }

                if (string.IsNullOrEmpty(comboBoxTipoSupporto.Text))
                {
                    MessageBox.Show("Seleziona il tipo di imbracatura ");
                    return false;
                }
            }

            if (datePickerAccettazione.Value > DateTime.Now)
            {
                MessageBox.Show("La data di accettazione non può essere nel futuro.");
                return false;
            }

            if (datePickerInizio.Value < datePickerAccettazione.Value)
            {
                MessageBox.Show("La data di inizio non può essere precedente alla data di accettazione.");
                return false;
            }

            return true;
        }
    }
}
