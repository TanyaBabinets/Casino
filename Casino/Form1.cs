using Casino;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Label = System.Windows.Forms.Label;

namespace Casino
{
    public partial class Form1 : Form
    {
        public SynchronizationContext uiContext;
        public Game game;
     //   public Player player;
        static public List<Player> listP = new List<Player>();

        List<Label> NamePlayers = null;
        List<Label> NameBalance = null;
        List<Label> NameBet = null;
        List<Label> NameNumber = null;

        public Form1()
        {
            InitializeComponent();
            this.Text = "CASINO";
            uiContext = SynchronizationContext.Current;
            NamePlayers = new List<Label>() { label1, label6, label11, label16, label21 };
            NameBalance = new List<Label>() { label4, label9, label14, label19, label24 };
            NameBet = new List<Label>() { label5, label10, label15, label20, label25 };
            NameNumber = new List<Label>() { label29, label31, label33, label35, label37 };
        }
        private void button1_Click(object sender, EventArgs e)//START GAME
        {
            Random rnd = new Random();
                      
            for (int i = 1; i < rnd.Next(20, 100); i++)
            {
                Player player = new Player(i);              
                game = new Game(this, player);
               // Thread.Sleep(2000);                           
            }
                Thread.Sleep(100);
    
        }

        /// <summary>
        public void AddStringToList(string str)
        {
      
            uiContext.Send(d => listBox1.Items.Add(str), null);
        }
      
        public void SetLabel(Player p)
        {
            uiContext.Send(d => NamePlayers[p.ChairNumber].Text= p.name, null);
            uiContext.Send(d => NameBalance[p.ChairNumber].Text = p.balance.ToString(), null);
            uiContext.Send(d => NameBet[p.ChairNumber].Text  = p.bet.ToString(), null);
            uiContext.Send(d => NameNumber[p.ChairNumber].Text   = p.number.ToString(), null);


        }
        public void SetLabel26(string n)
        {
            uiContext.Send(d => label26.Text = n, null);
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) /// list of all movements at the table
        {
           
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = 960;
        }
    }
}







