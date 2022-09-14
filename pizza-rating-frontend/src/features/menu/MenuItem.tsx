import React from 'react';
import styled from 'styled-components';
import IPizza from '../../app/models/IPizza';
import Reactions from './Reactions';
const Container = styled.div`
    display: flex;
    margin: 1rem 0;
`;

const Thumbnail = styled.div<{ imageSource: string }>`
    background: url(${p => p.imageSource});
    background-color: gray;
    background-size: cover;
    background-position: center;
    
    height: 12rem;
    width: 12rem;
`;

const Content = styled.div`
    padding: 1rem;
    display: flex;
    text-align: center;
    flex-direction: column;
    justify-content: center;
`;

const Title = styled.h3`
    margin-bottom: 1rem;
    margin-top: 0;
`;

interface IProps {
    pizza: IPizza;
    cosmosConnection: string;
}

const MenuItem = ({ pizza, cosmosConnection }: IProps) => {
    return (
        <Container>
            <Thumbnail imageSource={pizza.imageSource} />
            <Content>
                <Title>{pizza.name}</Title>
                <Reactions cosmosConnection={cosmosConnection} pizza={pizza} />
            </Content>
        </Container>
    );
}

export default MenuItem;