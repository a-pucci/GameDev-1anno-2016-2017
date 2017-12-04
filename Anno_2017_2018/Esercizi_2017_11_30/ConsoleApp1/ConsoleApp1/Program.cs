using System;
using System.Collections.Generic;
using System.Xml;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static void PrintIdentity() { }

        static string GetRandomBirthdate()
        {

        }

        static string GetRandomWeight()
        {
            Random random = new Random();
            int randomNumber = random.Next(40, 150);
            return randomNumber.ToString();
        }

        static string GetRandomHeight()
        {
            Random random = new Random();
            int randomNumber = random.Next(140, 210);
            return randomNumber.ToString();
        }

        static string GetRandomIDCard()
        {
            Random random = new Random();
            int randomNumber;
            string idCard = "";

            for (int i = 0; i < 10; i++)
            {
                randomNumber = random.Next(0, 9);
                idCard = idCard + randomNumber.ToString();
            }
            return idCard;
        }

        static string GetRandomIDExpiration() { }
        static string GetRandomCountry() { }
        static string GetRandomCity() { }
    }
}
