import { useMutation } from "@tanstack/react-query";
import {
  ApprovedConditionalEffectsDto,
  makeAttackRoll as makeAttackRollApi,
} from "../../../services/apiEncounter";
import toast from "react-hot-toast";

export function useMakeAttackRoll(
  encounterId: number,
  characterId: number,
  targetId: number,
  weaponId: number,
  isRanged: boolean,
  onSuccess: (x: string) => void
) {
  const { mutate: makeAttackRoll, isPending } = useMutation({
    mutationFn: (approvedConditionalEffects: ApprovedConditionalEffectsDto) => {
      return makeAttackRollApi(
        encounterId,
        characterId,
        targetId,
        weaponId,
        isRanged,
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
