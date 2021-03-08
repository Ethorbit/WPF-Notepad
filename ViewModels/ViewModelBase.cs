using System.ComponentModel;
namespace SimpleViewModel2.ViewModels
{
    class Base : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, property) => { };

        public void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string caller = "")
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }
    }
}
