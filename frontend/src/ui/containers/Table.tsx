import { useContext } from "react";
import { createContext } from "react";
import styled from "styled-components";
import Button from "../interactive/Button";

const StyledTable = styled.div`
  border: 1px solid var(--color-border);

  font-size: 1rem;
  background-color: var(--color-grey-0);
  border-radius: 7px;
  overflow: hidden;
`;

const StyledHeaderWithButton = styled.div`
  display: flex;
  justify-content: space-between;
  border-bottom: 1px solid var(--color-border);
`;

const CommonRow = styled.div`
  display: grid;
  grid-template-columns: ${(props) => props.columns};

  column-gap: 2.4rem;
  font-size: 1rem;
  align-items: center;
  transition: none;
`;

const StyledHeader = styled(CommonRow)`
  padding: 0.2rem 2.4rem;

  background-color: var(--color-grey-50);
  border-bottom: 1px solid var(--color-border);
  text-transform: uppercase;
  letter-spacing: 0.4px;
  font-weight: 600;
  color: var(--color-grey-600);
`;

const StyledRow = styled(CommonRow)`
  padding: 0rem 0.9rem;
  font-size: 1rem;

  &:not(:last-child) {
    border-bottom: 1px solid var(--color-border);
  }
`;

const StyledBody = styled.section`
  margin: 0rem 0;
`;

const Footer = styled.footer`
  background-color: var(--color-button-primary);
  display: flex;
  justify-content: center;
  padding: 1.2rem;

  /* This will hide the footer when it contains no child elements. Possible thanks to the parent selector :has 🎉 */
  &:not(:has(*)) {
    display: none;
  }
`;

const Empty = styled.p`
  font-size: 1.6rem;
  font-weight: 500;
  text-align: center;
  margin: 2.4rem;
`;

const TableHeader = styled.div`
  padding: 0.5rem;
  font-size: 1.2rem;
  font-weight: bold;
  text-align: left;
`;

// const TableButton = styled.button`
//   background-color: var(--color-button-primary);
//   color: var(--color-grey-0);
//   font-size: 1rem;
//   font-weight: bold;
//   padding: 0rem 0.5rem;
//   margin: 0.2rem 0.2rem;
//   cursor: pointer;
//   width: auto;
//   border-radius: 0.7rem;
// `;

const TableButton = styled(Button)`
  background-color: var(--color-button-primary);
  color: var(--color-grey-0);
  font-size: 1rem;
  font-weight: bold;
  padding: 0rem 0.5rem;
  margin: 0.2rem 0.2rem;
  cursor: pointer;
  width: auto;
  border-radius: 0.7rem;
`;

const TableContext = createContext({ columns: "" });

type TableProps = {
  header?: string;
  button?: string;
  columns: string;
  children: React.ReactNode;
};

function Table({ header, columns, children, button }: TableProps) {
  return (
    <TableContext.Provider value={{ columns }}>
      <StyledTable role="table">
        {header && (
          <StyledHeaderWithButton>
            <TableHeader>{header}</TableHeader>
            {button && <TableButton>{button}</TableButton>}
          </StyledHeaderWithButton>
        )}
        {children}
      </StyledTable>
    </TableContext.Provider>
  );
}

function Header({ children }: { children: React.ReactNode }) {
  const { columns } = useContext(TableContext);

  return (
    <StyledHeader role="row" columns={columns} as="header">
      {children}
    </StyledHeader>
  );
}

function Row({ children }: { children: React.ReactNode }) {
  const { columns } = useContext(TableContext);

  return (
    <StyledRow role="row" columns={columns}>
      {children}
    </StyledRow>
  );
}

function Body({
  data,
  render,
}: {
  data: any[];
  render: (item: any) => React.ReactNode;
}) {
  if (!data.length) {
    return <Empty>No data to show at the moment</Empty>;
  }

  return <StyledBody>{data.map(render)}</StyledBody>;
}

Table.Header = Header;
Table.Row = Row;
Table.Body = Body;
Table.Footer = Footer;

export default Table;
