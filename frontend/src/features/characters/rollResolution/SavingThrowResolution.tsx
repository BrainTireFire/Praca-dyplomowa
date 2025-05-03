import { useEffect, useReducer, useRef } from "react";
import { ability } from "../../effects/abilities";
import Spinner from "../../../ui/interactive/Spinner";
import Heading from "../../../ui/text/Heading";
import { ReusableTable } from "../../../ui/containers/ReusableTable2";
import ButtonGroup from "../../../ui/interactive/ButtonGroup";
import Button from "../../../ui/interactive/Button";
import styled from "styled-components";
import { initialState, RollConditionalEffectsReducer } from "./RollConditionalEffectsReducer";
import { useGetCharactersSavingThrowConditionalEffects } from "../hooks/useGetCharactersSavingThrowConditionalEffects";
import { useSelectCharactersSavingThrowConditionalEffects } from "../hooks/useSelectCharactersSavingThrowConditionalEffects";

export function SavingThrowRollResolution({
  characterId,
  ability
}: {
  characterId: number
  ability: ability
}) {


  const { isLoading: isLoadingEffects, conditionalEffects } = useGetCharactersSavingThrowConditionalEffects(
    characterId,
    ability
  );

  const [state, dispatch] = useReducer(
    RollConditionalEffectsReducer,
    initialState
  );

  const hasEffectRun = useRef(false);
  useEffect(() => {
    if (!!conditionalEffects) {
      hasEffectRun.current = true;
      dispatch({
        type: "SYNC_CONDITIONAL_EFFECTS",
        payload: { data: conditionalEffects },
      });
    }
  }, [conditionalEffects]);

  const { isPending: isPendingRoll, selectSavingThrowRollConditionalEffects } = useSelectCharactersSavingThrowConditionalEffects(
    () => {},
    characterId,
    ability
  );

  if (isLoadingEffects || !hasEffectRun || isPendingRoll) {
    return <Spinner></Spinner>;
  }

  return (
    <Container>
      <Heading as="h1">Select effects affecting this {ability} Saving Throw roll</Heading>

                <ContainerEffects>
                  <TabsContainer1>
                    <TableContainer>
                      <ReusableTable
                        mainHeader={`Conditional effects`}
                        tableRowsColomns={{
                          Name: "name",
                          Description: "description",
                        }}
                        data={state.conditionalEffects.map(
                          (effect, index: number) => {
                            return {
                              id: index,
                              name: conditionalEffects?.find(
                                (x) => x.effectId === effect.effectId
                              )?.effectName,
                              description:
                                conditionalEffects?.find(
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
                  </TabsContainer1>
                </ContainerEffects>
      <ButtonGroup>
        <Button onClick={() => selectSavingThrowRollConditionalEffects(state)}>Resolve</Button>
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
  height: 100%;
  overflow-y: hidden;
`;


const TableContainer = styled.div`
  overflow-y: hidden;
  height: 100%;
`;
