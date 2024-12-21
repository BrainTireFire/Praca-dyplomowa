import { useContext, useEffect, useState } from "react";
import { CharacterIdContext } from "../contexts/CharacterIdContext";
import { useObjectPowers } from "../../../hooks/useObjectPowers";
import { useUpdateObjectPowers } from "../../../hooks/useUpdateObjectPowers";
import { PowerListItem } from "../../../models/power";
import { ReusableTable } from "../../../ui/containers/ReusableTable";
import Spinner from "../../../ui/interactive/Spinner";
import Button from "../../../ui/interactive/Button";
import styled from "styled-components";

export function PreparedPowerSelectionForm() {
  const { characterId } = useContext(CharacterIdContext);

  const { isLoading: isLoadingPowersToPrepare, powers: powersToPrepare } =
    useObjectPowers(characterId, "CharacterToPrepare");
  const { isLoading: isLoadingPowersPrepared, powers: powersPrepared } =
    useObjectPowers(characterId, "CharacterPrepared");
  const { isPending, updateObjectPowers: updateItemPowers } =
    useUpdateObjectPowers(() => {}, characterId as number, "CharacterPrepared");

  const [powersPreparedLocal, setPowersPreparedLocal] = useState(
    powersPrepared as PowerListItem[]
  );
  useEffect(() => {
    setPowersPreparedLocal(powersPrepared ?? []);
  }, [powersPrepared]);

  const [selectedPowerIdFromToPrepare, setSelectedPowerIdFromToPrepare] =
    useState<number | null>(null);
  const [selectedPowerIdFromPrepared, setSelectedPowerIdFromPrepared] =
    useState<number | null>(null);

  const powersToPrepareWithoutPrepared = powersToPrepare
    ? powersToPrepare.filter(
        (power) =>
          !powersPreparedLocal?.find(
            (preparedPower) => preparedPower.id === power.id
          )
      )
    : [];

  const handleSelectPowersToPrepare = (row: { id: number; name: string }) => {
    let selectedItem = powersToPrepareWithoutPrepared?.find(
      (_value, index) => index === row.id
    );
    setSelectedPowerIdFromToPrepare(selectedItem ? selectedItem.id : null);
    setSelectedPowerIdFromPrepared(null);
  };
  const handleSelectPreparedPowers = (row: { id: number; name: string }) => {
    let selectedItem = powersPreparedLocal?.find(
      (_value, index) => index === row.id
    );
    setSelectedPowerIdFromPrepared(selectedItem ? selectedItem.id : null);
    setSelectedPowerIdFromToPrepare(null);
  };

  return (
    <Grid>
      <Column1>
        {!isLoadingPowersToPrepare && (
          <ReusableTable
            mainHeader="All powers to prepare"
            tableRowsColomns={{ Name: "name" }}
            data={powersToPrepareWithoutPrepared.map((power, index) => {
              return {
                id: index,
                name: power.name,
              };
            })}
            isSelectable={true}
            onSelect={handleSelectPowersToPrepare}
          ></ReusableTable>
        )}
        {isLoadingPowersToPrepare && <Spinner />}
      </Column1>
      <Column2>
        {!isPending && (
          <>
            <Button
              disabled={selectedPowerIdFromToPrepare === null}
              onClick={() => {
                setPowersPreparedLocal(() => {
                  return [
                    ...(powersPreparedLocal as PowerListItem[]),
                    powersToPrepare?.find(
                      (power) => power.id === selectedPowerIdFromToPrepare
                    ) as PowerListItem,
                  ];
                });
                setSelectedPowerIdFromToPrepare(null);
              }}
            >
              {">>"}
            </Button>
            <Button
              disabled={selectedPowerIdFromPrepared === null}
              onClick={() => {
                setPowersPreparedLocal(() => {
                  return (powersPreparedLocal as PowerListItem[]).filter(
                    (slot) => slot.id !== selectedPowerIdFromPrepared
                  );
                });
                setSelectedPowerIdFromPrepared(null);
              }}
            >
              {"<<"}
            </Button>
            <Button onClick={() => updateItemPowers(powersPreparedLocal)}>
              {"Save"}
            </Button>
          </>
        )}
      </Column2>
      <Column3>
        {!isLoadingPowersPrepared && (
          <ReusableTable
            mainHeader="Selected slots"
            tableRowsColomns={{ Name: "name" }}
            data={
              powersPreparedLocal?.map((power, index) => {
                return {
                  id: index,
                  name: power.name,
                };
              }) ?? []
            }
            isSelectable={true}
            onSelect={handleSelectPreparedPowers}
          ></ReusableTable>
        )}
        {isLoadingPowersPrepared && <Spinner />}
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
