#include<iostream>
#include<stdlib.h>
#include <vector>
#include<ctime>
#include<conio.h>
#include<algorithm>

using namespace std;

void showBoard(vector< vector<int> >& board, int won, bool hard);
void putBlocks(vector< vector<int> >& board);
void putTreasure(vector< vector<int> >& board);
void putHero(vector< vector<int> >& board);
void putEnemy(vector< vector<int> >& board);
int heroMove(vector< vector<int> >& board);
int enemyMove(vector< vector<int> >& board, bool hard);
int enemyRandomMove(vector< vector<int> >& board);
int enemyCloserMove(vector< vector<int> >& board);
bool playerWon(vector< vector<int> >& board);
bool playerLost(vector< vector<int> >& board);
bool hardMode();

const int HERO = 7;
const int ENEMY = 5;
const int TREASURE = 1;
const int EMPTY = 0;
const int BLOCK = 6;
const int ROWS = 5;
const int COLS = 5;


 //   char move = getch();

int main()
{
    int heroWon;
    int enemyWon;
    bool again = true;

    srand(time(NULL));

    cout << "\n\tDUNGEON TREASURE II" << endl;
    cout << "\n\nSei in un dungeon e devi trovare il tesoro evitando i nemici." << endl;
    cout << "\nPer muovere il tuo eroe premi i tasti W, A, S, D."
         << "\n  W - Su"
         << "\n  A - Sinistra"
         << "\n  S - Giu'"
         << "\n  D - Destra" << endl;

    cout << "\n\n\nPremi un tasto per continuare...";

    char wait = getch();

    while(again)
    {
        heroWon = 0;
        enemyWon = 0;

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

        bool hard = hardMode();
        putEnemy(mainBoard);
        putBlocks(mainBoard);
        putTreasure(mainBoard);
        putHero(mainBoard);
        showBoard(mainBoard, heroWon, hard);

        while(heroWon == 0 && enemyWon == 0)
        {
            heroWon = heroMove(mainBoard);
            if(heroWon == 0)
            {
                enemyWon = enemyMove(mainBoard, hard);
            }

            showBoard(mainBoard, heroWon, hard);
        }
        if(heroWon == 1 && enemyWon == 0)
        {
            cout << "\n\n\tHAI VINTO!" << endl;
            cout << "\nSei riuscito ad evitare tutti i nemici e a trovare il tesoro! COMPLIMENTI!" << endl;
        }
        else
        {
            cout << "\n\n\tHAI PERSO!" << endl;
            cout << "\nPurtroppo sei stato preso dai nemici, la tua avventura finisce qua..." << endl;
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

void showBoard(vector< vector<int> >& board, int won, bool hard)
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

            if( *itRows == BLOCK )
            {
            cout << 'R';
            }


            if( *itRows == TREASURE && won != 0 )
            {
            cout << 'M';
            }
            else if(*itRows == TREASURE && won == 0)
            {
                cout << ' ';
            }

            if(*itRows == ENEMY)
            {
                cout << 'E';
            }

            if(*itRows == EMPTY)
            {
                cout << ' ';
            }
        }
        cout << " | " << endl;
    }
    cout << "\t ---------------------\n" << endl;
    if(!hard)
    {
        cout << "\n\tNORMAL MODE" << endl;
    }
    else
    {
        cout << "\n\tHARD MODE" << endl;
    }

    cout << "\n     Comandi:             Legenda:"
         << "\n       W - Su               O - Il tuo Eroe"
         << "\n       A - Sinistra         E - Nemico"
         << "\n       S - Giu'             R - Roccia"
         << "\n       D - Destra           M - Tesoro"
         << endl;

    return;

}

void putBlocks(vector< vector<int> >& board)
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
            board[i][j] = BLOCK;
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

