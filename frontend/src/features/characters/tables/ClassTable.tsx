import Menus from "../../../ui/containers/Menus";
import Table, { TableButton } from "../../../ui/containers/Table";
import { CharacterClass } from "../../../models/characterclass";
import ClassRow from "./ClassRow";
import Modal from "../../../ui/containers/Modal";
import Button from "../../../ui/interactive/Button";
import Box from "../../../ui/containers/Box";
import styled, { css } from "styled-components";
import { useCharacterNextClassLevels } from "../hooks/useNextClassLevels";
import { useEffect, useState } from "react";
import {
  Effect,
  NextClassLevel,
  Power,
  Resource,
} from "../../../services/apiCharacters";
import Spinner from "../../../ui/interactive/Spinner";
import VerticalLine from "../../../ui/separators/VerticalLine";
import ElementToChoose from "../ElementToChoose";
import DisplayBox, { DisplayBoxContent } from "../DisplayBox";
import { DiceSetString } from "../../../models/diceset";
import Heading from "../../../ui/text/Heading";
import { useSelectNextClassLevel } from "../hooks/useSelectNextClassLevel";
import toast from "react-hot-toast";

export default function ClassTable({
  characterClasses,
  characterId,
}: {
  characterClasses: CharacterClass[];
  characterId: number;
}) {
  return (
    <Menus>
      <Table
        header="Class"
        button="Level up"
        modal={
          <ClassLevelSelectionScreen
            characterId={characterId}
            onCloseModal={() => {}}
          />
        }
        columns="1fr 1fr"
      >
        <Table.Header>
          <div>Name</div>
          <div>Level</div>
        </Table.Header>
        <Table.Body
          data={characterClasses}
          columnCount={2}
          render={(characterClass) => (
            <ClassRow key={characterClass.id} characterClass={characterClass} />
          )}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}

function ClassLevelSelectionScreen({
  characterId,
  onCloseModal,
}: {
  characterId: number;
  onCloseModal: () => void;
}) {
  //query
  const {
    isLoading: isLoadingNextLevels,
    nextLevels,
    error: errorNextLevels,
  } = useCharacterNextClassLevels(characterId);

  console.log(nextLevels);

  const [selectedNextClassId, setSelectedNextClassId] = useState<number | null>(
    null
  );
  const [selectedChoiceGroupId, setSelectedChoiceGroupId] = useState<
    number | null
  >(null);

  const [selectedEffectId, setSelectedEffectId] = useState<number | null>(null);
  const [selectedPowerId, setSelectedPowerId] = useState<number | null>(null);
  const [selectedResourceId, setSelectedResourceId] = useState<number | null>(
    null
  );

  const updateEffectId = (id: number): void => {
    setSelectedEffectId(id);
    setSelectedPowerId(null);
    setSelectedResourceId(null);
  };

  const updatePowerId = (id: number): void => {
    setSelectedEffectId(null);
    setSelectedPowerId(id);
    setSelectedResourceId(null);
  };

  const updateResourceId = (id: number): void => {
    setSelectedEffectId(null);
    setSelectedPowerId(null);
    setSelectedResourceId(id);
  };

  // State to store transformed data
  const [nextClassLevelsLocal, setNextClassLevelsLocal] = useState<
    NextClassLevelLocal[]
  >([]);

  // Function to transform fetched data
  const mapNextLevelsToLocal = (
    nextLevels: NextClassLevel[]
  ): NextClassLevelLocal[] => {
    return nextLevels.map((classLevel) => ({
      selected: false,
      ...classLevel,
    }));
  };

  // Update local state when data is fetched
  useEffect(() => {
    if (nextLevels) {
      const transformedData = mapNextLevelsToLocal(nextLevels);
      console.log(transformedData);
      setNextClassLevelsLocal(transformedData);
    }
  }, [nextLevels]);

  //mutation
  const { saveNextClassLevel, isPending } = useSelectNextClassLevel(() => {
    toast.success("Character developed!");
    onCloseModal();
    return;
  });

  if (errorNextLevels) {
    return "Error";
  }
  if (isLoadingNextLevels || isPending) {
    return <Spinner />;
  }
  console.log(nextClassLevelsLocal);

  return (
    <>
      <Heading as="h1">Class level selection</Heading>
      <MainGrid>
        <MainGridColumn1>
          <Heading as="h3">Class choices</Heading>
          {nextClassLevelsLocal.map((classLevel) => (
            <ChoiceGroupContainer
              key={classLevel.classId}
              onClick={() => setSelectedNextClassId(classLevel.id)}
              selected={classLevel.id === selectedNextClassId}
            >
              {classLevel.name + " " + classLevel.level}
            </ChoiceGroupContainer>
          ))}
        </MainGridColumn1>
        <VerticalLine></VerticalLine>
        <MainGridColumn2>
          <Heading as="h3">Class level properties</Heading>
          <DescriptionGrid>
            {selectedNextClassId && (
              <RandomInfo>
                <DisplayBox label="Hitpoints">
                  <DisplayBoxContent>
                    {
                      nextClassLevelsLocal.filter(
                        (x) => x.id === selectedNextClassId
                      )[0]?.hitPoints
                    }
                  </DisplayBoxContent>
                </DisplayBox>
                <DisplayBox label="Hit die">
                  <DisplayBoxContent>
                    {DiceSetString(
                      nextClassLevelsLocal.filter(
                        (x) => x.id === selectedNextClassId
                      )[0]?.hitDice
                    )}
                  </DisplayBoxContent>
                </DisplayBox>
              </RandomInfo>
            )}
            <ChoiceColumn1>
              <Heading as="h3">Category name</Heading>
              {nextClassLevelsLocal
                .filter((x) => x.id === selectedNextClassId)[0]
                ?.choiceGroups.map((choiceGroup) => (
                  <ChoiceGroupContainer
                    key={choiceGroup.id}
                    selected={choiceGroup.id === selectedChoiceGroupId}
                    onClick={() => setSelectedChoiceGroupId(choiceGroup.id)}
                  >
                    <ChoiceGroupName>{choiceGroup.name}</ChoiceGroupName>
                    {choiceGroup.numberToChoose > 0 && (
                      <>
                        <VerticalLine />
                        <span>{choiceGroup.numberToChoose} to select</span>
                      </>
                    )}
                  </ChoiceGroupContainer>
                ))}
            </ChoiceColumn1>
            <ChoiceColumn2>
              <Heading as="h3">Elements of category</Heading>
              {selectedChoiceGroupId &&
                nextClassLevelsLocal
                  .filter((x) => x.id === selectedNextClassId)[0]
                  ?.choiceGroups?.filter(
                    (cg) => cg.id === selectedChoiceGroupId
                  )[0]
                  ?.effects.map((effect) => (
                    <EffectPowerOption
                      effectOrPower={effect}
                      setSelectedEffectPowerId={updateEffectId}
                    ></EffectPowerOption>
                  ))}
              {selectedChoiceGroupId &&
                nextClassLevelsLocal
                  .filter((x) => x.id === selectedNextClassId)[0]
                  ?.choiceGroups?.filter(
                    (cg) => cg.id === selectedChoiceGroupId
                  )[0]
                  ?.powersAlwaysAvailable.map((power) => (
                    <EffectPowerOption
                      effectOrPower={power}
                      setSelectedEffectPowerId={updatePowerId}
                    ></EffectPowerOption>
                  ))}
              {selectedChoiceGroupId &&
                nextClassLevelsLocal
                  .filter((x) => x.id === selectedNextClassId)[0]
                  ?.choiceGroups?.filter(
                    (cg) => cg.id === selectedChoiceGroupId
                  )[0]
                  ?.resources.map((resource) => (
                    <EffectPowerOption
                      effectOrPower={resource}
                      setSelectedEffectPowerId={updateResourceId}
                    ></EffectPowerOption>
                  ))}
            </ChoiceColumn2>
            <ChoiceColumn3>
              <Heading as="h3">Elements description</Heading>
              {selectedEffectId &&
                nextClassLevelsLocal
                  .filter((x) => x.id === selectedNextClassId)[0]
                  ?.choiceGroups?.filter(
                    (cg) => cg.id === selectedChoiceGroupId
                  )[0]
                  ?.effects?.filter((effect) => effect.id === selectedEffectId)
                  .map((effect) => (
                    <ElementToChoose
                      key={effect.id}
                      element={effect}
                    ></ElementToChoose>
                  ))}
              {selectedPowerId &&
                nextClassLevelsLocal
                  .filter((x) => x.id === selectedNextClassId)[0]
                  ?.choiceGroups?.filter(
                    (cg) => cg.id === selectedChoiceGroupId
                  )[0]
                  ?.powersAlwaysAvailable?.filter(
                    (power) => power.id === selectedPowerId
                  )
                  .map((power) => (
                    <ElementToChoose
                      key={power.id}
                      element={power}
                    ></ElementToChoose>
                  ))}
              {selectedResourceId &&
                nextClassLevelsLocal
                  .filter((x) => x.id === selectedNextClassId)[0]
                  ?.choiceGroups?.filter(
                    (cg) => cg.id === selectedChoiceGroupId
                  )[0]
                  ?.resources?.filter(
                    (resource) => resource.id === selectedResourceId
                  )
                  .map((resource) => (
                    <ElementToChoose
                      key={resource.id}
                      element={resource}
                    ></ElementToChoose>
                  ))}
            </ChoiceColumn3>
          </DescriptionGrid>
        </MainGridColumn2>
      </MainGrid>
      <Button
        disabled={selectedNextClassId === null}
        onClick={() => {
          if (selectedNextClassId != null) {
            saveNextClassLevel({
              characterId,
              classLevelId: selectedNextClassId,
            });
          }
        }}
      >
        Select class level
      </Button>
    </>
  );
}

const MainGrid = styled(Box)`
  display: grid;
  grid-template-columns: 1fr auto 5fr;
  gap: 0.4rem;
  max-height: 500px;
  height: 500px;
  max-width: 800px;
  width: 800px;
  overflow: hidden; /* Prevent the grid from overflowing */
`;

const MainGridColumn1 = styled.div`
  display: flex;
  flex-direction: column;
  grid-column-start: 1;
  grid-column-end: 2;
  gap: 0.4rem;
  max-height: 100%;
  height: 100%;
  overflow: auto;
`;
const MainGridColumn2 = styled.div`
  display: flex;
  flex-direction: column;
  grid-column-start: 3;
  grid-column-end: 4;
  gap: 0.4rem;
  max-height: 100%;
  height: 100%;
  overflow: auto;
`;

const DescriptionGrid = styled(Box)`
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  grid-template-rows: auto 2fr;
  gap: 0.4rem;
  overflow: hidden; /* Prevent the grid from overflowing */
  height: 100%;
`;
const RandomInfo = styled.div`
  display: flex;
  flex-direction: row;
  grid-column-start: 1;
  grid-column-end: -1;
  grid-row-start: 1;
  grid-row-end: 2;
  gap: 0.4rem;
  max-height: 100%;
  height: 100%;
  max-width: 100%;
  width: 100%;
  overflow: auto;
  flex-wrap: wrap;
  border-bottom: 1px solid var(--color-border);
  margin-bottom: 3px;
  padding-bottom: 3px;
`;

const ChoiceColumn1 = styled.div`
  display: flex;
  flex-direction: column;
  grid-column-start: 1;
  grid-column-end: 2;
  grid-row-start: 2;
  grid-row-end: 3;
  gap: 0.4rem;
  max-height: 100%;
  height: 100%;
  overflow: auto;
`;
const ChoiceColumn2 = styled.div`
  display: flex;
  flex-direction: column;
  grid-column-start: 2;
  grid-column-end: 3;
  grid-row-start: 2;
  grid-row-end: 3;
  gap: 0.4rem;
  max-height: 100%;
  height: 100%;
  overflow: auto;
`;
const ChoiceColumn3 = styled.div`
  display: flex;
  flex-direction: column;
  grid-column-start: 3;
  grid-column-end: 4;
  grid-row-start: 2;
  grid-row-end: 3;
  gap: 0.4rem;
  max-height: 100%;
  height: 100%;
  overflow: auto;
`;

type NextClassLevelLocal = NextClassLevel & {
  selected: boolean;
};

type ChoiceGroupContainerPropsType = {
  selected?: boolean;
  customStyles?: ReturnType<typeof css>;
};

const border = {
  selected: css`
    border: 2px solid var(--color-button-primary);
    background-color: rgba(var(--color-primary-background-rgb), 0.05);
  `,
  unselected: css`
    border: 2px solid var(--color-border);
    background-color: rgba(var(--color-secondary-background-rgb), 0.05);
  `,
};

const ChoiceGroupContainer = styled.div<ChoiceGroupContainerPropsType>`
  display: flex;
  flex-direction: row;
  /* Box */
  ${(props) => (props.selected ? border.selected : border.unselected)}
  transition:
    border 0s;
  cursor: pointer;

  /* overflow: hidden; */
  font-size: 1.4rem;

  /* Border radius */
  border-radius: var(--border-radius-md);
  padding: 0.5rem 1rem;
  width: auto;
  height: auto;

  /* Custom styles */
  ${(props) => props.customStyles}
`;

const ChoiceGroupName = styled.span`
  flex-grow: 1;
`;

const EffectPowerSelector = styled(Box)`
  display: flex;
  flex-direction: row;
`;
const EffectPowerName = styled.span`
  flex-grow: 1;
`;

function EffectPowerOption({
  effectOrPower,
  setSelectedEffectPowerId,
}: {
  effectOrPower: Effect | Power | Resource;
  setSelectedEffectPowerId: (arg: number) => void;
}) {
  return (
    <EffectPowerSelector
      key={effectOrPower.id}
      onMouseEnter={() => setSelectedEffectPowerId(effectOrPower.id)}
    >
      <EffectPowerName>{effectOrPower.name}</EffectPowerName>
    </EffectPowerSelector>
  );
}

const CategoryBox = styled.div`
  background-color: rgba(var(--color-secondary-background-rgb), 0.05);
  border: 1px solid var(--color-border);
  padding: 1rem 1rem;
  width: auto;
  height: 100%;
  border-radius: var(--border-radius-lg);
`;
