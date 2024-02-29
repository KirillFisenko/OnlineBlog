import { useState } from "react"
import { getToken } from "C:/Users/justi/source/repos/OnlineBlog/onlineblog.client/src/services/commonService";
import './Login.css';

// страница входа
const Login = () => {
    const [username, setUserName] = useState();
    const [password, setPassword] = useState();

    const enterClick = () => {
        getToken(username, password);
    }
    return (
        <div className="center-container">
            <div>
                <p>Login</p>
                <input type='text' onChange={e => setUserName(e.target.value)} />
                <p>Password</p>
                <input type='password' onChange={e => setPassword(e.target.value)} />
                <button className="btn btn-primary" onClick={enterClick}>Enter</button>
            </div>
        </div>
    );
}

export default Login;