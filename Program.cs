using homework18._06.интерфейсы;
using homework18._06.наследники;
using System.Xml.Serialization;
using System.Xml;
using homework18._06.файлы;
using System;
using System.Collections.Generic;
using System.Linq;

namespace homework18._06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
                ShopManager shopManager = new ShopManager();

                string xmlFile = "antique.xml";
                string jsonFile = "modern.json";
                string txtFile = "legends.txt";
                string reportFile = "report.txt";
                string cursedFile = "cursed.json";
                string topFile = "top.xml";

                shopManager.LoadAllData(xmlFile, jsonFile, txtFile);

                Console.WriteLine($"Всего артефактов загружено: {shopManager.Artifacts.Count}");

                shopManager.GenerateReport(reportFile);
                Console.WriteLine($"Отчет создан: {reportFile}");


                List<LegendaryArtifact> cursed = shopManager.FindCursedArtifacts();
                Console.WriteLine($"Проклятых артефактов с силой > 50: {cursed.Count}");
                if (cursed.Any())
                {
                    JsonProcessor cursedProcessor = new JsonProcessor();  
                    cursedProcessor.SaveData(cursed.ConvertAll(x => new ModernArtifact
                    { 
                        Id = x.Id,
                        Name = x.Name,
                        PowerLevel = x.PowerLevel,
                        Rarity = x.Rarity,
                        Manufacturer = x.CurseDescription,
                        TechLevel = x.IsCursed ? 1 : 0,  
                    }), cursedFile);
                }

                List<Artifact> top = shopManager.TopByPower(5);
                Console.WriteLine($"Топ 5 артефактов по силе: {string.Join(", ", top.Select(x => x.Name))}");
                if (top.Any())
                {
                    XmlProcessor topProcessor = new XmlProcessor();
                    topProcessor.SaveData(top.ConvertAll(x => new AntiqueArtifact
                    {
                        Id = x.Id,
                        Name = x.Name,
                        PowerLevel = x.PowerLevel,
                        Rarity = x.Rarity,
                        Age = 100,
                        OriginRealm = "Unknown",
                    }), topFile); 
                }


                Console.ReadKey();
            }
        }



}