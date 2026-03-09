using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace game4
{
    public partial class FormBaccarat : Form
    {
        private string playerName;
        private decimal playerMoney;  // เก็บจำนวนเงินของผู้เล่น
        private BaccaratGame baccaratGame;
        public string Username { get; set; }  // Property สำหรับเก็บ username
        public FormBaccarat()
        {
            InitializeComponent();
            baccaratGame = new BaccaratGame();
            
            }
     

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        // ปุ่มเริ่มเกม
        private void button1_Click(object sender, EventArgs e)
        {
        
            if (decimal.TryParse(textBox2.Text, out playerMoney) && playerMoney > 0)  // รับยอดเงินเริ่มต้น
            {
                MessageBox.Show($"Welcome {playerName}! You have {playerMoney:C}.");  // แสดงข้อความยินดีต้อนรับ
                tabControl1.SelectedTab = tabPage2;  // ไปที่แท็บที่ 2 (เกม Baccarat)
                lblPlayerMoney.Text = $"Your balance: {playerMoney:C}";  // แสดงยอดเงินใน Tab 2
            }
            else
            {
                MessageBox.Show("Invalid amount of money.");  // ถ้าจำนวนเงินไม่ถูกต้อง
            }
        }

        private List<decimal> playerBets = new List<decimal>();  // เก็บการเดิมพันฝั่ง Player
        private List<decimal> bankerBets = new List<decimal>();  // เก็บการเดิมพันฝั่ง Banker
        private decimal totalBetAmount = 0;  // ตัวแปรเก็บยอดรวมการเดิมพัน
        private Timer countdownTimer;
        private int countdown = 3;  // เวลาในการนับถอยหลัง
        private List<decimal> tigerBets = new List<decimal>();  // เก็บการเดิมพันฝั่ง Tiger
        private List<decimal> dragonBets = new List<decimal>();  // เก็บการเดิมพันฝั่ง Dragon
        private decimal totalTigerDragonBetAmount = 0;  // ตัวแปรเก็บยอดรวมการเดิมพันเสือมังกร


        // ปุ่มเริ่มเดิมพัน
        private void button2_Click(object sender, EventArgs e)
        {
            // เคลียร์ข้อมูลการเดิมพันจากรอบก่อนหน้า
            totalBetAmount = 0;  // เคลียร์ยอดรวมการเดิมพัน
            playerBets.Clear();  // เคลียร์รายการเดิมพัน Player
            bankerBets.Clear();  // เคลียร์รายการเดิมพัน Banker

            // อ่านข้อมูลจาก TextBox และคำนวณยอดเดิมพัน
            foreach (Control control in tabPage2.Controls)
            {
                if (control is TextBox)
                {
                    decimal betAmount;
                    if (decimal.TryParse(control.Text, out betAmount) && betAmount > 0)
                    {
                        totalBetAmount += betAmount;

                        // ถ้าเดิมพันฝั่ง Player
                        if (control.Name.Contains("Player"))
                        {
                            playerBets.Add(betAmount);
                        }
                        // ถ้าเดิมพันฝั่ง Banker
                        else if (control.Name.Contains("Banker"))
                        {
                            bankerBets.Add(betAmount);
                        }
                    }
                }
            }

            // ตรวจสอบว่าผู้เล่นมีเงินพอหรือไม่
            if (totalBetAmount > 0 && totalBetAmount <= playerMoney)
            {
                // หักเงินจากยอดเงินผู้เล่น
                playerMoney -= totalBetAmount;

                // แสดงยอดเงินคงเหลือทันที
                lblPlayerMoney.Text = $"Your balance: {playerMoney:C}";

                // เริ่มต้น Timer
                countdown = 3;  // ตั้งเวลานับถอยหลัง
                lblCountdown.Text = $"Time remaining: {countdown}s";  // แสดงเวลาใน Label

                // สร้าง Timer ใหม่ทุกครั้งที่มีการกดปุ่ม
                countdownTimer = new Timer();
                countdownTimer.Interval = 1000; // ตั้งเวลาให้ Timer ทำงานทุกๆ 1 วินาที
                countdownTimer.Tick += CountdownTimer_Tick;  // เชื่อมโยงกับเมธอด CountdownTimer_Tick
                countdownTimer.Start();  // เริ่มการนับถอยหลัง
            }
            if (totalBetAmount > playerMoney)
            {
                DialogResult result = MessageBox.Show("ยอดเงินของคุณไม่พอ คุณต้องการย้อนกลับไปหน้าหลักหรือไม่?", "ยืนยันการออก", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ตรวจสอบว่าผู้ใช้เลือกปุ่มอะไร
                if (result == DialogResult.Yes)
                {
                    // ถ้าผู้ใช้เลือก Yes ให้ย้อนกลับไปที่ TabControl ที่ 1
                    tabControl1.SelectedIndex = 0;  // สมมติว่า TabControl ที่ 1 คือ index 0
                }
                else if (result == DialogResult.No)
                {
                    // ถ้าผู้ใช้เลือก No ให้ปิดโปรแกรม
                    Application.Exit();
                }
            }
            else
            {
                MessageBox.Show("Invalid bet amount or exceeds available balance.");  // ถ้าจำนวนเงินเดิมพันไม่ถูกต้อง
            }

        }

        // เมธอดที่ถูกเรียกเมื่อ Timer เริ่มทำงาน (ทุกๆ 1 วินาที)
        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            countdown--;  // ลดเวลาทุกๆ 1 วินาที
            lblCountdown.Text = $"Time remaining: {countdown}s";  // แสดงเวลาที่เหลือใน Label
            label8.Text = $"Time remaining: {countdown}s";  // แสดงเวลาที่เหลือใน Label

            if (countdown <= 0)
            {
                countdownTimer.Stop();  // หยุด Timer เมื่อเวลาเป็นศูนย์
                PlayGameAndShowResult();  // เริ่มเล่นเกมและแสดงผล
            }
        }

        // เมธอดที่เล่นเกมและแสดงผลเมื่อหมดเวลา
        private void PlayGameAndShowResult()
        {
            // สุ่มผลของเกม
            var result = baccaratGame.PlayGame();
            var playerCards = baccaratGame.PlayerCards;
            var bankerCards = baccaratGame.BankerCards;

            // แสดงหน้าไพ่หลังจากแจกไพ่
            picPlayerCard1.Image = playerCards[0].GetCardImage();
            picPlayerCard2.Image = playerCards[1].GetCardImage();
            picBankerCard1.Image = bankerCards[0].GetCardImage();
            picBankerCard2.Image = bankerCards[1].GetCardImage();

            // คำนวณคะแนนของ Player และ Banker
            int playerScore = baccaratGame.GetPlayerScore();
            int bankerScore = baccaratGame.GetBankerScore();

            // แสดงคะแนนใน Label
            lblPlayerScore.Text = $"Player Score: {playerScore}";
            lblBankerScore.Text = $"Banker Score: {bankerScore}";

            // คำนวณผลของเกมและการจ่ายเงิน
            decimal totalWin = 0;

            if (result == "Player wins!")
            {
                // การจ่ายเงินสำหรับฝั่ง Player (1:1)
                foreach (var bet in playerBets)
                {
                    totalWin += bet; // ได้รับคืน 1:1
                }

                // เพิ่มเงินเดิมพันคืนให้ผู้เล่น (1:1)
                totalWin += playerBets.Sum();  // จ่ายเงินคืนให้ผู้เล่น
            }
            else if (result == "Banker wins!")
            {
                // การจ่ายเงินสำหรับฝั่ง Banker (1:0.95)
                foreach (var bet in bankerBets)
                {
                    totalWin += bet * 0.95m; // ได้รับคืน 1:0.95
                }

                // เพิ่มเงินเดิมพันคืนให้ผู้เล่น (1:0.95)
                totalWin += bankerBets.Sum() * 0.95m;
            }
            else if (result == "Tie")
            {
                // ถ้าเกมเสมอ (Tie), คืนเงินเดิมพันทั้งหมด
                totalWin = totalBetAmount*2; // คืนเงินทั้งหมดที่เดิมพัน
            }

            // เพิ่มเงินที่ชนะกลับไปในยอดเงินของผู้เล่น
            playerMoney += totalWin;

            // แสดงยอดเงินคงเหลือ
            lblPlayerMoney.Text = $"Your balance: {playerMoney:C}";

            // แจ้งผลการชนะ
            MessageBox.Show($"Result: {result}. You won: {totalWin:C}. Your current balance is {playerMoney:C}");
        }


       
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormBaccarat_Load(object sender, EventArgs e)
        {
            labelUsername.Text = "Welcome, " + Username;  // แสดงชื่อผู้ใช้ใน Label
        }
       
        private void button4_Click(object sender, EventArgs e)
        {
            
            // สร้างอ็อบเจ็กต์เกม
            TigerDragonGame game = new TigerDragonGame();

            // รับค่าคะแนนจากทั้งสองฝั่ง
            int tigerScore, dragonScore;
            string result = game.PlayGame(out tigerScore, out dragonScore);

            // แสดงผลคะแนนใน Label
            lblTigerScore.Text = $"Tiger Score: {tigerScore}";
            lblDragonScore.Text = $"Dragon Score: {dragonScore}";

            // แสดงผลไพ่ใน PictureBox
            picTigerCard.Image = game.TigerCard.GetCardImage(); // แสดงภาพไพ่ของ Tiger
            picDragonCard.Image = game.DragonCard.GetCardImage(); // แสดงภาพไพ่ของ Dragon

            // แสดงผลการชนะ
            MessageBox.Show($"Result: {result}\nTiger Score: {tigerScore}\nDragon Score: {dragonScore}");
        }

        private void CountdownTimer1_Tick(object sender, EventArgs e)
        {
            //countdown--;  // ลดเวลาทุกๆ 1 วินาที
            //label8.Text = $"Time remaining: {countdown}s";  // แสดงเวลาที่เหลือใน Label

            //if (countdown <= 0)
            //{
            //    countdownTimer.Stop();  // หยุด Timer เมื่อเวลาเป็นศูนย์
            //    PlayGameAndShowResult();  // เริ่มเล่นเกมและแสดงผล
            //}
        }

        private void CountdownTimer2_Tick(object sender, EventArgs e)
        {
            //countdown--;  // ลดเวลาทุกๆ 1 วินาที
            //lblCountdown.Text = $"Time remaining: {countdown}s";  // แสดงเวลาที่เหลือใน Label

            //if (countdown <= 0)
            //{
            //    countdownTimer.Stop();  // หยุด Timer เมื่อเวลาเป็นศูนย์
            //    PlayTigerDragonGameAndShowResult();  // เริ่มเล่นเสือมังกรและแสดงผล
            //}
        }
        private void PlayTigerDragonGameAndShowResult()
        {
            TigerDragonGame game = new TigerDragonGame();

            // รับค่าคะแนนของ Tiger และ Dragon จากฟังก์ชัน PlayGame
            int tigerScore, dragonScore;
            string result = game.PlayGame(out tigerScore, out dragonScore);

            // แสดงผลไพ่
            picTigerCard.Image = Image.FromFile($"images/{game.TigerCard.Value}_of_{game.TigerCard.Suit}.png");
            picDragonCard.Image = Image.FromFile($"images/{game.DragonCard.Value}_of_{game.DragonCard.Suit}.png");

            // แสดงคะแนนใน Label
            lblTigerScore.Text = $"Tiger Score: {tigerScore}";
            lblDragonScore.Text = $"Dragon Score: {dragonScore}";

            decimal totalWin = 0;

            if (result == "Tiger wins!")
            {
                foreach (var bet in tigerBets)
                {
                    totalWin += bet; // ได้รับคืน 1:1
                }
            }
            else if (result == "Dragon wins!")
            {
                foreach (var bet in dragonBets)
                {
                    totalWin += bet; // ได้รับคืน 1:1
                }
            }
            else if (result == "It's a tie!")
            {
                totalWin = totalTigerDragonBetAmount * 2; // คืนเงินทั้งหมดที่เดิมพัน
            }

            playerMoney += totalWin;
            label7.Text = $"Your balance: {playerMoney:C}";
            MessageBox.Show($"Result: {result}. You won: {totalWin:C}. Your current balance is {playerMoney:C}");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(textBox2.Text, out playerMoney) && playerMoney > 0)  // รับยอดเงินเริ่มต้น
            {
                MessageBox.Show($"Welcome {playerName}! You have {playerMoney:C}.");  // แสดงข้อความยินดีต้อนรับ
                tabControl1.SelectedTab = tabPage3;  // ไปที่แท็บที่ 2 (เกม Baccarat)
                label7.Text = $"Your balance: {playerMoney:C}";  // แสดงยอดเงินใน Tab 2
            }
            else
            {
                MessageBox.Show("Invalid amount of money.");  // ถ้าจำนวนเงินไม่ถูกต้อง
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
