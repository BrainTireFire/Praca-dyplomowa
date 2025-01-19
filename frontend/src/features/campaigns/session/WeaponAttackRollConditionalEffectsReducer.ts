import { WeaponAttackConditionalEffectsDto } from "../../../services/apiEncounter";

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
    targetId: number;
    effectId: number;
  };
}

export interface SetStateAction {
  type: typeof SET_STATE;
  payload: WeaponAttackConditionalEffectsDto;
}

// Define the reducer
function weaponAttackConditionalEffectsReducer(
  state: WeaponAttackConditionalEffectsDto,
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
      const { targetId, effectId } = action.payload;
      const updatedTargets = {
        ...state.targetsConditionalEffects,
        [targetId]: {
          ...state.targetsConditionalEffects[targetId],
          targetConditionalEffects: state.targetsConditionalEffects[
            targetId
          ].targetConditionalEffects.map((effect) => {
            if (effect.effectId === effectId) {
              return { ...effect, selected: !effect.selected };
            }
            return effect;
          }),
        },
      };

      return { ...state, targetsConditionalEffects: updatedTargets };
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

export const toggleTargetEffect = (targetId: number, effectId: number) => ({
  type: TOGGLE_TARGET_EFFECT,
  payload: { targetId, effectId },
});

export const setState = (newState: WeaponAttackConditionalEffectsDto) => ({
  type: SET_STATE,
  payload: newState,
});

export default weaponAttackConditionalEffectsReducer;

export const initialData: WeaponAttackConditionalEffectsDto = {
  weaponId: 1,
  weaponName: "Initial name",
  weaponDescription: "Initial description",
  casterConditionalEffects: [],
  targetsConditionalEffects: {},
};
