using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PhoneBook_with_Commands
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Contact selecteContact;
        private readonly ICollection<Contact> contacts = new ObservableCollection<Contact>();

        // Приватне поле команд
        private RelayCommand copyCommand;
        private RelayCommand removeCommand;
        private RelayCommand clearCommand;

        public ViewModel()
        {
            contacts.Add(new Contact() { Name = "Vova", Age = 30, Surname = "Pupkin", Phone = "+3807575828", IsMale = true });
            contacts.Add(new Contact() { Name = "Marusia", Age = 25, Surname = "Shishik", Phone = "+380771244", IsMale = false });
            contacts.Add(new Contact() { Name = "Olga", Age = 33, Surname = "Shelesh", Phone = "+38067285792", IsMale = false });

            // створення екземпляру команди, встановлюючи алгоритм для виклику
            // та алгоритм перевірки на доступність команди
            copyCommand = new RelayCommand((o) => DublicateSelectedContact(), IsSelectedContact);
            removeCommand = new RelayCommand((o) => RemoveSelectedContact(), IsSelectedContact);
            clearCommand = new RelayCommand((o) => contacts.Clear(), (o) => contacts.Count > 0);
        }

        private bool IsSelectedContact(object obj)
        {
            return SelectedContact != null;
        }

        // Властивості для прив'язки елементами на вікні
        public IEnumerable<Contact> Contacts => contacts;
        
        public Contact SelectedContact
        {
            get => selecteContact;
            set
            {
                selecteContact = value;
                OnPropertyChanged();
            }
        }

        // Властивості команд для прив'язки
        public ICommand CopyCmd => copyCommand;
        public ICommand RemoveCmd => removeCommand;
        public ICommand ClearCmd => clearCommand;

        // Метод дублювання вибраного контакта
        public void DublicateSelectedContact()
        {
            if (SelectedContact != null)
            {
                contacts.Add((Contact)SelectedContact.Clone());
            }
        }

        // Метод видалення вибраного контакта
        public void RemoveSelectedContact()
        {
            if (SelectedContact != null)
                contacts.Remove(SelectedContact);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
