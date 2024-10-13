import styled, { css } from "styled-components";
import Box from "../../ui/containers/Box";
import Spinner from "../../ui/interactive/Spinner";
import { useChoiceGroups } from "./hooks/useChoiceGroups";
import { useEffect, useState } from "react";
import EffectPowerToChoose from "./ElementToChoose";
import {
  ChoiceGroup,
  Effect,
  Power,
  Resource,
} from "../../services/apiCharacters";
import EffectPowerOption from "./EffectPowerOption";
import VerticalLine from "../../ui/separators/VerticalLine";
import Button from "../../ui/interactive/Button";
import { useChoiceGroupUsage } from "./hooks/useChoiceGroupsUsage";
import toast from "react-hot-toast";
import ElementToChoose from "./ElementToChoose";

const MainGrid = styled(Box)`
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  gap: 0.4rem;
  max-height: 500px;
  height: 500px;
  max-width: 500px;
  width: 500px;
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
  grid-column-start: 2;
  grid-column-end: 3;
  gap: 0.4rem;
  max-height: 100%;
  height: 100%;
  overflow: auto;
`;
const MainGridColumn3 = styled.div`
  display: flex;
  flex-direction: column;
  grid-column-start: 3;
  grid-column-end: 4;
  gap: 0.4rem;
  max-height: 100%;
  height: 100%;
  overflow: auto;
`;

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

export type Choice = {
  choiceGroupId: number;
  elementId: number;
  elementType: "effect" | "power" | "resource";
};

const ChoiceGroupName = styled.span`
  flex-grow: 1;
`;

