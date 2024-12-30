import { Box, Card, Flex, Avatar, Text } from "@radix-ui/themes";

export default function QuoteCard({quote, firstName, lastName, imageUrl}){
    return (
        <Box>

            <Card className="quote-card">

                <Flex gap="3" align="center">

                    <Avatar
                        size="7"
                        src={imageUrl}
                        radius="full"
                        fallback={firstName[0]}
                        color="gray"
                        variant="solid"
                        highContrast 
                    />

                    <Box>
                        <Text as="div" size="2" weight="bold">
                            {quote}
                        </Text>

                        <Text as="div" size="2" className="card-author">
                            {firstName} {lastName}
                        </Text>
                    </Box>
                    
                </Flex>
            </Card>
        </Box>
    );
}