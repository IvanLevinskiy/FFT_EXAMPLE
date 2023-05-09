using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace WpfDemo
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Unit> Units
        {
            get;
            set;
        }

        public int Counter
        {
            get
            {
                return counter;
            }
            set
            {
                counter = value;
                OnPropertyChanged("Counter");
            }
        }
        int counter = 0;

        public MainViewModel()
        {
            Units = new ObservableCollection<Unit>()
            {
                new Unit("Димас", "Калатушкин"),
                new Unit("Саня", "Пушкин"),
                new Unit("Колян", "Бидонов"),
            };
        }

        public ICommand AddCMD
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Counter++;
                },
                (obj) => (true));
            }
        }

        #region Реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }

        }
        #endregion
    }
}
