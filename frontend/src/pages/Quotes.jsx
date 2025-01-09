import QuoteCard from "../components/QuoteCard";
import { useEffect, useState } from "react";
import { Box , Spinner, Flex, ScrollArea, Select } from '@radix-ui/themes'
import { selectionItems } from '../constants/selectionItems';

export default function Quotes(){

    const [data , setData] = useState(null);
    const [type , setType ] = useState(0);

    let filteredData = ()=>{
        if (type === 0) return data
        else return data.filter((item)=> item.type == type )
    }

    useEffect(()=>{

        const endpoint = import.meta.env.VITE_API_URL;
        
        fetch(endpoint + "/api/quotes")
        .then(res => res.json())
        .then(json => setData(json));

    },[]); //run once

    if (data == null) {
        return (
            <Box className="quotes-spinner">
                <Spinner className="spinner" size="3"/>
            </Box>
        );
    }


    return (
        
        <>
            <Select.Root defaultOpen={0} value={type} onValueChange={ setType } >

                <Select.Trigger />

                <Select.Content >

                    <Select.Group className="dark">

                        <Select.Label className="select-item-label">Type</Select.Label>
                        {
                            selectionItems.map(
                                (label, index)=> (<Select.Item className="select-item" key={index} value={index}> {label} </Select.Item>) 
                            )
                        }

                    </Select.Group>

                </Select.Content>

            </Select.Root>

            <Box style={ {width: '100%', height: '100vh'} } >
                { 
                    <ScrollArea type="always" scrollbars="vertical" style={{ height: '95%' }}>
                        <Flex direction="column"> 
                            { 
                                filteredData().map(item => ( < QuoteCard key={item.id} {...item} /> ) )
                            } 
                        </Flex>  
                    </ScrollArea>
                }
            </Box >
        
        </>

    );
}