import { useCallback, useContext, useEffect, useState } from "react";
import { CharacterIdContext } from "../contexts/CharacterIdContext";
import { PowerListItem } from "../../../models/power";
import { ReusableTable } from "../../../ui/containers/ReusableTable";
import Spinner from "../../../ui/interactive/Spinner";
import Button from "../../../ui/interactive/Button";
import styled, { css } from "styled-components";
import { PowersToPrepare } from "../../../services/apiCharacters";
import { useUpdateCharactersPowersPrepared } from "../hooks/useUpdateCharactersPowersPrepared";
import { useCharactersPowersPreparedForClass } from "../hooks/useCharactersPowersPreparedForClass";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Heading from "../../../ui/text/Heading";

export function PreparedPowerSelectionForm({
  powersToPrepareContainer,
}: {
  powersToPrepareContainer: PowersToPrepare;
}) {
  const { characterId } = useContext(CharacterIdContext);
  let powersToPrepare = powersToPrepareContainer.powerList;
  const { isLoading: isLoadingPowersPrepared, powers: powersPrepared } =
    useCharactersPowersPreparedForClass(
      characterId,
      powersToPrepareContainer.classId
    );
  const { isPending, updateCharactersChosenPowersForClass } =
    useUpdateCharactersPowersPrepared(
      () => {},
      characterId as number,
      powersToPrepareContainer.classId
    );

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

  let blockSave = useCallback(
    () => powersPreparedLocal?.length > powersToPrepareContainer.numberToChoose,
    [powersPreparedLocal?.length, powersToPrepareContainer.numberToChoose]
  );

  return (
    <Grid>
      <Column1>
        {!isLoadingPowersPrepared && (
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
            customTableContainer={css`
              height: 100%;
            `}
          ></ReusableTable>
        )}
        {isLoadingPowersPrepared && <Spinner />}
      </Column1>
      <Column2>
        {!isPending && (
          <>
            {powersPreparedLocal && (
              <Heading as="h5">
                Selected {powersPreparedLocal.length} out of{" "}
                {powersToPrepareContainer.numberToChoose} allowed
              </Heading>
            )}
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
            <FormRowVertical
              assistiveText={
                blockSave() ? "Maximum selection number exceeded" : undefined
              }
            >
              <Button
                disabled={blockSave()}
                onClick={() =>
                  updateCharactersChosenPowersForClass(powersPreparedLocal)
                }
              >
                {"Save"}
              </Button>
            </FormRowVertical>
          </>
        )}
      </Column2>
      <Column3>
        {!isLoadingPowersPrepared && (
          <ReusableTable
            mainHeader="Selected powers"
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
            customTableContainer={css`
              height: 100%;
            `}
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
  width: 80vw;
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
