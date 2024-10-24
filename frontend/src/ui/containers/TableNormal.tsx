import { useState } from "react";
import styled from "styled-components";

const TableContainer = styled.div`
  overflow-x: auto;
  max-height: 350px;
  margin: 20px;
  border: 1px solid var(--color-border);

  scrollbar-color: var(--color-button-primary) var(--color-main-background);
  scrollbar-width: thin;
  scrollbar-gutter: stable;
`;

const StyledTable = styled.table`
  width: 100%;
  border-collapse: collapse;
`;

const TableHeader = styled.th`
  background-color: var(--color-main-background);
  padding: 10px;
  text-align: left;
  border-bottom: 2px solid var(--color-border);
`;

const TableRow = styled.tr`
  cursor: pointer;
  background-color: ${({ isSelected }) => (isSelected ? "#9CC5A1" : "#1F2421")};
  transition: background-color 0.3s;

  &:hover {
    background-color: var(--color-header-text-hover);
  }
`;

const TableCell = styled.td`
  padding: 10px;
  border-bottom: 1px solid var(--color-border);
`;

export const TableNormal = ({ data, selected, setSelected }) => {
  const handleRowClick = (index) => {
    setSelected(index);
  };

  return (
    <TableContainer>
      <StyledTable>
        <thead>
          <tr>
            <TableHeader>Name</TableHeader>
            <TableHeader>Size</TableHeader>
            <TableHeader>Created at</TableHeader>
          </tr>
        </thead>
        <tbody>
          {data.map((item, index) => (
            <TableRow
              key={item.id}
              isSelected={selected === index}
              onClick={() => handleRowClick(index)}
            >
              <TableCell>{item.name}</TableCell>
              <TableCell>{item.sizeX + " x " + item.sizeY}</TableCell>
              <TableCell></TableCell>
            </TableRow>
          ))}
        </tbody>
      </StyledTable>
    </TableContainer>
  );
};
