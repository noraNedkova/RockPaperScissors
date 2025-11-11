//Лора Недкова Недева F112913
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RockPaperScissors
{
    public partial class Form1 : Form
    {
        private Button[] buttons;//масив, съдържащ всички клетки върху, които ще се играе
        private string currPlayer = "X";// ред на играча
        public Form1()
        {
            InitializeComponent();
            buttons = new Button[] { button1, button2, button3, button4, button5, button6,
            button7, button8, button9};//добавяне на бутоните в масива

            turn.Text = $"Играч {currPlayer} на ход";//текст, показващ кой е на ред

            foreach (Button btn in buttons){//присвояване на клетките за игра метод клик
                btn.Click += BtnClick;
            }
        }

        private void BtnClick(object sender, EventArgs e)// метод за натискане на клетките
        {
            Button btn = sender as Button;//кой бутон е натиснат
            if(btn.Text == "")//ако полето още не е използвано
            {
                btn.Text = currPlayer;//маркира се с О или Х

                if (CheckWin())//проверяваме дали има победа
                {
                    result.Text = $"Играч {currPlayer} печели!";
                    turn.Text = "";
                    DisableBtns();//бутоните стават неактивни след приключване на играта
                    return;
                }

                if (IsDraw())//проверяваме равенство
                {
                    result.Text = $"Равенство!";
                    turn.Text = "";
                    return;
                }

                currPlayer = currPlayer=="X"?"O":"X";//сегашния играч се променя
                turn.Text = $"Играч {currPlayer} на ход";
            }
        }

        private bool CheckWin()//проверка за победа
        {
            int[,] wins = new int[,]//масив с всички възможни победи
            {
                {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, //победи редове
                {0, 4, 8}, {2, 4, 6},            // победи диагонали
                {0, 3, 6}, {1, 4, 7}, {2, 5, 8}  //победи колони
            };

            for(int i = 0; i < wins.GetLength(0); i++)//обхождане на масива
            {
                //променливи, приемащи стойности на възможните комбинации победи
                int first = wins[i, 0];
                int second = wins[i, 1];
                int third = wins[i, 2];

                //проверка дали те съвпадат с изиграните ходове
                if (buttons[first].Text != "" &&
                    buttons[first].Text == buttons[second].Text &&
                    buttons[first].Text == buttons[third].Text)
                {
                    return true;
                }

            }
            return false;
        }

        private bool IsDraw()//проверка за равенство
        {
            foreach(Button b in buttons)//обхождане на всяко поле за игра
            {
                if(b.Text == "")
                {
                    return false;// ако е празно няма как да има равенство
                }
            }
            //ако всички полета притежават стойност и не е настъпила победа, има равенство
            return true;
        }

        private void DisableBtns()//деактивиране на всички полета за игра
        {
            foreach(Button b in buttons)
            {
                b.Enabled = false;
            }
        }

        private void new_game_Click(object sender, EventArgs e)//бутон за нова игра
        {
            foreach (Button btn in buttons)//всички полета са празни и могат да се попълнят
            {
                btn.Text = "";
                btn.Enabled = true;
            }

            currPlayer = "X";//първи играч по подразбиране е Х
            result.Text = "";
            turn.Text = $"Играч {currPlayer} на ход";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
