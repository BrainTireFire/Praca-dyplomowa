import { useEffect, useReducer, useRef } from "react";
import { ability } from "../../effects/abilities";
import { useGetCharactersAbilityConditionalEffects } from "../hooks/useGetCharactersAbilityConditionalEffects";
import { useSelectCharactersAbilityConditionalEffects } from "../hooks/useSelectCharactersAbilityConditionalEffects";
import Spinner from "../../../ui/interactive/Spinner";
import Heading from "../../../ui/text/Heading";
import { ReusableTable } from "../../../ui/containers/ReusableTable2";
import ButtonGroup from "../../../ui/interactive/ButtonGroup";
import Button from "../../../ui/interactive/Button";
import styled from "styled-components";
import { initialState, RollConditionalEffectsReducer } from "./RollConditionalEffectsReducer";

export function AbilityRollResolution({
  characterId,
  ability
}: {
  characterId: number
  ability: ability
}) {


  const { isLoading: isLoadingEffects, conditionalEffects } = useGetCharactersAbilityConditionalEffects(
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

  const { isPending: isPendingRoll, selectAbilityRollConditionalEffects } = useSelectCharactersAbilityConditionalEffects(
    () => {},
    characterId,
    ability
  );

  if (isLoadingEffects || !hasEffectRun || isPendingRoll) {
    return <Spinner></Spinner>;
  }

  return (
    <Container>
      <Heading as="h1">Select effects affecting this {ability} roll</Heading>

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
        <Button onClick={() => selectAbilityRollConditionalEffects(state)}>Resolve</Button>
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
