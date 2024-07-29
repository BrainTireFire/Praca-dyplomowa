import { useRef, useState } from "react";
import Box from "../../ui/containers/Box";
import Heading from "../../ui/text/Heading";
import { MemberSelect } from "../../ui/interactive/MemberSelect";
import Input from "../../ui/forms/Input";
import Button from "../../ui/interactive/Button";
import styled from "styled-components";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  align-content: center;
  gap: 20px;
`;

function GiveXP({ membersList }) {
  const [members, setMembers] = useState(membersList);
  const [selectedMembers, setSelectedMembers] = useState<number[]>([]);
  const inputXPRef = useRef(0);

  const handleClick = () => {
    const inputXP = Number(inputXPRef.current.value);
    setMembers((previous) =>
      previous.map((member) =>
        selectedMembers.includes(member.id)
          ? { ...member, xp: member.xp + inputXP }
          : member
      )
    );
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
          ></MemberSelect>
        ))}
      </Box>
      <Heading as="h1">Amount of XP</Heading>
      <Input ref={inputXPRef} placeholder="Type amount of XP"></Input>
      <Button onClick={handleClick}>Give Points</Button>
    </Container>
  );
}

export default GiveXP;
