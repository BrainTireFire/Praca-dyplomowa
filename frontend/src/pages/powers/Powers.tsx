import { usePowers } from "./hooks/usePowers";
import PowerForm, { initialState } from "../../features/powers/PowerForm";
import { ReusableTable } from "../../ui/containers/ReusableTable2";
import Spinner from "../../ui/interactive/Spinner";
import styled, { css } from "styled-components";
import { useState } from "react";
import { PowerListItem } from "../../models/power";
import Button from "../../ui/interactive/Button";
import { useCreatePower } from "./hooks/useCreatePower";

export default function Powers() {
  const { isLoading, powers, error } = usePowers({ pageSize: 99999999 });
  const [openNewPowerForm, setOpenNewPowerForm] = useState(false);

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
    setOpenNewPowerForm(false);
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
            Description: "Description",
          }}
          data={
            powers
              ? powers.map((power, index) => {
                  return {
                    id: index,
                    Name: power.name,
                    Description: power.description,
                  };
                })
              : []
          }
          isSelectable={true}
          onSelect={handleSelect}
          isSearching={true}
          customTableContainer={css`
            height: 95%;
          `}
        ></ReusableTable>
        <Button
          onClick={() => {
            setOpenNewPowerForm(true);
            setSelectedPowerId(null);
          }}
          customStyles={css`
            height: 5%;
          `}
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
        {openNewPowerForm && (
          <PowerForm
            powerId={null}
            key={"x"}
            onCreate={(id) => {
              setOpenNewPowerForm(false);
              setSelectedPowerId(id);
            }}
          ></PowerForm>
        )}
      </Column2>
    </Container>
  );
}

const Container = styled.div`
  display: flex;
  flex-direction: row;
  max-height: 100%;
  height: 100%;
`;

const Column1 = styled.div`
  max-height: 100%;
  height: 100%;
  width: 40%;
  display: flex;
  flex-direction: column;
`;
const Column2 = styled.div`
  max-height: 100%;
  height: 100%;
  max-width: 60%;
`;
