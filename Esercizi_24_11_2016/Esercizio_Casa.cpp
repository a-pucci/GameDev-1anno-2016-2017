#include <iostream>
#include <cstdlib>
#include <string>
#include <ctime>
using namespace std;

void printBoard(char a[][3]);
int getRow();
int getCol();
bool checkWin(char board[][3]);
void aiRandomMove(char board[][3], char aiChar);
void aiHardMove(char board [][3], char aiChar, char playerChar, int lastRow, int lastCol);
bool isFirst();
char getSign();
bool hardMode();
bool playAgain();
bool aboutToWin(char board[][3], char playerChar);
bool blockRowCol(char board[][3], char aiChar, char playerChar, int row, int col);
void blockDiagonals(char board[][3], char aiChar, char playerChar, int row, int col);

int main()
{
    bool again = true;
    bool hard = true;
    while(again)
    {
        system("CLS");
        cout << "\tTIC TAC TOE\n" << endl;
        if(hard)
        {
            cout << "\nHARD MODE!!!\n" << endl;
        }

       //Scelta carattere giocatore

        char playerChar = getSign();

        char aiChar;
        if(playerChar == 'X')
        {
            aiChar = 'O';
        }
        else
        {
            aiChar = 'X';
        }

        //Scelta primo giocatore

        bool playerTurn = isFirst();

        //costruzione e inizializzazione board

        const int ROWS = 3;
        const int COLUMNS = 3;
        char board[ROWS][COLUMNS] = { {' ', ' ', ' '},
                                      {' ', ' ', ' '},
                                      {' ', ' ', ' '} };

        cout << "\n";
        printBoard(board);
        cout << "\n";
        bool winner = false;
        int counter = 0;
        int row = 4;
        int col = 4;
        while(!winner && counter < 9)
        {


            if(playerTurn)
            {


                cout << "\nE' il tuo turno" << endl;

                while(board[row][col] != ' ')
                {
                    row = getRow();
                    col = getCol();

                    if(board[row][col] != ' ')
                    {
                        cout << "\nSpazio gia' occupato" << endl;
                    }
                }
                if(board[row][col] == ' ')
                {
                    board[row][col] = playerChar;
                }

                cout << "\n\nTurno Giocatore:" << "\n" << endl;
                printBoard(board);
                winner = checkWin(board);
                playerTurn = !playerTurn;
                if(winner)
                {
                    again = playAgain();
                    hard = hardMode();
                }
            }
            else
            {
                if(hard)
                {
                    aiHardMove(board, aiChar, playerChar, row, col);
                    cout << "\n\nTurno Computer:\n" << endl;
                    printBoard(board);
                    winner = checkWin(board);
                    playerTurn = !playerTurn;
                    if(winner)
                    {
                        again = playAgain();
                        if(again)
                        {
                            hard = hardMode();
                        }

                    }
                }
                else
                {
                    aiRandomMove(board, aiChar);
                    cout << "\n\nTurno Computer:\n" << endl;
                    printBoard(board);
                    winner = checkWin(board);
                    playerTurn = !playerTurn;
                    if(winner)
                    {
                        again = playAgain();
                    }
                }

            }
            counter++;
        }

        if(!winner)
        {
            cout << "\nLa partita e' finita con un pareggio" << endl;
            again = playAgain();
            if(again && hard)
            {
                hard = hardMode();
            }
        }
    }


    return 0;


}

bool aboutToWin(char board [][3], char playerChar)
{
    int countRow = 0;
    int countCol = 0;
    int countDiag1 = 0;
    int countDiag2 = 0;

    for(int i = 0; i < 3; i++)
    {
        for(int j = 0; j < 3; j++)
        {
            if(board[i][j] == playerChar)
            {
                countCol++;
            }
        }
        if(countCol > 1)
        {
            break;
        }
    }

    for(int j = 0; j < 3; j++)
    {
        for(int i = 0; i < 3; i++)
        {
            if(board[i][j] == playerChar)
            {
                countRow++;
            }
        }
        if(countRow > 1)
        {
            break;
        }
    }

    for(int i = 0; i < 3; i++)
    {
        if(board[i][i] == playerChar)
        {
            countDiag1++;
        }
    }

    int k = 2;
    for( int i = 0; i < 3; i++)
    {
        if(board[i][k] == playerChar)
        {
            countDiag2++;
        }
        k--;
    }

    if(countRow > 1 || countCol > 1 || countDiag1 > 1 || countDiag2 > 1 )
    {
        return true;
    }

    return false;
}

