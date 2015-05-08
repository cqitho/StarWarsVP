﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace StarWarsVP
{

    public class Scene : Sprites
    {
        private int PLAYER_Y;
        public static Rectangle Bounds;
        private List<Enemy> Enemies;
        private Player Player;
        private List<Bullet> Bullets;
        private Random random;
        private int count;

        public Scene(Rectangle Rectangle)
        {
            Shape.DEFAULT_RADIUS = Rectangle.Width / 20;
            Bounds = new Rectangle(Rectangle.X, Rectangle.Y, Rectangle.Width - Shape.DEFAULT_RADIUS, Rectangle.Height);
            PLAYER_Y = Bounds.Bottom - 50;
            Player = new Player(new Point(Bounds.Left + Bounds.Width / 2 - 15, Bounds.Bottom - Shape.DEFAULT_RADIUS*5));
            Enemies = new List<Enemy>();
            Bullets = new List<Bullet>();
            random = new Random();
            count = 0;
        }

        public void Draw(Graphics g)
        {
            foreach (Enemy e in Enemies)
            {
                e.Draw(g);
            }

            foreach (Bullet b in Bullets)
            {
                b.Draw(g);
            }

            Player.Draw(g);
        }

        private void GenerateEnemies()
        {
            int n = random.Next(0, 4);
            int lastX = -Shape.DEFAULT_RADIUS*2;
            int p = 0;
            for (int i = 0; i < n; i++)
            {
                p = random.Next(0, Bounds.Width);
                while( p >= lastX && p<=lastX+Shape.DEFAULT_RADIUS*2){
                    p = random.Next(0, Bounds.Width);
                }
                Enemies.Add(new Enemy(new Point((Shape.DEFAULT_RADIUS*2 + p)%Bounds.Width, Bounds.Top-Shape.DEFAULT_RADIUS + i * Shape.DEFAULT_RADIUS * 2)));
                lastX = p;
            }

        }

        public void DetectColisions()
        {
            foreach(Enemy e in Enemies){
                if (e.IsHit(Player))
                    {
                        Player.Life--;
                        e.Dead = true;
                        if (Player.Life == 0)
                        {
                            //GAME OVER
                            Player.Dead = true;
                        }
                    }
                foreach (Bullet b in Bullets)
                {
                    if (e.IsHit(b))
                    {
                        if (b.Type == BulletType.GREEN)
                        {
                            Player.Score++;
                        }
                        e.Dead = true;
                        b.Dead = true;
                    }
                    
                    if(Player.IsHit(b) && BulletType.RED == b.Type){
                        Player.Life--;
                        b.Dead = true;
                        if (Player.Life == 0)
                        {
                            //GAME OVER
                            Player.Dead = true;
                        }
                    }
                }

            }
        }

        public void Update()
        {
            DetectColisions();
            if (count++ % 20 == 0)
            {
                GenerateEnemies();
            }

            UpdateEnemies();

            List<Bullet> AliveBullets = new List<Bullet>();
            foreach (Bullet b in Bullets)
            {
                b.Move(Direction.UP);
                if (b.Position.Y >= Bounds.Top && b.Position.Y <= Bounds.Bottom && !b.Dead)
                {
                    AliveBullets.Add(b);
                }
            }

            Bullets = AliveBullets;
            
        }

        private void UpdateEnemies()
        {
            List<Enemy> Alive = new List<Enemy>();

            foreach (Enemy e in Enemies)
            {
                e.Move(Direction.DOWN);
                if (random.Next(0, 100) < 5)
                {
                    Bullets.AddRange(e.Shoot());
                }
                if (e.Position.Y >= Bounds.Top - Shape.DEFAULT_RADIUS && e.Position.Y <= Bounds.Bottom && !e.Dead)
                {
                    Alive.Add(e);
                }
            }

            Enemies = Alive;
        }

        public void Move(Direction Direction)
        {
            Player.Move(Direction);
        }

        public int Life()
        {
            return Player.Life;
        }


        public void Shoot()
        {
            Bullets.AddRange(Player.Shoot());
        }

        public int GetScore()
        {
            return Player.Score;
        }
    }
}
