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
};

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
                <Input
                  id="sizeX"
                  type="text"
                  placeholder="X"
                  {...register("sizeX", {
                    required: "This field is required",
                  })}
                  style={{ width: "70px" }}
                />
                <Span> x </Span>
                <Input
                  id="sizeY"
                  type="text"
                  placeholder="Y"
                  {...register("sizeY", {
                    required: "This field is required",
                  })}
                  style={{ width: "70px" }}
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
