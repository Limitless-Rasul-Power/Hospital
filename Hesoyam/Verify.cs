using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hesoyam
{
    public static class Verify
    {
        public static bool IsValidEmail(in string email)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDataCorrectFormat(in string data)
        {
            return (!string.IsNullOrWhiteSpace(data)) && Regex.IsMatch(data, @"^[a-zA-Z ]+$") && (!data.Any(char.IsDigit));
        }

        public static bool IsDataContainsOnlyIntegers(in string data)
        {
            return data.All(char.IsDigit) && (!String.IsNullOrWhiteSpace(data));
        }

        public static bool IsValidBranch(in string branch)
        {
            return branch == Branch.Pediatria || branch == Branch.Stomotology || branch == Branch.Traumatology;
        }

        public static bool IsCorrectChoice(string choice, int max)
        {
            return (!string.IsNullOrWhiteSpace(choice)) && IsDataContainsOnlyIntegers(choice) && int.Parse(choice) <= max && int.Parse(choice) > 0;
        }

        public static bool IsYearCorrect(string year)
        {
            if((!string.IsNullOrWhiteSpace(year)) && IsDataContainsOnlyIntegers(year))
            {
                return int.Parse(year) >= DateTime.Now.Year && int.Parse(year) <= 9999;
            }
            return false;
        }

        public static bool IsMonthCorrect(string month)
        {
            if ((!string.IsNullOrWhiteSpace(month)) && IsDataContainsOnlyIntegers(month))
            {
                return int.Parse(month) >= 1 && int.Parse(month) <= 12;
            }
            return false;
        }

        public static bool IsDateCorrect(DateTime date)
        {
            if (date.Year == DateTime.Now.Year && date.Month == DateTime.Now.Month && date.Day < DateTime.Now.Day)
                return false;
            else if ((date.Year == DateTime.Now.Year && date.Month < DateTime.Now.Month) || date.Year < DateTime.Now.Year)
                return false;

            return true;
        }

    }
}
