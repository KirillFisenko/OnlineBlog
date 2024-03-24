import { useEffect, useState } from "react";
import { getNewsByUser, updateNews, deleteNews } from "../services/newsService";
import ImageComponent from "./ImageComponent";
import NewsCreation from "./NewsCreation";
import ModalButton from "./ModalButton"

export const News = ({id, text, imageStr, date, updateAction }) => {

    const updateNewsView = async (news) => {
        await updateNews(news);       
        updateAction();
    }

    const deleteNewsView = async () => {
        await deleteNews(id);        
        updateAction();
    }

    return (
        <div>
             <ModalButton btnName={'Редактировать пост'} modalContent={<NewsCreation id={id} oldText={text} oldImage={imageStr} setAction={updateNewsView} />} title={'Редактировать пост'} />
            <button type="button" onClick={deleteNewsView}>Удалить пост</button>
            <p>{date}</p>
            <p>{text}</p>
            <ImageComponent base64String={imageStr} />           
        </div>
    )
}

export const NewsByUser = ({ userId }) => {
    const [news, setNews] = useState([]);
    const [updateUser, setUpdateUser] = useState(0);

    const getAllNews = async () => {
        if (userId === 0) return;
        const allNews = await getNewsByUser(userId);
        setNews(allNews);
    }

    useEffect(() => {
        getAllNews();
    }, [userId, updateUser]);

    return (
        <div>
            {news.map((el, key) => {
                return <News key={key} id={el.id} text={el.text} imageStr={el.image} date={el.postDate} updateAction={setUpdateUser} />
            })}
        </div>
    )
}