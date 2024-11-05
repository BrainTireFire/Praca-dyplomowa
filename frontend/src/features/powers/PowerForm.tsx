import { useReducer } from "react";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import { ability } from "../effects/abilities";
import { EffectBlueprint } from "../effects/EffectBlueprintForm";
import {
  ActionType,
  actionTypeOptions,
  AreaShape,
  CastableBy,
  CharacterClass,
  ImmaterialResource,
  MaterialResource,
  Power,
  PowerType,
  SavingThrowRoll,
  TargetType,
  UpcastBy,
} from "./power";
import Dropdown from "../../ui/forms/Dropdown";

// Action types
export enum PowerActionTypes {
  UPDATE_NAME = "UPDATE_NAME",
  UPDATE_DESCRIPTION = "UPDATE_DESCRIPTION",
  UPDATE_ACTION_TYPE = "UPDATE_ACTION_TYPE",
  UPDATE_IS_IMPLEMENTED = "UPDATE_IS_IMPLEMENTED",
  UPDATE_CASTABLE_BY = "UPDATE_CASTABLE_BY",
  UPDATE_POWER_TYPE = "UPDATE_POWER_TYPE",
  UPDATE_TARGET_TYPE = "UPDATE_TARGET_TYPE",
  UPDATE_RANGE = "UPDATE_RANGE",
  UPDATE_MAX_TARGETS = "UPDATE_MAX_TARGETS",
  UPDATE_MAX_TARGETS_TO_EXCLUDE = "UPDATE_MAX_TARGETS_TO_EXCLUDE",
  UPDATE_AREA_SIZE = "UPDATE_AREA_SIZE",
  UPDATE_AREA_SHAPE = "UPDATE_AREA_SHAPE",
  UPDATE_AURA_SIZE = "UPDATE_AURA_SIZE",
  UPDATE_DIFFICULTY_CLASS = "UPDATE_DIFFICULTY_CLASS",
  UPDATE_SAVING_THROW_ABILITY = "UPDATE_SAVING_THROW_ABILITY",
  UPDATE_REQUIRES_CONCENTRATION = "UPDATE_REQUIRES_CONCENTRATION",
  UPDATE_SAVING_THROW_BEHAVIOUR = "UPDATE_SAVING_THROW_BEHAVIOUR",
  UPDATE_SAVING_THROW_ROLL = "UPDATE_SAVING_THROW_ROLL",
  UPDATE_VERBAL_COMPONENT = "UPDATE_VERBAL_COMPONENT",
  UPDATE_SOMATIC_COMPONENT = "UPDATE_SOMATIC_COMPONENT",
  UPDATE_DURATION = "UPDATE_DURATION",
  UPDATE_UPCAST_BY = "UPDATE_UPCAST_BY",
  UPDATE_CLASS_FOR_UPCASTING = "UPDATE_CLASS_FOR_UPCASTING",
  UPDATE_IMMATERIAL_RESOURCE_USED = "UPDATE_IMMATERIAL_RESOURCE_USED",
  UPDATE_MATERIAL_RESOURCES_USED = "UPDATE_MATERIAL_RESOURCES_USED",
  UPDATE_EFFECT_BLUEPRINTS = "UPDATE_EFFECT_BLUEPRINTS",
}

// Action interfaces
interface UpdateNameAction {
  type: PowerActionTypes.UPDATE_NAME;
  payload: string;
}

interface UpdateDescriptionAction {
  type: PowerActionTypes.UPDATE_DESCRIPTION;
  payload: string;
}

interface UpdateActionTypeAction {
  type: PowerActionTypes.UPDATE_ACTION_TYPE;
  payload: ActionType;
}

interface UpdateIsImplementedAction {
  type: PowerActionTypes.UPDATE_IS_IMPLEMENTED;
  payload: boolean;
}

interface UpdateCastableByAction {
  type: PowerActionTypes.UPDATE_CASTABLE_BY;
  payload: CastableBy;
}

interface UpdatePowerTypeAction {
  type: PowerActionTypes.UPDATE_POWER_TYPE;
  payload: PowerType;
}

interface UpdateTargetTypeAction {
  type: PowerActionTypes.UPDATE_TARGET_TYPE;
  payload: TargetType;
}

interface UpdateRangeAction {
  type: PowerActionTypes.UPDATE_RANGE;
  payload: number;
}