void putEnemy(vector< vector<int> >& board)
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
            board[i][j] = ENEMY;
            count++;
        }
    }

    return;
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
        if(row != 0 && board[row-1][col] != BLOCK)
        {
            if(board[row-1][col] == ENEMY)
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
        if(col != 0 && board[row][col-1] != BLOCK)
        {
            if(board[row][col-1] == ENEMY)
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
        if(row != 4 && board[row+1][col] != BLOCK)
        {
            if(board[row+1][col] == ENEMY)
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
        if(col != 4 && board[row][col+1] != BLOCK)
        {
            if(board[row][col+1] == ENEMY)
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

int enemyMove(vector< vector<int> >& board, bool hard)
{
    int status = 0;
    if(!hard)
    {
        status = enemyRandomMove(board);
    }
    else
    {
        status = enemyCloserMove(board);
    }
    return status;
}

int enemyRandomMove(vector< vector<int> >& board)
{
    int rowA[3];
    int colA[3];
    int row;
    int col;
    int k = 0;
    bool eaten = false;

    for(int i = 0; i < 5; i++)
    {
        for(int j = 0; j < 5; j++)
        {
            if(board[i][j] == ENEMY)
            {
                rowA[k] = i;
                colA[k] = j;
                k++;
            }
        }
    }

    for(int i = 0; i < 3; i++)
    {
        row = rowA[i];
        col = colA[i];

        int move = rand() % 4;

        switch(move)
        {
        case 0:
            if(row != 0 && board[row-1][col] != BLOCK && board[row-1][col] != ENEMY && board[row-1][col] != TREASURE)
            {
                if(board[row-1][col] == HERO)
                {
                    eaten = true;
                }
                board[row][col] = EMPTY;
                board[row-1][col] = ENEMY;
                if(eaten)
                {
                return -1;
                }
            }

            break;

        case 1:
            if(col != 0 && board[row][col-1] != BLOCK && board[row][col-1] != ENEMY && board[row][col-1] != TREASURE)
            {
                if(board[row][col-1] == HERO)
                {
                    eaten = true;
                }
                board[row][col] = EMPTY;
                board[row][col-1] = ENEMY;
                if(eaten)
                {
                    return -1;
                }
            }
            break;

        case 2:
            if(row != 4 && board[row+1][col] != BLOCK && board[row+1][col] != ENEMY && board[row+1][col] != TREASURE)
            {
                if(board[row+1][col] == HERO)
                {
                    eaten = true;
                }
                board[row][col] = EMPTY;
                board[row+1][col] = ENEMY;
                if(eaten)
                {
                    return -1;
                }
            }

            break;

        case 3:
            if(col != 4 && board[row][col+1] != BLOCK && board[row][col+1] != ENEMY && board[row][col+1] != TREASURE)
            {
                if(board[row][col+1] == HERO)
                {
                    eaten = true;
                }
                board[row][col] = EMPTY;
                board[row][col+1] = ENEMY;
                if(eaten)
                {
                    return -1;
                }
            }

            break;

        }
    }
    return 0;
}

int enemyCloserMove( vector< vector<int> >& board)
{
    int rowH = 0;
    int colH = 0;
    int rowE = 0;
    int colE = 0;
    int row[3];
    int col[3];
    int k = 0;
    bool eaten = false;

    for(int i = 0; i < 5; i++)
    {
        for(int j = 0; j < 5; j++)
        {
            if(board[i][j] == HERO)
            {
                rowH = i;
                colH = j;
                break;
            }
        }
    }

    for(int i = 0; i < 5; i++)
    {
        for(int j = 0; j < 5; j++)
        {
            if(board[i][j]== ENEMY)
            {
                rowE = i;
                colE = j;
                row[k] = i;
                col[k] = j;
                k++;
            }
        }
    }

    for(int i = 0; i < 3; i++)
    {
        rowE = row[i];
        colE = col[i];
        eaten = false;

        if(rowE < rowH)
        {
            if(board[rowE+1][colE] == HERO)
            {
                eaten = true;
            }
        if(board[rowE+1][colE] != BLOCK && board[rowE+1][colE] != ENEMY && board[rowE+1][colE] != TREASURE)
            {
                board[rowE][colE] = EMPTY;
                board[rowE+1][colE] = ENEMY;
                if(eaten)
                {
                    return -1;
                }
            }
        }
        else if(colE < colH)
        {
            if(board[rowE][colE+1] == HERO)
            {
                eaten = true;
            }
            if(board[rowE][colE+1] != BLOCK && board[rowE][colE+1] != ENEMY && board[rowE][colE+1] != TREASURE)
            {
                board[rowE][colE] = EMPTY;
                board[rowE][colE+1] = ENEMY;
                if(eaten)
                {
                    return -1;
                }
            }
        }
        else if(rowE > rowH)
        {
            if(board[rowE-1][colE] == HERO)
            {
                eaten = true;
            }
            if(board[rowE-1][colE] != BLOCK && board[rowE-1][colE] != ENEMY && board[rowE-1][colE] != TREASURE)
            {
                board[rowE][colE] = EMPTY;
                board[rowE-1][colE] = ENEMY;
                if(eaten)
                {
                    return -1;
                }
            }
        }
        else if(colE > colH)
        {
            if(board[rowE][colE-1] == HERO)
            {
                eaten = true;
            }
            if(board[rowE][colE-1] != BLOCK && board[rowE][colE-1] != ENEMY && board[rowE][colE-1] != TREASURE)
            {
                board[rowE][colE] = EMPTY;
                board[rowE][colE-1] = ENEMY;
                if(eaten)
                {
                    return -1;
                }
            }
        }
    }


    return 0;
}

bool hardMode()
{
    system("CLS");

    cout << "\n\nVuoi giocare in modalita' HARD? I nemici si muoveranno verso di te invece che in modo casuale"
    << "\n\n y - Yes"
    << "\n n - No" << endl;
    char hard = ' ';
    while(hard != 'y' && hard != 'n')
    {
        cin >> hard;
        if(hard != 'y' && hard != 'n')
        {
            cout << "\nNon hai inserito un carattere corretto"
                 << "\n y - Yes"
                 << "\n n - No" << endl;
        }
    }
    if(hard == 'y')
    {
        return true;
    }
    else
    {
        return false;
    }
}
