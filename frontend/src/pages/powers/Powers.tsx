import { usePowers } from "./hooks/usePowers";
import PowerForm, { initialState } from "../../features/powers/PowerForm";
import { ReusableTable } from "../../ui/containers/ReusableTable";
import Spinner from "../../ui/interactive/Spinner";
import styled, { css } from "styled-components";
import { useState } from "react";
import { PowerListItem } from "../../models/power";
import Button from "../../ui/interactive/Button";
import { useCreatePower } from "./hooks/useCreatePower";

export default function Powers() {
  const { isLoading, powers, error } = usePowers();

  const [selectedPowerId, setSelectedPowerId] = useState<null | number>(null);
  const { createPower, isPending: isPendingCreation } = useCreatePower(
    () => {}
  );
  const handleSelect = (row: any) => {
    console.log(powers);
    console.log(row);
    let selectedPower = powers?.find((_value, index) => index === row.id);
    console.log(selectedPower);

    setSelectedPowerId(selectedPower ? selectedPower.id : null);
    console.log(selectedPowerId);
  };

  if (isLoading || isPendingCreation) {
    return <Spinner></Spinner>;
  }
  // console.log(power);
  return (
    <Container>
      <Column1>
        <ReusableTable
          tableRowsColomns={{
            Name: "Name",
          }}
          data={
            powers
              ? powers.map((power, index) => {
                  return {
                    id: index,
                    Name: power.name,
                  };
                })
              : []
          }
          isSelectable={true}
          onSelect={handleSelect}
        ></ReusableTable>
        <Button
          onClick={() => {
            createPower(initialState);
          }}
        >
          Create new
        </Button>
      </Column1>
      <Column2>
        {selectedPowerId && (
          <PowerForm
            powerId={selectedPowerId}
            key={selectedPowerId}
          ></PowerForm>
        )}
      </Column2>
    </Container>
  );
}

const Container = styled.div`
  display: grid;
  grid-template-columns: auto 1fr;
`;

const Column1 = styled.div`
  grid-column: 1;
`;
const Column2 = styled.div`
  grid-column: 2;
`;
