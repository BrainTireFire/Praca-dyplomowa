import { useParams } from "react-router-dom";
import { ControlState } from "./SessionLayout";
import { useContext, useEffect, useReducer, useRef } from "react";
import { ControlledCharacterContext } from "./context/ControlledCharacterContext";
import TabList from "../../../ui/containers/tabs/TabList";
import Spinner from "../../../ui/interactive/Spinner";
import TabItem from "../../../ui/containers/tabs/TabItem";
import styled, { css } from "styled-components";
import { PowerEffectDto } from "../../../services/apiEncounter";
import Button from "../../../ui/interactive/Button";
import Heading from "../../../ui/text/Heading";
import { ReusableTable } from "../../../ui/containers/ReusableTable2";

import ButtonGroup from "../../../ui/interactive/ButtonGroup";
import { useGetPowerCastData } from "../hooks/useGetPowerCastData";
import {
  PowerCastConditionalEffectsReducer,
  initialState,
} from "./PowerCastConditionalEffectsReducer";
import Dropdown from "../../../ui/forms/Dropdown";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import useCastPower from "../hooks/useCastPower";

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

  let spellSlotLevel =
    controlState.powerSelected?.chosenLevel?.resourceLevel ?? null;

  const { isLoading: isLoadingPowerData, powerCastData } = useGetPowerCastData(
    Number(groupName),
    controlState.powerCastOverlayData!.sourceId,
    controlState.powerSelected?.powerId!,
    controlState.powerSelected?.chosenLevel?.powerLevel ?? null,
    spellSlotLevel,
    controlState.powerTargets
  );

  const [state, dispatch] = useReducer(
    PowerCastConditionalEffectsReducer,
    initialState
  );

  const hasEffectRun = useRef(false);
  useEffect(() => {
    if (!!powerCastData) {
      hasEffectRun.current = true;
      dispatch({
        type: "SYNC_CONDITIONAL_EFFECTS",
        payload: { data: powerCastData },
      });
    }
  }, [powerCastData]);

  const { isPending: isPendingPowerCast, castPower } = useCastPower(
    Number(groupName),
    controlState.powerCastOverlayData!.sourceId,
    controlState.powerSelected?.powerId!,
    controlState.powerSelected?.chosenLevel?.powerLevel ?? null,
    spellSlotLevel,
    () => {}
  );

  if (isLoadingPowerData || !hasEffectRun || isPendingPowerCast) {
    return <Spinner></Spinner>;
  }

  return (
    <Container>
      <Heading as="h1">Select target</Heading>
      <StyledTabListContainer>
        <TabList activeTabIndex={0}>
          {[
            ...powerCastData!.conditionalEffects.targetData.map((x) => (
              <TabItem label={x.targetName} key={x.targetId}>
                <ContainerEffects>
                  <TabsContainer1>
                    <TableContainer>
                      <ReusableTable
                        mainHeader={`Conditional effects for caster`}
                        tableRowsColomns={{
                          Name: "name",
                          Description: "description",
                        }}
                        data={state.conditionalEffects.casterConditionalEffects.map(
                          (effect, index: number) => {
                            return {
                              id: index,
                              name: powerCastData?.conditionalEffects.casterConditionalEffects.find(
                                (x) => x.effectId === effect.effectId
                              )?.effectName,
                              description:
                                powerCastData?.conditionalEffects.casterConditionalEffects.find(
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
                            type: "TOGGLE_CASTER_CONDITIONAL_EFFECT",
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
                        mainHeader={`Conditional effects for ${x.targetName}`}
                        tableRowsColomns={{
                          Name: "name",
                          Description: "description",
                        }}
                        data={
                          state.conditionalEffects.targetConditionalEffects[
                            x.targetId
                          ]?.map((effect, index: number) => {
                            return {
                              id: index,
                              name: powerCastData?.conditionalEffects.targetData
                                .find((t) => t.targetId === x.targetId)
                                ?.targetConditionalEffects.find(
                                  (t) => t.effectId === effect.effectId
                                )?.effectName,
                              description:
                                powerCastData?.conditionalEffects.targetData
                                  .find((t) => t.targetId === x.targetId)
                                  ?.targetConditionalEffects.find(
                                    (t) => t.effectId === effect.effectId
                                  )?.effectDescription,
                              selected: effect.selected,
                              itemId: effect.effectId,
                            };
                          }) ?? []
                        }
                        isSelectable={false}
                        isMultiSelect={true}
                        handleMultiSelectionChange={(id: number | string) =>
                          dispatch({
                            type: "TOGGLE_TARGET_CONDITIONAL_EFFECT",
                            payload: {
                              effectId: Number(id),
                              targetId: x.targetId,
                            },
                          })
                        }
                        customTableContainer={css`
                          height: 100%;
                        `}
                      ></ReusableTable>
                    </TableContainer>
                  </TabsContainer1>
                  <TabsContainer2>
                    <TableContainer>
                      <ReusableTable
                        mainHeader={`Power effects on hit`}
                        tableRowsColomns={{
                          Name: "name",
                          Description: "description",
                        }}
                        data={(() => {
                          console.log(powerCastData);
                          console.log(spellSlotLevel);
                          return (
                            powerCastData?.powerData.powerEffects[0]?.map(
                              (effect: PowerEffectDto, index: number) => {
                                return {
                                  id: index,
                                  name: effect.powerEffectName,
                                  description: effect.powerEffectDescription,
                                };
                              }
                            ) ?? []
                          );
                        })()}
                        isSelectable={false}
                        isMultiSelect={false}
                        // customTableContainer={css`
                        //   height: 100%;
                        // `}
                      ></ReusableTable>
                    </TableContainer>
                    <TableContainer>
                      <ReusableTable
                        mainHeader={`Power effects on miss`}
                        tableRowsColomns={{
                          Name: "name",
                          Description: "description",
                        }}
                        data={(() => {
                          console.log(powerCastData);
                          console.log(spellSlotLevel);
                          return (
                            powerCastData?.powerData.powerEffects[1]?.map(
                              (effect: PowerEffectDto, index: number) => {
                                return {
                                  id: index,
                                  name: effect.powerEffectName,
                                  description: effect.powerEffectDescription,
                                };
                              }
                            ) ?? []
                          );
                        })()}
                        isSelectable={false}
                        isMultiSelect={false}
                        // customTableContainer={css`
                        //   height: 100%;
                        // `}
                      ></ReusableTable>
                    </TableContainer>
                  </TabsContainer2>
                </ContainerEffects>
              </TabItem>
            )),
          ]}
        </TabList>
      </StyledTabListContainer>
      <ButtonGroup>
        <Button onClick={() => castPower(state)}>Resolve</Button>
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

const StyledTabListContainer = styled.div`
  flex-grow: 1;
`;

const ContainerEffects = styled.div`
  display: flex;
  flex-direction: column;
  height: 100%;
  overflow-y: hidden;
`;

const TabsContainer1 = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr;
  height: 60%;
  overflow-y: hidden;
`;
const TabsContainer2 = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr;
  height: 40%;
  overflow-y: hidden;
`;

const TableContainer = styled.div`
  overflow-y: hidden;
  height: 100%;
`;
