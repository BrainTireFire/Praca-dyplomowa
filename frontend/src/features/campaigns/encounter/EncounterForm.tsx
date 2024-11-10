import React, { useEffect, useState } from "react";
import Heading from "../../../ui/text/Heading";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import { useForm } from "react-hook-form";
import Input from "../../../ui/forms/Input";
import Button from "../../../ui/interactive/Button";
import Form from "../../../ui/forms/Form";
import styled from "styled-components";
import EncounterMapTable from "./EncounterMapTable";
import EncounterNPCTable from "./EncounterNPCTable";
import { useCampaign } from "../hooks/useCampaign";
import Spinner from "../../../ui/interactive/Spinner";
import MapBoard from "../../homebrew/maps/MapBoard";
import { Coordinate } from "../../../models/session/Coordinate";
import EncounterMapForm from "./EncounterMapForm";

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

type Board = {
  id: number;
  name: string;
  sizeX: number;
  sizeY: number;
  fields: any[];
};

export default function EncounterForm() {
  const [board, setBoard] = useState<Board | null>(null);
  const [encounterCreated, setEncounterCreated] = useState(false);
  const { isLoading, campaign } = useCampaign(encounterCreated);
  const { register, formState, handleSubmit, reset } = useForm<any>();
  const { errors } = formState;

  function onSubmit(data: any) {
    if (data.name && board) {
      setEncounterCreated(true);
    }
  }

  return (
    <>
      {encounterCreated ? (
        <EncounterMapForm
          campaign={campaign}
          board={board}
          fields={board?.fields}
        />
      ) : (
        <Form onSubmit={handleSubmit(onSubmit)}>
          <GridStyled>
            <GridInputStyled>
              <FormRowVertical label={"Name"} error={errors?.name?.message}>
                <Input
                  type="text"
                  id="name"
                  placeholder="Please write name of the encounter"
                  {...register("name", { required: "Name is required" })}
                />
              </FormRowVertical>
            </GridInputStyled>

            <TableContainerStyled>
              <TableStyled>
                <EncounterMapTable onSelectBoard={setBoard} />
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
      )}
    </>
  );
}
