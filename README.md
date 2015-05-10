StarWars

Целта на апликацијата е да се имплементира едноставна 2D игра. Играта има функционалности на движење и пукање.
Контролите за играње се едноставни, движењата се ограничени. Начинот на играње е едноставен.
Целта е да уништите што е можно повеќе непријатели и да ги преживеете нивните напади.

Идејата за изгледот на играта е превземена од филмот Star Wars, леталата од тамошните летала кои се среќаваат.
Целиод код е смислен и ништо не е превземено од готови проекти и класи.

При уклучување на играта, корисникот се среќава со форма која дава опција за New Game, High Scores и Exit.
Со кликнување на New Game се започнува играта, играчот може да го движи леталото и да пука кон противнички играчи.
На формата може да се забележи времето од почетокот на играта, колку животи има и колкав резултат при уништувања на противнички летала. Има копче кое ве враќа првата форма. При кликнување на High Scores дадена е листа на највисоки резултати. При кликнување на Exit играта се гаси.

  ![](http://i.imgur.com/Vr5UHbx.png)

  ![](http://i.imgur.com/8kXxWVY.png)

Методот Move(Direction direction) од класата Scene, има функција ако не се погодени леталата да ги движи објектите во одредена насока. Од лева страна на мапата до плус 40, а од десна страна до минус 40.


public override void Move(Direction direction)
        {
            if (!Hit)
            {
                int L = Scene.Bounds.Left;
                int R = Scene.Bounds.Right;

                if (Position.X + VelocityX <= L || Position.X + VelocityX >= R)
                {
                    VelocityX = -VelocityX;
                }
                else
                {
                    if (random.Next() < 200)
                    {
                        int newVel = random.Next(1, 11);
                        VelocityX = VelocityX>0? -newVel:newVel;
                    }
                }

                Position = new Point(Position.X + VelocityX, Position.Y + VelocityY);
            }
        }


Контроли за игра:

    • Left Arrow - движење на лево со леталото.
    • Right Arrow - движење на десно со леталото.
    • D - пукање кон противнички летала.

Правила на игра:

    • Имате 3 животи и при секој удар се губи живот.
    • Противничките авиони имаат 1 живот.

Членови во тимот:
    
    • Славчо Малезанов 133066
    • Андреј Лаброски 131038
