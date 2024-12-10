using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.DTOs
{
    public class ItemFormDto
    {
        public int? Id {get; set;}
        public string Name {get; set;}
        public string Description {get; set;}
        public int Weight {get; set;}
        public CoinPurseDto Price {get; set;}
        public int ItemFamilyId {get; set;}
        public enum ItemTypeEnum {
            MundaneItem,
            Tool,
            Apparel,
            MeleeWeapon,
            RangedWeapon,
        }
        public ItemTypeEnum ItemType {get; set;}

        public class Body {

        }

    }

    public class ToolFormDto : ItemFormDto {
        public Body ItemTypeBody {get; set;}
        public new class Body : ItemFormDto.Body {
            public Skill Skill {get; set;} 
        }
    }

    public abstract class EquippableItemFormDto : ItemFormDto {
        public new class Body : ItemFormDto.Body {
            public List<EffectBlueprintDto> EffectsOnWearer {get; set;} 
            public List<PowerDto> Powers {get; set;}
            public List<ResourceDto> ResourcesOnEquip {get; set;}
            public List<SlotDto> Slots {get; set;}
            public bool IsSpellFocus {get; set;}
            public bool OccupiesAllSlots {get; set;}

            public class EffectBlueprintDto {
                public int Id {get; set;}
                public string Name {get; set;}
            }
            public class PowerDto {
                public int Id {get; set;}
                public string Name {get; set;}
            }
            public class ResourceDto {
                public int Id {get; set;}
                public string Name {get; set;}
                public int Charges {get; set;}
            }
            public class SlotDto {
                public int Id {get; set;}
                public string Name {get; set;}
            }
        }
    }

    public class ApparelFormDto : EquippableItemFormDto {
        public Body ItemTypeBody {get; set;}
        public new class Body : EquippableItemFormDto.Body {
            public int MinimumStrength {get; set;}
            public bool DisadvantageOnStealth {get; set;}
            public int ArmorClass { get; set; }
        }
    }
    public abstract class WeaponFormDto : EquippableItemFormDto {

        public abstract new class Body : EquippableItemFormDto.Body {
            public DiceSetFormDto Damage {get; set;}
            public DamageType DamageType {get; set;}
            public WeaponWeight WeightProperty {get; set;}

            public class DiceSetFormDto {
                public int d20 { get; set; }
                public int d12 { get; set; }
                public int d10 { get; set; }
                public int d8 { get; set; }
                public int d6 { get; set; }
                public int d4 { get; set; }
                public int d100 { get; set; }
                public int flat {get; set;}
            }
        }
    }

    public class MeleeWeaponFormDto : WeaponFormDto {
        public Body ItemTypeBody {get; set;}
        public new class Body : WeaponFormDto.Body {
            public bool Reach {get; set;}
            public bool Finesse {get; set;}
            public bool Throwable { get; set;}
            public int RangeThrowable {get; set;}
            public bool Versatile { get; set; }
            public DiceSetFormDto VersatileDamage { get; set;}
        }
    }

    public class RangedWeaponFormDto : WeaponFormDto {
        public Body ItemTypeBody {get; set;}
        public new class Body : WeaponFormDto.Body {
            public int Range {get; set;}
            public bool Loaded {get; set;}
        }
    }

    // public class MeleeThrowableWeaponFormDto : MeleeWeaponFormDto {
    //     public new Body ItemTypeBody {get; set;}
    //     public new class Body : MeleeWeaponFormDto.Body {
    //         public int Range {get; set;}
    //     }
    // }
}