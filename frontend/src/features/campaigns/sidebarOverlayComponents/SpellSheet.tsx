import styled from "styled-components";
import { Character } from "../../../models/character";
import { EditModeContext } from "../../../context/EditModeContext";
import { useContext } from "react";
import ReadyPowerTable from "../../characters/tables/ReadyPowersTable";
import PowersTable from "../../items/tables/PowersTable";
import ResourceTable from "../../characters/tables/ResourceTable";

const MainGrid = styled.div`
  display: grid;
  grid-template-columns: auto;
  grid-template-rows: auto auto auto;
  gap: 0.4rem;
  max-height: 100%;
  height: 100%;
  overflow: hidden; /* Prevent the grid from overflowing */
`;

export function SpellSheet({ character }: { character: Character }) {
  const { editMode } = useContext(EditModeContext);
  let EditPowersKnownPermission =
    character?.accessLevels.includes("EditPowersKnown") ?? false;
  let EditResourcesPermission =
    character?.accessLevels.includes("EditResources") ?? false;
  let EditSpellbookPermission =
    character?.accessLevels.includes("EditSpellbook") ?? false;
  if (!character) {
    return <></>;
  }
  return (
    <MainGrid>
      <div style={{ gridColumnStart: 1, gridRowStart: 1 }}>
        <EditModeContext.Provider
          value={{ editMode: editMode && EditSpellbookPermission }}
        >
          <ReadyPowerTable powers={character.preparedPowers} />
        </EditModeContext.Provider>
      </div>
      <div style={{ gridColumnStart: 1, gridRowStart: 2 }}>
        <EditModeContext.Provider
          value={{ editMode: editMode && EditPowersKnownPermission }}
        >
          <PowersTable powers={character.knownPowers} />
        </EditModeContext.Provider>
      </div>
      <div style={{ gridColumnStart: 1, gridRowStart: 3 }}>
        <EditModeContext.Provider
          value={{ editMode: editMode && EditResourcesPermission }}
        >
          <ResourceTable resources={character.resources} />
        </EditModeContext.Provider>
      </div>
    </MainGrid>
  );
}
