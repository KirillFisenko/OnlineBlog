import ImageComponent from './ImageComponent';
import { NewsByUser } from './News';
import NewsCreation from './NewsCreation';
import ModalButton from './ModalButton';
import { PROFILE_URL } from '../services/commonService';
import { createNews } from "../services/newsService";

const UserView = ({ user }) => {

    const addNewNews = async (news) => {
        await createNews(news);
        window.location.href = PROFILE_URL;
    }

    return (
        // отображения страницы профиля
        <div>
            <h2>{user.firstName} {user.lastName}</h2>
            <div>
                <div>
                    <div>
                        <ImageComponent base64String={user.photo} />
                    </div>
                    <p><strong>Имя:</strong> {user.firstName}</p>
                    <p><strong>Фамилия:</strong> {user.lastName}</p>
                    <p><strong>Email:</strong> {user.email}</p>
                    <p><strong>О себе:</strong> {user.description}</p>
                </div>
            </div>
            <ModalButton btnName={'Создать пост'} modalContent={<NewsCreation id={0} oldText={''} oldImage={''} setAction={addNewNews} />} title={'Создать пост'} />
            {<NewsByUser userId={user.id} />}
        </div>
    )
}

export default UserView;