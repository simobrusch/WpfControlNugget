using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfControlNugget.Model;
using WpfControlNugget.Repository;
using WpfControlNugget.ViewModel.Commands;

namespace WpfControlNugget.ViewModel
{
    public class LocationViewModel :INotifyPropertyChanged
    {
        private string _txtConnectionString;
        
        private ICommand _btnLoadDataClick;
        private ICommand _btnAdddataClick;
        private ICommand _btnDeleteDataClick;
        private ICommand _btnUpdateDataClick;

        public List<LocationModel> Locations
        {
            get => _locations;
            set
            {
                _locations = value;
                OnPropertyChanged("Locations");
            }
        }
        private List<LocationModel> _locations;
        public LocationModel NewLocationModelEntry { get; set; }
        public List<Node<LocationModel>> LocationTree { get; set; }
        
        public LocationViewModel()
        {
            TxtConnectionString = "Server=localhost;Database=;Uid=root;Pwd=;";
            Locations = new List<LocationModel>();
            NewLocationModelEntry = new LocationModel();
        }
        public LogEntryModel MySelectedItem { get; set; }
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
        public ICommand BtnAddDataClick
        {
            get
            {
                return _btnAdddataClick ?? (_btnAdddataClick = new RelayCommand(
                           x =>
                           {
                               AddData();
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
        private void LoadData()
        {
            try
            {
                var locationModelRepository = new LocationRepository(TxtConnectionString);
                this.Locations = locationModelRepository.GetAll();
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
                var locationModelRepository = new LocationRepository(TxtConnectionString);
                locationModelRepository.Add(this.NewLocationModelEntry);
                this.Locations = locationModelRepository.GetAll();
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
                var locationModelRepository = new LocationRepository(TxtConnectionString);
                locationModelRepository.Delete(this.NewLocationModelEntry);
                this.Locations = locationModelRepository.GetAll();
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
                var locationModelRepository = new LocationRepository(TxtConnectionString);
                locationModelRepository.Update(this.NewLocationModelEntry);
                this.Locations = locationModelRepository.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }
        public void GenerateLocationTreeFromList(List<LocationModel> locationList)
        {
            var treeBuilder = new LocationTreeBuilder();
            var locationNode = treeBuilder.BuildTree(locationList);
            this.LocationTree.Add(locationNode);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
