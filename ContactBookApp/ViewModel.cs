using Data;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBook_with_Commands
{
    public class ViewModel : INotifyPropertyChanged
    {
        private ContactVm selecteContact;
        private readonly ICollection<ContactVm> contacts;

        // Приватне поле команд
        private RelayCommand copyCommand;
        private RelayCommand removeCommand;
        private RelayCommand clearCommand;

        private readonly ContactBookDbContext context;
        public ViewModel()
        {
            context = new ContactBookDbContext();

            var contactsVms = context.Contacts.Select(x => new ContactVm(x));
            this.contacts = new ObservableCollection<ContactVm>(contactsVms);

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
        public IEnumerable<ContactVm> Contacts => contacts;
        
        public ContactVm SelectedContact
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
                contacts.Add((ContactVm)SelectedContact.Clone());

                // add contact to db
                context.Contacts.Add(new Contact()
                {
                    Name = SelectedContact.Name,
                    Surname = SelectedContact.Surname,
                    Age = SelectedContact.Age,
                    Phone = SelectedContact.Phone,
                    IsMale = SelectedContact.IsMale,
                });
                context.SaveChanges();
            }
        }

        // Метод видалення вибраного контакта
        public void RemoveSelectedContact()
        {
            if (SelectedContact != null)
            {
                // add contact to db
                var entity = context.Contacts.Find(SelectedContact.Id);

                // remove from db
                if (entity == null)
                {
                    MessageBox.Show("The contact you want to delete does not exist!");
                }
                else 
                {
                    context.Contacts.Remove(entity);
                    context.SaveChanges();
                }

                contacts.Remove(SelectedContact);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
