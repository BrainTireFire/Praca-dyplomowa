import { useContext } from "react";
import { createContext } from "react";
import styled from "styled-components";

const StyledTable = styled.div`
  border: 1px solid var(--color-button-primary);

  font-size: 1.4rem;
  background-color: var(--color-grey-0);
  border-radius: 7px;
  overflow: hidden;
`;

const StyledHeaderWithButton = styled.div`
  display: flex;
  justify-content: space-between;
  border-bottom: 1px solid var(--color-button-primary);
`;

const CommonRow = styled.div`
  display: grid;
  grid-template-columns: ${(props) => props.columns};

  column-gap: 2.4rem;
  align-items: center;
  transition: none;
`;

const StyledHeader = styled(CommonRow)`
  padding: 1.6rem 2.4rem;

  background-color: var(--color-grey-50);
  border-bottom: 1px solid var(--color-button-primary);
  text-transform: uppercase;
  letter-spacing: 0.4px;
  font-weight: 600;
  color: var(--color-grey-600);
`;

const StyledRow = styled(CommonRow)`
  padding: 1.2rem 2.4rem;

  &:not(:last-child) {
    border-bottom: 1px solid var(--color-button-primary);
  }
`;

const StyledBody = styled.section`
  margin: 0.4rem 0;
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
  padding: 1rem;
  font-size: 1.6rem;
  font-weight: bold;
  text-align: left;
`;

const TableButton = styled.button`
  background-color: var(--color-button-primary);
  color: var(--color-grey-0);
  font-size: 1.6rem;
  font-weight: bold;
  padding: 1rem;
  cursor: pointer;
  width: 25%;
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
