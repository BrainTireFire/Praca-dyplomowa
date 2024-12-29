import { useEffect, useReducer, useState } from "react";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import { abilitiesDropdown, ability } from "../effects/abilities";
import {
  ActionType,
  actionTypeOptions,
  AreaShape,
  areaShapeOptions,
  CastableBy,
  castableByOptions,
  CharacterClass,
  ImmaterialResource,
  MaterialComponent,
  Power,
  PowerType,
  powerTypeOptions,
  SavingThrowBehaviour,
  savingThrowBehaviourOptions,
  SavingThrowRoll,
  savingThrowRollOptions,
  // SavingThrowRoll,
  TargetType,
  targetTypeOptions,
  UpcastBy,
  upcastByOptions,
} from "./models/power";
import Dropdown from "../../ui/forms/Dropdown";
import { useImmaterialResourceBlueprints } from "./hooks/useImmaterialResourceBlueprints";
import Spinner from "../../ui/interactive/Spinner";
import FormRowLabelRight from "../../ui/forms/FormRowLabelRight";
import RadioGroup from "../../ui/forms/RadioGroup";
import styled, { css } from "styled-components";
import EffectTable from "./tables/EffectTable";
import Box from "../../ui/containers/Box";
import { EffectBlueprintListItem } from "./models/effectBlueprint";
import { useUpdatePower } from "./hooks/useUpdatePower";
import Button from "../../ui/interactive/Button";
import { usePower } from "./hooks/usePower";
import MaterialResourceTable from "./tables/MaterialResourceTable";
import { useCreatePower } from "./hooks/useCreatePower";
import Heading from "../../ui/text/Heading";
import { useClasses } from "../characters/hooks/useClass";

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
  UPDATE_OVERRIDE_CHARACTER_DC = "UPDATE_OVERRIDE_CHARACTER_DC",
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
  RESET_STATE = "RESET_STATE",
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
  payload: number;
}

interface UpdateDifficultyClassAction {
  type: PowerActionTypes.UPDATE_DIFFICULTY_CLASS;
  payload: number;
}

interface UpdateOverrideCharacterDCAction {
  type: PowerActionTypes.UPDATE_OVERRIDE_CHARACTER_DC;
  payload: boolean;
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
  payload: number | null;
}

interface UpdateImmaterialResourceUsedAction {
  type: PowerActionTypes.UPDATE_IMMATERIAL_RESOURCE_USED;
  payload: ImmaterialResource | null;
}

interface UpdateMaterialResourcesUsedAction {
  type: PowerActionTypes.UPDATE_MATERIAL_RESOURCES_USED;
  payload: MaterialComponent[];
}

interface UpdateEffectBlueprintsAction {
  type: PowerActionTypes.UPDATE_EFFECT_BLUEPRINTS;
  payload: EffectBlueprintListItem[];
}

interface ResetState {
  type: PowerActionTypes.RESET_STATE;
  payload: Power;
}

// Union type for all action interfaces
type PowerAction =
  | ResetState
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
  | UpdateOverrideCharacterDCAction
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
  console.log(action);
  switch (action.type) {
    case PowerActionTypes.UPDATE_NAME:
      return { ...state, name: action.payload };
    case PowerActionTypes.UPDATE_DESCRIPTION:
      return { ...state, description: action.payload };
    case PowerActionTypes.UPDATE_ACTION_TYPE:
      return { ...state, requiredActionType: action.payload };
    case PowerActionTypes.UPDATE_IS_IMPLEMENTED:
      return { ...state, isImplemented: action.payload };
    case PowerActionTypes.UPDATE_CASTABLE_BY:
      return { ...state, castableBy: action.payload };
    case PowerActionTypes.UPDATE_POWER_TYPE:
      if (action.payload === "Saveable") {
        return {
          ...state,
          powerType: action.payload,
          savingThrowAbility: "STRENGTH",
        };
      } else {
        return {
          ...state,
          powerType: action.payload,
          savingThrowAbility: null,
        };
      }
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
      if (action.payload === "None") {
        return { ...state, areaShape: action.payload, areaSize: 0 };
      }
      return { ...state, areaShape: action.payload };
    case PowerActionTypes.UPDATE_AURA_SIZE:
      return { ...state, auraSize: action.payload };
    case PowerActionTypes.UPDATE_OVERRIDE_CHARACTER_DC:
      if (action.payload) {
        return {
          ...state,
          difficultyClass: 10,
          overrideCastersDC: true,
        };
      } else {
        return {
          ...state,
          difficultyClass: 0,
          overrideCastersDC: false,
        };
      }
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
      let classForUpcasting = state.classForUpcasting;
      if (action.payload !== "ClassLevel") {
        classForUpcasting = null;
      }
      return {
        ...state,
        upcastBy: action.payload,
        classForUpcasting: classForUpcasting,
      };
    case PowerActionTypes.UPDATE_CLASS_FOR_UPCASTING:
      return { ...state, classForUpcasting: action.payload };
    case PowerActionTypes.UPDATE_IMMATERIAL_RESOURCE_USED:
      return { ...state, immaterialResourceUsed: action.payload };
    case PowerActionTypes.UPDATE_MATERIAL_RESOURCES_USED:
      return { ...state, materialResourcesUsed: action.payload };
    case PowerActionTypes.UPDATE_EFFECT_BLUEPRINTS:
      return { ...state, effectBlueprints: action.payload };
    case PowerActionTypes.RESET_STATE:
      return action.payload;
    default:
      return state;
  }
};

