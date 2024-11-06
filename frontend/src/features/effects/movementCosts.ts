export const movementCosts = ["high", "impassable"] as const;

export type movementCost = (typeof movementCosts)[number];

export const MovementCostLabelMap = {
  high: "High",
  impassable: "Impassable",
} as const;

export const movementCostsDropdown = movementCosts.map((x) => {
  return { value: x, label: MovementCostLabelMap[x] };
});
