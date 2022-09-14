import styled, { keyframes } from "styled-components";

const Spin = keyframes`
    from {transform:rotate(0deg);}
    to {transform:rotate(360deg);}
`;

const Styled = styled.span`
    animation: ${Spin} 1s infinite;
    font-size: 2rem;
`;

const Spinner = () => {
    return <Styled>🕐</Styled>
};

export default Spinner;