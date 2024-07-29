import styled from "styled-components";
import Box from "../../ui/containers/Box";
import Heading from "../../ui/text/Heading";
import Button from "../../ui/interactive/Button";
import { MemberSelect } from "../../ui/interactive/MemberSelect";
import { useState } from "react";

const Container = styled.div`
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  grid-gap: 15px;
  justify-content: center;
`;

export default function ShortRest({ membersList }) {
  const [members, setMembers] = useState(membersList);
  const [selectedMembers, setSelectedMembers] = useState<number[]>([]);

  const handleClick = () => {
    setMembers((previous) =>
      previous.map((member) =>
        selectedMembers.includes(member.id) ? { ...member, rest: true } : member
      )
    );
  };

  return (
    <Container>
      <Heading as="h4" style={{ gridColumn: "1/3" }}>
        Short rest
      </Heading>
      <Box style={{ gridColumn: "1/2", gridRow: "2/5" }}>
        <p style={{ gridColumn: "1/2", marginBottom: "10px" }}>
          Select Members:
        </p>
        {members.map((e) => (
          <MemberSelect
            setSelectedMembers={setSelectedMembers}
            member={e}
            key={e.id}
            type="rest"
          ></MemberSelect>
        ))}
      </Box>
      <div
        style={{
          gridRow: "2/4",
          display: "flex",
          flexDirection: "column",
        }}
      >
        <Heading as="h2" style={{ gridColumn: "2/3", marginBottom: "15px" }}>
          Pick strength dice
        </Heading>
        <Button style={{ gridColumn: "2/3" }}>Send notification</Button>
      </div>
      <Button
        onClick={handleClick}
        size="small"
        style={{ gridColumn: "2/3", gridRow: "4/5" }}
      >
        Rest
      </Button>
    </Container>
  );
}
