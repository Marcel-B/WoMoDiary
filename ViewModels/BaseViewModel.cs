using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using com.b_velop.WoMoDiary.Services;
using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.Domain;

namespace com.b_velop.WoMoDiary.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected readonly IDataStore<Place> PlaceStore;
        protected readonly IDataStore<Trip> TripStore;
        protected readonly IDataStore<User> UserStore;

        public Action<string> ErrorAction { get; set; }

        public BaseViewModel()
        {
            PlaceStore = ServiceLocator.Instance.Get<IDataStore<Place>>();
            TripStore = ServiceLocator.Instance.Get<IDataStore<Trip>>();
            UserStore = ServiceLocator.Instance.Get<IDataStore<User>>();
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
