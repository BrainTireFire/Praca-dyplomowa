import { useContext } from "react";
import { CharacterIdContext } from "./contexts/CharacterIdContext";
import { useCharactersPowersToPrepare } from "./hooks/useCharactersPowersToPrepare";
import TabList from "../../ui/containers/tabs/TabList";
import TabItem from "../../ui/containers/tabs/TabItem";
import Spinner from "../../ui/interactive/Spinner";
import { PreparedPowerSelectionForm } from "./tables/PreparedPowerSelectionForm";

export function ClassPowersToPrepareTabs() {
  const { characterId } = useContext(CharacterIdContext);
  const {
    isLoading: isLoadingPowersToPrepare,
    powerSelection: powersToPrepare,
  } = useCharactersPowersToPrepare(characterId);
  if (isLoadingPowersToPrepare) {
    return <Spinner></Spinner>;
  }
  console.log(powersToPrepare);
  return (
    <>
      <TabList activeTabIndex={0}>
        {powersToPrepare?.map((x) => (
          <TabItem label={x.className} key={x.classId}>
            <PreparedPowerSelectionForm
              powersToPrepareContainer={x}
            ></PreparedPowerSelectionForm>
          </TabItem>
        )) ?? []}
      </TabList>
    </>
  );
}
