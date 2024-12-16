import Nav from "../components/Nav.jsx"
import { Outlet } from "react-router-dom"

function Container() {

  return (
    <>
      <Nav/>
      <Outlet/>
    </>
  )
}

export default Container
