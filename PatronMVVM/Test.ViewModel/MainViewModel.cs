using Crud;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Test.Model;
using Test.Model.Models;

namespace Test.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private int id=0;
        private int edad=0;
        private string nombre;
        private string email;
        private bool doubleClic;
        private CustomerLocal item;
        private ICommand guardarDatos;
        private ICommand guardarDatosDB;
        private ICommand eliminarDatos;
        private ICommand nuevosDatos;
        private ICommand doubleClick;
        private ObservableCollection<CustomerLocal> peoples;

        public MainViewModel()
        {
            peoples = new ObservableCollection<CustomerLocal>();
            doubleClic = false;
        }

        public ObservableCollection<CustomerLocal> Peoples
        { 
            get { return peoples; }
            set
            {
                peoples = value;
                OnPropertyChanged(nameof(peoples));
            }
        }

        public CustomerLocal Item
        {
            get { return item; }
            set
            {
                if (item != value)
                {
                    item = value;
                    OnPropertyChanged("Item");
                }
            }
        }

        public int Id
        {
            get { return id;}
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public int Edad
        {
            get { return edad; }
            set
            {
                edad = value;
                OnPropertyChanged("Edad");
            }
        }

        public string Email
        {
            get { return email; }
            set 
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                OnPropertyChanged("Nombre");
            }
        }

        public ICommand GuardarDatos
        {
            get
            {
                if (guardarDatos == null)
                {
                    guardarDatos = new RelayCommand(p => this.Guardar());
                }
                return guardarDatos;
            }
        }
        public ICommand GuardarDatosDB
        {
            get
            {
                if (guardarDatosDB == null)
                {
                    guardarDatosDB = new RelayCommand(p => this.GuardarDB());
                }
                return guardarDatosDB;
            }
        }

        public ICommand EliminarDatos
        {
            get
            {
                if (eliminarDatos == null)
                {
                    eliminarDatos = new RelayCommand(p => this.Eliminar());
                }
                return eliminarDatos;
            }
        }

        public ICommand NuevosDatos
        {
            get
            {
                if (nuevosDatos == null)
                {
                    nuevosDatos = new RelayCommand(p => this.Limpiar());
                }
                return nuevosDatos;
            }
        }

        public ICommand DoubleClick
        {
            get
            {
                if (doubleClick == null)
                {
                    doubleClick = new RelayCommand(p => this.MouseDoubleClick());
                }
                return doubleClick;
            }
        }

        public void MouseDoubleClick()
        {
            doubleClic = true;
            if (item!=null)
                Cargar(item);
        }

        private void Guardar()
        {
            bool ban = false;
            int i = 0;
            try
            {
                if (edad > 0 && !string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(email))
                {
                    CustomerLocal p = new CustomerLocal();
                    p.Email = email;
                    p.Nombre = nombre;
                    p.Edad = edad;
                    p.Id = id;
                    for (i = 0; i < peoples.Count; i++)
                    {
                        if (peoples[i].Id == p.Id)
                        {
                            ban = true; break;
                        }
                    }

                    if (!doubleClic)
                    {
                        peoples.Add(p);
                    }
                    else
                    {
                        peoples[i] = p;
                    }
                    this.Limpiar();
                }

            }
            catch (Exception ex) { }
        }

        private void GuardarDB()
        {
            MassiveDataAccess MDA = new MassiveDataAccess();
            MDA.SaveMassive(peoples);
        }

        private void Eliminar()
        {
            try
            {
                if (item != null)
                {
                    var result = MessageBox.Show("Seguro que deseas eliminar este registro!", "Message", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        ServiceAdapter sa = new ServiceAdapter();
                        var row = item.Id;
                        sa.DeleteCustomerService(row);
                        //this.GetAll();
                    }
                }
                else
                {
                    MessageBox.Show("Selecciona un registro!", "Message", MessageBoxButton.OK);
                }
            }
            catch (Exception ex) { }
        }

        public void Limpiar()
        {
            id = 0;
            edad = 0;
            nombre = null;
            email = null;
            doubleClic = false;
            ActualizarCampos();
        }

        private void Cargar(CustomerLocal person)
        {
            id = person.Id;
            edad = person.Edad;
            nombre = person.Nombre;
            email = person.Email;
            ActualizarCampos();
        }

        private void ActualizarCampos()
        {
            OnPropertyChanged("Id");
            OnPropertyChanged("Edad");
            OnPropertyChanged("Nombre");
            OnPropertyChanged("Email");
        }

        private void GetAll()
        {
            ServiceAdapter sa = new ServiceAdapter();
            var aux = sa.GetAllCustomerService();
        }
    }
}
