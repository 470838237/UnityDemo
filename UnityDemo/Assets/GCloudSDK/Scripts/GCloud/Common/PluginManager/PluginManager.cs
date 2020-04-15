
using System;
using System.Collections.Generic;
namespace GCloud
{
    public class PluginManager
    {
        private PluginManager()
        {
        }

        private Dictionary<string, PluginBase> pluginCollection  = new Dictionary<string, PluginBase>();

        public static PluginManager Instance = new PluginManager();

        public void Add(PluginBase plugin)
        {
            if (plugin == null || string.IsNullOrEmpty(plugin.GetPluginName()))
            {
                throw new Exception("Plugin Name and plugin instance could not be empty or null");
            }

            string name = plugin.GetPluginName();
            if (pluginCollection.ContainsKey(name))
            {
                pluginCollection [name] = plugin;
            } else
            {
                pluginCollection.Add(name, plugin);
            }
        }

        public PluginBase GetPlugin(string name)
        {
            if (!string.IsNullOrEmpty(name) && pluginCollection.ContainsKey(name))
            {
                return pluginCollection[name];
            }
            return null;
        }

        public PluginBase GetCurrentPlugin()
        {
			string name = GCloudCommon.InitializeInfo.PluginName;
            return GetPlugin(name);
        }
             
   
    }
}

