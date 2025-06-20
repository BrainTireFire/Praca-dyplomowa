import styled from "styled-components";
import Heading from "../../ui/text/Heading";
import Button from "../../ui/interactive/Button";
import { useEffect, useState } from "react";
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from "@microsoft/signalr";
import { BASE_URL } from "../../services/constAPI";
import { useParams } from "react-router-dom";
import { useGetHitDice } from "./hooks/useGetHitDice";
import Spinner from "../../ui/interactive/Spinner";
import { DiceSet } from "../../models/diceset";
import toast from "react-hot-toast";

const Container = styled.div`
  display: grid;
  grid-template-columns: repeat(6, 1fr);
  grid-gap: 5px;
  justify-content: center;
`;

export default function ShortRestModalCharacter({
  onCloseModal,
}: {
  onCloseModal: () => void;
}) {
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const { campaignId } = useParams();
  const { hitDice, isLoading } = useGetHitDice(Number(campaignId));
  const [hitDiceLocal, setHitDiceLocal] = useState<DiceSet>({
    d20: 0,
    d12: 0,
    d10: 0,
    d8: 0,
    d6: 0,
    d4: 0,
    d100: 0,
    flat: 0,
  });

  const updateDice = (field: keyof DiceSet, delta: number) => {
    setHitDiceLocal((prev) => ({
      ...prev,
      [field]: (prev[field] ?? 0) + delta,
    }));
  };

  const isButtonDisabled = (field: keyof DiceSet, delta: number): boolean => {
    const current = hitDiceLocal[field] ?? 0;
    const max = hitDice![field] ?? 0;

    const newValue = current + delta;

    return newValue < 0 || newValue > max;
  };

  const diceFields: (keyof DiceSet)[] = ["d12", "d10", "d8", "d6", "d4"];

  useEffect(() => {
    const hubConnection = new HubConnectionBuilder()
      .withUrl(`${BASE_URL}/shortRest?campaignId=${campaignId}`)
      .configureLogging(LogLevel.Information)
      .withAutomaticReconnect()
      .build();

    hubConnection
      .start()
      .then(() => {
        setConnection(hubConnection);
      })
      .catch((error) => toast.error("Error connecting to short rest hub: "));

    return () => {
      if (hubConnection) {
        hubConnection.stop().then(() => {});
      }
    };
  }, [campaignId]);

  const handleSendHitDice = async (e: React.FormEvent) => {
    e.preventDefault();
    if (connection) {
      try {
        await connection
          .invoke("ShortRestDiceSelected", Number(campaignId), hitDiceLocal)
          .catch((err) => toast.error("Error sending hit dice: "));
        toast.success("Hit dice sent to DM");
        onCloseModal();
      } catch (error) {
        toast.error("Error sending hit dice");
      }
    }
  };

  if (isLoading) {
    return <Spinner></Spinner>;
  }
  return (
    <div style={{ display: "flex", flexDirection: "column" }}>
      <Heading as="h2">Select hit dice</Heading>
      <Container>
        {diceFields.map((field) => (
          <div key={field} style={{ display: "contents" }}>
            <span style={{ textAlign: "center" }}>{field}:</span>
            <Button
              size="small"
              onClick={() => updateDice(field, -1)}
              disabled={isButtonDisabled(field, -1)}
            >
              -
            </Button>
            <span style={{ textAlign: "center" }}>{hitDiceLocal[field]}</span>
            <Button
              size="small"
              onClick={() => updateDice(field, +1)}
              disabled={isButtonDisabled(field, +1)}
            >
              +
            </Button>
            <span style={{ textAlign: "center" }}>/</span>
            <span style={{ textAlign: "center" }}>{hitDice![field]}</span>
          </div>
        ))}
      </Container>
      <Button style={{ marginTop: "5px" }} onClick={handleSendHitDice}>
        Confirm
      </Button>
    </div>
  );
}
