using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DmvAppointmentScheduler
{
    class Program
    {
        public static Random random = new Random();
        public static List<Appointment> appointmentList = new List<Appointment>();
        static void Main(string[] args)
        {
            CustomerList customers = ReadCustomerData();
            TellerList tellers = ReadTellerData();
            Calculation(customers, tellers);
            OutputTotalLengthToConsole();

        }
        private static CustomerList ReadCustomerData()
        {
            string fileName = "CustomerData.json";
            string path = Path.Combine(Environment.CurrentDirectory, @"InputData\", fileName);
            string jsonString = File.ReadAllText(path);
            CustomerList customerData = JsonConvert.DeserializeObject<CustomerList>(jsonString);
            return customerData;

        }
        private static TellerList ReadTellerData()
        {
            string fileName = "TellerData.json";
            string path = Path.Combine(Environment.CurrentDirectory, @"InputData\", fileName);
            string jsonString = File.ReadAllText(path);
            TellerList tellerData = JsonConvert.DeserializeObject<TellerList>(jsonString);
            return tellerData;

        }
        static void Calculation(CustomerList customers, TellerList tellers)
        {
            // Your code goes here .....
            // Test Comment
            // Re-write this method to be more efficient instead of a assigning all customers to the same teller
            /*            foreach (Customer customer in customers.Customer)
                        {
                            var appointment = new Appointment(customer, tellers.Teller[0]);
                            appointmentList.Add(appointment);
                        }*/
            //1005
            List<Teller> specialtyOne = new List<Teller>();
            List<Teller> specialtyZero = new List<Teller>();
            List<Teller> specialtyTwo = new List<Teller>();
            List<Teller> specialtyThree = new List<Teller>();
            foreach (Teller teller in tellers.Teller)
            {
                if (teller.specialtyType == "0")
                {
                    specialtyZero.Add(teller);
                }
                if (teller.specialtyType == "1")
                {
                    specialtyOne.Add(teller);
                }
                if (teller.specialtyType == "2")
                {
                    specialtyTwo.Add(teller);
                }
                if (teller.specialtyType == "3")
                {
                    specialtyThree.Add(teller);
                }
            }
            // Your code goes here .....
            // Re-write this method to be more efficient instead of a random assignment
            int i = 0;
            int j = 0;
            int k = 0;
            int l = 0;
            foreach (Customer customer in customers.Customer)
            {

                if (customer.type == "4")
                {
                    var appointmentZero = new Appointment(customer, specialtyZero[i]);
                    appointmentList.Add(appointmentZero);
                    i++;
                    if (i >= specialtyZero.Count)
                    {
                        i = 0;
                    }
                }
                if (customer.type == "1")
                {
                    var appointmentOne = new Appointment(customer, specialtyOne[j]);
                    appointmentList.Add(appointmentOne);
                    j++;
                    if (j >= specialtyOne.Count)
                    {
                        j = 0;
                    }
                }
                if (customer.type == "2")
                {
                    var appointmentTwo = new Appointment(customer, specialtyTwo[k]);
                    appointmentList.Add(appointmentTwo);
                    k++;
                    if (k >= specialtyTwo.Count)
                    {
                        k = 0;
                    }
                }
                if (customer.type == "3")
                {
                    var appointmentThree = new Appointment(customer, specialtyThree[l]);
                    appointmentList.Add(appointmentThree);
                    l++;
                    if (l >= specialtyThree.Count)
                    {
                        l = 0;
                    }
                }

            }
        }
        static void OutputTotalLengthToConsole()
        {
            var tellerAppointments =
                from appointment in appointmentList
                group appointment by appointment.teller into tellerGroup
                select new
                {
                    teller = tellerGroup.Key,
                    totalDuration = tellerGroup.Sum(x => x.duration),
                };
            var max = tellerAppointments.OrderBy(i => i.totalDuration).LastOrDefault();
            Console.WriteLine("Teller " + max.teller.id + " will work for " + max.totalDuration + " minutes!");
        }

    }
}
