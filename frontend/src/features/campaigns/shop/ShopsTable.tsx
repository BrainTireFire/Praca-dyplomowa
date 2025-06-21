import { useNavigate } from "react-router-dom";
import styled from "styled-components";
import Button from "../../../ui/interactive/Button";
import { HiWrench, HiXMark } from "react-icons/hi2";
import { Shop } from "../../../models/shop";
import { useLocation } from "react-router-dom";

const Table = styled.table`
  width: 90%;
  border-radius: var(--border-radius-md);
  border-spacing: 0;
  table-layout: fixed;
  border: 1px solid var(--color-border);
  background-color: rgba(var(--color-secondary-background-rgb), 0.05);
  overflow: clip;
`;

const Th = styled.th`
  padding: 10px;
  text-align: center;
`;

const Tr = styled.tr`
  transition: background-color 100ms ease;
  cursor: pointer;

  &:hover {
    background-color: rgba(116, 177, 116, 0.5);
  }
`;

const Td = styled.td`
  padding: 8px;
  text-align: center;
  border-top: 1px solid var(--color-border);
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
`;

const ActionTd = styled.td`
  padding: 8px 16px;
  border-top: 1px solid var(--color-border);
  display: flex;
  justify-content: flex-end;
  gap: 10px;
`;

export default function ShopsTable({
  shops,
  onRemove,
}: {
  shops: Shop[];
  onRemove: Function;
}) {
  const location = useLocation();
  const isGameMaster = location.state?.isGameMaster;
  const navigate = useNavigate();
  return (
    <Table>
      <thead>
        <tr>
          <Th style={{ width: "20%" }}>Name</Th>
          <Th style={{ width: "20%" }}>Type</Th>
          <Th style={{ width: "20%" }}>Location</Th>
          <Th style={{ width: "30%" }}>Description</Th>
          {isGameMaster && <Th style={{ width: "10%" }}></Th>}
        </tr>
      </thead>
      <tbody>
        {shops.map((shop: Shop, index: number) => (
          <Tr onClick={() => navigate(`${shop.id}`)} key={index}>
            <Td>{shop.name}</Td>
            <Td>{shop.type}</Td>
            <Td>{shop.location}</Td>
            <Td>{shop.description}</Td>
            {isGameMaster && (
              <ActionTd onClick={(e) => e.stopPropagation()}>
                <Button onClick={() => navigate(`edit/${shop.id}`)}>
                  <HiWrench />
                </Button>
                <Button onClick={() => onRemove(shop.id)}>
                  <HiXMark />
                </Button>
              </ActionTd>
            )}
          </Tr>
        ))}
      </tbody>
    </Table>
  );
}
