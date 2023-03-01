using Banks.Accounts;
using Banks.Console;
using Banks.Entities;

var centralBank = new CentralBank(10, 0.05M, 0.1M, 0.1M, 100);
var menu = new Menu();

Bank bank = centralBank.CreateBank("Sber");

Console.WriteLine(centralBank.GetBanks.Any(bank => bank.Name == "Sber") ? "Successfully" : "Bad");
Client client = new ClientBuilder().SetName("Mde").GetClient();

var acc = new CreditAccount(client, bank, 10, 5);
