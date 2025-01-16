import Input from "../forms/Input";

export function MemberSelect({ member, setSelectedMembers, type }) {
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
        id={member.id}
      ></Input>
      <label htmlFor={member.id}>
        {member.name}
        {type === "xp" ? ` - XP: ${member.xp}` : ""}
        {type === "rest" ? ` - Rest: ${member.rest}` : ""}
      </label>
    </div>
  );
}
