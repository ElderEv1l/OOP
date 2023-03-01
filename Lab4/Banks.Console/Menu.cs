namespace Banks.Console;

public class Menu
{
    public void PrintMenu()
    {
        System.Console.WriteLine("1.Create a bank");
        System.Console.WriteLine("2.Create a client profile");
        System.Console.WriteLine("3.Create an account for client");
        System.Console.WriteLine("4.TopUp money");
        System.Console.WriteLine("5.Withdraw money");
        System.Console.WriteLine("6.Transfer money");
        System.Console.WriteLine("7.Cancel transfer");
        System.Console.WriteLine("8.Skip few days");
        System.Console.WriteLine("9.Change credit commission");
    }
}