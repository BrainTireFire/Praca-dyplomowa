import { useState } from "react";
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
import { CharacterItem } from "../../../models/character";
import { Board } from "../../../models/map/Board";
import { useCreateEncounter } from "../hooks/useCreateEncounter";

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

type EncounterFormProps = {
  name: string;
};

export default function EncounterForm() {
  const { isLoading, campaign } = useCampaign();
  const { createEncounter, isPending } = useCreateEncounter();
  const [selectedMap, setSelectedMap] = useState<Board | null>(null);
  const [selectedNpcs, setSelectedNpcs] = useState<CharacterItem[]>([]);
  const { register, formState, handleSubmit, reset } =
    useForm<EncounterFormProps>();
  const { errors } = formState;

  const isFormValid = selectedMap !== null && selectedNpcs.length > 0;

  if (isLoading) {
    return <Spinner />;
  }

  if (!campaign) {
    return <div></div>;
  }

  function onSubmit({ name }: EncounterFormProps) {
    if (selectedMap && selectedNpcs && name && campaign) {
      createEncounter(
        {
          name: name,
          boardId: selectedMap.id,
          campaignId: campaign.id,
          charactersIds: selectedNpcs.map((npc) => npc.id),
        },
        {
          onSettled: () => {
            reset();
            setSelectedMap(null);
            setSelectedNpcs([]);
          },
        }
      );
    }
  }

  return (
    <Form onSubmit={handleSubmit(onSubmit)}>
      <GridStyled>
        <GridInputStyled>
          <FormRowVertical label={"Name"} error={errors?.name?.message}>
            <Input
              type="text"
              id="username"
              placeholder={"Please write name of the encounter"}
              {...register("name", {
                required: "Name is required",
              })}
              disabled={isPending}
            />
          </FormRowVertical>
        </GridInputStyled>

        <TableContainerStyled>
          <TableStyled>
            <EncounterMapTable onSelect={setSelectedMap} />
          </TableStyled>
          <TableStyled>
            <EncounterNPCTable
              chosenNpcs={selectedNpcs}
              onConfirm={(selectedCharacters: CharacterItem[]) =>
                setSelectedNpcs(selectedCharacters)
              }
            />
          </TableStyled>
        </TableContainerStyled>

        <GridButtonStyled>
          <Button size="large" variation="primary" disabled={!isFormValid}>
            Create Encounter
          </Button>
        </GridButtonStyled>
      </GridStyled>
    </Form>
  );
}
