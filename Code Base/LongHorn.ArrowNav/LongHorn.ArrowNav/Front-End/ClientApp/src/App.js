import React, { Component } from 'react';
import { BrowserRouter as Router } from "react-router-dom";
import { Routes, Route } from "react-router-dom";
import { Home } from "./components/pages/Home";
import { NavBar } from "./components/NavBar";
import { Login } from "./components/pages/Login";
import { Register } from "./components/pages/Register";
import { WellnessHub } from "./components/pages/WellnessHub";
import { ConfirmationPage } from "./components/pages/ConfirmationPage";
import { UserHome } from "./components/pages/UserHome";
import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <div>
                <Router>
                    <NavBar />
                    <Routes>
                        <Route path="/" element={<Home />} />
                        <Route path="/account" element={<Login />} />
                        <Route path="/wellnesshub" element={<WellnessHub />} />
                        <Route path="/account/register" element={<Register />} />
                        <Route path="/account/confirmationpage" element={<ConfirmationPage />} />
                        <Route path="/account/userhome" element={<UserHome />}/>
                    </Routes>

                </Router>
            </div>
        );
    }
}