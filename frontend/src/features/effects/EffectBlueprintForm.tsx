import {
  useCallback,
  useContext,
  useEffect,
  useReducer,
  useState,
} from "react";
import Box from "../../ui/containers/Box";
import FormRowLabelRight from "../../ui/forms/FormRowLabelRight";
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
import styled from "styled-components";
import Button from "../../ui/interactive/Button";
import Spinner from "../../ui/interactive/Spinner";
import { useEffectBlueprint } from "./hooks/useEffectBlueprint";
import { useUpdateEffectBlueprint } from "./hooks/useUpdateEffectBlueprint";
import { PowerIdContext } from "../powers/contexts/PowerIdContext";
import { ValueEffect } from "./valueEffect";
import { EffectContext } from "./contexts/BlueprintOrInstanceContext";
import { useCreateEffectBlueprint } from "./hooks/useCreateEffectBlueprint";
import { EditModeContext } from "../../context/EditModeContext";

const effectTypes = [
  "movementEffect",
  "savingThrow",
  "abilityCheck",
  "skillCheck",
  "resistance",
  "attackBonus",
  "armorClassBonus",
  "proficiency",
  "healing",
  "actions",
  "magicItemStatus",
  "size",
  "initiative",
  "damage",
  "hitpoints",
  "attacksPerAction",
  "statusEffect",
  "movementCost",
] as const; // remember to update EffectTypeToInitialStateMap when editing this

type EffectBody =
  | MovementEffect
  | SavingThrowEffect
  | AbilityEffect
  | SkillEffect
  | ResistanceEffect
  | AttackRollEffect
  | ArmorClassEffect
  | ProficiencyEffect
  | HealingEffect
  | ActionEffect
  | MagicItemEffect
  | SizeEffect
  | InitiativeEffect
  | DamageEffect
  | HitpointsEffect
  | AttacksPerActionEffect
  | StatusEffect
  | MovementCostEffect;

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
]);

export type EffectBlueprint = {
  id: number | null;
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
    | "setSavingThrowSuccess"
    | "setConditional"
    | "setEffectType"
    | "setEffectTypeBody"
    | "resetState";
  payload: any;
};

export const initialState: EffectBlueprint = {
  id: null,
  name: "New effect",
  description: "Effect description",
  resourceLevel: 1,
  savingThrowSuccess: false,
  effectType: "actions",
  effectTypeBody: ActionEffectInitialState,
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
      console.log("reset state");
      newState = action.payload;
      break;
    default:
      newState = state;
      break;
  }
  console.log(action.type);
  console.log(newState);
  return newState;
};

export default function EffectBlueprintForm({
  effectId,
}: {
  effectId: number | null;
}) {
  const { editMode } = useContext(EditModeContext);
  const [actualEffectId, setActualEffectId] = useState(effectId);
  const { powerId } = useContext(PowerIdContext);
  const { isLoading, effectBlueprint, error } =
    useEffectBlueprint(actualEffectId);
  const { isPending, updateEffectBlueprint } = useUpdateEffectBlueprint(
    () => {},
    effectBlueprint?.id as number,
    powerId
  );
  const { isPending: isPendingCreate, createEffectBlueprint } =
    useCreateEffectBlueprint(
      (id: number) => setActualEffectId(id),
      powerId as number
    );
  const [state, dispatch] = useReducer(effectReducer, initialState);
  const [resetHappened, setResetHappened] = useState(false);
  // Update local state when data is fetched
  useEffect(() => {
    if (effectBlueprint) {
      dispatch({
        type: "resetState",
        payload: effectBlueprint,
      });
      setResetHappened(true);
    }
  }, [effectBlueprint]);
  const handleChildStateUpdate = useCallback((x: EffectBody) => {
    dispatch({ type: "setEffectTypeBody", payload: x });
  }, []);
  const disableUpdateButton = () => {
    let body = state.effectTypeBody as ValueEffect;
    return (
      body.value?.additionalValues?.some(
        (value) =>
          (value.levelsInClassId === null &&
            (value.additionalValueType === "LevelsInClass" ||
              value.additionalValueType === "TotalLevel")) ||
          (value.skill === null &&
            value.additionalValueType === "SkillBonus") ||
          (value.ability === null &&
            value.additionalValueType === "AbilityScoreModifier")
      ) || false
    );
  };
  if (
    isLoading ||
    isPending ||
    isPendingCreate ||
    (!resetHappened && effectId)
  ) {
    return <Spinner></Spinner>;
  }
  if (error) {
    console.log(error);
    return <>Error</>;
  }
  let disableForm = !editMode;
  return (
    <EffectContext.Provider value={{ effect: "Blueprint" }}>
      <ScrollContainer>
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
            <FormRowVertical label="Level of immaterial resource used">
              <Input
                disabled={disableForm}
                type="number"
                value={state.resourceLevel}
                onChange={(x) =>
                  dispatch({
                    type: "setResourceLevel",
                    payload: x.target.value,
                  })
                }
              ></Input>
            </FormRowVertical>
            <FormRowLabelRight label="Successful saving throw">
              <Input
                disabled={disableForm}
                type="checkbox"
                checked={state.savingThrowSuccess}
                onChange={(x) =>
                  dispatch({
                    type: "setSavingThrowSuccess",
                    payload: x.target.checked,
                  })
                }
              ></Input>
            </FormRowLabelRight>
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
                { value: "healing", label: "Healing" },
                { value: "actions", label: "Actions" },
                { value: "magicItemStatus", label: "Magic item status" },
                { value: "size", label: "Size" },
                { value: "initiative", label: "Initiative" },
                { value: "damage", label: "Damage" },
                { value: "hitpoints", label: "Hit points" },
                { value: "attacksPerAction", label: "Attacks per action" },
                { value: "statusEffect", label: "Status effect" },
                { value: "movementCost", label: "Movement cost" },
              ]}
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
          </Div3>
        </Container>
      </ScrollContainer>
      {effectBlueprint && (
        <Button
          onClick={() => updateEffectBlueprint(state)}
          disabled={disableUpdateButton() || disableForm}
        >
          Update
        </Button>
      )}
      {!effectBlueprint && (
        <Button
          onClick={() => createEffectBlueprint(state)}
          disabled={disableUpdateButton() || disableForm}
        >
          Save
        </Button>
      )}
    </EffectContext.Provider>
  );
}

const Container = styled.div`
  display: grid;
  grid-template-columns: auto 1fr;
  grid-template-rows: auto auto;
  gap: 10px;
  width: 70vw;
  height: 90vh;
  overflow-y: scroll;
  scrollbar-color: var(--color-button-primary) var(--color-main-background);
  scrollbar-width: thin;
  padding: 10px;
`;

const ScrollContainer = styled(Box)`
  overflow-y: hidden;
  padding: 0;
`;

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
