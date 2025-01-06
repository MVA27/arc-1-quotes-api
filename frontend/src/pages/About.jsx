import { Avatar, Text, Flex, Button, Link } from "@radix-ui/themes";
import { GitHubLogoIcon } from "@radix-ui/react-icons"

export default function About(){
    return (
        <>
            <Flex 
                direction="column" 
                align="center"
                className="about"
                m="5"
                >

                <Avatar
                    src="https://images.unsplash.com/photo-1502823403499-6ccfcf4fb453?&w=256&h=256&q=70&crop=focalpoint&fp-x=0.5&fp-y=0.3&fp-z=1&fit=crop"
                    fallback="M"
                    size="9"
                    radius="full"
                />

                <Text
                    mt="5"
                    align="center"
                    size="9"
                > 
                    Mehul Anvekar 
                </Text>

                <Text
                    mt="5"
                    mr="9"
                    ml="9"
                    >Lorem ipsum dolor sit amet consectetur adipisicing elit. Optio deleniti inventore ducimus esse laudantium eveniet nisi quo, neque maiores aut explicabo quasi vitae libero, corrupti sed ab eligendi tempore molestiae!
                </Text>

                <Button className="button-primary" mt="5">
                    <GitHubLogoIcon /> GitHub
                </Button>
            </Flex>

        </>
    );
}