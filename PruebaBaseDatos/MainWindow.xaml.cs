using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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

namespace PruebaBaseDatos
{
    public partial class MainWindow : Window
    {
        private BDMarcosEntities contexto;
        private CLIENTE nuevo;
        private CollectionViewSource vista;
        ObservableCollection<CLIENTE> Clientes;
        public MainWindow()
        {
            InitializeComponent();
            contexto = new BDMarcosEntities();

            var consulta = from Cliente in contexto.CLIENTES.Include("PEDIDOS")
                           select Cliente;

            Clientes = new ObservableCollection<CLIENTE>(consulta.ToList());

            contexto.CLIENTES.Load();
            contexto.PEDIDOS.Load();


            ClientesListBox.DataContext =  contexto.CLIENTES.Local;
            ClientesComboBox.DataContext = contexto.CLIENTES.Local;
            ClientesModificarComboBox.DataContext = contexto.CLIENTES.Local;
            nuevo = new CLIENTE();
            AñadirStackPanel.DataContext = nuevo;

            DataGrid.DataContext = contexto.CLIENTES.Local;


            vista = new CollectionViewSource();
            vista.Source = Clientes;
            FiltrarDataGrid.DataContext = vista;
            vista.Filter += Vista_Filter;

            
        }
        
        private void Vista_Filter(object sender, FilterEventArgs e)
        {
            CLIENTE item = (CLIENTE)e.Item;

            if(FiltrarTextBox.Text == "")
            {
                e.Accepted = true;
            }
            else
            {
                if (item.nombre.Contains(FiltrarTextBox.Text))
                    e.Accepted = true;
                else
                    e.Accepted = false;
            }
        }

        private void InsertarButton_Click(object sender, RoutedEventArgs e)
        {
            contexto.CLIENTES.Add(nuevo);
            contexto.SaveChanges();
            nuevo = new CLIENTE();
            AñadirStackPanel.DataContext = nuevo;
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            contexto.CLIENTES.Remove((CLIENTE)ClientesComboBox.SelectedItem);
            contexto.SaveChanges();
        }

        private void ModificarButton_Click(object sender, RoutedEventArgs e)
        {
            contexto.SaveChanges();
        }

        private void ActualizarButton_Click(object sender, RoutedEventArgs e)
        {
            contexto.SaveChanges();
        }

        private void FiltrarButton_Click(object sender, RoutedEventArgs e)
        {
            vista.View.Refresh();
        }
    }
}
