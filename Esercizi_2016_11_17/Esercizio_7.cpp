#include <iostream>
#include <string>
using namespace std;
int main()
{
    int userNumber = 0;
    while(userNumber != 5)
    {
        cout << "\nInserire un numero diverso da 5: " << endl;
        cin >> userNumber;
        cout << "Hai inserito il numero " << userNumber << endl;
    }
    cout << "Ehi! Non dovevi inserire il numero 5!" << endl;
    return 0;
}
