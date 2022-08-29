using System;

//Task of https://lk.ijunior.ru/Homework/Detail/33
namespace MoneyConvertor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region data
            string exitCommad = "YES";

            string USDCurrencieName = "USD";
            string BTCCurrencieName = "BTC";
            string ETHCurrencieName = "ETH";
         
            float USDtoBTC = 15326f,USDtoETH = 4500f;
            float BTCtoUSD = 0.0001f, BTCtoETH = 0.20f;
            float ETHtoBTC = 5f, ETHtoUSD = 0.002f;
            #endregion data

            var USDBalance = 1000f;
            var BTCBalance = 0.1f;
            var ETHBalance = 4f;  

            while(true)
            {
                #region exit
                Console.WriteLine("Exit? (YES or NO, other answer is NO)");
                var exit = Console.ReadLine().ToUpper() == exitCommad;

                if (exit)
                    break;
                #endregion exit
                
                #region userInput
                Console.WriteLine("\n"+$"On balanse: {USDBalance} USD, {BTCBalance} BTC, {ETHBalance} ETH.");

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

                if (currencieFromConvett == USDCurrencieName && USDBalance < convetteSum)
                {
                    Console.WriteLine($"Convette sum is larger then Balanse of {USDCurrencieName}");
                    continue;
                }
                if (currencieFromConvett == BTCCurrencieName && BTCBalance < convetteSum)
                {
                    Console.WriteLine($"Convette sum is larger then Balanse of {BTCCurrencieName}");
                    continue;
                }
                if (currencieFromConvett == ETHCurrencieName && ETHBalance < convetteSum)
                {
                    Console.WriteLine($"Convette sum is larger then Balanse of {ETHCurrencieName}");
                    continue;
                }
                #endregion inVariant

                #region logic
                if (currencieFromConvett == USDCurrencieName && currencieToConvett == BTCCurrencieName)
                {
                    USDBalance -= convetteSum;
                    BTCBalance += convetteSum / USDtoBTC;
                }
                else if (currencieFromConvett == USDCurrencieName && currencieToConvett == ETHCurrencieName)
                {
                    USDBalance -= convetteSum;
                    ETHBalance += convetteSum / USDtoETH;
                }
                else if (currencieFromConvett == BTCCurrencieName && currencieToConvett == USDCurrencieName)
                {
                    BTCBalance -= convetteSum;
                    USDBalance += convetteSum / BTCtoUSD;
                }
                else if(currencieFromConvett == BTCCurrencieName && currencieToConvett == ETHCurrencieName)
                {
                    BTCBalance -= convetteSum;
                    ETHBalance += convetteSum / BTCtoETH;
                }
                else if (currencieFromConvett == ETHCurrencieName && currencieToConvett == USDCurrencieName)
                {
                    ETHBalance -= convetteSum;
                    USDBalance += convetteSum / ETHtoUSD;
                }
                else if (currencieFromConvett == ETHCurrencieName && currencieToConvett == BTCCurrencieName)
                {
                    ETHBalance -= convetteSum;
                    BTCBalance += convetteSum / ETHtoBTC;
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
