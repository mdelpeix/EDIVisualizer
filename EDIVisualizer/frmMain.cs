using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EDIVisualizer.Interfaces;
using System.IO;
using Microsoft.VisualBasic;
using System.Security;

namespace EDIVisualizer
{
    public partial class frmMain : Form, ILoadEDIFile
    {

        #region Variables

        //Identifiants Sharepoint (PluginSynchrolink)
        private Classes.Settings sts;
        //Utilisé pour coloriser les headers du tab
        private Dictionary<TabPage, Color> TabColors = new Dictionary<TabPage, Color>();
        private string ediTag;
        public string EDITag
        {
            get
            {
                return ediTag.Trim();
            }
            set
            {
                ediTag = value.Trim();
                if (ediTag.Trim() != string.Empty && File.Exists(ediTag))
                {
                    IPlugin plg = ((IPlugin)((System.Windows.Forms.TabControl)this.Controls[0]).TabPages["Synchrolink"].Controls[0]);
                    if (plg != null)
                        plg.LoadFile(ediTag);
                }
            }
        }
        private string ediEnvironnement;
        public string EDIEnvironnement
        {
            get
            {
                return ediEnvironnement.Trim();
            }
            set
            {
                ediEnvironnement = value.Trim();
                tsslEnvironnement.Text = string.Format("Environnement {0}", value.Trim());
            }
        }
        private string ediTrace;
        public string EDITrace
        {
            get
            {
                return ediTrace.Trim();
            }
            set
            {
                ediTrace = value.Trim();
            }
        }
        private string ediSession;
        public string EDISession
        {
            get { return ediSession.Trim(); }
            set
            {
                ediSession = value.Trim();
                tsslSessionNumber.Text = string.Format("Session n°{0}", value.Trim());
            }
        }
        private string ediMapping;
        public string EDIMapping
        {
            get 
            {
                if (ediMapping != null)
                    return ediMapping.Trim();
                else
                    return string.Empty;
            }
            set
            {
                string mn = value.Split('|')[0];
                string me = value.Split('|')[1];
                ediMapping = mn;
                tsslMappingName.Text = string.Format("Mapping {0}", mn);
                tsslMappingName.Tag = me;
                if (ediMapping.Trim() != string.Empty)
                {
                    IPlugin plg = ((IPlugin)((System.Windows.Forms.TabControl)this.Controls[0]).TabPages["Mapping"].Controls[0]);
                    if (plg != null)
                    {
                        string fullMappingPath = Path.Combine(me, mn);
                        plg.LoadFile(fullMappingPath);
                    }
                }
            }
        }
        public bool isCommandLine { get; set; }
        public string SPLogin
        {
            get
            {
                if (sts != null)
                    return sts.SPLogin;
                else
                    return string.Empty;
            }
        }
        public SecureString SPPassword
        {
            get
            {
                if (sts != null)
                    return sts.SPMdp;
                else
                    return new SecureString();
            }
        }
        List<IPlugin> plugins;
        IPlugin pluginSelected;
        frmMessage frm;

        #endregion

        #region UI

