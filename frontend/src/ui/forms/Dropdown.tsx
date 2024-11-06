import styled from "styled-components";

const Select = styled.select`
  padding: 8px 12px;
  border: 1px solid var(--color-border);
  border-radius: 4px;
  background-color: var(--color-main-background);
  color: var(--color-secondary-text);
  font-size: 16px;
  outline: none;
  transition: border-color 0.3s, box-shadow 0.3s;

  &:focus {
    border-color: var(--color-input-focus);
    box-shadow: 0 0 5px var(--color-input-focus);
  }

  option {
    background-color: var(--color-main-background);
    color: var(--color-secondary-text);
  }
`;

const StyledRow = styled.div`
  display: flex;
  align-items: center;
  gap: 12px;
`;

function Dropdown({
  valuesList,
  chosenValue,
  setChosenValue,
}: {
  valuesList: readonly { value: string; label: string }[];
  chosenValue: string;
  setChosenValue: (value: string) => void;
}) {
  return (
    <StyledRow>
      <Select
        onChange={(e) => setChosenValue(e.target.value)}
        value={chosenValue}
      >
        <option value={0} disabled>
          Select value
        </option>
        {valuesList.map((element) => {
          return (
            <option key={element.value} value={element.value}>
              {element.label}
            </option>
          );
        })}
      </Select>
    </StyledRow>
  );
}

export default Dropdown;
