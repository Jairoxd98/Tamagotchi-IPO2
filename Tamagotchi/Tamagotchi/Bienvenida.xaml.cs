using System;
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
using System.Windows.Shapes;

namespace Tamagotchi
{
    /// <summary>
    /// Lógica de interacción para Bienvenida.xaml
    /// </summary>
    public partial class Bienvenida : Window
    {
        MainWindow padre;

        public Bienvenida(MainWindow padre)
        {
            InitializeComponent();
            this.padre = padre;
        }

        private void eventoEmpezar(object sender, RoutedEventArgs e)
        {
            padre.setNombre(this.tbNombre.Text);
            this.Close();
        }

        private void mostratInfo(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Práctica realizada por Jairo Celada Cebrián\n\n Instrucciones:\nEl juego es como un tamagotchi en el que tienes que cuidar a una rana todo el tiempo que puedas" +
                " y conseguir la mayor cantidad de puntos posible. Hay premios ocultos, trofeos, coleccionables que cambian aspectos y bonificaciones del juego, y un ranking con los mejores jugadores.\n" +
                "Para jugar solo tienes que pulsar los botones de la esquina superior derecha e investigar y alcanzar los diferentes coleccionables y bonus del juego. No hay que usar el teclado.", "IPO2 Tamagotchi", MessageBoxButton.OK);

        }
    }
}
