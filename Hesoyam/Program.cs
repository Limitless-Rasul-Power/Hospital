using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hesoyam
{
    class Program
    {
        static void Main(string[] args)
        {
            Director director = new Director("one", "day");            
            Section section = new Section();

            if (!File.Exists(FileHelper.fileName))
            {
                List<Doctor> doctors = Configuration.GetDoctors();
                for (int i = 0; i < doctors.Count; i++)
                    section.AddDoctor(doctors[i]);
            }
            else
            {
                section.ReadDoctorsInfoFromFile();
            }

            string[] category = Configuration.GetBranch();
            string[] doctorMenu = Configuration.GetDoctorMenu();
            string[] directorMenu = Configuration.GetDirectorMenu();
            string[] headMenu = Configuration.GetHeadMenu();

            string name = null, surname = null, choice = null;


            while (true)
            {
                Configuration.PrintMenu(headMenu);
                Console.Write("Enter choice: ");
                choice = Console.ReadLine();

                while (!Verify.IsCorrectChoice(choice, headMenu.Length))
                {
                    Console.WriteLine("\nEnter one of this chocies: ");
                    choice = Console.ReadLine();
                }
                Console.Clear();


                if (choice == HeadMenuChoices.Exit)
                {
                    MessageBox.Show("See you next time goodbye", HospitalInfo.Name, MessageBoxButtons.OK);
                    section.WriteDoctorsInfoFile();
                    break;
                }

                Console.Write("Enter your name: ");
                name = Console.ReadLine();
                Console.Write("Enter your surname: ");
                surname = Console.ReadLine();

                Console.Clear();

                if (choice == HeadMenuChoices.Director)
                {
                    while (true)
                    {
                        if (director.Name == name && director.Surname == surname)
                        {
                            Console.WriteLine($"Hello {name} {surname} which operation do you want: ");
                            while (true)
                            {
                                Configuration.PrintMenu(directorMenu);
                                Console.Write("Enter choice: ");
                                choice = Console.ReadLine();

                                while (!Verify.IsCorrectChoice(choice, directorMenu.Length))
                                {
                                    Console.WriteLine("\nEnter one of this choices: ");
                                    choice = Console.ReadLine();
                                }
                                Console.Clear();

                                if (choice == DirectorMenuChoices.Exit)
                                {
                                    MessageBox.Show("See you next time goodbye.", HospitalInfo.Name, MessageBoxButtons.OK);
                                    break;
                                }

                                switch (choice)
                                {
                                    case DirectorMenuChoices.ShowAllDoctors:
                                        {
                                            if (!section.IsDoctorExits())
                                                MessageBox.Show($"There is no doctor in the {HospitalInfo.Name}", HospitalInfo.Name, MessageBoxButtons.OK);
                                            else
                                            {
                                                section.ShowAllDoctors();
                                                MessageBox.Show("Press Ok to continue", HospitalInfo.Name, MessageBoxButtons.OK);
                                            }
                                        }
                                        break;

                                    case DirectorMenuChoices.AddDoctor:
                                        {
                                            Console.Write("Enter doctor's name: ");
                                            name = Console.ReadLine();
                                            Console.Write("Enter doctor's surname: ");
                                            surname = Console.ReadLine();
                                            Console.Write("Enter doctor's experience year: ");
                                            string experienceYear = Console.ReadLine();

                                            while (!Verify.IsDataContainsOnlyIntegers(experienceYear))
                                            {
                                                Console.WriteLine("\nExperience year must containd only integer: ");
                                                experienceYear = Console.ReadLine();
                                            }

                                            Console.WriteLine();
                                            for (int i = 0; i < category.Length - 1; i++)
                                                Console.WriteLine($"{i + 1}.{category[i]}");

                                            Console.Write("\nEnter branch with number: ");
                                            choice = Console.ReadLine();

                                            while (!Verify.IsCorrectChoice(choice, category.Length - 1))
                                            {
                                                Console.WriteLine("\nEnter correct branch with number: ");
                                                choice = Console.ReadLine();
                                            }
                                            Console.Clear();

                                            try
                                            {
                                                Doctor doctor = new Doctor(name, surname, experienceYear, category[int.Parse(choice) - 1]);
                                                section.AddDoctor(doctor);
                                                MessageBox.Show($"Doctor {name} {surname} added successfully", HospitalInfo.Name, MessageBoxButtons.OK);
                                            }
                                            catch (Exception)
                                            {
                                                MessageBox.Show("There is wrong credential in doctor's data and didn't added", HospitalInfo.Name, MessageBoxButtons.OK);
                                            }

                                        }
                                        break;

                                    case DirectorMenuChoices.RemoveDoctor:
                                        {
                                            if (!section.IsDoctorExits())
                                                MessageBox.Show($"There is no doctor in the {HospitalInfo.Name}", HospitalInfo.Name, MessageBoxButtons.OK);
                                            else
                                            {
                                                section.ShowAllDoctors();
                                                Console.Write("Enter which ID doctor do you want to delete: ");
                                                string removedId = Console.ReadLine();

                                                while (!Verify.IsDataContainsOnlyIntegers(removedId))
                                                {
                                                    Console.WriteLine("\nEnter number: ");
                                                    removedId = Console.ReadLine();
                                                }
                                                Console.Clear();

                                                try
                                                {
                                                    section.DeleteDoctorWithID(int.Parse(removedId));
                                                    Console.Clear();
                                                    MessageBox.Show($"Doctor with id {removedId} removed successfully", HospitalInfo.Name, MessageBoxButtons.OK);
                                                }
                                                catch (InvalidOperationException caption)
                                                {
                                                    MessageBox.Show(caption.Message, HospitalInfo.Name, MessageBoxButtons.OK);
                                                }
                                            }
                                        }
                                        break;
                                }
                                Console.Clear();
                            }
                            if (choice == DirectorMenuChoices.Exit)
                                break;
                        }
                        else
                        {
                            MessageBox.Show($"Wrong director credential", HospitalInfo.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            break;
                        }
                    }
                }
                else
                {
                    if (!section.IsDoctorExits())
                        MessageBox.Show("We are new in this industry we have no doctors yet coming soon...", HospitalInfo.Name, MessageBoxButtons.OK);

                    else
                    {
                        Console.Write("Enter your email: ");
                        string email = Console.ReadLine();
                        Console.Write("Enter your phone number: ");
                        string telephone = Console.ReadLine();

                        try
                        {
                            while (true)
                            {
                                Console.Clear();

                                User user = new User(name, surname, email, telephone);
                                Console.WriteLine($"Welcome {name} {surname} to {HospitalInfo.Name}, which branch do you want enter number: ");
                                Configuration.PrintMenu(category);
                                Console.Write("Enter option ");
                                choice = Console.ReadLine();

                                while (!Verify.IsCorrectChoice(choice, category.Length))
                                {
                                    Console.WriteLine("\nEnter correct number: ");
                                    choice = Console.ReadLine();
                                }
                                Console.Clear();

                                if (choice == DoctorMenuChoices.Exit)
                                {
                                    MessageBox.Show("Goodbye take care of yourself", HospitalInfo.Name, MessageBoxButtons.OK);
                                    break;
                                }

                                choice = category[int.Parse(choice) - 1];

                                while (true)
                                {
                                    int currentID = section.GetCurrentID() + 1;
                                    section.FilterByBranch(choice);
                                    Console.Write(new string(' ', (Console.WindowWidth - $"Enter {currentID} to go Back".Length) / 2));
                                    Console.WriteLine($"Enter {currentID} to go Back\n");
                                    Console.Write($"{name} {surname} which doctor do you choose to go to next step: ");
                                    string option = Console.ReadLine();

                                    while (!Verify.IsDataContainsOnlyIntegers(option))
                                    {
                                        Console.WriteLine("\nEnter one of this ID's: ");
                                        option = Console.ReadLine();
                                    }
                                    Console.Clear();

                                    int id = int.Parse(option);

                                    if (id == currentID)
                                        break;

                                    try
                                    {
                                        while (true)
                                        {
                                            Doctor doctor = section.FindDoctorWithBranchAndID(choice, id);

                                            Console.WriteLine($"Doctor = {doctor.Name} {doctor.Surname}\n");
                                            Configuration.PrintMenu(doctorMenu);
                                            Console.Write("\nEnter number: ");
                                            option = Console.ReadLine();

                                            while (!Verify.IsCorrectChoice(option, doctorMenu.Length))
                                            {
                                                Console.WriteLine("\nEnter one of this choices: ");
                                                option = Console.ReadLine();
                                            }
                                            Console.Clear();


                                            if (option == DoctorMenuChoices.Back)
                                                break;
                                            switch (option)
                                            {
                                                case DoctorMenuChoices.GetAppointment:
                                                    {
                                                        Console.Write("Enter appointment year: ");
                                                        string year = Console.ReadLine();

                                                        Console.Write("Enter appointment month with number: ");
                                                        string month = Console.ReadLine();

                                                        Console.Write("Enter appointment day: ");
                                                        string day = Console.ReadLine();


                                                        try
                                                        {
                                                            DateTime date = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
                                                            if (!Verify.IsDateCorrect(date))
                                                                throw new InvalidOperationException("Date must be present or future.");

                                                            if (!doctor.IsKeyExist(date))
                                                            {
                                                                doctor.AddDefaultAppointment(date);
                                                            }

                                                            Console.Clear();
                                                            Console.WriteLine($"{date.ToLongDateString()}\n");
                                                            doctor.PrintReservaitonDays(date);
                                                            Console.Write("Enter time option: ");
                                                            option = Console.ReadLine();

                                                            while (!Verify.IsCorrectChoice(option, Doctor.workHours.Length))
                                                            {
                                                                Console.WriteLine("\nEnter correct time option: ");
                                                                option = Console.ReadLine();
                                                            }
                                                            Console.Clear();

                                                            if (!doctor.IsAlreadyReserved(date, int.Parse(option)))
                                                            {
                                                                MessageBox.Show($"{name} {surname} you reserved {Doctor.workHours[int.Parse(option) - 1]} o'clock with {doctor.Name} {doctor.Surname} doctor", HospitalInfo.Name, MessageBoxButtons.OK);
                                                                doctor.AddAppointment(date, int.Parse(option));
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show($"{Doctor.workHours[int.Parse(option) - 1]} is reserved by someone.", HospitalInfo.Name, MessageBoxButtons.OK);
                                                            }
                                                            Console.Clear();

                                                        }
                                                        catch (Exception caption)
                                                        {
                                                            Console.Clear();
                                                            MessageBox.Show(caption.Message, HospitalInfo.Name, MessageBoxButtons.OK);
                                                        }

                                                    }
                                                    break;

                                                case DoctorMenuChoices.ShowReservedDateAndTimes:
                                                    {
                                                        if (!doctor.IsDoctorHasAnyAppointment())
                                                            MessageBox.Show($"Doctor {doctor.Name} {doctor.Surname} doest't have any appointment yet", HospitalInfo.Name, MessageBoxButtons.OK);
                                                        else
                                                        {
                                                            doctor.ShowReservedDatesAndTimes();
                                                            MessageBox.Show("All done", HospitalInfo.Name, MessageBoxButtons.OK);
                                                            Console.Clear();
                                                        }

                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                    catch (InvalidOperationException caption)
                                    {
                                        MessageBox.Show(caption.Message, HospitalInfo.Name, MessageBoxButtons.OK);
                                        Console.Clear();
                                    }


                                }
                            }


                        }
                        catch (Exception caption)
                        {
                            Console.Clear();
                            MessageBox.Show(caption.Message, HospitalInfo.Name, MessageBoxButtons.OK);
                        }
                    }
                 
                }




            }
        }
    }
}