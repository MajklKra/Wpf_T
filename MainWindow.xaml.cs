using System.IO.Enumeration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using Vozidla;

namespace Wpf_T
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string path = " ";

        SpravceAut SpravceAut = new SpravceAut();

        static List<Vozidlo> vozidla = new List<Vozidlo>();
        List<Vozidlo> vozidla2 = new List<Vozidlo>();
        List<Vozidlo> vozidla3 = new List<Vozidlo>();

        Dictionary<string, double> ceny = new Dictionary<string, double>();
        Dictionary<string, double> dph = new Dictionary<string, double>();

        /// <summary>
        /// Constructor for MainWindow
        /// </summary>

        public MainWindow()
        {
            InitializeComponent();

            AutaDataGrid.ItemsSource = SpravceAut.vozidla2;
            AutaDataGrid2.ItemsSource = SpravceAut.result;

            Vikend();

            Cena();

            Vypis();

        }

        private void AutaDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// Method which check if date is at wekeends or not
        /// </summary>

        public void Vikend()
        {

            foreach (var v in vozidla2)

            {
                if ((DateTime.Parse(v.Datum).DayOfWeek == DayOfWeek.Saturday) || (DateTime.Parse(v.Datum).DayOfWeek == DayOfWeek.Sunday))
                {
                    vozidla3.Add(v);
                }
            }

        }

        /// <summary>
        /// Method which states prices for each vehicle
        /// </summary>

        public void Cena()
        {
            double sum = 0;
            double sumDPH = 0;

            List<string> vyrobky = new List<string>();

            foreach (var v in vozidla3)
            {
                vyrobky.Add(v.Nazev);
            }

            vyrobky = vyrobky.Distinct().ToList();

            Console.WriteLine();

            foreach (var s in vyrobky)
            {
                sum = 0;
                sumDPH = 0;

                foreach (var v in vozidla3)
                {
                    if (v.Nazev == s)
                    {
                        sum = sum + v.Cena;
                        sumDPH = sumDPH + (v.Cena + (v.Cena * (v.Dph / 100)));
                    }
                }

                ceny.Add(s, sum);
                dph.Add(s, sumDPH);
            }

        }

        /// <summary>
        /// Method which prints prices for vehicles 
        /// </summary>

        public void Vypis()
        {
            Console.WriteLine();
            Console.WriteLine("Vypis vozidel prodanych o vikendech ");
            Console.WriteLine("------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Nazev modelu  Celkova cena   Cena s DPH ");

            for (int i = 0; i < ceny.Count; i++)
            {
                Console.Write(ceny.ElementAt(i).Key + "  ");
                Console.Write(ceny.ElementAt(i).Value + "  ");
                Console.WriteLine(dph.ElementAt(i).Value);
            }
        }

        /// <summary>
        /// Method which onclick of button opens dialog and enable the user to choose a file
        /// </summary>


        public void btnNeco_Click(object sender, RoutedEventArgs e)
        {
          
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".xml"; // Default file extension
            dialog.Filter = "XML documents (.xml)|*.xml"; // Filter files by extension

            string filename = " ";

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                filename = dialog.FileName;

                SpravceAut SpravceAut = new SpravceAut(filename);

                InitializeComponent();

                AutaDataGrid.ItemsSource = SpravceAut.vozidla2;
                AutaDataGrid2.ItemsSource = SpravceAut.result;
            }

        }
    }
}