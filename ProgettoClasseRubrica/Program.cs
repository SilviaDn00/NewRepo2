//classe Program
namespace ProgettoClasseRubrica;
class Program 
{
    static void Main() //metodo principale dell'applicazione, che viene eseguito all'avvio
    {
        Rubrica.CaricaContatti(); //per richiamare i contatti all'avvio dell'applicazione

        Console.WriteLine("Benvenuto nella Rubrica, cosa vorresti fare?");
        while (true) //inizia un ciclo infinito finchè non lo interrompe l'utente
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Visualizza contatti");
            Console.WriteLine("2. Aggiungi contatto");
            Console.WriteLine("3. Modifica contatto");
            Console.WriteLine("4. Elimina contatto");
            Console.WriteLine("5. Elimina rubrica");
            Console.WriteLine("6. Importa contatti");
            Console.WriteLine("7. Esporta contatti");
            Console.WriteLine("0. Esci");
            Console.Write("Scegli un'opzione: ");

            switch (Console.ReadLine()) //Legge l'input dell'utente e utilizza switch per determinare quale azione eseguire in base al valore inserito
            {
                case "1":
                    Rubrica.VisualizzaContatti();
                    break;
                //senza i break, il programma eseguirebbe tutti i case
                case "2":
                    Rubrica.AggiungiContatto();
                    break;
                case "3":
                    Rubrica.ModificaContatto();
                    break;
                case "4":
                    Rubrica.EliminaContatto();
                    break;
                case "5":
                    Rubrica.EliminaRubrica();
                    break;
                case "6":
                    Rubrica.ImportaContatti();
                    break;
                case "7":
                    Rubrica.EsportaContatti();
                    break;
                case "0":
                    return; //termina l'esecuzione del programma
                default:
                    Console.WriteLine("Spiacente, opzione non valida.");
                    break;
            }
        }
    }
}