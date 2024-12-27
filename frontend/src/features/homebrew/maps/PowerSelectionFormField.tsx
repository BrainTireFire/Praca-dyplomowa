import { useEffect, useState } from "react";
import styled from "styled-components";
import { ParentObjectIdContext } from "../../context/ParentObjectIdContext";
import { usePowers } from "../../../pages/powers/hooks/usePowers";
import { ReusableTable } from "../../../ui/containers/ReusableTable";
import Spinner from "../../../ui/interactive/Spinner";
import Button from "../../../ui/interactive/Button";

export function PowerSelectionFormField() {
  const { isLoading: isLoadingAllPowers, powers: allPowers } = usePowers({
    CastableBy: "Terrain",
  });

  const [selectedPowerIdFromAll, setSelectedPowerIdFromAll] = useState<
    number | null
  >(null);
  const [selectedPowerIdFromItem, setSelectedPowerIdFromItem] = useState<
    number | null
  >(null);

  const allPowersWithoutLocal = allPowers
    ? allPowers.filter(
        (power) => !allPowers?.find((itemPower) => itemPower.id === power.id)
      )
    : [];

  const handleSelectAllPowers = (row: { id: number; name: string }) => {
    let selectedItem = allPowersWithoutLocal?.find(
      (_value, index) => index === row.id
    );
    setSelectedPowerIdFromAll(selectedItem ? selectedItem.id : null);
    setSelectedPowerIdFromItem(null);
  };

  const handleSelectItemPowers = (row: { id: number; name: string }) => {
    let selectedItem = allPowers?.find((_value, index) => index === row.id);
    setSelectedPowerIdFromItem(selectedItem ? selectedItem.id : null);
    setSelectedPowerIdFromAll(null);
  };

  return (
    <Grid>
      <Column1>
        {!isLoadingAllPowers && (
          <ReusableTable
            mainHeader="All possible slots"
            tableRowsColomns={{ Name: "name" }}
            data={allPowersWithoutLocal.map((power, index) => {
              return {
                id: index,
                name: power.name,
              };
            })}
            isSelectable={true}
            onSelect={handleSelectAllPowers}
          ></ReusableTable>
        )}
        {isLoadingAllPowers && <Spinner />}
      </Column1>
      <Column2>
        <>
          <Button
            disabled={selectedPowerIdFromAll === null}
            onClick={() => {
              setSelectedPowerIdFromAll(null);
            }}
          >
            {">>"}
          </Button>
          <Button
            disabled={selectedPowerIdFromItem === null}
            onClick={() => {
              //   setItemPowersLocal(() => {
              //     return (itemPowersLocal as PowerListItem[]).filter(
              //       (slot) => slot.id !== selectedPowerIdFromItem
              //     );
              //   });
              setSelectedPowerIdFromItem(null);
            }}
          >
            {"<<"}
          </Button>
          <Button
            onClick={() => {
              alert("ELO");
            }}
          >
            {"Save"}
          </Button>
        </>
      </Column2>
      <Column3>
        <ReusableTable
          mainHeader="Selected slots"
          tableRowsColomns={{ Name: "name" }}
          data={
            allPowers?.map((power, index) => {
              return {
                id: index,
                name: power.name,
              };
            }) ?? []
          }
          isSelectable={true}
          onSelect={handleSelectItemPowers}
        ></ReusableTable>
      </Column3>
    </Grid>
  );
}

const Grid = styled.div`
  display: grid;
  grid-template-columns: auto auto auto;
  grid-column-gap: 10px;
`;

const Column1 = styled.div`
  grid-column: 1/2;
`;
const Column2 = styled.div`
  grid-column: 2/3;
  display: flex;
  flex-direction: column;
`;
const Column3 = styled.div`
  grid-column: 3/4;
`;
