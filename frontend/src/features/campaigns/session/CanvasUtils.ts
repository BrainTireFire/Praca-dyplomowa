const getCssVariable = (variableName: string): string => {
  return getComputedStyle(document.documentElement)
    .getPropertyValue(variableName)
    .trim();
};

export const GRID_SIZE = 70;
export const INITIAL_WIDTH = 1260;
export const INITIAL_HEIGHT = 700;
export const CURSOR_SIZE = 10;

export const getColorForUser = (connectionId: string) => {
  // Example function to assign colors
  const colors = [
    "#FF0000",
    "#00FF00",
    "#0000FF",
    "#FFFF00",
    "#FF00FF",
    "#00FFFF",
  ];
  const index = parseInt(connectionId, 36) % colors.length;
  return colors[index];
};

export const drawGrid = (
  ctx: CanvasRenderingContext2D,
  width: number,
  height: number
) => {
  ctx.save();

  ctx.strokeStyle = getCssVariable("--color-border");
  ctx.lineWidth = 1;

  for (let x = 0; x <= width; x += GRID_SIZE) {
    ctx.beginPath();
    ctx.moveTo(x, 0);
    ctx.lineTo(x, height);
    ctx.stroke();
  }

  for (let y = 0; y <= height; y += GRID_SIZE) {
    ctx.beginPath();
    ctx.moveTo(0, y);
    ctx.lineTo(width, y);
    ctx.stroke();
  }

  ctx.restore();
};

export const highlightBox = (
  ctx: CanvasRenderingContext2D,
  x: number,
  y: number,
  color: string
) => {
  ctx.save();
  ctx.strokeStyle = color;
  ctx.lineWidth = 2;
  ctx.strokeRect(x * GRID_SIZE, y * GRID_SIZE, GRID_SIZE, GRID_SIZE);
  ctx.restore();
};

export const drawCustomCursor = (
  ctx: CanvasRenderingContext2D,
  x: number,
  y: number,
  color: string,
  userName: string
) => {
  // Constants for cursor dimensions
  const cursorWidth = 20;
  const cursorHeight = 20;

  // Draw custom arrow cursor, centering on (x, y)
  ctx.save();
  ctx.fillStyle = color;
  ctx.beginPath();
  // ctx.moveTo(x * GRID_SIZE, y * GRID_SIZE); // Tip of the arrow
  // ctx.lineTo(x * GRID_SIZE - cursorWidth / 2, y * GRID_SIZE + cursorHeight); // Left base
  // ctx.lineTo(x * GRID_SIZE, y * GRID_SIZE + cursorHeight / 2); // Middle base
  // ctx.lineTo(x * GRID_SIZE + cursorWidth / 2, y * GRID_SIZE + cursorHeight); // Right base
  ctx.arc(
    x * GRID_SIZE + GRID_SIZE / 2,
    y * GRID_SIZE + GRID_SIZE / 2,
    CURSOR_SIZE,
    0,
    2 * Math.PI
  );
  ctx.closePath();
  ctx.fill();
  ctx.restore();

  // Draw user name above the cursor
  ctx.save();
  ctx.fillStyle = color;
  ctx.font = "12px Poppins";
  const textWidth = ctx.measureText(userName).width;
  const textX = x * GRID_SIZE + GRID_SIZE / 2 - textWidth / 2;
  const textY = y * GRID_SIZE + GRID_SIZE / 2 - CURSOR_SIZE - 5;

  ctx.fillText(userName, textX, textY);
  ctx.restore();
};
