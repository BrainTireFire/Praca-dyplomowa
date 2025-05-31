import { createGlobalStyle } from "styled-components";

const GlobalStyles = createGlobalStyle`
    :root {

  //Dark mode
  &.dark-mode {
  --color-main-background: #1F2421;
  --color-secondary-background: #DCE1DE;
  --color-secondary-background-rgb: 220, 225, 222; //#DCE1DE
  --color-border: #49A078;
  --color-border2:rgb(45, 102, 76);

  --color-button-primary: #D14836;
  --color-button-primary-disabled:rgb(206, 138, 129);
  --color-button-secondary: #49C5B6;
  --color-button-danger: #ab0101;

  --color-button-hover-primary:  #831b0d;
  --color-button-hover-secondary: #096359;
  --color-button-hover-danger: #730202;

  --color-form-error: #ce1313;

  --color-button-text: #DCE1DE;
  --color-secondary-text: #DCE1DE;
  --color-header-text: #9CC5A1;
  --color-header-text-hover: #638767;

  --color-secondary-hover-text: #9a9b9a;
  --color-image-hover: #000000;

  --color-link: #FA9021;
  --color-link-hover: #8f4a00;

  --color-input-text: #000000;
  --color-input-focus: #001fe7;
  --color-input-disable-background: #828583;

  --color-navbar: #1D1D1D;
  --color-navbar-hover: #222222;
  --color-navbar-border: #1D1D1D;

  --color-button-green: #49A078;

  --color-text-name-canvas: "#011b84";

  --backdrop-color: rgba(255, 255, 255, 0.1);
  --backdrop-color-hover: rgba(255, 255, 255, 0.2);

  --shadow-sm: 0 1px 2px rgba(0, 0, 0, 0.04);
  --shadow-md: 0px 0.6rem 2.4rem rgba(0, 0, 0, 0.06);
  --shadow-lg: 0 2.4rem 3.2rem rgba(0, 0, 0, 0.12);
  }

  &, &.light-mode {
    /* Base colors */
    --color-main-background: #ffffff;
    --color-secondary-background: #f0f2f5;
    --color-secondary-background-rgb: 240, 242, 245;
    --color-border: #252525;

    /* Text colors */
    --color-header-text: #101828;
    --color-header-text-hover: #344054;
    --color-button-text: #ffffff;
    --color-secondary-text: #171717;
    --color-input-text: #101828;
    
    /* Button colors - more vibrant and distinct */
    --color-button-primary: #0052cc;
    --color-button-secondary: #5d769d;
    --color-button-danger: #d92d20;
    --color-button-green: #039855;

    --color-button-hover-primary: #0747a6;
    --color-button-hover-secondary: #1455c5;
    --color-button-hover-danger: #b42318;

    /* Link colors */
    --color-link: #c11574;
    --color-link-hover: #9e0861;
    
    /* Form colors */
    --color-form-error: #d92d20;
    --color-input-focus: #2970ff;
    --color-input-disable-background: #f2f4f7;
    
    /* Navigation */
    --color-navbar: #ffffff;
    --color-navbar-border: #252525;
    
    /* Interactive states */
    --color-secondary-hover-text: #344054;
    --color-image-hover: #ffffff;
    
    /* Effects */
    --backdrop-color: rgba(16, 24, 40, 0.08);
    
    /* Enhanced shadows for better depth */
    --shadow-sm: 0 1px 2px rgba(16, 24, 40, 0.05), 0 1px 3px rgba(16, 24, 40, 0.1);
    --shadow-md: 0 4px 8px -2px rgba(16, 24, 40, 0.1), 0 2px 4px -1px rgba(16, 24, 40, 0.06);
    --shadow-lg: 0 12px 16px -4px rgba(16, 24, 40, 0.08), 0 4px 6px -2px rgba(16, 24, 40, 0.05);
  }

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
  height: 100%;
    box-sizing: border-box;
  margin: 0;
  padding: 0;
}

body {
  font-family: "Poppins", sans-serif;
  color: var(--color-secondary-text);
  background: var(--color-main-background);

  transition: color 0.3s, background-color 0.3s;
  min-height: 100vh;
  line-height: 1.5;
  font-size: 1.6rem;
  height: 100%;
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}

#root {
  height: 100%; /* Ensure root div takes full height */
}

input,
button,
textarea,
select {
  font: inherit;
  color: var(--color-input-text);
}

button {
  cursor: pointer;
}

*:disabled {
  cursor: not-allowed;
}

select:disabled,
input:disabled {
  background-color: var(--color-input-disable-background);
  border: 1px solid var(--color-input-disable-background);
  color: var(--color-input-text);
}

input:focus,
button:focus,
textarea:focus,
select:focus {
  outline: 2px solid var(--color-input-focus);
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

`;

export default GlobalStyles;
