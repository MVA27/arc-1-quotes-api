import { Box, Card, Flex, Avatar, Text } from "@radix-ui/themes";

export default function QuoteCard({quote, author, image}){
    return (
        <Box>
            <Card className="quote-card">
                <Flex gap="3" align="center">

                    <Avatar
                        size="7"
                        src={image}
                        radius="full"
                        fallback="T"
                    />

                    <Box>
                        <Text as="div" size="2" weight="bold">
                            {quote}
                        </Text>

                        <Text as="div" size="2" className="card-author">
                            {author}
                        </Text>
                    </Box>
                    
                </Flex>
            </Card>
        </Box>
    );
}