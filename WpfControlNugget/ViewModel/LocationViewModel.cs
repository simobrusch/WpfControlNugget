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
    public class LocationViewModel : INotifyPropertyChanged
    {
        private string _txtConnectionString;

        private ICommand _btnLoadDataClick;
        private ICommand _btnAddDataClick;
        private ICommand _btnDeleteDataClick;
        private ICommand _btnUpdateDataClick;
        private ICommand _btnBuildTreeClick;

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
                return _btnAddDataClick ?? (_btnAddDataClick = new RelayCommand(
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
        public ICommand BtnBuildTreeClick
        {
            get
            {
                return _btnBuildTreeClick ?? (_btnBuildTreeClick = new RelayCommand(
                           x =>
                           {
                               LoadTreeData();
                           }));
            }
        }
        private void LoadData()
        {
            try
            {
                var locationModelRepository = new LocationRepository();
                this.Locations = locationModelRepository.GetAll().ToList();
                this.LocationTree = new List<Node<LocationModel>>();
                GenerateLocationTreeFromList(Locations);
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
                var locationModelRepository = new LocationRepository();
                locationModelRepository.Add(this.NewLocationModelEntry);
                this.Locations = locationModelRepository.GetAll().ToList();
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
                var locationModelRepository = new LocationRepository();
                locationModelRepository.Delete(this.NewLocationModelEntry);
                this.Locations = locationModelRepository.GetAll().ToList();
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
                var locationModelRepository = new LocationRepository();
                locationModelRepository.Update(this.NewLocationModelEntry);
                this.Locations = locationModelRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }
        private void LoadTreeData()
        {
            try
            {
                var locationModelRepository = new LocationRepository();
                this.Locations = locationModelRepository.GetAll().ToList();
                this.LocationTree = new List<Node<LocationModel>>();
                GenerateLocationTreeFromList(Locations);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LocationTree"));
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