        public frmMain()
        {
            InitializeComponent();
            //Identifiants Sharepoint (PluginSynchrolink)
            sts = new Classes.Settings();
            isCommandLine = false;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            //Source : http://stackoverflow.com/questions/92540/save-and-restore-form-position-and-size
            // restore location and size of the form on the desktop
            this.DesktopBounds = new Rectangle(Properties.Settings.Default.Location, Properties.Settings.Default.Size);
            // restore form's window state
            this.WindowState = Properties.Settings.Default.WindowState;
            this.Text = string.Format("{0} v{1}", "EDIVisualizer", "2.0.5");


            try
            {

                LoadingScreen();

                frm.Message = "Load plugins...";

                //Load plugins
                DirectoryInfo di = new DirectoryInfo(Path.Combine(Application.StartupPath, "Plugins"));
                plugins = new List<IPlugin>();

                foreach (FileInfo dll in di.GetFiles("*.dll"))
                {
                    if (dll.Name != "EDIVisualizerInterfaces.dll" && dll.Name != "EDIVisualizerClasses.dll" && dll.Name != "IBM.Data.DB2.iSeries.dll")
                    {
                        try
                        {
                            IPlugin plug = (IPlugin)AppDomain.CurrentDomain.CreateInstanceFromAndUnwrap(dll.FullName, "PluginApp.PluginClass");
                            plugins.Add(plug);
                        }
                        catch (Exception ex)
                        {
                            DefaultOption();
                            MessageBox.Show(string.Format("{0} : {1}", dll.Name, ex.InnerException.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }


                //Sort plugin
                var pluginsSort = plugins.OrderBy(m => m.Index).ThenBy(m => m.Name);
                //Create plugins tabs
                CreateTab(pluginsSort);


                ManageTagSession(string.Empty);


                frm.Message = "Load arguments...";

                //Read command line arguments
                string[] args = Environment.GetCommandLineArgs();
                if (args.Length > 1)
                {

                    isCommandLine = true;
                    //determine which kind of args (file path or session number)
                    string argmt01 = args[1].Trim();

                    if (args.Length == 3 && detectArgsSessionNumber(argmt01))
                    {

                        string argmt02 = args[2].Trim();

                        ediSession = argmt01;
                        ediEnvironnement = argmt02;

                        frm.Message = string.Format("Load Session n°{0} on {1}...", argmt01, argmt02);

                        this.EDISession = argmt01;
                        List<IPlugin> plugs = ListPluginSession();
                        foreach (IPlugin plugin in plugs)
                            plugin.LoadSession(argmt01, argmt02);

                    }
                    else
                    {

                        if (File.Exists(argmt01))
                        {

                            string extension = argmt01.Substring(argmt01.LastIndexOf('.') + 1, 3).ToLower();

                            var plugin2LoadColl = from plg in plugins
                                                  where plg.PluginType == EDIVisualizer.Interfaces.Type.Norme && plg.Extension == extension
                                                  select plg;


                            if (plugin2LoadColl.Count() > 0)
                                pluginSelected = plugin2LoadColl.First();
                            else
                                AutoDetect(argmt01);

                            if (pluginSelected != null)
                            {
                                frm.Message = string.Format("Load {0} plugin...", pluginSelected.Name);
                                pluginSelected.LoadFile(argmt01);
                                tabControl.SelectedTab = tabControl.TabPages[pluginSelected.Name];
                                ManageTagSession(argmt01);
                                tsslFilePath.Text = argmt01;
                            }
                            else
                                throw new Exception("None plugin can load for the input file");

                        }
                        else
                            throw new Exception(string.Format("Incorrect argument in the command line : {0}", argmt01));

                    }


                }

            }
            catch (Exception ex)
            {
                DefaultOption();
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DefaultOption();
            }

        }

        private void CreateTab(IOrderedEnumerable<IPlugin> pluginsSort)
        {
            foreach (IPlugin plugin in pluginsSort)
            {
                UserControl ctrl = (UserControl)(plugin);
                TabPage tp = new TabPage(ctrl.Name);
                switch (plugin.PluginType)
                {
                    case EDIVisualizer.Interfaces.Type.Norme:
                        SetTabHeader(tp, SystemColors.Control);
                        break;
                    case EDIVisualizer.Interfaces.Type.None:
                        SetTabHeader(tp, Color.LightBlue);
                        break;
                    case EDIVisualizer.Interfaces.Type.Report:
                        SetTabHeader(tp, Color.LightBlue);
                        break;
                    case EDIVisualizer.Interfaces.Type.Session:
                        SetTabHeader(tp, Color.White);
                        break;
                    default:
                        break;
                }
                tp.Name = plugin.Name;
                tp.Controls.Add(ctrl);
                tabControl.TabPages.Add(tp);
                ctrl.Dock = DockStyle.Fill;
            }
        }

        private bool detectArgsSessionNumber(string p)
        {
            int n;
            return int.TryParse(p, out n);
        }

        private void MenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
                Properties.Settings.Default.Location = bounds.Location;
                Properties.Settings.Default.Size = bounds.Size;
                Properties.Settings.Default.WindowState = this.WindowState;
                // persist location ,size and window state of the form on the desktop
                Properties.Settings.Default.Save();
            }
            catch (Exception)
            { }

        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            tsslFilePath.Text = string.Empty;
            try
            {
                ofd.Filter = Services.GetExtensions(plugins);
                if (ofd.ShowDialog() == DialogResult.OK)
                    EDILoadFile(ofd.FileName);
            }
            catch (Exception ex)
            {
                DefaultOption();
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DefaultOption();
            }

        }

        private void searchToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            searchToolStripButton1_Click(sender, e);
        }

        private void searchToolStripButton1_Click(object sender, EventArgs e)
        {
            if (pluginSelected != null)
                if (pluginSelected.SearchInParent)
                    this.search(Interaction.InputBox("Search :", this.Text, string.Empty));
                else
                    if (!pluginSelected.Search(string.Empty))
                        MessageBox.Show("Search returns no results", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            TabPage current = (sender as TabControl).SelectedTab;
            pluginSelected = pluginFromName(current.Text.Replace("*", string.Empty).Trim());
            if (current.Tag != null)
                tsslFilePath.Text = current.Tag.ToString();
            else
                tsslFilePath.Text = string.Empty;
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetToolStripButton_Click(sender, e);
        }

        private void resetToolStripButton_Click(object sender, EventArgs e)
        {

            try
            {
                LoadingScreen();
                frm.Message = "Reset...";
                EDIReset();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DefaultOption();
            }

        }

        private void frmMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void frmMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files) EDILoadFile(file);
        }

        private void openInFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFolderToolStripButton_Click(sender, e);
        }

