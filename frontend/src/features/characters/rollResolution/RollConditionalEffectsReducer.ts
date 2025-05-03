import { Reducer } from "react";
import { ConditionalEffectDto } from "../../models/DTOs/ConditionalEffectDto";

export type StateType = {
  conditionalEffects: ConditionalEffectSelection[];
};

export type ConditionalEffectSelection = {
  effectId: number;
  selected: boolean;
};

export type Action =
  | {
      type: "TOGGLE_CASTER_CONDITIONAL_EFFECT";
      payload: { effectId: number };
    }
  | {
      type: "SYNC_CONDITIONAL_EFFECTS";
      payload: { data: ConditionalEffectDto[] };
    };

export const RollConditionalEffectsReducer: Reducer<StateType, Action> = (
  state,
  action
) => {
  switch (action.type) {
    case "TOGGLE_CASTER_CONDITIONAL_EFFECT": {
      const { effectId } = action.payload;
      return {
        ...state,
        conditionalEffects: 
          state.conditionalEffects.map((effect) =>
              effect.effectId === effectId
                ? { ...effect, selected: !effect.selected }
                : effect
            ),
      };
    }

    case "SYNC_CONDITIONAL_EFFECTS": {
      const { data } = action.payload;

      const syncEffects = (
        existingEffects: ConditionalEffectSelection[],
        newEffects: ConditionalEffectDto[]
      ): ConditionalEffectSelection[] => {
        const newEffectIds = new Set(newEffects.map((e) => e.effectId));
        const updatedEffects = existingEffects.filter((e) =>
          newEffectIds.has(e.effectId)
        );

        newEffects.forEach((effect) => {
          if (!existingEffects.find((e) => e.effectId === effect.effectId)) {
            updatedEffects.push({ effectId: effect.effectId, selected: false });
          }
        });

        return updatedEffects;
      };

      return {
        ...state,
        conditionalEffects: syncEffects(
            state.conditionalEffects,
            data
          ),
      };
    }

    default:
      return state;
  }
};

export const initialState: StateType = {
  conditionalEffects: [],
};
