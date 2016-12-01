#include <iostream>
#include <vector>
#include <algorithm>
#include <string>
#include <ctime>

using namespace std;

void showWares(vector<string>& shipStow);
void loadShip(vector<string>& shipStow);
void looseWares(vector<string>& shipStow);
void giftedWares(vector<string>& shipStow, string gift);
void tradeGoods(vector<string>& shipStow, string sold, string bought);
void pirateAttack(vector<string>& shipStow, string stolenGoods);
void orderWares(vector<string>& shipStow);
void tempest(vector<string>& shipStow);
void sellAllWares(vector<string>& shipStow);

int main()
{
    cout << "Sei il capitano di un mercantile. Devi portare il carico al mercato di Tiro" << endl;

    vector<string> ship;

    cout << "\nCarichi la tua nave con Vino, Lana, Avorio e Legno" << endl;
    loadShip(ship);

    cout << "\n\nPoco dopo la tua partenza, perdi l'ultima merce caricata perche' non e' stata legata bene" << endl;
    looseWares(ship);

    cout << "\n\nLungo il viaggio un mercante ti ha donato del Te'" << endl;
    giftedWares(ship, "Te'");

    cout << "\n\nArrivato ad un mercato decidi di scambiare la Lana per del Frumento" << endl;
    tradeGoods(ship, "Lana", "Frumento");

    cout << "\n\nVieni attaccato da dei pirati che ti rubano il Vino" << endl;
    pirateAttack(ship, "Vino");

    cout << "\n\nDecidi di rimettere tutto in ordine alfabetico" << endl;
    orderWares(ship);

    cout << "\n\nUna tempesta mischia tutte le merci nella stiva" << endl;
    tempest(ship);

    cout << "\n\nArrivi al mercato di Tiro e vendi tutte le merci per dell'oro" << endl;
    sellAllWares(ship);

    return 0;
}

void showWares(vector<string>& shipStow)
{
    cout << "\nMerce nella stiva:" << endl;
    for(int i = 0; i < shipStow.size(); i++)
    {
        cout << shipStow[i] << endl;
    }

    return;
}

void loadShip(vector<string>& shipStow)
{
    shipStow.push_back("Vino");
    shipStow.push_back("Lana");
    shipStow.push_back("Avorio");
    shipStow.push_back("Legno");

    showWares(shipStow);

    return;
}

void looseWares(vector<string>& shipStow)
{
    shipStow.pop_back();

    showWares(shipStow);

    return;
}

void giftedWares(vector<string>& shipStow, string gift)
{
    vector<string> :: iterator it;
    it = shipStow.begin();
    shipStow.insert(it, gift);

    showWares(shipStow);

    return;
}

void tradeGoods(vector<string>& shipStow, string sold, string bought)
{
    vector<string> :: iterator it;
    it = find(shipStow.begin(), shipStow.end(), sold);
    *it = bought;

     showWares(shipStow);

     return;
}

void pirateAttack(vector<string>& shipStow, string stolenGoods)
{
    vector<string> :: iterator it;
    it = find(shipStow.begin(), shipStow.end(), stolenGoods);
    shipStow.erase(it);

    showWares(shipStow);

    return;
}

void orderWares(vector<string>& shipStow)
{
    sort(shipStow.begin(), shipStow.end());

    showWares(shipStow);

    return;
}

void tempest(vector<string>& shipStow)
{
    srand(time(NULL));
    random_shuffle(shipStow.begin(), shipStow.end());

    showWares(shipStow);

    return;
}

void sellAllWares(vector<string>& shipStow)
{
    shipStow.clear();
    shipStow.push_back("Oro");

    showWares(shipStow);

    return;
}


