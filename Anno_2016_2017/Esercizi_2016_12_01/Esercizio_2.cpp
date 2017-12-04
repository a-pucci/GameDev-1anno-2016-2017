#include<iostream>
#include<stdlib.h>
#include <vector>
#include<ctime>
#include<conio.h>
#include<algorithm>

using namespace std;

void showBoard(vector< vector<int> >& board, int won, bool show);
void putTraps(vector< vector<int> >& board);
void putTreasure(vector< vector<int> >& board);
void putHero(vector< vector<int> >& board);
int heroMove(vector< vector<int> >& board);
bool playerWon(vector< vector<int> >& board);
bool playerLost(vector< vector<int> >& board);
bool easyMode();

const int HERO = 7;
const int TRAP = 5;
const int TREASURE = 1;
const int EMPTY = 0;
const int ROWS = 5;
const int COLS = 5;


 //   char move = getch();

int main()
{
    int won;
    bool again = true;
    bool show;

    srand(time(NULL));

    cout << "\n\tDUNGEON TREASURE" << endl;
    cout << "\n\nSei in un dungeon e devi trovare il tesoro evitando le trappole." << endl;
    cout << "\nPer muovere il tuo eroe premi i tasti W, A, S, D."
         << "\n  W - Su"
         << "\n  A - Sinistra"
         << "\n  S - Giu'"
         << "\n  D - Destra" << endl;

    cout << "\n\n\nPremi un tasto per continuare...";

    char wait = getch();



    while(again)
    {
        won = 0;


        //Crea board

        vector< vector<int> > mainBoard;
        vector< vector<int> > displayBoard;
        for(int i = 0; i < ROWS; i++)
        {
            vector<int> vecRows;
            for(int j = 0; j < ROWS; j++)
            {
                vecRows.push_back(EMPTY);
            }
            mainBoard.push_back(vecRows);
        }

        bool showTrap = easyMode();
        putTraps(mainBoard);
        putTreasure(mainBoard);
        putHero(mainBoard);
        showBoard(mainBoard, won, showTrap);

        while(won == 0)
        {
            won = heroMove(mainBoard);
            showBoard(mainBoard, won, showTrap);
        }
        if(won == 1)
        {
            cout << "\n\n\tHAI VINTO!" << endl;
            cout << "\nSei riuscito ad evitare tutte le trappole e a trovare il tesoro! COMPLIMENTI!" << endl;
        }
        else
        {
            cout << "\n\n\tHAI PERSO!" << endl;
            cout << "\nPurtroppo sei caduto in una trappola, la tua avventura finisce qua..." << endl;
        }


        cout << "\nVuoi giocare ancora?"
        << "\n y - Yes"
        << "\n n - No" << endl;
        char yesNo = ' ';
        while(yesNo != 'y' && yesNo != 'n')
        {
            cin >> yesNo;
            if(yesNo != 'y' && yesNo != 'n')
            {
               cout << "\nNon hai inserito un carattere corretto"
                    << "\n y - Yes"
                    << "\n n - No" << endl;
            }
        }
        if(yesNo == 'y')
        {
            again = true;
        }
        else
        {
            again = false;
            cout << "\n\n\n\tSpero ti sia divertito. Arrivederci!\n\n\n";
        }
    }


    return 0;
}

void showBoard(vector< vector<int> >& board, int won, bool show)
{
    vector< vector<int> > :: iterator itBoard;
    vector<int> :: iterator itRows;

    system("CLS");
    cout << endl;

    for(itBoard = board.begin(); itBoard < board.end(); itBoard++)
    {
        cout << "\t ---------------------"<< endl;
        cout << "\t";
        for(itRows = itBoard -> begin(); itRows < itBoard -> end(); itRows++)
        {
            cout << " | ";
            if(*itRows == HERO)
            {
                cout << 'O';
            }

            if( (*itRows == TRAP && won != 0) || (*itRows == TRAP && show) )
            {
            cout << 'X';
            }
            else if(*itRows == TRAP && won == 0)
            {
            cout << ' ';
            }

            if( *itRows == TREASURE && won != 0 )
            {
            cout << 'M';
            }
            else if(*itRows == TREASURE && won == 0)
            {
                cout << ' ';
            }

            if(*itRows == EMPTY)
            {
                cout << ' ';
            }
        }
        cout << " | " << endl;
    }
    cout << "\t ---------------------\n" << endl;
    if(show)
    {
        cout << "\n\tEASY MODE" << endl;
    }
    else
    {
        cout << "\n\tNORMAL MODE" << endl;
    }

    cout << "\n     Comandi:             Legenda:"
         << "\n       W - Su               O - Il tuo Eroe"
         << "\n       A - Sinistra         X - Trappola"
         << "\n       S - Giu'             M - Tesoro"
         << "\n       D - Destra"
         << endl;

    return;

}

