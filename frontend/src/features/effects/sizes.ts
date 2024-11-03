export const sizes = [
  "tiny",
  "small",
  "medium",
  "large",
  "huge",
  "gargantuan",
] as const;

export type size = (typeof sizes)[number];

export const SizeLabelMap = {
  tiny: "Tiny",
  small: "Small",
  medium: "Medium",
  large: "Large",
  huge: "Huge",
  gargantuan: "Gargantuan",
} as const;

export const sizesDropdown = sizes.map((x) => {
  return { value: x, label: SizeLabelMap[x] };
});
