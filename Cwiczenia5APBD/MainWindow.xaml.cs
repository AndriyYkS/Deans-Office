using Cwiczenia5APBD.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cwiczenia5APBD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StudentsDBService sdbs;

        public MainWindow()
        {
            InitializeComponent();
            //1
            sdbs = new StudentsDBService();
            gridOfStudents.ItemsSource = sdbs.GetStudentList();

        }

        public object MessageBoxButtons { get; private set; }


        private void Usun_Button_Click(object sender, RoutedEventArgs e)
        {
            var selected = gridOfStudents.SelectedItems.Cast<Student>();
            List<Student> selectedStudentList = selected.ToList<Student>();

            //2.4
            String result = MessageBox.Show("Do you really want to delete?", "Warning", MessageBoxButton.YesNoCancel).ToString();

            if (result == "Yes")
                foreach (Student st in selectedStudentList)
                    sdbs.removeStudent(st);
            else return;

        }

        //3
        private void Dodaj_Button_Click(object sender, RoutedEventArgs e)
        {
            AddStudentWindow win = new AddStudentWindow(this);
            win.Show();
        }

        public List<String> getStudiesList()
        {
            return sdbs.getStudiesList();
        }

        public List<String> getSubjectList()
        {
            return sdbs.getSubjectList();
        }

        public void addNewStudent(Student newSt)
        {
            int abstractId = 0;

            //if edit or create new
            if (newSt.idStudent != 0)
                sdbs.editStudent(newSt);
            else
                abstractId = sdbs.addStudent(newSt);

            //dodajemy Przedmioty
            foreach(String sub in newSt.wybranePrzedmioty)
                sdbs.addStudentSubjects(abstractId, sub);

            //??? jak odswiezycz
            gridOfStudents.ItemsSource = sdbs.GetStudentList();
        }

        private void GridOfStudents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddStudentWindow adsw = new AddStudentWindow(this, (Student)gridOfStudents.SelectedItem);
            adsw.Show();
        }

        private void GridOfStudents_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var list = gridOfStudents.SelectedItems.Cast<Object>();
            XLabel.Content="Wybrałeś "+list.Count()+" studentów";
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
