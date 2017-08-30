using Experiment.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiment
{
    class Program
    {
        static void Main(string[] args)
        {
            var u = new Utils();

            //Create a new destination (blank for skip)
            Console.Write("Crate a new destination (press enter to skip): ");
            var dest = Console.ReadLine();
            if (!dest.Equals(""))
            {
                Console.Write("Enter path for " + dest + ": ");
                var destpath = Console.ReadLine();
                var destination = u.CreateDestination(dest, destpath);
            }

            //Define a Sepo property( blank for skip)
            Console.Write("Define a new SEPO property. Write Name (press enter to skip): ");
            var propName = Console.ReadLine();
            if (!propName.Equals(""))
            {
                //List lige mulige tabeller for brugeren!
                Console.Write("Enter source table name for " + propName + ": ");
                var propTable = Console.ReadLine();
                var propId = u.CreatePropertyDefinition(propName, propTable);

                //map Sepo Property to Destination
                Console.WriteLine("Possible destinations: ");
                using (var db = new Experiment_DbContext())
                {
                    var dests = db.Destinations.ToList();
                    foreach (var item in dests)
                    {
                        Console.WriteLine(item.destinationId.ToString() + " - " + item.destinationName);
                    }
                }
                Console.Write("Map " + propName + " to a destination by typing the Id (press enter to skip): ");
                var destIdStr = Console.ReadLine();
                if (!destIdStr.Equals(""))
                {
                    Console.Write("Select Value format for " + propName + ": ");
                    Console.Write(ValueFormat.single.ToString());
                    Console.Write(ValueFormat.file.ToString());
                    Console.Write(ValueFormat.csv.ToString());
                    var valFormatStr = Console.ReadLine();
                    int valformatInt;
                    ValueFormat valFormat = 0;
                    if (Int32.TryParse(valFormatStr, out valformatInt))
                        valFormat = (ValueFormat)valformatInt;

                    int destId;
                    if (Int32.TryParse(destIdStr, out destId))
                    {
                        u.MapProp2Destination(propId, destId, valFormat);
                    }
                }
            }

            //Console.Write("Enter value for " + propName + ": ");
            //var pd = new Model.PropertyDefinition();
            //TODO: List lige names fra definitions
            Console.Write("Create a new pair in KeyValueProperties (press enter to skip): ");
            var key = Console.ReadLine();
            if (!key.Equals(""))
            {
                Console.Write("Enter value for " + key + ": ");
                var value = Console.ReadLine();
                u.CreateKeyValue(key, value);
            }

            Console.WriteLine();
            Console.WriteLine("Listing Destinations");

            using (var db = new Experiment_DbContext())
            {
                var dests = db.Destinations.Where(f => f.destinationId>=0).ToList();
                foreach (var item in dests)
                {
                    u.PrintDestination(item);
                }
            }
            Console.WriteLine("Quit?");
            var dum = Console.ReadLine();
        }
    }


    public class Utils
    {
        public void PrintDestination(Destination destination)
        {
            using (var db = new Experiment_DbContext())
            {
                Console.WriteLine("Destination name:" + destination.destinationName);
                Console.WriteLine("Destination path:" + destination.destinationPath);
                Console.WriteLine("Destination type:" + destination.destinationType.ToString());
                Console.WriteLine("-----------------");
                foreach (var propdef in db.SamToSepoMap
                                    .Where(f => f.SepoProperty.destinationId.Equals(destination.destinationId))
                                    .Select(f => f.PropertyDefinition)
                                    .ToList())
                {
                    switch (propdef.valueTable)
                    {
                        case "KeyValueProperties":
                            if (propdef.)
                            {

                            }
                            using (var dbConfig = new Experiment_config_DbContext())
                            {
                                foreach (var kvProp in dbConfig.KeyValueProperties.Where(f => f.key.Equals(propdef.name)).ToList())
                                {
                                    //noget med at ordne dem efter propid og concatenere alle med csv
                                    string csv = string.Join(",", kvProp);//det er ikke korrekt, men der hen af .GroupBy(f => f.key)
                                    Console.WriteLine("Property line: " + kvProp.key + "=" + kvProp.value);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine("-----------------");
                Console.WriteLine("");
            }
        }

        public Destination CreateDestination(string name, string path)
        {
            var destination = new Destination();
            destination.destinationName = name;
            destination.destinationPath = path;
            destination.destinationType = Model.DestinationType.KeyValueFile;
            using (var db = new Experiment_DbContext())
            {
                db.Destinations.Add(destination);
                db.SaveChanges();
            }
            return destination;
        }

        public int CreatePropertyDefinition(string name, string valueType)
        {
            var pd = new PropertyDefinition();
            pd.name = name;
            pd.valueTable = valueType;
            using (var db = new Experiment_DbContext())
            {
                db.PropertyDefinitions.Add(pd);
                var res = db.SaveChanges();
            }
            return pd.id;
        }
        public void MapProp2Destination(int propertyId,int destinationId, ValueFormat valueFormat)
        {
            //TODO: tjek lige om de findes først
            var sepoProp = new SepoProperty();
            var s2sMap = new SamToSepoMapping();
            sepoProp.destinationId = destinationId;
            sepoProp.valueFormat = valueFormat;
            using (var db = new Experiment_DbContext())
            {
                db.SepoProperties.Add(sepoProp);
                db.SaveChanges();

                s2sMap.propId = propertyId;
                s2sMap.sepoPropId = sepoProp.sepoPropId;
                db.SamToSepoMap.Add(s2sMap);
                db.SaveChanges();
            }
        }

        public void CreateKeyValue(string key,string value)
        {
            //TODO: Tjek lige om key findes i definitions
            var pv = new KeyValueProperty();
            pv.key = key;
            pv.value = value;
            using (var db_config = new Experiment_config_DbContext())
            {
                db_config.KeyValueProperties.Add(pv);
                var res = db_config.SaveChanges();
            }
        }
    }
}
