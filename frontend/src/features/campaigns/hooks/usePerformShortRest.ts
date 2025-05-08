import { useMutation, useQueryClient } from '@tanstack/react-query';
import { performShortRest as performShortRestApi} from '../../../services/apiCampaigns';
import { DiceSet } from '../../../models/diceset';
import toast from 'react-hot-toast';


// Hook
export const usePerformShortRest = (campaignId: number, onSuccess: () => any) => {
  const queryClient = useQueryClient();
  const { mutate: performShortRest, isPending } = useMutation({
    mutationFn: (hitDiceMap: Record<number, DiceSet>) => performShortRestApi(campaignId, hitDiceMap),
    onSuccess: () => {
      // queryClient.invalidateQueries({
      //   queryKey: ["participance", encounterId, characterId],
      // });
      // queryClient.invalidateQueries({
      //   queryKey: ["concentration", characterId],
      // });
      toast.success("Short rest performed succesfully");
      onSuccess();
    },
    onError: (err) => toast.error(err.message),
  });

  return { performShortRest, isPending };
};