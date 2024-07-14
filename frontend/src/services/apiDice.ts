export async function getDice(): Promise<Dice[]> {
  const response = await fetch("http://localhost:3000/dice");

  if (!response.ok) throw new Error(`HTTP error! Status: ${response.status}`);

  const data: Dice[] = await response.json();
  return data;
}

export async function getDiceById(diceId: string): Promise<Dice> {
  const response = await fetch(`http://localhost:3000/dice/${diceId}`);

  if (!response.ok) throw new Error(`HTTP error! Status: ${response.status}`);

  const data: Dice = await response.json();
  return data;
}
