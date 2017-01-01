#include<iostream>
#include<stdlib.h>
#include <vector>
#include<ctime>
#include<algorithm>

using namespace std;

#define ROWS 10
#define COLUMNS 10
int EMPTY = -1;
int HIT = -2;
int WATER = -3;

class Ship
{
private:
    string _name;
    int _lenght;
    void SetName();

public:
    int _hit = _lenght;
    Ship(string name, int lenght);
    string GetName();
    int GetLenght() const;
    bool isAlive();
};

Ship::Ship(string name, int lenght)
{
    _name = name;
    _lenght = lenght;
}

string Ship::GetName()
{
    return _name;
}

int Ship::GetLenght() const
{
    return _lenght;
}


void positionShips(vector< vector<int*> >& board, bool isPlayer);
void askPosition(vector< vector<int*> >& board, Ship& ship, int& row, int& col, int& direction);
void insertShip(vector< vector<int*> >& board, Ship ship,int row, int col, int direction);
bool isLegit(vector< vector<int*> >& board, Ship ship, int row, int col, int direction);
void getRandomPosition(vector< vector<int*> >& board, Ship& ship, int& row, int& col, int& direction);
void showBoard(vector< vector<int*> >& board);
void showBoard(vector< vector<int*> >& playerBoard, vector< vector<int*> >& computerBoard);
void playerMove(vector< vector<int*> >& board);
void computerMove(vector< vector<int*> >& board);
bool checkWin(vector< vector<int*> >& board);
bool checkLost(vector< vector<int*> >& board);
char askYesNo(string question);


int main()
{
    srand(time(NULL));
    char again = 'y';

    cout << "\n\t\tBATTAGLIA NAVALE"
         << "\n\nColpisci le navi avversarie, distruggile tutte per vincere."
         << "\nHai a disposizione queste navi:"
         << "\n  - 4 Sommergibili   (Lunghezza: 1)"
         << "\n  - 3 Torpedinieri   (Lunghezza: 2)"
         << "\n  - 2 Incrociatori   (Lunghezza: 3)"
         << "\n  - 1 Portaerei      (lunghezza: 4)" << endl;

    //main loop
    while(again == 'y')
    {

        // crea board
        vector< vector<int*> > playerBoard;
        for(int i = 0; i < ROWS; i++)
        {
            vector<int*> vecRows;
            for(int j = 0; j < ROWS; j++)
            {
                vecRows.push_back(&EMPTY);
            }
            playerBoard.push_back(vecRows);
        }

        vector< vector<int*> > computerBoard;
        for(int i = 0; i < ROWS; i++)
        {
            vector<int*> vecRows;
            for(int j = 0; j < ROWS; j++)
            {
                vecRows.push_back(&EMPTY);
            }
            computerBoard.push_back(vecRows);
        }
        bool isPlayer = true;
        positionShips(playerBoard, isPlayer);
        positionShips(computerBoard, !isPlayer);
        bool playerTurn = true;
        bool won = false;
        bool lost = false;

        while(!won && !lost)
        {
            if(playerTurn)
            {
                playerMove(computerBoard);
                playerTurn = !playerTurn;
            }
            else
            {
                computerMove(playerBoard);
                playerTurn = !playerTurn;

            }
            showBoard(playerBoard, computerBoard);

            won = checkWin(computerBoard);
            lost = checkLost(playerBoard);
        }
        if(won)
        {
            cout << "\n\nCOMPLIMENTI HAI VINTO!" << endl;
        }
        if(lost)
        {
            cout << "\n\nMi dispiace hai perso" << endl;
        }

         again = askYesNo("Vuoi fare un'altra partita?");
    }

    return 0;
}

void positionShips(vector< vector<int*> >& board, bool isPlayer)
{
    Ship sub1("Sommergibile 1", 1);
    Ship sub2("Sommergibile 2", 1);
    Ship sub3("Sommergibile 3", 1);
    Ship sub4("Sommergibile 4", 1);
    Ship torp1("Torpediniere 1", 2);
    Ship torp2("Torpediniere 2", 2);
    Ship torp3("Torpediniere 3", 2);
    Ship incr1("Incrociatore 1", 3);
    Ship incr2("Incrociatore 2", 3);
    Ship carrier("Portaerei", 4);

    Ship fleet[10] = {sub1, sub2, sub3, sub4, torp1, torp2, torp3, incr1, incr2, carrier};

    int row;
    int col;
    int direction;

        if(isPlayer)
        {
            char random = askYesNo("Vuoi mettere le navi Random?");
            if(random == 'y')
            {
                for(int i = 0; i < 10; i++)
                {
                    row = -1;
                    col = -1;
                    direction = 0;
                    getRandomPosition(board, fleet[i], row, col, direction);
                    insertShip(board, fleet[i], row, col, direction);
                }
            }
            else
            {
                for(int i = 0; i < 10; i++)
                {
                    row = -1;
                    col = -1;
                    direction = 0;
                    askPosition(board, fleet[i], row, col, direction);
                    insertShip(board, fleet[i], row, col, direction);
                    showBoard(board);
                }
            }
            if(random == 'y')
            {
                showBoard(board);
            }
        }
        else
        {
            for(int i = 0; i < 10; i++)
            {
                row = -1;
                col = -1;
                direction = 0;
                getRandomPosition(board, fleet[i], row, col, direction);
                insertShip(board, fleet[i], row, col, direction);
            }

        }

}

