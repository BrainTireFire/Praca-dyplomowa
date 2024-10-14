using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Items
{
    public class MeleeThrowableWeapon : MeleeWeapon, IRangedWeapon
    {
        protected MeleeThrowableWeapon() : base(){
            
        }
        public int Range { get; set; }
    }
}