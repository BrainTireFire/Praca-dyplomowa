import styled, { css } from "styled-components";
import StatsContainer from "../../ui/characters/StatsContainer";
import EquipmentTable from "./tables/EquipmentTable";
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
import SelectFromChoiceGroupScreen from "./SelectFromChoiceGroupScreen";
import { CharacterIdContext } from "./contexts/CharacterIdContext";
import { useContext, useEffect, useState } from "react";
import AddEquipmentScreen from "./AddEquipmentScreen";
import { EditModeContext } from "../../context/EditModeContext";
import { useUpdateCharacter } from "./hooks/useUpdateCharacter";
import SavingThrowProficiencyContainer from "../../ui/characters/SavingThrowProficiencyContainer";
import SkillProficiencyContainer from "../../ui/characters/SkillProficiencyContainer";

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
  const { editMode } = useContext(EditModeContext);
  const { isLoading, isError, error, character } = useCharacter(characterId);
  const [name, setName] = useState<string>("");
  const [description, setDescription] = useState<string>("");
  useEffect(() => {
    setName(character?.name ?? "");
    setDescription(character?.description ?? "");
  }, [character?.description, character?.name]);
  const {
    isPending,
    updateCharacter,
    isError: isErrorUpdate,
    error: errorUpdate,
  } = useUpdateCharacter(characterId as number, () => {});

  console.log(characterId);
  console.log(character);
  if (isLoading || isPending) {
    return <Spinner />;
  }
  if (isError) {
    return `${error}`;
  }
  if (isErrorUpdate) {
    return `${errorUpdate}`;
  }

  let disableForm = !editMode;
  let EditDescriptiveFieldsPermission =
    character?.accessLevels.includes("EditDescriptiveFields") ?? false;
  let EditEffectsPermission =
    character?.accessLevels.includes("EditEffects") ?? false;
  let EditEquipmentInBackpackPermission =
    character?.accessLevels.includes("EditEquipmentInBackpack") ?? false;
  let EditEquippingItemsPermission =
    character?.accessLevels.includes("EditEquippingItems") ?? false;
  let EditLevelingUpPermission =
    character?.accessLevels.includes("EditLevelingUp") ?? false;
  let EditPowersKnownPermission =
    character?.accessLevels.includes("EditPowersKnown") ?? false;
  let EditResourcesPermission =
    character?.accessLevels.includes("EditResources") ?? false;
  let EditSpellbookPermission =
    character?.accessLevels.includes("EditSpellbook") ?? false;

  const InvalidName = name.length <= 0 || name.length > 40;

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
              <FormRowVertical
                label="Name"
                error={
                  InvalidName
                    ? "Name must be filled in and cannot exceed 40 signs"
                    : undefined
                }
              >
                <Input
                  disabled={disableForm || !EditDescriptiveFieldsPermission}
                  size="medium"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
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
                <TextArea
                  disabled={disableForm || !EditDescriptiveFieldsPermission}
                  value={description}
                  onChange={(e) => setDescription(e.target.value)}
                ></TextArea>
              </FormRowVertical>
              {!disableForm &&
                EditDescriptiveFieldsPermission &&
                !!characterId && (
                  <Button
                    disabled={InvalidName}
                    onClick={() =>
                      updateCharacter({
                        name: name,
                        description: description,
                      })
                    }
                  >
                    Update name and description
                  </Button>
                )}
            </div>
            <div style={{ gridColumnStart: 3, gridRowStart: 1, gridRowEnd: 4 }}>
              <SkillProficiencyContainer
                data={character.skills}
                header="Skills"
              ></SkillProficiencyContainer>
            </div>
            <div style={{ gridColumnStart: 2, gridRowStart: 3, gridRowEnd: 4 }}>
              <SavingThrowProficiencyContainer
                data={character.savingThrows}
                header="Saving throws"
              ></SavingThrowProficiencyContainer>
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
            {/* <div
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
            </div> */}
            <div
              style={{
                gridColumnStart: 2,
                gridColumnEnd: -1,
                gridRowStart: 5,
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
              <EditModeContext.Provider
                value={{
                  editMode:
                    editMode &&
                    EditLevelingUpPermission &&
                    character.canLevelUp,
                }}
              >
                <ClassTable
                  characterClasses={character.classes}
                  characterId={characterId as number}
                ></ClassTable>
              </EditModeContext.Provider>
            </div>
            <div
              style={{
                gridColumnStart: 1,
                gridColumnEnd: 3,
                gridRowStart: 2,
                gridRowEnd: 3,
              }}
            >
              {editMode && EditLevelingUpPermission && (
                <div style={{display: "flex", justifyContent: "center", alignItems: "center", margin: "5px"}}>
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
              )}
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
              <EditModeContext.Provider
                value={{ editMode: editMode && EditEquippingItemsPermission }}
              >
                <EquipmentTable equipments={character.equipment} />
              </EditModeContext.Provider>
              {editMode && EditEquipmentInBackpackPermission && (
                <div style={{display: "flex", justifyContent: "center", alignItems: "center", margin: "5px"}}>
                  <Modal>
                    <Modal.Open opens="AddNewItem">
                      <Button>Add new item</Button>
                    </Modal.Open>
                    <Modal.Window name="AddNewItem">
                      <AddEquipmentScreen></AddEquipmentScreen>
                    </Modal.Window>
                  </Modal>
                </div>
              )}
            </div>
            <div
              style={{ gridColumnStart: 1, gridColumnEnd: 5, gridRowStart: 6 }}
            >
              <EditModeContext.Provider
                value={{ editMode: editMode && EditSpellbookPermission }}
              >
                <ReadyPowerTable powers={character.preparedPowers} />
              </EditModeContext.Provider>
            </div>
          </MainGridColumn2>
          <MainGridColumn3>
            <div style={{ gridColumnStart: 1, gridRowStart: 1 }}>
              <EditModeContext.Provider
                value={{ editMode: editMode && EditEffectsPermission }}
              >
                <ConstantEffectTable effects={character.constantEffects} />
              </EditModeContext.Provider>
            </div>
            <div style={{ gridColumnStart: 1, gridRowStart: 2 }}>
              <EditModeContext.Provider
                value={{ editMode: editMode && EditEffectsPermission }}
              >
                <EffectTable effects={character.effects} />
              </EditModeContext.Provider>
            </div>
            <div style={{ gridColumnStart: 1, gridRowStart: 3 }}>
              <EditModeContext.Provider
                value={{ editMode: editMode && EditResourcesPermission }}
              >
                <ResourceTable resources={character.resources} />
              </EditModeContext.Provider>
            </div>
            <div style={{ gridColumnStart: 1, gridRowStart: 4 }}>
              <EditModeContext.Provider
                value={{ editMode: editMode && EditPowersKnownPermission }}
              >
                <PowersTable powers={character.knownPowers} />
              </EditModeContext.Provider>
            </div>
          </MainGridColumn3>
        </MainGrid>
      )}
    </Box>
  );
}
