import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updatePlaceEncounter as updatePlaceEncounterAPI } from "../../../services/apiEncounter";
import toast from "react-hot-toast";
import { EncounterUpdateDto } from "../../../models/encounter/EncounterUpdateDto";
import { useNavigate, useParams } from "react-router-dom";

export function useUpdatePlaceEncounter(id: number, startEncounter?: boolean) {
  const { campaignId } = useParams<{ campaignId: string }>();
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  const { mutate: updatePlaceEncounter, isPending: isUpdating } = useMutation({
    mutationFn: ({
      encounterId,
      encounterUpdateDto,
    }: {
      encounterId: number;
      encounterUpdateDto: EncounterUpdateDto[];
    }) => {
      var setEncounterPositionDto = {
        IsActive: startEncounter ? true : false,
        FieldsToUpdate: encounterUpdateDto,
      };

      return updatePlaceEncounterAPI(encounterId, setEncounterPositionDto);
    },
    onSuccess: () => {
      toast.success("Encounter updated");
      queryClient.invalidateQueries({ queryKey: ["encounters"] });

      if (startEncounter) {
        navigate("/campaigns/" + campaignId + `/session/${id}`);
      } else {
        navigate("/campaigns/" + campaignId + `/encounters`);
      }
    },
    onError: (error) => {
      toast.error("Encounter update failed");
    },
  });

  return { updatePlaceEncounter, isUpdating };
}
