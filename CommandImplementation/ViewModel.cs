using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CommandImplementation
{
    internal sealed class ViewModel : INotifyPropertyChanged
    {
        private readonly RelayCommand redCommand;
        private readonly RelayCommand blueCommand;
        private readonly RelayCommand greenCommand;

        private string selectedColor = "Black";

        public ViewModel()
        {
            blueCommand = new RelayCommand((o) => SetBlueColor(), (o) => CanSetBlueColor());
            greenCommand = new RelayCommand((o) => SelectedColor = "Green", (o) => SelectedColor != "Green");
            redCommand = new RelayCommand((o) => SelectedColor = "Red", (o) => SelectedColor != "Red");

            //this.PropertyChanged += (s, a) =>
            //{
            //    if (a.PropertyName == nameof(SelectedColor))
            //    {
            //        blueCommand.RaiseCanExecuteChanged();
            //        greenCommand.RaiseCanExecuteChanged();
            //        redCommand.RaiseCanExecuteChanged();
            //    }
            //};
        }
        void SetBlueColor()
        {
            SelectedColor = "Blue";
        }
        bool CanSetBlueColor()
        {
            return SelectedColor != "Blue";
        }

        public ICommand BlueCommand => blueCommand;
        public ICommand GreenCommand => greenCommand;
        public ICommand RedCommand => redCommand;

        public string SelectedColor
        {
            get => selectedColor;
            set
            {
                selectedColor = value;
                OnPropertyChanged();
                ++Count;
            }
        }

        private int count;

        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
