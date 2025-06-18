using homework18._06.интерфейсы;
using homework18._06.наследники;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework18._06.файлы
{
    public class LegendaryProcessor : IDataProcessor<LegendaryArtifact>
    {
        public List<LegendaryArtifact> LoadData(string filePath)
        {
            List<LegendaryArtifact> artifacts = new List<LegendaryArtifact>();
            try
            {
                foreach (string line in File.ReadLines(filePath))
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 5)
                    {
                        try
                        {
                            artifacts.Add(new LegendaryArtifact
                            {
                                Name = parts[0].Trim(),
                                PowerLevel = int.Parse(parts[1].Trim()),
                                Rarity = Enum.Parse<Rarity>(parts[2].Trim()),
                                CurseDescription = parts[3].Trim(),
                                IsCursed = bool.Parse(parts[4].Trim())
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка при парсинге строки: {line}. Ошибка: {ex.Message}");
                        }

                    }
                    else
                    {
                        Console.WriteLine($"Неверный формат строки: {line}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке текстового файла: {ex.Message}");
            }
            return artifacts;
        }

        public void SaveData(List<LegendaryArtifact> data, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (LegendaryArtifact artifact in data)
                    {
                        writer.WriteLine($"{artifact.Name}|{artifact.PowerLevel}|{artifact.Rarity}|{artifact.CurseDescription}|{artifact.IsCursed}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении в текстовый файл: {ex.Message}");
            }
        }
    }
}
