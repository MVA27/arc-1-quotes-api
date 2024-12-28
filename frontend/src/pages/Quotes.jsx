import QuoteCard from "../components/QuoteCard";
import { useState, useEffect } from "react";

export default function Quotes(){

    let quotes = [ 
                 {quote: 'Be yourself; everyone else is already taken', author: 'Oscar Wilde', image: 'https://images.gr-assets.com/authors/1673611182p8/3565.jpg'},
                 {quote: 'Two things are infinite: the universe and human stupidity; and I`m not sure about the universe', author: 'Albert Einstein', image: 'https://images.gr-assets.com/authors/1429114964p8/9810.jpg'}
               ];

    const [data , updateData ] = useState(quotes);

    const generateCards = data.map(item => QuoteCard(item));

    useEffect(()=>{
        const endpoint = import.meta.env.VITE_API_URL;  // for Vite

        fetch(endpoint)
        .then(data => data.json())
        .then(json => updateData(json))
    },[])

    return (
        <>
            { generateCards }
        </>
    );
}