void askPosition(vector< vector<int*> >& board, Ship& ship, int& row, int& col, int& direction)
{
    int legitMove = false;
    while(legitMove == false)
    {
        cout << "\nDove vuoi posizionare la testa della nave " << ship.GetName() << "?" << endl;
        while(row < 0 || row > 9)
        {
            cout << "\nInserire numero riga (0-9)" << endl;
            cin >> row;
            if(row < 0 || row > 9)
            {
                cout << "\nNon hai inserito una riga corretta";
            }
        }

        while(col < 0 || col > 9)
        {
            cout << "\nInserire numero colonna (0-9)" << endl;
            cin >> col;
            if(row < 0 || row > 9)
            {
                cout << "\nNon hai inserito una colonna corretta";
            }
        }

        while(direction != 1 && direction != 2 && direction != 3 && direction != 4)
        {
            cout << "\nInserire direzione (1 = Destra, 2 = Sinistra, 3 = Su, 4 = Giu')" << endl;
            cin >> direction;
            if(direction != 1 && direction != 2 && direction != 3 && direction != 4)
            {
                cout << "\nNon hai inserito una diirezione corretta";
            }
        }
        legitMove = isLegit(board, ship, row, col, direction);
        if(legitMove == false)
        {
            cout << "\nNon puoi inserire la nave in questa posizione" << endl;
            row = -1;
            col = -1;
            direction = -1;
        }
    }


}

void insertShip(vector< vector<int*> >& board, Ship ship, int row, int col, int direction)
{
    int* shipHit = &ship._hit;
    switch(direction)
    {
    case 1:
        for(int i = 0; i < ship.GetLenght(); i++)
        {
            board[row][col+i] = shipHit;
        }
        break;

    case 2:
        for(int i = 0; i < ship.GetLenght(); i++)
        {
            board[row][col-i] = shipHit;
        }
        break;

    case 3:
        for(int i = 0; i < ship.GetLenght(); i++)
        {
            board[row-i][col] = shipHit;
        }
        break;

    case 4:
        for(int i = 0; i < ship.GetLenght(); i++)
        {
            board[row+i][col] = shipHit;
        }
        break;
    }
}

bool isLegit(vector< vector<int*> >& board, Ship ship, int row, int col, int direction)
{
    bool legitMove;
    switch(direction)
    {
    case 1:
        if( (col + ship.GetLenght()) < 10 )
        {
            for(int i = 0; i < ship.GetLenght(); i++)
            {
                if(board[row][col+i] == &EMPTY)
                {
                    legitMove = true;
                }
                else
                {
                    legitMove = false;
                }
            }
        }
        break;

    case 2:
        if( (col - ship.GetLenght()) >= 0 )
        {
            int* shipHit = &ship._hit;
            for(int i = 0; i < ship.GetLenght(); i++)
            {
                if(board[row][col-i] == &EMPTY)
                {
                    legitMove = true;
                }
                else
                {
                    legitMove = false;
                }
            }
        }
        break;

    case 3:
        if( (row - ship.GetLenght()) >= 0 )
        {
            int* shipHit = &ship._hit;
            for(int i = 0; i < ship.GetLenght(); i++)
            {
                if(board[row-i][col] == &EMPTY)
                {
                    legitMove = true;
                }
                else
                {
                    legitMove = false;
                }
            }
        }
        break;

    case 4:
        if( (row + ship.GetLenght()) < 10 )
        {
            int* shipHit = &ship._hit;
            for(int i = 0; i < ship.GetLenght(); i++)
            {
                if(board[row+i][col] == &EMPTY)
                {
                    legitMove = true;
                }
                else
                {
                    legitMove = false;
                }
            }
        }
        break;
    }
    return legitMove;
}

void getRandomPosition(vector< vector<int*> >& board, Ship& ship, int& row, int& col, int& direction)
{
    int legitMove = false;
    while(legitMove == false)
    {
        row = rand() % 10;
        col = rand() % 10;
        direction = (rand() % 4) + 1;
        legitMove = isLegit(board, ship, row, col, direction);
    }
}

