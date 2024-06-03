using MixAholicCommon.Model;
using System.Text.Json;

namespace MixAholicAPI.Store
{
    public class MixStore
    {
        private string filePath = "mix.json";
        public List<Mix> Mixes{ get; set; }

        public MixStore()
        {

            filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Data\mix.json");
            var json = File.ReadAllText(filePath);
            if (!string.IsNullOrEmpty(json))
            {
                Mixes = JsonSerializer.Deserialize<List<Mix>>(json) ?? new List<Mix>();
            }
            else
            {
                Mixes = new List<Mix>();
            }
        }

        public void SaveChanges()
        {
            var json = JsonSerializer.Serialize(Mixes);
            File.WriteAllText(filePath, json);
        }
    }
}
