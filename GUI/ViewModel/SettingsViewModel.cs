﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI.Model;

namespace GUI.ViewModel
{
    class SettingsViewModel : ViewModel
    {
        private ISettingsModel model;

        public SettingsViewModel()
        {
        }

        public SettingsViewModel(ISettingsModel model)
        {
            this.model = model;
        }
        public string ServerIP
        {
            get { return model.ServerIP; }
            set
            {
                model.ServerIP = value;
                NotifyPropertyChanged("ServerIP");
            }
        }
        public int ServerPort
        {
            get { return model.ServerPort; }
            set
            {
                model.ServerPort = value;
                NotifyPropertyChanged("ServerPort");
            }
        }
      
        public void SaveSettings()
        {
            model.SaveSettings();
        }
    }
}
