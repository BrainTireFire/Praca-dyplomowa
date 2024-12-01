import styled, { css } from "styled-components";
import Box from "../containers/Box";
import Heading from "../text/Heading";
import FormRowLabelRight from "./FormRowLabelRight";
import Input from "./Input";

type ValueSet = {
  label: string;
  value: string;
};

const RadioGroupContainer = styled(Box)`
  display: flex;
  flex-direction: column;
  width: fit-content;
  ${(props) => props.customStyles}
`;

export default function RadioGroup({
  values,
  onChange,
  name,
  label,
  currentValue,
  customStyles,
}: {
  values: ValueSet[];
  onChange: (param: string) => void;
  name: string;
  label: string;
  currentValue: string;
  customStyles?: ReturnType<typeof css>;
}) {
  return (
    <RadioGroupContainer customStyles={customStyles}>
      <Heading as="h3">{label}</Heading>
      {values.map((value) => (
        <FormRowLabelRight label={value.label} key={value.value}>
          <Input
            type="radio"
            id={value.value}
            name={name}
            onChange={() => onChange(value.value)}
            checked={currentValue === value.value}
          ></Input>
        </FormRowLabelRight>
      ))}
    </RadioGroupContainer>
  );
}
