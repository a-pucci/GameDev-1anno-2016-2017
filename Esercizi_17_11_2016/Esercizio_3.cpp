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
  char rank;
  if(score<60)
  {
      rank = 'E';
  }
  else if (score<70)
  {
      rank = 'D';
  }
  else if (score<80)
  {
      rank = 'C';
  }
  else if (score<90)
  {
      rank = 'B';
  }
  else if (score<=100)
  {
      rank = 'A';
  }
  cout << "Hai ottenuto un punteggio di: " << score << " e hai ottenuto il rank " << rank << endl;
  return 0;
}
