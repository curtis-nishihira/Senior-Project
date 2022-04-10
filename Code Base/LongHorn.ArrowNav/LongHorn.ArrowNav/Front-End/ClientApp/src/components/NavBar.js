import React, { useState } from "react";
import { NavLink } from "react-router-dom";
import "./NavBar.css";

export const NavBar = () => {
    const [click, setClick] = useState(false);

    const handleClick = () => setClick(!click);

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
                            <NavLink to="/schedule" className={(navData) => navData.isActive ? "active" : "nav-links"}
                                onClick={handleClick}>
                                Schedule
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink to="/account" className={(navData) => navData.isActive ? "active" : "nav-links"}
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
                            <NavLink to="/rewards" className={(navData) => navData.isActive ? "active" : "nav-links"}
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