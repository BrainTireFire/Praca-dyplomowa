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

export default function EncounterMapTable() {
  const { isLoading, maps } = useMaps();
  const [selected, setSelected] = useState(null);

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
        tableRowsColomns={{ Name: "name", Size: "size", "Create at": "test" }}
        data={formattedMaps}
        selected={selected}
        setSelected={setSelected}
        //isSearching={true}
        //mainHeader="Maps"
      />
    </Container>
  );
}
