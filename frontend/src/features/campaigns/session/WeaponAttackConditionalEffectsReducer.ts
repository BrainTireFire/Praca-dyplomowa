import { Reducer } from "react";
import {
  ConditionalEffectDto,
  WeaponAttackData,
} from "../../../services/apiEncounter";

export type conditionalEffectType = {
  effectId: number;
  selected: boolean;
};

export type conditionalEffectSet = {
  casterConditionalEffects: conditionalEffectType[];
  targetConditionalEffects: conditionalEffectType[];
};

export type powerWithConditionalEffectSets = {
  powerId: number;
  powerConditionalEffects: conditionalEffectSet;
};

export type stateType = {
  weaponAttackConditionalEffects: conditionalEffectSet;
  powers: powerWithConditionalEffectSets[];
};

// Define the action types
export type Action =
  | {
      type: "TOGGLE_WEAPON_ATTACK_CONDITIONAL_EFFECT";
      payload: {
        effectId: number;
        isCaster: boolean; // true for caster, false for target
      };
    }
  | {
      type: "TOGGLE_POWER_CONDITIONAL_EFFECT";
      payload: {
        powerId: number;
        effectId: number;
        isCaster: boolean; // true for caster, false for target
      };
    }
  | {
      type: "SYNC_CONDITIONAL_EFFECTS";
      payload: {
        data: WeaponAttackData;
      };
    };

// Reducer function
export const WeaponAttackConditionalEffectsReducer: Reducer<
  stateType,
  Action
> = (state, action) => {
  switch (action.type) {
    case "TOGGLE_WEAPON_ATTACK_CONDITIONAL_EFFECT": {
      const { effectId, isCaster } = action.payload;
      const key = isCaster
        ? "casterConditionalEffects"
        : "targetConditionalEffects";

      return {
        ...state,
        weaponAttackConditionalEffects: {
          ...state.weaponAttackConditionalEffects,
          [key]: state.weaponAttackConditionalEffects[key].map((effect) =>
            effect.effectId === effectId
              ? { ...effect, selected: !effect.selected }
              : effect
          ),
        },
      };
    }

    case "TOGGLE_POWER_CONDITIONAL_EFFECT": {
      const { powerId, effectId, isCaster } = action.payload;
      const key = isCaster
        ? "casterConditionalEffects"
        : "targetConditionalEffects";

      return {
        ...state,
        powers: state.powers.map((power) => {
          if (power.powerId !== powerId) return power;

          return {
            ...power,
            powerConditionalEffects: {
              ...power.powerConditionalEffects,
              [key]: power.powerConditionalEffects[key].map((effect) =>
                effect.effectId === effectId
                  ? { ...effect, selected: !effect.selected }
                  : effect
              ),
            },
          };
        }),
      };
    }

    case "SYNC_CONDITIONAL_EFFECTS": {
      const { data } = action.payload;

      const syncEffects = (
        existingEffects: conditionalEffectType[],
        newEffects: ConditionalEffectDto[]
      ): conditionalEffectType[] => {
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

      const updatedPowers = data.weaponDamageAndPowers.powersOnHit.map(
        (power) => {
          const existingPower = state.powers.find(
            (p) => p.powerId === power.powerId
          );

          return {
            powerId: power.powerId,
            powerConditionalEffects: existingPower
              ? {
                  casterConditionalEffects: syncEffects(
                    existingPower.powerConditionalEffects
                      .casterConditionalEffects,
                    data.conditionalEffects.casterConditionalEffects
                  ),
                  targetConditionalEffects: syncEffects(
                    existingPower.powerConditionalEffects
                      .targetConditionalEffects,
                    data.conditionalEffects.targetConditionalEffects
                  ),
                }
              : {
                  casterConditionalEffects: syncEffects(
                    [],
                    data.conditionalEffects.casterConditionalEffects
                  ),
                  targetConditionalEffects: syncEffects(
                    [],
                    data.conditionalEffects.targetConditionalEffects
                  ),
                },
          };
        }
      );

      return {
        ...state,
        weaponAttackConditionalEffects: {
          casterConditionalEffects: syncEffects(
            state.weaponAttackConditionalEffects.casterConditionalEffects,
            data.conditionalEffects.casterConditionalEffects
          ),
          targetConditionalEffects: syncEffects(
            state.weaponAttackConditionalEffects.targetConditionalEffects,
            data.conditionalEffects.targetConditionalEffects
          ),
        },
        powers: updatedPowers,
      };
    }

    default:
      return state;
  }
};

export const initialData: stateType = {
  weaponAttackConditionalEffects: {
    casterConditionalEffects: [],
    targetConditionalEffects: [],
  },
  powers: [],
};
