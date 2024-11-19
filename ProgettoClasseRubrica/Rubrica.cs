//classe Rubrica
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoClasseRubrica;

public class Rubrica
{
    const string FilePath = "rubrica.csv"; //contiene il nome del file CSV dove verranno salvati i contatti.

    static Dictionary<string, Utente> contatti = new Dictionary<string, Utente>(); //struttura dati che memorizza coppie chiave-valore 
                                                                                   //contatti è un dizionario dove la chiave è l'email del contatto e il valore è un array di stringhe contenente informazioni sul contatto (in questo caso, nome, cognome e numero di telefono).
    public static void VisualizzaContatti()
    {
        if (contatti.Count == 0)
        {
            Console.WriteLine("Nessun contatto trovato.");
            return;
        }

        foreach (var contatto in contatti)
        {
            Console.WriteLine($"Nome: {contatto.Value.Nome}, Cognome: {contatto.Value.Cognome}, Email: {contatto.Value.Email}, Telefono: {contatto.Value.NumeroTelefono}");
        }
    }

    public static void AggiungiContatto() //aggiunge contatti
    {
        Console.Write("Nome: ");
        string? nome = Console.ReadLine();

        Console.Write("Cognome: ");
        string? cognome = Console.ReadLine();

        Console.Write("Email: ");
        string? email = Console.ReadLine();

        Console.Write("Numero di Telefono: ");
        string? numeroTelefono = Console.ReadLine();

        if (contatti.ContainsKey(email))
        {
            Console.WriteLine("Contatto già esistente.");
            return;
        }

        if (!email.Contains("@"))
        {
            Console.WriteLine("ERRORE: \nl'email deve contenere una @");
            return;
        }

        if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(cognome) || string.IsNullOrEmpty(numeroTelefono))
        {
            Console.WriteLine("ERRORE: \nContatto non salvato, inserisci tutti i dati.");
            return;
        }

