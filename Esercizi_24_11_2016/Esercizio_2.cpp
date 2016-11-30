#include <iostream>
#include <cstdlib>
#include <ctime>
using namespace std;

void printBoard(char field[][10]);
bool isHot(int shipR, int shipC, int playerR, int playerC);
bool isWarm(int shipR, int shipC, int playerR, int playerC);
int getRow();
int getCol();

int main()
{
    bool again = true;
    while(again)
    {
        system("CLS");
        cout << "\t\tBATTAGLIA NAVALE\n"
         << "\nIn questo mare si nasconde una piccola nave, riuscirai a colpirla?\n"
         << "\n  * = Acqua, la nave e' lontana"
         << "\n  f = Fuochino, la nave e' vicina"
         << "\n  F = Fuoco, l'hai mancata per pochissimo" << endl;

        char field[10][10];
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                field[i][j] = ' ';
            }
        }

        srand(static_cast<unsigned int>(time(0)));
        int shipRow = rand() % 10;
        int shipCol = rand() % 10;

        //la riga sotto serve per rendere visibile la nave e testare meglio il programma
        //field[shipRow][shipCol] = 'X';

        cout << "\nProva a colpire la nave:\n\n";
        printBoard(field);

        bool won = false;
        int moves = 1;
        while(!won)
        {
            won = false;
            int playerRow = getRow();
            int playerCol = getCol();
            system("CLS");

            if(playerRow-1 == shipRow && playerCol-1 == shipCol)
            {
                cout << "\n\nCOMPLIMENTI! Hai colpito la nave in " << moves << " mosse\n" << endl;
                field[shipRow][shipCol] = 'X';
                printBoard(field);
                won = true;
            }
            else if(field[playerRow-1][playerCol-1] == '*' ||
                    field[playerRow-1][playerCol-1] == 'f' ||
                    field[playerRow-1][playerCol-1] == 'F')
            {
                cout << "\nHai gia' inserito queste coordinate, prova qualcosa di diverso\n" << endl;
                printBoard(field);
            }
            else
            {
                if(isHot(shipRow, shipCol, playerRow-1, playerCol-1))
                {

                    cout << "\nFuoco! Ci sei andato vicino!\n" << endl;
                    field[playerRow-1][playerCol-1] = 'F';
                }
                else if(isWarm(shipRow, shipCol, playerRow-1, playerCol-1))
                {
                    cout << "\nFuochino, la nave e' vicina\n" << endl;
                    field[playerRow-1][playerCol-1] = 'f';
                }
                else
                {
                    cout << "\nAcqua, la nave non e' da queste parti\n" << endl;
                    field[playerRow-1][playerCol-1] = '*';
                }

                printBoard(field);
                moves++;

            }


        }

        char choice = ' ';
        while(choice != 'y' && choice != 'n')
        {
            cout << "\n\nVuoi giocare ancora?" << "\n y - Yes" << "\n n - No" << endl;
            cin >> choice;
            if(choice != 'y' && choice != 'n')
            {
                cout << "\nNon hai inserito un carattere corretto" << endl;
            }
        }

        if(choice == 'y')
        {
            again = true;
        }
        else if(choice == 'n')
        {
            again = false;
        }
    }

    return 0;
}

void printBoard(char board[][10])
{
    cout << "\t  1   2   3   4   5   6   7   8   9  10" << endl;
    cout << "\t-----------------------------------------" << endl;
    for(int i = 0; i< 10; i++)
    {
        cout << "     " << i+1 << "\t| ";
        for (int j =0; j< 10; j++)
        {
            cout << board[i][j] << " | ";
        }
        cout << endl;
        cout << "\t-----------------------------------------" << endl;
    }
}

bool isHot(int shipR, int shipC, int playerR, int playerC)
{
    if( (playerR <= shipR+1 && playerR >= shipR-1) &&
        (playerC <= shipC+1 && playerC >= shipC-1))
    {
        return true;
    }
    else
    {
        return false;
    }
}

bool isWarm(int shipR, int shipC, int playerR, int playerC)
{
    if( (playerR <= shipR+2 && playerR >= shipR-2) &&
        (playerC <= shipC+2 && playerC >= shipC-2))
    {
        return true;
    }
    else
    {
        return false;
    }
}

int getRow()
{
    int playerRow = 15;
    while(playerRow < 0 || playerRow > 10)
    {
        cout << "\nInserire numero riga: ";
        cin >> playerRow;
        if(playerRow < 0 || playerRow > 10)
        {
            cout << "\nPer favore inserisci una riga tra 1 e 10";
        }
    }
}

int getCol()
{
    int playerCol = 15;
    while(playerCol < 0 || playerCol > 10)
    {
        cout << "\nInserire numero colonna: ";
        cin >> playerCol;
        if(playerCol < 0 || playerCol > 10)
        {
            cout << "Per favore inserisci una colonna tra 1 e 10";
        }
    }
    return playerCol;
}



