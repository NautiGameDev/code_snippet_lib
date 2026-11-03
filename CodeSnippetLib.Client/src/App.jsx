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
import Home from './Pages/Home';
import LandingPage from './Pages/LandingPage';

//Admin Dashboard
import DbHome from './Pages/Dashboard/DbHome';
import Write from './Pages/Dashboard/Write';
import ActivityLogs from './Pages/Dashboard/ActivityLogs';

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
                <Route path="/Dashboard" element={<Dashboard />}>
                    <Route index element={<DbHome />} />
                    <Route path="new-post" element={<Write />} />
                    <Route path="activity-logs" element={<ActivityLogs />} />
                </Route>                
            </Routes>
        </div>
    );
}

export default App;