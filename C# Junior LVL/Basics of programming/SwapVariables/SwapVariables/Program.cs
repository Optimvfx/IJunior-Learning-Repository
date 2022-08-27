//Task of https://lk.ijunior.ru/Homework/Detail/201

namespace SwapVariables
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var userFamily = "Olexay";
            var userName = "Sahorow";

            //Split user name and family.
            var temp = userName;
            userName = userFamily;
            userFamily = temp;
        }
    }
}
