export function PlayerSelect({ player }) {
  return (
    <div
      style={{
        display: "flex",
        alignItems: "center",
      }}
    >
      <input
        style={{ marginRight: "8px" }}
        type="checkbox"
        id={player.id}
      ></input>
      <label htmlFor={player.id}>
        {player.name} - XP: {player.xp}
      </label>
    </div>
  );
}
