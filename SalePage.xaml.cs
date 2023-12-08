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

namespace garipov_glazki
{
    /// <summary>
    /// Логика взаимодействия для SalePage.xaml
    /// </summary>
    public partial class SalePage : Page
    {
        Agent currentAgent;
        public SalePage(Agent SelectedAgent)
        {
            InitializeComponent();
            currentAgent = SelectedAgent;

            var currentSales = Garipov_glazkiEntities.GetContext().ProductSale.ToList();

            if (SelectedAgent.ID != 0)
            {
                currentSales = currentSales.Where(p => p.AgentID == SelectedAgent.ID).ToList();
            }
            SalesListView.ItemsSource = currentSales;
            SalesListView.Items.Refresh();

            DeleteSale.Visibility = Visibility.Collapsed;

        }
        private void UpdateSales()
        {
            var currentSales = Garipov_glazkiEntities.GetContext().ProductSale.ToList();

            if (currentAgent.ID != 0)
            {
                currentSales = currentSales.Where(p => p.Agent.ID == currentAgent.ID).ToList();
            }
            SalesListView.ItemsSource = currentSales;
            SalesListView.Items.Refresh();
        }




        private void SalesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SalesListView.SelectedItems.Count == 0)
                DeleteSale.Visibility = Visibility.Collapsed;
            if (SalesListView.SelectedItems.Count > 0)
                DeleteSale.Visibility = Visibility.Visible;
            UpdateSales();
            SalesListView.Items.Refresh();

        }

        private void AddSale_Click(object sender, RoutedEventArgs e)
        {
            
            Manager.MainFrame.Navigate(new AddSalePage(currentAgent));
            UpdateSales();
            SalesListView.Items.Refresh();
            

        }

        private void DeleteSale_Click(object sender, RoutedEventArgs e)
        {
            List<ProductSale> SelectedSales = SalesListView.SelectedItems.Cast<ProductSale>().ToList();

            foreach (ProductSale currentSales in SelectedSales)
            {
                Garipov_glazkiEntities.GetContext().ProductSale.Remove(currentSales);
            }
            Garipov_glazkiEntities.GetContext().SaveChanges();
            UpdateSales();

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateSales();
        }
    }
}
