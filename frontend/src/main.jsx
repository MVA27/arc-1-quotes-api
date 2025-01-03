import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'

import { Theme } from "@radix-ui/themes"
import { Route, createBrowserRouter, createRoutesFromElements, RouterProvider} from 'react-router-dom';
import "@radix-ui/themes/styles.css";

import Home from "./pages/Home.jsx"
import Quotes from "./pages/Quotes.jsx"
import About from "./pages/About.jsx"

import Container from './pages/Container.jsx';
import { ROUTES } from './constants/routes.js';


const router = createBrowserRouter(
  createRoutesFromElements(
    <>
      <Route path={ROUTES.HOME} element={<Container />}>

        <Route index element={ <Home/> } />

        <Route 
          path={ROUTES.QUOTES} element={ <Quotes/> } 
          loader = {
            async () => {

              const endpoint = import.meta.env.VITE_API_URL;

              const response = await fetch(endpoint + "/api/quotes");
              const json = await response.json();
              return json;

            }
          }
        />

        <Route path={ROUTES.ABOUT} element={ <About /> } />
        
      </Route>
    </>
  )
);

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <Theme hasBackground={false}>

      <RouterProvider router={router}/>

    </Theme>
  </StrictMode>,
)
