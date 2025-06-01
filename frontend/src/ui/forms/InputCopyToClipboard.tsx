import React, { useState } from "react";
import styled from "styled-components";
import { FaRegClipboard } from "react-icons/fa";
import { toast } from "react-hot-toast";

const Container = styled.div`
  display: flex;
  align-items: center;
`;

const Input = styled.input`
  padding: 8px;
  width: 250px;
  border: 1px solid #ccc;
  border-radius: 4px;
  /* margin-right: 8px; */
`;

const Button = styled.button`
  padding: 8px 12px;
  border: none;
  background-color: #007bff;
  color: white;
  border-radius: 4px;
  cursor: pointer;

  &:hover {
    background-color: #0056b3;
  }
`;

const IconContainer = styled.div`
  /* position: absolute;
  right: 1000px;
  top: 75%; */
  transform: translateX(-205%) translateY(10%);
  color: blue;
  pointer-events: none; /* Allows click to pass through to input */
`;

const encode = (number: number): string => {
  let base64 = btoa(number.toString() + "zoNK");
  base64 = base64.split("").reverse().join("");
  return base64;
};

const InputCopyToClipboard = ({ campaignId }) => {
  const [value, setValue] = useState("");

  React.useEffect(() => {
    if (campaignId) {
      const origin = window.location.origin;
      const encodedId = encode(campaignId);
      setValue(`${origin}/join/${encodedId}`);
    }
  }, [campaignId]);

  const copyToClipboard = () => {
    navigator.clipboard
      .writeText(value)
      .then(() => {
        toast.success("Copied to clipboard!");
      })
      .catch((err) => {
        toast.error("Failed to copy!");
        console.error("Failed to copy!", err);
      });
  };

  return (
    <Container>
      <Input type="text" value={value} readOnly onClick={copyToClipboard} />
      <IconContainer>
        <FaRegClipboard />
      </IconContainer>
    </Container>
  );
};

export default InputCopyToClipboard;
