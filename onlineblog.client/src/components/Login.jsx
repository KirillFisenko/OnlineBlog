import { useState } from "react"
import { getToken, SIGNUP_URL } from "C:/Users/justi/source/repos/OnlineBlog/onlineblog.client/src/services/commonService";
import './Login.css';


// страница входа
const Login = () => {
    const [username, setUserName] = useState();
    const [password, setPassword] = useState();

    const enterClick = () => {
        getToken(username, password);
    }

const registerBtnClick = () => {
    window.location.href = SIGNUP_URL;
}

    return (
        <div className="center-container">
            <div>
                <p>Логин</p>
                <input type='text' onChange={e => setUserName(e.target.value)} />
                <p>Пароль</p>
                <input type='password' onChange={e => setPassword(e.target.value)} />
                <button className="btn btn-primary" onClick={enterClick}>Войти</button>
                <button className="btn btn-link" onClick={registerBtnClick}>Зарегистрироваться</button>
            </div>
        </div>
    );
}

export default Login;