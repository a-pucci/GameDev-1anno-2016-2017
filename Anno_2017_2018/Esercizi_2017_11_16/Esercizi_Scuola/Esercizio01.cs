using System.Collections.Generic;

// La classe Items serve per evitare errori del compilatore
public class Item { } 

// Enumeratore per i vari tipi di Heart che è possibile trovare
public enum Heart { Empty, Red, Blue, Black };

public class Player
{
    // Statistiche del player
    private int health;
    private int movementSpeed;
    private int damage;

    // Numero massimo dei cuori rossi che può avere inizialmente
    private int maxRedHearts = 6;

    // Oggetti che può possedere il player
    private int keys;
    private int bombs;
    private int coins;
    private Item[] activeItems;
    private Item[] passiveItems;
    private Item card;
    private Item trinket;

    // Lista dei cuori del player, inizialmente inizializzata a 6 mezzi cuori Red
    public List<Heart> hearts = new List<Heart>() { Heart.Red, Heart.Red, Heart.Red, Heart.Red, Heart.Red, Heart.Red };

    // Metodi per le azioni che può compiere il player
    public void Move() { }
    public void Fire() { }
    public void Use(Item item) { }

    // Metodo per rimuovere cuori dal player
    public void RemoveHeart(int heartsToRemove)
    {
        for (int i = 0; i < heartsToRemove; i++)
        {
            // Controlla l'ultimo elemento della lista:
            // Se è Black o Blue, l'elemento viene rimosso
            // Se è Red, viene sostituito con l'elemento Empty
            // Se è Empty, aggiungo + 1 a heartsToRemove in modo da fare un ciclo in più "ignorare" l'elemento Empty
            switch (hearts[hearts.Count - 1])
            {
                case Heart.Black:
                    hearts.RemoveAt(hearts.Count - 1);
                    break;

                case Heart.Blue:
                    hearts.RemoveAt(hearts.Count - 1);
                    break;

                case Heart.Red:
                    hearts[hearts.Count - 1] = Heart.Empty;
                    break;

                case Heart.Empty:
                    if ((hearts.Count - 1) != 0)
                    {
                        heartsToRemove++;
                    }
                    break;

                default:
                    break;
            }
        }
    }

    // Estensione di RemoveHeart aggiungendo il tipo di cuore da rimuovere
    public void RemoveHeart(int heartsToRemove, Heart heartType)
    {
            switch (heartType)
            {
                // Se il cuore è Black, parto dal fondo della lista a rimuovere gli elementi e per sicurezza faccio un controllo
                case Heart.Black:
                    for (int i = 0; i < heartsToRemove; i++)
                    { 
                        if (hearts[hearts.Count - 1] == Heart.Black)
                        {
                            hearts.RemoveAt(hearts.Count - 1);
                        }
                    }
                    break;

                // Se il cuore è Blue, rimuovo gli elementi successivi ai Red o Empty e per sicurezza faccio un controllo
                case Heart.Blue:
                    for (int i = 0; i < heartsToRemove; i++)
                    {
                        if (hearts[maxRedHearts + 1] == Heart.Blue)
                        {
                            hearts.RemoveAt(maxRedHearts + 1);
                        }
                    }
                    break;

                // Se il cuore è Red, cerco l'indice dell'ultimo cuore Red e lo sostituisco con Empty
                case Heart.Red:
                    int lastRedHeartIndex;
                    for (int i = 0; i < heartsToRemove; i++)
                    {
                        lastRedHeartIndex = hearts.LastIndexOf(Heart.Red);
                        hearts[lastRedHeartIndex] = Heart.Empty;
                    }
                    break;

                default:
                    break;
            }
            
    }
        