void putTraps(vector< vector<int> >& board)
{
    int i;
    int j;
    int count = 0;

    while(count < 3)
    {
        i = rand() % 5;
        j = rand() % 5;

        if(board[i][j] == EMPTY)
        {
            board[i][j] = TRAP;
            count++;
        }
    }

    return;
}

void putTreasure(vector< vector<int> >& board)
{
    int i;
    int j;
    bool empty = false;
    while(!empty)
    {
        i = rand() % 5;
        j = rand() % 5;

        if(board[i][j] == EMPTY)
        {
            board[i][j] = TREASURE;
            empty = true;
        }
        else
        {
            empty = false;
        }
    }
}

void putHero(vector< vector<int> >& board)
{
    int i;
    int j;
    bool empty = false;
    while(!empty)
    {
        i = rand() % 5;
        j = rand() % 5;

        if(board[i][j] == EMPTY)
        {
            board[i][j] = HERO;
            empty = true;
        }
        else
        {
            empty = false;
        }
    }
}

int heroMove(vector< vector<int> >& board)
{
    int row = 0;
    int col = 0;

    for(int i = 0; i < 5; i++)
    {
        for(int j = 0; j < 5; j++)
        {
            if(board[i][j] == HERO)
            {
                row = i;
                col = j;
                break;
            }
        }
    }


    char move = ' ';

    while(move != 'w' && move != 'a' && move != 's' && move != 'd')
    {
        move = getch();
        if(move != 'w' && move != 'a' && move != 's' && move != 'd')
        {
            cout << "\n\nMuoviti usando i tasti W A S D" << endl;
        }
    }

    switch(move)
    {
    case 'w':
        if(row != 0)
        {
            if(board[row-1][col] == TRAP)
            {
                return -1;
            }
            else if(board[row-1][col] == TREASURE)
            {
                return 1;
            }
            else
            {
                board[row][col] = EMPTY;
                board[row-1][col] = HERO;
            }
        }
        break;

    case 'a':
        if(col != 0)
        {
            if(board[row][col-1] == TRAP)
            {
                return -1;
            }
            else if(board[row][col-1] == TREASURE)
            {
                return 1;
            }
            else
            {
                board[row][col] = EMPTY;
                board[row][col-1] = HERO;
            }
        }
        break;

    case 's':
        if(row != 4)
        {
            if(board[row+1][col] == TRAP)
            {
                return -1;
            }
            else if(board[row+1][col] == TREASURE)
            {
                return 1;
            }
            else
            {
                board[row][col] = EMPTY;
                board[row+1][col] = HERO;
            }
        }
        break;

    case 'd':
        if(col != 4)
        {
            if(board[row][col+1] == TRAP)
            {
                return -1;
            }
            else if(board[row][col+1] == TREASURE)
            {
                return 1;
            }
            else
            {
                board[row][col] = EMPTY;
                board[row][col+1] = HERO;
            }
        }
        break;
    }
    return 0;
}

bool easyMode()
{
    system("CLS");

    cout << "\n\nVuoi giocare in modalita' EASY e poter vedere dove sono le trappole?"
    << "\n\n y - Yes"
    << "\n n - No" << endl;
    char easy = ' ';
    while(easy != 'y' && easy != 'n')
    {
        cin >> easy;
        if(easy != 'y' && easy != 'n')
        {
            cout << "\nNon hai inserito un carattere corretto"
                 << "\n y - Yes"
                 << "\n n - No" << endl;
        }
    }
    if(easy == 'y')
    {
        return true;
    }
    else
    {
        return false;
    }
}
