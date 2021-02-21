using System;
using System.Collections.Generic;
using System.IO;

namespace Hesoyam
{
    public static class FileHelper
    {
        public static readonly string fileName = "Doctors.bin";
        public static void WriteFile(List<Doctor> doctors)
        {
            using(FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                using (BinaryWriter bw = new BinaryWriter(fileStream))
                {
                    bw.Write(doctors.Count);
                    foreach (var doctor in doctors)
                    {
                        bw.Write(doctor.Name);
                        bw.Write(doctor.Surname);
                        bw.Write(doctor.ExperienceYear);
                        bw.Write(doctor.Branch);

                        bw.Write(doctor.GetAppointmentsCount());                     
                        foreach (var appointment in doctor.GetAppointments())
                        {
                            bw.Write(appointment.Key.ToShortDateString());                            
                            for (int i = 0; i < appointment.Value.Count; i++)
                            {
                                bw.Write(appointment.Value[i]);
                            }
                        }

                    }
                }
            }
        }

        public static void ReadFile(List<Doctor> doctors)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                using (BinaryReader br = new BinaryReader(fileStream))
                {
                    int length = br.ReadInt32();
                    for (int i = 0; i < length; i++)
                    {
                        Doctor doctor = new Doctor
                        {
                            Name = br.ReadString(),
                            Surname = br.ReadString(),
                            ExperienceYear = br.ReadString(),
                            Branch = br.ReadString()
                        };

                        int number = br.ReadInt32();

                        for (int j = 0; j < number; j++)
                        {
                            string inputDate = br.ReadString();
                            DateTime date = Convert.ToDateTime(inputDate);
                            doctor.AddDefaultAppointment(date);

                            List<bool> temp = new List<bool>();

                            const int count = 3;
                            for (int k = 0; k < count; k++)
                            {
                                temp.Add(br.ReadBoolean());
                            }
                            doctor.AddSequence(date, temp);
                        }                        

                        doctors.Add(doctor);
                    }
                }
            }
        }

    }
}