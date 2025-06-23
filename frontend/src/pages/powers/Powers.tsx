import { usePowers } from "./hooks/usePowers";
import PowerForm, { initialState } from "../../features/powers/PowerForm";
import { ReusableTable } from "../../ui/containers/ReusableTable2";
import Spinner from "../../ui/interactive/Spinner";
import styled, { css } from "styled-components";
import { useState } from "react";
import { PowerListItem } from "../../models/power";
import Button from "../../ui/interactive/Button";
import { useCreatePower } from "./hooks/useCreatePower";
import { useNavigate, useSearchParams } from "react-router-dom";

export default function Powers() {
    
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const selectedPowerId = searchParams.get("id");
  const openNewPowerForm = searchParams.get("new");

  const handleChangePower = (chosenPowerId: number) => {
    navigate(`/powers?id=${chosenPowerId}`)
  };

  const handleOpenNewPowerForm = () => {
    navigate(`/powers?new=true`)
  };
  const { isLoading, powers, error } = usePowers({ pageSize: 99999999 });

  // const [selectedPowerId, setSelectedPowerId] = useState<null | number>(null);
  const { createPower, isPending: isPendingCreation } = useCreatePower(
    () => {}
  );
  const handleSelect = (row: any) => {
    let selectedPower = powers?.find((_value, index) => index === row.id);

    handleChangePower(selectedPower!.id);
  };

  if (isLoading || isPendingCreation) {
    return <Spinner></Spinner>;
  }
  
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
            handleOpenNewPowerForm();
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
            powerId={Number(selectedPowerId)}
            key={selectedPowerId}
          ></PowerForm>
        )}
        {openNewPowerForm && (
          <PowerForm
            powerId={null}
            key={"x"}
            onCreate={(id) => {
              navigate(`/powers?id=${id}`);
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
