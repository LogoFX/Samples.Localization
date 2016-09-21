using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using JetBrains.Annotations;

namespace Samples.Localization.App
{
    public enum CategoryTypes
    {
        None,
        Load
    }

    public class Product : INotifyPropertyChanged
    {
        private CategoryTypes _category;

        public CategoryTypes Category
        {
            get { return _category; }
            set
            {
                if (value == _category)
                {
                    return;
                }

                _category = value;
                OnPropertyChanged();
            }
        }

        public Product()
        {
            Category = CategoryTypes.Load;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SessionViewModel
    {
        public Product Product { get; set; }

        public SessionViewModel()
        {
            Product = new Product();
        }
    }

    public sealed class MainViewModel : INotifyPropertyChanged
    {
        private readonly Random _rnd = new Random();
        private readonly DispatcherTimer _timer;
        
        public MainViewModel()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            _timer.Tick += OnTimer;

            CurrentSessions = new SessionViewModel();

            _timer.Start();
        }

        private void OnTimer(object sender, EventArgs e)
        {
            var categoris = (CategoryTypes[]) Enum.GetValues(typeof(CategoryTypes));
            var index = _rnd.Next(categoris.Length);
            CurrentSessions.Product.Category = categoris[index];

            IdentityValueZeroLength = _rnd.NextDouble() >= 0.5;
        }

        private bool _identityValueZeroLength = false;

        public bool IdentityValueZeroLength
        {
            get { return _identityValueZeroLength; }
            private set
            {
                if (_identityValueZeroLength == value)
                {
                    return;
                }

                _identityValueZeroLength = value;
                OnPropertyChanged();
            }
        }

        public SessionViewModel CurrentSessions { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
