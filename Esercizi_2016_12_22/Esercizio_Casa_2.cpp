#include<iostream>
#include<stdlib.h>
#include <vector>
#include<ctime>
#include<algorithm>

using namespace std;

#define ROWS 8
#define COLUMNS 8

class Pawn
{
private:
    int _number;
    char _color;
    bool _draught = false;

public:
    Pawn();
    Pawn(int number);
    Pawn(int number, char color);
    int GetNumber() const;
    char GetColor() const;
    void SetDraught(bool draught);
    bool IsDraught() const;
};

Pawn::Pawn(){}

Pawn::Pawn(int number)
{
    _number = number;
}

Pawn::Pawn(int number, char color)
{
    _number = number;
    _color = color;
}

int Pawn::GetNumber() const
{
    return _number;
}

char Pawn::GetColor() const
{
    return _color;
}

void Pawn::SetDraught(bool draught)
{
    _draught = draught;
}

bool Pawn::IsDraught() const
{
    return _draught;
}

void initializeBoard(vector< vector<Pawn> >& board);
void showBoard(vector< vector<Pawn> >& board);
bool playerMove(vector< vector<Pawn> >& board);
bool computerMove(vector< vector<Pawn> >& board);
void askMove(vector< vector<Pawn> >& board, int& pawn, int& dir);
void getRandomMove(vector< vector<Pawn> >& board, int& pawn, int& dir);
bool isLegit(vector< vector<Pawn> >& board, int row, int col, int direction);
bool findPawn(vector< vector<Pawn> >& board, int pawn, char color, int& row, int& col);
void movePawn(vector< vector<Pawn> >& board, int pawn, char color, int direction);


bool checkWin(vector< vector<Pawn> >& board);
bool checkLost(vector< vector<Pawn> >& board);
char askYesNo(string question);


int main()
{
    srand(time(NULL));
    char again = 'y';

    cout << "\n\t\t\t\tDAMA"
         << "\n\nRegole:\n"
         << "\n  1 - La damiera e' formata da 64 caselle di colore bianco e ner alternate"
         << "\n  2 - Ciascun giocatore ha 12 pedine posizionate sulle prime 3 righe di caselle nere del proprio lato"
         << "\n  3 - La pedina si muove solo in diagonale di una casella e soltanto in avanti,\n      se raggiunge una casella dell'ultima riga viene promossa a Dama"
         << "\n  4 - Ogni pedina deve mangiare quelle avversarie che si trovano in una casella diagonale"
         << "\n  5 - La Dama si muove di una casella alla volta in diagonale ma puo' muoversi liberamente avanti o indietro"
         << "\n  6 - Vince chi mangia tutte le pedine avversarie"
         << endl;

    while(again == 'y')
    {
        Pawn emptyPawn(0);
        vector< vector<Pawn> > board;
        for(int i = 0; i < ROWS; i++)
        {
            vector<Pawn> vecRows;
            for(int j = 0; j < ROWS; j++)
            {

                vecRows.push_back(emptyPawn);
            }
            board.push_back(vecRows);
        }

        initializeBoard(board);
        showBoard(board);

        bool win = false;
        bool lost = false;
        bool playerTurn = true;
        bool playerMoves = true;
        bool computerMoves = true;

        while(!win && !lost && playerMoves && computerMoves)
        {
            if(playerTurn)
            {
                playerMoves = playerMove(board);
                showBoard(board);
                playerTurn = !playerTurn;
            }
            else
            {
                computerMoves = computerMove(board);
                showBoard(board);
                playerTurn = !playerTurn;
            }

            win = checkWin(board);
            lost = checkLost(board);
        }
        if(win)
        {
            cout << "\nComplimenti hai vinto!!!" << endl;
        }
        else if(lost || !playerMoves)
        {
            cout << "\nMi dispiace hai perso" << endl;
        }
        else if(!computerMoves)
        {
            cout << "\nIl Computer non ha piu' mosse disponibili, hai vinto!" << endl;
        }

        again = askYesNo("Vuoi fare un'altra partita?");
    }

    return 0;
}

