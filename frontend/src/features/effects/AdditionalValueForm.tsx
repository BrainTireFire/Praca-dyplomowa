import Box from "../../ui/containers/Box";
import Dropdown from "../../ui/forms/Dropdown";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import {
  AdditionalValue,
  AdditionalValueTypeLabelMap,
  AdditionalValueTypes,
} from "./DiceSetForm";
import { useClasses } from "../characters/hooks/useClass";
import Spinner from "../../ui/interactive/Spinner";
import { abilities, abilitiesDropdown } from "./abilities";
import { skills, skillsDropdown } from "./skills";
import Heading from "../../ui/text/Heading";

export function AdditionalValueForm({
  value,
  onChange,
}: {
  value: AdditionalValue;
  onChange: (x: AdditionalValue) => void;
}) {
  const {
    isLoading: isLoadingClasses,
    classes,
    error: errorClasses,
  } = useClasses();
  let localValue = { ...value };
  const updateType = (type: (typeof AdditionalValueTypes)[number]) => {
    localValue.additionalValueType = type;
    onChange(localValue);
  };
  const updateClass = (classId: number) => {
    localValue.levelsInClassId = classId;
    localValue.className = classes
      ? classes.filter((x) => x.id === classId)[0].name
      : "error";
    onChange(localValue);
  };
  const updateAbility = (ability: (typeof abilities)[number]) => {
    localValue.ability = ability;
    onChange(localValue);
  };
  const updateSkill = (skill: (typeof skills)[number]) => {
    localValue.skill = skill;
    onChange(localValue);
  };
  if (errorClasses) {
    return "Error";
  }
  if (isLoadingClasses) return <Spinner></Spinner>;

  return (
    <Box>
      <Heading as="h3">
        Additional values are calculated based on casters stats
      </Heading>
      <FormRowVertical label={"Type"}>
        <Dropdown
          valuesList={AdditionalValueTypes.map((item) => {
            return {
              value: item,
              label: AdditionalValueTypeLabelMap[item],
            };
          })}
          setChosenValue={(e) =>
            updateType(e as (typeof AdditionalValueTypes)[number])
          }
          chosenValue={localValue.additionalValueType}
        ></Dropdown>
      </FormRowVertical>
      <FormRowVertical label={"Class"}>
        <Dropdown
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
          setChosenValue={(e) => updateClass(Number(e))}
          chosenValue={localValue.levelsInClassId?.toString() ?? null}
        ></Dropdown>
      </FormRowVertical>
      <FormRowVertical label={"Ability"}>
        <Dropdown
          valuesList={abilitiesDropdown}
          setChosenValue={(e) => updateAbility(e as (typeof abilities)[number])}
          chosenValue={localValue.additionalValueType}
        ></Dropdown>
      </FormRowVertical>
      <FormRowVertical label={"Skill"}>
        <Dropdown
          valuesList={skillsDropdown}
          setChosenValue={(e) => updateSkill(e as (typeof skills)[number])}
          chosenValue={localValue.additionalValueType}
        ></Dropdown>
      </FormRowVertical>
    </Box>
  );
}
