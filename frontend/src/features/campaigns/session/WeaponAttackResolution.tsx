import { useParams } from "react-router-dom";
import { ControlState } from "./SessionLayout";
import { useContext, useEffect, useReducer } from "react";
import { ControlledCharacterContext } from "./context/ControlledCharacterContext";
import TabList from "../../../ui/containers/tabs/TabList";
import Spinner from "../../../ui/interactive/Spinner";
import TabItem from "../../../ui/containers/tabs/TabItem";
import styled, { css } from "styled-components";
import {
  ApprovedConditionalEffectsDto,
  ConditionalEffectDto,
  DamageValueDto,
} from "../../../services/apiEncounter";
import Button from "../../../ui/interactive/Button";
import Heading from "../../../ui/text/Heading";
import useMakeAttackRoll from "../hooks/useMakeAttackRoll";
import { useWeaponDamageAndPowersOnHit } from "../hooks/useWeaponDamageAndPowersOnHit";
import { ReusableTable } from "../../../ui/containers/ReusableTable2";
import { useConditionalEffects } from "../hooks/useConditionalEffects";
import ConditionalEffectsReducer, {
  initialData,
} from "./ConditionalEffectsReducer";
import { DiceSetString } from "../../../models/diceset";
import ButtonGroup from "../../../ui/interactive/ButtonGroup";
import useApplyWeaponHitDamage from "../hooks/useApplyWeaponHitDamage";

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

export function WeaponAttackResolution({
  controlState,
}: {
  controlState: ControlState;
}) {
  const { groupName } = useParams<{ groupName: string }>();
  const [controlledCharacterId] = useContext(ControlledCharacterContext);
  const { isLoading: isLoadingWeaponDamage, weaponAttackData } =
    useWeaponDamageAndPowersOnHit(
      Number(groupName),
      controlledCharacterId,
      controlState.weaponAttackSelected?.weaponId!
    );

  const {
    isLoading: isLoadingConditionalEffects,
    conditionalEffects: conditionalEffectsApi,
  } = useConditionalEffects(
    Number(groupName),
    controlledCharacterId,
    controlState.weaponAttackRollOverlayData?.targetId!
  );

  const [conditionalEffects, dispatch] = useReducer(
    ConditionalEffectsReducer,
    initialData
  );

  useEffect(() => {
    if (!!conditionalEffectsApi) {
      dispatch({ type: "SET_STATE", payload: conditionalEffectsApi });
    }
  }, [conditionalEffectsApi]);

  const { isPending: isPendingAttackRoll, makeAttackRoll } = useMakeAttackRoll(
    Number(groupName),
    controlledCharacterId,
    controlState.weaponAttackRollOverlayData?.targetId!,
    controlState.weaponAttackSelected?.weaponId!,
    controlState.weaponAttackSelected?.isRanged!,
    () => {}
  );

  const { isPending: isPendingDamageApplication, applyWeaponHitEffects } =
    useApplyWeaponHitDamage(
      Number(groupName),
      controlledCharacterId,
      controlState.weaponAttackRollOverlayData?.targetId!,
      controlState.weaponAttackSelected?.weaponId!,
      controlState.weaponAttackSelected?.isRanged!,
      () => {}
    );

  if (
    isLoadingConditionalEffects ||
    isLoadingWeaponDamage ||
    isPendingAttackRoll ||
    isPendingDamageApplication
  ) {
    return <Spinner></Spinner>;
  }
  console.log(weaponAttackData);

  const resultPayload: ApprovedConditionalEffectsDto = {
    CasterConditionalEffects: conditionalEffects.casterConditionalEffects
      .filter((x) => x.selected)
      .map((x) => x.effectId),
    TargetConditionalEffects: conditionalEffects.targetConditionalEffects
      .filter((x) => x.selected)
      .map((x) => x.effectId),
  };

  return (
    <Container>
      <Heading as="h1">Select element of the attack</Heading>
      <TabList activeTabIndex={0}>
        {[
          <TabItem key={"weaponData"} label={"Base attack"}>
            <ContainerEffects>
              <TabsContainer>
                <TableContainer>
                  <ReusableTable
                    mainHeader={`Conditional effects for attacker`}
                    tableRowsColomns={{
                      Name: "name",
                      Description: "description",
                    }}
                    data={conditionalEffects!.casterConditionalEffects.map(
                      (effect: ConditionalEffectDto, index: number) => {
                        return {
                          id: index,
                          name: effect.effectName,
                          description: effect.effectDescription,
                          selected: effect.selected,
                          itemId: effect.effectId,
                        };
                      }
                    )}
                    isSelectable={false}
                    isMultiSelect={true}
                    handleMultiSelectionChange={(id: number | string) => {
                      console.log(id);
                      dispatch({
                        type: "TOGGLE_CASTER_EFFECT",
                        payload: { effectId: Number(id) },
                      });
                    }}
                    // customTableContainer={css`
                    //   height: 100%;
                    // `}
                  ></ReusableTable>
                </TableContainer>
                <TableContainer>
                  <ReusableTable
                    mainHeader={`Conditional effects for target`}
                    tableRowsColomns={{
                      Name: "name",
                      Description: "description",
                    }}
                    data={conditionalEffects.targetConditionalEffects.map(
                      (effect: ConditionalEffectDto, index: number) => {
                        return {
                          id: index,
                          name: effect.effectName,
                          description: effect.effectDescription,
                          selected: effect.selected,
                          itemId: effect.effectId,
                        };
                      }
                    )}
                    isSelectable={false}
                    isMultiSelect={true}
                    handleMultiSelectionChange={(id: number | string) =>
                      dispatch({
                        type: "TOGGLE_TARGET_EFFECT",
                        payload: {
                          effectId: Number(id),
                        },
                      })
                    }
                    customTableContainer={css`
                      height: 100%;
                    `}
                  ></ReusableTable>
                </TableContainer>
              </TabsContainer>
              <TableContainer>
                <ReusableTable
                  mainHeader={`Expected applied damage`}
                  tableRowsColomns={{
                    Type: "damageType",
                    Value: "damageValue",
                    Source: "source",
                  }}
                  data={weaponAttackData!.damageValues!.map(
                    (damage: DamageValueDto, index: number) => {
                      return {
                        id: index,
                        damageType: damage.damageType,
                        damageValue: DiceSetString(damage.damageValue),
                        source: damage.damageSource,
                      };
                    }
                  )}
                  isSelectable={false}
                  isMultiSelect={false}
                  // customTableContainer={css`
                  //   height: 100%;
                  // `}
                ></ReusableTable>
              </TableContainer>
              <ButtonGroup>
                <Button onClick={() => makeAttackRoll(resultPayload)}>
                  Make attack roll
                </Button>
                <Button
                  onClick={() =>
                    applyWeaponHitEffects({
                      approvedConditionalEffects: resultPayload,
                      isCritical: false,
                    })
                  }
                >
                  Apply damage
                </Button>
              </ButtonGroup>
            </ContainerEffects>
          </TabItem>,
          ...weaponAttackData!.powersOnHit.map((x) => (
            <TabItem key={x.powerId} label={x.powerName}>
              <div>Test power</div>
            </TabItem>
          )),
        ]}
      </TabList>
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
