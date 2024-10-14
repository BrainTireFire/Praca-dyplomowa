import { useContext } from "react";
import { createContext } from "react";
import styled from "styled-components";
import Button from "../interactive/Button";

const StyledTableContainer = styled.div`
  border: 1px solid var(--color-border);

  font-size: 1rem;
  background-color: var(--color-main-background);
  border-radius: 7px;
  max-height: 100%;
  display: flex;
  flex-direction: column;
`;

const StyledHeaderWithButton = styled.div`
  display: flex;
  justify-content: space-between;
  border-bottom: 1px solid var(--color-border);
`;

const CommonRow = styled.div`
  display: contents;
`;

const CommonTable = styled.div`
  display: grid;
  grid-template-columns: ${(props) => props.columns};
  column-gap: 2.4rem;
  font-size: 1rem;
  align-items: center;
  transition: none;
  overflow-y: auto;
  max-height: 100%;
  scrollbar-color: var(--color-button-primary) var(--color-main-background);
  scrollbar-width: thin;
  scrollbar-gutter: stable;
`;

const StyledHeader = styled(CommonRow)`
  // none of these CSS properties will work due to CommonRow having display: contents
  padding: 0.2rem 2.4rem;

  background-color: var(--color-main-background);
  border-bottom: 1px solid var(--color-border);
  text-transform: uppercase;
  letter-spacing: 0.4px;
  font-weight: 600;
  color: var(--color-secondary-text);
`;

const StyledRow = styled(CommonRow)`
  padding: 0rem 0.9rem; //useless due to common row having display: contents
  font-size: 1rem; //useless due to common row having display: contents

  &:not(:last-child) {
    border-bottom: 1px solid var(--color-border);
  }
`;

const StyledBody = styled.section`
  margin: 0rem 0;
  display: contents;
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
  grid-column: 1 / ${(props) => props.width + 1};
`;

const TableHeader = styled.div`
  padding: 0.5rem;
  font-size: 1.2rem;
  font-weight: bold;
  text-align: left;
`;

const TableButton = styled(Button)`
  background-color: var(--color-button-primary);
  color: var(--color-secondary-text);
  font-size: 1rem;
  font-weight: bold;
  padding: 0rem 0.5rem;
  margin: 0.2rem 0.2rem;
  cursor: pointer;
  width: auto;
  border-radius: 0.7rem;
`;

type TableProps = {
  header?: string;
  button?: string;
  columns: string;
  children: React.ReactNode;
};

function Table({ header, columns, children, button }: TableProps) {
  return (
    <StyledTableContainer role="table">
      {header && (
        <StyledHeaderWithButton>
          <TableHeader>{header}</TableHeader>
          {button && <TableButton>{button}</TableButton>}
        </StyledHeaderWithButton>
      )}
      <CommonTable columns={columns}>{children}</CommonTable>
    </StyledTableContainer>
  );
}

function Header({ children }: { children: React.ReactNode }) {
  return (
    <StyledHeader role="row" as="header">
      {children}
    </StyledHeader>
  );
}

function Row({ children }: { children: React.ReactNode }) {
  return <StyledRow role="row">{children}</StyledRow>;
}

function Body({
  data,
  columnCount,
  render,
}: {
  data: any[];
  columnCount: Number;
  render: (item: any) => React.ReactNode;
}) {
  if (!data.length) {
    return <Empty width={columnCount}>No data to show at the moment</Empty>;
  }

  return <StyledBody>{data.map(render)}</StyledBody>;
}

Table.Header = Header;
Table.Row = Row;
Table.Body = Body;
Table.Footer = Footer;

export default Table;
