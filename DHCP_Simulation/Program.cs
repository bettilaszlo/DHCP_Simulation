using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHCP_Simulation
{
    class Program
    {
        static List<string> excluded = new List<string>();
        static Dictionary<string, string> dhcp = new Dictionary<string, string>();
        static Dictionary<string, string> reserved = new Dictionary<string, string>();
        static void BeolvasExcluded()
        {
            try
            {
                StreamReader file = new StreamReader("excluded.csv");
                try
                {
                    while (!file.EndOfStream)
                    {
                        excluded.Add(file.ReadLine());
                    }

                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
                finally
                {
                    file.Close();
                }
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void BeolvasDictionary(Dictionary<string, string> d, string filenev)
        {
            try
            {
                StreamReader file = new StreamReader(filenev);
                while (!file.EndOfStream)
                {
                    string[] adatok = file.ReadLine().Split(';');
                    d.Add(adatok[0], adatok[1]);
                }
                file.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static string CimEgyenlNo(string cim)
        {
            /*
             * cim = "192.168.10.100"
             * return "192.168.10.101"
             * 
             * Szétvágni a '.' mentén
             * Az utolsót int-é konvertálni és
             * egyet hozzáadni (255-öt ne lépjül túl)
             * 
             * Összefűzni string-é
             */

            cim = "192.168.10.100";

            string[] adatok = cim.Split('.');
            int okt4 = Convert.ToInt32(adatok[3]);
            if (okt4 < 255)
            {
                okt4++;
            }
            return adatok[0] + '.' + adatok[1] + '.' + adatok[2] + '.' + Convert.ToString(okt4);
        }
        static void Main(string[] args)
        {
            BeolvasExcluded();
            BeolvasDictionary(dhcp, "dhcp.csv");
            BeolvasDictionary(reserved, "reserved.csv");

            foreach (var e in reserved)
            {
                Console.WriteLine(e);
            }
            
            Console.ReadLine();
        }
    }
}
