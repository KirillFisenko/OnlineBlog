import { PROFILE_URL } from '../services/commonService';
import { createNews } from '../services/newsService';
import ImageComponent from './ImageComponent';
import ModalButton from './ModalButton';
import { NewsByUser, NewsProfileView } from './News';
import NewsCreation from './NewsCreation';

const UserView = ({ user, isProfile }) => {

    const addNewNews = async (news) => {
        await createNews(news);
        window.location.href = PROFILE_URL;
    }

    return (
        <div>
            <h2>{user.lastName} {user.firstName}</h2>            
            <div style={
                {
                    display: 'flex',
                    flexDirection: 'row',
                    justifyContent: 'center'
                }}>
                <div className='image-box' style={{ width: '50%' }}>
                    <ImageComponent base64String={user.photo} />
                </div>
                <div className='user-data' style={{ margin: '0 10%' }}>
                    <p>Почта: {user.email}</p>
                    <p>О себе: {user.description}</p>
                    <div style={
                        {
                            display: 'flex',
                            justifyContent: 'space-around'
                        }}>
                    </div>
                </div>
            </div>

            {isProfile ?
                <div>
                    <ModalButton
                        btnName={'Создать пост'}
                        modalContent={<NewsCreation id={0} oldtext={''} oldImage={''} setAction={addNewNews} />}
                        title={'Новый пост'} />
                    <NewsProfileView userId={user.id} />
                </div> :

                <NewsByUser userId={user.id} />}
        </div>
    )
}

export default UserView;