import React from "react";
import Heading from "../../../ui/text/Heading";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import { useForm } from "react-hook-form";
import Input from "../../../ui/forms/Input";
import Button from "../../../ui/interactive/Button";
import Form from "../../../ui/forms/Form";
import styled from "styled-components";
import EncounterMapTable from "./EncounterMapTable";
import EncounterNPCTable from "./EncounterNPCTable";

const GridStyled = styled.div`
  display: grid;
  grid-template-columns: 1fr;
  grid-template-rows: 1fr auto 1fr;
  gap: 20px;
  height: 100%;
`;

const GridInputStyled = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
`;

const TableContainerStyled = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin: 0 100px;
  gap: 20px;
`;

const TableStyled = styled.div`
  flex: 1;
`;

const GridButtonStyled = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
`;

export default function EncounterForm() {
  const { register, formState, handleSubmit, reset } = useForm<any>();
  const { errors } = formState;

  function onSubmit() {}

  return (
    <Form onSubmit={handleSubmit(onSubmit)}>
      <GridStyled>
        <GridInputStyled>
          <FormRowVertical
            label={"Name"}
            // error={errors?.username?.message}
          >
            <Input
              type="text"
              id="username"
              placeholder={"Please write name of the encounter"}
            />
          </FormRowVertical>
        </GridInputStyled>

        <TableContainerStyled>
          <TableStyled>
            <EncounterMapTable />
          </TableStyled>
          <TableStyled>
            <EncounterNPCTable />
          </TableStyled>
        </TableContainerStyled>

        <GridButtonStyled>
          <Button size="large" variation="primary">
            Create Encounter
          </Button>
        </GridButtonStyled>
      </GridStyled>
    </Form>
  );
}
