#include<iostream>
#include<stdlib.h>
#include<ctime>
#include <vector>

using namespace std;

class Tank
{
    int _name;
    int _health = 100;
    int _fuel = 100;
    int _ammo = 100;
    bool _engineUpgrade = false;
    bool _armorUpgrade = false;
    bool _shellUpgrade = false;


    public:
        void SetName(int name);
        void Patrol(int distance);
        void Attack(int enemyStr);
        void Repair(int supply);
        bool Alive();
        void ShowState();
        void UpgradeEngine();
        void UpgradeArmor();
        void UpgradeShell();
        bool IsEngineUpgraded();
        bool IsArmorUpgraded();
        bool IsShellUpgraded();
};

void Tank::SetName(int name)
{
    _name = name;
}

void Tank::Patrol(int distance)
{
    if(IsArmorUpgraded())
    {
        _health = (_health - (distance * 2))/2;
    }
    else
    {
        _health = _health - (distance * 2);
    }
    if(IsEngineUpgraded())
    {
        _fuel = (_fuel - (distance * 8))/2;
    }
    else
    {
        _fuel = _fuel - (distance * 8);
    }
    _ammo = _ammo - (distance * 2);
}

void Tank::Attack(int enemyStr)
{
    if(IsArmorUpgraded())
    {
        _health = (_health - (enemyStr * 7))/2;
    }
    else
    {
        _health = _health - (enemyStr * 7);
    }
    if(IsEngineUpgraded())
    {
        _fuel = (_fuel - (enemyStr * 3))/2;
    }
    else
    {
        _fuel = _fuel - (enemyStr * 3);
    }
    _ammo = _ammo - (enemyStr * 7);
}

void Tank::Repair(int supply)
{
    _health = _health + (supply * 5);
    if(_health > 100)
    {
        _health = 100;
    }
    _fuel = _fuel + (supply * 5);
    if(_fuel > 100)
    {
        _fuel = 100;
    }
    _ammo = _ammo + (supply * 5);
    if(_ammo = 100)
    {
        _ammo = 100;
    }
}

bool Tank::Alive()
{
    if(_health <= 0 || _fuel <= 0 || _ammo <= 0)
    {
        return false;
    }
    else
    {
        return true;
    }
}

void Tank::ShowState()
{
    bool alive = Alive();
    if(alive)
    {
        cout << "\nTank " << _name << ":"
             << "   Salute: " << _health
             << "   Carburante: " << _fuel
             << "   Munizioni: " << _ammo;
             if(IsEngineUpgraded())
             {
                 cout << "   E";
             }
             if(IsArmorUpgraded())
             {
                 cout << "   A";
             }
             if(IsShellUpgraded())
             {
                 cout << "   S";
             }
             cout << endl;
    }
    else
    {
        cout << "\nNome Tank: " << _name
             << "   PERSO" << endl;
    }

}

void Tank::UpgradeEngine()
{
    _engineUpgrade = true;
}

void Tank::UpgradeArmor()
{
    _armorUpgrade = true;
}

void Tank::UpgradeShell()
{
    _shellUpgrade = true;
}

bool Tank::IsEngineUpgraded()
{
    return _engineUpgrade;
}

bool Tank::IsArmorUpgraded()
{
    return _armorUpgrade;
}

bool Tank::IsShellUpgraded()
{
    return _shellUpgrade;
}



bool gameWon(int valor);
bool gameLost(vector< Tank >& team);
int tankAliveCounter(vector< Tank >& team);
void showTeam(vector< Tank >& team);
int chooseMission(vector<Tank>& team, int valor);
void goMission(vector< Tank >& team, int mission);
int valorCounter(vector< Tank >& team, int mission, int valor);
char askYesNo(string question);
int engineUpgradeCounter(vector< Tank >& team);
int armorUpgradeCounter(vector< Tank >& team);
int shellUpgradeCounter(vector< Tank >& team);



int main()
{
    cout << "\n\n\tTANK WAR" << endl;
    cout << "\nSei il capo di una squadra di 5 Tank, puoi mandarli in missione e accumulare punti Valore.";
    cout << "\nLo scopo e' arrivare a 100 punti Valore" << endl;

    srand(time(NULL));

    char again = 'y';

    // Main loop
    while(again == 'y')
    {
        int valor = 0;
        int turnCounter = 1;

        // Creazione team
        vector<Tank> team;
        string tankName;
        for(int i = 0; i < 5; i++)
        {

            Tank tank;
            tank.SetName(i+1);
            team.push_back(tank);
        }

        bool won = false;
        bool lost = false;
        while(!won && !lost)
        {
            cout << "\n\t\tTURNO: " << turnCounter;
            cout << "\nPUNTI VALORE:" << valor;

            // Stampa il Team
            showTeam(team);

            // Chiede la missione al giocatore
            int mission = chooseMission(team, valor);

            // Esegue la missione
            goMission(team, mission);

            // Aggiorna il Valore
            valor = valorCounter(team, mission, valor);

            // Controlla la Vittoria o la Sconfitta
            won = gameWon(valor);
            lost = gameLost(team);

            //system("CLS");
            turnCounter ++;

        }
        if(lost)
        {
            cout << "\n\n\nMi dispiace hai perso" << endl;
            cout << "\nPUNTI VALORE: " << valor << endl;
            showTeam(team);
        }
        else
        {
            cout << "\n\n\nCOMPLIMENTI hai vinto in " << turnCounter << " turni!" << endl;
            cout << "\nPUNTI VALORE: " << valor << endl;
            showTeam(team);
        }

        again = askYesNo("Vuoi giocare di nuovo?");
    }



}

