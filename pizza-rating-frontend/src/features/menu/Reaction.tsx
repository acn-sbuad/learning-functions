import { useState } from 'react';
import styled from 'styled-components';
import { ratePizza } from '../../app/utils/pizzaService';


// to do animate on click
const Style = styled.button`
    font-size: 2rem;
    background: none;
    border: none;
    cursor: pointer;
    &:hover {
        transform: scale(1.1);
    } 
`;

const Container = styled.div`
    display: flex;
    flex-direction: column;
`;

interface IProps {
    symbol: string;
    count: number;
    pizzaId: string;
    ranking: number;
    cosmosConnection: string;
}
const Reaction = ({ symbol, count, pizzaId, ranking, cosmosConnection }: IProps) => {
    const [countLocal, setCountLocal] = useState(count);
    const [isLoading, setIsLoading] = useState(false);

    const onClick = async () => {
        setIsLoading(true);
        try {
            const success = await ratePizza(cosmosConnection, pizzaId, ranking);
            if (success) {
                setCountLocal(countLocal + 1);
            }
        } catch(err) {
            console.log(err);
        }
        setIsLoading(false);
    }
    return (
        <Container>
            <Style disabled={isLoading} onClick={onClick}>
                {symbol}
            </Style>
            {countLocal}
        </Container>
    );
}

export default Reaction;