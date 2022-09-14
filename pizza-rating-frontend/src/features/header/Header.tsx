import { FC, useState } from 'react';
import styled from 'styled-components';

const Container = styled.div`
  background: #ff5800;
  display: flex;
  justify-content: space-between;
  padding: 1rem;
  align-items: bottom;
  color: white;

  @media (max-width: 700px) {
        flex-direction: column;
    }
`;

const Logo = styled.div`

    font-size: 1.5rem;
`;

const Button = styled.button`
    background: none;
    color: white;
    border: 1px solid white;
    outline: none;
    cursor: pointer;
    font-size: 1rem;
    line-height: 100%;
    padding: 0.5rem;
    min-width: 100px;
`;

const Input = styled.input`
    background: white;
    border: none;
    outline: none;
    padding: 0.5rem;
    margin: 0 0.5rem;
`;



interface IProps {
    cosmosConnection: string;
}
const Header: FC<IProps> = ({ cosmosConnection }) => {
    const [connectionString, setConnectionString] = useState(cosmosConnection);

    const handleConnectionStringChange = (value: string) => {
        setConnectionString(value);
    }

    const updateConnection = () => {
        window.localStorage.setItem("cosmos", connectionString);
        window.location.reload();
    }

    return (<Container>
        <Logo>Pizza rank</Logo>
        <div>
            <label>Cosmos connectionstring:</label>
            <Input value={connectionString} onChange={(e) => handleConnectionStringChange(e.target.value)} type="text"></Input>
            {cosmosConnection !== connectionString && <Button onClick={updateConnection}>Oppdater</Button>}
        </div>

    </Container>)
}

export default Header;