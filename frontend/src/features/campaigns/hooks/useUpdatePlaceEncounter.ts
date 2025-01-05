import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updatePlaceEncounter as updatePlaceEncounterAPI } from "../../../services/apiEncounter";
import toast from "react-hot-toast";
import { EncounterUpdateDto } from "../../../models/encounter/EncounterUpdateDto";
import { useNavigate, useParams } from "react-router-dom";

export function useUpdatePlaceEncounter(id: number) {
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
      return updatePlaceEncounterAPI(encounterId, encounterUpdateDto);
    },
    onSuccess: () => {
      toast.success("Encounter updated");
      queryClient.invalidateQueries({ queryKey: ["encounters"] });
      navigate("/campaigns/" + campaignId + `/session/${id}`);
    },
    onError: (error) => {
      toast.error("Encounter update failed");
    },
  });

  return { updatePlaceEncounter, isUpdating };
}
