using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMMCopyCat_Spelprojekt
{
    class Buildings
    {
        public bool built;
        public int goldCost;
        public int woodCost;
        public int stoneCost;
        public int buildingTier;

        public Buildings()
        {
            built = false;
        }
    }

    class MilitaryBuilding : Buildings
    {
        private int weeklyTroopProduction;
        public int weeklyBoughtTroops;
        public int troopCost;
        int troopTier;
        string troopName;
        public MilitaryBuilding(int buildingTier, int weeklyTroopProduction, int troopTier, string troopName, int goldCost, int woodCost, int stoneCost)
        {
            this.buildingTier = buildingTier;
            this.weeklyTroopProduction = weeklyTroopProduction;
            this.troopTier = troopTier;
            this.troopName = troopName;
            this.goldCost = goldCost;
            this.woodCost = woodCost;
            this.stoneCost = stoneCost;
            //Styckpriset av soldater
            troopCost = 20 * this.troopTier;
        }

        //Om built är sant så returnerar den det innan ":" och om det är falskt returnerar den det efter ":"
        public int AvailableTroops() => built ? weeklyTroopProduction - weeklyBoughtTroops : 0;
    }
}
