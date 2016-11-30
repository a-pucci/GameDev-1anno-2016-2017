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
    switch(difficulty)
    {
        case(1):
        cout << "Hai Scelto VERY EASY!" << endl;
        break;
        
        case(2):
        cout << "Hai scelto EASY!" << endl;
        break;
        
        case(3):
        cout << "Hai scelto MEDIUM!" << endl;
        break;
        
        case(4):
        cout << "Hai scelto HARD!" << endl;
        break;
        
        case(5):
        cout << "Hai scelto VERY HARD!" << endl;
        break;
        
        default:
        cout << "Non hai inserito una difficoltà corretta" << endl;
    }
    return 0;
}
