using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Utility
{
    public class Utils
    {
        public static Ability SkillToAbility(Skill skill){
            return (Ability)(((int)skill) / 10);
        }
    }
}