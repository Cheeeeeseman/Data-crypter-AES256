using System.IO.Ports;
using System.Security.Policy;
using System.Text.Json;

namespace swBootloader
{
    public static class Configurator
    {
        public static Settings settings;

        /** @brief Функция загрузки конфига в json file*/
        public static void SaveSettingsToJSON(string path)
        {
            string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { IncludeFields = true, });
            File.WriteAllText(path, json);
        }

        /** @brief Функция загрузки конфига из json фала */
        public static void LoadSettingsFromJSON(string path)
        {
            string Path = path;
            string jsonString = File.ReadAllText(path);
            try
            {
                settings = JsonSerializer.Deserialize<Settings>(jsonString, new JsonSerializerOptions { IncludeFields = true, });
            } catch
            {
                SettingsSetDefaultValues();
            }
            
        }

        /** @brief Установка дефолтных значений настроек */
        private static void SettingsSetDefaultValues()
        {
            settings.sourceFileName = "C://";
        }
    }
}
