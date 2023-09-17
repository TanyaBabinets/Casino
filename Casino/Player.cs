using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public class Player
    {
        public string name { get; set; }
        public int balance { get; set; }
        public int bet { get; set; }
        public int number { get; set; }
        public bool HasMoney { get; set; }  
        public int ChairNumber { get; set; }
        

        public Player() { }
        public Player(int i) { this.name = $"Player {i}"; GenerateBalance(); }
        public override string ToString()
        {           
                 return $"{name}  {balance}";
        }

        public void GenerateBalance() 
        {
            Random rnd = new Random();
            balance = rnd.Next(1, 10000);
          
        }
        public int MakeBet()
        {
            Random rnd = new Random();
            bet = rnd.Next(1, balance);
           // balance -= bet;
            return bet;

        }
        public int ChooseNumber()
        {
            Random rnd = new Random();
            number = rnd.Next(1, 37);
            return number;

        }
    }
}
