import styled from "styled-components";
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
`;

export default function RadioGroup({
  values,
  onChange,
  name,
  label,
}: {
  values: ValueSet[];
  onChange: (param: string) => void;
  name: string;
  label: string;
}) {
  return (
    <RadioGroupContainer>
      <Heading as="h3">{label}</Heading>
      {values.map((value) => (
        <FormRowLabelRight label={value.label}>
          <Input
            type="radio"
            id={value.value}
            name={name}
            onChange={() => onChange(value.value)}
          ></Input>
        </FormRowLabelRight>
      ))}
    </RadioGroupContainer>
  );
}
