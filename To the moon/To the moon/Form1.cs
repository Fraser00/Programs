using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace To_the_moon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            ///creating arrays for each set of buttons that will be used as groups for bets
            ///

            //array for the number buttons 1-36
            numberButtons = new Button[] {
                btn0, btn1, btn2, btn3, btn4,btn5, btn6, btn7, btn8, btn9, btn10, btn11, btn12,
                 btn13, btn14, btn15, btn16, btn17, btn18, btn19, btn20, btn21, btn22, btn23, btn24,
                btn25, btn26, btn27, btn28, btn29, btn30, btn31, btn32, btn33, btn34, btn35, btn36 };

            //array for the white numbers and blue numbers
            whiteBtns = new Button[] { btn2, btn4, btn6, btn8, btn10, btn12, btn14, btn15, btn16, btn17,
                        btn18, btn20, btn26, btn28, btn30, btn33, btn34, btn36};

            blueBtns = new Button[] { btn1, btn3, btn5, btn7, btn9, btn11, btn13, btn19, btn21, btn22,
                        btn23, btn24, btn25, btn27, btn29, btn31, btn32, btn35 };

            //arrays for the sets of 12s
            first12Btns = new Button[] { btn1, btn3, btn5, btn7, btn9, btn11, btn2, btn4, btn6, btn8, btn10, btn12 };
            second12Btns = new Button[] { btn13, btn14, btn15, btn16, btn17, btn18, btn19, btn20, btn21, btn22, btn23, btn24 };
            third12Btns = new Button[] { btn25, btn26, btn27, btn28, btn29, btn30, btn31, btn32, btn33, btn34, btn35, btn36 };

            //arrays for the row buttons
            firstRowBtns = new Button[] { btn3, btn6, btn9, btn12, btn15, btn18, btn21, btn27, btn30, btn33, btn36 };
            secondRowBtns = new Button[] {btn2, btn5, btn8, btn11, btn14, btn17, btn20, btn23, btn26, btn29, btn32,btn35  };
            thirdRowBtns = new Button[] { btn1, btn4, btn7, btn10, btn13, btn16, btn19, btn22, btn25, btn28, btn31, btn34};

            //array for even and odd buttons
            evenBtns = new Button[] {
                  btn2,  btn4, btn6,  btn8, btn10, btn12,
                 btn14, btn16, btn18, btn20, btn22, btn24,
                 btn26,  btn28, btn30, btn32, btn34,  btn36 };

            oddBtns = new Button[] {
                 btn1,  btn3, btn5,  btn7,  btn9,  btn11,
                 btn13, btn15,  btn17,  btn19,  btn21,  btn23,
                btn25, btn27, btn29, btn31, btn33, btn35 };

            firstHalfBtns = new Button[] {
                 btn1, btn2, btn3, btn4,btn5, btn6, btn7, btn8, btn9, btn10, btn11, btn12,
                 btn13, btn14, btn15, btn16, btn17, btn18};
            secondHalfBtns = new Button[] {
                btn19, btn20, btn21, btn22, btn23, btn24,
                btn25, btn26, btn27, btn28, btn29, btn30, btn31, btn32, btn33, btn34, btn35, btn36 };

            labelsUpd();
        }
        //arrays for groups of numbers
        Button[] numberButtons;
        Button[] whiteBtns;
        Button[] blueBtns;
        Button[] first12Btns;
        Button[] second12Btns;
        Button[] third12Btns;
        Button[] firstRowBtns;
        Button[] secondRowBtns;
        Button[] thirdRowBtns;
        Button[] evenBtns;
        Button[] oddBtns;
        Button[] secondHalfBtns;
        Button[] firstHalfBtns;

        int balance = 100; //players money
        int betTotal = 0; //players bet size
        int profit = 0; //players profit +/-
        List<Button> selectedBets = new List<Button>(); //stores players chosen bet
        bool win = false;
        bool spun = false;
        int betSize = 10;  //defaults bet size to 10
        int winningNum;
        int totalPayout = 0;
        List<int> bets = new List<int>();  //stores bet values for each button clicked

        private void btnClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            ///
            /// checking if a chip is selected, if user has enough balance to bet 
            /// removing bet amount from balance and highlighting the button chosen for the bet
            ///

            if (balance < betSize || balance <= 0)
            {

                MessageBox.Show("Not enough money", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }

            //updating users balance and profit 
            //selecting the button the user clicked for their bet and highlighting with yellow boldened text
            if (!selectedBets.Contains(button))
            {
                selectedBets.Add(button); //adding to list of chosen buttons

                button.ForeColor = Color.Yellow;
                button.Font = new Font("Impact", button.Font.Size, FontStyle.Bold);
                profit -= betSize;
                balance -= betSize;
                betTotal += betSize;
                bets.Add(betSize); //adding current betsize to list to sync with button chosen
            }
            else
            {
                MessageBox.Show($"You already placed a bet here!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //updating the profit balance and bet total

            labelsUpd();
        }

        private void btnSpin_Click(object sender, EventArgs e)
        {

            ///Generates a random number, depending on result will display winning message with the payout or losing screen
            spun = true;
            Random rand = new Random();
            winningNum = rand.Next(0, 37); // Random number between 0 and 36
            lblWinningNum.Text = $"Winning number:" + winningNum; // Display result

            if (selectedBets.Any()) //checking if any bets have been placed
            {
                checkWin();

                if (win == false)
                {

                    MessageBox.Show($"You LOST", "LOSER!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    balance += totalPayout;
                    profit += totalPayout;
                    MessageBox.Show($"You won yipee! You won £{totalPayout}", "Winner!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }

            labelsUpd();
            clear();
            win = false;


        }

        private void labelsUpd()
        {
            lblBalance.Text = $"Balance: £" + balance; //update balance for user
            lblBet.Text = $"Bet: £" + betTotal; //update bet for user

            //changing color of profit label depending on if the user is winning overall
            if (profit > 0)
            {
                lblProfit.ForeColor = Color.Green;
            }
            else
            {
                lblProfit.ForeColor = Color.Red;
            }

            lblProfit.Text = $"+/-: £" + profit; //update profit for user
        }


        private void checkWin()
        {
            ///Loops for each button selected
            ///checks which button was clicked then check the relating array of buttons to check for win
            ///if condition is met calculates the payout and adds to total payout, balance and profit
            ///


            for (int i=0; i < selectedBets.Count; i++ ) //looping for each button betted on using i as index to go through lists
            {
                Button button =selectedBets[i];
                int payout = 0; //sets payout to 0 at the start of each loop

                if (button == numberButtons[winningNum])
                {
                    payout = bets[i] * 35;  //calculates payout for this buttons bet
                    totalPayout += payout;  //adds to total payout
                    win = true;

                }
                else if (button == btnWhite) //checks if button betted on is the "White" buttoon
                {
                    if (whiteBtns.Contains(numberButtons[winningNum])) //checks if the winning number is inside the white buttons array
                    {
                        payout = bets[i] * 2;
                        totalPayout += payout;
                        win = true;
                    }
                }
                else if (button == btnBlue)
                {
                    if (blueBtns.Contains(numberButtons[winningNum]))
                    {
                        payout = bets[i] * 2;
                        totalPayout += payout;
                        win = true;
                    }
                }
                else if (button == btn112)
                {
                    if (first12Btns.Contains(numberButtons[winningNum]))
                    {
                        payout = bets[i] * 3;
                        totalPayout += payout;
                        win = true;
                    }
                }

                else if (button == btn212)
                {
                    if (second12Btns.Contains(numberButtons[winningNum]))
                    {
                        payout = bets[i] * 3;
                        totalPayout += payout;
                        win = true;
                    }
                }

                else if (button == btn312)
                {
                    if (third12Btns.Contains(numberButtons[winningNum]))
                    {
                        payout = bets[i] * 3;
                        totalPayout += payout;
                        win = true;
                    }
                }
                else if (button == btnRow1)
                {
                    if (firstRowBtns.Contains(numberButtons[winningNum]))
                    {
                        payout = bets[i] * 3;
                        totalPayout += payout;
                        win = true;
                    }
                }
                else if (button == btnRow2)
                {
                    if (secondRowBtns.Contains(numberButtons[winningNum]))
                    {
                        payout = bets[i] * 3;
                        totalPayout += payout;
                        win = true;
                    }
                }
                else if (button == btnRow3)
                {
                    if (thirdRowBtns.Contains(numberButtons[winningNum]))
                    {
                        payout = bets[i] * 3;
                        totalPayout += payout;
                        win = true;
                    }
                }
                else if (button == btnEven)
                {
                    if (evenBtns.Contains(numberButtons[winningNum]))
                    {
                        payout = bets[i] * 2;
                        totalPayout += payout;
                        win = true;
                    }
                }
                else if (button == btnOdd)
                {
                    if (oddBtns.Contains(numberButtons[winningNum]))
                    {
                        payout = bets[i] * 2;
                        totalPayout += payout;
                        win = true;
                    }
                }
                else if (button == btn118)
                {
                    if (firstHalfBtns.Contains(numberButtons[winningNum]))
                    {
                        payout = bets[i] * 2;
                        totalPayout += payout;
                        win = true;
                    }
                }
                else if (button == btn1936)
                {
                    if (secondHalfBtns.Contains(numberButtons[winningNum]))
                    {
                        payout = bets[i] * 2;
                        totalPayout += payout;
                        win = true;
                    }
                }
               
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void clear()
        {
            if (selectedBets != null) //checks if any buttons need reset
            {
                foreach (Button button in selectedBets) //loops for every button clicked and resets back to previous state
                {
                    if (whiteBtns.Contains(button) || button == btnWhite) //resets any buttons that were highlighted
                    {
                        button.ForeColor = Color.Black;
                        button.Font = new Font("Microsoft Sans Serif", button.Font.Size, FontStyle.Regular);
                    }
                    else
                    {
                        button.ForeColor = Color.White;
                        button.Font = new Font("Microsoft Sans Serif", button.Font.Size, FontStyle.Regular);
                    }
                }
            }
            //resets totals to give the users money back if user did not spin
            if (spun == false)
            {
                profit += betTotal;
                balance += betTotal;
            }
            selectedBets.Clear();  //resets values and lists
            bets.Clear();
            betTotal = 0;
            totalPayout = 0;
            labelsUpd();
            spun = false;
        }

        private void chipReset()
        {
            ///resets the appearances of the chips

            btnChip10.ForeColor = Color.White;
            btnChip10.Font = new Font("Microsoft Sans Serif", 24, FontStyle.Regular);
            btnChip10.FlatStyle = FlatStyle.Popup;

            btnChip20.ForeColor = Color.White;
            btnChip20.Font = new Font("Microsoft Sans Serif", 24, FontStyle.Regular);
            btnChip20.FlatStyle = FlatStyle.Popup;

            btnChip50.ForeColor = Color.White;
            btnChip50.Font = new Font("Microsoft Sans Serif", 24, FontStyle.Regular);
            btnChip50.FlatStyle = FlatStyle.Popup;

            btnChip100.ForeColor = Color.White;
            btnChip100.Font = new Font("Microsoft Sans Serif", 24, FontStyle.Regular);
            btnChip100.FlatStyle = FlatStyle.Popup;
        }

        private void btnChip10_Click(object sender, EventArgs e)
        {
            chipClick(btnChip10, 10);
        }

        private void btnChip20_Click(object sender, EventArgs e)
        {
            chipClick(btnChip20, 20);
        }

        private void btnChip50_Click(object sender, EventArgs e)
        {
            chipClick(btnChip50, 50);
        }

        private void btnChip100_Click(object sender, EventArgs e)
        {
            chipClick(btnChip100, 100);
        }

        private void chipClick(Button chip, int amount)
        {
            //highlights the button clicked, changing colour and font etc
            chipReset();
            betSize = amount;

            chip.ForeColor = Color.Yellow;
            chip.Font = new Font("Impact", 28, FontStyle.Bold);
            chip.FlatStyle = FlatStyle.Flat;
            labelsUpd();
        }
    }
}
