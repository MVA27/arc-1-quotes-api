import { Box, Card, Flex, Avatar, Text, Skeleton } from "@radix-ui/themes";
import { TEXT } from "../constants/text"

export default function SkeletonCard(){
    return (
        <Card>
            
            <Flex gap="3" align="center">

                <Skeleton>
                    <Avatar
                        size="7"
                        src=""
                        radius="full"
                        fallback=""
                    />
                </Skeleton>

                <Box>
                    <Text as="div" size="2" weight="bold">
                        <Skeleton>{TEXT.DUMMY_QUOTE}</Skeleton>
                    </Text>

                    <Text as="div" size="2" className="card-author">
                        <Skeleton>{TEXT.DUMMY_FIRST_NAME}</Skeleton> <Skeleton>{TEXT.DUMMY_LAST_NAME}</Skeleton>
                    </Text>
                </Box>

            </Flex>            
            
        </Card>
    );
}