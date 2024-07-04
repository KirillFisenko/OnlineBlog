import { useState } from "react";
import { SIGNUP_URL, getToken } from "../services/commonService";
import "../Login.css";

const Login = () => {
    const [username, setUserName] = useState("");
    const [password, setPassword] = useState("");
    const [errorMessage, setErrorMessage] = useState("");
    const [successMessage, setSuccessMessage] = useState("");

    const enterClick = async () => {
        try {
            await getToken(username, password);
            setErrorMessage("");
            setSuccessMessage("Успешный вход!");
        } catch {
            setErrorMessage("Неверные логин или пароль.");
        }
    };

    const registerBtnClick = () => {
        window.location.href = SIGNUP_URL;
    };

    return (
        <div className="login-container">
            <img className="mb-4 rounded" src="src\orig.jpg" alt="" width="122" height="107" />
            <h1 className="h3 mb-3 fw-normal">Онлайн блог</h1>

            <div className="mb-4 form-floating">
                <input type="email" className="form-control" id="floatingInput" placeholder="name@example.com" onChange={e => setUserName(e.target.value)} />
                <label>Email</label>
            </div>
            <div className="form-floating">
                <input type="password" className="form-control" id="floatingPassword" placeholder="Password" onChange={e => setPassword(e.target.value)} />
                <label>Пароль</label>
            </div>

            {errorMessage && <div className="text-danger mb-2">{errorMessage}</div>}
            {successMessage && <div className="text-success mb-2">{successMessage}</div>}

            <div className="form-check text-start my-3">
                <input className="form-check-input" type="checkbox" value="remember-me" id="flexCheckDefault" />
                <label className="form-check-label">Запомнить меня</label>
            </div>
            <button className="btn btn-primary w-100 py-2" onClick={enterClick}>Войти</button>
            <button className="btn btn-link" onClick={registerBtnClick}>Зарегистрироваться</button>
        </div>
    );
};

export default Login;
