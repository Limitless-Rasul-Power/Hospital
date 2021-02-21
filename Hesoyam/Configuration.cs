using System;
using System.Collections.Generic;

namespace Hesoyam
{
    public static class Configuration
    {
        public static string[] GetBranch()
        {
            return new string[] { Branch.Pediatria, Branch.Stomotology, Branch.Traumatology, "Exit" };
        }

        public static string[] GetDoctorMenu()
        {
            return new string[] { "Get Appointment", "Show doctor's reserved appointment days and times", "Exit" };
        }

        public static string[] GetHeadMenu()
        {
            return new string[] { "Director", "Patient", "Exit" };
        }
        public static void PrintMenu(string[] menu)
        {
            for (int i = 0; i < menu.Length; i++)
            {
                Console.WriteLine($"{i + 1}) {menu[i]}");
            }
            Console.WriteLine();
        }

        public static string[] GetDirectorMenu()
        {
            return new string[] { "Show All Doctors", "Add Doctor", "Remove Doctor", "Exit" };
        }               

    
        public static List<Doctor> GetDoctors()
        {

            Doctor d1 = new Doctor("Marissa", "Peer", "25", Branch.Pediatria);
            Doctor d2 = new Doctor("Mike", "Tyson", "32", Branch.Traumatology);
            Doctor d3 = new Doctor("George", "Tall", "25", Branch.Stomotology);
            Doctor d4 = new Doctor("Leo", "Galante", "28", Branch.Traumatology);
            Doctor d5 = new Doctor("Michael", "Corle", "29", Branch.Pediatria);
            Doctor d6 = new Doctor("Jane", "Sweet", "22", Branch.Stomotology);

            return new List<Doctor> { d1, d2, d3, d4, d5, d6 };

        }

    }
}