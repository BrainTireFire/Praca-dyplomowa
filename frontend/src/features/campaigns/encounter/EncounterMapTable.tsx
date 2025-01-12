import { useEffect, useState } from "react";
import Spinner from "../../../ui/interactive/Spinner";
import { useMaps } from "../hooks/useMaps";
import styled, { css } from "styled-components";
import { ReusableTable } from "../../../ui/containers/ReusableTable2";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  height: 100%;
`;

const TABLE_COLUMNS = {
  Name: "name",
  Size: "size",
  "Create at": "test",
};

export default function EncounterMapTable({ onSelect }) {
  const { isLoading, maps } = useMaps();
  const [selectedMap, setSelectedMap] = useState(null);

  useEffect(() => {
    if (selectedMap) {
      const selectedMapFull = maps?.find((map) => map.id === selectedMap.id);
      onSelect(selectedMapFull);
    }
  }, [selectedMap, onSelect]);

  if (isLoading) {
    return <Spinner />;
  }

  if (!maps || maps.length === 0) {
    return <div>No maps available.</div>;
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
        customTableContainer={css`
          flex: 1;
        `}
        //isSearching={true}
        //mainHeader="Maps"
      />
    </Container>
  );
}
