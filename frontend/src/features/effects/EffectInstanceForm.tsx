import {
  useCallback,
  useContext,
  useEffect,
  useReducer,
  useState,
} from "react";
import Box from "../../ui/containers/Box";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import RadioGroup from "../../ui/forms/RadioGroup";
import TextArea from "../../ui/forms/TextArea";
import MovementEffectForm, {
  Effect as MovementEffect,
  initialState as MovementEffectInitialState,
} from "./effectTypes/MovementEffectForm";
import SavingThrowEffectForm, {
  Effect as SavingThrowEffect,
  initialState as SavingThrowEffectInitialState,
} from "./effectTypes/SavingThrowEffectForm";
import AbilityEffectForm, {
  Effect as AbilityEffect,
  initialState as AbilityEffectInitialState,
} from "./effectTypes/AbilityEffectForm";
import SkillEffectForm, {
  Effect as SkillEffect,
  initialState as SkillEffectInitialState,
} from "./effectTypes/SkillEffectForm";
import ResistanceEffectForm, {
  Effect as ResistanceEffect,
  initialState as ResistanceEffectInitialState,
} from "./effectTypes/ResistanceEffectForm";
import AttackRollEffectForm, {
  Effect as AttackRollEffect,
  initialState as AttackRollEffectInitialState,
} from "./effectTypes/AttackRollEffectForm";
import ArmorClassEffectForm, {
  Effect as ArmorClassEffect,
  initialState as ArmorClassEffectInitialState,
} from "./effectTypes/valueEffects/ArmorClassEffectForm";
import ProficiencyEffectForm, {
  Effect as ProficiencyEffect,
  initialState as ProficiencyEffectInitialState,
} from "./effectTypes/ProficiencyEffectForm";
import LanguageEffectForm, {
  Effect as LanguageEffect,
  initialState as LanguageEffectInitialState,
} from "./effectTypes/LanguageEffectForm";
import HealingEffectForm, {
  Effect as HealingEffect,
  initialState as HealingEffectInitialState,
} from "./effectTypes/valueEffects/HealingEffectForm";
import ActionEffectForm, {
  Effect as ActionEffect,
  initialState as ActionEffectInitialState,
} from "./effectTypes/ActionEffectForm";
import MagicItemEffectForm, {
  Effect as MagicItemEffect,
  initialState as MagicItemEffectInitialState,
} from "./effectTypes/valueEffects/MagicItemEffectForm";
import SizeEffectForm, {
  Effect as SizeEffect,
  initialState as SizeEffectInitialState,
} from "./effectTypes/SizeEffectForm";
import InitiativeEffectForm, {
  Effect as InitiativeEffect,
  initialState as InitiativeEffectInitialState,
} from "./effectTypes/valueEffects/InitiativeEffectForm";
import DamageEffectForm, {
  Effect as DamageEffect,
  initialState as DamageEffectInitialState,
} from "./effectTypes/DamageEffectForm";
import HitpointsEffectForm, {
  Effect as HitpointsEffect,
  initialState as HitpointsEffectInitialState,
} from "./effectTypes/HitpointsEffectForm";
import AttacksPerActionEffectForm, {
  Effect as AttacksPerActionEffect,
  initialState as AttacksPerActionEffectInitialState,
} from "./effectTypes/AttacksPerActionEffectForm";
import StatusEffectForm, {
  Effect as StatusEffect,
  initialState as StatusEffectInitialState,
} from "./effectTypes/StatusEffectForm";
import MovementCostEffectForm, {
  Effect as MovementCostEffect,
  initialState as MovementCostEffectInitialState,
} from "./effectTypes/MovementCostEffectForm";
import DummyEffectForm, {
  Effect as DummyEffect,
  initialState as DummyEffectInitialState,
} from "./effectTypes/DummyEffectForm";
import styled from "styled-components";
import Button from "../../ui/interactive/Button";
import Spinner from "../../ui/interactive/Spinner";
import { ValueEffect } from "./valueEffect";
import { useEffectInstance } from "./hooks/useEffectInstance";
import { useUpdateEffectInstance } from "./hooks/useUpdateEffectInstance";
import { EffectContext } from "./contexts/BlueprintOrInstanceContext";
import { EffectParentObjectIdContext } from "../../context/EffectParentObjectIdContext";
import { useCreateEffectInstance } from "./hooks/useCreateEffectInstance";
import { EditModeContext } from "../../context/EditModeContext";
import FormRowLabelRight from "../../ui/forms/FormRowLabelRight";
import { ItemContext } from "../../context/ItemContext";

