#include <iostream>
#include <string>
using namespace std;

int main()
{
    int playerScore[10];

    cout << "\t\tLEADERBOARD\n";

    for(int i = 0; i < 10; i++)
    {
        cout << "\nInserire punteggio ottenuto dal Giocatore " << i+1 << ": ";
        cin >> playerScore[i];
    }

    cout << "\n\n\tLISTA PUNTEGGI: " << endl;
    for(int i = 0; i < 10; i++)
    {
        cout << "\n Giocatore " << i+1 << " - " << playerScore[i] << endl;
    }

    cout << "\n\n\tLISTA PUNTEGGI INVERSI: " << endl;
    for(int i = 9; i >= 0; i--)
    {
        cout << "\n Giocatore " << i+1 << " - " << playerScore[i];
    }
    int sum;
    cout << "\n\n\tMEDIA PUNTEGGI: " << endl;
    for(int i = 0; i < 10; i++)
    {
        sum += playerScore[i];
    }
    int media = sum/10;

    cout << "\nLa media dei punteggi e': " << media << endl;



}