    // Metodo per aggiungere Heart al player
    public void AddHeart(int heartsToAdd, Heart heartType)
    {
        // Controllo dei casi in base al tipo di Heart da aggiungere 
        switch (heartType)
        {
            // Guardo se l'indice dell'ultimo cuore Red è minore o uguale al numero massimo di cuori Red che è possibile avere
            // Se è minore rimpiazzo i cuori successivi, che saranno Empty, 
            // con cuori Red fino a che o non raggiungo i cuori massimi o non finisco i cuori da aggiungere
            case Heart.Red:
                int lastRedHeartIndex = hearts.LastIndexOf(Heart.Red);
                for (int i = 0; i < heartsToAdd; i++)
                {
                    if(lastRedHeartIndex + i + 1 <= maxRedHearts)
                    {
                        hearts[lastRedHeartIndex + i + 1] = Heart.Red;
                    }
                }
                break;

            // Conto gli elementi presenti nella lista 
            // se gli elementi sono uguali al numero massimo di cuori Red 
            // vuol dire che non ho cuori aggiuntivi e che posso aggiungere in fondo alla lista i cuori Blue
            // (anche se i cuori Red sono minori del massimo, i restanti sono elementi Empty) 
            // se gli elementi sono maggiori, vuol dire che ho dei cuori aggiuntivi e inserico i cuori Blue dopo l'ultimo elemento dei cuori Red o Empty
            // in modo da non importare se i cuori aggiuntivi sono Blue o Black
            case Heart.Blue:
                for (int i = 0; i < heartsToAdd; i++)
                {
                    if(hearts.Count  == maxRedHearts)
                    {
                        hearts.Add(Heart.Blue);
                    }
                    else if (hearts.Count > maxRedHearts)
                    {
                        hearts.Insert(maxRedHearts + 1, Heart.Blue);
                    }
                }
                break;
            
            // Essendo i cuori Black gli ultimi della lista, semplicemente aggiungo i nuovi cuori in fondo alla lista
            case Heart.Black:
                for (int i = 0; i < heartsToAdd; i++)
                {
                    hearts.Add(Heart.Black);
                }
                break;

            default:
                break;
        }
    }

    // Estensione di AddHeart in modo da aumentare il limite massimo di cuori Red
    // Ho commentato solo le parti diverse dal metodo precedente
    public void AddHeart(int heartsToAdd, Heart heartType, bool isPowerUp)
    {

        switch (heartType)
        {
            case Heart.Red:
                int lastRedHeartIndex = hearts.LastIndexOf(Heart.Red);
                
                // Controllo se è il caso in cui devo aumentare cuori Red aggiuntivi
                if(isPowerUp)
                {
                    // Conto il numero di cuori da aggiungere al limite massimo
                    // ho fatto tutti i passsaggi per la chiarezza, non per l'efficienza
                    int currentRedHearts = maxRedHearts - lastRedHeartIndex + 1;
                    int newMaxRedHearts = currentRedHearts + heartsToAdd;
                    int emptyHeartsToAdd = newMaxRedHearts - maxRedHearts;

                    // Se il numero degli elementi è uguale al massimo, vuol dire che non ho altri elementi dopo i Red o Empty 
                    // e posso aggiungere i nuovi Empty in fondo alla lista
                    if (hearts.Count == maxRedHearts)
                    {
                        for (int i = 0; i < emptyHeartsToAdd; i++)
                        {
                            hearts.Add(Heart.Empty);
                        }
                    }
                    // Se il numero degli elementi è maggiore, vuol dire che ho altri elementi dopo i Red o Empty
                    // e inserisco i nuovi Empty dopo l'ultimo elemento dei Red o Empty
                    else if (hearts.Count > maxRedHearts)
                    {
                        for (int i = 0; i < emptyHeartsToAdd; i++)
                        {
                            hearts.Insert(maxRedHearts + 1, Heart.Empty);
                        }
                    }
                    // Aggiorno il valore massimo dei cuori Red che il player può avere
                    maxRedHearts = newMaxRedHearts;
                }

                // Guardo se l'indice dell'ultimo cuore Red è minore o uguale al numero massimo di cuori Red che è possibile avere
                // Se è minore rimpiazzo i cuori successivi, che saranno Empty, 
                // con cuori Red fino a che o non raggiungo i cuori massimi o non finisco i cuori da aggiungere
                for (int i = 0; i < heartsToAdd; i++)
                {
                    if (lastRedHeartIndex + i + 1 <= maxRedHearts)
                    {
                        hearts[lastRedHeartIndex + i + 1] = Heart.Red;
                    }
                }

                break;

            case Heart.Blue:
                for (int i = 0; i < heartsToAdd; i++)
                {
                    if (hearts.Count == maxRedHearts)
                    {
                        hearts.Add(Heart.Blue);
                    }
                    else if (hearts.Count > maxRedHearts)
                    {
                        hearts.Insert(maxRedHearts + 1, Heart.Blue);
                    }
                }
                break;

            case Heart.Black:
                for (int i = 0; i < heartsToAdd; i++)
                {
                    hearts.Add(Heart.Black);
                }
                break;

            default:
                break;
        }
    }
}
