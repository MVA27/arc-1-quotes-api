import { TabNav, Flex, Avatar} from "@radix-ui/themes";
import { Link, NavLink } from "react-router-dom";
import { useLocation } from 'react-router-dom'; 
import { ROUTES } from "../constants/routes"


export default function Nav(){

    const { pathname } = useLocation();

    return (
        <>
            <TabNav.Root justify="center">

                {/* / */}
                <TabNav.Link asChild active={pathname === ROUTES.HOME}>
                    <Link to={ROUTES.HOME}> Home </Link>
                </TabNav.Link>

                {/* /quotes */}
                <TabNav.Link asChild active={pathname === ROUTES.HOME + ROUTES.QUOTES}>
                    <Link to={ROUTES.HOME + ROUTES.QUOTES}> Quotes </Link>
                </TabNav.Link>   

                {/* /about */}
                <TabNav.Link asChild active={pathname === ROUTES.HOME + ROUTES.ABOUT}>
                    <Link to={ROUTES.HOME + ROUTES.ABOUT}> About </Link>
                </TabNav.Link>           

            </TabNav.Root>
        </>
    );
}