import { Reducer } from "react";
import {
  ConditionalEffectDto,
  PowerDataAndConditionalEffectsDto,
} from "../../../services/apiEncounter";

export type StateType = {
  spellSlotLevel: number | null;
  conditionalEffects: ConditionalEffectsSelectionSet;
};

export type ConditionalEffectSelection = {
  effectId: number;
  selected: boolean;
};

export type ConditionalEffectsSelectionSet = {
  casterConditionalEffects: ConditionalEffectSelection[];
  targetConditionalEffects: Record<number, ConditionalEffectSelection[]>;
};

export type Action =
  | {
      type: "TOGGLE_CASTER_CONDITIONAL_EFFECT";
      payload: { effectId: number };
    }
  | {
      type: "TOGGLE_TARGET_CONDITIONAL_EFFECT";
      payload: { targetId: number; effectId: number };
    }
  | {
      type: "SET_SPELL_SLOT_LEVEL";
      payload: { level: number };
    }
  | {
      type: "SYNC_CONDITIONAL_EFFECTS";
      payload: { data: PowerDataAndConditionalEffectsDto };
    };

export const PowerCastConditionalEffectsReducer: Reducer<StateType, Action> = (
  state,
  action
) => {
  switch (action.type) {
    case "TOGGLE_CASTER_CONDITIONAL_EFFECT": {
      const { effectId } = action.payload;
      return {
        ...state,
        conditionalEffects: {
          ...state.conditionalEffects,
          casterConditionalEffects:
            state.conditionalEffects.casterConditionalEffects.map((effect) =>
              effect.effectId === effectId
                ? { ...effect, selected: !effect.selected }
                : effect
            ),
        },
      };
    }

    case "TOGGLE_TARGET_CONDITIONAL_EFFECT": {
      const { targetId, effectId } = action.payload;
      return {
        ...state,
        conditionalEffects: {
          ...state.conditionalEffects,
          targetConditionalEffects: {
            ...state.conditionalEffects.targetConditionalEffects,
            [targetId]: state.conditionalEffects.targetConditionalEffects[
              targetId
            ].map((effect) =>
              effect.effectId === effectId
                ? { ...effect, selected: !effect.selected }
                : effect
            ),
          },
        },
      };
    }

    case "SET_SPELL_SLOT_LEVEL": {
      const { level } = action.payload;
      return {
        ...state,
        spellSlotLevel: level,
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

      const targetEffects = Object.entries(
        data.conditionalEffects.targetConditionalEffects
      ).reduce<Record<number, ConditionalEffectSelection[]>>(
        (acc, [targetId, effects]) => {
          acc[Number(targetId)] = syncEffects(
            state.conditionalEffects.targetConditionalEffects[
              Number(targetId)
            ] || [],
            effects
          );
          return acc;
        },
        {}
      );

      return {
        ...state,
        spellSlotLevel: null, // Reset spell slot level on sync
        conditionalEffects: {
          casterConditionalEffects: syncEffects(
            state.conditionalEffects.casterConditionalEffects,
            data.conditionalEffects.casterConditionalEffects
          ),
          targetConditionalEffects: targetEffects,
        },
      };
    }

    default:
      return state;
  }
};

export const initialState: StateType = {
  spellSlotLevel: null,
  conditionalEffects: {
    casterConditionalEffects: [],
    targetConditionalEffects: {},
  },
};
