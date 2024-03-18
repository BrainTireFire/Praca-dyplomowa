import { createGlobalStyle } from "styled-components";

const GlobalStyles = createGlobalStyle`
    :root {

  --color-main-background: #1F2421;
  --color-secondary-background: #DCE1DE;
  --color-secondary-background-rgb: 220, 225, 222; //#DCE1DE
  --color-border: #49A078;

  --color-button-primary: #D14836;
  --color-button-secondary: #49C5B6;

  --color-button-hover-primary:  #831b0d;
  --color-button-hover-secondary: #096359;

  --color-secondary-text: #DCE1DE;
  --color-header-text: #9CC5A1;

  --color-link: #FA9021;

  --color-black-100: #200d0d;

  --color-navbar: #1D1D1D;

  --color-button-green: #49A078;
  


  --backdrop-color: rgba(255, 255, 255, 0.1);

  --shadow-sm: 0 1px 2px rgba(0, 0, 0, 0.04);
  --shadow-md: 0px 0.6rem 2.4rem rgba(0, 0, 0, 0.06);
  --shadow-lg: 0 2.4rem 3.2rem rgba(0, 0, 0, 0.12);

  --border-radius-tiny: 3px;
  --border-radius-sm: 5px;
  --border-radius-md: 15px;
  --border-radius-lg: 9px;

  /* For dark mode */
  --image-grayscale: 0;
  --image-opacity: 100%;
}

*,
*::before,
*::after {
  box-sizing: border-box;
  padding: 0;
  margin: 0;

  /* Creating animations for dark mode */
  transition: background-color 0.3s, border 0.3s;
}

html {
  font-size: 62.5%;
}

body {
  font-family: "Poppins", sans-serif;
  color: var(--color-secondary-text);
  background: var(--color-main-background);

  transition: color 0.3s, background-color 0.3s;
  min-height: 100vh;
  line-height: 1.5;
  font-size: 1.6rem;
}

input,
button,
textarea,
select {
  font: inherit;
  color: var(--color-black-100);
}

button {
  cursor: pointer;
}

*:disabled {
  cursor: not-allowed;
}

select:disabled,
input:disabled {
  background-color: var(--color-grey-200);
  color: var(--color-grey-500);
}

input:focus,
button:focus,
textarea:focus,
select:focus {
  outline: 2px solid var(--color-brand-600);
  outline-offset: -1px;
}

/* Parent selector, finally 😃 */
button:has(svg) {
  line-height: 0;
}

a {
  color: inherit;
  text-decoration: none;
}

ul {
  list-style: none;
}

p,
h1,
h2,
h3,
h4,
h5,
h6 {
  overflow-wrap: break-word;
  hyphens: auto;
}

img {
  max-width: 100%;

  /* For dark mode */
  filter: grayscale(var(--image-grayscale)) opacity(var(--image-opacity));
}


//FOR DARK MODE

`;

export default GlobalStyles;
