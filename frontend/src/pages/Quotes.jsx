import QuoteCard from "../components/QuoteCard";
import { useState, useEffect } from "react";
import { Flex, Spinner } from '@radix-ui/themes'

export default function Quotes(){

    let quotes = [ 
                 {id: 1, quote: 'Be yourself; everyone else is already taken', firstName: 'Oscar', lastName: 'Wilde', imageUrl: 'https://images.gr-assets.com/authors/1673611182p8/3565.jpg'},
                 {id: 2, quote: 'Two things are infinite: the universe and human stupidity; and I`m not sure about the universe', firstName: 'Albert', lastName: 'Einstein', imageUrl: 'https://images.gr-assets.com/authors/1429114964p8/9810.jpg'}
                ];

    const [data , updateData ] = useState(quotes);

    useEffect(()=>{

        const endpoint = import.meta.env.VITE_API_URL;

        fetch(endpoint + "/api/quotes")
            .then(data => data.json())
            .then(json => updateData(json))
   
    },[])

    return (
        <>
            { 
                data.map(item => ( < QuoteCard key={item.id} {...item} /> ) )
            }
        </>
    );
}