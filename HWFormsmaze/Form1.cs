using System;
using System.Drawing;
using System.Windows.Forms;

namespace HWFormsmaze
{
    public partial class Form1 : Form
    {
        private Point startPosition;
        private int healthPoints = 100;
        private int collectedCoins = 0;

        public Form1()
        {
            InitializeComponent();
            startPosition = hero.Location;
            labelHP.Text = "HP: 100%";
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int stepSize = 5;

            if (keyData == Keys.Left)
            {
                hero.Left -= stepSize;
            }
            else if (keyData == Keys.Right)
            {
                hero.Left += stepSize;
            }
            else if (keyData == Keys.Up)
            {
                hero.Top -= stepSize;
            }
            else if (keyData == Keys.Down)
            {
                hero.Top += stepSize;
            }

            bool isCollision = false;
            for (int i = 1; i <= 13; i++)
            {
                string labelWall = "label" + i;
                Label labelW = Controls.Find(labelWall, true).FirstOrDefault() as Label;

                if (labelW != null && hero.Bounds.IntersectsWith(labelW.Bounds))
                {
                    isCollision = true;
                    break;
                }
            }
            for (int i = 1; i <= 7; i++)
            {
                string labelCoin = "labelCoin" + i;
                Label labelC = Controls.Find(labelCoin, true).FirstOrDefault() as Label;
                if (labelC != null && hero.Bounds.IntersectsWith(labelC.Bounds))
                {
                    collectedCoins++;
                    Controls.Remove(labelC);
                    labelC.Dispose();
                    break;
                }
            }
            for (int i = 1; i <= 2; i++)
            {
                string labelHeal = "poisonHP" + i;
                Label labelH = Controls.Find(labelHeal, true).FirstOrDefault() as Label;
                if (labelH != null && hero.Bounds.IntersectsWith(labelH.Bounds))
                {
                    if (healthPoints >= 100)
                    {
                        MessageBox.Show("В вас максимально хп!");
                    }
                    else if (healthPoints == 95)
                    {
                        healthPoints += 5;

                    }
                    else
                    {
                        healthPoints += 10;
                    }
                    Controls.Remove(labelH);
                    labelH.Dispose();
                    break;
                }
            }

            if (isCollision || IsOutOfBounds(hero))
            {
                healthPoints -= 5;
                if (healthPoints <= 0)
                {
                    MessageBox.Show("Ви програли!");
                    Close();
                }
                else
                {
                    hero.Location = startPosition;
                }
            }
            labelHP.Text = $"HP: {healthPoints}%";
            labelCoinText.Text = $"Coins: {collectedCoins}";

            if (collectedCoins == 7)
            {
                MessageBox.Show("Вітаємо, ви перемогли!");
                Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private bool IsOutOfBounds(Label label)
        {
            return label.Left < 0 || label.Right > ClientSize.Width || label.Top < 0 || label.Bottom > ClientSize.Height;
        }
    }
}
