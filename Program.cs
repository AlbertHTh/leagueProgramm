using System;
using System.Data.SqlClient;
using System.IO;




namespace LeagueVorProgramm
{
    public class Daten
    {
        string[] botlaneMe = new string[2];
        string[] botlaneEnemy = new string[2];
        string outcome;

        string path = @"C:\Users\Albert\source\repos\LeagueVorProgramm\Datenbank.txt";

        public void GetBotlanes()
        {
            Console.WriteLine("Gib bitte deinen Adc ein:");
            botlaneMe[0] = Console.ReadLine();
            Console.WriteLine("Gib bitte deinen Support ein:");
            botlaneMe[1] = Console.ReadLine();
            Console.WriteLine("Gib bitte den gegnerischen Adc ein:");
            botlaneEnemy[0] = Console.ReadLine();
            Console.WriteLine("Gib bitte den gegnerischen Support ein:");
            botlaneEnemy[1] = Console.ReadLine();
            Console.WriteLine("Wer hat gewonnen? ('Me' oder 'Enemy'):");
            outcome = Console.ReadLine();
        }

        public void PrintBotlanes()
        {
            Console.WriteLine("\nDeine Eingaben: \n----------------\n" + "Dein ADC: " + botlaneMe[0] + "\nDein SUP: "
            + botlaneMe[1] + "\nEnemy ADC: " + botlaneEnemy[0] + "\nEnemy SUP: " + botlaneEnemy[1] + "\nWinner: " +
            "" + outcome + "\n----------------");
        }

        public void DatenSpeichern()
        {
            if (File.Exists(path))
            {
                //create a file to write to.
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(botlaneMe[0]);
                    sw.WriteLine(botlaneMe[1]);
                    sw.WriteLine(botlaneEnemy[0]);
                    sw.WriteLine(botlaneEnemy[1]);
                    sw.WriteLine(outcome);
                }
            }
        }

        public void Statictic()
        {
            //wr = winratio
            double wr;

        }

        public void DatenLöschen()
        {
            if (File.Exists(path))
            {
                File.WriteAllText(path, String.Empty);
            }
        }
        public void DatenAnzeigen()
        {
            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                Console.WriteLine("Dateiinhalt:");
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
                string length = Convert.ToString(File.ReadAllLines(path).Length);
                Console.WriteLine("Anzahl an Daten:" + length);
            }
        }

        public int Decision(string message)
        {
            int decision = 0;
            Console.WriteLine(message);
            decision = Convert.ToInt32(Console.ReadLine());
            return decision;
        }
        // Für Später
        //-----------------------------------------------------------------------------------
        public void DatenInSQL()
        {
            string connectionString;
            string queryString;
            string ergebnis;
            connectionString = "Server=127.0.0.1;Uid=root;Pwd=Loth10rien;Database=leagueprogramm";
            queryString = "SELECT Adc1 FROM stats WHERE id=1";
            ergebnis = SQLConcect(queryString, connectionString);
            Console.WriteLine("Das Ergbenis aus der Datenbank sollte Xayah sein ist aber:\n" + ergebnis);
        }

        private static string SQLConcect(string queryString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string ergebnis;
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                Console.WriteLine(command.Connection.Database);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    ergebnis = reader.GetString(2);
                }
                return ergebnis;
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            string datenlöschen = "Alle vorhandenen Daten löschen? 1-Ja, 2-Nein:";
            Daten datensatz1 = new Daten();
            datensatz1.GetBotlanes();
            datensatz1.PrintBotlanes();
            datensatz1.DatenSpeichern();
            datensatz1.DatenAnzeigen();
            if (datensatz1.Decision(datenlöschen) == 1)
            {
                datensatz1.DatenLöschen();
                datensatz1.DatenAnzeigen();
            }

            //datensatz1.datenInSQL();
        }
    }
}
