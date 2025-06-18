using homework18._06.наследники;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace homework18._06
{
    public class ShopManager
    {
        public List<Artifact> Artifacts { get; set; } = new List<Artifact>();

        public void LoadAllData(string xmlPath, string jsonPath, string txtPath)
        {
            XmlProcessor xmlProcessor = new XmlProcessor();
            JsonProcessor jsonProcessor = new JsonProcessor();
            LegendaryProcessor legendaryProcessor = new LegendaryProcessor();

            Artifacts.AddRange(xmlProcessor.LoadData(xmlPath));
            Artifacts.AddRange(jsonProcessor.LoadData(jsonPath));
            Artifacts.AddRange(legendaryProcessor.LoadData(txtPath));
        }

        public void GenerateReport(string reportPath)
        {
            var rarityGroups = Artifacts.GroupBy(a => a.Rarity)
                                      .Select(g => new
                                      {
                                          Rarity = g.Key,
                                          AveragePower = g.Average(a => a.PowerLevel),
                                          Count = g.Count()
                                      });

            try
            {
                using (StreamWriter writer = new StreamWriter(reportPath))
                {
                    writer.WriteLine("Статистика по редкости:");
                    foreach (var group in rarityGroups)
                    {
                        writer.WriteLine($"{group.Rarity}: Средняя сила = {group.AveragePower:F2}, Количество = {group.Count}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании отчета: {ex.Message}");
            }
        }

        public List<LegendaryArtifact> FindCursedArtifacts()
        {
            return Artifacts.OfType<LegendaryArtifact>()
                           .Where(a => a.IsCursed && a.PowerLevel > 50)
                           .ToList();
        }

        public IGrouping<Rarity, Artifact>[] GroupByRarity()
        {
            return Artifacts.GroupBy(a => a.Rarity).ToArray();
        }

        public List<Artifact> TopByPower(int count)
        {
            return Artifacts.OrderByDescending(a => a.PowerLevel)
                           .Take(count)
                           .ToList();
        }
    }
}
