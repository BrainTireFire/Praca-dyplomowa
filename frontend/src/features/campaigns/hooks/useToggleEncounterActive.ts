import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { useNavigate, useParams } from "react-router-dom";
import { toggleEncounterActive as toggleEncounterActiveAPI } from "../../../services/apiEncounter";

function useToggleEncounterActive() {
  const { campaignId } = useParams<{ campaignId: string }>();
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  const { mutate: toggleEncounterActive, isPending } = useMutation({
    mutationFn: (encounterId: number) => toggleEncounterActiveAPI(encounterId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["encounters"] });
      navigate(`/campaigns/${campaignId}/encounters`);
    },
    onError: (error) => toast.error(error.message),
  });

  return { toggleEncounterActive, isPending };
}

export default useToggleEncounterActive;
