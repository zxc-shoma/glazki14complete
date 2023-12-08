using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace garipov_glazki
{
    /// <summary>
    /// Логика взаимодействия для PRW.xaml
    /// </summary>
    public partial class PRW : Window
    {
        private Agent currentAgent = new Agent();
        public PRW(int max)
        {
            InitializeComponent();
            PriorityBox.Text = max.ToString();

        }
        
       





    private void SaveBut_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PriorityBox.Text))
            {
                Close();
            }
            else
            {
                MessageBox.Show("Введите новый приоритет для агента");
            }


        }

        private void CloseBut_Click(object sender, RoutedEventArgs e)
        {
            PriorityBox.Text = "";
            Close();


        }

        private void PriorityBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
           

        }

        private void PriorityBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            //var dbContext = new Garipov_glazkiEntities();
            //int maxPriority = dbContext.Agent.Max(a => a.Priority);
            //PriorityBox.Text = maxPriority.ToString();
        }
    }
}
