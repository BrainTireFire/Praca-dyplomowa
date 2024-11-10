import { useState } from "react";
import Spinner from "../../../ui/interactive/Spinner";
import { ReusableTable } from "../../../ui/containers/ReusableTable";
import { useMaps } from "./useMaps";
import styled from "styled-components";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
`;

const TABLE_COLUMNS = {
  Name: "name",
  Size: "size",
  "Create at": "test",
};

type TypeMap = {
  id: number;
  name: string;
  size: string;
  test: string;
};

export default function EncounterMapTable({
  onSelectBoard,
}: {
  onSelectBoard: any;
}) {
  const [selectedMap, setSelectedMap] = useState<TypeMap | null>(null);
  const { isLoading, maps } = useMaps();

  if (isLoading) {
    return <Spinner />;
  }

  if (!maps || maps.length === 0) {
    return <div>No maps available.</div>;
  }

  if (selectedMap) {
    const board = maps.find((map) => map.id === selectedMap.id);
    onSelectBoard(board);
  }

  const formattedMaps = maps.map((map) => ({
    id: map.id,
    name: map.name,
    size: `${map.sizeX} x ${map.sizeY}`,
    test: map.id,
  }));

  return (
    <Container>
      Pick a map
      <ReusableTable
        tableRowsColomns={TABLE_COLUMNS}
        data={formattedMaps}
        isSelectable={true}
        onSelect={setSelectedMap}
        isSearching={true}
        //mainHeader="Maps"
      />
    </Container>
  );
}
