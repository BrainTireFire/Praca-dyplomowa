import styled from "styled-components";
import Box from "../../ui/containers/Box";
import { Choice, EffectLocal, PowerLocal } from "./SelectFromChoiceGroupScreen";

const EffectPowerSelector = styled(Box)`
  display: flex;
  flex-direction: row;
`;
const EffectPowerName = styled.span`
  flex-grow: 1;
`;

export default function EffectPowerOption({
  choiceGroupId,
  effectOrPower,
  setSelectedEffectPowerId,
  updateChoiceGroupsLocal,
}: {
  choiceGroupId: number;
  effectOrPower: EffectLocal | PowerLocal;
  setSelectedEffectPowerId: (arg: number) => void;
  updateChoiceGroupsLocal: (arg: Choice) => void;
}) {
  return (
    <EffectPowerSelector
      key={effectOrPower.id}
      onMouseEnter={() => setSelectedEffectPowerId(effectOrPower.id)}
    >
      <EffectPowerName>{effectOrPower.name}</EffectPowerName>
      <input
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
    </EffectPowerSelector>
  );
}
