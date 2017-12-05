using System;
using System.Collections.Generic;
using System.Xml;

namespace XML
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("CustomIdentities.xml");

            PrintIdentities(xDoc);

            Console.ReadLine();
        }

        static void PrintIdentities(XmlDocument xDoc)
        {
            XmlNode parentNode = xDoc.SelectSingleNode("queue");

            foreach (XmlNode childNode in parentNode.ChildNodes)
            {
                // Nome
                if (childNode.SelectSingleNode("firstName") != null)
                {
                    string firstName = childNode.SelectSingleNode("firstName").InnerText;
                    Console.WriteLine("NOME: " + firstName);
                }
                
                // Cognome
                if (childNode.SelectSingleNode("lastName") != null)
                {
                    string lastName = childNode.SelectSingleNode("lastName").InnerText;
                    Console.WriteLine("COGNOME: " + lastName);
                }

                // Data di nascita
                string birthdate = "";
                if (childNode.SelectSingleNode("birthdate") != null)
                {
                   birthdate = childNode.SelectSingleNode("birthdate").InnerText;                    
                }
                else
                {
                   birthdate = GetRandomBirthdate();
                }
                String[] splittedDate = birthdate.Split('/');
                int currentYear = DateTime.Now.Year;
                int birthYear = Convert.ToInt32(splittedDate[2]);
                int ages = currentYear - birthYear;
                Console.WriteLine("DATA DI NASCITA: " + birthdate + " (" + ages + " anni)");

                // Peso
                string weight = "";
                if (childNode.SelectSingleNode("weight") != null)
                {
                     weight = childNode.SelectSingleNode("weight").InnerText;
                }
                else
                {
                    weight = GetRandomWeight();
                }
                Console.WriteLine("PESO: " + weight + " kg");

                // Altezza
                string height = "";
                if (childNode.SelectSingleNode("height") != null)
                {
                    string heightString = childNode.SelectSingleNode("height").InnerText;
                    double heightCentimeters = Convert.ToDouble(heightString);
                    double heightMeters = heightCentimeters / 100;
                    height = heightMeters.ToString();
                }
                else
                {
                    height = GetRandomHeight();
                }
                Console.WriteLine("ALTEZZA: " + height + " m");

                // ID documento
                string IDCard = "";
                if (childNode.SelectSingleNode("IDCard") != null)
                {
                    IDCard = childNode.SelectSingleNode("IDCard").InnerText;
                    
                }
                else
                {
                    IDCard = GetRandomIDCard();
                }
                Console.WriteLine("ID DOCUMENTO: " + IDCard);

                // Scadenza documento
                string IDExpiration = "";
                if (childNode.SelectSingleNode("IDExpiration") != null)
                {
                    IDExpiration = childNode.SelectSingleNode("IDExpiration").InnerText;                    
                }
                else
                {
                   IDExpiration = GetRandomIDExpiration();
                }
                Console.WriteLine("SCADENZA DOCUMENTO: " + IDExpiration);

                // Città
                if (childNode.SelectSingleNode("city") != null && childNode.SelectSingleNode("country") != null)
                {
                    string city = childNode.SelectSingleNode("city").InnerText;
                    string country = childNode.SelectSingleNode("country").InnerText;
                    Console.WriteLine("CITTÀ: " + city + " (" + country + ")");
                }
                else
                {
                    Console.WriteLine("CITTÀ: " + GetRandomCity());
                }

                // Dichiarazione
                string declaration = "";
                if (childNode.SelectSingleNode("declaration") != null)
                {
                    declaration = childNode.SelectSingleNode("declaration").InnerText;                    
                }
                else
                { 
                    declaration = GetRandomDeclaration();
                }
                Console.WriteLine("DICHIARAZIONE: " + declaration);

                Console.WriteLine("\n");
            }

        }

        static string GetRandomBirthdate()
        {
            Random random = new Random();
            int randomDay = random.Next(1, 31);
            int randomMonth = random.Next(1, 12);
            int currentYear = DateTime.Now.Year;
            int randomYear = random.Next(1923, currentYear - 18);

            int ages = currentYear - randomYear;
            string randomBirthdate = randomDay.ToString() + "/" + randomMonth.ToString() + "/" + randomYear.ToString();

            return randomBirthdate;

        }

        static string GetRandomWeight()
        {
            Random random = new Random();
            int randomWeight = random.Next(40, 150);

            return randomWeight.ToString();
        }

        static string GetRandomHeight()
        {
            Random random = new Random();
            double randomHeightCentimeters = random.Next(140, 210);
            double randomHeightMeters = randomHeightCentimeters / 100;

            return randomHeightMeters.ToString();
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

        static string GetRandomIDExpiration()
        {
            Random random = new Random();
            int currentDay = DateTime.Now.Day;
            int randomDay = random.Next(1, currentDay);
            int currentMonth = DateTime.Now.Month;
            int randomMonth = random.Next(1, currentMonth);
            int currentYear = DateTime.Now.Year;
            int randomYear = random.Next(currentYear, currentYear + 10);

            string randomExpirationDate = randomDay.ToString() + "/" + randomMonth.ToString() + "/" + randomYear.ToString();

            return randomExpirationDate;
        }

        static string GetRandomCity()
        {
            List<string> countries = new List<string>() { "Italia", "Germania", "Inghilterra", "Spagna" };

            List<string> italyCities = new List<string>() {"Milano", "Roma", "Firenze", "Napoli", "Torino" };
            List<string> GermanyCities = new List<string>() {"Berlino", "Francoforte", "Amburgo" };
            List<string> EnglandCities = new List<string>() { "Londra", "Manchester", "Liverpool", "Oxford" };
            List<string> SpainCities = new List<string>() { "Madrid", "Barcellona", "Siviglia", "Marsiglia", "Valencia" };

            Random random = new Random();
            int randomCountryNumber = random.Next(0, countries.Count - 1);
            string randomCountry = countries[randomCountryNumber];
            Console.WriteLine(randomCountry);
            string randomCity = "";

            switch (randomCountryNumber)
            {
                case 0:
                    int randomItalyCityNumber = random.Next(0, italyCities.Count - 1);
                    randomCity = italyCities[randomItalyCityNumber];
                    break;

                case 1:
                    int randomGermanyCityNumber = random.Next(0, GermanyCities.Count - 1);
                    randomCity = GermanyCities[randomGermanyCityNumber];
                    break;

                case 2:
                    int randomEnglandCityNumber = random.Next(0, EnglandCities.Count - 1);
                    randomCity = EnglandCities[randomEnglandCityNumber];
                    break;

                case 3:
                    int randomSpainCityNumber = random.Next(0, SpainCities.Count - 1);
                    randomCity = SpainCities[randomSpainCityNumber];
                    break;

            }

            return randomCity + " (" + randomCountry + ")";
        }

        static string GetRandomDeclaration()
        {
            return "Nulla da dichiarare.";
        }
    }
}