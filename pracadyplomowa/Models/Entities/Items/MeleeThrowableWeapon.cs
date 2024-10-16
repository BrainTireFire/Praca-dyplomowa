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
        public MeleeThrowableWeapon(MeleeThrowableWeapon weapon) : base(weapon){
            this.Range = weapon.Range;
        }
        public int Range { get; set; }

        public override Item Clone(){
            return new MeleeThrowableWeapon(this);
        }
    }
}