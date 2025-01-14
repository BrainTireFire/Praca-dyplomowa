import styled, { css } from "styled-components";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Input from "../../../ui/forms/Input";
import Button from "../../../ui/interactive/Button";
import { ControlStateActions } from "./SessionLayout";
import useRollInitiative from "../hooks/useRollInitiative";
import { Encounter } from "../../../models/encounter/Encounter";
import { HubConnection } from "@microsoft/signalr";
import Spinner from "../../../ui/interactive/Spinner";

export default function ActionBar({
  encounter,
  dispatch,
  connection,
}: {
  encounter: Encounter;
  dispatch: React.Dispatch<ControlStateActions>;
  connection: HubConnection;
}) {
  const { rollInitiative, isPending } = useRollInitiative(encounter.id, () =>
    connection.invoke("SendRequeryInitiative")
  );
  return (
    <Container>
      <Cell>
        <FormRowVertical label={"Actions taken"}>
          <span>
            <Input
              type="number"
              size="small"
              customStyles={css`
                width: 5em;
              `}
            />
            &nbsp;/ X
          </span>
        </FormRowVertical>
        <FormRowVertical label={"Bonus actions taken"}>
          <span>
            <Input
              type="number"
              size="small"
              customStyles={css`
                width: 5em;
              `}
            />
            &nbsp;/ X
          </span>
        </FormRowVertical>
        <FormRowVertical label={"Reactions taken"}>
          <span>
            <Input
              type="number"
              size="small"
              customStyles={css`
                width: 5em;
              `}
            />
            &nbsp;/ X
          </span>
        </FormRowVertical>
        <FormRowVertical label={"Attacks made"}>
          <span>
            <Input
              type="number"
              size="small"
              customStyles={css`
                width: 5em;
              `}
            />
            &nbsp;/ X
          </span>
        </FormRowVertical>
      </Cell>
      <Cell>
        <span>Concentrates on: powerName</span>
        <Button
          size="small"
          customStyles={css`
            height: 50px;
          `}
        >
          Drop concentration
        </Button>
      </Cell>
      <Cell>
        <FormRowVertical label={"Movement used"}>
          <span>
            <Input type="number" size="small" customStyles={{ width: "5em" }} />
            &nbsp;/ X
          </span>
        </FormRowVertical>
        <Button
          size="small"
          customStyles={css`
            height: 50px;
          `}
        >
          Update
        </Button>
      </Cell>
      <Cell>
        <Button
          size="small"
          customStyles={css`
            height: 50px;
          `}
        >
          Weapon attack
        </Button>
        <Button
          size="small"
          customStyles={css`
            height: 50px;
          `}
        >
          Use power
        </Button>
        <Button
          size="small"
          customStyles={css`
            height: 50px;
          `}
          onClick={() => dispatch({ type: "CHANGE_MODE", payload: "Movement" })}
        >
          Plot movement
        </Button>
        <Button
          size="small"
          customStyles={css`
            height: 50px;
          `}
          onClick={() => dispatch({ type: "CHANGE_MODE", payload: "Idle" })}
        >
          Idle
        </Button>
        <Button
          size="small"
          customStyles={css`
            height: 50px;
          `}
        >
          Next turn
        </Button>
        {isPending && <Spinner />}
        {!isPending && (
          <Button
            size="small"
            customStyles={css`
              height: 50px;
            `}
            onClick={() => rollInitiative()}
          >
            Roll initiative
          </Button>
        )}
      </Cell>
    </Container>
  );
}

const Container = styled(Box)`
  border-radius: 0;
  width: 100%;
  height: 1fr;
  display: grid;
  grid-template-columns: auto auto;
  grid-template-rows: auto auto;
  overflow: auto;
  gap: 5px;
`;

const Cell = styled.div`
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  gap: 10px;
  align-items: center;
`;
