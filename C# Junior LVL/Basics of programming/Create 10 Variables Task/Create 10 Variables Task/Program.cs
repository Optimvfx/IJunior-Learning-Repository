
// Task of https://lk.ijunior.ru/Homework/Detail/26
using System;

namespace Create_10_Variables_Task
{
    public static class Program
    {
        static void Main(string[] args)
        {
            uint MaximalHealth = 200;
            int CurrentHealth = (int)MaximalHealth;

            byte CharByteCode = (byte)'h';

            sbyte ColorPaletteRedForce = 42;

            long UserCardNumber = 4127631616283717;
            string UserName = "Stepan Valeriev";

            float PlayerMoveSpeed = 15f;
            double GrawityForce = 9.8;

            short ExceptedRegistrationConfirmationCode = 1526;
            var registrationConfirmationCode = 1526;
            bool RegistrationCompletedSuccessfully = registrationConfirmationCode == ExceptedRegistrationConfirmationCode;
        }
    }   
}
