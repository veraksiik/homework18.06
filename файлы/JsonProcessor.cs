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
                return JsonConvert.DeserializeObject<List<ModernArtifact>>(json);
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
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении JSON: {ex.Message}");
            }
        }
    }
}
