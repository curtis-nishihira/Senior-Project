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
import Popup from "./components/pages/Popup";
import Privacy from "./components/pages/Privacy";
import { ProtectedRoutes } from "./components/ProtectedRoutes";
import WellnessHubPhysicalMain from './components/pages/WellnessHubPhysicalMain';
import WellnessPhysicalRecreation from './components/pages/WellnessPhysicalRecreation';
import WellnessPhysicalMedical from './components/pages/WellnessPhysicalMedical';
import WellnessHubBMAC from './components/pages/WellnessHubBMAC';
import WellnessHubMentalMain from './components/pages/WellnessHubMentalMain';
import WellnessMentalCAPS from './components/pages/WellnessMentalCAPS';
import WellnessHydrationReminder from './components/pages/WellnessHydrationReminder';
import './custom.css'

export default class App extends Component {
    static displayName = App.name;
    constructor(props) {
        super(props);
        this.state = {
            isOpen: true
        };
    }
    togglePopup = () => {
        this.setState({ isOpen: !this.state.isOpen })
    }

    render() {
        return (
            <>
                <div>
                    <Router>
                        <NavBar />
                        <Routes>
                            <Route path="/" element={<Home />} />
                            <Route path="/account" element={<Login />} />
                            <Route path="/wellnesshub" element={<WellnessHub />} />
                              <Route path="/wellnesshub/wellnesshubphysicalmain" element={<WellnessHubPhysicalMain />} />
                           <Route path="/wellnesshub/wellnesshubmentalmain" element={<WellnessHubMentalMain />} />
                          
                            <Route path="/wellnesshub/wellnesshubmentalmain/wellnessmentalcaps" element={<WellnessMentalCAPS />} />
                            <Route path="/wellnesshub/wellnesshubphysicalmain/wellnessphysicalrecreation" element={<WellnessPhysicalRecreation />} />
                          <Route path="/wellnesshub/wellnesshubphysicalmain/wellnessphysicalmedical" element={<WellnessPhysicalMedical />} />
                          <Route path="/wellnesshub/wellnesshubbmac" element={<WellnessHubBMAC />} />
                            <Route path="/account/register" element={<Register />} />
                            <Route path="/account/confirmationpage" element={<ConfirmationPage />} />

                            <Route element={<ProtectedRoutes />}>
                                <Route path="/account/userhome" element={<UserHome />} />
                                <Route path="/wellnesshydrationreminder" element={<WellnessHydrationReminder />} />
                            </Route>
                        </Routes>
                    </Router>
                </div>
                <div>
                    {this.state.isOpen && <Popup
                        content={<>
                            <Privacy handleCloser={this.togglePopup} />
                        </>}
                        handleClose={this.togglePopup}
                    />}
                </div>
            </>
        );
    }
}