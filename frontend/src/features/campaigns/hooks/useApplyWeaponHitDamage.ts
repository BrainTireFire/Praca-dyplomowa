import { useMutation } from "@tanstack/react-query";
import {
  AppliedDamage,
  applyWeaponHitEffects as applyWeaponHitEffectsApi,
  ApprovedConditionalEffectsDto,
} from "../../../services/apiEncounter";
import toast from "react-hot-toast";

export function describeDamage(appliedDamage: AppliedDamage): string {
  return Object.entries(appliedDamage)
    .map(([damageType, damageValue]) => `${damageType}: ${damageValue}`)
    .join(", ");
}

export function useApplyWeaponHitDamage(
  encounterId: number,
  characterId: number,
  targetId: number,
  weaponId: number,
  isRanged: boolean,
  onSuccess: (x: AppliedDamage) => void
) {
  const { mutate: applyWeaponHitDamage, isPending } = useMutation({
    mutationFn: ({
      approvedConditionalEffects,
      isCritical,
    }: {
      approvedConditionalEffects: ApprovedConditionalEffectsDto;
      isCritical: boolean;
    }) => {
      return applyWeaponHitEffectsApi(
        encounterId,
        characterId,
        targetId,
        weaponId,
        isRanged,
        isCritical,
        approvedConditionalEffects
      );
    },
    onSuccess: (result: AppliedDamage) => {
      toast.success(describeDamage(result));
      onSuccess(result);
    },
    onError: (err) => toast.error(err.message),
  });

  return { applyWeaponHitEffects: applyWeaponHitDamage, isPending };
}

export default useApplyWeaponHitDamage;
