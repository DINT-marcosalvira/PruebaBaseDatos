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
        public MainWindow()
        {
            InitializeComponent();
            contexto = new BDMarcosEntities();

            contexto.CLIENTES.Load();
            ClientesListBox.DataContext =  contexto.CLIENTES.Local;
            ClientesComboBox.DataContext = contexto.CLIENTES.Local;
            ClientesModificarComboBox.DataContext = contexto.CLIENTES.Local;
            nuevo = new CLIENTE();
            AñadirStackPanel.DataContext = nuevo;
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
    }
}
