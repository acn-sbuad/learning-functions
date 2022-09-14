import axios from "axios"
import IPizza from "../models/IPizza";

const getPizzas = async (cosmosConnection: string) => {
    const searchParams = new URLSearchParams();
    searchParams.append('connectionString', cosmosConnection);
    const response = await axios.get<IPizza[]>(`${process.env.REACT_APP_API_URL}/pizza?${searchParams.toString()}`);
    console.log(response);
    return response.data;
}

const ratePizza = async (cosmosConnection: string, pizzaId: string, rating: number) => {
    const searchParams = new URLSearchParams();
    searchParams.append('connectionString', cosmosConnection);
    const response = await axios.post(`${process.env.REACT_APP_API_URL}/ratings?${searchParams}`, {
        pizzaId,
        score: rating
    });
    return response.status === 200;
}

export { getPizzas, ratePizza }