using Guna.UI2.WinForms;
using MatchUp.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Media;

namespace MatchUp
{

    enum Result { Success = 1, Failure = 2 };
    public partial class onePlayer : Form
    {
        struct GameInfo
        {
            public Guna2ImageButton firstCard;
            public Guna2ImageButton secondCard;
            public Result attemptResult;
            public Result finalResult;
            public int numberOfWin;
        }

        List<int> imgNumber = new List<int> { 1, 2, 3, 4, 5,6,7,8 };
        Random rnd = new Random();
        int playerLives = 4;
        int timer = 7;
        GameInfo gameInfo = new GameInfo();

        private bool isVideoReady = false;
        public onePlayer()
        {
            InitializeComponent();
        }

        private List<int> FillListInRange( int from, int to)
        {
            List<int> filledList = new List<int>();

            for (int i = from; i <= to; i++)
            {
                filledList.Add(i);
            }
            return filledList;
        }

        private void GetRandomImageNumber(Guna2ImageButton button)
        {
            int num = imgNumber[rnd.Next(0, imgNumber.Count)];
            button.Tag = num;
            imgNumber.Remove(num);

            if (imgNumber.Count == 0) {
                
                imgNumber = FillListInRange(1,8);
            }
        }

        private Result GetAttemptResult(Guna2ImageButton firstCard, Guna2ImageButton secondCard)
        {
            int.TryParse(firstCard.Tag.ToString(), out int cardNumber1);
            int.TryParse(secondCard.Tag.ToString(), out int cardNumber2);

            if (cardNumber1 == cardNumber2)
            {
                SoundPlayer player = new SoundPlayer(Resources.winSound);

                player.Play();
                return Result.Success;

            }
            else
            {
                return Result.Failure;

            }
        }

        private void UpdatePlayerLives()
        {
            switch (playerLives)
            {
                case 1:
                    btnlive3.Visible = false;
                    btnlive4.Image = Resources._1live;
                    break;
                    case 2:
                    btnlive2.Visible = false;
                    btnlive3.Image = Resources._2live;
                    btnlive4.Image = Resources._2live;
                    break;
                    case 3:
                    btnlive1.Visible = false;
                    btnlive2.Image = Resources._3live;
                    btnlive3.Image = Resources._3live;
                    btnlive4.Image = Resources._3live;

                    break;
                default:
                    btnlive4.Visible = false;
                    break;
            }
        }

        private void UpdateGameState()
        {
            if(gameInfo.attemptResult == Result.Success)
            {
                gameInfo.numberOfWin++;
                gameInfo.firstCard = null;
                gameInfo.secondCard = null;
                UnlockBoard();

            }
            else
            {
                playerLives--;
                UpdatePlayerLives();
                timer1.Enabled = true;
            }

        }

        private void ProcessCardFlip(Guna2ImageButton button)
        {
             if(gameInfo.firstCard == null)
            {
                gameInfo.firstCard = button;
                gameInfo.firstCard.ImageSize = new Size(110, 110);

            }
            else
            {
                gameInfo.secondCard = button;
                gameInfo.secondCard.ImageSize = new Size(110, 110);
                LockBoard();
                gameInfo.attemptResult = GetAttemptResult(gameInfo.firstCard, gameInfo.secondCard);


            }
        }
        private void onePlayer_Load(object sender, EventArgs e)
        {
            Guna2ImageButton[] btns = {
                        btn1, btn2, btn3, btn4,
                        btn5, btn6, btn7, btn8,
                        btn9, btn10, btn11, btn12,
                        btn13, btn14, btn15, btn16
                    };
            for (int i = 0; i < btns.Length; i++)
            {
                GetRandomImageNumber(btns[i]);
            }
            timer2.Enabled = true;
            ShowAllImg();
            gameInfo.numberOfWin = 0;
        }

        private void HideImg(Guna2ImageButton button)
        {
            button.Image = Resources.qimg;
        }
        private void HideAllImg()
        {
            Guna2ImageButton[] btns = {
                        btn1, btn2, btn3, btn4,
                        btn5, btn6, btn7, btn8,
                        btn9, btn10, btn11, btn12,
                        btn13, btn14, btn15, btn16
                    };
            for (int i = 0; i < btns.Length; i++)
            {
                btns[i].ImageSize = new Size(150, 150);
                HideImg(btns[i]);
            }
        }

        private void ShowAllImg()
        {
            Guna2ImageButton[] btns = {
                        btn1, btn2, btn3, btn4,
                        btn5, btn6, btn7, btn8,
                        btn9, btn10, btn11, btn12,
                        btn13, btn14, btn15, btn16
                    };
            for (int i = 0; i < btns.Length; i++)
            {
                btns[i].ImageSize = new Size(110, 110);
                ChangeImg(btns[i]);
            }
        }

      
        private void LockBoard()
        {
            Guna2ImageButton[] btns = {
                        btn1, btn2, btn3, btn4,
                        btn5, btn6, btn7, btn8,
                        btn9, btn10, btn11, btn12,
                        btn13, btn14, btn15, btn16
                    };
            for (int i = 0; i < btns.Length; i++)
            {
               
                    btns[i].Enabled = false;
                
            }
        }


