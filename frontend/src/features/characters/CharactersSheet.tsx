import styled, { css } from "styled-components";
import StatsContainer from "../../ui/characters/StatsContainer";
import EquipmentTable from "./tables/EquipmentTable";
import ProficiencyBox from "../../ui/characters/ProficiencyBox";
import Box from "../../ui/containers/Box";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import KnownLanguagesTable from "./tables/KnownLanguagesTable";
import ToolProficiencyTable from "./tables/ToolProficiencyTable";
import DisplayBox, { DisplayBoxContent } from "./DisplayBox";
import TextArea from "../../ui/forms/TextArea";
import WeaponArmorProficiencyTable from "./tables/WeaponArmorProficiencyTable";
import ClassTable from "./tables/ClassTable";
import WeaponAttackTable from "./tables/WeaponAttacksTable";
import ReadyPowerTable from "./tables/ReadyPowersTable";
import ConstantEffectTable from "./tables/ConstantEffectTable";
import EffectTable from "./tables/EffectTable";
import ResourceTable from "./tables/ResourceTable";
import PowersTable from "./tables/PowersTable";
import { useCharacter } from "./hooks/useCharacter";
import Spinner from "../../ui/interactive/Spinner";
import Modal from "../../ui/containers/Modal";
import Button from "../../ui/interactive/Button";
import NewCharacter from "./NewCharacter";
import SelectFromChoiceGroupScreen from "./SelectFromChoiceGroupScreen";
import { CharacterIdContext } from "./contexts/CharacterIdContext";
import { useContext } from "react";
import EquipmentSlotScreen from "./EquipmentSlotScreen";

const MainGrid = styled.div`
  display: grid;
  grid-template-columns: auto auto auto;
  gap: 0.4rem;
  max-height: 100%;
  height: 100%;
  overflow: hidden; /* Prevent the grid from overflowing */
`;

const MainGridColumn1 = styled.div`
  display: grid;
  grid-template-columns: auto auto auto;
  grid-template-rows: repeat(6, minmax(0, auto));
  grid-column-start: 1;
  grid-column-end: 2;
  gap: 0.4rem;
  max-height: 100%;
  height: 100%;
  overflow: auto;
`;
const MainGridColumn2 = styled.div`
  display: grid;
  grid-template-columns: auto auto auto auto;
  grid-template-rows: repeat(7, minmax(0, auto));
  grid-column-start: 2;
  grid-column-end: 3;
  gap: 0.4rem;
  max-height: 100%;
  height: 100%;
  overflow: auto;
`;
const MainGridColumn3 = styled.div`
  display: grid;
  grid-template-columns: auto;
  grid-template-rows: repeat(4, minmax(0, auto));
  grid-column-start: 3;
  grid-column-end: 4;
  gap: 0.4rem;
  max-height: 100%;
  height: 100%;
  overflow: auto;
`;

