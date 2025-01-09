import { Avatar, Text, Flex, Button, Container } from "@radix-ui/themes";
import { GitHubLogoIcon } from "@radix-ui/react-icons"
import { TEXT } from "../constants/text";
import profile from "../assets/images/profile.png"

export default function About(){
    return (
        <>
            <Container size="4">
                <Flex 
                    direction="column" 
                    align="center"
                    className="about"
                    m="7"
                    >

                    <Avatar
                        src={profile}
                        fallback="M"
                        size="9"
                        radius="full"
                    />

                    <Text
                        mt="7"
                        align="center"
                        size="9"
                    > 
                        {TEXT.NAME}
                    </Text>

                    <Text
                        mt="7"
                        align="center"
                    >
                        {TEXT.DESC_PARA1}
                        <br/>
                        <br/>
                        {TEXT.DESC_PARA2}

                    </Text>

                    <Button className="button-primary" mt="7">
                        <GitHubLogoIcon /> GitHub
                    </Button>
                </Flex>
            </Container>
        </>
    );
}