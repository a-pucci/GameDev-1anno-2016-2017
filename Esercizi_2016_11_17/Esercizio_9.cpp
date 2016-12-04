#include <iostream>
#include <string>
using namespace std;
int main()
{
    int userNumber;
    int i = 0;
    while(userNumber != i-1)
    {
        cout << "\nInserire un numero diverso da " << i << ":"<< endl;
        cin >> userNumber;
        cout << "Hai inserito il numero " << userNumber << endl;
        i++;
    }
    cout << "Ehi! Non dovevi inserire il numero " << i-1 << "!" << endl;
    return 0;
}
