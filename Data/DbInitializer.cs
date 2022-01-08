using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moglan_Vlad_ProiectEB.Models;

namespace Moglan_Vlad_ProiectEB.Data
{
    public class DbInitializer
    {
        public static void Initialize(CarServiceContext context)
        {
            context.Database.EnsureCreated();
            if (context.Services.Any())
            {
                return; // BD a fost creata anterior
            }
            var services = new Service[]
            {
                 new Service{Title="Inlocuire gume iarna",Price=Decimal.Parse("50")},
                 new Service{Title="Vopsire element",Price=Decimal.Parse("250")},
                 new Service{Title="Reparatie cutie viteze",Price=Decimal.Parse("800")}
            };
            foreach (Service s in services)
            {
                context.Services.Add(s);
            }
            context.SaveChanges();
            var clients = new Client[]
            {

                 new Client{ClientID=001,Name="Popescu Ion",PhoneNumber="0711111111"},
                 new Client{ClientID=002,Name="Munteanu Dorinel",PhoneNumber="0722222222"},

             };
            foreach (Client c in clients)
            {
                context.Clients.Add(c);
            }
            context.SaveChanges();
            var appointments = new Appointment[]
            {
                 new Appointment{ServiceID=1,ClientID=001},
                 new Appointment{ServiceID=3,ClientID=001},
                 new Appointment{ServiceID=1,ClientID=002},
                 new Appointment{ServiceID=2,ClientID=002},
            };
            foreach (Appointment e in appointments)
            {
                context.Appointments.Add(e);
            }
            context.SaveChanges();
            var locations = new Location[]
            {

                 new Location{LocationName="Tired Tires",Adress="Str. Fagului, nr. 40, Cluj-Napoca"},
                 new Location{LocationName="Colored Stain",Adress="Str. Lalelelor, nr. 35, Deva"},
            };
            foreach (Location l in locations)
            {
                context.Locations.Add(l);
            }
            context.SaveChanges();
            var providedservices = new ProvidedService[]
            {
                 new ProvidedService { ServiceID = services.Single(c => c.Title == "Inlocuire gume iarna" ).ID, LocationID = locations.Single(i => i.LocationName =="Tired Tires").ID },
                 new ProvidedService { ServiceID = services.Single(c => c.Title == "Vopsire element" ).ID, LocationID = locations.Single(i => i.LocationName =="Colored Stain").ID },
                 new ProvidedService { ServiceID = services.Single(c => c.Title == "Reparatie cutie viteze" ).ID, LocationID = locations.Single(i => i.LocationName == "Colored Stain").ID },
                 new ProvidedService { ServiceID = services.Single(c => c.Title == "Inlocuire gume iarna" ).ID, LocationID = locations.Single(i => i.LocationName == "Tired Tires").ID },
                 new ProvidedService { ServiceID = services.Single(c => c.Title == "Vopsire element" ).ID, LocationID = locations.Single(i => i.LocationName == "Colored Stain").ID },
                 new ProvidedService { ServiceID = services.Single(c => c.Title == "Inlocuire gume iarna" ).ID, LocationID = locations.Single(i => i.LocationName == "Tired Tires").ID },
            };
            foreach (ProvidedService ps in providedservices)
            {
                context.ProvidedServices.Add(ps);
            }
            context.SaveChanges();
        }
    }
}


