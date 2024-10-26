import { useState } from "react";
import styled from "styled-components";
import { FaSort } from "react-icons/fa";

const TableContainer = styled.div`
  margin: 20px;
  border: 1px solid var(--color-border);
`;

const StyledTable = styled.table`
  width: 100%;
  border-collapse: collapse;
  display: block;
`;

const TableHeader = styled.th`
  background-color: var(--color-main-background);
  padding: 10px;
  text-align: left;
  border-bottom: 2px solid var(--color-border);
  cursor: pointer;
  position: relative;

  &:hover {
    color: var(--color-button-primary);
  }
`;

const TableHeaderLabel = styled.th`
  background-color: var(--color-main-background);
  padding: 10px;
  text-align: left;
  border-bottom: 2px solid var(--color-border);
`;

const SortIcon = styled.span`
  margin-left: 5px;
  font-size: 0.8em;
`;

const TableRow = styled.tr<{ isSelected: boolean }>`
  cursor: pointer;
  background-color: ${({ isSelected }) =>
    isSelected
      ? "var(--color-header-text-hover)"
      : "var(--color-main-background)"};
  transition: background-color 0.3s;

  &:hover {
    background-color: var(--color-header-text);
  }

  display: table;
  width: 100%;
  table-layout: fixed;
`;

const TableHead = styled.thead``;

const TableRowHead = styled.tr`
  display: table;
  width: 100%;
  table-layout: fixed;
`;

const TableBody = styled.tbody`
  display: block;
  max-height: 300px;
  overflow-y: auto;

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

const TableCell = styled.td`
  padding: 10px;
  border-bottom: 1px solid var(--color-border);
`;

const TableFooter = styled.tfoot`
  background-color: var(--color-main-background);
  padding: 10px;
`;

const FooterRow = styled.tr`
  display: table;
  width: 100%;
  table-layout: fixed;
`;

const FilterInput = styled.input`
  width: 100%;
  padding: 8px;
  border: 1px solid var(--color-border);
  border-radius: 4px;
`;

type ReusableTableProps = {
  tableRowsColomns: { [key: string]: string };
  mainHeader?: string;
  data: { id: string | number; [key: string]: any }[];
  // selected?: number | null;
  onSelect?: React.Dispatch<React.SetStateAction<any>>;
  isSelectable?: boolean;
  isSearching?: boolean;
};

export const ReusableTable = ({
  tableRowsColomns,
  mainHeader,
  data,
  onSelect,
  isSelectable,
  isSearching,
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
      onSelect && onSelect(data[index]);
    }
  };

  const handleSort = (headerIndex: number) => {
    const key = visibleColumns[headerIndex];
    let direction: "asc" | "desc" = "asc";
    if (sortConfig?.key === key && sortConfig.direction === "asc") {
      direction = "desc";
    }

    setSortConfig({ key, direction });
  };

  return (
    <>
      <TableContainer>
        <StyledTable>
          {mainHeader && (
            <TableHead>
              <TableHeaderLabel>{mainHeader}</TableHeaderLabel>
            </TableHead>
          )}

          <TableHead>
            <TableRowHead>
              {headers.map((header, index) => (
                <TableHeader key={index} onClick={() => handleSort(index)}>
                  {header}
                  <SortIcon>
                    <FaSort />
                  </SortIcon>
                </TableHeader>
              ))}
            </TableRowHead>
          </TableHead>

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
              </TableRow>
            ))}
          </TableBody>

          {isSearching && (
            <TableFooter>
              <FooterRow>
                <TableCell colSpan={headers.length}>
                  <FilterInput
                    type="text"
                    placeholder="Search..."
                    value={filterText}
                    onChange={(e) => setFilterText(e.target.value)}
                  />
                </TableCell>
              </FooterRow>
            </TableFooter>
          )}
        </StyledTable>
      </TableContainer>
    </>
  );
};
