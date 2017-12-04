#include <iostream>
#include <string>
using namespace std;
int main()
{
  unsigned int score = 0;
  do
  {
      cout << "Inserisci un punteggio tra 0 e 100: "<< endl;
      cin >> score;
      if (score>100)
      {
          cout << "Non hai inserito un punteggio tra 0 e 100" << endl;
      }
  }while(score>100);
  cout << "Il tuo punteggio è: " << score << endl;
  if (score == 100)
  {
      cout<< "Hai ottenuto il punteggio massimo! " << endl;
  }
  return 0;
}