using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook_with_Commands
{
    public class Contact : INotifyPropertyChanged
    {
        private string name;
        private string surname;
        private int age;
        private string phone;
        private bool isMale;

        public string Name
        {
            get { return name; }
            set
            {
                // якщо новий елемент рівний старому
                // тоді генерація події оновлення не відбувається, 
                // що уникає зайвого оновлення компонентів інтерфейсу
                if (name != value)
                {
                    // оновлення властивості
                    name = value;
                    // виклик події оновлення властивості Name
                    OnPropertyChanged();
                    // так як властивість FullName залежить від Name
                    // виклик події для її оновлення
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }
        public string Surname
        {
            get { return surname; }
            set
            {
                if (surname != value)
                {
                    surname = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }
        public int Age
        {
            get { return age; }
            set
            {
                if (age != value)
                {
                    age = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Phone
        {
            get { return phone; }
            set
            {
                if (phone != value)
                {
                    phone = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsMale
        {
            get { return isMale; }
            set
            {
                if (isMale != value)
                {
                    isMale = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(GenderName));
                }
            }
        }

        // Властивсть, яка повертає назву статі контакта
        // використовуєьтся при відображенні елемента в списку
        public string GenderName => IsMale ? "Male" : "Female";

        // Властивсть, яка повертає повне ім'я контакта
        public string FullName => $"{Name} {Surname}";

        // Метод слонування об'єкта,
        // який виконує глибоке копіювання всіх властивостей
        public object Clone()
        {
            // shallow copy (поверхневе копіювання) - копіюються лише 
            // значення value типів та посилання reference типів
            Contact clone = (Contact)this.MemberwiseClone();

            // deep copy (глибоке копіювання) - кожний reference тип
            // копіюється власним методом клонування
            clone.Name = (string)this.Name.Clone();
            clone.Surname = (string)this.Surname.Clone();
            clone.Phone = (string)this.Phone.Clone();

            return clone;
        }

        // Оголошення події оновлення властивості
        public event PropertyChangedEventHandler PropertyChanged;

        // Створення методу OnPropertyChanged для виклику події
        // В якості параметра буде використано ім'я властивості, що його викликала
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
