import React from 'react';
import styled from 'styled-components';
import Header from '../../features/header/Header';
import Menu from '../../features/menu/Menu';

const Container = styled.section`
  min-height: 100vh;
`;

const Footer = styled.section`
  text-align: center;
`;


function App() {
  const cosmosConnection = window.localStorage.getItem("cosmos") || '';

  return (
    <Container>
      <Header cosmosConnection={cosmosConnection} />
      <Menu cosmosConnection={cosmosConnection} />
      <Footer>
        <p><a target="_blank" href="https://acn-sbuad.github.io/avanade-workshop">Instructions</a></p>
      </Footer>
    </Container>
  );
}

export default App;
