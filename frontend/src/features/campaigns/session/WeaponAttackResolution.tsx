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
import {
  initialData,
  WeaponAttackConditionalEffectsReducer,
} from "./WeaponAttackConditionalEffectsReducer";
import useMakeWeaponAttack from "../hooks/useMakeWeaponAttack";

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
    useGetWeaponAttackData(
      Number(groupName),
      controlledCharacterId,
      controlState.weaponAttackRollOverlayData!.targetId,
      controlState.weaponAttackSelected?.weaponId!,
      controlState.weaponAttackSelected?.isRanged!
    );

  const [state, dispatch] = useReducer(
    WeaponAttackConditionalEffectsReducer,
    initialData
  );

  useEffect(() => {
    if (!!weaponAttackData) {
      dispatch({
        type: "SYNC_CONDITIONAL_EFFECTS",
        payload: { data: weaponAttackData },
      });
    }
  }, [weaponAttackData]);

  const { isPending: isPendingAttack, makeWeaponAttack } = useMakeWeaponAttack(
    Number(groupName),
    controlledCharacterId,
    controlState.weaponAttackRollOverlayData?.targetId!,
    controlState.weaponAttackSelected?.weaponId!,
    controlState.weaponAttackSelected?.isRanged!,
    () => {}
  );

  if (isLoadingWeaponDamage || isPendingAttack) {
    return <Spinner></Spinner>;
  }
  console.log(weaponAttackData);

  // const resultPayload: ApprovedConditionalEffectsDto = {
  //   CasterConditionalEffects: conditionalEffects.casterConditionalEffects
  //     .filter((x) => x.selected)
  //     .map((x) => x.effectId),
  //   TargetConditionalEffects: conditionalEffects.targetConditionalEffects
  //     .filter((x) => x.selected)
  //     .map((x) => x.effectId),
  // };

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
                    data={state.weaponAttackConditionalEffects.casterConditionalEffects.map(
                      (effect, index: number) => {
                        return {
                          id: index,
                          name: weaponAttackData?.conditionalEffects.casterConditionalEffects.find(
                            (x) => x.effectId === effect.effectId
                          )?.effectName,
                          description:
                            weaponAttackData?.conditionalEffects.casterConditionalEffects.find(
                              (x) => x.effectId === effect.effectId
                            )?.effectDescription,
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
                        type: "TOGGLE_WEAPON_ATTACK_CONDITIONAL_EFFECT",
                        payload: { effectId: Number(id), isCaster: true },
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
                    data={state.weaponAttackConditionalEffects.targetConditionalEffects.map(
                      (effect, index: number) => {
                        return {
                          id: index,
                          name: weaponAttackData?.conditionalEffects.targetConditionalEffects.find(
                            (x) => x.effectId === effect.effectId
                          )?.effectName,
                          description:
                            weaponAttackData?.conditionalEffects.targetConditionalEffects.find(
                              (x) => x.effectId === effect.effectId
                            )?.effectDescription,
                          selected: effect.selected,
                          itemId: effect.effectId,
                        };
                      }
                    )}
                    isSelectable={false}
                    isMultiSelect={true}
                    handleMultiSelectionChange={(id: number | string) =>
                      dispatch({
                        type: "TOGGLE_WEAPON_ATTACK_CONDITIONAL_EFFECT",
                        payload: { effectId: Number(id), isCaster: false },
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
                  data={
                    weaponAttackData?.weaponDamageAndPowers.damageValues!.map(
                      (damage: DamageValueDto, index: number) => {
                        return {
                          id: index,
                          damageType: damage.damageType,
                          damageValue: DiceSetString(damage.damageValue),
                          source: damage.damageSource,
                        };
                      }
                    ) ?? []
                  }
                  isSelectable={false}
                  isMultiSelect={false}
                  // customTableContainer={css`
                  //   height: 100%;
                  // `}
                ></ReusableTable>
              </TableContainer>
            </ContainerEffects>
          </TabItem>,
          ...weaponAttackData!.weaponDamageAndPowers.powersOnHit.map((x) => (
            <TabItem key={x.powerId} label={x.powerName}>
              <ContainerEffects>
                <TabsContainer>
                  <TableContainer>
                    <ReusableTable
                      mainHeader={`Conditional effects for attacker`}
                      tableRowsColomns={{
                        Name: "name",
                        Description: "description",
                      }}
                      data={(() => {
                        const power = state.powers.find(
                          (y) => y.powerId === x.powerId
                        );
                        return power
                          ? power.powerConditionalEffects.casterConditionalEffects.map(
                              (effect, index: number) => {
                                return {
                                  id: index,
                                  name: weaponAttackData?.conditionalEffects.casterConditionalEffects.find(
                                    (x) => x.effectId === effect.effectId
                                  )?.effectName,
                                  description:
                                    weaponAttackData?.conditionalEffects.casterConditionalEffects.find(
                                      (x) => x.effectId === effect.effectId
                                    )?.effectDescription,
                                  selected: effect.selected,
                                  itemId: effect.effectId,
                                };
                              }
                            )
                          : [];
                      })()}
                      isSelectable={false}
                      isMultiSelect={true}
                      handleMultiSelectionChange={(id: number | string) => {
                        console.log(id);
                        dispatch({
                          type: "TOGGLE_POWER_CONDITIONAL_EFFECT",
                          payload: {
                            powerId: x.powerId,
                            effectId: Number(id),
                            isCaster: true,
                          },
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
                      data={(() => {
                        const power = state.powers.find(
                          (y) => y.powerId === x.powerId
                        );
                        return power
                          ? power.powerConditionalEffects.targetConditionalEffects.map(
                              (effect, index: number) => {
                                return {
                                  id: index,
                                  name: weaponAttackData?.conditionalEffects.targetConditionalEffects.find(
                                    (x) => x.effectId === effect.effectId
                                  )?.effectName,
                                  description:
                                    weaponAttackData?.conditionalEffects.targetConditionalEffects.find(
                                      (x) => x.effectId === effect.effectId
                                    )?.effectDescription,
                                  selected: effect.selected,
                                  itemId: effect.effectId,
                                };
                              }
                            )
                          : [];
                      })()}
                      isSelectable={false}
                      isMultiSelect={true}
                      handleMultiSelectionChange={(id: number | string) => {
                        console.log(id);
                        dispatch({
                          type: "TOGGLE_POWER_CONDITIONAL_EFFECT",
                          payload: {
                            powerId: x.powerId,
                            effectId: Number(id),
                            isCaster: false,
                          },
                        });
                      }}
                      customTableContainer={css`
                        height: 100%;
                      `}
                    ></ReusableTable>
                  </TableContainer>
                </TabsContainer>
                <TableContainer>
                  <ReusableTable
                    mainHeader={`Effects applied by power`}
                    tableRowsColomns={{
                      Name: "name",
                      Description: "description",
                    }}
                    data={
                      x.powerEffects.map((effect, index: number) => {
                        return {
                          id: index,
                          name: effect.powerEffectName,
                          description: effect.powerEffectDescription,
                        };
                      }) ?? []
                    }
                    isSelectable={false}
                    isMultiSelect={false}
                    // customTableContainer={css`
                    //   height: 100%;
                    // `}
                  ></ReusableTable>
                </TableContainer>
              </ContainerEffects>
            </TabItem>
          )),
        ]}
      </TabList>
      <ButtonGroup>
        <Button onClick={() => makeWeaponAttack(state)}>
          Make attack roll
        </Button>
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
