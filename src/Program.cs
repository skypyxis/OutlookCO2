using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutlookCO2
{
    class Program
    {
        const string dataSource1 = "https://www.co2.earth/daily-co2";
        const string dataSource2 = "ftp://aftp.cmdl.noaa.gov/products/trends/co2/co2_weekly_mlo.txt";

        static void Main(string[] args)
        {
            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            if (version.EndsWith(".0")) version = version.Substring(0, version.Length - 2);

            Console.WriteLine($"Outlook Signature CO2 Levels by Pedro Fortes v{version}\n");

            try
            {
                var data = GetData().Result;

                var folder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft\\Signatures");
                new FileContentReplacer().Replace(folder, data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task<Dictionary<string, string>> GetData()
        {
            var webDataExtractor = new WebDataExtrator();

            var data = new Dictionary<string, string>();

            Console.WriteLine($"Getting data from {dataSource1}");
            var dataDaily = await webDataExtractor.GetDataFromHttp(
                dataSource1,
                @"(?s)Daily CO.*?([\d\.]*)\sppm.*?([\d\.]*)\sppm");
            data.Add("CO2-D", dataDaily.Length > 1 ? dataDaily[1] : null);
            data.Add("CO2-D-1Y", dataDaily.Length > 2 ? dataDaily[2] : null);

            Console.WriteLine($"Getting data from {dataSource2}");
            /* #      Start of week      CO2 molfrac           (-999.99 = no data)  increase
               # (yr, mon, day, decimal)    (ppm)  #days       1 yr ago  10 yr ago  since 1800 */
            var dataWeekly = await webDataExtractor.GetDataFromFtp(
                dataSource2,
                @"(?si)^.*\n(?:\s+(\d+)\s+(\d+)\s+(\d+)\s+([\d\.]+)\s+([\d\.]+)\s+([\d\.]+)\s+([-\d\.]+)\s+([-\d\.]+)\s+([-\d\.]+).*)\n?$");
            data.Add("CO2-W-DATE", dataWeekly.Length > 3 ? $"{dataWeekly[1]}-{dataWeekly[2].PadLeft(2, '0')}-{dataWeekly[3].PadLeft(2, '0')}" : null);
            data.Add("CO2-W", dataWeekly.Length > 5 ? dataWeekly[5] : null);
            data.Add("CO2-W-1Y", dataWeekly.Length > 7 ? dataWeekly[7] : null);
            data.Add("CO2-W-10Y", dataWeekly.Length > 8 ? dataWeekly[8] : null);
            data.Add("CO2-W-V1800", dataWeekly.Length > 9 ? dataWeekly[9] : null);

            foreach (var item in data) Console.WriteLine($"{item.Key,12}: {item.Value}");
            return data;
        }
    }
}
