import { useContext, useEffect, useState } from "react";
import { useUpdateObjectPowers } from "../../hooks/useUpdateObjectPowers";
import { usePowers } from "../../pages/powers/hooks/usePowers";
import { useObjectPowers } from "../../hooks/useObjectPowers";
import { PowerListItem } from "../../models/power";
import { ReusableTable } from "../../ui/containers/ReusableTable2";
import Spinner from "../../ui/interactive/Spinner";
import Button from "../../ui/interactive/Button";
import styled, { css } from "styled-components";
import { ParentObjectIdContext } from "../../context/ParentObjectIdContext";

export function PowerSelectionForm() {
  const { objectId, objectType } = useContext(ParentObjectIdContext);

  const { isLoading: isLoadingAllPowers, powers: allPowers } = usePowers(
    {
      CastableBy: "Character",
    }
  );
  const { isLoading: isLoadingItemPowers, powers: itemPowers } =
    useObjectPowers(objectId, objectType);
  const { isPending, updateObjectPowers: updateItemPowers } =
    useUpdateObjectPowers(() => {}, objectId as number, objectType);

  const [itemPowersLocal, setItemPowersLocal] = useState(
    itemPowers as PowerListItem[]
  );
  useEffect(() => {
    setItemPowersLocal(itemPowers ?? []);
  }, [itemPowers]);

  const [selectedPowerIdFromAll, setSelectedPowerIdFromAll] = useState<
    number | null
  >(null);
  const [selectedPowerIdFromItem, setSelectedPowerIdFromItem] = useState<
    number | null
  >(null);

  const allPowersWithoutLocal = allPowers
    ? allPowers.filter(
        (power) =>
          !itemPowersLocal?.find((itemPower) => itemPower.id === power.id)
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
    let selectedItem = itemPowersLocal?.find(
      (_value, index) => index === row.id
    );
    setSelectedPowerIdFromItem(selectedItem ? selectedItem.id : null);
    setSelectedPowerIdFromAll(null);
  };

  return (
    <Grid>
      <Column1>
        {!isLoadingAllPowers && (
          <ReusableTable
            mainHeader="All available powers"
            tableRowsColomns={{ Name: "name" }}
            data={allPowersWithoutLocal.map((power, index) => {
              return {
                id: index,
                name: power.name,
              };
            })}
            isSelectable={true}
            onSelect={handleSelectAllPowers}
            customTableContainer={css`
              height: 100%;
            `}
          ></ReusableTable>
        )}
        {isLoadingAllPowers && <Spinner />}
      </Column1>
      <Column2>
        {!isPending && (
          <>
            <Button
              disabled={selectedPowerIdFromAll === null}
              onClick={() => {
                setItemPowersLocal(() => {
                  return [
                    ...(itemPowersLocal as PowerListItem[]),
                    allPowers?.find(
                      (power) => power.id === selectedPowerIdFromAll
                    ) as PowerListItem,
                  ];
                });
                setSelectedPowerIdFromAll(null);
              }}
            >
              {">>"}
            </Button>
            <Button
              disabled={selectedPowerIdFromItem === null}
              onClick={() => {
                setItemPowersLocal(() => {
                  return (itemPowersLocal as PowerListItem[]).filter(
                    (slot) => slot.id !== selectedPowerIdFromItem
                  );
                });
                setSelectedPowerIdFromItem(null);
              }}
            >
              {"<<"}
            </Button>
            <Button onClick={() => updateItemPowers(itemPowersLocal)}>
              {"Save"}
            </Button>
          </>
        )}
        {isPending && <Spinner/>}
      </Column2>
      <Column3>
        {!isLoadingItemPowers && (
          <ReusableTable
            mainHeader="Selected powers"
            tableRowsColomns={{ Name: "name" }}
            data={
              itemPowersLocal?.map((power, index) => {
                return {
                  id: index,
                  name: power.name,
                };
              }) ?? []
            }
            isSelectable={true}
            onSelect={handleSelectItemPowers}
            customTableContainer={css`
              height: 100%;
            `}
          ></ReusableTable>
        )}
        {isLoadingItemPowers && <Spinner />}
      </Column3>
    </Grid>
  );
}

const Grid = styled.div`
  display: grid;
  grid-template-columns: 1fr 10% 1fr;
  grid-column-gap: 10px;
  width: 70vw;
  height: 70vh;
`;

const Column1 = styled.div`
  grid-column: 1/2;
  height: 100%;
  overflow-y: hidden;
`;
const Column2 = styled.div`
  grid-column: 2/3;
  display: flex;
  flex-direction: column;
`;
const Column3 = styled.div`
  grid-column: 3/4;
  height: 100%;
  overflow-y: hidden;
`;