void showBoard(vector< vector<int*> >& board)
{
    cout << "\n\t\t\tGIOCATORE:\n" << endl;
    cout << "\t   0   1   2   3   4   5   6   7   8   9";
    for(int i = 0; i < 10; i++)
    {
        cout << "\n\t -----------------------------------------\n";
        cout << "      " << i << "\t";
        for(int j = 0; j < 10; j++)
        {
            cout << " | ";
            if(board[i][j] == &EMPTY)
            {
                cout << " ";
            }
            else if(board[i][j] == &HIT)
            {
                cout << "X";
            }
            else if(board[i][j] == &WATER)
            {
                cout << "-";
            }
            else
            {
                cout << "O";
            }
        }
        cout << " | ";
    }
    cout << "\n\t -----------------------------------------\n" << endl;
}

void showBoard(vector< vector<int*> >& playerBoard, vector< vector<int*> >& computerBoard)
{
            cout << "\n\t\t\t GIOCATORE:\t\t\t\t\t\t   COMPUTER:\n" << endl;
        cout << "\t   0   1   2   3   4   5   6   7   8   9\t\t     0   1   2   3   4   5   6   7   8   9";
        for(int i = 0; i < 10; i++)
        {
            cout << "\n\t -----------------------------------------\t\t   -----------------------------------------\n";
            cout << "      " << i << "\t";
            for(int j = 0; j < 10; j++)
            {
                cout << " | ";
                if(playerBoard[i][j] == &EMPTY)
                {
                    cout << " ";
                }
                else if(playerBoard[i][j] == &HIT)
                {
                    cout << "X";
                }
                else if(playerBoard[i][j] == &WATER)
                {
                    cout << "-";
                }
                else
                {
                    cout << "O";
                }
            }
            cout << " |\t\t";
            cout << i << " ";
            for(int k = 0; k < 10; k++)
            {
                cout << " | ";
                if(computerBoard[i][k] == &HIT)
                {
                    cout << "X";
                }
                else if(computerBoard[i][k] == &WATER)
                {
                    cout << "-";
                }
                else
                {
                    cout << " ";
                }
            }
            cout << " | ";
        }
        cout << "\n\t -----------------------------------------\t\t   -----------------------------------------\n" << endl;
}

void playerMove(vector< vector<int*> >& board)
{
    int row = -1;
    int col = -1;
    bool newHit = false;
    while(!newHit)
    {
        cout << "\nInserire le coordinate di dove vuoi sparare"<< endl;
        cout << "\nRiga: ";
        while(row < 0 || row > 9)
        {
            cin >> row;
           if(row < 0 || row > 9)
           {
               cout << "\nNon hai inserito un valore corretto" << endl;
           }
        }
        cout << "\nColonna: ";
        while(col < 0 || col > 9)
        {
            cin >> col;
           if(col < 0 || col > 9)
           {
               cout << "\nNon hai inserito un valore corretto" << endl;
           }
        }
        if(board[row][col] == &WATER || board[row][col] == &HIT)
        {
            cout << "\nHai gia' sparato qua" << endl;
        }
        else
        {
            newHit = true;
        }
    }

    int* pBoard = board[row][col];
    if(*pBoard == EMPTY)
    {
        cout << "\nGIOCATORE - Hai sparato in ( " << row << " , " << col << " ) e lo hai MANCATO" << endl;
        board[row][col] = &WATER;
    }
    if(*pBoard > 0)
    {
        *pBoard -= 1;
        cout << "\nGIOCATORE - Hai sparato in ( " << row << " , " << col << " ) e lo hai PRESO" << endl;
        board[row][col] = &HIT;
    }
}

void computerMove(vector< vector<int*> >& board)
{
    bool newHit = false;
    int row;
    int col;
    while(!newHit)
    {
        row = rand() % 10;
        col = rand() % 10;

        if(board[row][col] != &WATER && board[row][col] != &HIT)
        {
            newHit = true;
        }
    }

    int* pBoard = board[row][col];
    if(*pBoard == EMPTY)
    {
        cout << "\nCOMPUTER - Ha sparato in ( " << row << " , " << col << " ) e ti ha MANCATO"  << endl;
        board[row][col] = &WATER;
    }
    if(*pBoard > 0)
    {
        *pBoard -= 1;
        cout << "\nCOMPUTER - Ha sparato in ( " << row << " , " << col << " ) e ti ha PRESO"<< endl;
        board[row][col] = &HIT;
    }
}

bool checkWin(vector< vector<int*> >& board)
{
    int* pBoard;
    for(int i = 0; i < ROWS; i++)
    {
        for(int j = 0; j < COLUMNS; j++)
        {
            pBoard = board[i][j];
            if(*pBoard > 0)
            {
                return false;
            }
        }
    }
    return true;
}

bool checkLost(vector< vector<int*> >& board)
{
    int* pBoard;
    for(int i = 0; i < ROWS; i++)
    {
        for(int j = 0; j < COLUMNS; j++)
        {
            pBoard = board[i][j];
            if(*pBoard > 0)
            {
                return false;
            }
        }
    }
    return true;
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
