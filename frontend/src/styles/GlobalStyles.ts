import { createGlobalStyle } from "styled-components";

const GlobalStyles = createGlobalStyle`
    :root {

  //Dark mode
  &.dark-mode {
  --color-main-background: #1F2421;
  --color-secondary-background: #DCE1DE;
  --color-secondary-background-rgb: 220, 225, 222; //#DCE1DE
  --color-border: #49A078;

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
  --color-input-disable-background: #b2b6b4;

  --color-navbar: #1D1D1D;
  --color-navbar-hover: #222222;
  --color-navbar-border: #1D1D1D;

  --color-button-green: #49A078;


  --backdrop-color: rgba(255, 255, 255, 0.1);

  --shadow-sm: 0 1px 2px rgba(0, 0, 0, 0.04);
  --shadow-md: 0px 0.6rem 2.4rem rgba(0, 0, 0, 0.06);
  --shadow-lg: 0 2.4rem 3.2rem rgba(0, 0, 0, 0.12);
  }

  &, &.light-mode {
  --color-main-background: #fff; /* Pure white background */
  --color-secondary-background: #969393; /* Very light gray background */
  --color-secondary-background-rgb: 108, 107, 107; //rgb(108, 107, 107)
  --color-border: #eaeaea; /* Light gray for borders */

  /* Buttons */
  --color-button-primary: #171717; /* Light gray for primary buttons */
  --color-button-secondary: #bcbbbb; /* Medium gray for secondary buttons */
  --color-button-danger: #ec6868; /* Light red for danger buttons */

  --color-button-hover-primary: #2a2a2a; /* Slightly darker gray for hover */
  --color-button-hover-secondary: #cbc9c9; /* Slightly darker medium gray for hover */
  --color-button-hover-danger: #f34c4c; /* Slightly darker red for hover */

  --color-form-error: #f02a2a; /* Same as button hover danger for error text */

  --color-button-text: #fff; /* Dark gray for text */
  --color-secondary-text: #555555; /* Dark gray for text */
  --color-header-text: #171717; /* Black for header text */
  --color-header-text-hover: #a1a1a1;

  --color-secondary-hover-text: #555555; /* Mid gray for hover text */
  --color-image-hover: #fff;

  --color-link: #bb6f6f;
  --color-link-hover: #dd8383; /* Dark gray for hover links */

  --color-input-text: #171717; /* Black for input text */
  --color-input-focus: #808080; /* Gray for focused inputs */
  --color-input-disable-background: #E0E0E0; /* Light gray for disabled background */

  --color-navbar: #fff; /* Very light gray for navbar */
  --color-navbar-border: #eaeaea;

  --color-button-green: #8ccba2; /* Light green for a specific button */

  --backdrop-color: rgba(0, 0, 0, 0.05); /* Very light black for backdrop */

  --shadow-sm: 0 1px 2px rgba(0, 0, 0, 0.04); /* Small shadow */
  --shadow-md: 0px 0.6rem 2.4rem rgba(0, 0, 0, 0.06); /* Medium shadow */
  --shadow-lg: 0 2.4rem 3.2rem rgba(0, 0, 0, 0.12); /* Large shadow */
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
