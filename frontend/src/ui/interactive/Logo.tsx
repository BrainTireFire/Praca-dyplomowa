import styled from "styled-components";

const StyledLogo = styled.div`
  text-align: center;
`;

const Img = styled.img`
  height: 350px;
  width: 350px;
`;

type LogoProps = {
  src?: string;
  alt?: string;
};

function Logo({ src, alt }: LogoProps) {
  return (
    <StyledLogo>
      <Img src={src} alt={alt} />
    </StyledLogo>
  );
}

export default Logo;
