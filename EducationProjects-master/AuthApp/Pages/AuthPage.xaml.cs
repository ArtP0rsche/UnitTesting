using System;
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

namespace AuthApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void authButton_Click(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text == "" || passwordBox.Password == "")
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            if (UsersSingleton.Instance.Auth(loginBox.Text, passwordBox.Password))
            {
                MessageBox.Show("Вы вошли в систему");
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }
    }
}
