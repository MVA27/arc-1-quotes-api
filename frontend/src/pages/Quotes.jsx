import QuoteCard from "../components/QuoteCard";
export default function Quotes(){

    let quotes = [ 
                 {quote: 'Be yourself; everyone else is already taken', author: 'Oscar Wilde', image: 'https://images.gr-assets.com/authors/1673611182p8/3565.jpg'},
                 {quote: 'Two things are infinite: the universe and human stupidity; and I`m not sure about the universe', author: 'Albert Einstein', image: 'https://images.gr-assets.com/authors/1429114964p8/9810.jpg'}
               ];

    const generateCards = quotes.map(item => QuoteCard(item));

    return (
        <>
            
            { generateCards }

        </>
    );
}