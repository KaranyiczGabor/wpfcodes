using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FürdőLibrary; // Használj namespace-t, ami a Furdo osztályé

namespace StrandWPF
{
    public partial class MainWindow : Window
    {
        List<Furdo> furdoLista = new();

        public MainWindow()
        {
            InitializeComponent();
            BetoltAdatok();
        }

        private void BetoltAdatok()
        {
            var sorok = File.ReadAllLines("strandadatok.txt").Skip(1); // első sor: fejléc
            furdoLista = sorok.Select(s => new Furdo(s)).ToList();
            lvFurdok.ItemsSource = furdoLista;
        }

        private void lvFurdok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvFurdok.SelectedItem is Furdo kivalasztott)
            {
                txtNev.Text = kivalasztott.Nev;
                txtCim.Text = kivalasztott.Cim;
                txtAr.Text = kivalasztott.Ar.ToString();
                txtVizhofok.Text = kivalasztott.Vizhofok.ToString();
                pbVizhofok.Value = kivalasztott.Vizhofok;
            }
        }

        private void btnMentes_Click(object sender, RoutedEventArgs e)
        {
            if (lvFurdok.SelectedItem is not Furdo kivalasztott)
            {
                MessageBox.Show("Nem menthető, amíg nincs kiválasztva semmi!");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = kivalasztott.Nev;
            dlg.Filter = "Szövegfájl (*.txt)|*.txt";

            if (dlg.ShowDialog() == true)
            {
                File.WriteAllText(dlg.FileName, kivalasztott.ToString());
                MessageBox.Show("Sikeres mentés!");
            }
        }
    }
}
