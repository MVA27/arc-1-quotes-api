import Nav from "../components/Nav.jsx"
import { Outlet } from "react-router-dom"
import "../main.css"

function Container() {

  return (
    <div className="dark">
      <Nav/>
      <Outlet />
    </div>
  )
  
}

export default Container