void aiHardMove(char board[][3], char aiChar, char playerChar, int lastRow, int lastCol)
{
    if(aboutToWin(board, playerChar))
    {
        int sum = lastRow+lastCol;
        bool block;

        if(sum == 0 || sum == 2 || sum == 4)
        {
            block = blockRowCol(board, aiChar, playerChar, lastRow, lastCol);
            if(!block)
            {
                blockDiagonals(board, aiChar, playerChar, lastRow, lastCol);
            }

        }
        else if(sum == 1 || sum == 3)
        {
            blockRowCol(board, aiChar, playerChar, lastRow, lastCol);
        }
    }
    else
    {
        aiRandomMove(board, aiChar);
    }


}

bool blockRowCol(char board[][3], char aiChar, char playerChar, int row, int col)
{
    int countRow = 0;
    bool matchRow[3];
    int countCol = 0;
    bool matchCol[3];
    int rowBlock;
    int colBlock;

    for(int i = 0; i < 3; i++)
    {
        if(board[i][col] == playerChar)
        {
            countRow++;
            matchRow[i] = false;
        }
        else
        {
            matchRow[i] = true;
        }
    }

    for(int j = 0; j < 3; j++)
    {
        if(board[row][j] == playerChar)
        {
            countCol++;
            matchCol[j] = false;
        }
        else
        {
            matchCol[j] = true;
        }
    }

    if(countRow > 1 )
    {
        for(int i = 0; i < 3; i++)
        {
            if(matchRow[i])
            {
                rowBlock = i;
            }
        }
        if(board[rowBlock][col] == ' ')
        {
            board[rowBlock][col] = aiChar;
            return true;
        }
        else
        {
            aiRandomMove(board, aiChar);
        }

        return false;
    }

    if(countCol > 1)
    {
        for(int j = 0; j < 3 ; j++)
        {
            if(matchCol[j])
            {
                colBlock = j;
            }
        }
        if(board[row][colBlock] == ' ')
        {
            board[row][colBlock] = aiChar;
            return true;
        }
        else
        {
            aiRandomMove(board, aiChar);
        }

        return false;
    }

    return false;

}

void blockDiagonals(char board[][3], char aiChar, char playerChar, int row, int col)
{
    int countDiag1 = 0;
    bool matchDiag1[3];
    int countDiag2 = 0;
    bool matchDiag2[3];
    int rowBlock;
    int colBlock;
    for(int i = 0; i < 3; i++)
    {
        if(board[i][i] == playerChar)
        {
            countDiag1++;
            matchDiag1[i] = false;
        }
        else
        {
            matchDiag1[i] = true;
        }
    }

    int k = 2;
    for( int i = 0; i < 3; i++ )
    {
        if(board[i][k] == playerChar)
        {
            countDiag2++;
            matchDiag2[i] = false;
        }
        else
        {
            matchDiag2[i] = true;
        }
        k--;
    }

    if(countDiag1 == 2)
    {
        for(int i = 0; i < 3; i++)
        {
            if(matchDiag1[i])
            {
                rowBlock = i;
                colBlock = i;
            }
        }

        if(board[rowBlock][colBlock] == ' ')
        {
            board[rowBlock][colBlock] = aiChar;
        }
        else
        {
            aiRandomMove(board, aiChar);
        }
    }

    if(countDiag2 == 2)
    {
        for(int j = 0; j < 3 ; j++)
        {
            if(matchDiag2[j])
            {
                rowBlock = j;
                colBlock = 2-j;
            }
        }
        if(board[rowBlock][colBlock] == ' ')
        {
            board[rowBlock][colBlock] = aiChar;
        }
        else
        {
            aiRandomMove(board, aiChar);
        }
    }
    return;
}

bool hardMode()
{
    char hard = ' ';
    while( hard != 'y' && hard != 'n')
    {
        cout << "\nVuoi provare in modalita' difficile?"
        << "\n y = Yes"
        << "\n n = No" << endl;

        cin >> hard;
        if( hard != 'y' && hard != 'n')
        {
            cout << "\nNon hai scelto una risposta corretta" << endl;
        }
        else if( hard == 'y')
        {
            return true;
        }
        else if( hard == 'n')
        {
            return false;
        }
    }


}

bool playAgain()
{
    char again;
    while(again != 'y' && again != 'n')
    {
        cout << "\nVuoi giocare di nuovo?"
        << "\n y = Yes"
        << "\n n = No" << endl;
        cin >> again;
        if(again != 'y' && again != 'n')
        {
            cout << "\nNon hai scelto una risposta corretta" << endl;
        }
        else if( again == 'y')
        {
            return true;
        }
        else if( again == 'n')
        {
            return false;
        }
    }

}

