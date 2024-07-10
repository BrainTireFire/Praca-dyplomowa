import Input from "../forms/Input";

export function PlayerSelect({ player }) {
  return (
    <div
      style={{
        gap: "8px",
        display: "flex",
        alignItems: "center",
      }}
    >
      <Input type="checkbox" id={player.id}></Input>
      <label htmlFor={player.id}>
        {player.name} - XP: {player.xp}
      </label>
    </div>
  );
}
