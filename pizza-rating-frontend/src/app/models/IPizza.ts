interface IPizza {
    name: string;
    id: string;
    imageSource: string;
    ratingSummary: {
        '0': number;
        '1': number;
        '2': number;
        '3': number;
        '4': number;
        '5': number;
    }
}

export default IPizza;