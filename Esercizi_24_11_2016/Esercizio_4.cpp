#include <iostream>
#include <string>
#include <cstdlib>
using namespace std;

void explore(int par[]);
void defend(int par[]);
void escort(int par[]);
void printShip(int par[]);
string getMission();
bool gameLost(int par[]);

enum shipship {FUEL, AMMO, SUPP};

int main()
{

    bool again = true;


    while(again)
    {
        bool lost = false;
        system("CLS");
        cout << "\n\t\tSPACE SHIP" << endl;
        cout << "\n\nSei il comandante di una navicella spaziale, hai a disposizione del carburante, delle munizioni e delle scorte"
        << "\nper poter andare in missione e aiutare la Flotta Terrestre. Abbiamo bisogno di te!" << endl;
        int ship[3] = {100, 50, 100};
        printShip(ship);

        while(!lost)
        {
            string mission = getMission();

             if(mission == "esplorazione")
             {
                 explore(ship);
                 printShip(ship);
                 lost = gameLost(ship);
             }
             else if(mission == "difesa")
             {
                 defend(ship);
                 printShip(ship);
                 lost = gameLost(ship);
             }
             else if(mission == "scorta")
             {
                 escort(ship);
                 printShip(ship);
                 lost = gameLost(ship);
             }
             if(lost)
             {
                 cout << "\n\nMi dispiace ma non puoi continuare con le missioni" << endl;
                 lost = true;
             }
        }
        char choice = ' ';
        while(choice != 'y' && choice != 'n')
        {
            cout << "\nVuoi giocare ancora?"
            << "\n  y - Yes"
            << "\n  n - No" << endl;
            cin >> choice;
            if(choice != 'y' && choice != 'n')
            {
                cout << "\n\nNon hai inserito uan scelta corretta" << endl;
            }
        }
        if(choice == 'y')
        {
            again = true;
        }
        else if(choice  == 'n')
        {
            again = false;
        }
    }
    return 0;
}

void explore(int par[])
{
    system("CLS");
    cout << "\n\n*** Hai esplorato una parte ancora sconosciuta dell' universo, c'e' ancora molto da esplorare ***" << endl;
    par[FUEL] -= 20;
    par[SUPP] -= 20;
}

void defend(int par[])
{
    system("CLS");
    cout << "\n\n*** Hai coraggiosamente difeso la Terra da quegli esseri verdi, sei un vero eroe ***" << endl;
    par[FUEL] -= 5;
    par[SUPP] -= 5;
    par[AMMO] -= 20;
}

void escort(int par[])
{
    system("CLS");
    cout << "\n\n*** Grazie alla tua scorta i ricercatori sono riusciti a scoprire una nuova forma di vita su un pianeta lontano ***" << endl;
    par[FUEL] -= 10;
    par[AMMO] -= 10;
    par[SUPP] -= 10;
}

bool gameLost(int par[])
{
    if(par[FUEL] <= 0 && par[AMMO] <=0 && par[SUPP] <= 0)
    {
        cout << "\n~~~ Hai finito tutto, hai usato al massimo le risorse a tua disposizione! COMPLIMENTI! ~~~" << endl;
        return true;
    }

    if(par[FUEL] <= 0 && par[SUPP] <= 0)
    {
        cout << "\n~~~ Hai finito il carburante e le scorte ~~~" << endl;
        return true;
    }


    if(par[FUEL] <= 0)
    {

        cout << "\n~~~ Sembra che sei rimasto senza carburante ~~~" << endl;
        return true;
    }

    if(par[AMMO] <= 0)
    {
        cout << "\n~~~ Non puoi combattere senza munizioni ~~~" << endl;
        return true;
    }

    if(par[SUPP] <= 0)
    {
        cout << "\n~~~ L'equipaggio inizia ad avere fame e non hai piu' scorte ~~~" << endl;
        return true;
    }
    return false;
}

void printShip(int par[])
{
    if(par[FUEL] < 0)
    {
        par[FUEL] = 0;
    }
    if(par[AMMO] < 0)
    {
        par[AMMO] = 0;
    }
    if(par[SUPP] < 0)
    {
        par[SUPP] = 0;
    }
    cout << "\nLa tua nave:\n"
    << "\n  Carburante: " << par[FUEL]
    << "\n  Munizioni: " << par[AMMO]
    << "\n  Scorte: " << par[SUPP] << endl;
}

string getMission()
{
    cout << "\n\nPuoi mandare la tua navicella in missione" << endl;
    string mission = " ";
    while(mission != "esplorazione" && mission != "difesa" && mission != "scorta")
    {
        cout << "\nScegli la missione:\n"
        << "\n  Esplorazione - Manda la tua navicella ad esplorare l'universo\t\t(Costo: 20 Carburante, 20 Scorte)"
        << "\n  Difesa - Difendi la Terra da gli attacchi alieni\t\t\t(Costo: 5 Carburante, 5 Scorte, 20 Munizioni)"
        << "\n  Scorta - Scorta i ricercatori nel loro viaggio di ricerca\t\t(Costo: 10 Carburante, 10 Scorte, 10 Munizioni)"
        << "\n" << endl;
        cin >> mission;
        if(mission != "esplorazione" && mission != "difesa" && mission != "scorta")
        {
            cout << "\nMi dispiace, non hai scelto una missione corretta, scegli tra: esplorazione  - difesa - scorta\n" << endl;
        }
    }
    return mission;
}
