import styled, { css } from "styled-components";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import { CoinPurse } from "./models/coinPurse";
import Box from "../../ui/containers/Box";
import { useContext } from "react";
import { EditModeContext } from "../../context/EditModeContext";

export function CoinPurseForm({
  value,
  onGoldChange,
  onSilverChange,
  onCopperChange,
  disabled,
}: {
  value: CoinPurse;
  onGoldChange: (x: any) => void;
  onSilverChange: (x: any) => void;
  onCopperChange: (x: any) => void;
  disabled: boolean;
}) {
  return (
    <FlexBox>
      <FormRowVertical label="Gold pieces">
        <Input
          disabled={disabled}
          type="number"
          value={value.goldPieces}
          onChange={onGoldChange}
          customStyles={css`
            width: 6em;
          `}
        ></Input>
      </FormRowVertical>
      <FormRowVertical label="Silver pieces">
        <Input
          disabled={disabled}
          type="number"
          value={value.silverPieces}
          onChange={onSilverChange}
          customStyles={css`
            width: 6em;
          `}
        ></Input>
      </FormRowVertical>
      <FormRowVertical label="Copper pieces">
        <Input
          disabled={disabled}
          type="number"
          value={value.copperPieces}
          onChange={onCopperChange}
          customStyles={css`
            width: 6em;
          `}
        ></Input>
      </FormRowVertical>
    </FlexBox>
  );
}

const FlexBox = styled(Box)`
  display: flex;
  flex-direction: row;
`;

CoinPurseForm.defaultProps = {
  disabled: false,
};