export default function CharactersSheet() {
  const { characterId } = useContext(CharacterIdContext);
  const { isLoading, isError, error, character } = useCharacter(characterId);

  console.log(characterId);
  console.log(character);
  if (isLoading) {
    return <Spinner />;
  }
  if (isError) {
    return `${error}`;
  }

  return (
    <Box
      radius="tiny"
      variation="fullScreen"
      customStyles={css`
        height: 100%;
      `}
    >
      {character && (
        <MainGrid>
          <MainGridColumn1>
            <div style={{ gridColumnStart: 1, gridColumnEnd: 3 }}>
              <FormRowVertical label="Name">
                <Input
                  size="small"
                  customStyles={css`
                    text-transform: uppercase;
                  `}
                  value={character.name}
                ></Input>
              </FormRowVertical>
            </div>
            <div
              style={{
                gridColumnStart: 1,
                gridColumnEnd: 3,
                gridRowStart: 2,
                display: "flex",
                flexDirection: "column",
              }}
            >
              <FormRowVertical label="Description" fillHeight={true}>
                <TextArea value={character.description}></TextArea>
              </FormRowVertical>
            </div>
            <div style={{ gridColumnStart: 3, gridRowStart: 1, gridRowEnd: 4 }}>
              <ProficiencyBox
                data={character.skills}
                header="Skills"
              ></ProficiencyBox>
            </div>
            <div style={{ gridColumnStart: 2, gridRowStart: 3, gridRowEnd: 4 }}>
              <ProficiencyBox
                data={character.savingThrows}
                header="Saving throws"
              ></ProficiencyBox>
            </div>
            <div
              style={{ gridColumnStart: 1, gridRowStart: 3, gridRowEnd: -1 }}
            >
              <StatsContainer stats={character.attributes}></StatsContainer>
            </div>
            <div
              style={{
                gridColumnStart: 2,
                gridColumnEnd: -1,
                gridRowStart: 4,
                gridRowEnd: 5,
              }}
            >
              <KnownLanguagesTable
                languages={character.languages}
              ></KnownLanguagesTable>
            </div>
            <div
              style={{
                gridColumnStart: 2,
                gridColumnEnd: -1,
                gridRowStart: 5,
                gridRowEnd: 6,
              }}
            >
              <ToolProficiencyTable
                toolFamilies={character.toolProficiencies}
              ></ToolProficiencyTable>
            </div>
            <div
              style={{
                gridColumnStart: 2,
                gridColumnEnd: -1,
                gridRowStart: 6,
                gridRowEnd: 7,
              }}
            >
              <WeaponArmorProficiencyTable
                weaponAndArmorProficiencies={
                  character.weaponAndArmorProficiencies
                }
              ></WeaponArmorProficiencyTable>
            </div>
          </MainGridColumn1>
          <MainGridColumn2>
            <div
              style={{
                gridColumnStart: 1,
                gridColumnEnd: 3,
                gridRowStart: 1,
                gridRowEnd: 3,
              }}
            >
              <ClassTable
                characterClasses={character.classes}
                characterId={characterId}
              ></ClassTable>
            </div>
            <div
              style={{
                gridColumnStart: 1,
                gridColumnEnd: 3,
                gridRowStart: 2,
                gridRowEnd: 3,
              }}
            >
              <Modal>
                <Modal.Open opens="DevelopCharacter">
                  <Button variation="primary">Develop character</Button>
                </Modal.Open>
                <Modal.Window name="DevelopCharacter">
                  <SelectFromChoiceGroupScreen
                    characterId={character.id}
                    onCloseModal={() => {}}
                  ></SelectFromChoiceGroupScreen>
                </Modal.Window>
              </Modal>
            </div>
            <div style={{ gridColumnStart: 3, gridColumnEnd: 5 }}>
              <DisplayBox label="Race">
                <DisplayBoxContent>{character.race.name}</DisplayBoxContent>
              </DisplayBox>
            </div>
            <div
              style={{ gridColumnStart: 3, gridColumnEnd: 5, gridRowStart: 2 }}
            >
              <DisplayBox label="Size">
                <DisplayBoxContent>{character.size.name}</DisplayBoxContent>
              </DisplayBox>
            </div>
            <div
              style={{ gridColumnStart: 1, gridColumnEnd: 2, gridRowStart: 3 }}
            >
              <DisplayBox label="Hit points">
                <DisplayBoxContent>
                  {`${character.hitPoints.current}/${character.hitPoints.maximum} (+${character.hitPoints.temporary})`}
                </DisplayBoxContent>
              </DisplayBox>
            </div>
            <div
              style={{ gridColumnStart: 2, gridColumnEnd: 3, gridRowStart: 3 }}
            >
              <DisplayBox label="Initiative">
                <DisplayBoxContent>{character.initiative}</DisplayBoxContent>
              </DisplayBox>
            </div>
            <div
              style={{ gridColumnStart: 3, gridColumnEnd: 4, gridRowStart: 3 }}
            >
              <DisplayBox label="Speed">
                <DisplayBoxContent>{character.speed}</DisplayBoxContent>
              </DisplayBox>
            </div>
            <div
              style={{ gridColumnStart: 4, gridColumnEnd: 5, gridRowStart: 3 }}
            >
              <DisplayBox label="Armor Class">
                <DisplayBoxContent>{character.armorClass}</DisplayBoxContent>
              </DisplayBox>
            </div>
            <div
              style={{ gridColumnStart: 1, gridColumnEnd: 5, gridRowStart: 4 }}
            >
              <WeaponAttackTable weaponAttacks={character.weaponAttacks} />
            </div>
            <div
              style={{ gridColumnStart: 1, gridColumnEnd: 5, gridRowStart: 5 }}
            >
              <EquipmentTable equipments={character.equipment} />
            </div>
            <div
              style={{ gridColumnStart: 1, gridColumnEnd: 5, gridRowStart: 6 }}
            >
              <ReadyPowerTable powers={character.preparedPowers} />
            </div>
          </MainGridColumn2>
          <MainGridColumn3>
            <div style={{ gridColumnStart: 1, gridRowStart: 1 }}>
              <ConstantEffectTable effects={character.constantEffects} />
            </div>
            <div style={{ gridColumnStart: 1, gridRowStart: 2 }}>
              <EffectTable effects={character.effects} />
            </div>
            <div style={{ gridColumnStart: 1, gridRowStart: 3 }}>
              <ResourceTable resources={character.resources} />
            </div>
            <div style={{ gridColumnStart: 1, gridRowStart: 4 }}>
              <PowersTable powers={character.knownPowers} />
            </div>
          </MainGridColumn3>
        </MainGrid>
      )}
      <EquipmentSlotScreen onCloseModal={() => {}}></EquipmentSlotScreen>
    </Box>
  );
}
