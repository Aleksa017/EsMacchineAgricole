public class CAggiustao
{
    public string Descrizione;
    public double Costo;
    public DateTime DataAccettazione;
    public int NumeroOrdine;
    public int numero = 0;
    static int count =  0;
    public CAggiustao(string descrizione, double costo, DateTime dataAccettazione)
    {
        Descrizione = descrizione;
        Costo = costo;
        DataAccettazione = dataAccettazione;
        count++;
        NumeroOrdine = numero;
        numero = count;
    }
    public override string ToString()
    {
        return $"Numero Ordine: {NumeroOrdine},Problema: {Descrizione}, Data Di Accettazione: {DataAccettazione.ToShortDateString()}, Costo: {Costo} €";
    }
}
