import './App.css';
import { Routes, Route } from 'react-router-dom';
import Login from "./Pages/Login";
import Welcome from "./Pages/Welcome";
import SessionExpired from './Pages/SessionExpired';
import Unauthenticated from "./Pages/Unauthenticated";
import Dashboard from './Pages/Dashboard';
import Register from './Pages/Register';
import NotFound from './Pages/NotFound';
import AccountConfirmation from './Pages/AccountConfirmation';
import RequestPasswordReset from './Pages/RequestPasswordReset';
import ConfirmPasswordReset from './Pages/ConfirmPasswordReset';
import ActivityLogs from './Pages/ActivityLogs';
import Home from './Pages/Home';
import LandingPage from './Pages/LandingPage';

function App() {
    return (
        <div className="app-container">
            <Routes>
                <Route path="/" element={<Home />}>
                    <Route index element={<LandingPage />} />
                    <Route path="login" element={<Login />} />
                    <Route path="register" element={<Register />} />
                    <Route path="welcome/:username" element={<Welcome />} />
                    <Route path="session-expired" element={<SessionExpired />} />
                    <Route path="unauthenticated" element={<Unauthenticated />} />
                    <Route path="confirm-email" element={<AccountConfirmation />} />
                    <Route path="request-password-reset" element={<RequestPasswordReset />} />
                    <Route path="reset-password" element={<ConfirmPasswordReset />} />
                    <Route path="*" element={<NotFound />} />
                </Route>
                <Route path="dashboard/:username" element={<Dashboard />} />
                <Route path="admin-logs" element={<ActivityLogs />} />
                
            </Routes>
        </div>
    );
}

export default App;