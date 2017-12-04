#include<iostream>
#include<stdlib.h>
#include<ctime>

using namespace std;

class Character
{
    string _name;
    int _health;
    int _will;
    int _attack;
    int _intimidation;

public:
    Character(string name, int health, int will, int attack, int intimidation);
    string GetName() const;
    void SetHealth(int health);
    int GetHealth() const;
    void SetWill(int will);
    int GetWill() const;
    int GetAttack() const;
    int GetIntim() const;
};

Character::Character(string name,int health, int will, int attack, int intimidation)
{
    _name = name;
    _health = health;
    _will = will;
    _attack = attack;
    _intimidation = intimidation;
}

string Character::GetName() const
{
    return _name;
}

void Character::SetHealth(int health)
{
    _health = health;
}

int Character::GetHealth() const
{
    return _health;
}

void Character::SetWill(int will)
{
    _will = will;
}

int Character::GetWill() const
{
    return _will;
}

int Character::GetAttack() const
{
    return _attack;
}

int Character::GetIntim() const
{
    return _intimidation;
}

int getPlayerMove();
int keepHunting(Character& player, Character& dracula, int encountersWon);
bool foundDracula(int chanceFindDracula);
void draculaEncounter(Character& player, Character& dracula);
void normalEncounter(Character& player);
void playerTurn(Character& player, Character& enemy);
void playerTurn(Character& player, Character& enemy1, Character& enemy2, Character& enemy3);
void enemyTurn(Character& player, Character& enemy);
void playerAttack(Character& attacker, Character& defender);
void playerIntimidate(Character& attacker, Character& defender);
void enemyAttack(Character& attacker, Character& defender);
void enemyIntimidate(Character& attacker, Character& defender);
int chooseEnemy();
void showStats(Character& character);
bool isAlive(Character& character);
void rest(Character& player);
void recover(Character& player);
char askYesNo(string question);



int main()
{
    srand(time(NULL));
    char again = 'y';

    //*****informazioni base****
    cout << "\n\t\tVAN HELSING vs DRACULA"
         << "\n\nVesti i panni del Dottor Van Helsing con la missione di uccidere Dracula"
         << "\nDurante la ricerca potresti pero' imbatterti nei suoi seguaci."
         << "\nIl tuo viaggio finira' se esaurisci la Salute o la Volonta'" << endl;

    //main loop
    while(again)
    {
        Character vanHelsing("Van Helsing", 100, 100, 10, 10);
        Character dracula("Dracula", 50, 50, 20, 20);
        int playerMove;
        int encountersWon = 0;
        int i = 0;

        do
        {
            cout << "\nBATTAGLIE VINTE: " << encountersWon << endl;
            playerMove = getPlayerMove();
            switch(playerMove)
            {
                case 1: encountersWon = keepHunting(vanHelsing, dracula, encountersWon); break;
                case 2: cout << 1; rest(vanHelsing); break;
            }

        }while(isAlive(vanHelsing) && isAlive(dracula));

        if(isAlive(vanHelsing))
        {
            cout << "\n\nCOMPLIMENTI! Sei riuscito a sconfiggere Dracula" << endl;
        }
        else
        {
            cout << "\n\nPeccato, non sei riuscito a sconfiggere Dracula e i suoi seguaci"  << endl;
        }
        again = askYesNo("Vuoi ricominciare la tua avventura?");
    }
    return 0;
}

int getPlayerMove()
{
    int playerMove = 0;
    cout << "\n\nCosa vuoi fare?\n"
         << "\n  1 - Caccia"
         << "\n  2 - Riposati" << endl;
    while(playerMove != 1 && playerMove != 2)
    {
        cin >> playerMove;
        if(playerMove != 1 && playerMove != 2)
        {
            cout << "\nNon hai inserito un valore corretto" << endl;
        }
    }
    return playerMove;
}

int keepHunting(Character& player, Character& dracula, int encountersWon)
{
    if(foundDracula(encountersWon))
    {
        draculaEncounter(player, dracula);
    }
    else
    {
        normalEncounter(player);
    }

    if(isAlive(player))
    {
        encountersWon++;
    }
    return encountersWon;
}

bool foundDracula(int encountersWon)
{
    int chance = rand() % 10;
    bool found = false;
    if(chance < encountersWon)
    {
        found = true;
    }

    return found;
}

void draculaEncounter(Character& player, Character& dracula)
{
    cout << "\nHai finalmente incontrato Dracula!" << endl;

    while(isAlive(player) && isAlive(dracula))
    {
        cout << "\n************************************************************************\n";
        showStats(player);
        cout << "\n";
        showStats(dracula);
        cout << "\n************************************************************************\n";
        playerTurn(player, dracula);
        enemyTurn(player, dracula);
    }
    cout << "\n************************************************************************\n";
    showStats(player);
    cout << "\n";
    showStats(dracula);
    cout << "\n************************************************************************\n";
}

