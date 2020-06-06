using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMMCopyCat_Spelprojekt
{
    class Cities
    {
        //List with all the buildings in the city
        protected List<Buildings> built = new List<Buildings>();

        public Cities()
        {

        }

        public List<Buildings> EnterTown()
        {
            //Sends the list of the buildings
            return built;
        }


        public List<bool> SaveBool()
        {
            List<bool> saveData = new List<bool>();
            for (int i = 0; i < 5; i++)
            {
                saveData.Add(built[i].built);
            }

            return saveData;
        }

        public void LoadBool(bool build1, bool build2, bool build3, bool build4, bool build5)
        {
            built[0].built = build1;
            built[1].built = build2;
            built[2].built = build3;
            built[3].built = build4;
            built[4].built = build5;
        }

        public List<double> SaveDouble()
        {
            List<double> saveData = new List<double>();
            for (int i = 0; i < 5; i++)
            {
                saveData.Add((built[i] as MilitaryBuilding).weeklyBoughtTroops);
            }

            return saveData;
        }
        public void LoadDouble(double weeklyBoughtTroops1, double weeklyBoughtTroops2, double weeklyBoughtTroops3, double weeklyBoughtTroops4, double weeklyBoughtTroops5)
        {
            (built[0] as MilitaryBuilding).weeklyBoughtTroops = Convert.ToInt32(weeklyBoughtTroops1);
            (built[1] as MilitaryBuilding).weeklyBoughtTroops = Convert.ToInt32(weeklyBoughtTroops2);
            (built[2] as MilitaryBuilding).weeklyBoughtTroops = Convert.ToInt32(weeklyBoughtTroops3);
            (built[3] as MilitaryBuilding).weeklyBoughtTroops = Convert.ToInt32(weeklyBoughtTroops4);
            (built[4] as MilitaryBuilding).weeklyBoughtTroops = Convert.ToInt32(weeklyBoughtTroops5);
        }
    }

    class Town1 : Cities
    {
        public Town1()
        {
            //Adds the buildings
            built.Add(new MilitaryBuilding(1, 30, 1, "Pugilist", 500, 5, 5));
            built.Add(new MilitaryBuilding(2, 20, 2, "Slinger", 2500, 10, 10));
            built.Add(new MilitaryBuilding(3, 15, 3, "Poison Master", 5000, 15, 15));
            built.Add(new MilitaryBuilding(4, 10, 4, "Knight", 10000, 25, 25));
            built.Add(new MilitaryBuilding(5, 5, 5, "Pyromancer", 15000, 35, 35));
        }
    }
}
