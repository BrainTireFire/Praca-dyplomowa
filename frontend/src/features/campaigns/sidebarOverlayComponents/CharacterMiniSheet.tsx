import styled, { css } from "styled-components";
import { Character } from "../../../models/character";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Input from "../../../ui/forms/Input";
import { EditModeContext } from "../../../context/EditModeContext";
import ClassTable from "../../characters/tables/ClassTable";
import { useContext } from "react";
import DisplayBox, { DisplayBoxContent } from "../../characters/DisplayBox";
import AttributeBox from "../../../ui/characters/AttributeBox";
import { DiceSetString } from "../../../models/diceset";
import ConstantEffectTable from "../../characters/tables/ConstantEffectTable";
import EffectTable from "../../characters/tables/EffectTable";
import SavingThrowProficiencyContainer from "../../../ui/characters/SavingThrowProficiencyContainer";
import SkillProficiencyContainer from "../../../ui/characters/SkillProficiencyContainer";

const MainGrid = styled.div`
  display: grid;
  grid-template-columns: auto auto;
  grid-template-rows: auto auto;
  gap: 0.4rem;
  max-height: 100%;
  height: 100%;
  overflow: hidden; /* Prevent the grid from overflowing */
`;

const MainGridColumn1 = styled.div`
  display: grid;
  grid-template-columns: auto;
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
  grid-template-columns: auto;
  grid-template-rows: repeat(7, minmax(0, auto));
  grid-column-start: 2;
  grid-column-end: 3;
  gap: 0.4rem;
  max-height: 100%;
  height: 100%;
  overflow: auto;
`;

export function CharacterMiniSheet({ character }: { character: Character }) {
  const { editMode } = useContext(EditModeContext);
  let disableForm = !editMode;
  let EditDescriptiveFieldsPermission =
    character?.accessLevels.includes("EditDescriptiveFields") ?? false;
  let EditEffectsPermission =
    character?.accessLevels.includes("EditEffects") ?? false;
  let EditLevelingUpPermission =
    character?.accessLevels.includes("EditLevelingUp") ?? false;
  if (!character) {
    return <></>;
  }
  return (
    <MainGrid>
      <MainGridColumn1>
        <div style={{ gridColumnStart: 1, gridRow: 1 }}>
          <FormRowVertical label="Name">
            <Input
              disabled={disableForm || !EditDescriptiveFieldsPermission}
              size="small"
              customStyles={css`
                text-transform: uppercase;
              `}
              value={character.name}
            ></Input>
          </FormRowVertical>
        </div>
        <div style={{ gridColumnStart: 1, gridRowStart: 2, gridRowEnd: 3 }}>
          <SkillProficiencyContainer
            data={character.skills}
            header="Skills"
          ></SkillProficiencyContainer>
        </div>
        <div style={{ gridColumnStart: 1, gridRowStart: 3, gridRowEnd: 4 }}>
          <SavingThrowProficiencyContainer
            data={character.savingThrows}
            header="Saving throws"
          ></SavingThrowProficiencyContainer>
        </div>
        <div
          style={{
            gridColumnStart: 1,
            gridRowStart: 4,
            gridRowEnd: 5,
          }}
        >
          <EditModeContext.Provider
            value={{
              editMode:
                editMode && EditLevelingUpPermission && character.canLevelUp,
            }}
          >
            <ClassTable
              characterClasses={character.classes}
              characterId={character.id as number}
            ></ClassTable>
          </EditModeContext.Provider>
        </div>
      </MainGridColumn1>
      <MainGridColumn2>
        <div style={{ gridColumn: 1, gridRow: 1 }}>
          <DisplayBox label="Race">
            <DisplayBoxContent>{character.race.name}</DisplayBoxContent>
          </DisplayBox>
        </div>
        <div
          style={{
            gridColumnStart: 1,
            gridRowStart: 2,
            gridRowEnd: 3,
            display: "grid",
            gridTemplateColumns: "auto auto auto",
            gridTemplateRows: "auto auto",
          }}
        >
          <AttributeBox
            attribute={{
              name: character.attributes[0].name,
              value: character.attributes[0].value,
              modifier: character.attributes[0].modifier,
            }}
          ></AttributeBox>
          <AttributeBox
            attribute={{
              name: character.attributes[1].name,
              value: character.attributes[1].value,
              modifier: character.attributes[1].modifier,
            }}
          ></AttributeBox>
          <AttributeBox
            attribute={{
              name: character.attributes[2].name,
              value: character.attributes[2].value,
              modifier: character.attributes[2].modifier,
            }}
          ></AttributeBox>
          <AttributeBox
            attribute={{
              name: character.attributes[3].name,
              value: character.attributes[3].value,
              modifier: character.attributes[3].modifier,
            }}
          ></AttributeBox>
          <AttributeBox
            attribute={{
              name: character.attributes[4].name,
              value: character.attributes[4].value,
              modifier: character.attributes[4].modifier,
            }}
          ></AttributeBox>
          <AttributeBox
            attribute={{
              name: character.attributes[5].name,
              value: character.attributes[5].value,
              modifier: character.attributes[5].modifier,
            }}
          ></AttributeBox>
        </div>
        <div
          style={{
            gridColumnStart: 1,
            gridRowStart: 3,
            gridRowEnd: 4,
            display: "flex",
          }}
        >
          <DisplayBox label="Initiative">
            <DisplayBoxContent>{character.initiative}</DisplayBoxContent>
          </DisplayBox>
          <DisplayBox label="Speed">
            <DisplayBoxContent>{character.speed}</DisplayBoxContent>
          </DisplayBox>
          <DisplayBox label="Armor Class">
            <DisplayBoxContent>{character.armorClass}</DisplayBoxContent>
          </DisplayBox>
        </div>
        <div
          style={{
            gridColumnStart: 1,
            gridRowStart: 4,
            gridRowEnd: 5,
            display: "flex",
          }}
        >
          <DisplayBox label="Size">
            <DisplayBoxContent>{character.size.name}</DisplayBoxContent>
          </DisplayBox>
          <DisplayBox label="Hit Dice">
            <div>{`Total: ${DiceSetString(character.hitDice.total)}`}</div>
            <div>{`Left: ${DiceSetString(character.hitDice.left)}`}</div>
          </DisplayBox>
        </div>
        <div
          style={{
            gridColumnStart: 1,
            gridRowStart: 5,
            gridRowEnd: 6,
            display: "flex",
          }}
        >
          <DisplayBox label="Hit points">
            <DisplayBoxContent>
              {`${character.hitPoints.current}/${character.hitPoints.maximum} (+${character.hitPoints.temporary})`}
            </DisplayBoxContent>
          </DisplayBox>
          <DisplayBox label="Death saves">
            <div>{`Success: ${character.deathSaves.successes}`}</div>
            <div>{`Fail: ${character.deathSaves.failures}`}</div>
          </DisplayBox>
        </div>
        <div style={{ gridColumnStart: 1, gridRowStart: 6, gridRowEnd: 7 }}>
          <EditModeContext.Provider
            value={{ editMode: editMode && EditEffectsPermission }}
          >
            <EffectTable effects={character.effects} />
          </EditModeContext.Provider>
        </div>
        <div style={{ gridColumnStart: 1, gridRowStart: 7, gridRowEnd: 8 }}>
          <EditModeContext.Provider
            value={{ editMode: editMode && EditEffectsPermission }}
          >
            <ConstantEffectTable effects={character.constantEffects} />
          </EditModeContext.Provider>
        </div>
      </MainGridColumn2>
    </MainGrid>
  );
}
