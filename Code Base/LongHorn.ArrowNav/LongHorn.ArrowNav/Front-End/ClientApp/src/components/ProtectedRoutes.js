import { Navigate, Outlet } from "react-router";

const useAuth = () => {
    var decodedCookies = decodeURIComponent(document.cookie);
    var listOfCookies = decodedCookies.split("; ");
    for (var i = 0; i < listOfCookies.length; i++) {
        let temp = listOfCookies[i].split("=");
        if (temp[0] == process.env.REACT_APP_COOKIE_KEY) {
            return true;
        }
    }
}

export const ProtectedRoutes = () => {
    const isAuth = useAuth();
    return isAuth ? <Outlet /> : <Navigate to="/account" />
}

export default ProtectedRoutes

