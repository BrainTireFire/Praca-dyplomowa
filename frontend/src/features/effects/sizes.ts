export const sizes = [
  "Tiny",
  "Small",
  "Medium",
  "Large",
  "Huge",
  "Gargantuan",
] as const;

export type size = (typeof sizes)[number];

export const SizeLabelMap = {
  Tiny: "Tiny",
  Small: "Small",
  Medium: "Medium",
  Large: "Large",
  Huge: "Huge",
  Gargantuan: "Gargantuan",
} as const;

export const sizesDropdown = sizes.map((x) => {
  return { value: x, label: SizeLabelMap[x] };
});
