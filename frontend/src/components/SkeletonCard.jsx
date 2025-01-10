import { Box, Card, Flex, Avatar, Text, Skeleton } from "@radix-ui/themes";

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
                        <Skeleton>Lorem ipsum dolor sit amet consectetur adipisicing elit. Qui nulla dicta error, quas delectus ad tempora, dolorem porro similique ipsa fugit voluptatem architecto exercitationem esse ducimus, aspernatur dolorum maiores maxime.</Skeleton>
                    </Text>

                    <Text as="div" size="2" className="card-author">
                        <Skeleton>FirstName</Skeleton> <Skeleton>LastName</Skeleton>
                    </Text>
                </Box>

            </Flex>            
            
        </Card>
    );
}