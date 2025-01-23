import React, { useState } from "react";
import { FaSort } from "react-icons/fa";
import styled, { css } from "styled-components";
import Button from "../interactive/Button";
import Input from "../forms/Input";

const TableWithFooterContainer = styled.div<{
  customTableContainer?: ReturnType<typeof css>;
}>`
  width: 100%; /* Adjust based on your layout */
  max-height: 100%; /* Set a maximum height for the table */
  height: 100%; /* Set a maximum height for the table */
  display: flex;
  flex-direction: column;
  overflow-y: hidden;
  padding: 10px;
  ${({ customTableContainer }) => customTableContainer && customTableContainer};
`;
const TableContainer = styled.div`
  width: 100%; /* Adjust based on your layout */
  flex: 1;
  border-right: 1px solid var(--color-border);
  border-left: 1px solid var(--color-border);
  border-top: 1px solid var(--color-border);
  border-bottom: 1px solid var(--color-border);

  overflow-y: auto; /* Enable vertical scrolling */
  scrollbar-color: var(--color-button-primary) var(--color-main-background);
  scrollbar-width: thin;

  &::-webkit-scrollbar {
    width: 8px;
  }

  &::-webkit-scrollbar-track {
    background: var(--color-main-background);
  }

  &::-webkit-scrollbar-thumb {
    background-color: var(--color-button-primary);
    border-radius: 4px;
  }
`;

const StyledTable = styled.table`
  width: 100%;
  border-collapse: separate;
  border-spacing: 0;
  table-layout: auto; /* Allows columns to adjust based on content */
  /* border: 1px solid var(--color-border); */
`;

const TableHeader = styled.thead`
  background-color: var(--color-main-background);
  position: sticky; /* Keeps the header visible during scrolling */
  top: 0;
  z-index: 1;
  border-bottom: 2px solid var(--color-border);
  font-weight: bold;
`;

const TableHeaderRow = styled.tr`
  border: 1px solid var(--color-border);
`;
const TableRow = styled.tr<{ isSelected: boolean }>`
  cursor: pointer;
  border: 1px solid var(--color-border);
  background-color: ${({ isSelected }) =>
    isSelected
      ? "var(--color-header-text-hover)"
      : "var(--color-main-background)"};
  transition: background-color 0.3s;

  &:hover {
    background-color: var(--color-header-text);
  }
`;

const TableCell = styled.td`
  padding: 8px;
  border-bottom: 1px solid var(--color-border);
  border-right: 1px solid var(--color-border);
  text-align: left;
`;
const TableHeaderCell = styled.td<{ customHeader?: ReturnType<typeof css> }>`
  padding: 8px;
  text-align: left;
  border-bottom: 2px solid var(--color-border);
  border-right: 2px solid var(--color-border);

  ${({ customHeader }) => customHeader && customHeader}
`;
const TableHeaderCellClickable = styled(TableHeaderCell)`
  cursor: pointer;
  &:hover {
    color: var(--color-button-primary);
  }
`;

const TableFooter = styled.tfoot`
  font-weight: bold;
  width: 100%;
  padding: 8px;
  text-align: left;
  display: block;
  border: 1px solid var(--color-border);
`;

const SortIcon = styled.span`
  margin-left: 5px;
  font-size: 0.8em;
`;

type ReusableTableProps = {
  tableRowsColomns: { [key: string]: string };
  mainHeader?: string;
  data: { id: string | number; [key: string]: any }[];
  // selected?: number | null;
  onSelect?: React.Dispatch<React.SetStateAction<any>>;
  isSelectable?: boolean;
  isSearching?: boolean;
  customTableContainer?: ReturnType<typeof css>;
  customHeader?: ReturnType<typeof css>;
  isMultiSelect?: boolean;
  handleMultiSelectionChange?: (id: number | string) => void;
};

const FilterInput = styled.input`
  width: 100%;
  padding: 8px;
  border: 1px solid var(--color-border);
  border-radius: 4px;
`;

const TableBody = styled.tbody``;

export const ReusableTable = ({
  tableRowsColomns,
  mainHeader,
  data,
  onSelect,
  isSelectable,
  isSearching,
  customTableContainer,
  customHeader,
  isMultiSelect,
  handleMultiSelectionChange,
}: ReusableTableProps) => {
  const [sortConfig, setSortConfig] = useState<{
    key: string;
    direction: "asc" | "desc";
  } | null>(null);
  const [filterText, setFilterText] = useState("");
  const [selected, setSelected] = useState<number | null>(null);

  const sortedData = [...data].sort((a, b) => {
    if (sortConfig) {
      const { key, direction } = sortConfig;
      if (a[key] < b[key]) return direction === "asc" ? -1 : 1;
      if (a[key] > b[key]) return direction === "asc" ? 1 : -1;
    }
    return 0;
  });

  const visibleColumns = Object.values(tableRowsColomns);
  const headers = Object.keys(tableRowsColomns);

  const filteredData = sortedData.filter((item) =>
    visibleColumns.some((col) =>
      String(item[col]).toLowerCase().includes(filterText.toLowerCase())
    )
  );

  const handleRowClick = (index: number) => {
    if (setSelected && isSelectable) {
      setSelected(index);
      onSelect && onSelect(filteredData[index]);
    }
  };

  const handleSort = (headerIndex: number) => {
    const key = visibleColumns[headerIndex];
    let direction: "asc" | "desc" = "asc";
    if (sortConfig?.key === key && sortConfig.direction === "asc") {
      direction = "desc";
    }

    setSortConfig({ key, direction });
    setSelected(null);
  };

  const handleSetFilterText = (text: string) => {
    setFilterText(text);
    setSelected(null);
  };

  return (
    <TableWithFooterContainer customTableContainer={customTableContainer}>
      <TableContainer>
        <StyledTable>
          <TableHeader>
            {mainHeader && (
              <TableHeaderRow>
                <TableHeaderCell colSpan={3} customHeader={customHeader}>
                  {mainHeader}
                </TableHeaderCell>
              </TableHeaderRow>
            )}
            <TableHeaderRow>
              {headers.map((header, index) => (
                <TableHeaderCellClickable
                  key={index}
                  onClick={() => handleSort(index)}
                  customHeader={customHeader}
                >
                  {header}
                  <SortIcon>
                    <FaSort />
                  </SortIcon>
                </TableHeaderCellClickable>
              ))}
              {isMultiSelect && <TableHeaderCell></TableHeaderCell>}
            </TableHeaderRow>
          </TableHeader>
          <TableBody>
            {filteredData.map((item, index) => (
              <TableRow
                key={item.id}
                isSelected={selected === index}
                onClick={() => handleRowClick(index)}
              >
                {visibleColumns.map((col, colIndex) => (
                  <TableCell key={colIndex}>{item[col]}</TableCell>
                ))}
                {isMultiSelect && handleMultiSelectionChange && (
                  <TableCell key={"button"}>
                    <Input
                      type="checkbox"
                      checked={item.selected}
                      onChange={() => handleMultiSelectionChange(item.itemId)}
                    ></Input>
                  </TableCell>
                )}
              </TableRow>
            ))}
          </TableBody>
        </StyledTable>
      </TableContainer>
      {isSearching && (
        <TableFooter>
          <FilterInput
            type="text"
            placeholder="Search..."
            value={filterText}
            onChange={(e) => handleSetFilterText(e.target.value)}
          />
        </TableFooter>
      )}
    </TableWithFooterContainer>
  );
};
