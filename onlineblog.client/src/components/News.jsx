import { useEffect, useState } from "react"
import ImageComponent from "./ImageComponent"
import { deleteNews, getNews, getNewsByUser } from "../services/newsService"
import NewsCreation from './NewsCreation';
import { updateNews } from '../services/newsService';
import { PROFILE_URL } from '../services/commonService';
import ModalButton from "./ModalButton";

export const News = ({ id, text, imageStr, date, updateAction }) => {

    const updateNewsView = async (news) => {
      await updateNews(news);
      updateAction();
    }

    const deleteNewsView = async () => {
      await deleteNews(id);
      updateAction();
    }

    return (
        <div className='news-item card p-2 mt-4'>
            <div className="card-body news-actions">
                <div className="d-grid gap-2 d-md-flex justify-content-md-end">
                <ModalButton 
                    btnName={'Редактировать пост'}
                    modalContent={<NewsCreation id={id} 
                        oldtext={text} 
                        oldImage={imageStr} 
                        setAction={updateNewsView}/>} />
                <button className="btn btn-sm btn-outline-danger float-end" onClick={() => deleteNewsView()}>Удалить</button>
                </div>
                
            </div>
            <NewsView date={date} text={text} imageStr={imageStr}/>
        </div>
    )
}

const NewsView = ({ date, text, imageStr }) => {
    return (
        <div className="card-body">
            <div>
                <p className="card-text">{date}</p>
                <p className="card-text">{text}</p>
            </div>
            <div className="img-box text-center mt-2">
                <ImageComponent base64String={imageStr}/>
            </div>            
        </div>
    )
}


export const NewsProfileView = ({userId}) => {
    const [news, setNews] = useState([]);

    const getAllNews = async () => {
        if (userId === 0) return;
        const allNews = await getNewsByUser(userId);
        setNews(allNews);
    }

    useEffect ( ()=> {
        getAllNews();
    },[userId]);

    return (
        <div>
            {news.map((el, key) => {
                return <News key={key} 
                    id = {el.id}
                    text = {el.text} 
                    imageStr={el.image} 
                    date={el.postDate}
                    updateAction={getAllNews}
                />
            })}
        </div>
    )
}


export const NewsByUser = ({ userId }) => {
    const [news, setNews] = useState([]);

    const getAllNews = async () => {
        if (userId === 0) return;
        const allNews = await getNewsByUser(userId);
        setNews(allNews);
    }

    useEffect ( ()=> {
        getAllNews();
    },[userId]);

    return (
        <div>
            {news.map((el, key) => {
                return <NewsView date={el.postDate} text={el.text} imageStr={el.image}/>
            })}
        </div>
    )
}

export const NewsForUser = () => {
    const [news, setNews] = useState([]);

    const getAllNews = async () => {
        const allNews = await getNews();
        setNews(allNews);
    }

    useEffect ( ()=> {
        getAllNews();
    },[userId, updateUser]);

    return (
        <div>
            {news.map((el, key) => {
                return <NewsView date={el.postDate} text={el.text} imageStr={el.image}/>
                return <News key={key} 
                    id = {el.id}
                    text = {el.text} 
                    imageStr={el.image} 
                    date={el.postDate}
                    updateAction={getAllNews}
                />
            })}
        </div>
    )
}