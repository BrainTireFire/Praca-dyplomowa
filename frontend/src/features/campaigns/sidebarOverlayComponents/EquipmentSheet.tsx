import styled, { css } from "styled-components";
import { Character } from "../../../models/character";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Input from "../../../ui/forms/Input";
import TextArea from "../../../ui/forms/TextArea";
import ProficiencyBox from "../../../ui/characters/ProficiencyBox";
import StatsContainer from "../../../ui/characters/StatsContainer";
import KnownLanguagesTable from "../../characters/tables/KnownLanguagesTable";
import ToolProficiencyTable from "../../characters/tables/ToolProficiencyTable";
import WeaponArmorProficiencyTable from "../../characters/tables/WeaponArmorProficiencyTable";
import { EditModeContext } from "../../../context/EditModeContext";
import ClassTable from "../../characters/tables/ClassTable";
import { useContext } from "react";
import DisplayBox, { DisplayBoxContent } from "../../characters/DisplayBox";
import AttributeBox from "../../../ui/characters/AttributeBox";
import { DiceSetString } from "../../../models/diceset";
import ConstantEffectTable from "../../characters/tables/ConstantEffectTable";
import EffectTable from "../../characters/tables/EffectTable";
import WeaponAttackTable from "../../characters/tables/WeaponAttacksTable";
import EquipmentTable from "../../characters/tables/EquipmentTable";
import Modal from "../../../ui/containers/Modal";
import Button from "../../../ui/interactive/Button";
import AddEquipmentScreen from "../../characters/AddEquipmentScreen";

const MainGrid = styled.div`
  display: grid;
  grid-template-columns: auto;
  grid-template-rows: auto auto auto;
  gap: 0.4rem;
  max-height: 100%;
  height: 100%;
  overflow: hidden; /* Prevent the grid from overflowing */
`;

export function EquipmentSheet({ character }: { character: Character }) {
  const { editMode } = useContext(EditModeContext);
  let EditEffectsPermission =
    character?.accessLevels.includes("EditEffects") ?? false;
  let EditEquipmentInBackpackPermission =
    character?.accessLevels.includes("EditEquipmentInBackpack") ?? false;
  let EditEquippingItemsPermission =
    character?.accessLevels.includes("EditEquippingItems") ?? false;
  if (!character) {
    return <></>;
  }
  return (
    <MainGrid>
      <div style={{ gridColumnStart: 1, gridRowStart: 1 }}>
        <WeaponAttackTable weaponAttacks={character.weaponAttacks} />
      </div>
      <div style={{ gridColumnStart: 1, gridRowStart: 2 }}>
        <EditModeContext.Provider
          value={{ editMode: editMode && EditEquippingItemsPermission }}
        >
          <EquipmentTable equipments={character.equipment} />
        </EditModeContext.Provider>
        {editMode && EditEquipmentInBackpackPermission && (
          <Modal>
            <Modal.Open opens="AddNewItem">
              <Button>Add new item</Button>
            </Modal.Open>
            <Modal.Window name="AddNewItem">
              <AddEquipmentScreen></AddEquipmentScreen>
            </Modal.Window>
          </Modal>
        )}
      </div>
      <div style={{ gridColumnStart: 1, gridRowStart: 3 }}>
        <EditModeContext.Provider
          value={{ editMode: editMode && EditEffectsPermission }}
        >
          <ConstantEffectTable effects={character.constantEffects} />
        </EditModeContext.Provider>
      </div>
    </MainGrid>
  );
}