const effectTypes = [
  "movementEffect",
  "savingThrow",
  "abilityCheck",
  "skillCheck",
  "resistance",
  "attackBonus",
  "armorClassBonus",
  "proficiency",
  "language",
  "healing",
  "actions",
  // "magicItemStatus",
  "size",
  "initiative",
  "damage",
  "hitpoints",
  "attacksPerAction",
  "statusEffect",
  "movementCost",
  "dummy",
] as const; // remember to update EffectTypeToInitialStateMap when editing this

const effectTypesWeapon = [
  "damage",
  "magicItemStatus",
  "attackBonus"
] as const;

type EffectBody =
  | MovementEffect
  | SavingThrowEffect
  | AbilityEffect
  | SkillEffect
  | ResistanceEffect
  | AttackRollEffect
  | ArmorClassEffect
  | ProficiencyEffect
  | LanguageEffect
  | HealingEffect
  | ActionEffect
  | MagicItemEffect
  | SizeEffect
  | InitiativeEffect
  | DamageEffect
  | HitpointsEffect
  | AttacksPerActionEffect
  | StatusEffect
  | MovementCostEffect
  | DummyEffect;

const EffectTypeToInitialStateMap = new Map<
  (typeof effectTypes)[number],
  EffectBody
>([
  ["movementEffect", MovementEffectInitialState],
  ["savingThrow", SavingThrowEffectInitialState],
  ["abilityCheck", AbilityEffectInitialState],
  ["skillCheck", SkillEffectInitialState],
  ["resistance", ResistanceEffectInitialState],
  ["attackBonus", AttackRollEffectInitialState],
  ["armorClassBonus", ArmorClassEffectInitialState],
  ["proficiency", ProficiencyEffectInitialState],
  ["language", LanguageEffectInitialState],
  ["healing", HealingEffectInitialState],
  ["actions", ActionEffectInitialState],
  ["magicItemStatus", MagicItemEffectInitialState],
  ["size", SizeEffectInitialState],
  ["initiative", InitiativeEffectInitialState],
  ["damage", DamageEffectInitialState],
  ["hitpoints", HitpointsEffectInitialState],
  ["attacksPerAction", AttacksPerActionEffectInitialState],
  ["statusEffect", StatusEffectInitialState],
  ["movementCost", MovementCostEffectInitialState],
  ["dummy", DummyEffectInitialState],
]);

export type EffectBlueprint = {
  id: number | null;
  durationLeft: number;
  name: string;
  description: string;
  resourceLevel: number;
  resourceAmount: number;
  savingThrowSuccess: boolean;
  conditional: boolean;
  isImplemented: boolean;
  effectType: (typeof effectTypes)[number];
  effectTypeBody: EffectBody;
};

type Action = {
  type:
    | "setName"
    | "setDescription"
    | "setResourceLevel"
    | "setDurationLeft"
    | "setSavingThrowSuccess"
    | "setConditional"
    | "setEffectType"
    | "setEffectTypeBody"
    | "resetState";
  payload: any;
};

export const initialState: EffectBlueprint = {
  id: null,
  durationLeft: 0,
  name: "New effect",
  description: "Effect description",
  resourceLevel: 1,
  savingThrowSuccess: false,
  effectType: "damage",
  effectTypeBody: DamageEffectInitialState,
  resourceAmount: 0,
  conditional: false,
  isImplemented: false,
};

const effectReducer = (
  state: EffectBlueprint,
  action: Action
): EffectBlueprint => {
  let newState: EffectBlueprint = initialState;
  switch (action.type) {
    case "setName":
      newState = { ...state, name: action.payload };
      break;
    case "setDescription":
      newState = { ...state, description: action.payload };
      break;
    case "setDurationLeft":
      newState = { ...state, durationLeft: Number(action.payload) };
      break;
    case "setResourceLevel":
      newState = { ...state, resourceLevel: Number(action.payload) };
      break;
    case "setSavingThrowSuccess":
      newState = { ...state, savingThrowSuccess: action.payload };
      break;
    case "setConditional":
      newState = { ...state, conditional: action.payload };
      break;
    case "setEffectType":
      newState = { ...state, effectType: action.payload };
      newState = {
        ...newState,
        effectTypeBody: EffectTypeToInitialStateMap.get(newState.effectType)!, // non-null assertion used "!"
      };
      break;
    case "setEffectTypeBody":
      newState = { ...state, effectTypeBody: action.payload };
      break;
    case "resetState":
      newState = action.payload;
      break;
    default:
      newState = state;
      break;
  }

  return newState;
};

