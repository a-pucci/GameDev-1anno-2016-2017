#include <iostream>
#include <string>
using namespace std;
int main()
{
    unsigned int difficulty;
    cout << "\nScegli il livello di difficoltà: "
    <<"\n   1 - VERY EASY"
    <<"\n   2 - EASY"
    <<"\n   3 - MEDIUM"
    <<"\n   4 - HARD"
    <<"\n   5 - VERY HARD" << endl;
    cin >> difficulty;
    if(difficulty == 1)
    {
        cout << "Hai scelto VERY EASY!" << endl;
    }
    else if(difficulty == 2)
    {
        cout << "Hai scelto EASY!" << endl;
    }
    else if(difficulty == 3)
    {
        cout << "Hai scelto MEDIUM!" << endl;
    }
    else if(difficulty == 4)
    {
        cout << "Hai scelto HARD!" << endl;
    }
    else if(difficulty == 5)
    {
        cout << "Hai scelto VERY HARD!" << endl;
    }
    else
    {
        cout << "Non hai scelto una difficoltà corretta" << endl;
    }
    return 0;
}
