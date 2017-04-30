namespace GUI.Model
{
    class SettingsModel : ISettingsModel
    {
        public string ServerIP
        {
            get { return Properties.Settings.Default.ServerIP; }
            set { Properties.Settings.Default.ServerIP = value; }
        }
        public int ServerPort
        {
            get { return Properties.Settings.Default.ServerPort; }
            set { Properties.Settings.Default.ServerPort = value; }
        }
    
        public int MazeRows { get; set; }
        public int MazeCols { get; set; }
        public int SearchAlgorithm { get; set; }

        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }
    }
}