function SelectFromChoiceGroupScreen({
  characterId,
  onCloseModal,
}: {
  characterId: number;
  onCloseModal: () => void;
}) {
  //query
  const {
    isLoading: isLoadingChoiceGroups,
    choiceGroups,
    error: errorChoiceGroups,
  } = useChoiceGroups(characterId);

  console.log(choiceGroups);

  const [selectedChoiceGroupId, setSelectedChoiceGroupId] = useState<
    number | null
  >(null);
  const [selectedEffectId, setSelectedEffectId] = useState<number | null>(null);

  // State to store transformed data
  const [choiceGroupsLocal, setChoiceGroupsLocal] = useState<
    ChoiceGroupLocal[]
  >([]);

  const updateChoiceGroupsLocal = (newChoice: Choice) => {
    setChoiceGroupsLocal((prevState) =>
      prevState.map((group) => {
        if (group.id === newChoice.choiceGroupId) {
          // Update the EffectLocal or PowerLocal based on elementType
          if (newChoice.elementType === "effect") {
            return {
              ...group,
              effects: group.effects.map((effect) =>
                effect.id === newChoice.elementId
                  ? { ...effect, selected: !effect.selected }
                  : effect
              ),
            };
          } else if (newChoice.elementType === "power") {
            return {
              ...group,
              powers: group.powers.map((power) =>
                power.id === newChoice.elementId
                  ? { ...power, selected: !power.selected }
                  : power
              ),
            };
          } else if (newChoice.elementType === "resource") {
            return {
              ...group,
              resources: group.resources.map((resource) =>
                resource.id === newChoice.elementId
                  ? { ...resource, selected: !resource.selected }
                  : resource
              ),
            };
          }
        }
        return group; // Return unchanged group if no match
      })
    );
  };

  // Function to transform fetched data
  const mapChoiceGroupsToLocal = (
    choiceGroups: ChoiceGroup[]
  ): ChoiceGroupLocal[] => {
    return choiceGroups.map((group) => ({
      id: group.id,
      name: group.name,
      numberToChoose: group.numberToChoose,
      effects: group.effects.map((effect) => ({
        ...effect,
        selected: false, // Default selected value
        selectionType: "effect",
      })),
      powers: group.powers.map((power) => ({
        ...power,
        selected: false, // Default selected value
        selectionType: "power",
      })),
      resources: group.resources.map((resource) => ({
        ...resource,
        selected: false, // Default selected value
        selectionType: "resource",
      })),
    }));
  };

  // Update local state when data is fetched
  useEffect(() => {
    if (choiceGroups) {
      const transformedData = mapChoiceGroupsToLocal(choiceGroups);
      setChoiceGroupsLocal(transformedData);
    }
  }, [choiceGroups]);

  //mutation
  const { generateChoiceGroupUsage, isPending } = useChoiceGroupUsage(() => {
    toast.success("Character developed!");
    onCloseModal();
    return;
  });

  if (errorChoiceGroups) {
    return "Error";
  }
  if (isLoadingChoiceGroups || isPending) {
    return <Spinner />;
  }

  var disableButton =
    choiceGroupsLocal.filter(
      (cg) =>
        cg.effects.filter((e) => e.selected).length +
          cg.powers.filter((e) => e.selected).length +
          cg.resources.filter((e) => e.selected).length ===
          cg.numberToChoose ||
        cg.effects.filter((e) => e.selected).length +
          cg.powers.filter((e) => e.selected).length +
          cg.resources.filter((e) => e.selected).length ===
          0
    ).length !== choiceGroupsLocal.length;
  console.log(disableButton);

  const generatePayload = () => {
    return choiceGroupsLocal.map((cg) => {
      return {
        id: cg.id,
        effectIds: cg.effects.filter((e) => e.selected).map((e) => e.id),
        powerIds: cg.powers.filter((p) => p.selected).map((p) => p.id),
        resourceIds: cg.resources.filter((r) => r.selected).map((r) => r.id),
      };
    });
  };

  return (
    <>
      <MainGrid>
        <MainGridColumn1>
          {choiceGroupsLocal?.map((choiceGroup) => (
            <ChoiceGroupContainer
              key={choiceGroup.id}
              selected={choiceGroup.id === selectedChoiceGroupId}
              onClick={() => setSelectedChoiceGroupId(choiceGroup.id)}
            >
              <ChoiceGroupName>{choiceGroup.name}</ChoiceGroupName>
              <VerticalLine />
              <span>
                {choiceGroup.effects.filter((e) => e.selected).length +
                  choiceGroup.powers.filter((p) => p.selected).length}
                /{choiceGroup.numberToChoose} selected
              </span>
            </ChoiceGroupContainer>
          ))}
        </MainGridColumn1>
        <MainGridColumn2>
          {selectedChoiceGroupId &&
            choiceGroupsLocal
              ?.filter((cg) => cg.id === selectedChoiceGroupId)[0]
              .effects.map((effect) => (
                <EffectPowerOption
                  choiceGroupId={selectedChoiceGroupId}
                  effectOrPower={effect}
                  setSelectedEffectPowerId={setSelectedEffectId}
                  updateChoiceGroupsLocal={updateChoiceGroupsLocal}
                ></EffectPowerOption>
              ))}
        </MainGridColumn2>
        <MainGridColumn3>
          {selectedEffectId &&
            choiceGroupsLocal
              ?.filter((cg) => cg.id === selectedChoiceGroupId)[0]
              .effects.filter((effect) => effect.id === selectedEffectId)
              .map((effect) => (
                <ElementToChoose
                  key={effect.id}
                  element={effect}
                ></ElementToChoose>
              ))}
        </MainGridColumn3>
      </MainGrid>
      <Button
        disabled={disableButton}
        onClick={() => {
          var choiceGroupUsage = generatePayload();
          generateChoiceGroupUsage({ characterId, choiceGroupUsage });
        }}
      >
        {disableButton ? "Invalid choice number" : "Confirm choices"}
      </Button>
    </>
  );
}

export default SelectFromChoiceGroupScreen;

type ChoiceGroupLocal = {
  id: number;
  name: string;
  numberToChoose: number;
  effects: EffectLocal[];
  powers: PowerLocal[];
  resources: ResourceLocal[];
};

export type EffectLocal = Effect & {
  selected: boolean;
  selectionType: "effect";
};

export type PowerLocal = Power & {
  selected: boolean;
  selectionType: "power";
};

export type ResourceLocal = Resource & {
  selected: boolean;
  selectionType: "resource";
};
