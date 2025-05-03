import styled from "styled-components";
import Heading from "../text/Heading";
import { SavingThrow } from "../../models/savingthrow";
import SavingThrowProficiencyBox from "./SavingThrowProficiencyBox";

const StyledProficiencyBox = styled.div`
  background-color: rgba(var(--color-secondary-background-rgb), 0.05);
  border: 1px solid var(--color-border);
  padding: 1rem 1rem;
  width: auto;
  height: 100%;
  border-radius: var(--border-radius-lg);
`;



export default function SavingThrowProficiencyContainer({
  data,
  header,
}: {
  data: SavingThrow[];
  header: string;
}) {
  return (
    <StyledProficiencyBox>
      <Heading as="h3">{header}</Heading>
      {data?.map((item) => (
        <SavingThrowProficiencyBox item={item}></SavingThrowProficiencyBox>
      ))}
    </StyledProficiencyBox>
  );
}
