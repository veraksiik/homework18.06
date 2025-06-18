using homework18._06.интерфейсы;
using homework18._06.наследники;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace homework18._06.файлы
{
    public class XmlProcessor : IDataProcessor<AntiqueArtifact>
    {
        public List<AntiqueArtifact> LoadData(string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<AntiqueArtifact>));  
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    return (List<AntiqueArtifact>)serializer.Deserialize(fileStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке XML: {ex.Message}");
                return new List<AntiqueArtifact>();
            }
        }

        public void SaveData(List<AntiqueArtifact> data, string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<AntiqueArtifact>));
                using (TextWriter writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении XML: {ex.Message}");
            }
        }
    }
}
