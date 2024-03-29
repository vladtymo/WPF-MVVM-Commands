﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBook_with_Commands
{
    public class ViewModel : INotifyPropertyChanged
    {
        private ContactVm selecteContact;
        private readonly ICollection<ContactVm> contacts = new ObservableCollection<ContactVm>();

        // Приватне поле команд
        private RelayCommand copyCommand;
        private RelayCommand removeCommand;
        private RelayCommand clearCommand;

        public ViewModel()
        {
            

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
