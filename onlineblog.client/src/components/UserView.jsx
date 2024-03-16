import ImageComponent from './ImageComponent';
import { NewsByUser } from './News';
import './UserProfile.css';

const UserView = ({ user }) => {
    return (
        // отображения страницы профиля
        <div>
            <h2>{user.firstName} {user.lastName}</h2>
            <div className="user-profile-container" style={{ display: 'flex', flexDirection: 'row' }}>
                <div className="user-profile-container" style={{ width: '50%' }}>
                    <div className="image=box">
                        <ImageComponent base64String={user.photo} />
                    </div>
                    <p><strong>Имя:</strong> {user.firstName}</p>
                    <p><strong>Фамилия:</strong> {user.lastName}</p>
                    <p><strong>Email:</strong> {user.email}</p>
                    <p><strong>О себе:</strong> {user.description}</p>
                </div>
            </div>
            {<NewsByUser userId={user.id} />}
        </div>
    )
}

export default UserView;