import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Login from "./auth/Login";
import Register from "./auth/Register";
import { PcList } from "./pc/PcList";
import { UserPcList} from "./pc/UserPcList";
import AddPcForm from "./pc/PcAddForm";
import EditPcForm from "./pc/PcEditForm";
import AddPerformanceForm from "./pcPerformance/AddPerformanceForm";

export const ApplicationViews = ({ isLoggedIn }) => {
    return (
        <main>
            <Routes>
                <Route path="/">
                    <Route
                        index
                        element={isLoggedIn ? <PcList/> : <Navigate to="/login" />}
                    />
                    <Route path="login" element={<Login />}/>
                    <Route path="register" element={<Register />}/>

                    <Route path="userpcs" element={isLoggedIn ? <UserPcList /> : <Navigate to="/login" />} />
                    <Route path="addpc" element={isLoggedIn ? <AddPcForm /> : <Navigate to="/login" />} />
                    <Route path="editpc/:id" element={isLoggedIn ? <EditPcForm /> : <Navigate to="/login" />} />
                    <Route path="addperformance/:id" element={isLoggedIn ? <AddPerformanceForm /> : <Navigate to="/login" />} />
                </Route>
            </Routes>
        </main>
    )
}