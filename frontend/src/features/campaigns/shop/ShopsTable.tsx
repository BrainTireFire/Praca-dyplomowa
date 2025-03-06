import { useNavigate } from "react-router-dom";
import styled from "styled-components";
import Button from "../../../ui/interactive/Button";
import { HiXMark } from "react-icons/hi2";
import { Shop } from "../../../models/shop";

const Table = styled.table`
  width: 90%;
  border-radius: var(--border-radius-md);
  border: 1px solid var(--color-border);
  background-color: rgba(var(--color-secondary-background-rgb), 0.05);
  border-spacing: 0;
`;

const Th = styled.th`
  padding: 10px;
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
  border-top: 1px solid var(--color-border);
  text-align: center;
`;

export default function ShopsTable({
  shops,
  onRemove,
}: {
  shops: Shop[];
  onRemove: Function;
}) {
  const navigate = useNavigate();
  return (
    <Table>
      <thead>
        <Th>Name:</Th>
        <Th>Type:</Th>
        <Th>Location:</Th>
        <Th>Description:</Th>
        <Th></Th>
      </thead>
      <tbody>
        {shops.map((shop: Shop, index: number) => (
          <Tr onClick={() => navigate(`${shop.id}`)} key={index}>
            <Td>{shop.name}</Td>
            <Td>{shop.type}</Td>
            <Td>{shop.location}</Td>
            <Td>{shop.description}</Td>
            {/* <Td>{shop.id}</Td> */}
            <Td>
              <Button
                onClick={(e) => {
                  onRemove(shop.id);
                  e.stopPropagation();
                }}
              >
                <HiXMark />
              </Button>
            </Td>
          </Tr>
        ))}
      </tbody>
    </Table>
  );
}
