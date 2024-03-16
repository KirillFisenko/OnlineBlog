import UserProfileCreation from "./UserProfileCreation"
import { createUser } from '../services/userService';

const SignUp = () => {

    const userDefault = {        
        firstName: '',
        lastName: '',
        email: '',     
        password: '',       
        description: '',            
        photo: ''
    }
    const signupAction = (newUser) => {
        createUser(newUser);
        // openLoginPage();
    }

    const openLoginPage = () => {
        window.location.href = 'login';
    }
    return (
        <div>
            <UserProfileCreation user={userDefault} setAction={signupAction} />
            <button className="btn btn-link" onClick={openLoginPage}>Отмена</button>
        </div>

    )
}

export default SignUp