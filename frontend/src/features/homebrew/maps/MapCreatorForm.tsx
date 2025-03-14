import { useCallback, useEffect, useRef, useState } from "react";
import styled, { css } from "styled-components";
import Input from "../../../ui/forms/Input";
import Button from "../../../ui/interactive/Button";
import ColorPicker from "react-pick-color";
import TextArea from "../../../ui/forms/TextArea";
import { useForm, Controller } from "react-hook-form";
import useClickOutside from "../../../hooks/useClickOutside";
import Heading from "../../../ui/text/Heading";
import { useCreateBoard } from "./useCreateBoard";
import { BoardCreateDto } from "../../../models/map/BoardDto";
import { useUpdateBoard } from "./useUpdateBoard";
import { BoardUpdateDto } from "../../../models/map/BoardUpdate";
import { ReusableTable } from "../../../ui/containers/ReusableTable2";
import Modal from "../../../ui/containers/Modal";
import { PowerSelectionFormField } from "./PowerSelectionFormField";
import { PowerListItem } from "../../../models/power";

const Label = styled.label`
  font-weight: bold;
  margin-bottom: 5px;
`;

const FieldSet = styled.div`
  width: 100%;
  /* margin-bottom: 15px; */
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
  top: calc(10% - 5px);
  left: calc(20% - 25px);
  z-index: 999;
`;

const ChatForm = styled.form`
  display: flex;
  flex-direction: column;
  gap: 10px;
  height: 100%;
  overflow-y: auto;
  scrollbar-color: var(--color-button-primary) var(--color-main-background);
  scrollbar-width: thin;
  scrollbar-gutter: stable;
  padding: 10px 0 10px 10px;
`;

const ErrorMessage = styled.p`
  color: var(--color-form-error);
  margin-top: 5px;
  font-size: 0.875rem;
`;

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

type MapFormProps = {
  color: string;
  positionZ: number;
  fieldEffects: string[];
  fieldMovementCost: string;
  fieldCoverLevel: string;
  description: string;
  powers: PowerListItem[];
};

const TABLE_COLUMNS = {
  Id: "id",
  Name: "name",
};

