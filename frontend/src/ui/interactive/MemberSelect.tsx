import { CharacterItem } from "../../models/character";
import { DiceSetString } from "../../models/diceset";
import Input from "../forms/Input";

export function MemberSelect({ member, setSelectedMembers, type, selected } : {member: CharacterItem, setSelectedMembers: any, type: "xp" | "rest", selected: boolean}) {
  const handleMemberSelect = (id: number) => {
    setSelectedMembers((previousSelection) =>
      previousSelection.includes(id)
        ? previousSelection.filter((memberId) => id !== memberId)
        : [...previousSelection, id]
    );
  };

  return (
    <div
      style={{
        gap: "8px",
        display: "flex",
        alignItems: "center",
      }}
    >
      <Input
        onChange={() => handleMemberSelect(member.id)}
        type="checkbox"
        id={member.id.toString()}
        checked={selected}
      ></Input>
      <label htmlFor={member.id.toString()}>
        {member.name}
        {type === "xp" ? ` - XP: ${member.xp}` : ""}
        {type === "rest" ? ` - Hit dice to roll: ${DiceSetString(member.restData)}` : ""}
      </label>
    </div>
  );
}
