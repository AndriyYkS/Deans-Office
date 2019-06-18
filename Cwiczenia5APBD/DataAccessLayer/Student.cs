using System;
using System.Collections.Generic;

namespace Cwiczenia5APBD.DataAccessLayer
{
    public class Student
    {
        public int idStudent { get; set; }
        public string imie { get; set; }//
        public string nazwisko { get; set; }//
        public string adress { get; set; }
        public string nrindeksu { get; set; }//
        public string idstudia { get; set; }//
        public List<String> wybranePrzedmioty;

        public override string ToString()
        {
            return idStudent.ToString() + " " + imie.ToString() + " "+ nazwisko.ToString() + " " + adress.ToString() + " " + nrindeksu.ToString() + " " + idstudia.ToString();       
      }

    }

}