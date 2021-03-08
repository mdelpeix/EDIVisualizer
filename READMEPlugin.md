How to create a new plugin for EDIVisualizer :

- Create main namespace "PluginApp"
- Create main public class "PluginClass"
- References EDIVisualizerInterfaces library
- inherit IPlugin interface in PluginClass
- Interface description :
    - Properties
        - public string Comment : plugin description, comments
        - public string Extension : plugin file extension (ex:"tpl")
        - public string ExtensionFilter : file extension description (ex:"Template files|*.tpl")
        - public EDIVisualizer.Interfaces.Type PluginType :
            - Plugin type enumeration ->
                - Norme : plugin that manage standard (Edifact, X12, Vda...)
                - Session : plugin that manage loading file (used by PluginSession)
                - None : Other plugin category
        - public bool SearchInParent : Indicate if plugin manage search in loading file
        - public int Index : order of the plugin in container tab control
    - Functions
        - public void LoadFile(string fileName) : main function for manage loading file (input fileName parameter)
        - public void reset() : Clean control function when it raise by the parent form
        - public bool Autodect(string fileName) : Function that recognise the file is in the right format with regular expression for example. Return true if the input fileName parameter is recognize.
        - public bool Search(string searchString) : search function. Return true for a success match (input searchString parameter)
        - public void LoadSession(string sessionNumber, string environnement) : not used in public version of EDIVisualizer
- Compile and put the dll in the "Plugins" directory beside EDIVisualizer.exe


Class example :

```
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EDIVisualizer.Interfaces;

namespace PluginApp
{
    public partial class PluginClass : UserControl, IPlugin
    {
        public PluginClass()
        {
            InitializeComponent();
            this.Name = "Template plugin";
        }


        public string Comment
        {
            get { return string.Empty; }
        }

        public string Extension
        {
            get { return "tpl".ToLower(); }
        }

        public string ExtensionFilter
        {
            get { return "Template files|*.tpl"; }
        }

        public EDIVisualizer.Interfaces.Type PluginType
        {
            get { return EDIVisualizer.Interfaces.Type.None; }
        }

        public bool SearchInParent
        {
            get { return false; }
        }

        public void LoadFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public void reset()
        {
            throw new NotImplementedException();
        }

        public bool Autodect(string fileName)
        {
            throw new NotImplementedException();
        }

        public bool Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public int Index
        {
            get { return 0; }
        }

        public void LoadSession(string sessionNumber, string environnement)
        {
            throw new NotImplementedException();
        }
    }
}
```
