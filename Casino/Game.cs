using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Casino
{
    public class Game
    {
        public Player player;
        static Semaphore sem = new Semaphore(5, 5);
        Thread thread;
        Form1 form;
        int count; //qty of players
        static int CountBet=0;
        static int NumbChair = 0;  //nomer stula

        static public List<Player> listP = new List<Player>();

        public Game(Form1 form, Player player) 
        {
            this.form = form; 
            this.player = player;
            thread = new Thread(StartGame);
            thread.Name = player.name; 
            thread.IsBackground = true;  
            thread.Start();
        }

        public void SetBalance()
        {

            foreach (var b in listP)
            {
                b.GenerateBalance();
            }
        }

      
        private void CheckNumbers(List<Player> LP)  ///сравниваем числа игроков и рулетки
        {
            int numberFromCasino = RouletteNumber(); // Генерируем число в казино
            form.SetLabel26(numberFromCasino.ToString());
            form.AddStringToList($"РУЛЕТКА: выпало число {numberFromCasino}");

            foreach (var player in LP)
            {
                // Сравниваем числа
                if (MatchNumber(numberFromCasino, player.number))//true
                {
                    player.bet *= 2;
                    player.balance += player.bet;
                }
                else
                {
                    player.balance -= player.bet;
                }
            }
        }
        public void StartGame()// One player
        {
            sem.WaitOne();
            player.ChairNumber = NumbChair++; //игрок занимает стул 
            if (NumbChair==5)
                NumbChair = 0; // когда заняли 5 стульев то кол-во стульев обнуляется
            listP.Add(player);

            form.SetLabel(player);
            while (player.balance > 0)
            {

                form.AddStringToList($"{Thread.CurrentThread.Name} делает денежную ставку {player.bet} долл. ");

                player.MakeBet();    //Игрок делает ставку в деньгах
                player.ChooseNumber(); // Игрок выбирает число для ставки
                form.SetLabel(player);
                form.AddStringToList($"{Thread.CurrentThread.Name} делает денежную ставку {player.bet} долл. на число {player.number} ");
                CountBet++;
                if (CountBet == 5)
                {
                   
                    CheckNumbers(listP);
                    CountBet = 0;
                 
                }
                Thread.Sleep(2000);
            }
            
            sem.Release();
            form.AddStringToList($"{Thread.CurrentThread.Name} покидает стол ");
        }
        public int RouletteNumber()
        {
            Random rnd = new Random();
            int n = rnd.Next(0, 37);
            Thread.Sleep(3000);
            return n;
        }

        public bool MatchNumber(int num1, int num2)
        {
            return num1 == num2;
        }











    }
}