export const initialState: Power = {
  id: null,
  name: "New power",
  description: "Power description",
  requiredActionType: "Action",
  isImplemented: false,
  castableBy: "Character",
  powerType: "Attack",
  targetType: "Character",
  range: 0,
  maxTargets: 0,
  maxTargetsToExclude: 0,
  areaSize: 0,
  areaShape: "None",
  auraSize: 0,
  difficultyClass: 0,
  savingThrowAbility: null,
  requiresConcentration: false,
  savingThrowBehaviour: "Breaks",
  savingThrowRoll: "TakenOnce",
  verbalComponent: false,
  somaticComponent: false,
  duration: 0,
  upcastBy: "ResourceLevel",
  classForUpcasting: null,
  immaterialResourceUsed: null,
  materialResourcesUsed: [],
  effectBlueprints: [],
  overrideCastersDC: false,
};

export default function PowerForm({
  powerId,
  onCreate,
  onUpdate,
}: {
  powerId: number | null;
  onCreate: (id: number) => void;
  onUpdate: (id: number) => void;
}) {
  const [actualPowerId, setActualPowerId] = useState(powerId);
  const {
    isLoading: isLoadingPower,
    power,
    error: powerError,
  } = usePower(powerId);
  const [state, dispatch] = useReducer(powerReducer, power ?? initialState);
  // Update local state when data is fetched
  useEffect(() => {
    if (power) {
      dispatch({
        type: PowerActionTypes.RESET_STATE,
        payload: power,
      });
    }
  }, [power]);
  const { updatePower, isPending } = useUpdatePower(() => {});
  const { createPower, isPending: isPendingCreate } = useCreatePower(
    (id: number) => {
      setActualPowerId(id);
      onCreate(id);
    }
  );
  console.log(state);

  const {
    isLoading: isLoadingImmaterialResources,
    immaterialResourceBlueprints,
    error: errorImmaterialResources,
  } = useImmaterialResourceBlueprints();
  // const {
  //   isLoading: isLoadingMaterialResources,
  //   materialComponents: materialResources,
  //   error: errorMaterialResources,
  // } = useMaterialComponents(powerId);
  const {
    isLoading: isLoadingClasses,
    classes,
    error: errorClasses,
  } = useClasses();

  const localImmaterialResourceBlueprints = immaterialResourceBlueprints?.map(
    (x) => {
      return { id: x.id, name: x.name };
    }
  );
  const immaterialResourceBlueprintsDropdown = localImmaterialResourceBlueprints
    ? localImmaterialResourceBlueprints.map((x) => {
        return { value: x.id.toString(), label: x.name };
      })
    : [];
  if (
    isLoadingImmaterialResources ||
    isPending ||
    isPendingCreate ||
    isLoadingPower
  ) {
    return <Spinner></Spinner>;
  }
  if (errorImmaterialResources || errorClasses) return <>Error</>;
  let classForUpcastingError =
    state.upcastBy === "ClassLevel" && state.classForUpcasting === null
      ? "Select value!"
      : undefined;
  let resourceForUpcastingError =
    state.upcastBy === "ResourceLevel" && state.immaterialResourceUsed === null
      ? "Select value!"
      : undefined;
  let lockSaveButton = !!classForUpcastingError || !!resourceForUpcastingError;
  return (
    <>
      <Grid>
        <Row1>
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
              chosenValue={state.requiredActionType}
            ></Dropdown>
          </FormRowVertical>
          <FormRowVertical
            label="Immaterial resource used"
            error={resourceForUpcastingError}
          >
            <Dropdown
              valuesList={[
                ...immaterialResourceBlueprintsDropdown,
                { value: null, label: "None" },
              ]}
              setChosenValue={(e) =>
                dispatch({
                  type: PowerActionTypes.UPDATE_IMMATERIAL_RESOURCE_USED,
                  payload:
                    localImmaterialResourceBlueprints?.find(
                      (x) => x.id.toString() === e
                    ) ?? null,
                })
              }
              chosenValue={state.immaterialResourceUsed?.id.toString() ?? null}
            ></Dropdown>
          </FormRowVertical>
          <FormRowLabelRight label="Verbal component">
            <Input
              type="checkbox"
              checked={state.verbalComponent}
              onChange={(x) =>
                dispatch({
                  type: PowerActionTypes.UPDATE_VERBAL_COMPONENT,
                  payload: x.target.checked,
                })
              }
            ></Input>
          </FormRowLabelRight>
          <FormRowLabelRight label="Somatic component">
            <Input
              type="checkbox"
              checked={state.somaticComponent}
              onChange={(x) =>
                dispatch({
                  type: PowerActionTypes.UPDATE_SOMATIC_COMPONENT,
                  payload: x.target.checked,
                })
              }
            ></Input>
          </FormRowLabelRight>
        </Row1>
        <Row2>
          <Column1>
            <FormRowLabelRight label="Is implemented">
              <Input
                type="checkbox"
                checked={state.somaticComponent}
                onChange={(x) =>
                  dispatch({
                    type: PowerActionTypes.UPDATE_IS_IMPLEMENTED,
                    payload: x.target.checked,
                  })
                }
              ></Input>
            </FormRowLabelRight>
            <RadioGroup
              values={castableByOptions}
              onChange={(x) => {
                dispatch({
                  type: PowerActionTypes.UPDATE_CASTABLE_BY,
                  payload: x as CastableBy,
                });
              }}
              name="castableBy"
              label="Castable by"
              currentValue={state.castableBy}
            ></RadioGroup>
            <RadioGroup
              values={powerTypeOptions}
              onChange={(x) => {
                dispatch({
                  type: PowerActionTypes.UPDATE_POWER_TYPE,
                  payload: x as PowerType,
                });
              }}
              name="powerType"
              label="Power type"
              currentValue={state.powerType}
            ></RadioGroup>
            <RadioGroup
              values={targetTypeOptions}
              onChange={(x) => {
                dispatch({
                  type: PowerActionTypes.UPDATE_TARGET_TYPE,
                  payload: x as TargetType,
                });
              }}
              name="targetType"
              label="Target type"
              currentValue={state.targetType}
            ></RadioGroup>
          </Column1>
          <Column2>
            <Row1InColumn2>
              {/* <EffectTable
                effects={state.effectBlueprints}
                powerId={powerId ?? -1}
              ></EffectTable> */}
              {power && (
                <>
                  <MaterialResourceTable
                    materialComponents={state.materialResourcesUsed}
                    powerId={powerId ?? -1}
                  ></MaterialResourceTable>

                  <EffectTable
                    effects={state.effectBlueprints}
                    powerId={powerId ?? -1}
                  ></EffectTable>
                </>
              )}
              <RadioGroup
                values={upcastByOptions}
                onChange={(x) => {
                  dispatch({
                    type: PowerActionTypes.UPDATE_UPCAST_BY,
                    payload: x as UpcastBy,
                  });
                }}
                name="upcastBy"
                label="Upcasted by"
                currentValue={state.upcastBy}
              ></RadioGroup>
              <FormRowVertical
                label={"Class for upcasting"}
                error={classForUpcastingError}
              >
                <Dropdown
                  disabled={state.upcastBy !== "ClassLevel"}
                  valuesList={
                    classes
                      ? classes.map((item) => {
                          return {
                            value: item.id.toString(),
                            label: item.name,
                          };
                        })
                      : []
                  }
                  setChosenValue={(value) =>
                    dispatch({
                      type: PowerActionTypes.UPDATE_CLASS_FOR_UPCASTING,
                      payload: Number(value) ?? null,
                    })
                  }
                  chosenValue={state.classForUpcasting?.toString() ?? null}
                ></Dropdown>
              </FormRowVertical>
            </Row1InColumn2>
            <Row2InColumn2>
              <FormRowVertical
                label="Range"
                customStyles={css`
                  grid-column: 1;
                  grid-row: 1;
                `}
              >
                <Input
                  value={state.range}
                  type="number"
                  onChange={(e) =>
                    dispatch({
                      type: PowerActionTypes.UPDATE_RANGE,
                      payload: Number(e.target.value),
                    })
                  }
                ></Input>
              </FormRowVertical>
              <FormRowVertical
                label="Max targets"
                customStyles={css`
                  grid-column: 1;
                  grid-row: 2;
                `}
              >
                <Input
                  value={state.maxTargets}
                  type="number"
                  onChange={(e) =>
                    dispatch({
                      type: PowerActionTypes.UPDATE_MAX_TARGETS,
                      payload: Number(e.target.value),
                    })
                  }
                ></Input>
              </FormRowVertical>
              <FormRowVertical
                label="Max targets to exclude"
                customStyles={css`
                  grid-column: 1;
                  grid-row: 3;
                `}
              >
                <Input
                  value={state.maxTargetsToExclude}
                  type="number"
                  onChange={(e) =>
                    dispatch({
                      type: PowerActionTypes.UPDATE_MAX_TARGETS_TO_EXCLUDE,
                      payload: Number(e.target.value),
                    })
                  }
                ></Input>
              </FormRowVertical>
              <FormRowVertical
                label="Area size"
                customStyles={css`
                  grid-column: 2;
                  grid-row: 1;
                `}
              >
                <Input
                  disabled={
                    state.powerType === "Attack" ||
                    state.powerType === "AuraCreator" ||
                    state.areaShape === "None"
                  }
                  value={state.areaSize}
                  type="number"
                  onChange={(e) =>
                    dispatch({
                      type: PowerActionTypes.UPDATE_AREA_SIZE,
                      payload: Number(e.target.value),
                    })
                  }
                ></Input>
              </FormRowVertical>
              <FormRowVertical
                label="Area shape"
                customStyles={css`
                  grid-column: 2;
                  grid-row: 2;
                `}
              >
                <Dropdown
                  disabled={
                    state.powerType === "Attack" ||
                    state.powerType === "AuraCreator"
                  }
                  valuesList={[...areaShapeOptions]}
                  setChosenValue={(e) =>
                    dispatch({
                      type: PowerActionTypes.UPDATE_AREA_SHAPE,
                      payload: e as AreaShape,
                    })
                  }
                  chosenValue={state.areaShape}
                ></Dropdown>
              </FormRowVertical>
              <FormRowVertical
                label="Aura size"
                customStyles={css`
                  grid-column: 2;
                  grid-row: 3;
                `}
              >
                <Input
                  disabled={state.powerType !== "AuraCreator"}
                  value={state.auraSize}
                  type="number"
                  onChange={(e) =>
                    dispatch({
                      type: PowerActionTypes.UPDATE_AURA_SIZE,
                      payload: Number(e.target.value),
                    })
                  }
                ></Input>
              </FormRowVertical>
              <SettingGroupContainer
                customStyles={css`
                  grid-column: 3;
                  grid-row: 1/3;
                `}
              >
                <Heading as="h3">Saving throw settings</Heading>
                <FormRowLabelRight label="Override casters DC">
                  <Input
                    type="checkbox"
                    disabled={state.powerType !== "Saveable"}
                    checked={state.overrideCastersDC}
                    onChange={(e) =>
                      dispatch({
                        type: PowerActionTypes.UPDATE_OVERRIDE_CHARACTER_DC,
                        payload: e.target.checked,
                      })
                    }
                  ></Input>
                </FormRowLabelRight>
                <FormRowVertical label="Overriding value">
                  <Input
                    disabled={
                      state.powerType !== "Saveable" || !state.overrideCastersDC
                    }
                    value={state.difficultyClass}
                    type="number"
                    onChange={(e) =>
                      dispatch({
                        type: PowerActionTypes.UPDATE_DIFFICULTY_CLASS,
                        payload: Number(e.target.value),
                      })
                    }
                  ></Input>
                </FormRowVertical>
                <FormRowVertical
                  label="Saving throw"
                  customStyles={css`
                    grid-column: 3;
                    grid-row: 2;
                  `}
                >
                  <Dropdown
                    disabled={state.powerType !== "Saveable"}
                    valuesList={[...abilitiesDropdown]}
                    setChosenValue={(e) =>
                      dispatch({
                        type: PowerActionTypes.UPDATE_SAVING_THROW_ABILITY,
                        payload: e as ability,
                      })
                    }
                    chosenValue={state.savingThrowAbility}
                  ></Dropdown>
                </FormRowVertical>
              </SettingGroupContainer>

              <RadioGroup
                disabled={state.powerType !== "Saveable"}
                values={savingThrowBehaviourOptions}
                onChange={(x) => {
                  dispatch({
                    type: PowerActionTypes.UPDATE_SAVING_THROW_BEHAVIOUR,
                    payload: x as SavingThrowBehaviour,
                  });
                }}
                name="savingThrowBehaviour"
                label="Saving throw behaviour"
                currentValue={state.savingThrowBehaviour}
                customStyles={css`
                  grid-column: 4;
                  grid-row: 1;
                `}
              ></RadioGroup>
              <RadioGroup
                disabled={state.powerType !== "Saveable"}
                values={savingThrowRollOptions}
                onChange={(x) => {
                  dispatch({
                    type: PowerActionTypes.UPDATE_SAVING_THROW_ROLL,
                    payload: x as SavingThrowRoll,
                  });
                }}
                name="savingThrowBehaviour"
                label="Saving throw roll moment"
                currentValue={state.savingThrowRoll}
                customStyles={css`
                  grid-column: 4;
                  grid-row: 2;
                `}
              ></RadioGroup>
              <FormRowLabelRight
                label="Concentration"
                customStyles={css`
                  grid-column: 3;
                  grid-row: 3;
                `}
              >
                <Input
                  type="checkbox"
                  checked={state.requiresConcentration}
                  onChange={(x) =>
                    dispatch({
                      type: PowerActionTypes.UPDATE_REQUIRES_CONCENTRATION,
                      payload: x.target.checked,
                    })
                  }
                ></Input>
              </FormRowLabelRight>
              <FormRowVertical
                label="Effect duration"
                customStyles={css`
                  grid-column: 4;
                  grid-row: 3;
                `}
              >
                <Input
                  value={state.duration}
                  type="number"
                  onChange={(e) =>
                    dispatch({
                      type: PowerActionTypes.UPDATE_DURATION,
                      payload: Number(e.target.value),
                    })
                  }
                ></Input>
              </FormRowVertical>
            </Row2InColumn2>
          </Column2>
        </Row2>
      </Grid>
      {actualPowerId && (
        <Button onClick={() => updatePower(state)} disabled={lockSaveButton}>
          Update
        </Button>
      )}
      {!actualPowerId && (
        <Button onClick={() => createPower(state)} disabled={lockSaveButton}>
          Save to unlock more configuration options
        </Button>
      )}
    </>
  );
}

