using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hesoyam
{
    public class Doctor : Person
    {
        private string _experienceYear;
        private string _branch;
        private readonly Dictionary<DateTime, List<bool>> appointment = new Dictionary<DateTime, List<bool>>();
        public static readonly string[] workHours = { "09:00 - 11:00", "12:00 - 14:00", "15:00 - 17:00" };

        public Doctor()
        {

        }
        public Doctor(in string name, in string surname, in string experienceYear, in string branch)
            : base(name, surname)
        {
            ExperienceYear = experienceYear;
            Branch = branch;
        }

        public string Branch
        {
            get
            {
                return _branch;
            }
            set
            {
                _branch = Verify.IsDataCorrectFormat(value) && Verify.IsValidBranch(value) ? value : throw new InvalidOperationException("Name is null or white space or contains integer or invalid branch");
            }
        }
        public string ExperienceYear
        {
            get
            {
                return _experienceYear;
            }
            set
            {
                _experienceYear = Verify.IsDataContainsOnlyIntegers(value) ? value : throw new InvalidOperationException("Phone number must contain only letters"); ;
            }
        }

        public bool IsAlreadyReserved(DateTime date, int index)
        {
            return appointment.ContainsKey(date) && appointment[date][index - 1];
        }

        public bool IsKeyExist(DateTime date)
        {
            return appointment.ContainsKey(date);
        }
        public void PrintReservaitonDays(DateTime date)
        {
            for (int time = 0; time < appointment[date].Count; time++)
            {
                if (appointment[date][time])
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{time + 1}) {workHours[time]} Reserved");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{time + 1}) {workHours[time]} Not Reserved");
                }
            }
            Console.ResetColor();

            Console.WriteLine("\n==================================\n");
        }

        public void AddDefaultAppointment(DateTime date)
        {
            if (!appointment.ContainsKey(date))
                appointment.Add(date, new List<bool> { false, false, false });
        }

        public void AddSequence(DateTime date, List<bool> times)
        {
            if(appointment.ContainsKey(date))
            {
                for (int i = 0; i < times.Count; i++)
                {
                    appointment[date][i] = times[i];
                }
            }
        }
        public void AddAppointment(DateTime date, int index)
        {
            if (index > 3 || index < 1)
                throw new InvalidOperationException("Index doesn't exist in this context.");

            if (IsAlreadyReserved(date, index))
                throw new InvalidOperationException("This date is reserved.");

            if (!appointment.ContainsKey(date))
            {
                appointment.Add(date, new List<bool>());
                for (int i = 0; i < 3; i++)
                {
                    if (index == i + 1)
                        appointment[date].Add(true);
                    else
                        appointment[date].Add(false);
                }
            }
            else
            {
                appointment[date][index - 1] = true;
            }
        } 
        public Dictionary<DateTime, List<bool>> GetAppointments()
        {
            return appointment;
        }
        public int GetAppointmentsCount()
        {
            return appointment.Count;
        }
        public bool IsDoctorHasAnyAppointment()
        {
            return appointment.Count > 0;
        }
        public void ShowReservedDatesAndTimes()
        {
            Show();
            Console.WriteLine("Appointments\n");
            foreach (var date in appointment.Keys)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Appointment Date: {date.ToLongDateString()}");
                PrintReservaitonDays(date);
            }
            Console.ResetColor();

        }

        public void Show()
        {
            Console.WriteLine($"Doctor ID: {ID}, Name: {Name}, Surname: {Surname}, Experience Year: {ExperienceYear}\n");
            Console.WriteLine("=====================================");
        }

    }
}