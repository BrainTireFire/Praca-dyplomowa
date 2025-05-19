namespace pracadyplomowa.Models.DTOs
{
    public class WeaponAttacksForEncounterDto
    {
        public int Id { get; set; }
        public bool Main { get; set; } = false;

        public DiceSetDto Damage { get; set; } = null!;
        public DiceSetDto AttackBonus { get; set; } = null!;
        public int DamageType { get; set; }
        public int? Reach { get; set; }
        public int? Range { get; set; }
        public string WeaponName {get; set;} = "";
        public bool RequiredWeaponAttackAvailable { get; set; }
    }
}