bool gameWon(int valor)
{
    if(valor >= 100)
    {
        return true;
    }
    else
    {
        return false;
    }
}

bool gameLost(vector<Tank>& team)
{
    int tankAlive = tankAliveCounter(team);
    if(tankAlive == 0)
    {
        return true;
    }
    else
    {
        return false;
    }

}

void showTeam(vector<Tank>& team)
{
        cout << "\n\nLa tua Squadra:" << endl;
        for(int i = 0; i < team.size(); i++)
        {
            team[i].ShowState();
        }
}

int chooseMission(vector<Tank>& team, int valor)
{
    int engineUpgraded = engineUpgradeCounter(team);
    int armorUpgraded = armorUpgradeCounter(team);
    int shellUpgraded = shellUpgradeCounter(team);
    int mission = 0;
    cout << "\nScegli la Missione:\n"
         << "\n  1 - Pattugliamento                                           (+1 Punto Valore per ogni Tank vivo)"
         << "\n  2 - Attacca base nemica                                      (+2 Punti Valore per ogni Tank Vivo)"
         << "\n  3 - Rifornimento e Riparazione                               (-5 Punti Valore)"
         << "\n  --------------------------------------------------------------------------------------------------"
         << "\n  4 - Migliora Motore (Bonus: costo Carburante dimezzato)      (-10 Punti Valore)"
         << "\n  5 - Migliora Armatura (Bonus: danni subiti dimezzati)        (-20 Punti Valore)"
         << "\n  6 - Migliora Scudo (Bonus: Punti Valore guadagnati doppi)    (-30 Punti Valore)" << endl;

    while(mission < 1 || mission > 6)
    {
        cin >> mission;

        if(mission < 1 || mission > 6)
        {
            cout << "Non hai inserito un valore corretto" << endl;
        }
        if(engineUpgraded >= 5 || armorUpgraded >= 5 || shellUpgraded >= 5)
        {
            cout << "Hai gia' migliorato tutti i tuoi Tank" << endl;
            mission = 0;
        }
        if( (mission == 3 && valor < 5) || (mission == 4 && valor < 10) || (mission == 5 && valor < 20) || (mission == 6 && valor < 30) )
        {
            cout << "Non hai abbastanza Punti Valore" << endl;
            mission = 0;
        }


    }
    return mission;
}

int tankAliveCounter(vector<Tank>& team)
{
    int counter = 0;
    for(int i = 0; i < team.size(); i++)
    {
        if(team[i].Alive())
        {
            counter++;
        }
    }
    return counter;
}

void goMission(vector<Tank>& team, int mission)
{
    int distance;
    int enemyStr;
    int supply;
    int j = 0;
    bool found = false;
    for(int i = 0; i < team.size(); i++)
    {
        switch(mission)
        {
        case 1:
            distance = (rand() % 2) +2;
            team[i].Patrol(distance);
            break;

        case 2:
            enemyStr = (rand() % 2) + 2;
            team[i].Attack(enemyStr);
            break;

        case 3:
            supply = (rand() % 2) +2;
            team[i].Repair(supply);
            break;

        case 4:
            while(!found && j < team.size())
            {
                if(!team[j].IsEngineUpgraded())
                {
                    team[j].UpgradeEngine();
                    found = true;
                }
                else
                {
                    j++;
                }
            }
            break;

        case 5:
            while(!found && j < team.size())
            {
                if(!team[j].IsArmorUpgraded())
                {
                    team[j].UpgradeArmor();
                    found = true;
                }
                else
                {
                    j++;
                }
            }
            break;

        case 6:
            while(!found && j < team.size())
            {
                if(!team[j].IsShellUpgraded())
                {
                    team[j].UpgradeShell();
                    found = true;
                }
                else
                {
                    j++;
                }
            }
            break;
        }
    }
}

int valorCounter(vector<Tank>& team, int mission, int valor)
{
    int shellUpgraded = shellUpgradeCounter(team);
    int tankAlive = tankAliveCounter(team);

    switch(mission)
    {
    case 1:
        valor += ((tankAlive * 1) + shellUpgraded);
        break;

    case 2:
        valor += ((tankAlive * 2) + shellUpgraded);
        break;

    case 3:
        valor -= (tankAlive * 1);
        break;

    case 4:
        valor -= 10;
        break;

    case 5:
        valor -= 20;
        break;

    case 6:
        valor -= 30;
        break;
    }
    return valor;
}

char askYesNo(string question)
{
    cout << "\n\n" << question << " (y/n) ";
    char answer = ' ';

    while(answer!= 'y' && answer != 'n')
    {
        cin >> answer;
        if(answer!= 'y' && answer != 'n')
        {
            cout << "\nNon hai inserito un carattere corretto" << endl;
        }
    }
    return answer;
}

int engineUpgradeCounter(vector< Tank >& team)
{
    int counter = 0;
    for(int i = 0; i < team.size(); i++)
    {
        if(team[i].IsEngineUpgraded())
        {
            counter++;
        }
    }
    return counter;
}

int armorUpgradeCounter(vector< Tank >& team)
{
    int counter = 0;
    for(int i = 0; i < team.size(); i++)
    {
        if(team[i].IsArmorUpgraded())
        {
            counter++;
        }
    }
    return counter;
}

int shellUpgradeCounter(vector< Tank >& team)
{
    int counter = 0;
    for(int i = 0; i < team.size(); i++)
    {
        if(team[i].IsShellUpgraded())
        {
            counter++;
        }
    }
    return counter;
}
