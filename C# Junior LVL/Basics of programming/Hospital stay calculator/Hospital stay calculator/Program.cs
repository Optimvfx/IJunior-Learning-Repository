using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalStayCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var doctorAppointmentTimeInMinutes = 10;
            var minutesPerHour = 60;

            Console.Write($"Перед вами стоит: ");
            var visitorsBeforeUser = Convert.ToInt32(Console.ReadLine());

            var waitingTimeInMinutes = visitorsBeforeUser * doctorAppointmentTimeInMinutes;

            var roundedWaitingTimeInMinutes = waitingTimeInMinutes % minutesPerHour;
            var waitingTimeInHours = waitingTimeInMinutes / minutesPerHour;

            Console.WriteLine($"Вы будете ждать в очереди {waitingTimeInHours}:{roundedWaitingTimeInMinutes}.");
            Console.ReadKey();
        }
    }
}
