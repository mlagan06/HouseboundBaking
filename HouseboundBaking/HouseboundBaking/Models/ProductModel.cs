using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace HouseboundBaking.Models
{
    public class ProductModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _ProductId;
        private string _BrandName;
        private string _Grape;
        private int _Quantity;
        private string _Description;
        private string _Size;
        private string _Image;
        private decimal _Price;
        private decimal _SubTotalForItem;
        private string _Genre;
        public ObservableCollection<QuantityOLD> _ListQuantites;

        //Constructor
        public ProductModel()
        {
            //Subscription
            this.PropertyChanged += OnPropertyChanged;
        }

        [PrimaryKey, AutoIncrement]
        public int ProductId
        {
            get { return _ProductId; }
            set
            {
                if (_ProductId == value) return;
                _ProductId = value;
                OnPropertyChanged();
            }
        }

        //Properties
        public int Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
                OnPropertyChanged();
            }
        }

        //public ObservableCollection<Quantity> ListQuantites
        //{
        //    get
        //    {
        //        return _ListQuantites;
        //    }
        //    set
        //    {
        //        _ListQuantites = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private Quantity _selectedQuantity;
        //public Quantity SelectedQuantity
        //{
        //    get
        //    {
        //        return _selectedQuantity;
        //    }
        //    set
        //    {
        //        if (value == null)
        //        {
        //            _selectedQuantity = _selectedQuantity;
        //        }
        //        else
        //        {
        //            _selectedQuantity = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        [MaxLength(50)]
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                OnPropertyChanged();
            }
        }


        [MaxLength(50)]
        public string Size
        {
            get
            {
                return _Size;
            }
            set
            {
                _Size = value;
                OnPropertyChanged();
            }
        }

        public string Image
        {
            get
            {
                return _Image;
            }
            set
            {
                _Image = value;
                OnPropertyChanged();
            }
        }

        public decimal Price
        {
            get
            {
                return _Price;
            }
            set
            {
                _Price = value;
                OnPropertyChanged();
            }
        }

        public decimal SubTotalForItem
        {
            get
            {
                return _SubTotalForItem;
            }
            set
            {
                _SubTotalForItem = value;
                OnPropertyChanged();
            }
        }

        public string Genre
        {
            get
            {
                return _Genre;
            }
            set
            {
                _Genre = value;
                OnPropertyChanged();
            }
        }

        public string BrandName
        {
            get { return _BrandName; }
            set
            {
                _BrandName = value;
                OnPropertyChanged();
            }
        }

        public string Grape
        {
            get { return _Grape; }
            set
            {
                _Grape = value;
                OnPropertyChanged();
            }
        }

        //OnPropertyChanged
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Quantity))
            {
                //test quantity amount
                var a = 1;
            }
        }

        // [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
