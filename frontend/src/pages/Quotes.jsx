import QuoteCard from "../components/QuoteCard";
import { useState } from "react";
import { Box , Spinner, Flex, ScrollArea, Select } from '@radix-ui/themes'
import { selectionItems } from '../constants/selectionItems';
import { useLoaderData, useNavigation } from "react-router-dom";

export default function Quotes(){

    const data = useLoaderData();
    const navigation = useNavigation()
    const [type , setType ] = useState(0);

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

                    display: navigation == 'loading' ? 'flex' : undefined,
                    justifyContent: navigation == 'loading' ? 'center' : undefined,
                    alignItems:  navigation == 'loading' ? 'center' : undefined
                }
            }>
                { 
                    navigation == 'loading'
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