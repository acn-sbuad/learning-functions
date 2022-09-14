import React, { useEffect, useState } from 'react';
import styled from 'styled-components';
import IPizza from '../../app/models/IPizza';
import { getPizzas } from '../../app/utils/pizzaService';
import Spinner from '../Loader/Loader';
import MenuItem from './MenuItem';

const Container = styled.div`
    display: grid;
    grid-template-columns: 1fr 1fr;
    margin: 1rem auto;
    justify-items: center;

    @media (max-width: 1200px) {
        grid-template-columns: 1fr;
    }
`;

const Loading = styled.div`
    display: flex;
    justify-content: center;
    margin: 10rem;
`;

const ErrorContainer = styled.div`
    margin: 5rem auto;
    padding: 2rem;
    background: #e74c3c;
    text-align: center;
    color: white;
    width: 60%;
`;

interface IProps {
    cosmosConnection: string;
}

const Menu = ({ cosmosConnection }: IProps) => {
    const [pizzas, setPizzas] = useState<IPizza[]>([]);
    const [error, setError] = useState(false);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        setError(false);
        getPizzas(cosmosConnection)
        .then(response => {
            setPizzas(response);
        })
        .catch(err => setError(true))
        .finally(() => setIsLoading(false))
    }, [])
    if(error) {
        return <ErrorContainer>Something went wrong while fetching pizzas.</ErrorContainer>
    }
    if(isLoading) {
        return <Loading><Spinner /></Loading>
    }
    return (
        <Container>
            {pizzas.map(pizza => <MenuItem cosmosConnection={cosmosConnection} key={pizza.id} pizza={pizza} />)}
        </Container>
    );
}

export default Menu;