import styled from "styled-components";
import Heading from "../text/Heading";
import { Skill } from "../../models/skill";
import SkillProficiencyBox from "./SkillProficiencyBox";

const StyledProficiencyBox = styled.div`
  background-color: rgba(var(--color-secondary-background-rgb), 0.05);
  border: 1px solid var(--color-border);
  padding: 1rem 1rem;
  width: auto;
  height: 100%;
  border-radius: var(--border-radius-lg);
`;



export default function SkillProficiencyContainer({
  data,
  header,
}: {
  data: Skill[];
  header: string;
}) {
  return (
    <StyledProficiencyBox>
      <Heading as="h3">{header}</Heading>
      {data?.map((item) => (
        <SkillProficiencyBox item={item}></SkillProficiencyBox>
      ))}
    </StyledProficiencyBox>
  );
}
