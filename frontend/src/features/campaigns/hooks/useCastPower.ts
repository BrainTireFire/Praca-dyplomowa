import { useMutation, useQueryClient } from "@tanstack/react-query";
import {
  castPower as castPowerApi,
  CastPowerResultDto,
  HitType,
} from "../../../services/apiEncounter";
import toast from "react-hot-toast";
import { StateType } from "../session/PowerCastConditionalEffectsReducer";

export function useCastPower(
  encounterId: number,
  characterId: number,
  powerId: number,
  onSuccess: (result: CastPowerResultDto) => void
) {
  const queryClient = useQueryClient();
  const { mutate: castPower, isPending } = useMutation({
    mutationFn: (powerCastData: StateType) => {
      return castPowerApi(encounterId, characterId, powerId, powerCastData);
    },
    onSuccess: (result: CastPowerResultDto) => {
      queryClient.invalidateQueries({
        queryKey: ["participance", encounterId, characterId],
      });

      function formatCastPowerResult(dto: CastPowerResultDto): string {
        // Define a mapping from serialized values to human-readable values
        const hitTypeDisplayMap: Record<HitType, string> = {
          CriticalHit: "Critical hit",
          Hit: "Hit",
          CriticalMiss: "Critical miss",
          Miss: "Miss",
        };

        const lines: string[] = [];

        for (const id in dto.nameMap) {
          const name = dto.nameMap[id];
          const rawHitType = dto.hitMap[id];
          const hitType = hitTypeDisplayMap[rawHitType] || "Unknown"; // Translate or fallback to "Unknown"
          lines.push(`${name}: ${hitType}`);
        }

        return lines.join("\n");
      }

      toast.success(formatCastPowerResult(result), { duration: 10000 });
      onSuccess(result);
    },
    onError: (err) => toast.error(err.message),
  });

  return { castPower, isPending };
}

export default useCastPower;
