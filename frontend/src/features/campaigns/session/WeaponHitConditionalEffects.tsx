import { useParams } from "react-router-dom";
import { useConditionalEffectsForAttackRoll } from "../hooks/useConditionalEffectsForAttackRoll";
import { ControlState } from "./SessionLayout";
import { useContext, useEffect, useReducer, useState } from "react";
import { ControlledCharacterContext } from "./context/ControlledCharacterContext";
import TabList from "../../../ui/containers/tabs/TabList";
import Spinner from "../../../ui/interactive/Spinner";
import TabItem from "../../../ui/containers/tabs/TabItem";
import styled, { css } from "styled-components";
import { ReusableTable } from "../../../ui/containers/ReusableTable2";
import {
  ConditionalEffectDto,
  ConditionalEffectsDtos,
  TargetDto,
  WeaponAttackConditionalEffectsDto,
} from "../../../services/apiEncounter";
import weaponAttackConditionalEffectsReducer, {
  initialData,
} from "./WeaponAttackRollConditionalEffectsReducer";
import Button from "../../../ui/interactive/Button";
import Heading from "../../../ui/text/Heading";
import useMakeAttackRoll from "../hooks/useMakeAttackRoll";
import { useConditionalEffectsForWeaponHit } from "../hooks/useConditionalEffectsForWeaponHit";

function mapToTargetsConditionalEffects(
  targets: Record<number, TargetDto>
): Record<number, number[]> {
  return Object.fromEntries(
    Object.entries(targets).map(([targetId, target]) => [
      targetId,
      target.targetConditionalEffects
        .filter((effect) => effect.selected)
        .map((effect) => effect.effectId),
    ])
  );
}

export function WeaponHitConditionalEffects({
  controlState,
}: {
  controlState: ControlState;
}) {
  const { groupName } = useParams<{ groupName: string }>();
  const [controlledCharacterId] = useContext(ControlledCharacterContext);
  const {
    isLoading: isLoadingConditionalEffects,
    conditionalEffects: conditionalEffectApi,
  } = useConditionalEffectsForWeaponHit(
    Number(groupName),
    controlledCharacterId,
    controlState.weaponAttackRollOverlayData?.targetId!,
    controlState.weaponAttackSelected?.weaponId!
  );
  console.log(conditionalEffectApi);

  const [conditionalEffects, dispatch] = useReducer(
    weaponAttackConditionalEffectsReducer,
    initialData
  );

  useEffect(() => {
    if (!!conditionalEffectApi) {
      dispatch({ type: "SET_STATE", payload: conditionalEffectApi });
    }
  }, [conditionalEffectApi]);

  const { isPending: isPendingAttackRoll, makeAttackRoll } = useMakeAttackRoll(
    Number(groupName),
    controlledCharacterId,
    controlState.weaponAttackRollOverlayData?.targetId!,
    controlState.weaponAttackSelected?.weaponId!,
    controlState.weaponAttackSelected?.isRanged!,
    () => {}
  );

  const resultPayload: ConditionalEffectsDtos = {
    CasterConditionalEffects: conditionalEffects.casterConditionalEffects
      .filter((x) => x.selected)
      .map((x) => x.effectId),
    TargetsConditionalEffects: mapToTargetsConditionalEffects(
      conditionalEffects.targetsConditionalEffects
    ),
  };

  if (isLoadingConditionalEffects || isPendingAttackRoll) {
    return <Spinner></Spinner>;
  }

  return (
    <Container>
      <Heading as="h1">
        Select effects which apply for this weapon hit analysis
      </Heading>
      <TabsContainer>
        <TabList activeTabIndex={0}>
          <TabItem label={"Attacker"} key={"Attacker"}>
            <TableContainer>
              <ReusableTable
                mainHeader={`Conditional effects for attacker`}
                tableRowsColomns={{ Name: "name", Description: "description" }}
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
          </TabItem>
        </TabList>
        <TabList activeTabIndex={0}>
          {Object.entries(conditionalEffects!.targetsConditionalEffects).map(
            ([key, target]) => (
              <TabItem key={key} label={target.targetName}>
                <TableContainer>
                  <ReusableTable
                    mainHeader={`Conditional effects for ${target.targetName}`}
                    tableRowsColomns={{
                      Name: "name",
                      Description: "description",
                    }}
                    data={target!.targetConditionalEffects.map(
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
                          targetId: Number(key),
                        },
                      })
                    }
                    customTableContainer={css`
                      height: 100%;
                    `}
                  ></ReusableTable>
                </TableContainer>
              </TabItem>
            )
          )}
        </TabList>
      </TabsContainer>
      <Button onClick={() => makeAttackRoll(resultPayload)}>Confirm</Button>
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