void normalEncounter(Character& player)
{
        int enemy = chooseEnemy();
        if(enemy == 0)
        {
            cout << "\nHai incontrato dei Servitori" << endl;
            Character minion1("Servitore 1", 12, 30, 12, 10);
            Character minion2("Servitore 2", 12, 30, 12, 10);
            Character minion3("Servitore 3", 12, 30, 12, 10);

            while( isAlive(player) && (isAlive(minion1) || isAlive(minion2) || isAlive(minion3)) )
            {
                cout << "\n************************************************************************\n";
                showStats(player);
                cout << "\n";
                showStats(minion1);
                showStats(minion2);
                showStats(minion3);
                cout << "\n************************************************************************\n";
                playerTurn(player, minion1, minion2, minion3);
                enemyTurn(player, minion1);
                enemyTurn(player, minion2);
                enemyTurn(player, minion3);

            }
            cout << "\n************************************************************************\n";
            showStats(player);
            cout << "\n";
            showStats(minion1);
            showStats(minion2);
            showStats(minion3);
            cout << "\n************************************************************************\n";
        }
        else
        {
            cout << "\nHai incontrato degli Spettri" << endl;
            Character spectre1("Spettro 1", 100, 12, 2, 20);
            Character spectre2("Spettro 2", 100, 12, 2, 20);
            Character spectre3("Spettro 3", 100, 12, 2, 20);

            while(isAlive(player) && (isAlive(spectre1) || isAlive(spectre2) || isAlive(spectre2)) )
            {
                cout << "\n************************************************************************\n";
                showStats(player);
                cout << "\n";
                showStats(spectre1);
                showStats(spectre2);
                showStats(spectre3);
                cout << "\n************************************************************************\n";
                playerTurn(player, spectre1, spectre2, spectre3);
                enemyTurn(player, spectre1);
                enemyTurn(player, spectre2);
                enemyTurn(player, spectre3);
            }
            cout << "\n************************************************************************\n";
            showStats(player);
            cout << "\n";
            showStats(spectre1);
            showStats(spectre2);
            showStats(spectre3);
            cout << "\n************************************************************************\n";
        }
}

void playerTurn(Character& player, Character& enemy)
{
    cout << "\nCosa vuoi fare?"
         << "\n  1 - Attacca"
         << "\n  2 - Intimidisci" << endl;
    int choice = 0;
    while(choice != 1 && choice != 2)
    {
        cin >> choice;
        if(choice != 1 && choice != 2)
        {
            cout << "Non hai inserito un valore corretto" << endl;
        }
    }
    switch(choice)
    {
        case 1: playerAttack(player, enemy); break;
        case 2: playerIntimidate(player, enemy); break;
    }
}

void playerTurn(Character& player, Character& enemy1, Character& enemy2, Character& enemy3)
{
    cout << "\nCosa vuoi fare?"
         << "\n  1 - Attacca"
         << "\n  2 - Intimidisci" << endl;
    int choice = 0;
    while(choice != 1 && choice != 2)
    {
        cin >> choice;
        if(choice != 1 && choice != 2)
        {
            cout << "Non hai inserito un valore corretto" << endl;
        }
    }
    switch(choice)
    {
        case 1:
            playerAttack(player, enemy1);
            playerAttack(player, enemy2);
            playerAttack(player, enemy3);
            break;
        case 2:
            playerIntimidate(player, enemy1);
            playerIntimidate(player, enemy2);
            playerIntimidate(player, enemy3);
            break;
    }
}

void playerAttack(Character& attacker, Character& defender)
{
    defender.SetHealth(defender.GetHealth() - (attacker.GetAttack() + (rand() % 5)) );
}

void playerIntimidate(Character& attacker, Character& defender)
{
    defender.SetWill(defender.GetWill() - (attacker.GetIntim() + (rand() % 5)) );
}

void enemyAttack(Character& attacker, Character& defender)
{
    defender.SetHealth(defender.GetHealth() - attacker.GetAttack());
}

void enemyIntimidate(Character& attacker, Character& defender)
{
    defender.SetWill(defender.GetWill() - attacker.GetIntim());
}

void enemyTurn(Character& player, Character& enemy)
{
    int enemyMove = rand() % 2;
    if(enemyMove == 0)
    {
        enemyAttack(enemy, player);
    }
    else
    {
        enemyIntimidate(enemy, player);
    }
}

int chooseEnemy()
{
    int enemy = rand() % 2;
    return enemy;
}

void showStats(Character& character)
{
    if(isAlive(character))
    {
        cout << character.GetName() << ":" << "   Salute: " << character.GetHealth() << "   Volonta': " << character.GetWill()
        << "   Attacco: " << character.GetAttack() << "   Intimidazione: " << character.GetIntim() << endl;
    }
    else
    {
        cout << character.GetName() << ":" << "   SCONFITTO" << endl;
    }

}

bool isAlive(Character& character)
{
    if(character.GetHealth() <= 0 || character.GetWill() <= 0)
    {
        return false;
    }
    else
    {
        return true;
    }
}

void rest(Character& player)
{

    int chance = rand() % 10;
    if(chance < 7)
    {
        cout << "\nSei riuscito a riposare un po' e hai recuperato le energie" << endl;
        recover(player);
    }
    else
    {
        cout << "\nSei stato interrotto durante il pisolino" << endl;
        normalEncounter(player);
    }
}

void recover(Character& player)
{
    int healthRecover = (rand() % 5) + 5;
    int willRecover = (rand() % 5) + 5;

    player.SetHealth(player.GetHealth() + healthRecover);
    if(player.GetHealth() > 100)
    {
        player.SetHealth(100);
    }

    player.SetWill(player.GetWill() + willRecover);
    if(player.GetWill() > 100)
    {
        player.SetWill(100);
    }
    cout << "\n************************************************************************\n";
    showStats(player);
    cout << "\n************************************************************************\n";
}

char askYesNo(string question)
{
    char answer = ' ';
    cout << "\n" << question << " (y/n) ";
    while(answer != 'y' && answer != 'n')
    {
        cin >> answer;
        if(answer != 'y' && answer != 'n')
        {
            cout << "Non hai inserito una risposta corretta";
        }
    }
    return answer;
}


