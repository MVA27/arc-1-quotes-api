import { Text } from "@radix-ui/themes"
import { TEXT } from "../constants/text";

export default function Home(){
    return (
        <div className="home-div">
            <Text 
                weight="bold"
                > {TEXT.APP_TITLE} </Text>
        </div>
    );
}