
// Task of https://lk.ijunior.ru/Homework/Detail/26
namespace Create_10_Variables_Task
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var classWithTenFields = new ClassWithTenFields();

            classWithTenFields.MaximalHealth = 200;
            classWithTenFields.CurrentHealth = (int)classWithTenFields.MaximalHealth;

            classWithTenFields.CharByteCode = (byte)'h';

            classWithTenFields.ColorPaletteRedForce = 42;

            classWithTenFields.UserCardNumber = 4127631616283717;
            classWithTenFields.UserName = "Stepan Valeriev";

            classWithTenFields.PlayerMoveSpeed = 15f;
            classWithTenFields.GrawityForce = 9.8;

            classWithTenFields.ExceptedRegistrationConfirmationCode = 1526;
            var registrationConfirmationCode = 1526;
            classWithTenFields.RegistrationCompletedSuccessfully = registrationConfirmationCode == classWithTenFields.ExceptedRegistrationConfirmationCode;
        }

        protected class ClassWithTenFields
        {
            public uint MaximalHealth { get; set; }
            public int CurrentHealth { get; set; }

            public byte CharByteCode { get; set; }

            public sbyte ColorPaletteRedForce { get; set; }

            public long UserCardNumber { get; set; }
            public string UserName { get; set; }

            public float PlayerMoveSpeed { get; set; }
            public double GrawityForce { get; set; }

            public short ExceptedRegistrationConfirmationCode { get; set; }
            public bool RegistrationCompletedSuccessfully { get; set; }
        }
    }   
}
