using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySqlConnector;

namespace foglalasok
{
    internal class Program
    {
        static void menu()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder()
            {
                Server = "127.0.0.1",
                Database = "foglalasok",
                UserID = "root",
                Password = "mysql"
            };
            MySqlConnection kapcsolat = new MySqlConnection(builder.ConnectionString);
            kapcsolat.Open();
            int sorszam = 0;
            do
            {
                Console.WriteLine("1. Szobák adatainak lekérdezése");
                Console.WriteLine("2. Vendég adatainak lekérdezése és módosítása  ");
                Console.WriteLine("3. Foglalás vagy foglalás törlése");
                Console.WriteLine("4. Számla lekérdezése");
                Console.WriteLine("5. Szálloda bevételének az adatai");
                Console.Write("Üsse be a használni kívánt menüpont sorszámát: ");
                
                
                sorszam = int.Parse(Console.ReadLine());
                Console.Clear();
            }
            while (sorszam < 0 || sorszam >= 5);
            Console.Clear();

            if (sorszam == 1)
            {
                Console.WriteLine("Szobák adatainak lekérdezése:");
                Console.WriteLine("Ha vissza szeretne lépni a menübe nyomja meg a Q betűt.");
                Console.WriteLine("Ha szeretné az összes szobának az adatát lekérdezni akkor nyomja meg az ENTER-t betűt.");
                Console.Write("Kérem a szoba számát: ");
                string szam = Console.ReadLine();
                szam = szam.ToLower();
                if (szam == "q")
                {
                   Console.Clear() ;
                    menu();
  
                }

                if (szam == "")
                {

                    var parancs = kapcsolat.CreateCommand();
                    parancs.CommandText = $"Select ferohely, allapot, ar from szobak";
                    var olvas = parancs.ExecuteReader();
                    Console.Write("Férőhely     "); Console.Write("Állapot      "); Console.WriteLine("Ár");
                    Console.WriteLine(" ");
                    while (olvas.Read())
                    {
                        int adat = olvas.GetInt32(0);
                        string allapot = olvas.GetString(1);
                        int ar = olvas.GetInt32(2);

                        Console.WriteLine("   " + adat + "         " + allapot + "      " + ar);
                    }
                    olvas.Close();
                }

                if (szam != "")
                {
                    var parancs = kapcsolat.CreateCommand();
                    parancs.CommandText = $"Select ferohely, allapot, ar from szobak where szobaszam = {szam}";
                    var olvas = parancs.ExecuteReader();
                    Console.Write("Férőhely     "); Console.Write("Állapot      "); Console.WriteLine("Ár");
                    while (olvas.Read())
                    {
                        int adat = olvas.GetInt32(0);
                        string allapot = olvas.GetString(1);
                        int ar = olvas.GetInt32(2);
                        Console.WriteLine("   " + adat + "         " + allapot + "      " + ar);
                    }
                    olvas.Close();
                }
                Console.WriteLine(" ");
                Console.WriteLine("Ha vissza szeretne lépni a menübe nyomja meg az ENTER-t");
                string menü = Console.ReadLine();
                if(menü == "")
                {
                    Console.Clear();
                    menu();
                }


            }
            
            if(sorszam == 2)
            {
                Console.WriteLine("Vendég adatainak lekérdezése és módosítása:");
                Console.WriteLine("Ha vissza szeretne lépni a menübe nyomja meg a Q betűt.");
                Console.WriteLine("1. Lekérdezés");
                Console.WriteLine("2. Módosítás");
                Console.Write("Válasszon a menüpontok közül: ");
                string szam = Console.ReadLine();

                szam = szam.ToLower();
                if (szam == "q")
                {
                    Console.Clear();
                    menu();

                }
                if(szam == "1")
                {
                    Console.Write("Írja be a vendég számát(Amit a foglalásnál megadtunk): ");
                    int bevitt = int.Parse(Console.ReadLine());
                    var parancs = kapcsolat.CreateCommand();
                    parancs.CommandText = $"Select nev, nem, telefonszam, szul, email, etkezes From sz_adatok Where id = {bevitt}";
                    var olvas = parancs.ExecuteReader();
                    while (olvas.Read()) 
                    {
                        string nev = olvas.GetString(0);
                        string nem = olvas.GetString(1);
                        string telefonszam = olvas.GetString(2);
                        DateTime szul = olvas.GetDateTime(3);
                        string email = olvas.GetString(4);
                        int etkezes = olvas.GetInt32(5);
                        Console.Write("Név           "); Console.Write("Neme    "); Console.Write("Telefonszám    "); Console.Write("Születési dátum    "); Console.Write("Email címe           "); Console.WriteLine("Étkezés ");
                        Console.Write(nev + "  " + nem + "   " + telefonszam + "    " + szul.Date.ToString("yyyy-MM-dd") + "         " + email + "      "); if(etkezes == 1) Console.Write("Igen"); if(etkezes == 2) Console.WriteLine("Nem");
                        
                         
                    
                    }
                    Console.WriteLine(" ");
                    Console.WriteLine("Ha vissza szeretne lépni a menübe nyomja meg az ENTER-t");
                    string menü = Console.ReadLine();
                    if (menü == "")
                    {
                        Console.Clear();
                        menu();
                    }
                }
                
                if(szam == "2")
                {
                    Console.Write("Írja be a vendég számát(Amit a foglalásnál megadtunk): ");
                    int bevitt = int.Parse(Console.ReadLine());
                    var parancs = kapcsolat.CreateCommand();
                    parancs.CommandText = $"Select nev, nem, telefonszam, szul, email, etkezes From sz_adatok Where id = {bevitt}";
                    var olvas = parancs.ExecuteReader();
                    while (olvas.Read())
                    {
                        string nev = olvas.GetString(0);
                        string nem = olvas.GetString(1);
                        string telefonszam = olvas.GetString(2);
                        DateTime szul = olvas.GetDateTime(3);
                        string email = olvas.GetString(4);
                        int etkezes = olvas.GetInt32(5);
                        Console.Write("Név           "); Console.Write("Neme    "); Console.Write("Telefonszám    "); Console.Write("Születési dátum    "); Console.Write("Email címe           "); Console.WriteLine("Étkezés ");
                        Console.Write(nev + "  " + nem + "   " + telefonszam + "    " + szul.Date.ToString("yyyy-MM-dd") + "         " + email + "      "); if (etkezes == 1) Console.Write("Igen"); if (etkezes == 2) Console.WriteLine("Nem");



                    }
                    Console.WriteLine("Ezek az adatok közül szeretné valamelyiket megváltoztatni (Ha nem akkor nyomjon egy N    )");

                }
            }









            kapcsolat.Close();
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            menu();
            
        }
    }
}