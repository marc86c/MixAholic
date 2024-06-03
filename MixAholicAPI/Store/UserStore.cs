using MixAholicCommon.Model;
using System.Text.Json;

namespace MixAholicAPI.Store
{
    public class UserStore
    {
        private string filePath = "user.json";
        public List<User> Users { get; set; }

        public UserStore()
        {

            filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Data\user.json");
            var json = File.ReadAllText(filePath);
            if (!string.IsNullOrEmpty(json))
            {
                Users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            else
            {
                Users = new List<User>();
            }
        }

        public void SaveChanges()
        {
            var json = JsonSerializer.Serialize(Users);
            File.WriteAllText(filePath, json);
        }
    }
}