export default function EffectInstanceForm({
  effectId,
  isConstant,
}: {
  effectId: number | null;
  isConstant: boolean;
}) {
  const { editMode } = useContext(EditModeContext);
  const [actualEffectId, setActualEffectId] = useState(effectId);
  const { objectId, objectType } = useContext(EffectParentObjectIdContext);
  const { objectType: itemObjectType } = useContext(ItemContext);
  const { isLoading, effectInstance, error } =
    useEffectInstance(actualEffectId);
  const { isPending: isPendingUpdate, updateEffectInstance } =
    useUpdateEffectInstance(
      () => {},
      effectInstance?.id as number,
      objectId as number,
      objectType
    );
  const { isPending: isPendingCreate, createEffectInstance } =
    useCreateEffectInstance(
      (id: number) => setActualEffectId(id),
      objectId as number,
      objectType,
      isConstant
    );
  const [state, dispatch] = useReducer(effectReducer, initialState);
  const [resetHappened, setResetHappened] = useState(false);
  // Update local state when data is fetched
  useEffect(() => {
    if (effectInstance) {
      dispatch({
        type: "resetState",
        payload: effectInstance,
      });
      setResetHappened(true);
    }
  }, [effectInstance]);
  const handleChildStateUpdate = useCallback((x: EffectBody) => {
    dispatch({ type: "setEffectTypeBody", payload: x });
  }, []);

  const bodyValueEffect = state.effectTypeBody as ValueEffect;
  const bodyDamageEffect = state.effectTypeBody as DamageEffect;
  const bodyProficiencyEffect = state.effectTypeBody as ProficiencyEffect;
  const bodyLanguageEffect = state.effectTypeBody as LanguageEffect;
  const bodyValueEffectDisable_Level =
    bodyValueEffect.value?.additionalValues?.some(
      (value) =>
        value.levelsInClassId === null &&
        (value.additionalValueType === "LevelsInClass")
    ) || false;
  const bodyValueEffectDisable_Skill =
    bodyValueEffect.value?.additionalValues?.some(
      (value) =>
        value.skill === null && value.additionalValueType === "SkillBonus"
    ) || false;
  const bodyValueEffectDisable_Ability =
    bodyValueEffect.value?.additionalValues?.some(
      (value) =>
        value.ability === null &&
        value.additionalValueType === "AbilityScoreModifier"
    ) || false;
  const bodyDamageEffectDisable_DamageType =
    bodyDamageEffect.effectType?.damageEffect !== "ExtraWeaponDamage" &&
    bodyDamageEffect.effectType?.damageEffect_DamageType === null;
  const bodyProficiencyEffectDisable_ItemFamily =
    bodyProficiencyEffect.effectType?.proficiencyEffect ===
      "SpecificItemFamily" &&
    bodyProficiencyEffect.grantsProficiencyInItemFamilyId === null;
  const bodyLanguageEffectDisable_Language =
    bodyLanguageEffect.languageId === null;

  const disableUpdateButton = () => {
    return (
      bodyValueEffectDisable_Level ||
      bodyValueEffectDisable_Skill ||
      bodyValueEffectDisable_Ability ||
      bodyDamageEffectDisable_DamageType ||
      bodyProficiencyEffectDisable_ItemFamily ||
      bodyLanguageEffectDisable_Language
    );
  };
  const disableReason = [];
  if (bodyValueEffectDisable_Level) {
    disableReason.push(
      "One of Additional Values in effect requires Class selection"
    );
  }
  if (bodyValueEffectDisable_Skill) {
    disableReason.push(
      "One of Additional Values in effect requires Skill selection"
    );
  }
  if (bodyValueEffectDisable_Ability) {
    disableReason.push(
      "One of Additional Values in effect requires Ability selection"
    );
  }
  if (bodyDamageEffectDisable_DamageType) {
    disableReason.push(
      "One of Additional Values in effect requires Damage Type selection"
    );
  }
  const disableReasonJoined = disableReason.join("; ");
  if (
    isLoading ||
    isPendingUpdate ||
    isPendingCreate ||
    (!resetHappened && effectId)
  ) {
    return <Spinner></Spinner>;
  }
  if (error) {
    return <>Error</>;
  }
  let disableForm = !editMode;
  let effectTypesAllowed = itemObjectType === "Weapon" && objectType === "ItemItself" ? effectTypesWeapon : effectTypes; 

  return (
    <EffectContext.Provider
      value={{ effect: "Instance", effectId: actualEffectId }}
    >
      {/* <ScrollContainer> */}
        <Container>
          <Div1>
            <FormRowVertical label="Name">
              <Input
                disabled={disableForm}
                value={state.name}
                onChange={(x) =>
                  dispatch({ type: "setName", payload: x.target.value })
                }
              ></Input>
            </FormRowVertical>
            <FormRowVertical label="Description">
              <TextArea
                disabled={disableForm}
                value={state.description}
                onChange={(x) =>
                  dispatch({ type: "setDescription", payload: x.target.value })
                }
              ></TextArea>
            </FormRowVertical>
            {!isConstant && (
              <FormRowVertical label="Duration">
                <Input
                  disabled={disableForm}
                  value={state.durationLeft}
                  type="number"
                  min={1}
                  onChange={(x) =>
                    dispatch({
                      type: "setDurationLeft",
                      payload: Number(x.target.value),
                    })
                  }
                ></Input>
              </FormRowVertical>
            )}
            {
              objectType !== "ItemItself" &&
              <FormRowLabelRight label="Is conditional">
                <Input
                  disabled={disableForm}
                  type="checkbox"
                  checked={state.conditional}
                  onChange={(x) =>
                    dispatch({
                      type: "setConditional",
                      payload: x.target.checked,
                    })
                  }
                ></Input>
              </FormRowLabelRight>
            }
          </Div1>
          <Div2>
            <RadioGroup
              disabled={disableForm}
              values={[
                { value: "movementEffect", label: "Movement effect" },
                { value: "savingThrow", label: "Saving throw" },
                { value: "abilityCheck", label: "Ability check" },
                { value: "skillCheck", label: "Skill check" },
                { value: "resistance", label: "Resistance" },
                { value: "attackBonus", label: "Attack bonus" },
                { value: "armorClassBonus", label: "Armor class bonus" },
                { value: "proficiency", label: "Proficiency" },
                { value: "language", label: "Language" },
                { value: "healing", label: "Healing" },
                { value: "actions", label: "Actions" },
                { value: "magicItemStatus", label: "Magic item status" },
                { value: "size", label: "Size" },
                { value: "initiative", label: "Initiative" },
                { value: "damage", label: "Damage" },
                { value: "hitpoints", label: "Hit points" },
                { value: "attacksPerAction", label: "Attacks per action" },
                { value: "statusEffect", label: "Status effect" },
                // { value: "movementCost", label: "Movement cost" },
                { value: "dummy", label: "Dummy effect" },
              ].filter( x => effectTypesAllowed.includes(x.value))}
              onChange={(x) => {
                console.log(x);
                dispatch({ type: "setEffectType", payload: x });
              }}
              name="effectType"
              label="Effect type"
              currentValue={state.effectType}
            ></RadioGroup>
          </Div2>
          <Div3>
            {state.effectType === "movementEffect" && (
              <MovementEffectForm
                onChange={handleChildStateUpdate}
                effect={state.effectTypeBody as MovementEffect}
              ></MovementEffectForm>
            )}
            {state.effectType === "savingThrow" && (
              <SavingThrowEffectForm
                onChange={handleChildStateUpdate}
                effect={state.effectTypeBody as SavingThrowEffect}
              ></SavingThrowEffectForm>
            )}
            {state.effectType === "abilityCheck" && (
              <AbilityEffectForm
                onChange={handleChildStateUpdate}
                effect={state.effectTypeBody as AbilityEffect}
              ></AbilityEffectForm>
            )}
            {state.effectType === "skillCheck" && (
              <SkillEffectForm
                onChange={handleChildStateUpdate}
                effect={state.effectTypeBody as SkillEffect}
              ></SkillEffectForm>
            )}
            {state.effectType === "resistance" && (
              <ResistanceEffectForm
                onChange={handleChildStateUpdate}
                effect={state.effectTypeBody as ResistanceEffect}
              ></ResistanceEffectForm>
            )}
            {state.effectType === "attackBonus" && (
              <AttackRollEffectForm
                onChange={handleChildStateUpdate}
                effect={state.effectTypeBody as AttackRollEffect}
              ></AttackRollEffectForm>
            )}
            {state.effectType === "armorClassBonus" && (
              <ArmorClassEffectForm
                onChange={handleChildStateUpdate}
                effect={state.effectTypeBody as ArmorClassEffect}
              ></ArmorClassEffectForm>
            )}
            {state.effectType === "proficiency" && (
              <ProficiencyEffectForm
                onChange={handleChildStateUpdate}
                effect={state.effectTypeBody as ProficiencyEffect}
              ></ProficiencyEffectForm>
            )}
            {state.effectType === "language" && (
              <LanguageEffectForm
                onChange={handleChildStateUpdate}
                effect={state.effectTypeBody as LanguageEffect}
              ></LanguageEffectForm>
            )}
            {state.effectType === "healing" && (
              <HealingEffectForm
                onChange={handleChildStateUpdate}
                effect={state.effectTypeBody as HealingEffect}
              ></HealingEffectForm>
            )}
            {state.effectType === "actions" && (
              <ActionEffectForm
                onChange={handleChildStateUpdate}
                effect={state.effectTypeBody as ActionEffect}
              ></ActionEffectForm>
            )}
            {state.effectType === "magicItemStatus" && (
              <MagicItemEffectForm
                onChange={handleChildStateUpdate}
                effect={state.effectTypeBody as MagicItemEffect}
              ></MagicItemEffectForm>
            )}
            {state.effectType === "size" && (
              <SizeEffectForm
                onChange={handleChildStateUpdate}
                effect={state.effectTypeBody as SizeEffect}
              ></SizeEffectForm>
            )}
            {state.effectType === "initiative" && (
              <InitiativeEffectForm
                onChange={handleChildStateUpdate}
                effect={state.effectTypeBody as InitiativeEffect}
              ></InitiativeEffectForm>
            )}
            {state.effectType === "damage" && (
              <DamageEffectForm
                onChange={handleChildStateUpdate}
                effect={state.effectTypeBody as DamageEffect}
              ></DamageEffectForm>
            )}
            {state.effectType === "hitpoints" && (
              <HitpointsEffectForm
                onChange={handleChildStateUpdate}
                effect={state.effectTypeBody as HitpointsEffect}
              ></HitpointsEffectForm>
            )}
            {state.effectType === "attacksPerAction" && (
              <AttacksPerActionEffectForm
                onChange={handleChildStateUpdate}
                effect={state.effectTypeBody as AttacksPerActionEffect}
              ></AttacksPerActionEffectForm>
            )}
            {state.effectType === "statusEffect" && (
              <StatusEffectForm
                onChange={handleChildStateUpdate}
                effect={state.effectTypeBody as StatusEffect}
              ></StatusEffectForm>
            )}
            {state.effectType === "movementCost" && (
              <MovementCostEffectForm
                onChange={handleChildStateUpdate}
                effect={state.effectTypeBody as MovementCostEffect}
              ></MovementCostEffectForm>
            )}
            {state.effectType === "dummy" && (
              <DummyEffectForm
                onChange={handleChildStateUpdate}
                effect={state.effectTypeBody as LanguageEffect}
              ></DummyEffectForm>
            )}
          </Div3>
        </Container>
      {/* </ScrollContainer> */}
      <FormRowVertical error={disableReasonJoined}>
        {effectInstance && (
          <Button
            onClick={() => updateEffectInstance(state)}
            disabled={disableUpdateButton() || disableForm}
          >
            Update
          </Button>
        )}
        {!effectInstance && (
          <Button
            onClick={() => createEffectInstance(state)}
            disabled={disableUpdateButton() || disableForm}
          >
            Save
          </Button>
        )}
      </FormRowVertical>
    </EffectContext.Provider>
  );
}

EffectInstanceForm.defaultProps = {
  isConstant: true,
};

const Container = styled(Box)`
  display: grid;
  grid-template-columns: auto 1fr;
  grid-template-rows: auto 1fr;
  gap: 10px;
  width: 70vw;
  height: 90vh;
  overflow-y: scroll;
  scrollbar-color: var(--color-button-primary) var(--color-main-background);
  scrollbar-width: thin;
  padding: 10px;
`;

// const ScrollContainer = styled(Box)`
//   overflow-y: hidden;
//   padding: 0;
// `;

const Div1 = styled.div`
  grid-column: 1 / 3;
  grid-row: 1 / 2;
`;
const Div2 = styled.div`
  grid-column: 1 / 2;
  grid-row: 2 / 3;
`;

const Div3 = styled.div`
  grid-column: 2 / 3;
  grid-row: 2 / 3;
`;
