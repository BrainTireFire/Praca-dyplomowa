import { useRef, useState } from "react";
import Box from "../../ui/containers/Box";
import Heading from "../../ui/text/Heading";
import { MemberSelect } from "../../ui/interactive/MemberSelect";
import Input from "../../ui/forms/Input";
import Button from "../../ui/interactive/Button";
import styled from "styled-components";
import { CharacterItem } from "../../models/character";
import { useUpdateXP } from "../characters/hooks/useUpdateCharacter";
import Spinner from "../../ui/interactive/Spinner";
import { useParams } from "react-router-dom";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  align-content: center;
  gap: 20px;
`;

function GiveXP({ membersList }: { membersList: CharacterItem[] }) {
  const { campaignId } = useParams<{ campaignId: string }>();
  const [members, setMembers] = useState(membersList);
  const [selectedMembers, setSelectedMembers] = useState<number[]>([]);
  const [inputXP, setInputXP] = useState<number>(0);
  const { isPending, updateXP } = useUpdateXP(Number(campaignId) || 0);

  if (isPending) return <Spinner />;

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    let value = parseInt(e.target.value);
    if (isNaN(value)) {
      value = 0;
    } else {
      value = value < 1 ? 0 : value;
    }

    setInputXP(value);
  };

  const handleClick = () => {
    setMembers((previous) => {
      return previous.map((member) => {
        if (selectedMembers.includes(member.id)) {
          const updatedXP = member.xp + inputXP;
          updateXP({ characterId: member.id, xp: updatedXP });
          return { ...member, xp: updatedXP };
        }
        return member;
      });
    });
  };

  return (
    <Container>
      <Heading as="h4">Give experience points</Heading>
      <Box>
        <p>Select Members:</p>
        {members.map((e) => (
          <MemberSelect
            setSelectedMembers={setSelectedMembers}
            member={e}
            key={e.id}
            type="xp"
            selected={!!selectedMembers.find((x) => x === e.id)}
          ></MemberSelect>
        ))}
      </Box>
      <Heading as="h1">Amount of XP</Heading>
      <Input
        type="number"
        placeholder="Type amount of XP"
        onChange={handleChange}
      ></Input>
      <Button onClick={handleClick}>Give Points</Button>
    </Container>
  );
}

export default GiveXP;