void initializeBoard(vector< vector<Pawn> >& board)
{
    Pawn black_1(1,'b');
    Pawn black_2(2,'b');
    Pawn black_3(3,'b');
    Pawn black_4(4,'b');
    Pawn black_5(5,'b');
    Pawn black_6(6,'b');
    Pawn black_7(7,'b');
    Pawn black_8(8,'b');
    Pawn black_9(9,'b');
    Pawn black_10(10,'b');
    Pawn black_11(11,'b');
    Pawn black_12(12,'b');
    Pawn white_1(1, 'w');
    Pawn white_2(2, 'w');
    Pawn white_3(3, 'w');
    Pawn white_4(4, 'w');
    Pawn white_5(5, 'w');
    Pawn white_6(6, 'w');
    Pawn white_7(7, 'w');
    Pawn white_8(8, 'w');
    Pawn white_9(9, 'w');
    Pawn white_10(10, 'w');
    Pawn white_11(11, 'w');
    Pawn white_12(12, 'w');

    Pawn black[12] = {black_1, black_2, black_3, black_4, black_5, black_6, black_7, black_8, black_9, black_10, black_11, black_12};
    Pawn white[12] = {white_1, white_2, white_3, white_4, white_5, white_6, white_7, white_8, white_9, white_10, white_11, white_12};

    int k = 0;
    for(int i = 0; i < 3; i++)
    {
        for(int j = 0; j < 8; j++)
        {
            if( ( ( i+j) % 2) == 0)
            {
                board[i][j] = black[k];
                k++;
            }
        }
    }

    k = 0;
    for(int i = 5; i < 8; i++)
    {
        for(int j = 0; j < 8; j++)
        {
            if( ( (i+j) % 2) == 0 )
            {
                board[i][j] = white[k];
                k++;
            }
        }
    }
}

void showBoard(vector< vector<Pawn> >& board)
{
    cout << "\n\t   0    1    2    3    4    5    6    7";
    for(int i = 0; i < ROWS; i++)
    {
        cout << "\n\t------------------------------------------\n";
        cout << "      " << i << "\t";
        for(int j = 0; j < COLUMNS; j++)
        {
            cout << "|";
            if(board[i][j].GetNumber() == 0)
            {
                cout << "    ";
            }
            else if(board[i][j].GetNumber() > 0)
            {
                if(board[i][j].IsDraught())
                {
                    cout << "D";
                }
                else
                {
                    cout << " ";
                }
                cout << board[i][j].GetNumber();
                cout << board[i][j].GetColor();
                if(board[i][j].GetNumber() < 10)
                {
                    cout << " ";
                }
            }
        }
        cout << " | ";
    }
    cout << "\n\t------------------------------------------\n";
}

bool playerMove(vector< vector<Pawn> >& board)
{
    int pawn = -1;
    int direction = -1;
    char color = 'w';
    askMove(board, pawn, direction);
    if(pawn == -1)
    {
        cout << "\n----- 1 -----" << endl;
        return false;
    }
    movePawn(board, pawn, color, direction);
    return true;

}

bool computerMove(vector< vector<Pawn> >& board)
{
    int pawn = -1;
    int direction = -1;
    char color = 'b';
    getRandomMove(board, pawn, direction);
    if(pawn == -1)
    {
        return false;
    }
    movePawn(board, pawn, color, direction);
    cout << "\nCOMPUTER: Pedina " << pawn << " - ";
    switch(direction)
        {
        case 1:
            cout << "Avanti Destra" << endl;
            break;

        case 2:
            cout << "Avanti Sinistra" << endl;
            break;

        case 3:
            cout << "Indietro Destra" << endl;
            break;

        case 4:
            cout << "Indietro Sinistra" << endl;
            break;
        }
    return true;
}

void askMove(vector< vector<Pawn> >& board, int& pawn, int& dir)
{
    int counter = 0;
    bool legitMove = false;
    while(!legitMove && counter < 5)
    {
        while(pawn < 0 || pawn > 12)
        {
            cout << "\nInserisci il numero del pedone che vuoi muovere" << endl;
            cin >> pawn;
            if(pawn < 0 || pawn > 12)
            {
                cout << "\nNon hai inserito un valore corretto" << endl;
            }
        }

        int pawnRow;
        int pawnCol;
        bool found = findPawn(board, pawn, 'w', pawnRow, pawnCol);

        if(found)
        {
            if(board[pawnRow][pawnCol].IsDraught())
            {
                while(dir != 1 && dir != 2 && dir != 3 && dir != 4)
                {
                    cout << "\nScegliere direzione:"
                         << "\n  1 - Avanti Destra"
                         << "\n  2 - Avanti Sinistra"
                         << "\n  3 - Indietro Destra"
                         << "\n  4 - Indietro Sinistra" << endl;

                    cin >> dir;
                    if(dir != 1 && dir != 2 && dir != 3 && dir != 4)
                    {
                        cout << "\nNon hai inserito un numero corretto";
                    }
                }
            }
            else
            {
                while(dir != 1 && dir != 2)
                {
                    cout << "\nScegliere direzione:"
                         << "\n  1 - Avanti Destra"
                         << "\n  2 - Avanti Sinistra" << endl;

                    cin >> dir;
                    if(dir != 1 && dir != 2)
                    {
                        cout << "\nNon hai inserito un numero corretto";
                    }
                }
            }

            legitMove = isLegit(board, pawnRow, pawnCol, dir);
            if(legitMove == false)
            {
                cout << "\nNon puoi muovere la pedina " << pawn << " in questa posizione" << endl;
                pawn = -1;
                dir = -1;
                counter++;
            }
        }
        else
        {
            cout << "\nLa pedina non esiste" << endl;
            pawn = -1;
            counter++;
        }
        if(counter == 5)
        {
            char surrender;
            surrender = askYesNo("Sembra che non hai piu' mosse diponibili, vuoi arrenderti?");
            if(surrender == 'y')
            {
                pawn = -1;
                dir = -1;
                return;
            }
            else
            {
                counter = 2;
            }
        }
    }
}

