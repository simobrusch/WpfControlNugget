using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfControlNugget.Security;
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

        public List<customer> Customers { get; set; }

        public ObservableCollection<CountryModel> Countries { get; set; }
        public CountryModel SelectedCountry { get; set; }
        public customer NewCustomerEntry { get; set; }

        public CustomerViewModel()
        {
            TxtConnectionString = "Server=localhost;Database=;Uid=root;Pwd=;";
            Countries = new ObservableCollection<CountryModel>();
            Customers = Enumerable.Empty<customer>().AsQueryable().ToList();
            ComboboxCountries();
            SelectedCountry = Countries[0];
            NewCustomerEntry = new customer();
            _dupChecker = new DuplicateChecker();
        }
        public customer MySelectedItem { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Combobox doesn't have a functionality at the moment. 
        /// </summary>
        private void ComboboxCountries()
        {
            Countries.Add(new CountryModel("Switzerland"));
            Countries.Add(new CountryModel("Germany"));
            Countries.Add(new CountryModel("Liechtenstein"));
        }
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
        public ICommand BtnSearchClick
        {
            get
            {
                return _btnLoadDataClick ?? (_btnLoadDataClick = new RelayCommand(
                           x =>
                           {
                               Search();
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
        public List<customer> BtnFindDuplicates_Click()
        {
            var customerRepository = new CustomerRepository();
            this.Customers = customerRepository.GetAll().ToList();
            var dupList = _dupChecker.FindDuplicates(Customers);
            Customers = new List<customer>(dupList.Cast<customer>());

            return Customers;
        }
        private void LoadData()
        {
            try
            {
                var customerRepository = new CustomerRepository();
                this.Customers = customerRepository.GetAll().ToList();
                OnPropertyChanged("Customers");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }

        private void Search()
        {
            try
            {
                var customerRepository = new CustomerRepository();
                customerRepository.GetSingle(this.NewCustomerEntry);
                this.Customers = customerRepository.GetAll().ToList();
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
                HashCustomerPassword();
                var customerRepository = new CustomerRepository();
                customerRepository.Add(this.NewCustomerEntry);
                this.Customers = customerRepository.GetAll().ToList();
                OnPropertyChanged("Customers");
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
                var customerRepository = new CustomerRepository();
                customerRepository.Delete(this.NewCustomerEntry);
                this.Customers = customerRepository.GetAll().ToList();
                OnPropertyChanged("MySelectedItem");
                OnPropertyChanged("Customers");
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
                HashCustomerPassword();
                var customerRepository = new CustomerRepository();
                customerRepository.Update(this.NewCustomerEntry);
                this.Customers = customerRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }
        private void HashCustomerPassword()
        {
            var encryption = new Encryption();
            var hash = encryption.ComputeHash(NewCustomerEntry.password, encryption.GenerateSalt(), 10101, 24);
            NewCustomerEntry.password = Convert.ToBase64String(hash);
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
