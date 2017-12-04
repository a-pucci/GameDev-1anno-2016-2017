#include <iostream>
#include <string>
using namespace std;
int main()
{
    int userNumber = 0;
    int i;
    for(i = 0; userNumber != 5 && i<10; i++)
    {
        cout << "\nInserire un numero diverso da 5: " << endl;
        cin >> userNumber;
        cout << "Hai inserito il numero " << userNumber << endl;
    }
    if(userNumber == 5)
    {
        cout << "Ehi! Non dovevi inserire il numero 5!" << endl;
    }
    else
    {
       cout << "Wow, sei più paziente di quanto lo sono io... Complimenti! Hai Vinto!" << endl;
    }
    return 0;
}
