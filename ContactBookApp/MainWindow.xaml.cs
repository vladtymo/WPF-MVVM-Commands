﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhoneBook_with_Commands
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Модель даних для відображення
        private ViewModel viewModel = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();

            // Встановлення контексту даних, який містить властивості
            // до яких будуть прив'язуватися елементи вікна
            this.DataContext = viewModel;
        }
    }
}
