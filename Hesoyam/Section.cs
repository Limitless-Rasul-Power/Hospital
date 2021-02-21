using System;
using System.Collections;
using System.Collections.Generic;

namespace Hesoyam
{
    public class Section
    {
        private readonly List<Doctor> doctors = new List<Doctor>();
        public void AddDoctor(Doctor doctor)
        {
            if (doctor != null)
                doctors.Add(doctor);
        }

        public int GetCurrentID()
        {
            return doctors[doctors.Count - 1].ID;
        }
        private bool IdExist(int id)
        {
            for (int i = 0; i < doctors.Count; i++)
            {
                if (doctors[i].ID == id)
                    return true;
            }
            return false;
        }

        public bool IsDoctorExits()
        {
            return doctors.Count > 0;
        }
        public void DeleteDoctorWithID(int id)
        {
            if (id < 0 || id > doctors[doctors.Count - 1].ID || (!IdExist(id)))
                throw new InvalidOperationException("Wrong id.");

            string info = doctors.Remove(doctors.Find(d => d.ID == id)) ? "Removed Successfully" : "Problem with delete operation";
            Console.WriteLine(info);
        }

        public void ShowAllDoctors()
        {
            doctors.ForEach(d => d.Show());
        }
        public IEnumerable GetDoctors()
        {
            return doctors;
        }

        public void FilterByBranch(string branch)
        {
            if (!String.IsNullOrWhiteSpace(branch))
            {
                List<Doctor> demo = doctors.FindAll(d => d.Branch == branch);
                if (demo != null)
                    demo.ForEach(d => d.Show());
                else
                    throw new InvalidOperationException("There is no doctor in this branch.");
            }
            else
                throw new InvalidOperationException("Branch is wrong.");
        }

        public Doctor FindDoctorWithBranchAndID(string branch, int id)
        {
            if (!String.IsNullOrWhiteSpace(branch))
            {
                Doctor demo = doctors.Find(d => d.ID == id && d.Branch == branch);
                if (demo != null)
                    return demo;

                throw new InvalidOperationException("There is no branch compatible with this ID.");
            }
            throw new InvalidOperationException("Branch did not find.");
        }
        public bool IsBranchAndIDCompatible(string branch, int id)
        {
            if (!String.IsNullOrWhiteSpace(branch))
            {
                Doctor demo = doctors.FindAll(d => d.Branch == branch).Find(d => d.ID == id);
                if (demo != null)
                    return true;

                throw new InvalidOperationException("There is no branch compatible with this ID.");
            }
            return false;
        }

        public void WriteDoctorsInfoFile()
        {
            FileHelper.WriteFile(doctors);
        }

        public void ReadDoctorsInfoFromFile()
        {
            FileHelper.ReadFile(doctors);
        }

    }
}