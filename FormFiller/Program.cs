using System;
using System.Collections.Generic;
using System.ComponentModel;
using Coypu;
using Coypu.Drivers;
using Coypu.Drivers.Selenium;

namespace FormFiller {
    class Program {
        static void Main(string[] args) {
            // import data records from file
            var list = GetFakeData();

            var formUrl = "https://register.wyd.va/vol/wizard?lang=pl";

            var sessionConfiguration = new SessionConfiguration()
                {
                AppHost = "register.wyd.va/",
                Browser = Browser.Chrome,
                SSL = true,
                Driver = typeof(SeleniumWebDriver),
                ConsiderInvisibleElements = true
                };

            using (var session = new BrowserSession(sessionConfiguration)) {
                foreach (var ewangelizator in list) {
                    session.Visit("vol/wizard?lang=pl");
                    //session.Select("Ksiądz / Ojciec").From("reg.title");
                    session.FillIn("firstName").With(ewangelizator.Imie);
                    session.FillIn("lastName").With(ewangelizator.Nazwisko);

                    // wait for input on the console to process the next person
                    Console.ReadKey();
                }
            }
        }

        private static IList<Ewangelizator> GetFakeData() {
            return new List<Ewangelizator>()
                {
                new Ewangelizator() { Status = "OK", Wplata = "500", Imie = "Mateusz", Nazwisko = "Ewangeli" },
                new Ewangelizator() { Status = "OK", Wplata = "300", Imie = "Bogdan", Nazwisko = "Kyrzułło" },
                new Ewangelizator() { Status = "OK", Wplata = "400", Imie = "Kasia", Nazwisko = "Bornert" },
                };
        }
    }

    internal class Ewangelizator {
        [Description("reg.title")]
        public string Status { get; set; }

        public string Wplata { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
    }
}