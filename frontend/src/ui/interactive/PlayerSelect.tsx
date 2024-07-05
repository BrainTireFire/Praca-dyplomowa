export function PlayerSelect({ playersList }) {
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
        id={playersList.id}
      ></input>
      <label htmlFor={playersList.id}>{playersList.name}</label>
    </div>
  );
}
