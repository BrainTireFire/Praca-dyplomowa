import React, { useState } from "react";
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
  fieldX: number;
  fieldY: number;
  description: string;
};

export default function BoardCreateForm() {
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

  function onSubmit(dataForm: MapFormProps) {
    setData(dataForm);
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
                  id="fieldX"
                  type="text"
                  placeholder="X"
                  {...register("fieldX", {
                    required: "This field is required",
                  })}
                  style={{ width: "70px" }}
                />
                <Span> x </Span>
                <Input
                  id="fieldY"
                  type="text"
                  placeholder="Y"
                  {...register("fieldY", {
                    required: "This field is required",
                  })}
                  style={{ width: "70px" }}
                />
              </FieldSet>
              {formState.errors.fieldX && (
                <ErrorMessage>{formState.errors.fieldX.message}</ErrorMessage>
              )}
              {formState.errors.fieldY && (
                <ErrorMessage>{formState.errors.fieldY.message}</ErrorMessage>
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