const Grid = styled(Box)`
  display: grid;
  grid-template-rows: auto auto;
  column-gap: 10px;
  row-gap: 10px;
`;

const Row1 = styled.div`
  display: flex;
  flex-direction: row;
  grid-row: 1/2;
  gap: 10px;
`;
const Row2 = styled.div`
  display: grid;
  grid-template-rows: auto auto;
  grid-row: 2/3;
  column-gap: 10px;
  row-gap: 10px;
`;

const Column1 = styled.div`
  display: flex;
  flex-direction: column;
  grid-column: 1/2;
  gap: 10px;
`;
const Column2 = styled.div`
  display: flex;
  flex-direction: column;
  grid-column: 2/3;
  gap: 10px;
`;

const Row1InColumn2 = styled.div`
  display: flex;
  flex-direction: row;
  gap: 10px;
`;

const Row2InColumn2 = styled.div`
  display: grid;
  column-gap: 10px;
  row-gap: 10px;
  grid-template-columns: auto auto auto auto;
  grid-template-rows: auto auto auto;
`;

PowerForm.defaultProps = {
  onCreate: (_id: number) => {},
  onUpdate: (_id: number) => {},
};

const SettingGroupContainer = styled(Box)`
  display: flex;
  flex-direction: column;
  width: fit-content;
  ${(props) => props.customStyles}
`;
