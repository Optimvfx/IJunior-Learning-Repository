using System;

//Task of https://lk.ijunior.ru/Homework/Detail/34
namespace MoneyConvertor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region data
            string exitCommand = "YES";
            bool exitCommandEntered = false;

            string usdCurrencieName = "USD";
            string btcCurrencieName = "BTC";
            string ethCurrencieName = "ETH";

            float usdToBtc = 15326f;
            float usdToEth = 4500f;
            float btcToUsd = 0.0001f;
            float btcToEth = 0.20f;
            float ethToBtc = 5f;
            float ethToUsd = 0.002f;
            #endregion data

            var usdBalance = 1000f;
            var btcBalance = 0.1f;
            var ethBalance = 4f;  

            while(exitCommandEntered == false)
            {
                #region exit
                Console.WriteLine("Exit? (YES or NO, other answer is NO)");
                exitCommandEntered = Console.ReadLine().ToUpper() == exitCommand;
                #endregion exit
                
                #region userInput
                Console.WriteLine("\n"+$"On balanse: {usdBalance} USD, {btcBalance} BTC, {ethBalance} ETH.");

                Console.WriteLine("Currencies allowed for convetting: USD BTC ETH"); 

                Console.Write("Convette from: ");
                var currencieFromConvett = Console.ReadLine().ToUpper();

                Console.Write("Convette to: ");
                var currencieToConvett = Console.ReadLine().ToUpper();

                Console.Write("Convette sum: ");
                var convetteSum = Convert.ToSingle(Console.ReadLine());
                #endregion userInput

                #region inVariant
                if (currencieFromConvett == currencieToConvett)
                {
                    Console.WriteLine("Cant convette to self!");
                    continue;
                }

                if(convetteSum <= 0)
                {
                    Console.WriteLine("Cant convette negative sum!");
                    continue;
                }

                if (currencieFromConvett == usdCurrencieName && usdBalance < convetteSum)
                {
                    Console.WriteLine($"Convette sum is larger then Balanse of {usdCurrencieName}");
                    continue;
                }
                if (currencieFromConvett == btcCurrencieName && btcBalance < convetteSum)
                {
                    Console.WriteLine($"Convette sum is larger then Balanse of {btcCurrencieName}");
                    continue;
                }
                if (currencieFromConvett == ethCurrencieName && ethBalance < convetteSum)
                {
                    Console.WriteLine($"Convette sum is larger then Balanse of {ethCurrencieName}");
                    continue;
                }
                #endregion inVariant

                #region logic
                if (currencieFromConvett == usdCurrencieName && currencieToConvett == btcCurrencieName)
                {
                    usdBalance -= convetteSum;
                    btcBalance += convetteSum / usdToBtc;
                }
                else if (currencieFromConvett == usdCurrencieName && currencieToConvett == ethCurrencieName)
                {
                    usdBalance -= convetteSum;
                    ethBalance += convetteSum / usdToEth;
                }
                else if (currencieFromConvett == btcCurrencieName && currencieToConvett == usdCurrencieName)
                {
                    btcBalance -= convetteSum;
                    usdBalance += convetteSum / btcToUsd;
                }
                else if(currencieFromConvett == btcCurrencieName && currencieToConvett == ethCurrencieName)
                {
                    btcBalance -= convetteSum;
                    ethBalance += convetteSum / btcToEth;
                }
                else if (currencieFromConvett == ethCurrencieName && currencieToConvett == usdCurrencieName)
                {
                    ethBalance -= convetteSum;
                    usdBalance += convetteSum / ethToUsd;
                }
                else if (currencieFromConvett == ethCurrencieName && currencieToConvett == btcCurrencieName)
                {
                    ethBalance -= convetteSum;
                    btcBalance += convetteSum / ethToBtc;
                }
                else
                {
                    Console.WriteLine("Unknown currencie pair!");
                }
                #endregion logic
            }
        }
    }
}