        private void openFolderToolStripButton_Click(object sender, EventArgs e)
        {
            if (tsslFilePath.Text != string.Empty && Directory.Exists(Path.GetDirectoryName(tsslFilePath.Text)))
                System.Diagnostics.Process.Start("explorer.exe", Path.GetDirectoryName(tsslFilePath.Text));
            else if (true)
            {
                IPlugin p = pluginFromName("Session");
                if (p != null)
                {
                    string temp = p.GetType().GetProperty("ImportFolder").GetValue(p, null).ToString();
                    if (Directory.Exists(temp))
                        System.Diagnostics.Process.Start("explorer.exe", temp);
                }
            }
        }

        private void openFileToolStripButton_Click(object sender, EventArgs e)
        {
            openFileToolStripMenuItem_Click(null, null);
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tsslFilePath.Text != string.Empty && File.Exists(tsslFilePath.Text))
                System.Diagnostics.Process.Start(tsslFilePath.Text);
        }

        private void addToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ofd.Filter = "*.exe|exe files";
            if (ofd.ShowDialog() == DialogResult.OK)
                EDILoadFile(ofd.FileName);
        }

        private void toolStripStatusLabel1_TextChanged(object sender, EventArgs e)
        {
            this.openFileToolStripMenuItem.Enabled = !string.IsNullOrWhiteSpace(this.tsslFilePath.Text.Trim());
            this.openFileToolStripButton.Enabled = !string.IsNullOrWhiteSpace(this.tsslFilePath.Text.Trim());
        }

        #endregion

        #region Functions

        private void AutoDetect(string fileName)
        {
            pluginSelected = null;
            foreach (IPlugin plug in plugins)
            {
                if (plug.Autodect(fileName))
                {
                    pluginSelected = plug;
                    break;
                }
            }
            if (pluginSelected != null)
                tabControl.SelectedTab = tabControl.TabPages[pluginSelected.Name];
        }

        public void EDILoadFile(string fileName)
        {

            try
            {
                LoadingScreen();

                frm.Message = "Plugins autodetection...";
                AutoDetect(fileName);

                if (pluginSelected != null)
                {
                    frm.Message = "Loading file...";
                    pluginSelected.LoadFile(fileName);
                    ManageTagSession(fileName);
                    tsslFilePath.Text = fileName;
                }
                else
                    throw new Exception("None plugin can load for the input file");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DefaultOption();
            }

        }

        private void ManageTagSession(string value)
        {
            tabControl.SelectedTab.Tag = value;
        }

        private List<IPlugin> ListPluginSession()
        {
            List<IPlugin> plugs = (from plugin in plugins
                                   where plugin.PluginType == Interfaces.Type.Session
                                   orderby plugin.Name
                                   select plugin).ToList();
            return plugs;
        }

        public void EDIReset()
        {
            foreach (IPlugin plug in plugins)
                plug.reset();
            tsslFilePath.Text = string.Empty;
            this.EDISession = string.Empty;
            this.EDITag = string.Empty;
            this.EDITrace = string.Empty;
            foreach (TabPage tp in tabControl.TabPages)
            {
                tp.Tag = string.Empty;
                tp.Text = tp.Text.Replace("*", string.Empty);
            }
            ManageTagSession(string.Empty);
        }

        private void IndicateusedPlugin()
        {
            foreach (TabPage tp in tabControl.TabPages)
                if (tp.Tag != null && tp.Tag.ToString() != string.Empty)
                    tp.Text = string.Format("{0}*", tp.Text);
                    //if (!tp.Text.Contains("**"))
                    //    tp.Text = string.Format("{0}*", tp.Text);
                    //else
                    //    tp.Text = tp.Text.Remove(tp.Text.Length - 1);

        }

        private void search(string searchString)
        {
            if (searchString != string.Empty)
                if (!pluginSelected.Search(searchString))
                    MessageBox.Show("Search returns no results", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void LoadingScreen()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (frm == null)
            {
                frm = new frmMessage();
                frm.StartPosition = FormStartPosition.Manual;
                frm.Show(this);
            }
            this.Enabled = false;
        }

        private void DefaultOption()
        {
            this.IndicateusedPlugin();
            if (frm != null)
                frm.Dispose();
            this.Enabled = true;
            this.Focus();
            Cursor.Current = Cursors.Default;
        }

        private IPlugin pluginFromName(string tabName)
        {
            IPlugin pluginTmp = null;
            foreach (IPlugin plug in plugins)
            {
                if (plug.Name == tabName)
                {
                    pluginTmp = plug;
                    break;
                }
            }
            return pluginTmp;
        }


        #endregion

        private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            using (Brush br = new SolidBrush(TabColors[tabControl.TabPages[e.Index]]))
            {
                e.Graphics.FillRectangle(br, e.Bounds);
                SizeF sz = e.Graphics.MeasureString(tabControl.TabPages[e.Index].Text, e.Font);
                e.Graphics.DrawString(tabControl.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

                Rectangle rect = e.Bounds;
                rect.Offset(0, 1);
                rect.Inflate(0, -1);
                e.Graphics.DrawRectangle(Pens.DarkGray, rect);
                e.DrawFocusRectangle();
            }
        }

        private void SetTabHeader(TabPage page, Color color)
        {
            TabColors[page] = color;
            tabControl.Invalidate();
        }

    }
}
