import {
  ConditionalEffectsDto,
  WeaponAttackConditionalEffectsDto,
} from "../../../services/apiEncounter";

// Define action types
const TOGGLE_CASTER_EFFECT = "TOGGLE_CASTER_EFFECT";
const TOGGLE_TARGET_EFFECT = "TOGGLE_TARGET_EFFECT";
const SET_STATE = "SET_STATE";

// Define action interfaces
export interface ToggleCasterEffectAction {
  type: typeof TOGGLE_CASTER_EFFECT;
  payload: {
    effectId: number;
  };
}

export interface ToggleTargetEffectAction {
  type: typeof TOGGLE_TARGET_EFFECT;
  payload: {
    effectId: number;
  };
}

export interface SetStateAction {
  type: typeof SET_STATE;
  payload: ConditionalEffectsDto;
}

// Define the reducer
function ConditionalEffectsReducer(
  state: ConditionalEffectsDto,
  action: ToggleCasterEffectAction | ToggleTargetEffectAction | SetStateAction
) {
  switch (action.type) {
    case TOGGLE_CASTER_EFFECT: {
      const updatedCasterEffects = state.casterConditionalEffects.map(
        (effect) => {
          if (effect.effectId === action.payload.effectId) {
            return { ...effect, selected: !effect.selected };
          }
          return effect;
        }
      );

      return { ...state, casterConditionalEffects: updatedCasterEffects };
    }

    case TOGGLE_TARGET_EFFECT: {
      const updatedTargetEffects = state.targetConditionalEffects.map(
        (effect) => {
          if (effect.effectId === action.payload.effectId) {
            return { ...effect, selected: !effect.selected };
          }
          return effect;
        }
      );

      return { ...state, targetConditionalEffects: updatedTargetEffects };
    }

    case SET_STATE: {
      return { ...action.payload };
    }

    default:
      return state;
  }
}

// Example action creators
export const toggleCasterEffect = (effectId: number) => ({
  type: TOGGLE_CASTER_EFFECT,
  payload: { effectId },
});

export const toggleTargetEffect = (effectId: number) => ({
  type: TOGGLE_TARGET_EFFECT,
  payload: { effectId },
});

export const setState = (newState: WeaponAttackConditionalEffectsDto) => ({
  type: SET_STATE,
  payload: newState,
});

export default ConditionalEffectsReducer;

export const initialData: ConditionalEffectsDto = {
  casterConditionalEffects: [],
  targetConditionalEffects: [],
};
