import styled, { css } from "styled-components";

type TypesBox = {
  radius?: "tiny" | "small" | "medium" | "large";
  variation?:
    | "none"
    | "fullScreen"
    | "squaredTiny"
    | "squaredSmall"
    | "squaredMedium"
    | "squaredLarge"
    | "rectangleTiny"
    | "rectangleSmall"
    | "rectangleMedium"
    | "rectangleLarge"
    | "rectangleInputTiny";
  customStyles?: ReturnType<typeof css>;
};

const borderRadius = {
  tiny: css`
    border-radius: var(--border-radius-tiny);
  `,
  small: css`
    border-radius: var(--border-radius-md);
  `,
  medium: css`
    border-radius: var(--border-radius-md);
  `,
  large: css`
    border-radius: var(--border-radius-lg);
  `,
};

const variations = {
  none: css`
    padding: 0.5rem 1rem;
    width: auto;
    height: auto;
  `,
  fullScreen: css`
    padding: 0.5rem 1rem;
    width: 100%;
    height: 100%;
    max-width: 100%;
    max-height: 100%;
  `,
  squaredTiny: css`
    padding: 1rem 2rem;
    width: 200px;
    height: 200px;
  `,
  squaredSmall: css`
    padding: 1rem 2rem;
    width: 300px;
    height: 300px;
  `,
  squaredMedium: css`
    padding: 1rem 2rem;
    width: 400px;
    height: 400px;
  `,
  squaredLarge: css`
    padding: 1rem 2rem;
    width: 590px;
    height: 590px;
  `,
  rectangleInputTiny: css`
    width: 250px;
    height: 35px;
  `,
  rectangleTiny: css`
    padding: 1rem 2rem;
    width: 200px;
    height: 100px;
  `,
  rectangleSmall: css`
    padding: 1rem 2rem;
    width: 300px;
    height: 150px;
  `,
  rectangleMedium: css`
    padding: 1rem 2rem;
    width: 400px;
    height: 200px;
  `,
  rectangleLarge: css`
    padding: 1rem 2rem;
    width: 590px;
    height: 295px;
  `,
};

const Box = styled.div<TypesBox>`
  /* Box */
  background-color: rgba(var(--color-secondary-background-rgb), 0.05);
  border: 1px solid var(--color-border);

  /* overflow: hidden; */
  font-size: 1.4rem;

  /* Border radius */
  ${(props) => borderRadius[props.radius || "medium"]}
  ${(props) => variations[props.variation || "none"]}

  /* Custom styles */
  ${(props) => props.customStyles}
`;

Box.defaultProps = {
  radius: "medium",
  variation: "none",
  customStyles: css``,
};

export default Box;