char getSign()
{
    char sign;
    while(sign != 'X' && sign != 'O')
    {
        cout << "\nScegli se giocare come X o O: ";
        cin >> sign;
        if(sign != 'X' && sign != 'O')
        {
            cout << "\nNon hai scelto un carattere corretto" << endl;
        }
        else
        {
            cout << "\nHai scelto " << sign << endl;
            return sign;
        }
    }
}

bool isFirst()
{
    char first;
    while (first != 'y' && first != 'n')
    {
        cout << "\nVuoi giocare per primo?"
        << "\n  y = yes"
        << "\n  n = no" << endl;
        cin >> first;
        if(first != 'y' && first != 'n')
        {
            cout << "\nNon hai scelto una risposta corretta" << endl;
        }
        else if(first == 'y')
        {
            return true;
            cout << "\nGiochi per primo!" << endl;
        }
        else if(first == 'n')
        {
            return false;
            cout << "\nIl primo turno e' del Computer" << endl;
        }
    }
}

void printBoard(char a[][3])
{
    cout << "           1   2   3" << endl;
    cout << "\t" << " -------------" << endl;
    for(int i = 0; i< 3; i++)
    {
        cout << "      "<< i+1;
        cout << "\t " << "| ";
        for (int j =0; j< 3; j++)
        {
            cout << a[i][j] << " | ";
        }
        cout << endl;
        cout << "\t" << " -------------" << endl;
    }
}

int getRow()
{
    int row;
    do
    {
        cout << "\nInserire numero riga: ";
        cin >> row;
        if(row > 3 || row < 1)
        {
            cout << "\nNon hai inserito un numero corretto, scegli tra 1, 2 o 3." << endl;
        }
    } while(row > 3 || row < 1);
    row -= 1;
    return row;
}

int getCol()
{
    int col;
    do
    {
        cout << "\nInserire numero colonna: ";
        cin >> col;
        if(col > 3 || col < 1)
        {
            cout << "\nNon hai inserito un numero corretto, scegli tra 1, 2 o 3." << endl;
        }
    } while(col > 3 || col < 1);
    col -= 1;
    return col;
}

bool checkWin(char board[][3])
{
    bool winner = false;

    if( ( board[0][0] == 'X' && board[0][1] == 'X' && board [0][2] == 'X' ) ||
        ( board[1][0] == 'X' && board[1][1] == 'X' && board [1][2] == 'X' ) ||
        ( board[2][0] == 'X' && board[2][1] == 'X' && board [2][2] == 'X' ) ||
        ( board[0][0] == 'X' && board[1][0] == 'X' && board [2][0] == 'X' ) ||
        ( board[0][1] == 'X' && board[1][1] == 'X' && board [2][1] == 'X' ) ||
        ( board[0][2] == 'X' && board[1][2] == 'X' && board [2][2] == 'X' ) ||
        ( board[0][0] == 'X' && board[1][1] == 'X' && board [2][2] == 'X' ) ||
        ( board[0][2] == 'X' && board[1][1] == 'X' && board [2][0] == 'X' ) )
    {
        winner = true;
        cout << "\n\n\tIl giocatore X ha vinto!" << endl;
        return winner;
    }

    if( ( board[0][0] == 'O' && board[0][1] == 'O' && board [0][2] == 'O' ) ||
        ( board[1][0] == 'O' && board[1][1] == 'O' && board [1][2] == 'O' ) ||
        ( board[2][0] == 'O' && board[2][1] == 'O' && board [2][2] == 'O' ) ||
        ( board[0][0] == 'O' && board[1][0] == 'O' && board [2][0] == 'O' ) ||
        ( board[0][1] == 'O' && board[1][1] == 'O' && board [2][1] == 'O' ) ||
        ( board[0][2] == 'O' && board[1][2] == 'O' && board [2][2] == 'O' ) ||
        ( board[0][0] == 'O' && board[1][1] == 'O' && board [2][2] == 'O' ) ||
        ( board[0][2] == 'O' && board[1][1] == 'O' && board [2][0] == 'O' ) )
    {
        winner = true;
        cout << "\n\n\tIl giocatore O ha vinto!" << endl;
        return winner;
    }
    return winner;

}

void aiRandomMove(char board[][3], char aiChar)
{
    int row;
    int col;
    srand(static_cast<unsigned int>(time(0)));
    do
    {
        row = rand() % 3;
        col = rand() % 3;
    }
    while(board[row][col] != ' ');
    if (board[row][col] == ' ')
    {
        board[row][col] = aiChar;
    }
}


