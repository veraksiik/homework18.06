using homework18._06.интерфейсы;
using homework18._06.наследники;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace homework18._06.файлы
{ 
    public class JsonProcessor : IDataProcessor<ModernArtifact>
    {
        public List<ModernArtifact> LoadData(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                return DeserializeModernArtifacts(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке JSON: {ex.Message}");
                return new List<ModernArtifact>();
            }
        }

        public void SaveData(List<ModernArtifact> data, string filePath)
        {
            try
            {
                string json = SerializeModernArtifacts(data);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении JSON: {ex.Message}");
            }
        }

        private List<ModernArtifact> DeserializeModernArtifacts(string json)
        {
            List<ModernArtifact> artifacts = new List<ModernArtifact>();

            // Удаляем квадратные скобки в начале и конце, если они есть
            json = json.Trim().TrimStart('[').TrimEnd(']');

            // Разбиваем строку на отдельные объекты JSON
            string[] jsonObjects = json.Split(new string[] { "},{" }, StringSplitOptions.None);

            foreach (string jsonObject in jsonObjects)
            {
                // Разбиваем объект JSON на пары ключ-значение
                string[] keyValuePairs = jsonObject.Split(',');

                ModernArtifact artifact = new ModernArtifact();
                foreach (string keyValuePair in keyValuePairs)
                {
                    string[] parts = keyValuePair.Split(':');
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim().Trim('"');
                        string value = parts[1].Trim().Trim('"');

                        switch (key)
                        {
                            case "Id":
                                artifact.Id = int.Parse(value);
                                break;
                            case "Name":
                                artifact.Name = value;
                                break;
                            case "PowerLevel":
                                artifact.PowerLevel = int.Parse(value);
                                break;
                            case "Rarity":
                                if (Enum.TryParse<Rarity>(value, out Rarity rarity))
                                {
                                    artifact.Rarity = rarity;
                                }
                                else
                                {
                                    Console.WriteLine($"Не удалось распарсить Rarity: {value}");
                                }
                                break;
                            case "TechLevel":
                                artifact.TechLevel = double.Parse(value);
                                break;
                            case "Manufacturer":
                                artifact.Manufacturer = value;
                                break;
                        }
                    }
                }
                artifacts.Add(artifact);
            }
            return artifacts;
        }

        private string SerializeModernArtifacts(List<ModernArtifact> artifacts)
        {
            string json = "[";
            foreach (ModernArtifact artifact in artifacts)
            {
                json += "{";
                json += $"\"Id\": \"{artifact.Id}\",";
                json += $"\"Name\": \"{artifact.Name}\",";
                json += $"\"PowerLevel\": \"{artifact.PowerLevel}\",";
                json += $"\"Rarity\": \"{artifact.Rarity}\",";
                json += $"\"TechLevel\": \"{artifact.TechLevel}\",";
                json += $"\"Manufacturer\": \"{artifact.Manufacturer}\"";
                json += "},";
            }

            // Удаляем последнюю запятую, если есть элементы
            if (artifacts.Any())
            {
                json = json.TrimEnd(',');
            }
            json += "]";
            return json;
        }
    }
}
