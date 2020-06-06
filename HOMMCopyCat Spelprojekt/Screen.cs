using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace HOMMCopyCat_Spelprojekt
{
    class Screen
    {
        //Resolution
        protected int screenWidth;
        protected int screenHeight;
        protected List<Texture> textures = new List<Texture>();

        public Screen()
        {
            screenWidth = 1000;
            screenHeight = 600;
        }

        public int _screenWidth
        {
            get { return screenWidth; }
        }
        public int _screenHeight
        {
            get { return screenHeight; }
        }

        public List<Texture> Draw()
        {
            return textures;
        }
        public void AddWhite()
        {
            for (int y = 0; y < screenHeight; y += 20)
            {
                for (int x = 0; x < screenWidth; x += 20)
                {
                    textures.Add(new White(x, y));
                }
            }
        }
    }
    class GameOverScreen : Screen
    {
        public GameOverScreen()
        {
            screenHeight = 700;
            AddWhite();
            textures.Add(new Button(200, 50, 400, 500, "Restart"));
        }
    }

    class ESCScreen : Screen
    {
        public ESCScreen()
        {
            screenHeight = 700;
            AddWhite();
            textures.Add(new Button(200, 50, 400, 100, "Spara"));
            textures.Add(new Button(200, 50, 400, 250, "AvslutaVald"));
            textures.Add(new Button(200, 50, 400, 400, "TillbakaVald"));
            textures.Add(new Button(200, 50, 400, 550, "Restart"));
        }
    }

    class LowerScreen : Screen
    {
        public LowerScreen()
        {
            screenWidth = 1000;
            screenHeight = 100;
        }
    }

    class ResourceScreen : LowerScreen
    {
        public List<Resources> resources = new List<Resources>();
        public ResourceScreen()
        {
            resources.Add(new Gold());
            resources.Add(new Wood());
            resources.Add(new Stone());
            textures.Add(new Button(100, 100, 900, 600, "NextTurn"));
        }

        public List<Resources> Resources()
        {
            return resources;
        }

        public List<double> Save()
        {
            List<double> saveData = new List<double>();
            for (int i = 0; i < 3; i++)
            {
                saveData.Add(resources[i].value);
            }
            return saveData;
        }
    }


    class MainMenu : Screen
    {
        public MainMenu()
        {
            textures.Add(new Button(200, 50, 400, 100, "NyttSpelVald"));
            textures.Add(new Button(200, 50, 400, 200, "LaddaInVald"));
            textures.Add(new Button(200, 50, 400, 300, "AvslutaVald"));
        }
    }
    class NewGameMenu : Screen
    {
        public NewGameMenu()
        {
            textures.Add(new Button(200, 50, 200, 100, "LättVald"));
            textures.Add(new Button(200, 50, 200, 200, "MedelVald"));
            textures.Add(new Button(200, 50, 200, 300, "SvårtVald"));
            textures.Add(new Button(200, 50, 600, 100, "Bana1Vald"));
            textures.Add(new Button(200, 50, 600, 200, "Bana2Vald"));
            textures.Add(new Button(200, 50, 600, 300, "TillbakaVald"));
            textures.Add(new Button(200, 50, 400, 400, "StartaVald"));
        }
    }

    class World : Screen
    {
        Random random = new Random();
        public List<Army> armies = new List<Army>();
        public World(int city1X, int city1Y, int heroX, int heroY)
        {
            List<int> citiesX = new List<int>();
            List<int> citiesY = new List<int>();

            for (int y = city1Y; y < city1Y + 60; y += 20)
            {
                for (int x = city1X; x < city1X + 60; x += 20)
                {
                    textures.Add(new City(x, y));
                    citiesX.Add(x);
                    citiesY.Add(y);
                }
            }
            for (int y = 0; y < screenHeight; y += 20)
            {
                for (int x = 0; x < screenWidth; x += 20)
                {
                    if (citiesX.Contains(x) && citiesY.Contains(y))
                    {

                    }
                    else
                    {
                        textures.Add(new Grass(x, y));
                        //Randomise and can't hit the hero
                        if (random.Next(10) == 1 && x != heroX && y != heroY)
                        {
                            armies.Add(new Enemy(0, true, x, y));
                        }
                    }
                }
            }
            foreach (Enemy a in armies)
            {
                int randomNumber = random.Next(5);
                a.RandomEnemy(randomNumber);
            }
        }
    }

    class FightingScreen : Screen
    {
        public FightingScreen()
        {
            screenHeight = base.screenHeight + 100;
            for (int y = 0; y < screenHeight; y += 100)
            {
                for (int x = 0; x < screenWidth; x += 100)
                {
                    textures.Add(new FightingGrass(x, y));
                }
            }
        }
    }

    class CityScreen : Screen
    {
        public CityScreen()
        {
            AddWhite();
            textures.Add(new PugilistBuilding(50, 100));
            textures.Add(new SlingerBuilding(250, 100));
            textures.Add(new PoisonMasterBuilding(450, 100));
            textures.Add(new KnightBuilding(650, 100));
            textures.Add(new PyromancerBuilding(850, 100));
            textures.Add(new Soldiers(50, 300, "Pugilist"));
            textures.Add(new Soldiers(250, 300, "Slinger"));
            textures.Add(new Soldiers(450, 300, "PoisonMaster"));
            textures.Add(new Soldiers(650, 300, "Knight"));
            textures.Add(new Soldiers(850, 300, "Pyromancer"));
        }
    }

}
