import styled, { css } from "styled-components";
import Box from "../containers/Box";
import Heading from "../text/Heading";
import FormRowLabelRight from "./FormRowLabelRight";
import Input from "./Input";
import { HiXMark } from "react-icons/hi2";

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
  disabled,
}: {
  values: ValueSet[];
  onChange: (param: string) => void;
  name: string;
  label: string;
  currentValue: string;
  customStyles?: ReturnType<typeof css>;
  disabled: boolean;
}) {
  return (
    <RadioGroupContainer customStyles={customStyles}>
      <Heading as="h3">{label}</Heading>
      {values.map((value) => (
        <FormRowLabelRight label={value.label} key={value.value}>
          <>
          {(!disabled || currentValue !== value.value) && 
            <Input
            disabled={disabled}
            type="radio"
            id ={value.value}
            name={name}
            onChange={() => onChange(value.value)}
            checked={currentValue === value.value}
            ></Input>
            
          }
          {disabled && currentValue === value.value &&
            <HiXMark style={{alignSelf: "center"}}/>
          }
          </>
        </FormRowLabelRight>
      ))}
    </RadioGroupContainer>
  );
}

RadioGroup.defaultProps = {
  disabled: false,
};
