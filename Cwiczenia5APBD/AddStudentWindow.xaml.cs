using Cwiczenia5APBD.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Cwiczenia5APBD
{
    /// <summary>
    /// Interaction logic for AddStudentWindow.xaml
    /// </summary>
    public partial class AddStudentWindow : Window
    {
        MainWindow parentWindow;
        Student stud = null;


        public AddStudentWindow(MainWindow parent)
        {
            InitializeComponent();
            this.parentWindow = parent;
            initComboBox();
        }

        public AddStudentWindow(MainWindow parent,Student st)
        {
            this.stud = st;
            InitializeComponent();
            this.parentWindow = parent;
            initComboBox();

            imieTextBox.Text = st.imie;
            nazwiskoTextBox.Text = st.nazwisko;
            nrIndeksuTextBox.Text = st.nrindeksu;
            adresTextBox.Text = st.adress.ToString();
            
            if(st.wybranePrzedmioty!=null)
            foreach(String s in st.wybranePrzedmioty)
                przedmiotyListBox.SelectedItems.Add(s);
            
        }

        private void initComboBox()
        {
            comboBoxStudia.ItemsSource = parentWindow.getStudiesList();
            przedmiotyListBox.ItemsSource = parentWindow.getSubjectList();
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            imieTextBox.Text = "";
            nazwiskoTextBox.Text = "";
            nrIndeksuTextBox.Text = "";
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            //sprawdzenie s12345
            Regex regex = new Regex(@"^s\d{5}");
            MatchCollection matches = regex.Matches(nrIndeksuTextBox.Text);
            if (matches.Count == 0)
            {
                MessageBox.Show("Podany nieprawidlowy numer indeksu");
                return;
            }

            //Wybieramy idStudia
            String idstudiaa = "";
            try
            {
                //idstudia (number)
                switch (comboBoxStudia.SelectedItem.ToString())
                {
                    case "Informatyka": idstudiaa = "1"; break;
                    case "Design": idstudiaa = "2"; break;
                    case "Zarzadzanie": idstudiaa = "3"; break;
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex);
            }

            if (stud == null)
                stud = new Student();

            stud.imie = imieTextBox.Text; ;
            stud.nazwisko = nazwiskoTextBox.Text; 
            stud.nrindeksu = nrIndeksuTextBox.Text;
            stud.idstudia = idstudiaa;
            stud.adress = adresTextBox.Text;
            stud.wybranePrzedmioty = przedmiotyListBox.SelectedItems.Cast<String>().ToList();

            parentWindow.addNewStudent(stud);
            Close();
        }
    }
}
