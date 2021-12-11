using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace HouseboundBaking.Models
{
    public class UserModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _UserId;
        private string _Email;
        private string _FullName;
        private string _AddressLine1;
        private string _AddressLine2;
        private string _County;
        private string _Country;
        private string _Postcode;
        private string _City;
        private string _MobileNumber;
        private DateTime _DateUserCreated;
        private string _Password;

        //Constructor
        public UserModel()
        {
            //Subscription
            this.PropertyChanged += OnPropertyChanged;
        }

        [PrimaryKey, AutoIncrement]
        public int UserId
        {
            get { return _UserId; }
            set
            {
                if (_UserId == value) return;
                _UserId = value;
                OnPropertyChanged();
            }
        }

        //Properties
        [MaxLength(50)]
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
                OnPropertyChanged();
            }
        }


        [MaxLength(50)]
        public string FullName
        {
            get
            {
                return _FullName;
            }
            set
            {
                _FullName = value;
                OnPropertyChanged();
            }
        }


        public string AddressLine1
        {
            get
            {
                return _AddressLine1;
            }
            set
            {
                _AddressLine1 = value;
                OnPropertyChanged();
            }
        }
        public string AddressLine2
        {
            get
            {
                return _AddressLine2;
            }
            set
            {
                _AddressLine2 = value;
                OnPropertyChanged();
            }
        }

        public string City
        {
            get
            {
                return _City;
            }
            set
            {
                _City = value;
                OnPropertyChanged();
            }
        }

        public string County
        {
            get
            {
                return _County;
            }
            set
            {
                _County = value;
                OnPropertyChanged();
            }
        }

        public string Country
        {
            get
            {
                return _Country;
            }
            set
            {
                _Country = value;
                OnPropertyChanged();
            }
        }

        public string Postcode
        {
            get
            {
                return _Postcode;
            }
            set
            {
                _Postcode = value;
                OnPropertyChanged();
            }
        }

        public string MobileNumber
        {
            get
            {
                return _MobileNumber;
            }
            set
            {
                _MobileNumber = value;
                OnPropertyChanged();
            }
        }
        public DateTime DateUserCreated
        {
            get
            {
                return _DateUserCreated;
            }
            set
            {
                _DateUserCreated = value;
                OnPropertyChanged();
            }
        }

        [MaxLength(50)]
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Email))
            {
                //test quantity amount
                var a = 1;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
