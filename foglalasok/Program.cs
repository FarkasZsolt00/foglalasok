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
        static void Main(string[] args)
        {

            while (true)
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
                while (sorszam < 1 || sorszam > 5);
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
                        Console.Clear();
                        continue;

                    }

                    if (szam == "")
                    {

                        var parancs = kapcsolat.CreateCommand();
                        parancs.CommandText = $"Select ferohely, allapot, ar from szobak";
                        var olvas = parancs.ExecuteReader();
                        Console.Write("{0,-20} {1,-21}{2}", "Férőhely", "Állapot", "Ár");
                        Console.WriteLine("\n ");
                        while (olvas.Read())
                        {
                            int ferohely = olvas.GetInt32(0);
                            string allapot = olvas.GetString(1);
                            int ar = olvas.GetInt32(2);

                            Console.WriteLine("   {0,-18}{1,-20}{2}", ferohely, allapot, ar);
                        }
                        olvas.Close();
                    }


                    if (szam != "")
                    {
                        var parancs = kapcsolat.CreateCommand();
                        parancs.CommandText = $"Select ferohely, allapot, ar from szobak where szobaszam = {szam}";
                        var olvas = parancs.ExecuteReader();
                        Console.Write("{0,-20} {1,-21}{2}", "Férőhely", "Állapot", "Ár");
                        Console.WriteLine(" ");
                        while (olvas.Read())
                        {
                            int ferohely = olvas.GetInt32(0);
                            string allapot = olvas.GetString(1);
                            int ar = olvas.GetInt32(2);

                            Console.WriteLine("   {0,-18}{1,-20}{2}", ferohely, allapot, ar);
                        }
                        olvas.Close();
                    }
                    Console.WriteLine("\n");
                    Console.WriteLine("Ha vissza szeretne lépni a menübe nyomja meg az ENTER-t");
                    string menü = Console.ReadLine();
                    if (menü == "")
                    {
                        Console.Clear();
                        continue;
                    }


                }

                if (sorszam == 2)
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
                        continue;

                    }
                    if (szam == "1")
                    {
                        Console.Clear();
                        Console.WriteLine("1. Lekérdezés");
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
                            Console.WriteLine("{0, -16}{1, -13}{2, -20}{3, -22}{4, -20}{5, -20}", "Név", "Neme", "Telefonszám", "Születési dátum", "Email címe", "Étkezés");
                            string etkezes0 = " ";
                            if (etkezes == 1) etkezes0 = "Igen"; if (etkezes == 2) etkezes0 = "Nem";
                            Console.WriteLine("{0, -16}{1, -13}{2, -20}{3, -22}{4, -20}{5, -20}", nev, nem, telefonszam, szul.Date.ToString("yyyy-MM-dd"), email, etkezes0);



                        }
                        olvas.Close();
                        Console.WriteLine("\n");
                        Console.WriteLine("Ha vissza szeretne lépni a menübe nyomja meg az ENTER-t");
                        string menü = Console.ReadLine();
                        if (menü == "")
                        {
                            Console.Clear();
                            continue;
                        }
                    }

                    if (szam == "2")
                    {
                        Console.Clear();
                        while (true)
                        {
                            Console.WriteLine("2. Módosítás");
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
                                Console.WriteLine("{0, -16}{1, -13}{2, -20}{3, -22}{4, -20}{5, -20}", "Név", "Neme", "Telefonszám", "Születési dátum", "Email címe", "Étkezés");
                                string etkezes0 = " ";
                                if (etkezes == 1) etkezes0 = "Igen"; if (etkezes == 2) etkezes0 = "Nem";
                                Console.WriteLine("{0, -16}{1, -13}{2, -20}{3, -22}{4, -20}{5, -20}", nev, nem, telefonszam, szul.Date.ToString("yyyy-MM-dd"), email, etkezes0);



                            }
                            olvas.Close();
                            Console.WriteLine("Ezek az adatok közül szeretné valamelyiket megváltoztatni(Ha igen nyomjon bármilyen karaktert, ha nam csak ENTERT) ");
                            string valasz = Console.ReadLine();
                            if (valasz == "")
                            {
                                Console.Clear();
                                continue;
                            }
                            Console.Clear();
                            if (valasz != "")
                            {
                                Console.WriteLine("Melyik adatot szeretné megváltoztatni?");
                                Console.WriteLine("1. Név");
                                Console.WriteLine("2. Nem");
                                Console.WriteLine("3. Telefonszám");
                                Console.WriteLine("4. Születési dátum");
                                Console.WriteLine("5. Email cím");
                                Console.WriteLine("6. Étkezés");
                                Console.Write("Válasszon a menüpontok közül: ");
                                string szam2 = Console.ReadLine();
                                Console.Clear();
                                try
                                {
                                    if (szam2 == "1")
                                    {
                                        Console.Write("Írja be a új nevet: ");
                                        string ujnev = Console.ReadLine();
                                        var parancs2 = kapcsolat.CreateCommand();
                                        parancs2.CommandText = $"Update sz_adatok Set nev = '{ujnev}' Where id = {bevitt}";
                                        parancs2.ExecuteNonQuery();
                                        Console.Clear();

                                    }
                                    if (szam2 == "2")
                                    {
                                        Console.Write("Írja be a új nemet: ");
                                        string ujnem = Console.ReadLine();
                                        var parancs2 = kapcsolat.CreateCommand();
                                        parancs2.CommandText = $"Update sz_adatok Set nem = '{ujnem}' Where id = {bevitt}";
                                        parancs2.ExecuteNonQuery();
                                        Console.Clear();

                                    }
                                    if (szam2 == "3")
                                    {
                                        Console.Write("Írja be a új telefonszámot: ");
                                        string ujtelefonszam = Console.ReadLine();
                                        var parancs2 = kapcsolat.CreateCommand();
                                        parancs2.CommandText = $"Update sz_adatok Set telefonszam = '{ujtelefonszam}' Where id = {bevitt}";
                                        parancs2.ExecuteNonQuery();
                                        Console.Clear();

                                    }
                                    if (szam2 == "4")
                                    {
                                        Console.Write("Írja be a új születési dátumot(ÉÉÉÉ-HH-NN): ");
                                        string ujszul = Console.ReadLine();
                                        var parancs2 = kapcsolat.CreateCommand();
                                        parancs2.CommandText = $"Update sz_adatok Set szul = '{ujszul}' Where id = {bevitt}";
                                        parancs2.ExecuteNonQuery();

                                    }
                                }
                                catch (Exception)
                                {

                                    Console.WriteLine("A módosítás sikertelen");
                                    continue;
                                }
                                Console.WriteLine("A módosítás sikeres");
                                break;


                            }

                        }
                        Console.WriteLine("\n");
                        Console.WriteLine("Ha vissza szeretne lépni a menübe nyomja meg az ENTER-t");
                        string menü = Console.ReadLine();
                        if (menü == "")
                        {
                            Console.Clear();
                            continue;
                        }
                    }
                }
                if (sorszam == 3)
                {
                    string szam = "";
                    while (true)
                    {
                        Console.WriteLine("Foglalás vagy foglalás törlése:");
                        Console.WriteLine("Ha vissza szeretne lépni a menübe nyomja meg a Q betűt.");
                        Console.WriteLine("1. Foglalás");
                        Console.WriteLine("2. Foglalás törlése");
                        Console.Write("Válasszon a menüpontok közül: ");
                        szam = Console.ReadLine();
                        szam = szam.ToLower();
                        if (szam == "q")
                        {
                            Console.Clear();
                            continue;
                        }
                        if (szam == "1")
                        {
                            Console.Clear();
                            Console.WriteLine("1. Foglalás");
                            var parancs = kapcsolat.CreateCommand();
                            parancs.CommandText = $"Select szobaszam, ferohely, allapot, ar from szobak WHERE allapot = 'üres'";
                            var olvas = parancs.ExecuteReader();
                            Console.WriteLine("{0,-20} {1,-20}{2, -21}{3}", "Szobaszám", "Férőhely", "Állapot", "Ár");
                            while (olvas.Read())
                            {
                                int szobaszam = olvas.GetInt32(0);
                                int ferohely = olvas.GetInt32(1);
                                string allapot = olvas.GetString(2);
                                int ar = olvas.GetInt32(3);

                                Console.WriteLine("   {0,-21}{1,-18}{2, -20}{3}", szobaszam, ferohely, allapot, ar);
                            }
                            olvas.Close();
                            try
                            {
                                Console.Write("Írja be a lefoglalni kívánt szobának a számát: ");
                                int szam2 = int.Parse(Console.ReadLine());
                                Console.Write("Kérem a foglaláshoz tartozó nevet: ");
                                string nev = Console.ReadLine();
                                Console.Write("Kérem a foglaláshoz tartozó telefonszámot: ");
                                string telefonszam = Console.ReadLine();
                                Console.Write("Kérem a nemét: ");
                                string nem = Console.ReadLine().ToLower();
                                Console.Write("Kérem a születési dátumát(Kötöjelekkel elválasztva Pl.: 1999-01-02): ");
                                string szul = Console.ReadLine();
                                Console.Write("Kérem az email címét: ");
                                string email = Console.ReadLine().ToLower();
                                Console.Write("Kér-e étkezést (Írjon 1-est ha igen, 2-est ha nem): ");
                                int etkezes = int.Parse(Console.ReadLine());
                                Console.WriteLine("Adja meg a fizetési módot(Készpénz/Bankártya): ");
                                string fizetes = Console.ReadLine().ToLower();
                                var parancs3 = kapcsolat.CreateCommand();
                                parancs3.CommandText = $"Select id from sz_adatok order by id desc limit 1";
                                var olvas2 = parancs3.ExecuteReader();
                                int id = 0;
                                while (olvas2.Read())
                                {
                                    id = olvas2.GetInt32(0);
                                }
                                olvas.Close();
                                var parancs4 = kapcsolat.CreateCommand();
                                parancs4.CommandText = $"Insert Into sz_adatok (id, nev, nem, telefonszam, szul, email, etkezes) Values ('{id += 1}','{nev}', '{nem}', '{telefonszam}', '{szul}', '{email}', {etkezes}, {fizetes})";
                                parancs4.ExecuteNonQuery();
                                var parancs2 = kapcsolat.CreateCommand();
                                parancs2.CommandText = $"Update szobak Set allapot = 'foglalt' Where szobaszam = {szam2}";
                                parancs2.ExecuteNonQuery();
                                Console.Write("Adja meg a foglalás kezdetét(Pl.: 2000-01-01): ");
                                string kezdet = Console.ReadLine();
                                Console.Write("Aja meg a távozás napját(Pl.: 2000-01-02): ");
                                string veg = Console.ReadLine();
                                var parancs5 = kapcsolat.CreateCommand();
                                parancs5.CommandText = $"Insert Into foglalas (szobaszam, vendeg_id, erkezes, tavozas) Values ({szam2}, {id}, '{kezdet}', '{veg}')";
                                parancs5.ExecuteNonQuery();
                            }
                            catch (Exception)
                            {
                                Console.Clear();
                                Console.WriteLine("A foglalas sikertelen");
                                continue;
                            }
                            Console.Clear();
                            Console.WriteLine("A foglalas sikeres");
                            break;
                        }

                        Console.WriteLine("\n");
                        Console.WriteLine("Ha vissza szeretne lépni a menübe nyomja meg az ENTER-t");
                        string menü = Console.ReadLine();
                        if (menü == "")
                        {
                            Console.Clear();
                            continue;
                        }
                    }
                    if (szam == "2")
                    {
                        Console.Clear();
                        Console.WriteLine("2. Foglalás törlése");
                        Console.Write("Kérem a törölni kívánt foglaláshoz tartozó szoba számát: ");
                        int szam2 = int.Parse(Console.ReadLine());
                        var parancs = kapcsolat.CreateCommand();
                        parancs.CommandText = $"Delete From foglalas Where szobaszam = {szam2}";
                        parancs.ExecuteNonQuery();
                        var parancs2 = kapcsolat.CreateCommand();
                        parancs2.CommandText = $"Update szobak Set allapot = 'üres' Where szobaszam = {szam2}";
                        parancs2.ExecuteNonQuery();
                        var parancs3 = kapcsolat.CreateCommand();
                        parancs3.CommandText = $"Select vendeg_id From foglalas Where szobaszam = {szam2}";
                        var olvas = parancs3.ExecuteReader();
                        int vendeg_id = 0;
                        while (olvas.Read())
                        {
                            vendeg_id = olvas.GetInt32(0);
                        }
                        var parancs4 = kapcsolat.CreateCommand();
                        parancs4.CommandText = $"Delete From sz_adatok Where id = {vendeg_id}";
                        parancs4.ExecuteNonQuery();
                        Console.WriteLine("\n");
                        Console.WriteLine("Ha vissza szeretne lépni a menübe nyomja meg az ENTER-t");
                        string menü = Console.ReadLine();
                        if (menü == "")
                        {
                            Console.Clear();
                            continue;
                        }
                    }
                }
                if (sorszam == 4)
                {
                    Console.WriteLine("Számla lekérdezése:");
                    Console.WriteLine("Ha vissza szeretne lépni a menübe nyomja meg a Q betűt.");
                    Console.Write("Adja meg a vendég számát (Amit a foglalásnál megadtunk): ");
                    string szam = Console.ReadLine();
                    szam = szam.ToLower();
                    if (szam == "q")
                    {
                        Console.Clear();
                        continue;
                    }
                    var parancs = kapcsolat.CreateCommand();
                    parancs.CommandText = $"Select nev, fizetesi_mod From sz_adatok Where id = {szam}";
                    var olvas = parancs.ExecuteReader();
                    string fizetesi_mod0 = " ";
                    string nev0 = " ";
                    while (olvas.Read())
                    {
                        string nev = olvas.GetString(0);
                        string fizetesi_mod = olvas.GetString(1);
                        nev0 = nev;
                        fizetesi_mod0 = fizetesi_mod;
                    }
                    olvas.Close();
                    var parancs2 = kapcsolat.CreateCommand();
                    parancs2.CommandText = $"Select szobaszam From foglalas Where vendeg_id = {szam}";
                    var olvas2 = parancs2.ExecuteReader();
                    int szobaszam0 = 0;
                    while (olvas2.Read())
                    {
                        int szobaszam = olvas2.GetInt32(0);
                        szobaszam0 = szobaszam;

                    }
                    olvas2.Close();
                    var parancs3 = kapcsolat.CreateCommand();
                    parancs3.CommandText = $"Select ar From szobak Where szobaszam = {szobaszam0}";
                    var olvas3 = parancs3.ExecuteReader();
                    int ar0 = 0;
                    while (olvas3.Read())
                    {
                        int ar = olvas3.GetInt32(0);
                        ar0 = ar;
                    }
                    olvas3.Close();
                    Console.WriteLine("{0, -16}{1, -20}{2}", "Név", "Fizetett összeg", "Fizesi mód");
                    Console.WriteLine("{0, -16}{1, -20}{2}", nev0, ar0, fizetesi_mod0);

                    Console.WriteLine("\n");
                    Console.WriteLine("Ha vissza szeretne lépni a menübe nyomja meg az ENTER-t");
                    string menü = Console.ReadLine();
                    if (menü == "")
                    {
                        Console.Clear();
                        continue;
                    }

                }
                if (sorszam == 5)
                {
                    Console.WriteLine("Szálloda bevételének az adatai:");
                    var parancs = kapcsolat.CreateCommand();
                    parancs.CommandText = $"Select sum(ar) From szobak Where allapot = 'foglalt'";
                    var olvas = parancs.ExecuteReader();
                    int bevetel = 0;
                    while (olvas.Read())
                    {
                        bevetel = olvas.GetInt32(0);
                    }
                    olvas.Close();
                    Console.WriteLine($"A szálloda jelenlegi bevétele: {bevetel}Ft");

                    Console.WriteLine("\n");
                    Console.WriteLine("Ha vissza szeretne lépni a menübe nyomja meg az ENTER-t");
                    string menü = Console.ReadLine();
                    if (menü == "")
                    {
                        Console.Clear();
                        continue;
                    }
                }

                kapcsolat.Close();
                Console.ReadKey();
            }

        }
    }
}