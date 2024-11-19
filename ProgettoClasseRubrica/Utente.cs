//classe Utente
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoClasseRubrica;

internal class Utente
{
    public string Nome { get; set; }
    public string Cognome { get; set; }
    public string Email { get; set; }
    public string NumeroTelefono { get; set; }

    // Costruttore per inizializzare un nuovo utente
    public Utente(string nome, string cognome, string email, string numeroTelefono)
    {
        Nome = nome;
        Cognome = cognome;
        Email = email;
        NumeroTelefono = numeroTelefono;
    }
} 
