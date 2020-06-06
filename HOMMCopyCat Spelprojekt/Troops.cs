using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HOMMCopyCat_Spelprojekt
{
    [SettingsSerializeAs(SettingsSerializeAs.Xml)]
    class Troops
    {
        public int health;
        public int damage;
        public int offense;
        public int defense;
        public int movementSpeed;
        public double totalHealth;
        public string troopName;
        public double troopSize;
        public bool ranged;
        public int team;
        public float X;
        public float Y;
        protected int troopTier;

        public Troops()
        {

        }

        public Troops(int team)
        {
            this.team = team;
        }


        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            //If there are more enemies then 0
            if (troopSize > 0)
            {
                //Draw the character
                spriteBatch.Draw(texture, new Vector2(X, Y));
            }
        }

        public virtual void ResetPosition()
        {
            X = 0;
            Y = 0;
        }

        public void DrawText(SpriteBatch spriteBatch, SpriteFont font, Texture2D texture)
        {
            //If there are more enemies then 0
            if (troopSize > 0)
            {
                //Draw the text
                spriteBatch.DrawString(font, troopSize.ToString(), new Vector2(X, Y), Color.Black);
            }
        }

        public double Damage(int heroOffense)
        {
            //Ökar skadan beroende på offense och truppantal
            return (damage * troopSize) * Math.Pow(1.025, (offense + heroOffense));
            
        }

        public void SetTotalHealth()
        {
            totalHealth = troopSize * health;
        }

        public double Defense(double attack, int heroDefense)
        {
            double temporaryTroopSize;
            //Sänker skadan beroende på försvar
            attack = Math.Floor(attack * Math.Pow(0.95, (heroDefense + defense)));
            //Kollar hur många trupper det var från början
            temporaryTroopSize = troopSize;
            //Kollar om alla dör instant och då dödar alla
            if (attack >= totalHealth)
            {
                
                troopSize = 0;
                totalHealth = 0;
                return temporaryTroopSize * troopTier;
            }
            
            else
            {
                totalHealth -= attack;
                troopSize = Math.Ceiling(totalHealth / health);
                return (temporaryTroopSize - troopSize) * troopTier;
            }
        }

        //Adds soldiers to army
        public void Buy()
        {
            troopSize ++;
            totalHealth = troopSize * health;
        }
    }

    class Pugilist : Troops
    {
        public Pugilist(int team) : base(team)
        {
            health = 5;
            damage = 2;
            offense = 2;
            defense = 2;
            movementSpeed = 5;
            troopName = "Pugilist";
            troopSize = 0;
            ranged = false;
            totalHealth = 0;
            troopTier = 1;

        }

        public override void ResetPosition()
        {
            //If it is not the player
            if (team != 1)
            {
                X = 900;
            }
            else
            {
                X = 0;
            }
            Y = 0;
        }
    }

    class Slinger : Troops
    {
        public Slinger(int team) : base(team)
        {
            health = 4;
            damage = 3;
            offense = 5;
            defense = 0;
            movementSpeed = 4;
            troopName = "Slinger";
            troopSize = 0;
            ranged = true;
            totalHealth = 0;
            troopTier = 2;
        }
        public override void ResetPosition()
        {
            //If it is not the player
            if (team != 1)
            {
                X = 900;
            }
            else
            {
                X = 0;
            }
            Y = 200;
        }
    }

    class PoisionMaster : Troops
    {
        public PoisionMaster(int team) : base(team)
        {
            health = 10;
            damage = 9;
            offense = 12;
            defense = 5;
            movementSpeed = 5;
            troopName = "Poison Master";
            troopSize = 0;
            ranged = false;
            totalHealth = 0;
            troopTier = 3;
        }
        public override void ResetPosition()
        {
            //If it is not the player
            if (team != 1)
            {
                X = 900;
            }
            else
            {
                X = 0;
            }
            Y = 300;
        }
    }

    class Knight : Troops
    {
        public Knight(int team) : base(team)
        {
            health = 20;
            damage = 15;
            offense = 12;
            defense = 15;
            movementSpeed = 5;
            troopName = "Knight";
            troopSize = 0;
            ranged = false;
            totalHealth = 0;
            troopTier = 4;
        }
        public override void ResetPosition()
        {
            //If it is not the player
            if (team != 1)
            {
                X = 900;
            }
            else
            {
                X = 0;
            }
            Y = 500;
        }
    }

    class Pyromancer : Troops
    {
        public Pyromancer(int team) : base(team)
        {
            health = 60;
            damage = 30;
            offense = 25;
            defense = 15;
            movementSpeed = 4;
            troopName = "Pyromancer";
            troopSize = 0;
            ranged = true;
            totalHealth = 0;
            troopTier = 5;
        }
        public override void ResetPosition()
        {
            //If it is not the player
            if (team != 1)
            {
                X = 900;
            }
            else
            {
                X = 0;
            }
            Y = 600;
        }

    }
}
