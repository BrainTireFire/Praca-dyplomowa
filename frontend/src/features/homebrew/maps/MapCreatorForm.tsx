import { useCallback, useEffect, useRef, useState } from "react";
import styled from "styled-components";
import Input from "../../../ui/forms/Input";
import Button from "../../../ui/interactive/Button";
import ColorPicker from "react-pick-color";
import TextArea from "../../../ui/forms/TextArea";
import { useForm, Controller } from "react-hook-form";
import { useOutsideClick } from "../../../hooks/useOutsideClick";
import useClickOutside from "../../../hooks/useClickOutside";
import Heading from "../../../ui/text/Heading";

const Label = styled.label`
  font-weight: bold;
  margin-bottom: 5px;
`;

const FieldSet = styled.div`
  position: relative;
  width: 100%;
  margin-bottom: 15px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding-bottom: 10px;
`;

const FieldContainerStyled = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  border-bottom: 1px solid var(--color-border);
`;

const RadioGroup = styled.div`
  display: flex;
  justify-content: space-around;
  margin-bottom: 10px;
  gap: 10px;
`;

const Table = styled.table`
  width: 100%;
  border-collapse: collapse;
  margin-bottom: 10px;
`;

const TableHeader = styled.th`
  border: 1px solid var(--color-border);
  padding: 5px;
  background-color: var(--color-navbar);
`;

const TableCell = styled.td`
  border: 1px solid var(--color-border);
  padding: 5px;
`;

const ToggleButton = styled.button<{ color: string }>`
  background-color: ${(props) => props.color};
  color: var(--color-link);
  border: 1px solid ${(props) => props.color};
  width: 30px;
  height: 30px;
  cursor: pointer;
  border-radius: 50px;

  &:hover {
    opacity: 0.8;
  }
`;

const ColorPickerStyled = styled.div`
  position: absolute;
  top: 0;
  left: calc(100% + 10px);
  z-index: 999;
`;

const ChatForm = styled.form`
  display: flex;
  flex-direction: column;
  gap: 10px;
`;

const ErrorMessage = styled.p`
  color: var(--color-form-error);
  margin-top: 5px;
  font-size: 0.875rem;
`;

type MapFormProps = {
  fieldColor: string;
  fieldHeight: number;
  fieldEffects: string[];
  movementCost: string;
  fieldCover: string;
};

const InstructionsContainer = styled.div`
  padding: 20px;
  background-color: var(--color-navbar-border);
  border-radius: 8px;
  border: 1px solid var(--color-header-text);
  max-width: 400px;
  margin: 0 auto;
`;

const InstructioSpan = styled.span`
  color: var(--color-link);
`;

const InstructionText = styled.p`
  font-size: 16px;
  color: var(--color-button-text);
  margin: 5px 0;
