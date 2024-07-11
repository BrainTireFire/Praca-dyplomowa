import Input from "../forms/Input";

export function PlayerSelect({ player, handlePlayerSelect, type }) {
  return (
    <div
      style={{
        gap: "8px",
        display: "flex",
        alignItems: "center",
      }}
    >
      <Input
        onChange={() => handlePlayerSelect(player.id)}
        type="checkbox"
        id={player.id}
      ></Input>
      <label htmlFor={player.id}>
        {player.name}
        {type === "xp" ? ` - XP: ${player.xp}` : ""}
        {type === "rest" ? ` - Stamina: ${player.stamina}` : ""}
      </label>
    </div>
  );
}
