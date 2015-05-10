﻿using StarWarsVP.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarWarsVP
{
    public partial class Game : Form
    {
        private Scene Scene;
        Timer timer;
        int time;
        static readonly int TIMER_INTERVAL = 40;
        public SpriteList Sprites;
        private int ttl;


        public Game()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            Sprites = SpriteList.GetSprites();
        }

        public void NewGame()
        {
            Scene = new Scene(pnlScene.DisplayRectangle);
            lblTime.Text = "00:00";
            pbHeart1.Visible = pbHeart2.Visible = pbHeart3.Visible = true;
            time = 0;
            ttl = 20;
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TIMER_INTERVAL;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Scene.Update();
            time++;
            lblScore.Text = string.Format("Score : {0}", Scene.GetScore());
            lblTime.Text = string.Format("{0:00}:{1:00}", (time/24)/60, (time/24)%60);
            if (Scene.GameOver())
            {
                if (ttl-- == 0)
                {
                    timer.Stop();
                }

            }

            switch (Scene.Life())
            {
                case 2: 
                    pbHeart3.Visible = false; 
                    break;
                case 1:
                    pbHeart2.Visible = false;
                    break;
                case 0:
                    pbHeart1.Visible = false;
                    break;
            }
            pnlScene.Invalidate();
        }


        public void ToggleViews()
        {
            pnlMainMenu.Visible = !pnlMainMenu.Visible;
            pnlOptions.Visible = !pnlOptions.Visible;
            pnlScore.Visible = !pnlScore.Visible;
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            if (Sprites.DoneLoading)
            {
                pnlScene.Visible = true;
                ToggleViews();
                NewGame();
            }
        }

        private void btnHighScores_Click(object sender, EventArgs e)
        {
            ToggleViews();
            pnlHighScores.Visible = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //TODO SAVE SCORE
            ToggleViews();
            if (timer != null)
            {
                timer.Stop();
            }
            Scene = null;
            pnlScene.Visible = false;
            pnlHighScores.Visible = false;
        }

        

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (Scene != null)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        Scene.Move(Direction.LEFT);
                        break;

                    case Keys.Right:
                        Scene.Move(Direction.RIGHT);
                        break;
                }

                if (e.KeyCode == Keys.D)
                {
                    Scene.Shoot();
                }
            }
            
        }

        private void pnlScene_Paint(object sender, PaintEventArgs e)
        {
            Scene.Draw(e.Graphics);
        }

        
        

    }


    



}
