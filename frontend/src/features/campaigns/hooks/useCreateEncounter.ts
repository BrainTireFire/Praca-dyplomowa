import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createEncounter as createEncounterAPI } from "../../../services/apiEncounter";
import toast from "react-hot-toast";
import { EncounterCreateDto } from "../../../models/encounter/EncounterCreateDto";
import { useNavigate } from "react-router-dom";

export function useCreateEncounter() {
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  const { mutate: createEncounter, isPending } = useMutation({
    mutationFn: (encounter: EncounterCreateDto) =>
      createEncounterAPI(encounter),
    onSuccess: () => {
      toast.success("Encounter created");
      queryClient.invalidateQueries({ queryKey: ["encounters"] });
      navigate("/campaigns", { replace: true });
    },
    onError: (error) => {
      //console.error(error);
      toast.error("Encounter creation failed");
    },
  });

  return { createEncounter, isPending };
}