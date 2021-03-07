    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EDIVisualizer.Interfaces
{
    public enum Type { Norme, None, Report, Session }
    public interface IPlugin
    {
        string Name { get; }
        string Comment { get; }
        string Extension { get; }
        string ExtensionFilter { get; }
        Type PluginType { get; }
        int Index { get; }
        bool SearchInParent { get; }

        void LoadFile(string fileName);
        void LoadSession(string sessionNumber, string environnement);
        void reset();
        bool Autodect(string fileName);
        bool Search(string searchString);
    }
}
