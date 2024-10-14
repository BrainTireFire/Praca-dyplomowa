namespace pracadyplomowa.Models.Enums
{
    public enum CastableBy
    {
        Character = 0,
        OnWeaponHit = 1, // effects which are castable OnWeaponHit can actually be casted just like castable by Character but whether they hit is controlled by implicitly performed weapon attack which also applies its normal damage.
        Terrain = 2
    }
}