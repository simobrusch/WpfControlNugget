using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DuplicateCheckerLib;
using MySql.Data.MySqlClient;
using WpfControlNugget.Model;
using WpfControlNugget.Repository;
using WpfControlNugget.ViewModel.Commands;

namespace WpfControlNugget.ViewModel
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private string _txtConnectionString;
        private readonly DuplicateChecker _dupChecker;

        private ICommand _btnAddDataClick;
        private ICommand _btnFindDuplicatesClick;
        private ICommand _btnUpdateDataClick;
        private ICommand _btnLoadDataClick;
        private ICommand _btnDeleteDataClick;

        public List<CustomerModel> Customers
        {
            get => _customers;
            set
            {
                _customers = value;
                OnPropertyChanged("Customers");
            }
        }
        private List<CustomerModel> _customers;
        public CustomerModel NewCustomerEntry { get; set; }

        public CustomerViewModel()
        {
            TxtConnectionString = "Server=localhost;Database=;Uid=root;Pwd=;";

            Customers = new List<CustomerModel>();
            NewCustomerEntry = new CustomerModel();
            _dupChecker = new DuplicateChecker();
        }
        public CustomerModel MySelectedItem { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public string TxtConnectionString
        {
            get => _txtConnectionString;
            set
            {
                _txtConnectionString = value;
                OnPropertyChanged(nameof(TxtConnectionString));
            }
        }
        public ICommand BtnAddDataClick
        {
            get
            {
                return _btnAddDataClick ?? (_btnAddDataClick = new RelayCommand(
                           x =>
                           {
                               AddData();
                           }));
            }
        }
        public ICommand BtnUpdateDataClick
        {
            get
            {
                return _btnUpdateDataClick ?? (_btnUpdateDataClick = new RelayCommand(
                           x =>
                           {
                               UpdateData();
                           }));
            }
        }
        public ICommand BtnLoadDataClick
        {
            get
            {
                return _btnLoadDataClick ?? (_btnLoadDataClick = new RelayCommand(
                           x =>
                           {
                               LoadData();
                           }));
            }
        }
        public ICommand BtnFindDuplicatesClick
        {
            get
            {
                return _btnFindDuplicatesClick ?? (_btnFindDuplicatesClick = new RelayCommand(
                           x =>
                           {
                               BtnFindDuplicates_Click();
                           }));
            }
        }
        public ICommand BtnDeleteDataClick
        {
            get
            {
                return _btnDeleteDataClick ?? (_btnDeleteDataClick = new RelayCommand(
                           x =>
                           {
                               DeleteData();
                           }));
            }
        }
        public List<CustomerModel> BtnFindDuplicates_Click()
        {
            var customerModelRepository = new CustomerRepository(TxtConnectionString);
            this.Customers = customerModelRepository.GetAll().ToList();
            var dupList = _dupChecker.FindDuplicates(Customers);
            Customers = new List<CustomerModel>(dupList.Cast<CustomerModel>());

            return Customers;
        }
        private void LoadData()
        {
            try
            {
                var customerModelRepository = new CustomerRepository(TxtConnectionString);
                this.Customers = customerModelRepository.GetAll().ToList();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LocationTree"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }
        private void AddData()
        {
            try
            {
                var customerModelRepository = new CustomerRepository(TxtConnectionString);
                customerModelRepository.Add(this.NewCustomerEntry);
                this.Customers = customerModelRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }
        private void DeleteData()
        {
            try
            {
                var customerModelRepository = new CustomerRepository(TxtConnectionString);
                customerModelRepository.Delete(this.NewCustomerEntry);
                this.Customers = customerModelRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }
        private void UpdateData()
        {
            try
            {
                var customerModelRepository = new CustomerRepository(TxtConnectionString);
                customerModelRepository.Update(this.NewCustomerEntry);
                this.Customers = customerModelRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }
    
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
