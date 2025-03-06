import React, { useEffect, useState } from "react";
import styled from "styled-components";
import Input from "../../../ui/forms/Input";
import TextArea from "../../../ui/forms/TextArea";
import { Controller, set, useForm } from "react-hook-form";
import Button from "../../../ui/interactive/Button";
import Form from "../../../ui/forms/Form";
import Heading from "../../../ui/text/Heading";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import FormContainer from "../../../ui/forms/FormContainer";
import MapCreatorLayout from "./MapCreatorLayout";
import Spinner from "../../../ui/interactive/Spinner";
import { useBoard } from "./useBoard";
import { Field } from "../../../models/map/Board";
import Dropdown from "../../../ui/forms/Dropdown";

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
  //justify-content: space-between;
  gap: 10px;
  padding-bottom: 10px;
`;

const FieldContainerStyled = styled.div`
  display: flex;
  flex-direction: column;
  align-items: left;
`;

const Span = styled.span`
  margin-right: 3px;
  margin-left: 3px;
`;

const ErrorMessage = styled.p`
  font-size: 1.4rem;
  color: var(--color-form-error);
  margin-top: 0.2rem;
  padding-bottom: 5px;
`;

type MapFormProps = {
  name: string;
  sizeX: number;
  sizeY: number;
  description: string;
  fields?: Field[];
  boardSize: string;
};

const BOARD_SIZES = [
  { value: "16 x 9", label: "16 x 9" },
  { value: "32 x 18", label: "32 x 18" },
  { value: "48 x 27", label: "48 x 27" },
  { value: "64 x 36", label: "64 x 36" },
  { value: "80 x 45", label: "80 x 45" },
  { value: "96 x 54", label: "96 x 54" },
];

export default function MapUpdateBoardForm() {
  const {
    register,
    formState,
    getValues,
    handleSubmit,
    reset,
    control,
    setValue,
  } = useForm<MapFormProps>();
  const { errors } = formState;
  const [toggleMap, setToggleMap] = useState(false);
  const [data, setData] = useState<MapFormProps>();
  const { isLoading, board } = useBoard();

  useEffect(() => {
    if (board) {
      setValue("name", board.name);
      setValue("description", board.description);
      setValue("sizeX", board.sizeX);
      setValue("sizeY", board.sizeY);
      setValue("boardSize", board.sizeX + " x " + board.sizeY);
    }
  }, [board, setValue, getValues]);

  if (isLoading) {
    return <Spinner />;
  }

  if (!board) {
    return <div>Board is null</div>;
  }

  const handleDropdownChange = (value: string | null) => {
    if (value === null) {
      return;
    }

    const [width, height] = value.split(" x ").map(Number);
    setValue("sizeX", width);
    setValue("sizeY", height);
    setValue("boardSize", value);

    setData((prevData) => ({
      ...prevData,
      boardSize: value,
      name: prevData?.name ?? "",
      sizeX: width ?? prevData?.sizeX ?? 0,
      sizeY: height ?? prevData?.sizeY ?? 0,
      description: prevData?.description ?? "",
    }));
  };

  function onSubmit(dataForm: MapFormProps) {
    setData({ ...dataForm, fields: board?.fields });
    setToggleMap(!toggleMap);
  }

  return (
    <>
      {toggleMap === true && <MapCreatorLayout boardData={data} />}
      {toggleMap === false && (
        <FormContainer>
          <Form onSubmit={handleSubmit(onSubmit)}>
            <FormRowVertical label={"Name"} error={errors?.name?.message}>
              <Input
                id="name"
                placeholder="Name"
                {...register("name")}
                style={{ width: "100%" }}
              />
            </FormRowVertical>
            <FormRowVertical
              label={"Description"}
              error={errors?.description?.message}
            >
              <TextArea
                id="description"
                placeholder="Description"
                {...register("description")}
                style={{ height: "150px" }}
              />
            </FormRowVertical>

            <FieldContainerStyled>
              <FieldSet>
                <Label>Board Size: </Label>
                <Controller
                  name="boardSize"
                  control={control}
                  rules={{ required: "Board size is required" }}
                  render={({ field }) => (
                    <Dropdown
                      valuesList={BOARD_SIZES}
                      chosenValue={field.value ?? null}
                      setChosenValue={(value) => {
                        field.onChange(value);
                        handleDropdownChange(value);
                      }}
                    />
                  )}
                />
              </FieldSet>
              {errors.boardSize && (
                <ErrorMessage>{errors.boardSize.message}</ErrorMessage>
              )}
            </FieldContainerStyled>

            <FormRowVertical>
              <Button size="large" variation="primary">
                Save board
              </Button>
            </FormRowVertical>
          </Form>
        </FormContainer>
      )}
    </>
  );
}
