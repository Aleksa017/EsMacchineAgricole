using System;
using System.Collections.Generic;
using System.Linq;

public class CMacchine
{
    protected string Marca;
    protected string NumeroSerie;
    protected string Nominativo;
    protected string NumeroTelefono;
    protected DateTime DataAccettazione;
    protected DateTime DataInizioIntervento; 
    protected bool Visionata;
    public List<CAggiustao> Riparazioni { get; private set; }

    public CMacchine(string marca, string numeroSerie, string nominativo, string numeroTelefono, DateTime dataAccettazione, DateTime dataInizioIntervento, bool visionata)
    {
        Marca = marca;
        NumeroSerie = numeroSerie;
        Nominativo = nominativo;
        NumeroTelefono = numeroTelefono;
        DataAccettazione = dataAccettazione;
        DataInizioIntervento = dataInizioIntervento;
        Visionata = visionata;
        Riparazioni = new List<CAggiustao>();
    }

    public void AggiungiRiparazione(CAggiustao riparazione)
    {
        Riparazioni.Add(riparazione);
    }

    public double CalcolaCostoTotale()
    {
        double costoTotale = Riparazioni.Sum(r => r.Costo);

        if (Visionata)
        {
            costoTotale *= 0.9; 
        }

        costoTotale *= (1 - Sconto());

        return costoTotale;
    }

    protected virtual double Sconto()
    {
        return 0.0; 
    }
}


public class CDecespugliatore : CMacchine
{
    bool AScoppio;
    string TipoSupporto;

    public CDecespugliatore(string marca, string numeroSerie, string nominativo, string numeroTelefono, DateTime dataAccettazione, DateTime dataInizioIntervento, bool visionata, bool aScoppio, string tipoSupporto)
        : base(marca, numeroSerie, nominativo, numeroTelefono, dataAccettazione, dataInizioIntervento, visionata)
    {
        AScoppio = aScoppio;
        TipoSupporto = tipoSupporto;
    }

    protected override double Sconto()
    {
        return 0.07; 
    }
}


public class CTosaerba : CMacchine
{
    int NumeroRuote;
    string TipoCarburante;

    public CTosaerba(string marca, string numeroSerie, string nominativo, string numeroTelefono, DateTime dataAccettazione, DateTime dataInizioIntervento, bool visionata, int numeroRuote, string tipoCarburante)
        : base(marca, numeroSerie, nominativo, numeroTelefono, dataAccettazione, dataInizioIntervento, visionata)
    {
        NumeroRuote = numeroRuote;
        TipoCarburante = tipoCarburante;
    }

    protected override double Sconto()
    {
        return 0.05; 
    }
}


public class CMotozappa : CMacchine
{
    int NumeroRuote;

    public CMotozappa(string marca, string numeroSerie, string nominativo, string numeroTelefono, DateTime dataAccettazione, DateTime dataInizioIntervento, bool visionata, int numeroRuote)
        : base(marca, numeroSerie, nominativo, numeroTelefono, dataAccettazione, dataInizioIntervento, visionata)
    {
        NumeroRuote = numeroRuote;
    }

  
}
