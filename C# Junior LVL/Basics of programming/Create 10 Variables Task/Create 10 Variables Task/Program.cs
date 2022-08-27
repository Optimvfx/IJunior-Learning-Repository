// Task of https://lk.ijunior.ru/Homework/Detail/26
using System;

namespace Create_10_Variables_Task
{
    public static class Program
    {
        static void Main(string[] args)
        {
            uint maximalHealth = 200;
            int currentHealth = (int)maximalHealth;

            byte charByteCode = (byte)'h';

            sbyte colorPaletteRedForce = 42;

            long userCardNumber = 4127631616283717;
            string userName = "Stepan Valeriev";

            float playerMoveSpeed = 15f;
            double grawityForce = 9.8;

            short exceptedRegistrationConfirmationCode = 1526;
            var registrationConfirmationCode = 1526;
            bool isRegistrationCompletedSuccessfully = registrationConfirmationCode == exceptedRegistrationConfirmationCode;
        }
    }   
}
