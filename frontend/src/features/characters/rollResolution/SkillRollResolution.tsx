import { useEffect, useReducer, useRef } from "react";
import Spinner from "../../../ui/interactive/Spinner";
import Heading from "../../../ui/text/Heading";
import { ReusableTable } from "../../../ui/containers/ReusableTable2";
import ButtonGroup from "../../../ui/interactive/ButtonGroup";
import Button from "../../../ui/interactive/Button";
import styled from "styled-components";
import {
  initialState,
  RollConditionalEffectsReducer,
} from "./RollConditionalEffectsReducer";
import { skill } from "../../effects/skills";
import { useGetCharactersSkillConditionalEffects } from "../hooks/useGetCharactersSkillConditionalEffects";
import { useSelectCharactersSkillConditionalEffects } from "../hooks/useSelectCharactersSkillConditionalEffects";

export function SkillRollResolution({
  characterId,
  skill,
}: {
  characterId: number;
  skill: skill;
}) {
  const { isLoading: isLoadingEffects, conditionalEffects } =
    useGetCharactersSkillConditionalEffects(characterId, skill);

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

  const { isPending: isPendingRoll, selectSkillRollConditionalEffects } =
    useSelectCharactersSkillConditionalEffects(() => {}, characterId, skill);

  if (isLoadingEffects || !hasEffectRun || isPendingRoll) {
    return <Spinner></Spinner>;
  }

  return (
    <Container>
      <Heading as="h1">Select effects affecting this {skill} roll</Heading>

      <ContainerEffects>
        <TabsContainer1>
          <TableContainer>
            <ReusableTable
              mainHeader={`Conditional effects`}
              tableRowsColomns={{
                Name: "name",
                Description: "description",
              }}
              data={state.conditionalEffects.map((effect, index: number) => {
                return {
                  id: index,
                  name: conditionalEffects?.find(
                    (x) => x.effectId === effect.effectId
                  )?.effectName,
                  description: conditionalEffects?.find(
                    (x) => x.effectId === effect.effectId
                  )?.effectDescription,
                  selected: effect.selected,
                  itemId: effect.effectId,
                };
              })}
              isSelectable={false}
              isMultiSelect={true}
              handleMultiSelectionChange={(id: number | string) => {
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
        <Button onClick={() => selectSkillRollConditionalEffects(state)}>
          Resolve
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