interface UpdateMaxTargetsAction {
  type: PowerActionTypes.UPDATE_MAX_TARGETS;
  payload: number;
}

interface UpdateMaxTargetsToExcludeAction {
  type: PowerActionTypes.UPDATE_MAX_TARGETS_TO_EXCLUDE;
  payload: number;
}

interface UpdateAreaSizeAction {
  type: PowerActionTypes.UPDATE_AREA_SIZE;
  payload: number;
}

interface UpdateAreaShapeAction {
  type: PowerActionTypes.UPDATE_AREA_SHAPE;
  payload: AreaShape;
}

interface UpdateAuraSizeAction {
  type: PowerActionTypes.UPDATE_AURA_SIZE;
  payload: number | null;
}

interface UpdateDifficultyClassAction {
  type: PowerActionTypes.UPDATE_DIFFICULTY_CLASS;
  payload: number | null;
}

interface UpdateSavingThrowAbilityAction {
  type: PowerActionTypes.UPDATE_SAVING_THROW_ABILITY;
  payload: ability;
}

interface UpdateRequiresConcentrationAction {
  type: PowerActionTypes.UPDATE_REQUIRES_CONCENTRATION;
  payload: boolean;
}

interface UpdateSavingThrowBehaviourAction {
  type: PowerActionTypes.UPDATE_SAVING_THROW_BEHAVIOUR;
  payload: SavingThrowBehaviour;
}

interface UpdateSavingThrowRollAction {
  type: PowerActionTypes.UPDATE_SAVING_THROW_ROLL;
  payload: SavingThrowRoll;
}

interface UpdateVerbalComponentAction {
  type: PowerActionTypes.UPDATE_VERBAL_COMPONENT;
  payload: boolean;
}

interface UpdateSomaticComponentAction {
  type: PowerActionTypes.UPDATE_SOMATIC_COMPONENT;
  payload: boolean;
}

interface UpdateDurationAction {
  type: PowerActionTypes.UPDATE_DURATION;
  payload: number;
}

interface UpdateUpcastByAction {
  type: PowerActionTypes.UPDATE_UPCAST_BY;
  payload: UpcastBy;
}

interface UpdateClassForUpcastingAction {
  type: PowerActionTypes.UPDATE_CLASS_FOR_UPCASTING;
  payload: CharacterClass;
}

interface UpdateImmaterialResourceUsedAction {
  type: PowerActionTypes.UPDATE_IMMATERIAL_RESOURCE_USED;
  payload: ImmaterialResource;
}

interface UpdateMaterialResourcesUsedAction {
  type: PowerActionTypes.UPDATE_MATERIAL_RESOURCES_USED;
  payload: MaterialResource[];
}

interface UpdateEffectBlueprintsAction {
  type: PowerActionTypes.UPDATE_EFFECT_BLUEPRINTS;
  payload: EffectBlueprint;
}

// Union type for all action interfaces
type PowerAction =
  | UpdateNameAction
  | UpdateDescriptionAction
  | UpdateActionTypeAction
  | UpdateIsImplementedAction
  | UpdateCastableByAction
  | UpdatePowerTypeAction
  | UpdateTargetTypeAction
  | UpdateRangeAction
  | UpdateMaxTargetsAction
  | UpdateMaxTargetsToExcludeAction
  | UpdateAreaSizeAction
  | UpdateAreaShapeAction
  | UpdateAuraSizeAction
  | UpdateDifficultyClassAction
  | UpdateSavingThrowAbilityAction
  | UpdateRequiresConcentrationAction
  | UpdateSavingThrowBehaviourAction
  | UpdateSavingThrowRollAction
  | UpdateVerbalComponentAction
  | UpdateSomaticComponentAction
  | UpdateDurationAction
  | UpdateUpcastByAction
  | UpdateClassForUpcastingAction
  | UpdateImmaterialResourceUsedAction
  | UpdateMaterialResourcesUsedAction
  | UpdateEffectBlueprintsAction;

