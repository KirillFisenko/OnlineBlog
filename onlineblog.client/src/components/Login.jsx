import { useState } from "react"
import { SIGNUP_URL, getToken } from "../services/commonService";


const Login = () => {
    const [username, setUserName] = useState();
    const [password, setPassword] = useState();

    const enterClick = () => {
        getToken(username, password);
    }

    const registerBtnClick = ()=> {
        window.location.href = SIGNUP_URL;
    }

    return (
        <div>
            <p>Login</p>
             <input type='text' onChange={e => setUserName(e.target.value)}/>
            <p>Password</p>
            <input type='password' onChange={e => setPassword(e.target.value)}/>
            <button className="btn btn-primary" onClick={enterClick}>Enter</button>
            <button className="btn btn-link" onClick={registerBtnClick}>Sign up</button>
        </div>
    );
}

export default Login;