using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMMCopyCat_Spelprojekt
{
    [SettingsSerializeAs(SettingsSerializeAs.Xml)]
    class Army
    {
        public int team;
        //Makes an army
        public List<Troops> troops = new List<Troops>();

        public Army()
        {
        }

        public Army(int team)
        {
            this.team = team;
        }

        public void AddPugilist()
        {
            troops.Add(new Pugilist(team));
        }

        public void AddSlinger()
        {
            troops.Add(new Slinger(team));
        }

        public void AddPoisonMaster()
        {
            troops.Add(new PoisionMaster(team));
        }

        public void AddKnight()
        {
            troops.Add(new Knight(team));
        }

        public void AddPyromancer()
        {
            troops.Add(new Pyromancer(team));
        }
    }

    class Enemy : Army
    {
        bool growth;
        int x;
        int y;
        public bool alive;
        
        public Enemy(int team, bool growth, int x, int y) : base(team)
        {
            this.team = 0;
            this.growth = growth;
            this.x = x;
            this.y = y;
            alive = true;
            AddPugilist();
            AddSlinger();
            AddPoisonMaster();
            AddKnight();
            AddPyromancer();
        }

        public int X { get { return x; } }
        public int Y { get { return y; } }

        public string Save()
        {
            string saveString;
            //Save X position
            saveString = x.ToString() + "S";
            //Save Y position
            saveString += y.ToString() + "S";
            //Save troops
            for (int i = 0; i < 5; i++)
            {
                saveString += troops[i].troopSize.ToString() + "S";
            }
            //Save Alive
            if (alive == true)
            {
                saveString += "1";
            }
            else
            {
                saveString += "0";
            }

            return saveString;

        }

        public void RandomEnemy(int r)
        {
            switch (r)
            {
                //Adds the troops
                case 0:
                    troops[0].troopSize = 25;
                    break;
                case 1:
                    troops[1].troopSize = 20;
                    break;
                case 2:
                    troops[2].troopSize = 15;
                    break;
                case 3:
                    troops[3].troopSize = 10;
                    break;
                case 4:
                    troops[4].troopSize = 5;
                    break;
            }
        }

        public void ChooseEnemy(int pugilist, int slinger, int poisonMaster, int knight, int pyromancer)
        {
            troops[0].troopSize = pugilist;
            troops[1].troopSize = slinger;
            troops[2].troopSize = poisonMaster;
            troops[3].troopSize = knight;
            troops[4].troopSize = pyromancer;
        }
        public string Draw()
        {
            return "enemyTexture";
        }
    }
}
