#include <iostream>
#include <string>
using namespace std;
int main()
{



    cout << "\t\tI NEED SOMEBODY! (Help!)" << endl;

    bool gioco = 1;
    unsigned int scelta1;
    unsigned int scelta2;
    unsigned int scelta3;
    unsigned int scelta4;
    unsigned int scelta5;
    unsigned int scelta6;


    while(gioco)
        {
            bool perso = 0;
            scelta1 = 5;
            cout << "\n\nSei a casa ed e' notte. Ti arriva un messaggio da Silvia"
            << "\n 'Solo tu puoi aiutarmi, mi ha preso!'" << endl;
            while(scelta1 > 4)
            {
                cout << "\nConosci Silvia, e' paranoica. Cosa fai?"
                <<"\n 1 - Sono le 2 di notte, scegli di rispondere domattina"
                <<"\n 2 - Rispondi al messaggio chiedendole cosa sia successo"
                <<"\n 3 - Provi a chiamarla"
                <<"\n 4 - Pensi che sara' una delle sue solite paranoie e la ignori" << "\n" <<endl;
                cin >> scelta1;
                if(scelta1 > 4)
                {
                    cout << "\nNon hai fatto una scelta corretta, riprova" << "\n" << endl;
                }
            }
                switch(scelta1)
                {
                case 1:
                    cout << "\nNon hai risposto ad un'amica in difficolta'"
                    << "\n\n\tHAI PERSO!" << endl;
                    perso = 1;
                    break;

                case 2:
                    perso = 0;
                    break;

                case 3:
                    cout << "\nHai fatto squillare il telefono e il rapitore si e' accorto che Silvia aveva un telefono"
                    << "\n\n\tHAI PERSO!" << endl;
                    perso = 1;
                    break;

                case 4:
                    cout << "\nStavolta aveva veramente bisogno di aiuto"
                    << "\n\n\tHAI PERSO!" << endl;
                    perso = 1;
                    break;
                }
                if(perso == 0)
                {
                    scelta2 = 5;
                    while(scelta2 > 4)
                        {
                            cout << "\n\nSilvia: Mi ha rapita! Lui controlla la polizia e solo tu puoi aiutarmi! Sono in una grande casa gialla vicino al fiume e non so cosa fare!" << endl;
                            cout << "\nLe consigli di:" << endl;
                            cout << "\n 1 - Chiamare la polizia, deve smettere di essere cosi' paranoica"
                            << "\n 2 - Attirare in qualche modo l'attenzione di qualcuno nelle case vicine"
                            << "\n 3 - Cercare un'arma per affrontarlo"
                            << "\n 4 - Cercare un'uscita" << endl;
                            cin >> scelta2;
                            if(scelta2 > 4)
                            {
                                cout << "\nNon hai fatto una scelta corretta, riprova" << "\n" << endl;
                            }
                        }
                            switch(scelta2)
                            {
                            case 1:
                                cout << "\nNon ha chiamato la polizia, ma nel provarci il rapitore ha scoperto che Silvia ha un telefono"
                                << "\n\n\tHAI PERSO!" << endl;
                                perso = 1;
                                break;

                            case 2:
                                cout << "\nLe case vicine sono vuote e la sola attenzione che e' riuscita ad attirare e' quella del rapitore"
                                << "\n\n\tHAI PERSO!" << endl;
                                perso = 1;
                                break;

                            case 3:
                                perso = 0;
                                break;

                            case 4:
                                cout << "\nTutte le uscite sono ben sigillate. Nel provare ad aprire una finestra vieni scoperta dal rapitore"
                                << "\n\n\tHAI PERSO!" << endl;
                                perso = 1;
                                break;
                            }
                            if(perso == 0)
                            {
                                scelta3 = 5;
                                while(scelta3 > 4)
                                {
                                    cout << "\n\nSilvia: 'L'ho colpito con una sedia, sono riuscita ad uscire dalla stanza e mi sono nascosta. Le uscite sono tutte bloccate, non so come fare!" << endl;
                                    cout << "\n 1 - 'Arrivo ad aiutarti' "
                                    << "\n 2 - 'Continua a cercare un'uscita, dovra' essercene una!' "
                                    << "\n 3 - 'Uccidilo, puoi farcela!'"
                                    << "\n 4 - 'Cerca le chiavi, probabilmente pero' le avra' con se' '" << endl;
                                    cin >> scelta3;
                                    if(scelta3 > 4)
                                    {
                                        cout << "\nNon hai fatto una scelta corretta, riprova" << "\n" << endl;
                                    }
                                }
                                    switch(scelta3)
                                    {
                                case 1:
                                        perso = 0;
                                        break;

                                    case 2:
                                        cout << "\nNon ci sono altre uscite e il rapitore ti ha scoperto"
                                        << "\n\n\tHAI PERSO!" << endl;
                                        perso = 1;
                                        break;

                                    case 3:
                                        cout << "\nEeeeeeee no, non ce l'ha fatta..."
                                        << "\n\n\tHAI PERSO!" << endl;
                                        perso = 1;
                                        break;

                                    case 4:
                                        cout << "\nLe aveva davvero con se', solo che non Silvia non e' una ladra abile"
                                        << "\n\n\tHAI PERSO!" << endl;
                                        perso = 1;
                                        break;
                                    }

                                    if(perso == 0)
                                    {
                                    scelta4 = 5;
                                    while(scelta4 > 4)
                                        {
                                            cout << "\n\nRiesci finalmente a trovare la casa gialla" << endl;
                                            cout << "\nCosa fai adesso:" << endl;
                                            cout << "\n 1 - Provi ad entrare dalla finestra della cantina"
                                            << "\n 2 - Provi a sfondare la porta"
                                            << "\n 3 - Suoni il campanello"
                                            << "\n 4 - Provi ad entrare dalla finestra su retro" << endl;
                                            cin >> scelta4;
                                            if(scelta4 > 4)
                                            {
                                                cout << "\nNon hai fatto una scelta corretta, riprova" << "\n" << endl;
                                            }
                                        }
                                            switch(scelta4)
                                            {
                                            case 1:
                                                cout << "\nIl rapitore ha messo una trappola"
                                                << "\n\n\tHAI PERSO!" << endl;
                                                perso = 1;
                                                break;

                                            case 2:
                                                cout << "\nNon so nemmeno perche' ci hai provato"
                                                << "\n\n\tHAI PERSO!" << endl;
                                                perso = 1;
                                                break;

                                            case 3:
                                                perso = 0;
                                                break;

                                            case 4:
                                                cout << "\nIl rapitore ha un complice che ti vede entrare"
                                                << "\n\n\tHAI PERSO!" << endl;
                                                perso = 1;
                                                break;
                                            }

                                            if(perso == 0)
                                            {
                                            scelta5 = 5;
                                            while(scelta5 > 4)
                                                {
                                                    cout << "\n\nTi apre la porta il rapitore" << endl;
                                                    cout << "\n 1 - Trovi una scusa per entrare"
                                                    << "\n 2 - Lo affronti fisicamnete"
                                                    << "\n 3 - Avrebbe un minuto per parlare del nostro salvatore Gesu'?"
                                                    << "\n 4 - Ti fingi un agente di polizia in borghese" << endl;
                                                    cin >> scelta5;
                                                    if(scelta5 > 4)
                                                    {
                                                        cout << "\nNon hai fatto una scelta corretta, riprova" << "\n" << endl;
                                                    }
                                                }
                                                    switch(scelta5)
                                                    {
                                                    case 1:
                                                        cout << "\nLa scusa non era credibile"
                                                        << "\n\n\tHAI PERSO!" << endl;
                                                        perso = 1;
                                                        break;

                                                    case 2:
                                                        cout << "\nNon so nemmeno perche' ci hai provato"
                                                        << "\n\n\tHAI PERSO!" << endl;
                                                        perso = 1;
                                                        break;

                                                    case 3:
                                                        cout << "A nessuno piacciono i Testimoni di Geova"
                                                        << "\n\n\tHAI PERSO!" << endl;
                                                        perso = 1;
                                                        break;

                                                    case 4:
                                                        perso = 0;
                                                        break;
                                                    }
                                                    if(perso == 0)
                                                    {
                                                        scelta6 = 5;
                                                        while(scelta6 > 4)
                                                        {
                                                            cout << "\n\nHai fatto scappare il rapitore" << endl;
                                                            cout << "\n 1 - Cerchi Silvia silenziosamente, magari il rapitore ha dei complici"
                                                            << "\n 2 - Cerchi Silvia chiamandola a voce alta"
                                                            << "\n 3 - Controlli la cantina"
                                                            << "\n 4 - Chiami Silvia al telefono" << endl;
                                                            cin >> scelta6;
                                                            if(scelta6 > 4)
                                                            {
                                                                cout << "\nNon hai fatto una scelta corretta, riprova" << "\n" << endl;
                                                            }
                                                        }
                                                            switch(scelta6)
                                                            {
                                                            case 1:
                                                                cout << "\nSilvia ti ha sentito dire che sei della polizia, non si fida di te" << endl;
                                                                cout << "\n\n\tHAI PERSO!" << endl;
                                                                perso = 1;
                                                                break;

                                                            case 2:
                                                                cout << "\nIl rapitore ha un complice che ti sente" << endl;
                                                                cout << "\n\n\tHAI PERSO!" << endl;
                                                                perso = 1;
                                                                break;

                                                            case 3:
                                                                cout << "\nNella cantina c'è' una trappola" << endl;
                                                                cout << "\n\n\tHAI PERSO!" << endl;
                                                                perso = 1;
                                                                break;

                                                            case 4:
                                                                cout << "\n\nCOMPLIMENTI, SEI RIUSCITO A SALVARE SILVIA NONOSTANTE LE DIFFICOLTA'. SEI UN VERO EROE!" << endl;
                                                                perso = 0;
                                                                break;
                                                            }
                                                        }
                                            }

                                            }

                                            }
                                        }





            char restart = 'f';
            while(restart != 'y' && restart != 'n')
            {
                cout << "\nVuoi ricominciare? "
                << "\n y - Ricomincia"
                << "\n n - Chiudi il gioco" << endl;
                cin >> restart;
                if(restart != 'y' && restart != 'n' )
                {
                    cout << "\nNon hai fatto una scelta corretta" << endl;
                }
                else if(restart == 'y')
                {
                    gioco = 1;
                }
                else if(restart == 'n')
                {
                    cout << "\nSpero ti sia piaciuto" << endl;
                    cout <<"\n\n--------------------FINE--------------------" << endl;
                    gioco = 0;
                    return 0;
                }
            }
        }
}



