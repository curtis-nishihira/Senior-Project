import React, { useState } from "react";
import { NavLink } from "react-router-dom";
import "./NavBar.css";

export const NavBar = () => {
    const [click, setClick] = useState(false);
    const cookieKey = process.env.REACT_APP_COOKIE_KEY;

    const handleClick = () => setClick(!click);
    const doesCookieExist = () => {
        if (document.cookie != "") {
            var cookieExist = false;
            var decodedCookies = decodeURIComponent(document.cookie);
            var listOfCookies = decodedCookies.split("; ");
            for (var i = 0; i < listOfCookies.length; i++)
            {
                let temp = listOfCookies[i].split("=");
                if (temp[0] == cookieKey)
                {
                    cookieExist = true;
                    break;
                }
            }
            if (cookieExist)
            {
                return "/account/userhome";
            }
            else
            {
                return "/account"
            }
        }
        else {
            
            return "/account";
        }
    }

    return (
        <div>
            <nav className="navbar">
                <div className="nav-container">
                    <NavLink to="/" className="nav-logo"> ArrowNav </NavLink>

                    <ul className={click ? "nav-menu active" : "nav-menu"}>
                        <li className="nav-item">
                            <NavLink
                                to="/"
                                className={(navData) => navData.isActive ? "active" : "nav-links"}
                                onClick={handleClick}>
                                Home
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink to={doesCookieExist()} className={(navData) => navData.isActive ? "active" : "nav-links"}
                                onClick={handleClick}>
                                Account
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink to="/wellnesshub" className={(navData) => navData.isActive ? "active" : "nav-links"}
                                onClick={handleClick} >
                                Wellness Hub
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink to="/reward" className={(navData) => navData.isActive ? "active" : "nav-links"}
                                onClick={handleClick} >
                                Rewards
                            </NavLink>
                        </li>
                    </ul>
                    <div className="nav-icon" onClick={handleClick}>
                        <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#collapsibleNavbar">
                            <span className="navbar-toggler-icon"></span>
                        </button>
                    </div>
                </div>
            </nav>

        </div>
    );
}


export default NavBar;