using System;
using System.ComponentModel;

namespace WindowsTasksPlanning.Infrastructure.Impl
{
    public class Fact : INotifyPropertyChanged
    {
        private readonly Func<bool> _predicate;

        public Fact(INotifyPropertyChanged observable, Func<bool> predicate)
        {
            _predicate = predicate;
            observable.PropertyChanged +=
                                (sender, args) =>
                                    PropertyChanged(this, new PropertyChangedEventArgs("Value"));
        }

        public bool Value
        {
            get { return _predicate(); }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
