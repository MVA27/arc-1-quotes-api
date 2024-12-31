import QuoteCard from "../components/QuoteCard";
import { useState, useEffect } from "react";
import { Box , Spinner, Flex, ScrollArea, Select } from '@radix-ui/themes'
import { selectionItems } from '../constants/selectionItems';

export default function Quotes(){

    const [ isLoading, updateLoadingStatus ] = useState(true);
    const [data , updateData ] = useState([{}]);
    const [type , setType ] = useState(0);

    function updateAllStates(json){

        updateData(json);
        updateLoadingStatus(false);

    }

    useEffect(()=>{

        const endpoint = import.meta.env.VITE_API_URL;

        fetch(endpoint + "/api/quotes")
            .then(data => data.json())
            .then(json => updateAllStates(json))

    },[])

    
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

            <Box style={ 
                {
                    width: '100%',
                    height: '100vh',

                    display: isLoading ? 'flex' : undefined,
                    justifyContent: isLoading ? 'center' : undefined,
                    alignItems:  isLoading ? 'center' : undefined
                }
            }>
                { 
                    isLoading 
                        ? <Spinner className="spinner" size="3"/> 
                        : <ScrollArea type="always" scrollbars="vertical" style={{ height: '95%' }}>
                            <Flex direction="column"> 
                                { 
                                    type == 0 
                                        ? data.map(item => ( < QuoteCard key={item.id} {...item} /> ) ) 
                                        : data
                                            .filter((item)=> item.type == type )
                                            .map(item => ( < QuoteCard key={item.id} {...item} /> ) )
                                    
                                } 
                            </Flex>  
                        </ScrollArea>
                }
            </Box >
        
        </>



    );
}