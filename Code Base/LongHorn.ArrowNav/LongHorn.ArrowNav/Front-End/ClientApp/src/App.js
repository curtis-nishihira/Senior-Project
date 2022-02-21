import React, { Component } from 'react';
import { BrowserRouter as Router } from "react-router-dom";
import { Routes, Route } from "react-router-dom";
import { Home } from "./components/pages/Home";
import { NavBar } from "./components/NavBar";
import { Login } from "./components/pages/Login";
import { WellnessHub } from "./components/pages/WellnessHub";;

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
                    </Routes>

                </Router>
            </div>
        );
    }
}
