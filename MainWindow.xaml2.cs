using System.IO;
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

namespace DolgozatWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public List<Dolgozat> Dolgozatok { get; set; } = new List<Dolgozat>();
    public MainWindow()
    {
        InitializeComponent();
        Betoltes();
        DolgozatokGrid.ItemsSource = Dolgozatok;
    }
    public class Dolgozat
    {
        public string Nev { get; set; }
        public int Eletkor { get; set; }
        public int Pontszam { get; set; }

        public Dolgozat(string nev, int eletkor, int pontszam)
        {
            Nev = nev;
            Eletkor = eletkor;
            Pontszam = pontszam;
        }
    }
    private void Betoltes()
    {  
       var sorok = File.ReadAllLines("dolgozatok.txt");
       foreach (var sor in sorok)
       {
          var adatok = sor.Split(';');
          Dolgozatok.Add(new Dolgozat(adatok[0], int.Parse(adatok[1]), int.Parse(adatok[2])));
       }
    }

    private void MentesButton_Click(object sender, RoutedEventArgs e)
    {
        var sorok = Dolgozatok.Select(d => $"{d.Nev};{d.Eletkor};{d.Pontszam}");
        File.WriteAllLines("dolgozatok.txt", sorok);
        MessageBox.Show("Adatok sikeresen elmentve!");
    }
    private void HozzaadasButton_Click(object sender, RoutedEventArgs e)
    {
        var nev = NevTextBox.Text;
        var eletkor = int.Parse(EletkorTextBox.Text);
        var pontszam = int.Parse(PontszamTextBox.Text);

        var ujDolgozat = new Dolgozat(nev, eletkor, pontszam);
        Dolgozatok.Add(ujDolgozat);
        DolgozatokGrid.Items.Refresh();
    }
}