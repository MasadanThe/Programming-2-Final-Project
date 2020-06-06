using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace HOMMCopyCat_Spelprojekt
{
    class Texture
    {
        protected int width;
        protected int height;
        protected int x;
        protected int y;
        public Texture()
        {
            width = 20;
            height = 20;
            x = 0;
            y = 0;
        }

        public int Width
        {
            get { return width; }
        }
        public int Height
        {
            get { return height; }
        }
        public int X
        {
            get { return x; }
        }
        public int Y
        {
            get { return y; }
        }
        public virtual string Draw()
        {
            return null;
        }
    }

    class White : Texture
    {
        public White()
        {
        }
        public White(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string Draw()
        {
            return "whiteTexture";
        }
    }

    class Grass : Texture
    {
        public Grass()
        {
        }
        public Grass(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string Draw()
        {
            return "grassTexture";
        }
    }

    class FightingGrass : Texture
    {
        public FightingGrass(int x, int y)
        {
            width = 100;
            height = 100;
            this.x = x;
            this.y = y;
        }
        public override string Draw()
        {
            return "fightingGrassTexture";
        }
    }

    class City: Texture
    {
        public City()
        {
        }
        public City(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string Draw()
        {
            return "cityTexture";
        }
    }

    class Button : Texture
    {
        string texture;
        public Button()
        {
            width = 20;
            height = 20;
        }

        public Button(int width, int height, int x, int y, string texture)
        {
            this.width = width;
            this.height = height;
            this.x = x;
            this.y = y;
            this.texture = texture;
        }
        public override string Draw()
        {
            return texture;
        }
    }

    class Building : Texture
    {
        public Building(int x, int y)
        {
            width = 100;
            height = 100;
            this.x = x;
            this.y = y;
        }

    }

    class PugilistBuilding : Building
    {
        public PugilistBuilding(int x, int y) : base(x, y)
        {

        }

        public override string Draw()
        {
            return "PugilistBuilding";
        }
    }

    class SlingerBuilding : Building
    {
        public SlingerBuilding(int x, int y): base(x, y)
        {

        }
        public override string Draw()
        {
            return "SlingerBuilding";
        }
    }

    class PoisonMasterBuilding : Building
    {
        public PoisonMasterBuilding(int x, int y) : base(x, y)
        {

        }
        public override string Draw()
        {
            return "PoisonMasterBuilding";
        }
    }

    class KnightBuilding : Building
    {
        public KnightBuilding(int x, int y) : base(x, y)
        {

        }
        public override string Draw()
        {
            return "KnightBuilding";
        }
    }

    class PyromancerBuilding : Building
    {
        public PyromancerBuilding(int x, int y) : base(x, y)
        {

        }
        public override string Draw()
        {
            return "PyromancerBuilding";
        }
    }

    class Character : Texture
    {
        public Character(int x, int y)
        {
            width = 100;
            height = 100;
            this.x = x;
            this.y = y;
        }
    }

    class Soldiers : Character
    {
        string texture;
        public Soldiers(int x, int y, string texture) : base(x ,y)
        {
            this.texture = texture;
        }

        public override string Draw()
        {
            return texture;
        }
    }
}
