import { useMutation, useQueryClient } from "@tanstack/react-query";
import {
  makeWeaponAttack as makeWeaponAttackApi,
  WeaponAttackResultDto,
} from "../../../services/apiEncounter";
import toast from "react-hot-toast";
import { stateType } from "../session/WeaponAttackConditionalEffectsReducer";

export function useMakeWeaponAttack(
  encounterId: number,
  characterId: number,
  targetId: number,
  weaponId: number,
  isRanged: boolean,
  onSuccess: (result: WeaponAttackResultDto) => void
) {
  const queryClient = useQueryClient();
  const { mutate: makeWeaponAttack, isPending } = useMutation({
    mutationFn: (weaponAttackData: stateType) => {
      return makeWeaponAttackApi(
        encounterId,
        characterId,
        targetId,
        weaponId,
        isRanged,
        weaponAttackData
      );
    },
    onSuccess: (result: WeaponAttackResultDto) => {
      queryClient.invalidateQueries({
        queryKey: ["participance", encounterId, characterId],
      });
      toast.success(describeWeaponAttackResult(result), { duration: 10000 });
      onSuccess(result);
    },
    onError: (err) => toast.error(err.message),
  });

  return { makeWeaponAttack, isPending };
}

export default useMakeWeaponAttack;

/**
 * Generates a user-friendly description of a WeaponAttackResultDto.
 * @param {WeaponAttackResultDto} attackResult - The attack result object.
 * @returns {string} - A readable description of the attack result.
 */
function describeWeaponAttackResult(
  attackResult: WeaponAttackResultDto
): string {
  if (!attackResult) {
    return "No attack result provided.";
  }

  const { attackRollResult, powerResult, totalDamage, hitpointsLeft } =
    attackResult;

  // Describe the attack roll result and total damage
  let description = `Attack Result: ${attackRollResult}\n`;
  description += `Total Damage: ${totalDamage}\n`;
  description += `Hitpoints left: ${hitpointsLeft}\n`;

  // Describe the power results
  if (powerResult && powerResult.length > 0) {
    description += `Power Results:\n`;
    powerResult.forEach((power, index) => {
      description += `  ${index + 1}. Power Name: ${
        power.powerName
      }, Success: ${power.success ? "Yes" : "No"}\n`;
    });
  } else {
    description += `No powers were used in this attack.\n`;
  }

  return description.trim();
}
