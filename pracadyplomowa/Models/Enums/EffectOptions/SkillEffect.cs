namespace pracadyplomowa.Models.Enums.EffectOptions
{
    public enum SkillEffect
    {
        Bonus,
        Advantage,
        RerollLowerThan,
        Proficiency,
        Expertise,
        UpgradeToExpertise // upgrade existing skill proficiency to expertise by supplying additional expertise effect. No need to specify skill in effect blueprint
    }
}