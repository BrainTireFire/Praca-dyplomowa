import { useParams } from "react-router-dom";
import { ControlState } from "./SessionLayout";
import { useContext, useEffect, useReducer } from "react";
import { ControlledCharacterContext } from "./context/ControlledCharacterContext";
import TabList from "../../../ui/containers/tabs/TabList";
import Spinner from "../../../ui/interactive/Spinner";
import TabItem from "../../../ui/containers/tabs/TabItem";
import styled, { css } from "styled-components";
import { DamageValueDto } from "../../../services/apiEncounter";
import Button from "../../../ui/interactive/Button";
import Heading from "../../../ui/text/Heading";
import { ReusableTable } from "../../../ui/containers/ReusableTable2";

import { DiceSetString } from "../../../models/diceset";
import ButtonGroup from "../../../ui/interactive/ButtonGroup";
import { useGetWeaponAttackData } from "../hooks/useGetWeaponAttackData";
import useMakeWeaponAttack from "../hooks/useMakeWeaponAttack";
import { useGetPowerCastData } from "../hooks/useGetPowerCastData";
import {
  PowerCastConditionalEffectsReducer,
  initialState,
} from "./PowerCastConditionalEffectsReducer";

// function mapToTargetsConditionalEffects(
//   targets: Record<number, TargetDto>
// ): Record<number, number[]> {
//   return Object.fromEntries(
//     Object.entries(targets).map(([targetId, target]) => [
//       targetId,
//       target.targetConditionalEffects
//         .filter((effect) => effect.selected)
//         .map((effect) => effect.effectId),
//     ])
//   );
// }

export function PowerCastResolution({
  controlState,
}: {
  controlState: ControlState;
}) {
  const { groupName } = useParams<{ groupName: string }>();
  const [controlledCharacterId] = useContext(ControlledCharacterContext);
  const { isLoading: isLoadingWeaponDamage, powerCastData } =
    useGetPowerCastData(
      Number(groupName),
      controlledCharacterId,
      controlState.powerSelected?.powerId!,
      controlState.powerTargets
    );

  const [state, dispatch] = useReducer(
    PowerCastConditionalEffectsReducer,
    initialState
  );

  useEffect(() => {
    if (!!powerCastData) {
      dispatch({
        type: "SYNC_CONDITIONAL_EFFECTS",
        payload: { data: powerCastData },
      });
    }
  }, [powerCastData]);

  // const { isPending: isPendingAttack, makeWeaponAttack } = useMakeWeaponAttack(
  //   Number(groupName),
  //   controlledCharacterId,
  //   controlState.weaponAttackRollOverlayData?.targetId!,
  //   controlState.weaponAttackSelected?.weaponId!,
  //   controlState.weaponAttackSelected?.isRanged!,
  //   () => {}
  // );

  if (isLoadingWeaponDamage) {
    return <Spinner></Spinner>;
  }
  console.log(powerCastData);

  return (
    <Container>
      <Heading as="h1">Select element of the attack</Heading>

      <ButtonGroup>
        <Button onClick={() => {}}>Make attack roll</Button>
      </ButtonGroup>
    </Container>
  );
}

const Container = styled.div`
  display: flex;
  flex-direction: column;
  height: 90vh;
  width: 80vw;
  overflow-y: hidden;
`;
const ContainerEffects = styled.div`
  display: flex;
  flex-direction: column;
  height: 100%;
  overflow-y: hidden;
`;

const TabsContainer = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr;
  flex: 1 1 auto;
  overflow-y: hidden;
`;

const TableContainer = styled.div`
  overflow-y: hidden;
  height: 100%;
`;
