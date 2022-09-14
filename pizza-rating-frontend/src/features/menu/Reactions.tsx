import React from 'react';
import styled from 'styled-components';
import IPizza from '../../app/models/IPizza';
import Reaction from './Reaction';

const Container = styled.div`
    display: flex;
    justify-content: center;
    background: #ecf0f1;
    border-radius: 4rem;
    padding: 1rem;
`;


const symbols = ['😭', '🙁', '😐', '🙂', '😍'];

interface IProps {
    pizza: IPizza;
    cosmosConnection: string;
}

const getKey = (index: number) => {
    switch (index) {
        case 0: return "0";
        case 1: return "1";
        case 2: return "2";
        case 3: return "3";
        case 4: return "4";
        case 5: return "5";
        default: return "0";
    }
}

const Reactions = ({ pizza, cosmosConnection }: IProps) => {
    return(
        <Container>{symbols.map((symbol, i) => <Reaction key={i} cosmosConnection={cosmosConnection} count={pizza.ratingSummary[getKey(i)]} pizzaId={pizza.id} ranking={i} symbol={symbol} />)}</Container>
    );
}

export default Reactions;