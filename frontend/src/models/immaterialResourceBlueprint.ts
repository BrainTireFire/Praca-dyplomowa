export type ImmaterialResourceBlueprint = {
  id: number;
  name: string;
};

export type ImmaterialResourceBlueprintWithOwner = {
  id: number | null;
  name: string;
  ownerName: string;
  editable: boolean;
};

export const immaterialResourceInitialValue: ImmaterialResourceBlueprintWithOwner =
  {
    id: null,
    name: "New immaterial resource",
    ownerName: "",
    editable: true,
  };
