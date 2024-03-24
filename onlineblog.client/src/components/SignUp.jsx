import { LOGIN_URL } from "../services/commonService"
import { createUser } from "../services/usersService"
import UserProfileCreation from "./UserProfileCreation"


const SignUp = () => {

    const userDefault = {
        
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        description: '',
        photo: '',
    }

    const signupAction = (newUser)=> {
        createUser(newUser);
    }

    const openLoginPage = ()=> {
        window.location.href = LOGIN_URL;
    }
    return (
        <div>
            <UserProfileCreation user={userDefault} setAction = {signupAction}/>
            <button className="btn btn-link" onClick={openLoginPage}>Sign in</button>
        </div>
    )
}

export default SignUp;