import { Board, BoardShort } from "../models/map/Board";
import { BoardCreateDto } from "../models/map/BoardDto";
import { BoardUpdateDto } from "../models/map/BoardUpdate";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function createBoard(
  boardCreateDto: BoardCreateDto
): Promise<BoardCreateDto> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(boardCreateDto),
  };

  const data: BoardCreateDto = await customFetch(
    `${BASE_URL}/api/board`,
    options
  );
  return data;
}

export async function updateBoard(
  boardId: number,
  boardUpdateDto: BoardUpdateDto
): Promise<null> {
  const options: RequestInit = {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(boardUpdateDto),
  };

  await customFetch(`${BASE_URL}/api/board/${boardId}`, options);

  return null;
}

export async function getBoards(): Promise<Board[]> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  const data: Board[] = await customFetch(
    `${BASE_URL}/api/board/myboards`,
    options
  );

  return data;
}

export async function getBoardsShort(): Promise<BoardShort[]> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  const data: Board[] = await customFetch(
    `${BASE_URL}/api/board/myboardsShort`,
    options
  );

  return data;
}

export async function getBoard(boardId: number): Promise<Board> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  const data: Board = await customFetch(
    `${BASE_URL}/api/board/${boardId}`,
    options
  );

  return data;
}

export async function deleteBoard(boardId: number): Promise<null> {
  const options: RequestInit = {
    method: "DELETE",
  };

  await customFetch(`${BASE_URL}/api/board/${boardId}`, options);

  return null;
}