`;

export default function MapCreatorForm({ selectedBox, onSubmit, fields }: any) {
  const colorPickerPopover = useRef();
  const [isColorPickerVisible, setIsColorPickerVisible] = useState(false);
  const [colorValue, setColorValue] = useState("#fff");
  const {
    register,
    formState,
    getValues,
    handleSubmit,
    reset,
    control,
    setValue,
  } = useForm<MapFormProps>();

  const setFormValues = useCallback(() => {
    const field = fields.find(
      (field: any) => field.x === selectedBox.x && field.y === selectedBox.y
    );
    if (field) {
      setValue("fieldColor", field.fieldColor);
      setValue("fieldHeight", field.fieldHeight);
      setValue("fieldEffects", field.fieldEffects);
      setValue("movementCost", field.movementCost);
      setValue("fieldCover", field.fieldCover);
      setColorValue(field.fieldColor);
    }
  }, [fields, selectedBox, setValue]);

  useEffect(() => {
    if (selectedBox) {
      setFormValues();
    }
  }, [selectedBox, setFormValues]);

  const onColorChange = (color: any) => {
    setColorValue(color.hex);
    setValue("fieldColor", color.hex);
  };

  const toggleColorPicker = (e: any) => {
    e.preventDefault();
    setIsColorPickerVisible(!isColorPickerVisible);
  };

  const close = useCallback(() => setIsColorPickerVisible(false), []);
  useClickOutside(colorPickerPopover, close);

  function onFormSubmit(data: MapFormProps) {
    onSubmit(data);
    setColorValue("#fff");
    reset();
  }

  return (
    <ChatForm onSubmit={handleSubmit(onFormSubmit)}>
      {selectedBox === null ? (
        <InstructionsContainer>
          <Heading as="h1">Instructions:</Heading>
          <InstructionText>
            To select a box:
            <InstructioSpan>Click on the desired box.</InstructioSpan>
          </InstructionText>
          <InstructionText>
            To deselect a box:
            <InstructioSpan>Click on the selected box.</InstructioSpan>
          </InstructionText>
        </InstructionsContainer>
      ) : (
        <>
          {/* Field Color */}
          <FieldContainerStyled>
            <FieldSet>
              <Label>Field Color: </Label>
              <ToggleButton color={colorValue} onClick={toggleColorPicker}>
                {isColorPickerVisible ? "" : ""}
              </ToggleButton>
              {isColorPickerVisible && (
                <ColorPickerStyled ref={colorPickerPopover}>
                  <Controller
                    name="fieldColor"
                    control={control}
                    defaultValue={colorValue}
                    render={({ field }) => (
                      <ColorPicker
                        color={field.value}
                        onChange={onColorChange}
                      />
                    )}
                  />
                </ColorPickerStyled>
              )}
            </FieldSet>
          </FieldContainerStyled>

          {/* Field Height */}
          <FieldContainerStyled>
            <FieldSet>
              <Label>Field Height:</Label>
              <Input
                id="fieldHeight"
                type="text"
                placeholder="Height"
                {...register("fieldHeight", {
                  required: "This field is required",
                })}
                style={{ width: "130px" }}
              />
            </FieldSet>
            {formState.errors.fieldHeight && (
              <ErrorMessage>
                {formState.errors.fieldHeight.message}
              </ErrorMessage>
            )}
          </FieldContainerStyled>

          {/* Field Effects */}
          <Label>Field Effects:</Label>
          <FieldContainerStyled>
            <FieldSet>
              <Table>
                <thead>
                  <tr>
                    <TableHeader>Name</TableHeader>
                    <TableHeader>Source</TableHeader>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <TableCell>Text</TableCell>
                    <TableCell>Text</TableCell>
                  </tr>
                </tbody>
              </Table>
            </FieldSet>
          </FieldContainerStyled>

          {/* Movement Cost */}
          <Label>Movement Cost:</Label>
          <FieldContainerStyled>
            <FieldSet>
              <RadioGroup>
                <label>
                  <Input
                    type="radio"
                    value="low"
                    {...register("movementCost", {
                      required: "This field is required",
                    })}
                  />{" "}
                  Low
                </label>
                <label>
                  <Input
                    type="radio"
                    value="high"
                    {...register("movementCost", {
                      required: "This field is required",
                    })}
                  />{" "}
                  High
                </label>
                <label>
                  <Input
                    type="radio"
                    value="impassable"
                    {...register("movementCost", {
                      required: "This field is required",
                    })}
                  />{" "}
                  Impassable
                </label>
              </RadioGroup>
            </FieldSet>
            {formState.errors.movementCost && (
              <ErrorMessage>
                {formState.errors.movementCost.message}
              </ErrorMessage>
            )}
          </FieldContainerStyled>

          {/* Field Cover Level */}
          <Label>Field Cover Level:</Label>
          <FieldContainerStyled>
            <FieldSet>
              <RadioGroup>
                <label>
                  <Input
                    type="radio"
                    value="no"
                    {...register("fieldCover", {
                      required: "This field is required",
                    })}
                  />{" "}
                  No Cover
                </label>
                <label>
                  <Input
                    type="radio"
                    value="half"
                    {...register("fieldCover", {
                      required: "This field is required",
                    })}
                  />{" "}
                  Half Cover
                </label>
                <label>
                  <Input
                    type="radio"
                    value="three"
                    {...register("fieldCover", {
                      required: "This field is required",
                    })}
                  />{" "}
                  Three Quarters
                </label>
                <label>
                  <Input
                    type="radio"
                    value="full"
                    {...register("fieldCover", {
                      required: "This field is required",
                    })}
                  />{" "}
                  Total Cover
                </label>
              </RadioGroup>
            </FieldSet>
            {formState.errors.fieldCover && (
              <ErrorMessage>{formState.errors.fieldCover.message}</ErrorMessage>
            )}
          </FieldContainerStyled>

          {/* Save Changes Button */}
          <Button size="medium" variation="primary" type="submit">
            Save Changes
          </Button>
        </>
      )}
    </ChatForm>
  );
}
