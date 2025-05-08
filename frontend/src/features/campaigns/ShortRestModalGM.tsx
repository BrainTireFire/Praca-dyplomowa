import styled from "styled-components";
import Box from "../../ui/containers/Box";
import Heading from "../../ui/text/Heading";
import Button from "../../ui/interactive/Button";
import { MemberSelect } from "../../ui/interactive/MemberSelect";
import { useEffect, useState } from "react";
import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { useParams } from "react-router-dom";
import { BASE_URL } from "../../services/constAPI";
import { ShortRestDiceData } from "../../models/shortRest/shortRestDiceData";
import { CharacterItem } from "../../models/character";
import { DiceSet, DiceSetDefaultValue } from "../../models/diceset";
import { usePerformShortRest } from "./hooks/usePerformShortRest";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  gap: 15px;
  justify-content: center;
`;

export default function ShortRestModalGM({ membersList, onCloseModal } : {membersList: CharacterItem[], onCloseModal: () => void}) {
  const [members, setMembers] = useState(membersList);
  const [selectedMembers, setSelectedMembers] = useState<number[]>([]);


  const [connection, setConnection] = useState<HubConnection | null>(null);
  const { campaignId } = useParams();

  const handleSuccess = () => {
    setMembers((previous) =>
      previous.map((member) => {
          return { ...member, restData: {...DiceSetDefaultValue} }
      })
    );
    setSelectedMembers([]);
  };

  const {performShortRest, isPending} = usePerformShortRest(Number(campaignId), handleSuccess) 

  useEffect(() => {
        const hubConnection = new HubConnectionBuilder()
          .withUrl(
            `${BASE_URL}/shortRest?campaignId=${campaignId}`
          )
          .configureLogging(LogLevel.Information)
          .withAutomaticReconnect()
          .build();
  
        hubConnection
          .start()
          .then(() => {
            console.log("SignalR Short Rest Hub Connected.");
  
            hubConnection.on(
                        "ShortRestDiceSelectedToGm",
                        (
                          characterId: number,
                          diceSet: DiceSet
                        ) => {
                          console.log("ShortRestHitDieData received");
                          let character = members.find(x => x.id === characterId);
                          console.log(characterId);
                          if(!!character){
                            setMembers((previous) =>
                              previous.map((member) =>
                                member.id === characterId ? {...member, restData: diceSet} : member
                              )
                            );
                          }
                          
                        }
                      );

  
            setConnection(hubConnection);
          })
          .catch((error) => console.error("SignalR Connection Error: ", error));
  
        return () => {
          if (hubConnection) {
            hubConnection.stop().then(() => console.log("Connection stopped"));
          }
        };
    }, [campaignId]);




  const handleClick = () => {
    console.log(members);
    console.log(selectedMembers);
    const hitDiceMap: Record<number, DiceSet> = members
      .filter(character => selectedMembers.includes(character.id))
      .reduce((acc, character) => {
        acc[character.id] = character.restData;
        return acc;
      }, {} as Record<number, DiceSet>);
    performShortRest(hitDiceMap);
  };

  return (
    <Container>
      <Heading as="h2">
        Short rest
      </Heading>
      <Box style={{ gridRow: "2/5" }}>
        <p style={{ marginBottom: "10px" }}>
          Select Characters:
        </p>
        {members.map((e) => (
          <MemberSelect
            setSelectedMembers={setSelectedMembers}
            member={e}
            key={e.id}
            type="rest"
            selected={!!(selectedMembers.find(x => x === e.id))}
          ></MemberSelect>
        ))}
      </Box>
      <Button
        onClick={handleClick}
        size="medium"
        style={{ gridColumn: "2/3", gridRow: "4/5" }}
      >
        Rest
      </Button>
    </Container>
  );
}
