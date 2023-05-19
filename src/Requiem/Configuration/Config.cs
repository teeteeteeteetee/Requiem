using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Windows;
using System.Windows.Shapes;
using Requiem.Templates;

namespace Requiem.Configuration
{
    public class Config
    {
        ConfigTemplate? config;
        string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "requiem.json");

        public Config()
        {
            Create();
            var json = File.ReadAllText(path);
            config = JsonSerializer.Deserialize<ConfigTemplate>(json);
        }

        public bool Validate()
        {
            // change to enum in future
            if (!File.Exists(config!.RiotPath))
            {
                return false;
            }

            return true;
        }

        private void Create()
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, JsonSerializer.Serialize(new ConfigTemplate(), new JsonSerializerOptions { WriteIndented = true }));
            }
        }
        private void Save()
        {
            Create();
            string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }

        public string GetPath()
        {
            return config!.RiotPath;
        }

        public void SetPath(string newPath)
        {
            config!.RiotPath = newPath;
            Save();
        }

        public void SetTray(bool newTray)
        {
            config!.IsTray = newTray;
            Save();
        }
        public void SetCloseExit(bool newCloseExit)
        {
            config!.IsCloseExit = newCloseExit;
            Save();
        }

        public void SetDetailed(bool newDetailed)
        {
            config!.IsDetailed = newDetailed;
            Save();
        }

        public void SetMultiLauncher(bool newMultiLauncher)
        {
            config!.IsMultiLauncher = newMultiLauncher;
            Save();
        }
    }
}