const powerReducer = (state: Power, action: PowerAction): Power => {
  switch (action.type) {
    case PowerActionTypes.UPDATE_NAME:
      return { ...state, name: action.payload };
    case PowerActionTypes.UPDATE_DESCRIPTION:
      return { ...state, description: action.payload };
    case PowerActionTypes.UPDATE_ACTION_TYPE:
      return { ...state, actionType: action.payload };
    case PowerActionTypes.UPDATE_IS_IMPLEMENTED:
      return { ...state, isImplemented: action.payload };
    case PowerActionTypes.UPDATE_CASTABLE_BY:
      return { ...state, castableBy: action.payload };
    case PowerActionTypes.UPDATE_POWER_TYPE:
      return { ...state, powerType: action.payload };
    case PowerActionTypes.UPDATE_TARGET_TYPE:
      return { ...state, targetType: action.payload };
    case PowerActionTypes.UPDATE_RANGE:
      return { ...state, range: action.payload };
    case PowerActionTypes.UPDATE_MAX_TARGETS:
      return { ...state, maxTargets: action.payload };
    case PowerActionTypes.UPDATE_MAX_TARGETS_TO_EXCLUDE:
      return { ...state, maxTargetsToExclude: action.payload };
    case PowerActionTypes.UPDATE_AREA_SIZE:
      return { ...state, areaSize: action.payload };
    case PowerActionTypes.UPDATE_AREA_SHAPE:
      return { ...state, areaShape: action.payload };
    case PowerActionTypes.UPDATE_AURA_SIZE:
      return { ...state, auraSize: action.payload };
    case PowerActionTypes.UPDATE_DIFFICULTY_CLASS:
      return { ...state, difficultyClass: action.payload };
    case PowerActionTypes.UPDATE_SAVING_THROW_ABILITY:
      return { ...state, savingThrowAbility: action.payload };
    case PowerActionTypes.UPDATE_REQUIRES_CONCENTRATION:
      return { ...state, requiresConcentration: action.payload };
    case PowerActionTypes.UPDATE_SAVING_THROW_BEHAVIOUR:
      return { ...state, savingThrowBehaviour: action.payload };
    case PowerActionTypes.UPDATE_SAVING_THROW_ROLL:
      return { ...state, savingThrowRoll: action.payload };
    case PowerActionTypes.UPDATE_VERBAL_COMPONENT:
      return { ...state, verbalComponent: action.payload };
    case PowerActionTypes.UPDATE_SOMATIC_COMPONENT:
      return { ...state, somaticComponent: action.payload };
    case PowerActionTypes.UPDATE_DURATION:
      return { ...state, duration: action.payload };
    case PowerActionTypes.UPDATE_UPCAST_BY:
      return { ...state, upcastBy: action.payload };
    case PowerActionTypes.UPDATE_CLASS_FOR_UPCASTING:
      return { ...state, classForUpcasting: action.payload };
    case PowerActionTypes.UPDATE_IMMATERIAL_RESOURCE_USED:
      return { ...state, immaterialResourceUsed: action.payload };
    case PowerActionTypes.UPDATE_MATERIAL_RESOURCES_USED:
      return { ...state, materialResourcesUsed: action.payload };
    case PowerActionTypes.UPDATE_EFFECT_BLUEPRINTS:
      return { ...state, effectBlueprints: action.payload };
    default:
      return state;
  }
};

export default function PowerForm({ power }: { power: Power }) {
  const [state, dispatch] = useReducer(powerReducer, power);
  return (
    <>
      <FormRowVertical label="Name">
        <Input
          value={state.name}
          onChange={(e) =>
            dispatch({
              type: PowerActionTypes.UPDATE_NAME,
              payload: e.target.value,
            })
          }
        ></Input>
      </FormRowVertical>
      <FormRowVertical label="Description">
        <Input
          value={state.description}
          onChange={(e) =>
            dispatch({
              type: PowerActionTypes.UPDATE_DESCRIPTION,
              payload: e.target.value,
            })
          }
        ></Input>
      </FormRowVertical>
      <FormRowVertical label="Action type">
        <Dropdown
          valuesList={actionTypeOptions}
          setChosenValue={(e) =>
            dispatch({
              type: PowerActionTypes.UPDATE_ACTION_TYPE,
              payload: e as ActionType,
            })
          }
          chosenValue={state.actionType}
        ></Dropdown>
      </FormRowVertical>
      <FormRowVertical label="Immaterial resource used">
        <Dropdown
          valuesList={}
          setChosenValue={(e) =>
            dispatch({
              type: PowerActionTypes.UPDATE_ACTION_TYPE,
              payload: e as ActionType,
            })
          }
          chosenValue={state.actionType}
        ></Dropdown>
      </FormRowVertical>
    </>
  );
}