        // Crea un nuovo oggetto Utente e aggiungilo al dizionario
        var utente = new Utente(nome, cognome, email, numeroTelefono);
        contatti[email] = utente; // Usa l'email come chiave
        Console.WriteLine("Contatto aggiunto.");
        SalvaContatti();
    }

    public static void ModificaContatto() //modifica i contatti presenti
    {
        Console.Write("Inserisci l'email del contatto da modificare: ");
        string? email = Console.ReadLine();

        if (!contatti.ContainsKey(email))
        {
            Console.WriteLine("Contatto non trovato.");
            return;
        }

        var contatto = contatti[email]; // Recupera l'oggetto Utente

        Console.Write("Nuovo Nome (premi invio per mantenere): ");
        string? nuovoNome = Console.ReadLine();
        Console.Write("Nuovo Cognome (premi invio per mantenere): ");
        string? nuovoCognome = Console.ReadLine();
        Console.Write("Nuovo Numero di Telefono (premi invio per mantenere): ");
        string? nuovoNumero = Console.ReadLine();

        // Modifica solo se il campo non è vuoto
        if (!string.IsNullOrEmpty(nuovoNome)) contatto.Nome = nuovoNome;
        if (!string.IsNullOrEmpty(nuovoCognome)) contatto.Cognome = nuovoCognome;
        if (!string.IsNullOrEmpty(nuovoNumero)) contatto.NumeroTelefono = nuovoNumero;

        Console.WriteLine("Contatto modificato.");
        SalvaContatti();
    }

    public static void EliminaContatto()
    {
        Console.Write("Inserisci l'email del contatto da eliminare: ");
        string? email = Console.ReadLine();

        if (contatti.Remove(email))  //rimuove la coppia chiave-valore con la chiave specificata
        {
            Console.WriteLine("Contatto eliminato."); //se lo trova, lo elimina e restituisce true
            SalvaContatti();
            return;
        }
        Console.WriteLine("Contatto non trovato."); //se non lo trova, restituisce false
    }

    public static void EliminaRubrica()
    {

        Console.WriteLine("Sei sicuro di voler eliminare la rubrica (S/N)?");
        string risposta = Console.ReadLine().ToUpper().Trim();
        if (risposta == "S") //ToUpper() converte l'input in maiuscolo
        {
            contatti.Clear(); // "pulisce" il dizionario (rimuove tutte le coppie dal dizionario)
            File.Delete(FilePath); //elimina il file
            Console.WriteLine("Rubrica eliminata.");
        }
        else if (risposta == "N")
        {
            Console.WriteLine("Eliminazione annullata.");
        }
        else
        {
            Console.WriteLine("Spiacente, operazione non valida.");
        }
    }


    public static void ImportaContatti() //importa tutti i dati da un file.csv alla rubrica.csv
    {
        Console.Write("Come si chiama il file da importare? ");
        string? fileName = Console.ReadLine();
        fileName = fileName.EndsWith(".csv") ? fileName : fileName + ".csv"; // Aggiunge il .csv se non presente

        if (!File.Exists(fileName)) // Se il file non esiste, mostra un messaggio di errore
        {
            Console.WriteLine("File non trovato.");
            return;
        }

        var righe = File.ReadAllLines(fileName); // Legge tutte le righe del file in un array

        foreach (string riga in righe) // Scorre ogni riga del file
        {
            string[] dati = riga.Split(','); // Divide la riga in base alla virgola

            if (dati.Length == 4) // Verifica che ci siano 4 elementi (Nome, Cognome, Email, Telefono)
            {
                string? nome = dati[0];
                string? cognome = dati[1];
                string? email = dati[2];
                string? telefono = dati[3];

                var nuovoUtente = new Utente(nome, cognome, email, telefono); // Crea un oggetto Utente

                if (contatti.ContainsKey(email)) // Controlla se l'utente esiste già
                {
                    Console.WriteLine($"Utente con email {email} già esistente. Vuoi sovrascriverlo nella rubrica? (S/N)");

                    string risposta = Console.ReadLine().ToUpper().Trim();

                    if (risposta == "N")
                    {
                        Console.WriteLine("Nessuna sovrascrizione eseguita.");
                    }
                    else if (risposta == "S")
                    {
                        contatti[email] = nuovoUtente; // Sovrascrive il contatto esistente
                        Console.WriteLine("Contatto sovrascritto.");
                    }
                    else
                    {
                        Console.WriteLine("Spiacente, operazione non valida.");
                    }
                }
                else
                {
                    contatti[email] = nuovoUtente; // Aggiunge il nuovo utente alla rubrica
                }
            }
        }

        SalvaContatti(); // Salva i contatti aggiornati nel file
        Console.WriteLine("Contatti importati.");
    }

    public static void EsportaContatti()
    {
        Console.Write("Come vuoi chiamare il file? ");
        string? fileName = Console.ReadLine();
        fileName = fileName.EndsWith(".csv") ? fileName : fileName + ".csv";

        var righe = new List<string>();
        foreach (var contatto in contatti)
        {
            var utente = contatto.Value;
            string? riga = $"{utente.Nome},{utente.Cognome},{utente.Email},{utente.NumeroTelefono}";
            righe.Add(riga);
        }

        File.WriteAllLines(fileName, righe);
        Console.WriteLine("Contatti esportati.");
    }


    public static void SalvaContatti()
    {
        var righe = new List<string>(); // Crea una lista di stringhe

        foreach (var contatto in contatti) // Scorre ogni contatto
        {
            var utente = contatto.Value;
            string? riga = $"{utente.Nome},{utente.Cognome},{utente.Email},{utente.NumeroTelefono}";
            righe.Add(riga);
        }

        File.WriteAllLines(FilePath, righe); // Scrive le righe nel file CSV
        Console.WriteLine("Contatti aggiornati nel file rubrica.csv.");
    }

    public static void CaricaContatti()
    {
        if (File.Exists(FilePath))
        {
            string[] righe = File.ReadAllLines(FilePath);
            foreach (string riga in righe)
            {
                string[] dati = riga.Split(',');
                if (dati.Length == 4)
                {
                    string email = dati[2];
                    Utente utente = new Utente(dati[0], dati[1], dati[2], dati[3]);
                    contatti[email] = utente;
                }
                else
                {
                    Console.WriteLine($"Formato errato nella riga: {riga}");
                }
            }
            Console.WriteLine("Contatti caricati dalla rubrica.csv.");
        }
    }
}