export default function MapCreatorForm({ state, onSubmit }: any) {
  const colorPickerPopover = useRef();
  const [isColorPickerVisible, setIsColorPickerVisible] = useState(false);
  const [colorValue, setColorValue] = useState("#fff");
  const { register, formState, handleSubmit, reset, control, setValue } =
    useForm<MapFormProps>();
  const { selectedBox, fields } = state;
  const { updateBoard } = useUpdateBoard();
  const { createBoard, isLoading } = useCreateBoard();

  const setFormValues = useCallback(() => {
    const field = fields.find(
      (field: any) =>
        field.positionX === selectedBox.x && field.positionY === selectedBox.y
    );
    if (field) {
      setValue("description", field.description);
      setValue("color", field.color);
      setValue("positionZ", field.positionZ);
      setValue("fieldEffects", field.fieldEffects);
      setValue("fieldMovementCost", field.fieldMovementCost);
      setValue("fieldCoverLevel", field.fieldCoverLevel);
      setValue("powers", field.powers);
      setColorValue(field.color);
    }
  }, [fields, selectedBox, setValue]);

  useEffect(() => {
    if (selectedBox) {
      setFormValues();
    }
  }, [selectedBox, setFormValues]);

  const onColorChange = (color: any) => {
    setColorValue(color.hex);
    setValue("color", color.hex);
  };

  const onPowerChange = (powers: PowerListItem[]) => {
    setValue("powers", powers);
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

  function handleCreateBoard() {
    if (state.isUpdating) {
      const boardData: BoardUpdateDto = {
        name: state.board.name,
        description: state.board.description,
        sizeX: state.board.sizeX,
        sizeY: state.board.sizeY,
        fields: state.fields,
      };

      // I removing the id from the fields array
      boardData.fields = boardData.fields.map(({ ...field }) => {
        return {
          ...field,
          positionX: Number(field.positionX),
          positionY: Number(field.positionY),
          positionZ: Number(field.positionZ),
        };
      });

      updateBoard(boardData);
    } else {
      const boardData: BoardCreateDto = {
        name: state.board.name,
        description: state.board.description,
        sizeX: state.board.sizeX,
        sizeY: state.board.sizeY,
        fields: state.fields,
      };

      // I removing the id from the fields array
      boardData.fields = boardData.fields.map(({ ...field }) => {
        return {
          ...field,
          positionX: Number(field.positionX),
          positionY: Number(field.positionY),
          positionZ: Number(field.positionZ),
        };
      });

      createBoard(boardData, {
        onSettled: () => reset(),
      });
    }
  }

  return (
    <ChatForm onSubmit={handleSubmit(onFormSubmit)}>
      {selectedBox === null ? (
        <>
          <InstructionsContainer>
            <Heading as="h1">Instructions:</Heading>
            <InstructionText>
              To select a box:{" "}
              <InstructioSpan>Click on the desired box.</InstructioSpan>
            </InstructionText>
            <InstructionText>
              To deselect a box:{" "}
              <InstructioSpan>Click on the selected box.</InstructioSpan>
            </InstructionText>
            <InstructionText>
              To save all changes:{" "}
              <InstructioSpan>Click on the "Finished" button.</InstructioSpan>
            </InstructionText>
          </InstructionsContainer>

          <Button
            size="medium"
            variation="primary"
            type="button"
            onClick={handleCreateBoard}
            disabled={isLoading}
          >
            Finished
          </Button>
        </>
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
                    name="color"
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
                id="positionZ"
                type="text"
                placeholder="Height"
                {...register("positionZ", {
                  required: "This field is required",
                })}
                style={{ width: "130px" }}
              />
            </FieldSet>
            {formState.errors.positionZ && (
              <ErrorMessage>{formState.errors.positionZ.message}</ErrorMessage>
            )}
          </FieldContainerStyled>

          {/* Field Powers */}
          <Label>Field Powers:</Label>
          <Modal>
            <Modal.Open opens="mapCreateNewPowerField">
              <Button size="small" variation="primary" type="button">
                Change selection
              </Button>
            </Modal.Open>
            <Modal.Window name="mapCreateNewPowerField">
              <Controller
                name="powers"
                control={control}
                defaultValue={[]}
                render={({ field }) => (
                  <PowerSelectionFormField
                    onSelectPower={onPowerChange}
                    selectedPowers={field.value}
                  />
                )}
              />
            </Modal.Window>
          </Modal>

          <Controller
            name="powers"
            control={control}
            defaultValue={[]}
            render={({ field }) => (
              <ReusableTable
                tableRowsColomns={TABLE_COLUMNS}
                data={field.value}
                customTableContainer={css`
                  margin: 1px;
                  min-height: 100px;
                `}
                customHeader={css`
                  padding: 0.5px;
                `}
                // isSelectable={true}
                // onSelect={setSelectedMap}
                //isSearching={true}
                //mainHeader="Maps"
              />
            )}
          />

          {/* Movement Cost */}
          <Label>Movement Cost:</Label>
          <FieldContainerStyled>
            <FieldSet>
              <RadioGroup>
                <label>
                  <Input
                    type="radio"
                    value="Low"
                    {...register("fieldMovementCost", {
                      required: "This field is required",
                    })}
                  />{" "}
                  Low
                </label>
                <label>
                  <Input
                    type="radio"
                    value="High"
                    {...register("fieldMovementCost", {
                      required: "This field is required",
                    })}
                  />{" "}
                  High
                </label>
                <label>
                  <Input
                    type="radio"
                    value="Impassable"
                    {...register("fieldMovementCost", {
                      required: "This field is required",
                    })}
                  />{" "}
                  Impassable
                </label>
              </RadioGroup>
            </FieldSet>
            {formState.errors.fieldMovementCost && (
              <ErrorMessage>
                {formState.errors.fieldMovementCost.message}
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
                    value="NoCover"
                    {...register("fieldCoverLevel", {
                      required: "This field is required",
                    })}
                  />{" "}
                  No Cover
                </label>
                <label>
                  <Input
                    type="radio"
                    value="HalfCover"
                    {...register("fieldCoverLevel", {
                      required: "This field is required",
                    })}
                  />{" "}
                  Half Cover
                </label>
                <label>
                  <Input
                    type="radio"
                    value="ThreeQuartersCover"
                    {...register("fieldCoverLevel", {
                      required: "This field is required",
                    })}
                  />{" "}
                  Three Quarters
                </label>
                <label>
                  <Input
                    type="radio"
                    value="TotalCover"
                    {...register("fieldCoverLevel", {
                      required: "This field is required",
                    })}
                  />{" "}
                  Total Cover
                </label>
              </RadioGroup>
            </FieldSet>
            {formState.errors.fieldCoverLevel && (
              <ErrorMessage>
                {formState.errors.fieldCoverLevel.message}
              </ErrorMessage>
            )}
          </FieldContainerStyled>

          <Label>Description:</Label>
          <FieldContainerStyled>
            <TextArea
              id="description"
              placeholder="Description"
              {...register("description")}
              style={{ height: "100px", marginBottom: "10px" }}
            />
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
