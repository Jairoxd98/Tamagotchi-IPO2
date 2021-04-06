using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Tamagotchi
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer t1;
        
        double decremento = 1.0;
        double puntos = 0.0;
        string nombreTamagotchi;
        rankingEntities dataEntities = new rankingEntities();
        int extra = 0;
        int tiempo = 0;
        Boolean trof1 = true;
        Boolean trof2 = true;
        Boolean trof3 = true;
        Boolean trof4 = true;

        public MainWindow()
        {
            InitializeComponent();
            Bienvenida ventanaInicial = new Bienvenida(this);
            ventanaInicial.ShowDialog();

            t1 = new DispatcherTimer();
            t1.Interval = TimeSpan.FromMilliseconds(1000);
            t1.Tick += new EventHandler(reloj);
            t1.Start();
        }

        private void reloj(object sender, EventArgs e)
        {
            this.pbEnergia.Value -= decremento;
            this.pbComer.Value -= decremento;
            this.pbDiversion.Value -= decremento;
            
            //Niveles de dificultad segun el mapa
            
            if (imFondo.Source == imFondo3.Source && decremento < 4.3)//Modo Normal
            {
                decremento += 0.15;
            }
            else if (imFondo.Source == imFondo4.Source && decremento < 6) //Modo Dificil
            {
                decremento += 0.3;
            }
            else
            {
                //decremento += 0.01;
                
            }
            
            

            //Puntos extra por usar coleccionables
            if (imGafas.IsVisible)
            {
                extra = 5;
            }else if (imGorro.IsVisible)
            {
                extra = 10;
            }

            //Desbloqueo de Trofeos de tiempo
            if(trof3 && tiempo >=60)
            {
                Storyboard sbT60 = (Storyboard)this.Resources["LogroT60"];
                sbT60.Begin();
                imLogro60.Opacity = 100;
                trof3 = false;
            }
            else if(trof4 && tiempo == 360)
            {
                Storyboard sbT360 = (Storyboard)this.Resources["LogroT360"];
                sbT360.Begin();
                imLogro360.Opacity = 100;
                trof4 = false;
            }
            else
            {
                tiempo++;
            }

            if (pbEnergia.Value <= 0 || pbComer.Value <= 0 || pbDiversion.Value <= 0) //Game Over
            {
                this.lbGameOver.Visibility = Visibility.Visible;
                this.lbFrase.Visibility = Visibility.Visible;
                this.lbPuntos.Content = puntos;
                this.lbPuntos.Visibility = Visibility.Visible;
                t1.Stop();
                btComer.IsEnabled = false;
                
                btJugar.IsEnabled = false;
                
                btDescansar.IsEnabled = false;
            }

            // ESTADOS 
            if (pbComer.Value <= 20)
            {
                Storyboard sbHambre = (Storyboard)this.Resources["Hambriento"];
                sbHambre.SpeedRatio = 0.75;
                sbHambre.Begin();
            }
            if (pbDiversion.Value <= 20)
            {
                Storyboard sbAburrido = (Storyboard)this.Resources["Aburrido"];
                
                sbAburrido.SpeedRatio = 0.75;
                sbAburrido.Begin();
            }
            if (pbEnergia.Value <= 20)
            {
                Storyboard sbCansado = (Storyboard)this.Resources["Cansado"];
                sbCansado.SpeedRatio = 0.75;
                sbCansado.Begin();
            }
        }

        //BOTON JUGAR
        private void btJugar_Click(object sender, RoutedEventArgs e)
        {
            if(this.pbDiversion.Value < 100)
            {
                puntos += 10+extra;
                this.pbDiversion.Value += 15;
                trofeoPts();
            }

            Storyboard sbJugar = (Storyboard)this.Resources["Jugar"];
            sbJugar.Completed += new EventHandler(finAccion);
            sbJugar.SpeedRatio = 2;
            sbJugar.Begin();

            //En la definición de la animación
            btJugar.IsEnabled = false;
            btComer.IsEnabled = false;
            btDescansar.IsEnabled = false;
        }

        //BOTON COMER
        private void btComer_Click(object sender, RoutedEventArgs e)
        {
            if(this.pbComer.Value < 100)
            {
                this.pbComer.Value += 15;
                puntos += 10+extra;
                trofeoPts();
            }

            Storyboard sbComer = (Storyboard)this.Resources["Comer"];
            sbComer.Completed += new EventHandler(finAccion);
            sbComer.SpeedRatio = 2.5;
            sbComer.Begin();

            //En la definición de la animación
            btJugar.IsEnabled = false;
            btComer.IsEnabled = false;
            btDescansar.IsEnabled = false;
        }
        
        //BOTON DESCANSAR
        private void btDescansar_Click(object sender, RoutedEventArgs e)
        {
            if(this.pbEnergia.Value < 100)
            {
                this.pbEnergia.Value += 15;
                puntos += 10+extra;
                trofeoPts();
            }

            Storyboard sbDormir = (Storyboard)this.Resources["Dormir"];
            sbDormir.Completed += new EventHandler(finAccion);
            sbDormir.SpeedRatio = 1.75;
            sbDormir.Begin();

            //En la definición de la animación
            btJugar.IsEnabled = false;
            btComer.IsEnabled = false;
            btDescansar.IsEnabled = false;
        }

        //Desbloqueo de Trofeos de puntos
        public void trofeoPts()
        {
            if(trof1 && puntos > 100)
            {
                Storyboard sbP100 = (Storyboard)this.Resources["LogroP100"];
                sbP100.Begin();
                imLogro100.Opacity = 100;
                trof1 = false;

                //animacion Pajaro
                Storyboard sbPajaro = (Storyboard)this.Resources["pajaro"];
                sbPajaro.Begin();
            }
            else if(trof2 && puntos > 500)
            {
                Storyboard sbP500 = (Storyboard)this.Resources["LogroP500"];
                sbP500.Begin();
                imLogro500.Opacity = 100;
                trof2 = false;

                //animacion Mosquitos
                Storyboard sbMosquito = (Storyboard)this.Resources["mosquito"];
                sbMosquito.Begin();
            }
        }

        //BOTON CAMBIAR FONDO
        private void cambiarFondo(object sender, MouseButtonEventArgs e)
        {
            this.imFondo.Source = ((Image)sender).Source;
        }

        //Evento que se ejecuta cuando se completa la animación y habilita los botones
        private void finAccion(object sender, EventArgs e)
        {
            btComer.IsEnabled = true;
            btDescansar.IsEnabled = true;
            btJugar.IsEnabled = true;
        }

        private void acercaDe(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Práctica realizada por:\nJairo Celada Cebrián\n\n ¿Desea Salir?", "IPO2 Tamagotchi",MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        public void setNombre(string nombre)
        {
            this.nombreTamagotchi = nombre;
            this.tbMensajes.Text = "Bienvenido " + nombreTamagotchi;
        }

        private void añadirObjeto(object sender, DragEventArgs e)
        {
            Image aux = (Image)e.Data.GetData(typeof(Image));
            switch (aux.Name)
            {
                case "imGafasNegras":
                    imGafas.Visibility = Visibility.Visible;
                    break;
                case "imGorroNavidad":
                    imGorro.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void inicioArrastrar(object sender, MouseButtonEventArgs e)
        {
            DataObject dataO = new DataObject(((Image)sender));
            DragDrop.DoDragDrop((Image)sender, dataO, DragDropEffects.Move);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query =
            from ranking in dataEntities.ranking
            select new { ranking.id, ranking.nombre, ranking.puntos };

            dataGrid1.ItemsSource = query.ToList();
        }
        
        private void darPremioPajaro(object sender, MouseButtonEventArgs e)
        {
            //Uri resourceUri = new Uri("/pajaro.png", UriKind.Relative);
            //imPajaro.Source = new BitmapImage(resourceUri);
            //imPajaro.IsEnabled = false; 
            imPajaro.Opacity = 100;
            imPulsaPajaro.IsEnabled = false;

            //Premio del Pajaro --> Da aleatoriamente a una de las 3 barras puntos entre 20-50
            var seed = Environment.TickCount; 
            var random = new Random(seed);
            var value1 = random.Next(20, 50);
            var value2 = random.Next(1, 3);
            
            if (value2 == 1)
            {
                pbEnergia.Value += value1;

            }
            else if (value2 == 2)
            {
                pbComer.Value += value1;
            }
            else
            {
                pbDiversion.Value += value1;
            }
            puntos += value1;
            
        }

        private void darPremioMosquito(object sender, MouseButtonEventArgs e)
        {
            //Uri resourceUri = new Uri("/mosquito.png", UriKind.Relative);
            //imMosquito.Source = new BitmapImage(resourceUri);
            //imMosquito.IsEnabled = false;
            imMosquito.Opacity = 100;
            imPulsaMosquito.IsEnabled = false; 

            //Premio del Mosquito --> Da 100 puntos a todos
            pbEnergia.Value = 100; 
            pbComer.Value = 100;
            pbDiversion.Value = 100;
            puntos += 100;
        }

        
    }
}
