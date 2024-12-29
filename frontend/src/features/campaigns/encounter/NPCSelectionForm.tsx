import { useState } from "react";
import styled, { css } from "styled-components";
import { useNpcs } from "../hooks/useNpcs";
import { ReusableTable } from "../../../ui/containers/ReusableTable";
import Button from "../../../ui/interactive/Button";
import Spinner from "../../../ui/interactive/Spinner";
import { CharacterItem } from "../../../models/character";
import { useCharacter } from "../../characters/hooks/useCharacter";
import CharactersSheet from "../../characters/CharactersSheet";
import { CharacterIdContext } from "../../characters/contexts/CharacterIdContext";

export function NPCSelectionForm({
  initialNpcList,
  onConfirm,
  onCloseModal,
}: {
  initialNpcList: CharacterItem[];
  onConfirm: (selectedCharacters: CharacterItem[]) => void;
  onCloseModal: () => {};
}) {
  const { isLoading: isLoadingAllNpcs, npcs: allNpcs } = useNpcs();
  // {
  //   CastableBy: "Character",
  // }
  const [npcsLocal, setNpcsLocal] = useState(initialNpcList);

  const [selectedNpcIdFromAll, setSelectedNpcIdFromAll] = useState<
    number | null
  >(null);
  const [selectedNpcIdFromEncounter, setSelectedNpcIdFromEncounter] = useState<
    number | null
  >(null);

  const allNpcsWithoutEncounter = allNpcs
    ? allNpcs.filter(
        (npc: any) => !npcsLocal?.find((localNpc) => localNpc.id === npc.id)
      )
    : [];

  const handleSelectAllNpcs = (row: { id: number; name: string }) => {
    let selectedItem = allNpcsWithoutEncounter?.find(
      (_value: any, index: number) => index === row.id
    );
    setSelectedNpcIdFromAll(selectedItem ? selectedItem.id : null);
    setSelectedNpcIdFromEncounter(null);
  };
  const handleSelectItemPowers = (row: { id: number; name: string }) => {
    let selectedItem = npcsLocal?.find((_value, index) => index === row.id);
    setSelectedNpcIdFromEncounter(selectedItem ? selectedItem.id : null);
    setSelectedNpcIdFromAll(null);
  };

  let selectedCharacterId = selectedNpcIdFromAll || selectedNpcIdFromEncounter;

  return (
    <Grid>
      <Row1>
        {!isLoadingAllNpcs && (
          <ReusableTable
            mainHeader="All available NPCs"
            tableRowsColomns={{ Name: "name" }}
            data={allNpcsWithoutEncounter.map((npc: any, index: number) => {
              return {
                id: index,
                name: npc.name,
              };
            })}
            isSelectable={true}
            onSelect={handleSelectAllNpcs}
          ></ReusableTable>
        )}
        {isLoadingAllNpcs && <Spinner />}
      </Row1>
      <Row2>
        <Button
          disabled={selectedNpcIdFromAll === null}
          onClick={() => {
            setNpcsLocal(() => {
              return [
                ...(npcsLocal as CharacterItem[]),
                allNpcs?.find(
                  (npc: any) => npc.id === selectedNpcIdFromAll
                ) as CharacterItem,
              ];
            });
            setSelectedNpcIdFromAll(null);
          }}
        >
          {"Add to encounter"}
        </Button>
        <Button
          disabled={selectedNpcIdFromEncounter === null}
          onClick={() => {
            setNpcsLocal(() => {
              return (npcsLocal as CharacterItem[]).filter(
                (npc) => npc.id !== selectedNpcIdFromEncounter
              );
            });
            setSelectedNpcIdFromEncounter(null);
          }}
        >
          {"Remove from encounter"}
        </Button>
        <Button
          onClick={() => {
            onConfirm(npcsLocal);
            onCloseModal();
          }}
        >
          {"Save"}
        </Button>
      </Row2>
      <Row3>
        <ReusableTable
          mainHeader="Selected NPCs"
          tableRowsColomns={{ Name: "name" }}
          data={
            npcsLocal?.map((npcs, index) => {
              return {
                id: index,
                name: npcs.name,
              };
            }) ?? []
          }
          isSelectable={true}
          onSelect={handleSelectItemPowers}
        ></ReusableTable>
      </Row3>
      <SheetContainer>
        {selectedCharacterId && (
          <CharacterIdContext.Provider
            value={{ characterId: selectedCharacterId }}
          >
            <CharactersSheet></CharactersSheet>
          </CharacterIdContext.Provider>
        )}
      </SheetContainer>
    </Grid>
  );
}

const Grid = styled.div`
  display: grid;
  grid-template-columns: 20% 80%;
  grid-template-rows: auto auto auto;
  grid-column-gap: 10px;
  width: 90vw;
  height: 90vh;
  overflow-y: hidden;
  overflow-x: hidden;
`;

const Row1 = styled.div`
  grid-column: 1;
  grid-row: 1/2;
`;
const Row2 = styled.div`
  grid-column: 1;
  grid-row: 2/3;
  display: flex;
  flex-direction: column;
  row-gap: 10px;
`;
const Row3 = styled.div`
  grid-column: 1;
  grid-row: 3/4;
`;
const SheetContainer = styled.div`
  grid-column: 2;
  grid-row-start: 1;
  grid-row-end: 4;
  overflow-y: auto;
  overflow-x: auto;
  scrollbar-color: var(--color-button-primary) var(--color-main-background);
  scrollbar-width: thin;
  scrollbar-gutter: stable;
`;
