import { useQuery } from "@tanstack/react-query";
import { getNpcCharacters } from "../../../services/apiCharacters";

export function useNpcs() {
  const {
    isLoading,
    data: npcs,
    error,
  } = useQuery({
    queryKey: ["npcs"],
    queryFn: getNpcCharacters,
  });

  return { isLoading, npcs, error };
}
// export function useNpcsExtended() {
//   const {
//     isLoading,
//     data: npcs,
//     error,
//   } = useQuery({
//     queryKey: ["npcsExtended"],
//     queryFn: getNpcCharactersExtended,
//   });

//   return { isLoading, npcs, error };
// }
