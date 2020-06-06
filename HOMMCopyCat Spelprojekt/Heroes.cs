using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMMCopyCat_Spelprojekt
{
    class Heroes
    {
        Random r = new Random();
        public string name;
        public int maxMovementSpeed;
        public int movementSpeed;
        double XP;
        double XPNeeded = 100;
        public int attackP;
        public int defendP;
        public int xPos;
        public int yPos;
        List<Skills> skills = new List<Skills>();
        public Army army;
        

        public Heroes(int team)
        {
            name = "Hero";
            skills.Add(new Attack());
            skills.Add(new Defend());
            maxMovementSpeed = 3;
            movementSpeed = maxMovementSpeed;
            attackP = skills[0].Value();
            defendP = skills[1].Value();

            //Adds all the different troops to the army
            army = new Army(team);
            army.AddPugilist();
            army.AddSlinger();
            army.AddPoisonMaster();
            army.AddKnight();
            army.AddPyromancer();
            
        }
        public List<double> Save()
        {
            List<double> saveData = new List<double>();
            saveData.Add(movementSpeed);
            saveData.Add(XP);
            saveData.Add(XPNeeded);
            saveData.Add(attackP);
            saveData.Add(defendP);
            saveData.Add(xPos);
            saveData.Add(yPos);
            for (int i = 0; i < 5; i++)
            {
                saveData.Add(army.troops[i].troopSize);
            }

            return saveData;
        }

        public void Load(double movementSpeed, double XP, double XPNeeded, double attackP, double defendP, double xPos, double yPos, double troopSize1, double troopSize2, double troopSize3, double troopSize4, double troopSize5)
        {
            this.movementSpeed = Convert.ToInt32(movementSpeed);
            this.XP = XP;
            this.XPNeeded = XPNeeded;
            this.attackP = Convert.ToInt32(attackP);
            this.defendP = Convert.ToInt32(defendP);
            this.xPos = Convert.ToInt32(xPos);
            this.yPos = Convert.ToInt32(yPos);
            army.troops[0].troopSize = troopSize1;
            army.troops[1].troopSize = troopSize2;
            army.troops[2].troopSize = troopSize3;
            army.troops[3].troopSize = troopSize4;
            army.troops[4].troopSize = troopSize5;
        }

        //Upgrade Attack
        public void UpgradeAttack()
        {
            skills[0].Upgrade();
        }

        //Upgrade Defense
        public void UpgradeDefense()
        {
            skills[1].Upgrade();
        }

        //Earn XP
        public void EarnXP(double xp)
        {
            XP += xp;
        }

        //Upgrade
        public void Upgrade()
        {
            if (XP >= XPNeeded)
            {
                XP -= XPNeeded;
                XPNeeded = Math.Ceiling(XPNeeded * 1.25);

                for (int i = 0; i < 5; i++)
                {
                    if (r.Next(2) == 0)
                    {
                        UpgradeAttack();
                    }
                    else
                    {
                        UpgradeDefense();
                    }
                }
                attackP = skills[0].Value();
                defendP = skills[1].Value();
            }
        }

        //Adds Pugilists to the army
        public void AddPugilist()
        {
            army.troops[0].Buy();
        }

        //Adds Slingers to the army
        public void AddSlinger()
        {
            army.troops[1].Buy();
        }

        //Adds Poison Masters to the army
        public void AddPoisonMaster()
        {
            army.troops[2].Buy();
        }

        //Adds Knights to the army
        public void AddKnight()
        {
            army.troops[3].Buy();
        }

        //Adds Pyromancers to the army
        public void AddPyromancer()
        {
            army.troops[4].Buy();
        }
    }

    class Boris : Heroes
    {
        public Boris(int team) : base(team)
        {
            name = "Boris";
        }
    }

}
