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

    const  deleteNewsView = async () => {
        await deleteNews(id);
        updateAction();
    }

    return (
        <div className='news-item'>
            <div className="news-actions">
                <ModalButton 
                    btnName = {'Edit post'}
                    modalContent = {<NewsCreation id = {id} 
                        oldtext={text} 
                        oldImage={imageStr} 
                        setAction={updateNewsView}/>} 
                    title = {'New post'}/>
                <button className="btn btn-danger" onClick={() => deleteNewsView()}>Delete</button>
            </div>
            <NewsView date={date} text={text} imageStr={imageStr}/>
        </div>
    )
}

const NewsView = ({date, text, imageStr}) => {
    return (
        <div>
            <div className="img-box">
                <ImageComponent base64String={imageStr}/>
            </div>
            <div>
                <p>{date}</p>
                <p>{text}</p>
            </div>
        </div>)
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