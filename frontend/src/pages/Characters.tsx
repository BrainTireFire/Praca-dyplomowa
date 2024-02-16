import React from "react";
import AttributeBox from "../ui/characters/AttributeBox";

export default function Characters() {
  return (
    <div>
      Characters
      <AttributeBox>
        <AttributeBox.Header>Strength</AttributeBox.Header>
        <AttributeBox.Box>
          20
          <AttributeBox.Circle>+5</AttributeBox.Circle>
        </AttributeBox.Box>
      </AttributeBox>
    </div>
  );
}
