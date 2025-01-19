import { useMutation } from "@tanstack/react-query";
import {
  ConditionalEffectsDtos,
  makeAttackRoll as makeAttackRollApi,
} from "../../../services/apiEncounter";
import toast from "react-hot-toast";

export function useApplyWeaponHitEffects(
  encounterId: number,
  characterId: number,
  targetId: number,
  weaponId: number,
  onSuccess: (x: string) => void
) {
  const { mutate: makeAttackRoll, isPending } = useMutation({
    mutationFn: (approvedConditionalEffects: ConditionalEffectsDtos) => {
      return makeAttackRollApi(
        encounterId,
        characterId,
        targetId,
        weaponId,
        approvedConditionalEffects
      );
    },
    onSuccess: (result: string) => {
      toast.success(result);
      onSuccess(result);
    },
    onError: (err) => toast.error(err.message),
  });

  return { makeAttackRoll, isPending };
}

export default useMakeAttackRoll;
