using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMMCopyCat_Spelprojekt
{
    class Skills
    {
        protected int value;
        
        public Skills()
        {
            value = 1;
        }

        public void Upgrade()
        {
            value++;
        }

        public int Value()
        {
            return value;
        }
    }

    class Attack : Skills
    {
        public Attack ()
        {
            value = 5;
        }
    }

    class Defend : Skills
    {
        public Defend()
        {
            value = 3;
        }
    }
}
