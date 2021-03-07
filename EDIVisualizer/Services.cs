using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDIVisualizer.Interfaces;

namespace EDIVisualizer
{
    class Services
    {
        internal static string GetExtensions(List<IPlugin> plugins)
        {
            string extensions = "All files|*.*|Text files|*.txt|";
            foreach (IPlugin plugin in plugins)
                if (plugin.PluginType == EDIVisualizer.Interfaces.Type.Norme)
                    extensions += string.Format("{0}|", plugin.ExtensionFilter);
            return extensions.Substring(0, extensions.Length - 1);
        }

        internal static EDIVisualizer.Interfaces.IPlugin GetPlugins(string fileName)
        {

            throw new NotImplementedException();
        }

        internal static IPlugin GetPlugins(List<IPlugin> plugins, string pluginName)
        {
            IPlugin selectedPlugin = null;
            foreach (IPlugin plugin in plugins)
                if (plugin.Name == pluginName)
                    selectedPlugin = plugin;
            return selectedPlugin;
        }
    }
}
