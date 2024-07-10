import { CharacterItem, Character } from "../models/character";

export async function getCharacters(): Promise<CharacterItem[]> {
  const response = await fetch("http://localhost:3000/characterList");

  if (!response.ok) {
    throw new Error(`HTTP error! Status: ${response.status}`);
  }

  const data: CharacterItem[] = await response.json();

  return data;
}

export async function getCharacter(characterId: number): Promise<Character> {
  const response = await fetch(
    `http://localhost:3000/characters/${characterId}`
  );

  if (!response.ok) {
    throw new Error(`HTTP error! Status: ${response.status}`);
  }

  const campaign: Character = await response.json();
  return campaign;
}
