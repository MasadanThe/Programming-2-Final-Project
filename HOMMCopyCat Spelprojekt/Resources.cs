using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMMCopyCat_Spelprojekt
{
    class Resources
    {
        public int value;
        protected int income;

        public Resources()
        {
            value = 0;
        }

        public void Add()
        {
            value += income;
        }

        public void Increase(int increase)
        {
            income += increase;
        }

        public void Decrease(int decrease)
        {
            income -= decrease;
        }

        public void Minus(int resource)
        {
            value -= resource;
        }

        public virtual string Value()
        {
            return value.ToString();
        }

        
    }

    class Gold : Resources
    {

        public Gold()
        {
            income = 1000;
        }

        public override string Value()
        {
            return "Gold: " + value.ToString();
        }
    }

    class Wood : Resources
    {
        public Wood()
        {
            income = 2;
        }

        public override string Value()
        {
            return "Wood: " + value.ToString();
        }
    }

    class Stone: Resources
    {
        public Stone()
        {
            income = 2;
        }

        public override string Value()
        {
            return "Stone: " + value.ToString();
        }
    }
}
