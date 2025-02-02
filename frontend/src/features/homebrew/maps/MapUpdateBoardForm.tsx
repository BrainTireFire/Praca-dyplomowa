import React, { useEffect, useState } from "react";
import styled from "styled-components";
import Input from "../../../ui/forms/Input";
import TextArea from "../../../ui/forms/TextArea";
import { set, useForm } from "react-hook-form";
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
  align-items: center;
`;

const Span = styled.span`
  margin-right: 3px;
  margin-left: 3px;
`;

const ErrorMessage = styled.p`
  color: var(--color-form-error);
  margin-top: 5px;
  font-size: 0.875rem;
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

  if (isLoading) {
    return <Spinner />;
  }

  if (!board) {
    return <div>Board is null</div>;
  }

  setValue("name", board.name);
  setValue("description", board.description);
  setValue("sizeX", board.sizeX);
  setValue("sizeY", board.sizeY);

  const handleDropdownChange = (value: string | null) => {
    console.log(value);

    if (value === null) {
      return;
    }

    setData((prevData) => ({
      ...prevData,
      boardSize: value,
      name: prevData?.name ?? "",
      sizeX: prevData?.sizeX ?? 0,
      sizeY: prevData?.sizeY ?? 0,
      description: prevData?.description ?? "",
    }));

    setValue("boardSize", value);

    const [width, height] = value.split(" x ").map(Number);
    setValue("sizeX", width);
    setValue("sizeY", height);
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
                <Dropdown
                  valuesList={BOARD_SIZES}
                  chosenValue={data?.boardSize ?? "16 x 9"}
                  setChosenValue={(value) => handleDropdownChange(value)}
                />
              </FieldSet>
              {formState.errors.sizeX && (
                <ErrorMessage>{formState.errors.sizeX.message}</ErrorMessage>
              )}
              {formState.errors.sizeY && (
                <ErrorMessage>{formState.errors.sizeY.message}</ErrorMessage>
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
