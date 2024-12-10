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
  valuesList: readonly { value: string | null; label: string }[];
  chosenValue: string | null;
  setChosenValue: (value: string | null) => void;
}) {
  let nullElement = valuesList.find((element) => element.value === null);
  console.log(chosenValue);
  return (
    <StyledRow>
      <Select
        onChange={(e) =>
          setChosenValue(e.target.value === "null" ? null : e.target.value)
        }
        value={chosenValue === null ? "null" : chosenValue}
      >
        <option value={"null"} disabled={nullElement ? false : true}>
          {nullElement ? nullElement.label : "Select value"}
        </option>
        {valuesList
          .filter((element) => element.value !== null)
          .map((element) => {
            return (
              <option key={element.value} value={element.value as string}>
                {element.label}
              </option>
            );
          })}
      </Select>
    </StyledRow>
  );
}

export default Dropdown;
