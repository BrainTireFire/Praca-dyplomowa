import styled from "styled-components";
import Box from "../../ui/containers/Box";
import {
  Choice,
  EffectLocal,
  PowerLocal,
  ResourceLocal,
} from "./SelectFromChoiceGroupScreen";
import Input from "../../ui/forms/Input";
import FormRowVertical from "../../ui/forms/FormRowVertical";

const EffectPowerSelector = styled(Box)`
  display: grid;
  grid-template-columns: 66% 34%;
  grid-template-rows: auto auto;
`;
const EffectPowerName = styled.span`
  grid-column: 1;
  grid-row: 1;
`;
const CheckboxCell = styled.div`
  grid-column: 2;
  grid-row: 1;
`;
const InfoCell = styled.span`
  grid-column-start: 1;
  grid-column-end: 3;
  grid-row: 2;
  color: red;
`;

export default function EffectPowerOption({
  choiceGroupId,
  effectOrPower,
  setSelectedEffectPowerId,
  updateChoiceGroupsLocal,
}: {
  choiceGroupId: number;
  effectOrPower: EffectLocal | PowerLocal | ResourceLocal;
  setSelectedEffectPowerId: (arg: number) => void;
  updateChoiceGroupsLocal: (arg: Choice) => void;
}) {
  const ExpertiseWithoutProficiency =
    (effectOrPower as EffectLocal).notAllowed === "ExpertiseWithoutProficiency";
  return (
    <EffectPowerSelector
      key={effectOrPower.id}
      onMouseEnter={() => setSelectedEffectPowerId(effectOrPower.id)}
    >
      <EffectPowerName>{effectOrPower.name}</EffectPowerName>
      <CheckboxCell>
        <Input
          disabled={ExpertiseWithoutProficiency}
          checked={effectOrPower.selected}
          type="checkbox"
          onChange={() =>
            updateChoiceGroupsLocal({
              choiceGroupId: choiceGroupId,
              elementId: effectOrPower.id,
              elementType: effectOrPower.selectionType,
            })
          }
        />
      </CheckboxCell>
      {ExpertiseWithoutProficiency && (
        <InfoCell>You must have proficiency first</InfoCell>
      )}
    </EffectPowerSelector>
  );
}
