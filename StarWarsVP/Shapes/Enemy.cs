﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace StarWarsVP
{
    public class Enemy : Shape, Armed
    {
        private Random random;
        private int Dir;
        //private EnemyType Type;

        public Enemy(Point position)
            : base(position)
        {
            random = new Random();
            VelocityY = random.Next(4,6);
            Dir = random.Next() % 2 == 0 ? 1 : -1;
        }

        public override void Move(Direction direction)
        {
            if (!Hit)
            {
                int newVel = random.Next(0, 11);
                VelocityX = newVel;
                if (random.Next(0, 100) < 1)
                {
                    Dir = -Dir;
                }

                if (Position.X - VelocityX * Dir <= Scene.Bounds.Left + 40)
                {
                    Dir = -Dir;
                }

                if (Position.X + VelocityX * Dir >= Scene.Bounds.Right - 40)
                {
                    Dir = -Dir;
                }

                if (Position.X + VelocityX * Dir >= Scene.Bounds.Left || Position.X + VelocityX * Dir <= Scene.Bounds.Right)
                {
                    Position = new Point(Position.X + VelocityX * Dir, Position.Y + VelocityY);
                }
                else
                {
                    Position = new Point(Position.X, Position.Y + VelocityY);
                }
            }
        }

        public override void Draw(Graphics g)
        {
            Image i = SpriteList.Instance.Imperial[0];
            if (!Hit)
            {
                if (DateTime.Now.Millisecond%2==0)
                {
                    //i = SpriteList.Instance.Imperial[1];
                }
            }
            else
            {
                timeToDie++;
                if (timeToDie == 10)
                {
                    Dead = true;
                }
                i = SpriteList.Instance.Explosion[timeToDie];
            }
            g.DrawImage(i,Position.X + DEFAULT_RADIUS+DEFAULT_RADIUS/10,Position.Y+DEFAULT_RADIUS,DEFAULT_RADIUS*2,DEFAULT_RADIUS*2);
        }


        public List<Bullet> Shoot()
        {
            List<Bullet> Bullets = new List<Bullet>();
            Bullets.Add(new Bullet(new Point(Position.X + DEFAULT_RADIUS, Position.Y + DEFAULT_RADIUS*2), BulletType.RED));
            return Bullets; 
        }
    }
}