        private void UnlockBoard()
        {
            Guna2ImageButton[] btns = {
                        btn1, btn2, btn3, btn4,
                        btn5, btn6, btn7, btn8,
                        btn9, btn10, btn11, btn12,
                        btn13, btn14, btn15, btn16
                    };
            for (int i = 0; i < btns.Length; i++)
            {
               
                    btns[i].Enabled = true;
                
            }
        }

        private void ChangeImg(Guna2ImageButton button)
        {
           if(int.TryParse(button.Tag.ToString(), out int num))
            {
                switch (num)
                {
                    case 1:
                        button.Image = Resources._1;
                        break;
                    case 2:
                        button.Image = Resources._2;
                        break;
                    case 3:
                        button.Image = Resources._3;
                        break;
                    case 4:
                        button.Image = Resources._4;
                        break;
                    case 5:
                        button.Image = Resources._5;
                        break;
                    case 6:
                        button.Image = Resources._6;
                        break;
                    case 7:
                        button.Image = Resources._7;
                        break;
                    default:
                        button.Image = Resources._8;
                        break;


                }
            }
        }
        
        private bool IsCardNull(Guna2ImageButton card)
        {
            return card == null;
        }

        private bool IsGameOver()
        {
            if (gameInfo.numberOfWin == 8)
            {
                gameInfo.finalResult = Result.Success;
                return true;
            }
            else if (playerLives == 0)
            {
                gameInfo.finalResult = Result.Failure;
                return true;

            }
            else
                return false;

        }

        private void RunWinVideo()
        {
            string videoPath = Path.Combine(Application.StartupPath, "Videos", "win.mp4");

            gameVideoPlayer.Visible = false;
            gameVideoPlayer.uiMode = "none";
            gameVideoPlayer.URL = videoPath;
            gameVideoPlayer.settings.setMode("loop", true);
            gameVideoPlayer.Ctlcontrols.play();
        }

        private void RunLoseVideo()
        {

            string videoPath = Path.Combine(Application.StartupPath, "Videos", "lose.mp4");

            gameVideoPlayer.Visible = false;
            gameVideoPlayer.uiMode = "none";
            gameVideoPlayer.URL = videoPath;
            gameVideoPlayer.settings.setMode("loop", true);
            gameVideoPlayer.Ctlcontrols.play();
        }


        private void EndGame()
        {
            if (IsGameOver())
            {
                if(gameInfo.finalResult == Result.Success)
                {
                    RunWinVideo();
                }
                else
                {
                    RunLoseVideo();

                }
                LockBoard();
            }
        }

        private void AnyButton_Click(object sender, EventArgs e)
        {
            Guna2ImageButton button = (Guna2ImageButton)sender;
            ChangeImg(button);
            ProcessCardFlip(button);

            if(!IsCardNull(gameInfo.secondCard))
            {
                UpdateGameState();
            }

            if (IsGameOver())
            {
                EndGame();
            }
        }

    

        private void Timer1_Tick(object sender, EventArgs e)
        {
            gameInfo.firstCard.Image = Resources.qimg;
            gameInfo.firstCard.ImageSize = new Size(150, 150);
            gameInfo.secondCard.Image = Resources.qimg;
            gameInfo.secondCard.ImageSize = new Size(150, 150);

            gameInfo.firstCard = null;
            gameInfo.secondCard = null;
   
            timer1.Enabled = false;
            if (!IsGameOver())
            {
                UnlockBoard();
            }

        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            HideAllImg();
            timer2.Enabled = false;
            timer3.Enabled = false;
            lblTimer.Visible = false;


        }


 


        private void Guna2Button1_Click(object sender, EventArgs e)
        {
            gameVideoPlayer.Ctlcontrols.stop();
            gameVideoPlayer.Visible = false;
        }

        private void Guna2CircleButton1_Click(object sender, EventArgs e)
        {
            gameVideoPlayer.Ctlcontrols.stop();
            gameVideoPlayer.Visible = false;
            this.Close();
        }

        private void Timer3_Tick(object sender, EventArgs e)
        {
            timer--;
            lblTimer.Text = timer.ToString();
        }

        private void ShowGameOverMag()
        {
            if (gameInfo.finalResult == Result.Success)
            {
                lblGameOverMsgWin.Text = "SOMEONE'S GOT BRAINS!";
                lblGameOverMsgWin.Visible = true;


            }
            else
            {
                lblGameOverMsgLosser.Text = "LOSER!";
                lblGameOverMsgLosser.Visible = true;

            }
            pGameOver.Visible = true;
        }
 

        private void GameVideoPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == 3)
            {
                gameVideoPlayer.Visible = true;
                ShowGameOverMag();

            }
        }
    }
}
