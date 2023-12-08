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
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    /// 


    

    public partial class ServicePage : Page
    {


        int CountRecords;
        int CountPage;
        int CurrentPage = 0;

        public List<Agent> CurrentPageList = new List<Agent>();
        public List<Agent> TableList;
        




        public ServicePage()
        {
            InitializeComponent();

            var currentServices = Garipov_glazkiEntities.GetContext().Agent.ToList();

            ServiceListView.ItemsSource = currentServices;
            UpdateAgents();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    Manager.MainFrame.Navigate(new AddEditPage()));

        //}

        private void TBSearch_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateAgents();

        }

        private void SortCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgents();
        }

        private void FilterCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgents();

        }

        private void ChangePage(int direction, int? selectedPage)
        {
            CurrentPageList.Clear();
            CountRecords = TableList.Count;
            if (CountRecords %10 >0)
            {
                CountPage = CountRecords / 10 + 1;
            }
            else
            {
                CountPage = CountRecords / 10;
            }

            Boolean Ifupdate = true;

            int min;
            if (selectedPage.HasValue)
            {
                if (selectedPage>=0 && selectedPage <= CountPage)
                {
                    CurrentPage = (int)selectedPage;
                    min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                    for (int i = CurrentPage *10; i<min; i++)
                    {
                        CurrentPageList.Add(TableList[i]);
                    }
                }
            }
            else
            {
                switch (direction)
                {
                    case 1:
                        if (CurrentPage >0)
                        {
                            CurrentPage--;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }

                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;

                    case 2:
                        if (CurrentPage < CountPage -1)
                        {
                            CurrentPage++;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }


                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;

                    
                }
                
            }
            if (Ifupdate)
            {
                PageListBox.Items.Clear();

                for ( int i =1; i<= CountPage;i++)
                {
                    PageListBox.Items.Add(i);
                }
                PageListBox.SelectedIndex = CurrentPage;


                min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                TBCount.Text = min.ToString();
                TBAllRecords.Text = " из " + CountRecords.ToString();

                ServiceListView.ItemsSource = CurrentPageList;
                ServiceListView.Items.Refresh();


            }





        }


        private void UpdateAgents()
        {
            var currentAgents = Garipov_glazkiEntities.GetContext().Agent.ToList();

            currentAgents = currentAgents.Where(p => p.Title.ToLower().Contains(TBSearch.Text.ToLower()) || p.Phone.Replace("+7", "8").Replace("(", "").Replace(") ", "").Replace(" ", "").Replace("-", "").Contains(TBSearch.Text.Replace("+7", "8").Replace("(", "").Replace(") ", "").Replace(" ", "").Replace("-", ""))
            || p.Email.ToLower().Contains(TBSearch.Text.ToLower())).ToList();



            

            if (SortCombo.SelectedIndex==0)
                currentAgents = currentAgents.OrderBy(p => p.Title).ToList();
            if (SortCombo.SelectedIndex == 1)
                currentAgents = currentAgents.OrderByDescending(p =>p.Title).ToList();
            if (SortCombo.SelectedIndex == 2)
                currentAgents = currentAgents.OrderBy(p => p.SaleProduct).ToList();
            if (SortCombo.SelectedIndex == 3)
                currentAgents = currentAgents.OrderByDescending(p => p.SaleProduct).ToList();
            if (SortCombo.SelectedIndex == 4)
                currentAgents = currentAgents.OrderBy(p => p.Priority).ToList();
            if (SortCombo.SelectedIndex == 5)
                currentAgents = currentAgents.OrderByDescending(p => p.Priority).ToList();


            if (FilterCombo.SelectedIndex == 0)
                currentAgents = currentAgents.Where(p => (p.AgentTypeID >= 1 && p.AgentTypeID <= 6)).ToList();
            if (FilterCombo.SelectedIndex == 1)
                currentAgents = currentAgents.Where(p => (p.AgentTypeID==1)).ToList();
            if (FilterCombo.SelectedIndex == 2)
                currentAgents = currentAgents.Where(p => (p.AgentTypeID == 2)).ToList();
            if (FilterCombo.SelectedIndex == 3)
                currentAgents = currentAgents.Where(p => (p.AgentTypeID == 3)).ToList();
            if (FilterCombo.SelectedIndex == 4)
                currentAgents = currentAgents.Where(p => (p.AgentTypeID == 4)).ToList();
            if (FilterCombo.SelectedIndex == 5)
                currentAgents = currentAgents.Where(p => (p.AgentTypeID == 5)).ToList();
            if (FilterCombo.SelectedIndex == 6)
                currentAgents = currentAgents.Where(p => (p.AgentTypeID == 6)).ToList();





            ServiceListView.ItemsSource = currentAgents;
            ServiceListView.Items.Refresh();

            TableList = currentAgents;
            ChangePage(0, 0);









        }

        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAgents();

        }

        private void ServiceListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ServiceListView.SelectedItems.Count > 1)
            {
                ChangePriority.Visibility = Visibility.Visible;
            }
            else
            {
                ChangePriority.Visibility = Visibility.Hidden;
            }

        }

        private void LeftDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(1, null);

        }

        private void RightDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(2, null);

        }

        private void PageListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangePage(0, Convert.ToInt32(PageListBox.SelectedItem.ToString()) - 1);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
            UpdateAgents();
            //Sale.Items.Refresh();

        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Agent));
            UpdateAgents();
            
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Garipov_glazkiEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            ServiceListView.ItemsSource = Garipov_glazkiEntities.GetContext().Agent.ToList();
            UpdateAgents();

        }

        private void ChangePriority_Click(object sender, RoutedEventArgs e)
        {
            int max = 0;
            foreach (Agent agent in ServiceListView.SelectedItems)
            {
                if (max < agent.Priority)
                    max = agent.Priority;

            }
            PRW Window = new PRW(max);
            Window.ShowDialog();
            if (string.IsNullOrEmpty(Window.PriorityBox.Text)) {
                return;
            }
            foreach (Agent AgentLV in ServiceListView.SelectedItems)
            {
                AgentLV.Priority = Convert.ToInt32(Window.PriorityBox.Text);
            }
            try
            {
                Garipov_glazkiEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация обновлена");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());

            }
            UpdateAgents();



        }
    }
}