void getRandomMove(vector< vector<Pawn> >& board, int& pawn, int& dir)
{
    bool legitMove = false;
    int counter = 0;
    while(!legitMove && counter < 100)
    {
        while(pawn < 0 || pawn > 12)
        {
            pawn = (rand() % 12) + 1;
        }

        int pawnRow;
        int pawnCol;
        bool found = findPawn(board, pawn, 'b', pawnRow, pawnCol);

        if(found)
        {
            if(board[pawnRow][pawnCol].IsDraught())
            {
                while(dir != 1 && dir != 2 && dir != 3 && dir != 4)
                {
                    dir = (rand() % 4) + 1;
                }
            }
            else
            {
                while(dir != 3 && dir != 4)
                {
                    dir = (rand() % 2) + 3;
                }
            }


            legitMove = isLegit(board, pawnRow, pawnCol, dir);
            if(legitMove == false)
            {
                pawn = -1;
                dir = -1;
                counter ++;
            }
        }
        else
        {
            pawn = -1;
            counter++;
        }
    }
}

bool isLegit(vector< vector<Pawn> >& board, int row, int col, int direction)
{
    if(board[row][col].GetColor() == 'w')
    {
        switch(direction)
        {
        case 1:
            if(row == 0 || col == 7)
            {
                return false;
            }
            else if(board[row-1][col+1].GetNumber() == 0)
            {
                return true;
            }
            else if(board[row-1][col+1].GetColor() == 'b')
            {
                if(row == 1 || col == 6)
                {
                    return false;
                }
                else if(board[row-2][col+2].GetNumber() == 0)
                {
                    if(board[row][col].IsDraught() == false && board[row-1][col+1].IsDraught() == true)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            break;

        case 2:
            if(row == 0 || col == 0)
            {
                return false;
            }
            else if(board[row-1][col-1].GetNumber() == 0)
            {
                return true;
            }
            else if(board[row-1][col-1].GetColor() == 'b')
            {
                if(row == 1 || col == 1)
                {
                    return false;
                }
                else if(board[row-2][col-2].GetNumber() == 0)
                {
                    if(board[row][col].IsDraught() == false && board[row-1][col-1].IsDraught() == true)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            break;

        case 3:
            if(row == 7 || col == 7)
            {
                return false;
            }
            else if(board[row+1][col+1].GetNumber() == 0)
            {
                return true;
            }
            else if(board[row+1][col+1].GetColor() == 'b')
            {
                if(row == 6 || col == 6)
                {
                    return false;
                }
                else if(board[row+2][col+2].GetNumber() == 0)
                {
                    if(board[row][col].IsDraught() == false && board[row+1][col+1].IsDraught() == true)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            break;

        case 4:
            if(row == 7 || col == 0)
            {
                return false;
            }
            else if(board[row+1][col-1].GetNumber() == 0)
            {
                return true;
            }
            else if(board[row+1][col-1].GetColor() == 'b')
            {
                if(row == 6 || col == 1)
                {
                    return false;
                }
                else if(board[row+2][col-2].GetNumber() == 0)
                {
                    if(board[row][col].IsDraught() == false && board[row+1][col-1].IsDraught() == true)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            break;
        }
    }
    else
    {
        switch(direction)
        {
        case 1:
            if(row == 0 || col == 7)
            {
                return false;
            }
            else if(board[row-1][col+1].GetNumber() == 0)
            {
                return true;
            }
            else if(board[row-1][col+1].GetColor() == 'w')
            {
                if(row == 1 || col == 6)
                {
                    return false;
                }
                else if(board[row-2][col+2].GetNumber() == 0)
                {
                    if(board[row][col].IsDraught() == false && board[row-1][col+1].IsDraught() == true)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            break;

        case 2:
            if(row == 0 || col == 0)
            {
                return false;
            }
            else if(board[row-1][col-1].GetNumber() == 0)
            {
                return true;
            }
            else if(board[row-1][col-1].GetColor() == 'w')
            {
                if(row == 1 || col == 1)
                {
                    return false;
                }
                else if(board[row-2][col-2].GetNumber() == 0)
                {
                    if(board[row][col].IsDraught() == false && board[row-1][col-1].IsDraught() == true)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            break;

        case 3:
            if(row == 7 || col == 7)
            {
                return false;
            }
            else if(board[row+1][col+1].GetNumber() == 0)
            {
                return true;
            }
            else if(board[row+1][col+1].GetColor() == 'w')
            {
                if(row == 6 || col == 6)
                {
                    return false;
                }
                else if(board[row+2][col+2].GetNumber() == 0)
                {
                    if(board[row][col].IsDraught() == false && board[row+1][col+1].IsDraught() == true)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            break;

        case 4:
            if(row == 7 || col == 0)
            {
                return false;
            }
            else if(board[row+1][col-1].GetNumber() == 0)
            {
                return true;
            }
            else if(board[row+1][col-1].GetColor() == 'w')
            {
                if(row == 1 || col == 6)
                {
                    return false;
                }
                else if(board[row+2][col-2].GetNumber() == 0)
                {
                    if(board[row][col].IsDraught() == false && board[row+1][col-1].IsDraught() == true)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            break;
        }
    }

}

bool findPawn(vector< vector<Pawn> >& board, int pawn, char color, int& row, int& col)
{
    for(int i = 0; i < ROWS; i ++)
    {
        for(int j = 0; j < COLUMNS; j++)
        {
            if(board[i][j].GetColor() == color && board[i][j].GetNumber() == pawn)
            {
                row = i;
                col = j;
                return true;
            }
        }
    }
    return false;
}

void movePawn(vector< vector<Pawn> >& board, int pawn, char color, int direction)
{
    int pawnRow = -1;
    int pawnCol = -1;
    bool found = findPawn(board, pawn, color, pawnRow, pawnCol);
    if(color == 'w')
    {
        switch(direction)
        {
        case 1:
            if(board[pawnRow-1][pawnCol+1].GetNumber() == 0)
            {
                board[pawnRow-1][pawnCol+1] = board[pawnRow][pawnCol];
                Pawn emptyPawn(0);
                board[pawnRow][pawnCol] = emptyPawn;
                if(pawnRow - 1 == 0)
                {
                    board[pawnRow-1][pawnCol+1].SetDraught(true);
                }
            }
            else if(board[pawnRow-1][pawnCol+1].GetColor() == 'b')
            {
                board[pawnRow-2][pawnCol+2] = board[pawnRow][pawnCol];
                Pawn emptyPawn(0);
                board[pawnRow][pawnCol] = emptyPawn;
                board[pawnRow-1][pawnCol+1] = emptyPawn;
                if(pawnRow - 2 == 0)
                {
                    board[pawnRow-2][pawnCol+2].SetDraught(true);
                }
            }
            break;

        case 2:
            if(board[pawnRow-1][pawnCol-1].GetNumber() == 0)
            {
                board[pawnRow-1][pawnCol-1] = board[pawnRow][pawnCol];
                Pawn emptyPawn(0);
                board[pawnRow][pawnCol] = emptyPawn;
                if(pawnRow - 1 == 0)
                {
                    board[pawnRow-1][pawnCol-1].SetDraught(true);
                }
            }
            else if(board[pawnRow-1][pawnCol-1].GetColor() == 'b')
            {
                board[pawnRow-2][pawnCol-2] = board[pawnRow][pawnCol];
                Pawn emptyPawn(0);
                board[pawnRow][pawnCol] = emptyPawn;
                board[pawnRow-1][pawnCol-1] = emptyPawn;
                if(pawnRow - 2 == 0)
                {
                    board[pawnRow-2][pawnCol-2].SetDraught(true);
                }
            }
            break;

        case 3:
            if(board[pawnRow+1][pawnCol+1].GetNumber() == 0)
            {
                board[pawnRow+1][pawnCol+1] = board[pawnRow][pawnCol];
                Pawn emptyPawn(0);
                board[pawnRow][pawnCol] = emptyPawn;
            }
            else if(board[pawnRow+1][pawnCol+1].GetColor() == 'b')
            {
                board[pawnRow+2][pawnCol+2] = board[pawnRow][pawnCol];
                Pawn emptyPawn(0);
                board[pawnRow][pawnCol] = emptyPawn;
                board[pawnRow+1][pawnCol+1] = emptyPawn;
            }
            break;

        case 4:
            if(board[pawnRow+1][pawnCol-1].GetNumber() == 0)
            {
                board[pawnRow+1][pawnCol-1] = board[pawnRow][pawnCol];
                Pawn emptyPawn(0);
                board[pawnRow][pawnCol] = emptyPawn;
            }
            else if(board[pawnRow+1][pawnCol-1].GetColor() == 'b')
            {
                board[pawnRow+2][pawnCol-2] = board[pawnRow][pawnCol];
                Pawn emptyPawn(0);
                board[pawnRow][pawnCol] = emptyPawn;
                board[pawnRow+1][pawnCol-1] = emptyPawn;
            }
            break;
        }
    }
    else
    {
        switch(direction)
        {
        case 1:
            if(board[pawnRow-1][pawnCol+1].GetNumber() == 0)
            {
                board[pawnRow-1][pawnCol+1] = board[pawnRow][pawnCol];
                Pawn emptyPawn(0);
                board[pawnRow][pawnCol] = emptyPawn;
            }
            else if(board[pawnRow-1][pawnCol+1].GetColor() == 'w')
            {
                board[pawnRow-2][pawnCol+2] = board[pawnRow][pawnCol];
                Pawn emptyPawn(0);
                board[pawnRow][pawnCol] = emptyPawn;
                board[pawnRow-1][pawnCol+1] = emptyPawn;
            }
            break;

        case 2:
            if(board[pawnRow-1][pawnCol-1].GetNumber() == 0)
            {
                board[pawnRow-1][pawnCol-1] = board[pawnRow][pawnCol];
                Pawn emptyPawn(0);
                board[pawnRow][pawnCol] = emptyPawn;
            }
            else if(board[pawnRow-1][pawnCol-1].GetColor() == 'w')
            {
                board[pawnRow-2][pawnCol-2] = board[pawnRow][pawnCol];
                Pawn emptyPawn(0);
                board[pawnRow][pawnCol] = emptyPawn;
                board[pawnRow-1][pawnCol-1] = emptyPawn;
            }
            break;

        case 3:
            if(board[pawnRow+1][pawnCol+1].GetNumber() == 0)
            {
                board[pawnRow+1][pawnCol+1] = board[pawnRow][pawnCol];
                Pawn emptyPawn(0);
                board[pawnRow][pawnCol] = emptyPawn;
                if(pawnRow + 1 == 7)
                {
                    board[pawnRow+1][pawnCol+1].SetDraught(true);
                }
            }
            else if(board[pawnRow+1][pawnCol+1].GetColor() == 'w')
            {
                board[pawnRow+2][pawnCol+2] = board[pawnRow][pawnCol];
                Pawn emptyPawn(0);
                board[pawnRow][pawnCol] = emptyPawn;
                board[pawnRow+1][pawnCol+1] = emptyPawn;
                if(pawnRow + 2 == 7)
                {
                    board[pawnRow+2][pawnCol+2].SetDraught(true);
                }
            }
            break;

        case 4:
            if(board[pawnRow+1][pawnCol-1].GetNumber() == 0)
            {
                board[pawnRow+1][pawnCol-1] = board[pawnRow][pawnCol];
                Pawn emptyPawn(0);
                board[pawnRow][pawnCol] = emptyPawn;
                if(pawnRow + 1 == 7)
                {
                    board[pawnRow+1][pawnCol-1].SetDraught(true);
                }
            }
            else if(board[pawnRow+1][pawnCol-1].GetColor() == 'w')
            {
                board[pawnRow+2][pawnCol-2] = board[pawnRow][pawnCol];
                Pawn emptyPawn(0);
                board[pawnRow][pawnCol] = emptyPawn;
                board[pawnRow+1][pawnCol-1] = emptyPawn;
                if(pawnRow + 2 == 7)
                {
                    board[pawnRow+2][pawnCol-2].SetDraught(true);
                }
            }
            break;
        }
    }
}

bool checkWin(vector< vector<Pawn> >& board)
{
    for(int i = 0; i < ROWS; i ++)
    {
        for(int j = 0; j < COLUMNS; j++)
        {
            if(board[i][j].GetColor() == 'b')
            {
                return false;
            }
        }
    }
    return true;
}

bool checkLost(vector< vector<Pawn> >& board)
{
    for(int i = 0; i < ROWS; i ++)
    {
        for(int j = 0; j < COLUMNS; j++)
        {
            if(board[i][j].GetColor() == 'w')
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
